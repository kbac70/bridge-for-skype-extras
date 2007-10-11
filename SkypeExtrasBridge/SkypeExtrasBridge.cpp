#include "StdAfx.h"
#include "SkypeExtrasBridge.h"

#include "ClassFactory.h"
#include "ISkypePluginB.h"
#include "CSkypePluginB.h"


BOOL APIENTRY DllMain(HANDLE /*hModule*/, 
                      DWORD  ul_reason_for_call, 
                      LPVOID /*lpReserved*/)
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
    }
    return TRUE;
}


STDAPI_(void) DllInitSkypePluginB (const BSTR ItemID, ICollectionManager *ColManager,
    ISkypePluginB **Plugin, const BSTR Params = NULL)
{
	_bstr_t itemID(ItemID);
	_bstr_t params(Params? Params : L"");

	*Plugin = new CSkypePluginB(itemID, ColManager, params);
}
