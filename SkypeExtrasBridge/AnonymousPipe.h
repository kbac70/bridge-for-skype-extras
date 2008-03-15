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

class ChildProcessManager;
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
	AnonymousPipe(const std::string& PluginID);
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
	long Read(std::string& Buffer, long ResponseSize);
	/**
	 * Writes contensts of the payload into the pipe.
	 */
	void Write(std::string& Payload);
	/**
	 * @Return const reference to instance of ChildProcessManager
	 */
	const ChildProcessManager& GetChildProcessManager()	{ return *m_processManager; }
private:
	AnonymousPipe& operator = (AnonymousPipe& lhs);

	HANDLE m_hChildStdOutRead;
	HANDLE m_hChildStdOutWrite;
	HANDLE m_hChildStdInWrite;
	HANDLE m_hChildStdInRead;
	ChildProcessManager* m_processManager;

	const std::string& m_id;

	void Cleanup();
	void SafeCloseHandle(HANDLE* Handle);
};