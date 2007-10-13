#pragma once

class PipePumpingThread;

#include <string>
#include "ISkypePluginB.h"

/**
 * SkypeBridge is a class wrapping access to the child process. It uses PipePumpingThread to 
 * serialize all requests and awaits responses to be able to communicate results to the SkypePM engine.
 */
class SkypeBridge
{
public:
	SkypeBridge(const char* PluginID);
	~SkypeBridge();
	
	void Open(POPEN_CONTEXT Context);
	void ShowSettingsDlg(unsigned int WndOwner);
	void Shutdown();
private:
	std::string m_id;
	PipePumpingThread* m_thread;
};

