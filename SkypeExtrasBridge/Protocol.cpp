#include "stdafx.h"
#include "Protocol.h"
#include "BSTRHelper.h"
#include "comutil.h"

Protocol::Protocol(void)
{
}

Protocol::~Protocol(void)
{
}

const char Protocol::SEPARATOR = '|';
const char Protocol::EOL = '\n';
const long Protocol::INVALID_ID = -1;
const char*Protocol::REQ_OPEN = "Open";
const char*Protocol::REQ_SHOW_SETTINGS_DLG = "ShowSettingsDlg";
const char*Protocol::REQ_SHUTDOWN = "Shutdown";

int Protocol::EncodeMessage(char* Buffer, _bstr_t& Payload)
{
	BSTRHelper msg = BSTRHelper(Payload);
	return sprintf_s(Buffer, BUFFER_SIZE, "%s|\n", msg.c_str());
}

long Protocol::ExtractMessageID(char* Buffer, _bstr_t& Payload)
{
	char* c = Buffer;
	int i = 0; 

	while (*c != 0 && i < BUFFER_SIZE)
	{
		if (*c == SEPARATOR)
		{
			*c = '\0';
			c++;
			break;
		}
		c++;
		i++;
	}

	BSTRHelper::CharToBSTR(c, Payload);

	long ret = c == Buffer || i == BUFFER_SIZE ? INVALID_ID : atol(Buffer);

	return ret;
}

long Protocol::ExtractHResult(_bstr_t& Payload)
{
	BSTRHelper h = BSTRHelper(Payload);
	
	char buffer[BUFFER_SIZE];
	memcpy(buffer, h.c_str(), h.Length());

	char* c = buffer;
	int i = 0; 

	while (*c != 0 && i < BUFFER_SIZE)
	{
		if (*c == SEPARATOR)
		{
			*c = '\0';
			c++;
			break;
		}
		c++;
		i++;
	}

	BSTRHelper::CharToBSTR(c, Payload);

	long ret = c == buffer || i == BUFFER_SIZE ? E_FAIL : atol(buffer);
	return ret;
}

int Protocol::EncodeOpenRequest(char* Buffer, const char* PluginID, int ContextType, const char* Participants, 
		const char* ContextRef, const char* UniqueID, const char* URIParams) 
{
	return sprintf_s(Buffer, BUFFER_SIZE, "%s|%s|%d|%s|%s|%s|%s" , 
			PluginID, 
			REQ_OPEN,
			ContextType, 
			Participants ? Participants : "",
			ContextRef   ? ContextRef   : "",
			UniqueID     ? UniqueID     : "",
			URIParams    ? URIParams    : ""
		);
}

int Protocol::EncodeShowSettingsDlg(char* Buffer, const char* PluginID, const unsigned int WndOwner)
{
	return sprintf_s(Buffer, BUFFER_SIZE, "%s|%s|%d" , 
			PluginID, 
			REQ_SHOW_SETTINGS_DLG,
			WndOwner
		);
}

int Protocol::EncodeShutdown(char* Buffer, const char* PluginID)
{
	return sprintf_s(Buffer, BUFFER_SIZE, "%s|%s" , 
			PluginID, 
			REQ_SHUTDOWN
		);
}

int Protocol::EncodeMessageID(char* Buffer, const long ID, const char* Payload)
{
	return sprintf_s(Buffer, BUFFER_SIZE, "%d|%s", ID, Payload);
}

