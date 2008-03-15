// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#include "stdafx.h"
#include "ChildProcessManager.h"
#include "EnumProc.h"
#include "Psapi.h"

ChildProcessManager::ChildProcessManager(void)
: m_piProcInfo(new PROCESS_INFORMATION())
{
	m_piProcInfo->dwProcessId = 0;
}

ChildProcessManager::~ChildProcessManager(void)
{
	TerminateChildProcess();
	delete m_piProcInfo;
}

bool ChildProcessManager::CreateChildProcess(HANDLE hChildStdOutWrite, HANDLE hChildStdInRead, char* szCmdline)
{
	assert(szCmdline != NULL);

	// Set up members of the PROCESS_INFORMATION structure.
	ZeroMemory( m_piProcInfo, sizeof(PROCESS_INFORMATION) );

	STARTUPINFO siStartInfo;
	// Set up members of the STARTUPINFO structure.
	ZeroMemory( &siStartInfo, sizeof(STARTUPINFO) );
	siStartInfo.cb = sizeof(STARTUPINFO);
	siStartInfo.hStdError = hChildStdOutWrite;
	siStartInfo.hStdOutput = hChildStdOutWrite;
	siStartInfo.hStdInput = hChildStdInRead;
	siStartInfo.dwFlags |= STARTF_USESTDHANDLES;
#ifndef _DEBUG_CLIENT
	siStartInfo.dwY = 10000;
	siStartInfo.dwX = 10000;
	siStartInfo.dwFlags |= STARTF_USEPOSITION;
#endif
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
			m_piProcInfo     // receives PROCESS_INFORMATION
		))
	{
		CloseHandle(m_piProcInfo->hProcess);
		m_piProcInfo->hProcess = NULL;
		CloseHandle(m_piProcInfo->hThread);
		m_piProcInfo->hThread = NULL;

		return true;
	}
	return false;
}

bool ChildProcessManager::IsSkypeRunning()
{
	return FindWindow("tSkMainForm.UnicodeClass", NULL) != NULL;
}

bool ChildProcessManager::ShouldChildProcessTerminate() const
{
	return !IsSkypeRunning();
}

bool ChildProcessManager::IsChildProcessCreated() const
{
	return m_piProcInfo->dwProcessId != 0;
}

void ChildProcessManager::TerminateChildProcess()
{
	if (IsChildProcessCreated())
	{
		HANDLE hChildProcess = OpenProcess(PROCESS_TERMINATE, FALSE, m_piProcInfo->dwProcessId);
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
	if (IsChildProcessCreated())
	{
		CProcessIterator itp;
		for (DWORD pid = itp.First(); pid; pid = itp.Next()) {
			if (pid == m_piProcInfo->dwProcessId)
			{
				return true;
			}
		}
	}
	return false;
}

HMODULE ChildProcessManager::GetChildProcessMainModule() const
{
	HMODULE ret(NULL);

	if (IsChildProcessCreated())
	{
		HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
				FALSE,
				m_piProcInfo->dwProcessId
			);
		if (hProcess != INVALID_HANDLE_VALUE)
		{
			char lpBaseName[MAX_PATH];

			CProcessModuleIterator pmi(m_piProcInfo->dwProcessId);

			for (HMODULE hModule = pmi.First(); hModule; hModule = pmi.Next())
			{
				if (GetModuleBaseName(hProcess, hModule, lpBaseName, MAX_PATH))
				{
					std::string moduleName(lpBaseName);
					if (moduleName.find(".exe") != std::string::npos)
					{
						ret = hModule;
						break;
					}
				}
			}

			CloseHandle(hProcess);
		}
	}

	return ret;
}
