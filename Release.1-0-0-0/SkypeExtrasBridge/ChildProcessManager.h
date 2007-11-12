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
