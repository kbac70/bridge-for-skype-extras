#include "stdafx.h"
#include "ChildProcessManager.h"
#include "BSTRHelper.h"

ChildProcessManager::ChildProcessManager(void)
: piProcInfo(new PROCESS_INFORMATION())
{
	piProcInfo->dwProcessId = 0;
}

ChildProcessManager::~ChildProcessManager(void)
{
	TerminateChildProcess();
	delete piProcInfo;
}

bool ChildProcessManager::CreateChildProcess(HANDLE hChildStdOutWrite, HANDLE hChildStdInRead, char* szCmdline)
{
	// Set up members of the PROCESS_INFORMATION structure.
	ZeroMemory( piProcInfo, sizeof(PROCESS_INFORMATION) );

	STARTUPINFO siStartInfo;
	// Set up members of the STARTUPINFO structure.
	ZeroMemory( &siStartInfo, sizeof(STARTUPINFO) );
	siStartInfo.cb = sizeof(STARTUPINFO);
	siStartInfo.hStdError = hChildStdOutWrite;
	siStartInfo.hStdOutput = hChildStdOutWrite;
	siStartInfo.hStdInput = hChildStdInRead;
	siStartInfo.dwFlags |= STARTF_USESTDHANDLES;
	siStartInfo.dwY = 10000;
	siStartInfo.dwX = 10000;
	siStartInfo.dwFlags |= STARTF_USEPOSITION;
	//siStartInfo.wShowWindow = SW_HIDE;
	//siStartInfo.dwFlags |= STARTF_USESHOWWINDOW;

	// Create the child process.
	if(CreateProcess(NULL,
			szCmdline,	   // command line
			NULL,          // process security attributes
			NULL,          // primary thread security attributes
			TRUE,          // handles are inherited
			0,             // creation flags
			NULL,          // use parent's environment
			NULL,          // use parent's current directory
			&siStartInfo,  // STARTUPINFO pointer
			piProcInfo     // receives PROCESS_INFORMATION
		))
	{
		 CloseHandle(piProcInfo->hProcess); 
		 piProcInfo->hProcess = NULL;
		 CloseHandle(piProcInfo->hThread);
		 piProcInfo->hThread = NULL;
		 return true;
	}
	return false;
}

void ChildProcessManager::TerminateChildProcess()
{
	if (piProcInfo->dwProcessId)
	{

		HANDLE hChildProcess = OpenProcess(PROCESS_TERMINATE, FALSE, piProcInfo->dwProcessId);
		if (INVALID_HANDLE_VALUE != hChildProcess)
		{
			// kill the probably unstable process
			if (TerminateProcess(hChildProcess, 255)> 0)
			{
				CloseHandle(hChildProcess);
			}
		}
	}
}

bool ChildProcessManager::IsChildProcessAvailable()
{
	if (piProcInfo->dwProcessId)
		{
		HANDLE hChildProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, piProcInfo->dwProcessId);
		if (INVALID_HANDLE_VALUE != hChildProcess)
		{
			return CloseHandle(hChildProcess) > 0;
		}
		piProcInfo->dwProcessId = 0;
	}
	return false;
}
