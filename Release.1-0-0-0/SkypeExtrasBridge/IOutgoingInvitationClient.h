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
