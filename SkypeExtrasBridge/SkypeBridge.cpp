#include "stdafx.h"
#include "SkypeBridge.h"
#include "PipePumpingThread.h"
#include "BSTRHelper.h"
#include "Protocol.h"
#include "DotNetCheck.h"

SkypeBridge::SkypeBridge(_bstr_t& PluginID)
: m_id(new BSTRHelper(PluginID))
{
	m_thread = new PipePumpingThread(PluginID);
}

SkypeBridge::~SkypeBridge()
{
	delete m_thread;
	delete m_id;
}

void SkypeBridge::Open(POPEN_CONTEXT Context)
{
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
			m_id->c_str(), 
			Context->ContextType, 
			Participants.c_str(),
			ContextRef.c_str(),
			UniqueID.c_str(),
			URIParams.c_str()
		);

	if(len > 0)
	{
		_bstr_t payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}

void SkypeBridge::ShowSettingsDlg(unsigned int WndOwner)
{
	char buffer[BUFFER_SIZE];
	int len = Protocol::EncodeShowSettingsDlg(buffer, m_id->c_str(), WndOwner);

	if(len > 0)
	{
		_bstr_t payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}

void SkypeBridge::Shutdown()
{
	char buffer[BUFFER_SIZE];
	int len = Protocol::EncodeShutdown(buffer, m_id->c_str());

	if(len > 0)
	{
		_bstr_t payload(buffer);
		m_thread->SyncWriteRequest(payload);
	}
}