// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

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
	try
	{
		if (m_plugin)
		{
			m_plugin->Open(OpenContext);
		}

		return S_OK;
	}
	catch(...)
	{
		return E_UNEXPECTED;
	}
}

STDMETHODIMP CSkypePluginB::ShowSettingsDlg(OLE_HANDLE WndOwner)
{
	try
	{
		if (m_plugin)
		{
			m_plugin->ShowSettingsDlg(WndOwner);
		}

		return S_OK;
	}
	catch(...)
	{
		return E_UNEXPECTED;
	}
}


STDMETHODIMP CSkypePluginB::Finalize()
{
	try
	{
		Shutdown();
		this->Release();

		return S_OK;
	}
	catch(...)
	{
		return E_UNEXPECTED;
	}
}


void CSkypePluginB::Shutdown()
{
	if (m_plugin)
	{
		m_plugin->Shutdown();
	}
}
