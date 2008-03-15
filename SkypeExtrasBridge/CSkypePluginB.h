// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

#include "ComBase.h"
#include "ISkypePluginB.h"

struct ICollectionManager;
class _bstr_t;
class SkypeBridge;

/**
 * COM Object implementing ISkypePluginB interface as required by the SkypePM.
 * @Author: KBac
 */
class CSkypePluginB : public CComBase<> , public InterfaceImpl<ISkypePluginB>
{
public:
	CSkypePluginB(_bstr_t& ItemID, ICollectionManager* ColManager, _bstr_t& Params, bool shouldCallAddRef = true);
	virtual ~CSkypePluginB();

	// we however need to write code for queryinterface
	STDMETHOD(QueryInterface)(REFIID riid,LPVOID *ppv);

	// ISkypePluginB methods
    STDMETHOD(Open)(/* [in] */ POPEN_CONTEXT OpenContext);
    STDMETHOD(ShowSettingsDlg)( /* [in] */ OLE_HANDLE WndOwner);
    STDMETHOD(Finalize)(void);

	void Shutdown();
private:
	_bstr_t m_id;
	_bstr_t m_params;
	ICollectionManager* m_manager;
	SkypeBridge* m_plugin;
};
