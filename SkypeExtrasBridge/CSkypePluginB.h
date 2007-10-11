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
private:
	_bstr_t m_id;
	_bstr_t m_params;
	ICollectionManager* m_manager;
	SkypeBridge* m_plugin;
};
