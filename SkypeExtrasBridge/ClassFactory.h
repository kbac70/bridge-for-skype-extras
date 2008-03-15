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
/**
 * Courtesy of Shoaib Ali
 */

// Creator class for Singleton class factories this class returns the pointer to the same
// object for multiple CreateObject requests ..
//
template<class comObj>
class CSingleCreator
{
protected:
	CSingleCreator():m_pObj(0) {};

	comObj *CreateObject()
	{
		if(!m_pObj)
		{
			m_pObj = new comObj;
		}
		return m_pObj;
	}
	comObj * m_pObj;
};

// Creator class for normal class factories this class returns the pointer to a new
// object for each CreateObject request ..
//
template<class comObj>
class CMultiCreator
{
protected:
	CMultiCreator():m_pObj(0) {};
	comObj *CreateObject()
	{
		return new comObj;
	}
	comObj * m_pObj;
};


// ClassFactory implementation using the classes all around us now the default creator
// class for classfactory is MultiCreator this class implements IClasFactory interface ....
//
template <class comObj, class creatorClass  = CMultiCreator < comObj > >
class CClassFactory :  public CComBase<>,public InterfaceImpl<IClassFactory>,public creatorClass
{
public:
	CClassFactory() {};
	virtual ~CClassFactory() {};

	STDMETHOD(QueryInterface)(REFIID riid,LPVOID *ppv)
	{
		*ppv = NULL;
		if(IsEqualIID(riid,IID_IUnknown) || IsEqualIID(riid,IID_IClassFactory))
		{
			*ppv = (IClassFactory *) this;
			_AddRef();
			return S_OK;
		}
		return E_NOINTERFACE;
	}

	STDMETHODIMP CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, LPVOID *ppvObj)
	{
		*ppvObj = NULL;
		if (pUnkOuter)
    		return CLASS_E_NOAGGREGATION;
		m_pObj = CreateObject();  // m_pObj is defined in creatorClass
		if (!m_pObj)
    		return E_OUTOFMEMORY;
		HRESULT hr = m_pObj->QueryInterface(riid, ppvObj);
		if(hr != S_OK)
		{
			delete m_pObj;
		}
		return hr;
	}

	STDMETHODIMP LockServer(BOOL) {	return S_OK; }  // not implemented
};