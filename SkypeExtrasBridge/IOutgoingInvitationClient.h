// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

#include "ISkypePluginB.h"
#include <ComDef.h>

// {C7C10D93-F001-425E-A291-AE653D4E16E3}
_declspec(selectany) GUID CLSID_IOutgoingInvitationClient =
		{ 0xC7C10D93, 0xF001, 0x425E, { 0xA2, 0x91, 0xAE, 0x65, 0x3D, 0x4E, 0x16, 0xE3 } };


interface __declspec(uuid("C7C10D93-F001-425E-A291-AE653D4E16E3")) IOutgoingInvitationClient : public IUnknown
{
public:
    STDMETHOD(InvitationCompleted)(
        /* [in] */ const UINT Invitation,
        /* [in] */ const LPWSTR ItemID,
        /* [in] */ const BOOL Succeeded,
        /* [in] */ const POPEN_CONTEXT OpenContext) PURE;

};
