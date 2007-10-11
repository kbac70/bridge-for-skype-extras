#include "stdafx.h"
#include "comutil.h"
#include "PipePumpingThread.h"
#include "AnonymousPipe.h"
#include "BSTRHelper.h"
#include "Protocol.h"

#define PROCESS_TERMINATION_ALLOWANCE 1000

DWORD PipeReadingThread( LPVOID* pArguments ) 
{
	PipePumpingThread& t = *((PipePumpingThread*)pArguments);
	AnonymousPipe pipe = AnonymousPipe(t.GetID());
	_bstr_t response;
	
	while(!t.isTerminated() || t.HasRequest())
	{
		if (t.HasRequest())
		{
			_bstr_t payload(t.NextRequest());
			pipe.Write(payload);

			Sleep(100);
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
		}

		Sleep(50);
	}

	Sleep(PROCESS_TERMINATION_ALLOWANCE);
	return 0;
}


PipePumpingThread::PipePumpingThread(_bstr_t& id)
: hThread(NULL), m_terminated(false), dwThreadID(0), m_id(new BSTRHelper(id)), isSuspended(true)
{
	hThread = CreateThread(NULL,0,(LPTHREAD_START_ROUTINE)PipeReadingThread,
		(LPVOID)this, BELOW_NORMAL_PRIORITY_CLASS | CREATE_SUSPENDED, &dwThreadID);
}

PipePumpingThread::~PipePumpingThread()
{
	m_terminated = true;

	if (hThread)
	{
		Resume();
		WaitForSingleObject(hThread, 3 * PROCESS_TERMINATION_ALLOWANCE);
		CloseHandle(hThread);
	}
	
	delete m_id;
}

void PipePumpingThread::Resume()
{
	if (isSuspended) 
		ResumeThread(hThread);
}

long PipePumpingThread::WriteRequest(const _bstr_t& payload)
{
	Resume();

	long ret = msgID++;
	
	char buffer[BUFFER_SIZE];	
	BSTRHelper msg = BSTRHelper(payload);
	int len = Protocol::EncodeMessageID(buffer, ret, msg.c_str());
	if (len > 0)
	{
		_bstr_t p = _bstr_t(buffer);
		m_outQ.push(p);	
		return ret;
	}
	
	return PipePumpingThread::INVALID_ID;
}

_bstr_t PipePumpingThread::SyncWriteRequest(const _bstr_t& payload)
{
	Resume();

	long requestID = WriteRequest(payload);

	for(int i = 0; i < 10; i++)
	{
		if (this->HasResponse(requestID))
		{
			return this->GetResponse(requestID);
		}
		Sleep(250);
	}
	return _bstr_t("ERROR|TIMEOUT");
}

void PipePumpingThread::ReadResponse(const long id, const _bstr_t& response)
{
	Resume();

	m_inQ.insert(std::pair<long, _bstr_t>(id, response));
}

_bstr_t PipePumpingThread::NextRequest()
{ 
	Resume();

	if (this->HasRequest())
	{
		_bstr_t cmd = m_outQ.front();
		m_outQ.pop();
		return cmd;
	}
	return _bstr_t(); 
}

bool PipePumpingThread::HasRequest() 
{ 
	return !m_outQ.empty(); 
}

bool PipePumpingThread::HasResponse(const long id)
{
	return m_inQ.find(id) != m_inQ.end();
}

_bstr_t PipePumpingThread::GetResponse(const long id)
{
	Resume();

	std::map<long, _bstr_t>::iterator response = m_inQ.find(id);
	if (response != m_inQ.end())
	{
		_bstr_t ret = (response)->second;
		m_inQ.erase(response);
		return ret;
	}
	return _bstr_t();
}

const long PipePumpingThread::INVALID_ID = -1;
long PipePumpingThread::msgID = 1;

