// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#include "stdafx.h"
#include "PipePumpingThread.h"
#include "AnonymousPipe.h"
#include "Protocol.h"
#include "ChildProcessManager.h"

#define PROCESS_TERMINATION_ALLOWANCE 1000

DWORD PipeManagingThread( LPVOID* pArguments )
{
	assert(pArguments != NULL);

	PipePumpingThread& t = *((PipePumpingThread*)pArguments);
	AnonymousPipe pipe = AnonymousPipe(t.GetID());

	if (pipe.GetChildProcessManager().IsChildProcessCreated())
	{
		std::string response;

		while(!t.isTerminated() || t.HasRequest())
		{
			if (t.HasRequest())
			{
				std::string payload(t.NextRequest());
				pipe.Write(payload);
			}
			else
			{
				const long responseSize = pipe.ResponseSize();
				if (responseSize > 0)
				{
					const long responseID = pipe.Read(response, responseSize);
					if(PipePumpingThread::INVALID_ID != responseID)
					{
						t.ReadResponse(responseID, response);
					}
				}
				else
				{
					if (!ChildProcessManager::IsSkypeRunning())
					{
						//pipe.Write(t.Abort());
					}
					Sleep(150);
				}
			}
		}

		Sleep(2 * PROCESS_TERMINATION_ALLOWANCE);
	}

	return 0;
}


PipePumpingThread::PipePumpingThread(std::string& id)
: m_hThread(NULL), m_terminated(false), m_dwThreadID(0), m_id(id), m_IsSuspended(true)
{
	m_hThread = CreateThread(NULL,0,(LPTHREAD_START_ROUTINE)PipeManagingThread,
		(LPVOID)this, BELOW_NORMAL_PRIORITY_CLASS | CREATE_SUSPENDED, &m_dwThreadID);

	char shutdown[BUFFER_SIZE];
	Protocol::EncodeShutdown(shutdown, m_id.c_str());
	char shutdownMsg[BUFFER_SIZE];
	Protocol::EncodeMessageID(shutdownMsg, msgID++, shutdown);
	m_abortRequest = std::string(shutdownMsg);
}

PipePumpingThread::~PipePumpingThread()
{
	m_terminated = true;

	if (m_hThread)
	{
		Resume();
		WaitForSingleObject(m_hThread, 3 * PROCESS_TERMINATION_ALLOWANCE);
		CloseHandle(m_hThread);
	}
}

std::string& PipePumpingThread::Abort()
{
	m_terminated = true;
	return m_abortRequest;
}


void PipePumpingThread::Resume()
{
	if (m_IsSuspended)
		ResumeThread(m_hThread);
}

long PipePumpingThread::WriteRequest(const std::string& Payload)
{
	Resume();

	long ret = msgID++;

	char buffer[BUFFER_SIZE];
	int len = Protocol::EncodeMessageID(buffer, ret, Payload.c_str());
	if (len > 0)
	{
		std::string p = std::string(buffer);
		m_outQ.push(p);
		return ret;
	}

	return PipePumpingThread::INVALID_ID;
}

std::string PipePumpingThread::SyncWriteRequest(const std::string& Payload)
{
	Resume();

	char buffer[BUFFER_SIZE];
	Protocol::EncodeResponseThreadAborted(buffer, 0, Payload.c_str());
	std::string ret(buffer);

	if (!m_terminated)
	{
		long requestID = WriteRequest(Payload);

		for(int i = 0; !m_terminated && i < 10; i++)
		{
			if (this->HasResponse(requestID))
			{
				return this->GetResponse(requestID);
			}
			Sleep(250);
		}
		if (!m_terminated)
		{
			Protocol::EncodeResponseTimeout(buffer, requestID, Payload.c_str());
			return std::string(buffer);
		}
	}
	return ret;
}

void PipePumpingThread::ReadResponse(const long id, const std::string& response)
{
	Resume();

	m_inQ.insert(std::pair<long, std::string>(id, response));
}

std::string PipePumpingThread::NextRequest()
{
	Resume();

	if (this->HasRequest())
	{
		std::string cmd = m_outQ.front();
		m_outQ.pop();
		return cmd;
	}
	return std::string();
}

bool PipePumpingThread::HasRequest()
{
	return !m_outQ.empty();
}

bool PipePumpingThread::HasResponse(const long id)
{
	return m_inQ.find(id) != m_inQ.end();
}

std::string PipePumpingThread::GetResponse(const long id)
{
	Resume();

	std::map<long, std::string>::iterator response = m_inQ.find(id);
	if (response != m_inQ.end())
	{
		std::string ret = (response)->second;
		m_inQ.erase(response);
		return ret;
	}
	return std::string();
}

const long PipePumpingThread::INVALID_ID = -1;
long PipePumpingThread::msgID = 1;

