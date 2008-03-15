// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

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

