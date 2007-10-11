#pragma once

class ChildProcessManager;
class _bstr_t;
class BSTRHelper;

/**
 * Anonymous Pipe encapulates IPC protocol used in the XtrsBrdg application. This includes:
 * - management of kernel handles
 * - seeting up and the management of the anonymous pipe
 * - creation of the child process hosting the .NET plugin
 * @author: KBac
 */
class AnonymousPipe
{
public:
	AnonymousPipe(const BSTRHelper& id);
	~AnonymousPipe(void);
public:
	/**
	 * Peeks the content of the pipe to avoid blocking calls when reading. 
	 * @Return Size that will be required for the buffer receiving the content.
	 */
	long ResponseSize();
	/**
	 * Reads the contents from the pipe. This is a blocking call therefore is it
	 * advised to use it in combination with ResponseSize. The buf parameter will receive the message body.
	 * @Return Message ID as defined by the protocol.
	 */
	long Read(_bstr_t& buf, long responseSize);
	/**
	 * Writes contensts of the payload into the pipe.
	 */
	void Write(_bstr_t& payload);

private:
	AnonymousPipe& operator = (AnonymousPipe& lhs);
	
	HANDLE hChildStdOutRead;
	HANDLE hChildStdOutWrite;
	HANDLE hChildStdInWrite;
	HANDLE hChildStdInRead;
	ChildProcessManager* m_processManager;

	const BSTRHelper& m_id;

	void Cleanup();
	void SafeCloseHandle(HANDLE* handle);
};