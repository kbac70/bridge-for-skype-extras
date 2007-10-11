#include "stdafx.h"
#include "AnonymousPipe.h"
#include "ChildProcessManager.h"
#include "BSTRHelper.h"
#include "PipePumpingThread.h"
#include "Protocol.h"



AnonymousPipe::AnonymousPipe(const BSTRHelper& id)
: m_id(id), hChildStdOutRead(NULL), hChildStdOutWrite(NULL), m_processManager(NULL)
{
	m_processManager = new ChildProcessManager();

	SECURITY_ATTRIBUTES saAttr;
	// Set the bInheritHandle flag so pipe handles are inherited.
	saAttr.nLength = sizeof(SECURITY_ATTRIBUTES);
	saAttr.bInheritHandle = TRUE;
	saAttr.lpSecurityDescriptor = NULL;

	// Create a pipe for the child process's STDOUT.
	if (CreatePipe(&hChildStdOutRead, &hChildStdOutWrite, &saAttr, BUFFER_SIZE))
	{
		// Ensure the read handle to the pipe for STDOUT is not inherited.
		SetHandleInformation( hChildStdOutRead, HANDLE_FLAG_INHERIT, 0);

		SetHandleInformation( hChildStdOutRead, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
		SetHandleInformation( hChildStdOutWrite, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
	}

	// Create a pipe for the child process's STDIN.
	if (CreatePipe(&hChildStdInRead, &hChildStdInWrite, &saAttr, 0))
	{
		// Ensure the write handle to the pipe for STDIN is not inherited.
		SetHandleInformation( hChildStdInWrite, HANDLE_FLAG_INHERIT, 0);

		SetHandleInformation( hChildStdInWrite, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
		SetHandleInformation( hChildStdInRead, HANDLE_FLAG_PROTECT_FROM_CLOSE, HANDLE_FLAG_PROTECT_FROM_CLOSE);
	}

	if (hChildStdOutRead != INVALID_HANDLE_VALUE &&
		hChildStdOutWrite!= INVALID_HANDLE_VALUE &&
		hChildStdInRead  != INVALID_HANDLE_VALUE &&
		hChildStdInWrite != INVALID_HANDLE_VALUE)
	{
		char szCmdline[BUFFER_SIZE];
		sprintf_s(szCmdline, 
				BUFFER_SIZE, 
				"C:/Documents and Settings/All Users/Application Data/Skype/Plugins/Plugins/%s/xtrshost.exe %s", 
				m_id.c_str(), 
#ifdef _DEBUG
				"DEBUG"
#else
				m_id.c_str()
#endif
			);

		if(!m_processManager->CreateChildProcess(hChildStdOutWrite, hChildStdInRead, szCmdline))
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

void AnonymousPipe::SafeCloseHandle(HANDLE* handle)
{
	if (*handle)
	{
		if (SetHandleInformation( *handle, HANDLE_FLAG_PROTECT_FROM_CLOSE, 0))
		{
			CloseHandle(*handle);
		}
		*handle = NULL;
	}
}

void AnonymousPipe::Cleanup()
{
	SafeCloseHandle(&hChildStdOutRead);
	SafeCloseHandle(&hChildStdOutWrite);
	SafeCloseHandle(&hChildStdInRead);
	SafeCloseHandle(&hChildStdInWrite);

	if (m_processManager)
		m_processManager->TerminateChildProcess();
}

long AnonymousPipe::ResponseSize()
{
	if (hChildStdOutRead)
	{
		DWORD dwRead = 0;
		
		char buffer[BUFFER_SIZE];
		ZeroMemory(buffer, BUFFER_SIZE);

		DWORD dwBytes;

		if (PeekNamedPipe(hChildStdOutRead, buffer, BUFFER_SIZE, &dwRead, &dwBytes, NULL))
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
	
long AnonymousPipe::Read(_bstr_t& buf, long responseSize)
{
	if (hChildStdOutRead)
	{
		DWORD dwRead = 0;
		
		char buffer[BUFFER_SIZE];
		ZeroMemory(buffer, BUFFER_SIZE);

		if (ReadFile(hChildStdOutRead, 
				buffer, 
				responseSize < BUFFER_SIZE ? responseSize : BUFFER_SIZE, 
				&dwRead, 
				NULL)
			)
		{	
			return Protocol::ExtractMessageID(buffer, buf);
		}

	}
	return Protocol::INVALID_ID;
}

void AnonymousPipe::Write(_bstr_t& payload)
{
	if(hChildStdOutWrite)
	{
		char buffer[BUFFER_SIZE];	
		int len = Protocol::EncodeMessage(buffer, payload);
		if (len > 0)
		{
			DWORD dwWritten = 0;
			if (!WriteFile(hChildStdInWrite, buffer, len, &dwWritten, NULL))
			{
				Cleanup();			
			}
		}
	}
}