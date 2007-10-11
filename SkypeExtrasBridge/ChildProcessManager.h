#pragma once

/**
 * Wrapper of the child process which is meant to host a .NET plugin for Skype.
 * @author: KBac
 */
class ChildProcessManager
{
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

private:
	PROCESS_INFORMATION* piProcInfo;
};
