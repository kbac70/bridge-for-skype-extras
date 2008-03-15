// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.


#pragma once

#include <ComDef.h>

typedef enum _OpenContextType
    {
	ctNone	= 0,
	ctURI	= 1,
	ctTools	= 2,
	ctContact	= 3,
	ctChat	= 4,
	ctCall	= 5,
	ctMyself	= 6,
	ctInvitee	= 7,
	ctStartup	= 8,
	ctManager	= 9,
	ctEvent	= 10
    } 	OPEN_CONTEXT_TYPE;

typedef struct _OpenContext
    {
    OPEN_CONTEXT_TYPE ContextType;
    LPWSTR Participants;
    LPWSTR ContextRef;
    LPWSTR UniqueID;
    LPWSTR URIParams;
    } 	OPEN_CONTEXT;
typedef OPEN_CONTEXT* POPEN_CONTEXT;

// {15BA80F6-02D0-47C9-AF51-4565B454E8B6}
_declspec(selectany) GUID CLSID_ISkypePluginB =
		{ 0x15BA80F6, 0x02D0, 0x47C9, { 0xAF, 0x51, 0x45, 0x65, 0xB4, 0x54, 0xE8, 0xB6 } };


interface __declspec(uuid("15BA80F6-02D0-47C9-AF51-4565B454E8B6")) ISkypePluginB : public IUnknown
{
public:
    STDMETHOD(Open)(
        /* [in] */ POPEN_CONTEXT OpenContext) PURE;

    STDMETHOD(ShowSettingsDlg)(
        /* [in] */ OLE_HANDLE WndOwner) PURE;

    STDMETHOD(Finalize)() PURE;
};
