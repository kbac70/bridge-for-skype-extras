#include "stdafx.h"
#include "CSkypePluginB.h"
#include "ICollectionManager.h"
#include "comutil.h"
#include "SkypeBridge.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CSkypePluginB::CSkypePluginB(_bstr_t& ItemID, ICollectionManager* ColManager,
			 _bstr_t& Params, bool shouldCallAddRef)
	: m_manager (ColManager), m_id(ItemID), m_plugin(NULL), m_params(Params)
{
	if (shouldCallAddRef)
	{
		this->AddRef();
		m_plugin = new SkypeBridge(m_id);
	}
}

CSkypePluginB::~CSkypePluginB()
{
	if (m_plugin)
		delete m_plugin;

	m_manager->PluginClosed(m_id.GetBSTR());
	m_manager = NULL;
}

STDMETHODIMP CSkypePluginB::QueryInterface(REFIID riid,LPVOID *ppv)
{
	*ppv = NULL;
	if(IsEqualIID(riid,IID_IUnknown) || IsEqualIID(riid,__uuidof(ISkypePluginB)))
	{
		*ppv = (ISkypePluginB *) this;
		_AddRef();
		return S_OK;
	}
	return E_NOINTERFACE;
}

STDMETHODIMP CSkypePluginB::Open(POPEN_CONTEXT OpenContext)
{
	if (m_plugin)
	{
		m_plugin->Open(OpenContext);
	}
	return S_OK;
}

STDMETHODIMP CSkypePluginB::ShowSettingsDlg(OLE_HANDLE WndOwner)
{
	if (m_plugin)
	{
		m_plugin->ShowSettingsDlg(WndOwner);
	}
	return S_OK;
}


STDMETHODIMP CSkypePluginB::Finalize()
{
	if (m_plugin)
	{
		m_plugin->Shutdown();
	}
	this->Release();
	return S_OK;
}
