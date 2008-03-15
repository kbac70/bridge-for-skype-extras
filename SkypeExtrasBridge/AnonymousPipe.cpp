// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#include "stdafx.h"
#include "AnonymousPipe.h"
#include "ChildProcessManager.h"
#include "PipePumpingThread.h"
#include "Protocol.h"



AnonymousPipe::AnonymousPipe(const std::string& PluginID)
: m_id(PluginID), m_hChildStdOutRead(NULL), m_hChildStdOutWrite(NULL), m_processManager(NULL)
{
	m_processManager = new ChildProcessManager();

	SECURITY_ATTRIBUTES saAttr;
	// Set the bInheritHandle flag so pipe handles are inherited.
	saAttr.nLength = sizeof(SECURITY_ATTRIBUTES);
	saAttr.bInheritHandle = TRUE;
	saAttr.lpSecurityDescriptor = NULL;

	// Create a pipe for the child process's STDOUT.
	if (CreatePipe(&m_hChildStdOutRead, &m_hChildStdOutWrite, &saAttr, BUFFER_SIZE))
	{
		// Ensure the read handle to the pipe for STDOUT is not inherited.
		SetHandleInformation( m_hChildStdOutRead, HANDLE_FLAG_INHERIT, 0);

		SetHandleInformation( m_hChildStdOutRead, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
		SetHandleInformation( m_hChildStdOutWrite, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
	}

	// Create a pipe for the child process's STDIN.
	if (CreatePipe(&m_hChildStdInRead, &m_hChildStdInWrite, &saAttr, 0))
	{
		// Ensure the write handle to the pipe for STDIN is not inherited.
		SetHandleInformation( m_hChildStdInWrite, HANDLE_FLAG_INHERIT, 0);

		SetHandleInformation( m_hChildStdInWrite, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
		SetHandleInformation( m_hChildStdInRead, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
	}

	if (m_hChildStdOutRead != INVALID_HANDLE_VALUE &&
		m_hChildStdOutWrite!= INVALID_HANDLE_VALUE &&
		m_hChildStdInRead  != INVALID_HANDLE_VALUE &&
		m_hChildStdInWrite != INVALID_HANDLE_VALUE)
	{
		char szCmdline[BUFFER_SIZE];
		sprintf_s(szCmdline,
				BUFFER_SIZE,
				"C:/Documents and Settings/All Users/Application Data/Skype/Plugins/Plugins/%s/xtrshost.exe %s",
				m_id.c_str(),
#ifdef _DEBUG_CLIENT
				"DEBUG"
#else
				m_id.c_str()
#endif
			);

		if(!m_processManager->CreateChildProcess(m_hChildStdOutWrite, m_hChildStdInRead, szCmdline))
		{
			Cleanup();
		}
	}
}

AnonymousPipe::~AnonymousPipe()
{
	delete m_processManager;
	m_processManager = NULL;

	Cleanup();
}

void AnonymousPipe::SafeCloseHandle(HANDLE* Handle)
{
	assert(Handle != NULL);

	if (*Handle)
	{
		if (SetHandleInformation( *Handle, HANDLE_FLAG_PROTECT_FROM_CLOSE, 0))
		{
			CloseHandle(*Handle);
		}
		*Handle = NULL;
	}
}

void AnonymousPipe::Cleanup()
{
	SafeCloseHandle(&m_hChildStdOutRead);
	SafeCloseHandle(&m_hChildStdOutWrite);
	SafeCloseHandle(&m_hChildStdInRead);
	SafeCloseHandle(&m_hChildStdInWrite);

	if (m_processManager)
		m_processManager->TerminateChildProcess();
}

long AnonymousPipe::ResponseSize()
{
	if (m_hChildStdOutRead)
	{
		DWORD dwRead = 0;

		char buffer[BUFFER_SIZE];
		ZeroMemory(buffer, BUFFER_SIZE);

		DWORD dwBytes;

		if (PeekNamedPipe(m_hChildStdOutRead, buffer, BUFFER_SIZE, &dwRead, &dwBytes, NULL))
			if (dwBytes > 0)
		{
			char* c = buffer;
			int responseSize = 1;
			while(*c != '\0' && *c != '\n')
			{
				c++;
				responseSize++;
			}
			return responseSize > BUFFER_SIZE ? BUFFER_SIZE : responseSize;
		}
	}

	return 0;
}

long AnonymousPipe::Read(std::string& Buffer, long ResponseSize)
{
	if (m_hChildStdOutRead)
	{
		DWORD dwRead = 0;

		char buffer[BUFFER_SIZE];
		ZeroMemory(buffer, BUFFER_SIZE);

		if (ReadFile(m_hChildStdOutRead,
				buffer,
				ResponseSize < BUFFER_SIZE ? ResponseSize : BUFFER_SIZE,
				&dwRead,
				NULL)
			)
		{
			return Protocol::ExtractMessageID(buffer, Buffer);
		}

	}
	return Protocol::INVALID_ID;
}

void AnonymousPipe::Write(std::string& Payload)
{
	if(m_hChildStdOutWrite)
	{
		char buffer[BUFFER_SIZE];
		int len = Protocol::EncodeMessage(buffer, Payload);
		if (len > 0)
		{
			DWORD dwWritten = 0;
			if (!WriteFile(m_hChildStdInWrite, buffer, len, &dwWritten, NULL))
			{
				Cleanup();
			}
		}
	}
}