// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

#include "IOutgoingInvitationClient.h"
#include <ComDef.h>

typedef struct InviteContext
    {
    UINT MinParticipiant;
    UINT MaxParticipiant;
    LPWSTR CustomMsg;
    LPWSTR UniqueID;
    } 	INVITE_CONTEXT;
typedef INVITE_CONTEXT* PINVITE_CONTEXT;

// {D9245AA5-E9E2-4C5B-B1AA-02621D035174}
_declspec(selectany) GUID CLSID_IHostUIServices =
		{ 0xD9245AA5, 0xE9E2, 0x4C5B, { 0xB1, 0xAA, 0x02, 0x62, 0x1D, 0x03, 0x51, 0x74 } };


interface __declspec(uuid("D9245AA5-E9E2-4C5B-B1AA-02621D035174")) IHostUIServices : public IUnknown
{
public:
    STDMETHOD(GetDialogOwner)(
        /* [out] */ OLE_HANDLE *result) PURE;

    STDMETHOD(InstallSPARC)(
        /* [in] */ const LPWSTR FileName,
        /* [in] */ const LPWSTR ItemID,
        /* [out] */ BOOL *result) PURE;

    STDMETHOD(OrderLicense)(
        /* [in] */ const LPWSTR DrmID,
        /* [out] */ BOOL *result) PURE;

    STDMETHOD(CancelInvitation)(
        /* [in] */ const UINT Invitation) PURE;

    STDMETHOD(ShowItemMenu)(
        /* [in] */ const LPWSTR ItemID,
        /* [in] */ const INT x,
        /* [in] */ const INT y) PURE;

    STDMETHOD(OpenItem)(
        /* [in] */ const LPWSTR ItemID) PURE;

    STDMETHOD(RefreshList)(
        /* [in] */ INT DelayMSec) PURE;

    STDMETHOD(CallTo)(
        /* [in] */ const LPWSTR ContactHandle) PURE;

    STDMETHOD(OpenChat)(
        /* [in] */ const LPWSTR ContactHandle,
        /* [in] */ const LPWSTR Msg) PURE;

    STDMETHOD(ShowInfo)(
        /* [in] */ const LPWSTR ContactHandle) PURE;

    STDMETHOD(Invite)(
        /* [in] */ const LPWSTR ItemID,
        /* [in] */ const IOutgoingInvitationClient *Client,
        /* [out] */ UINT *Invitation) PURE;

    STDMETHOD(InviteEX)(
        /* [in] */ const LPWSTR ItemID,
        /* [in] */ const IOutgoingInvitationClient *Client,
        /* [in] */ const PINVITE_CONTEXT InviteContext,
        /* [out] */ UINT *Invitation) PURE;

};
