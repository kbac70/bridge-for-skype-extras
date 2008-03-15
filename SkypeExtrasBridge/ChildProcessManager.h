// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

#include <string>

/**
 * Wrapper of the child process which is meant to host a .NET plugin for Skype.
 * @author: KBac
 */
class ChildProcessManager
{
public:
	static bool IsSkypeRunning();
public:
	ChildProcessManager(void);
	~ChildProcessManager(void);
	/**
	 * Creates the child process passing the handle information and command line parameters.
	 * Handles are going to be inherited. It is responsibility of the caller to protect the handles.
	 * Command line is to include plugin id for the XstrHost validation
	 * @Returns true on success
     */
	bool CreateChildProcess(HANDLE hChildStdOutWrite, HANDLE hChildStdInRead, char* szCmdline);
	/**
	 * Terminates the child process forcefully and immediately. The communication protocol
	 * ensures that the child process has enough time to uninitialize on Shutdown. This method
	 * is meant to garbage collect unstable spawn child processes.
     */
	void TerminateChildProcess();
	/**
	 * Call this method to check if the child process is still available.
	 * @Returns true on success
     */
	bool IsChildProcessAvailable();
	/**
	 * @Return Process information of the child process
	 */
	const PROCESS_INFORMATION& GetProcessInfo() const { return *m_piProcInfo; }
	/**
	 * @Return True when there is no Skype instance running
	 */
	bool ShouldChildProcessTerminate() const;
	/**
	 *@Return true when m_piProcInfo contains valid process id
	 */
	bool IsChildProcessCreated() const;
	/**
	 *@Return main module handle of the child process
	 */
	HMODULE GetChildProcessMainModule() const;
private:
	PROCESS_INFORMATION* m_piProcInfo;
};
