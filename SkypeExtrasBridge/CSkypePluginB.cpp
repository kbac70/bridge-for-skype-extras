#include "stdafx.h"
#include "CSkypePluginB.h"
#include "ICollectionManager.h"
#include "comutil.h"
#include "BSTRHelper.h"
#include "SkypeBridge.h"

//singleton
CSkypePluginB* g_plugin;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CSkypePluginB::CSkypePluginB(_bstr_t& ItemID, ICollectionManager* ColManager,
			 _bstr_t& Params, bool shouldCallAddRef)
	: m_manager (ColManager), m_id(ItemID), m_plugin(NULL), m_params(Params)
{
	assert(ColManager != NULL);

	if (shouldCallAddRef)
	{
		this->AddRef();
	}
	BSTRHelper id = BSTRHelper(ItemID);
	m_plugin = new SkypeBridge(id.c_str());
	g_plugin = this;
}

CSkypePluginB::~CSkypePluginB()
{
	if (m_plugin)
		delete m_plugin;

	if (m_manager)
	{
		m_manager->PluginClosed(m_id.GetBSTR());
		m_manager = NULL;
	}
	g_plugin = NULL;
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
	Shutdown();
	this->Release();
	return S_OK;
}


void CSkypePluginB::Shutdown()
{
	if (m_plugin)
	{
		m_plugin->Shutdown();
	}
}
