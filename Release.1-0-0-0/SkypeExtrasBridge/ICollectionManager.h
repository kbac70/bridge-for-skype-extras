#pragma once

#include "IHostUIServices.h"
#include <ComDef.h>

// {CC6387C6-B31A-4E7B-B49E-503F4D671C09}
_declspec(selectany) GUID CLSID_ICollectionManager = 
		{ 0xCC6387C6, 0xB31A, 0x4E7B, { 0xB4, 0x9E, 0x50, 0x3F, 0x4D, 0x67, 0x1C, 0x09 } };

interface __declspec(uuid("CC6387C6-B31A-4E7B-B49E-503F4D671C09"))  ICollectionManager : public IUnknown
{
public:
    STDMETHOD(InstallItem)( 
        /* [in] */ const LPWSTR ItemID,
        /* [out] */ BOOL *result) PURE;
    
    STDMETHOD(OpenItem)( 
        /* [in] */ const LPWSTR ItemID,
        /* [out] */ BOOL *result) PURE;
    
    STDMETHOD(PluginClosed)( 
        /* [in] */ const LPWSTR ItemID) PURE;
    
    STDMETHOD(PluginChanged)( 
        /* [in] */ const LPWSTR ItemID) PURE;
    
    STDMETHOD(ProtectFiles)( 
        /* [in] */ const LPWSTR ItemID) PURE;
    
    STDMETHOD(GetHostUIServices)( 
        /* [out] */ IHostUIServices **result) PURE;
private:
};

