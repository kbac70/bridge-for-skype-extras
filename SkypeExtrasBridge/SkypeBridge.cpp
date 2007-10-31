#include "stdafx.h"
#include "SkypeBridge.h"
#include "PipePumpingThread.h"
#include "BSTRHelper.h"
#include "Protocol.h"
#include "DotNetCheck.h"

SkypeBridge::SkypeBridge(const char* PluginID)
: m_id(PluginID), m_thread(NULL)
{
}

SkypeBridge::~SkypeBridge()
{
	delete m_thread;
}

bool SkypeBridge::IsDotNetAvailable()
{
	DotNetCheck dotNet;
	const bool isDotNetInstalled = dotNet.CheckIsInstalled();
	
	if(isDotNetInstalled)
	{
		if (!m_thread)
			m_thread = new PipePumpingThread(m_id);
	}
	else
	{
		delete m_thread;
		m_thread = NULL;
	}

	return isDotNetInstalled;
}

void SkypeBridge::Open(POPEN_CONTEXT Context)
{
	assert(Context != NULL);

	if (!IsDotNetAvailable())
		return;

	char buffer[BUFFER_SIZE];
	BSTRHelper Participants = BSTRHelper(Context->Participants);
	BSTRHelper ContextRef = BSTRHelper(Context->ContextRef);
	BSTRHelper UniqueID = BSTRHelper(Context->UniqueID);
	BSTRHelper URIParams = BSTRHelper(Context->URIParams);

	int len = Protocol::EncodeOpenRequest(buffer,
			m_id.c_str(), 
			Context->ContextType, 
			Participants.c_str(),
			ContextRef.c_str(),
			UniqueID.c_str(),
			URIParams.c_str()
		);

	if(len > 0)
	{
		std::string payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}

void SkypeBridge::ShowSettingsDlg(unsigned int WndOwner)
{
	if (!IsDotNetAvailable())
		return;

	char buffer[BUFFER_SIZE];
	int len = Protocol::EncodeShowSettingsDlg(buffer, m_id.c_str(), WndOwner);

	if(len > 0)
	{
		std::string payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}

void SkypeBridge::Shutdown()
{
	if (m_thread)
	{
		char buffer[BUFFER_SIZE];
		int len = Protocol::EncodeShutdown(buffer, m_id.c_str());

		if(len > 0)
		{
			std::string payload(buffer);
			m_thread->SyncWriteRequest(payload);
		}
	}
}