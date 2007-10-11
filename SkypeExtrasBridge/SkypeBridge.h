#pragma once

class _bstr_t;
class PipePumpingThread;
class BSTRHelper;

#include "ISkypePluginB.h"

/**
 * SkypeBridge is a class wrapping access to the child process. It uses PipePumpingThread to 
 * serialize all requests and awaits responses to be able to communicate results to the SkypePM engine.
 */
class SkypeBridge
{
public:
	SkypeBridge(_bstr_t& PluginID);
	~SkypeBridge();
	
	void Open(POPEN_CONTEXT Context);
	void ShowSettingsDlg(unsigned int WndOwner);
	void Shutdown();
private:
	BSTRHelper* m_id;
	PipePumpingThread* m_thread;
};

