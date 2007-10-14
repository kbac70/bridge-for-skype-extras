#include "stdafx.h"
#include "SkypeBridge.h"
#include "PipePumpingThread.h"
#include "BSTRHelper.h"
#include "Protocol.h"
#include "DotNetCheck.h"

SkypeBridge::SkypeBridge(const char* PluginID)
: m_id(PluginID)
{
	m_thread = new PipePumpingThread(m_id);
}

SkypeBridge::~SkypeBridge()
{
	delete m_thread;
}

void SkypeBridge::Open(POPEN_CONTEXT Context)
{
	assert(Context != NULL);

	if (Context->ContextType == ctNone)
	{
		DotNetCheck dotNet;
		dotNet.CheckIsInstalled();
	}

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
	char buffer[BUFFER_SIZE];
	int len = Protocol::EncodeShutdown(buffer, m_id.c_str());

	if(len > 0)
	{
		std::string payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}