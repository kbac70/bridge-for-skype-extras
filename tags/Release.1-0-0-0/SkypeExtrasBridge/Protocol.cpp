#include "stdafx.h"
#include "Protocol.h"

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

int Protocol::EncodeMessage(char* Buffer, std::string& Payload)
{
	assert(Buffer != NULL);

	return sprintf_s(Buffer, BUFFER_SIZE, "%s|\n", Payload.c_str());
}

long Protocol::ExtractMessageID(char* Buffer, std::string& Payload)
{
	assert(Buffer != NULL);

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

	Payload = c;

	long ret = c == Buffer || i == BUFFER_SIZE ? INVALID_ID : atol(Buffer);

	return ret;
}

long Protocol::ExtractHResult(std::string& Payload)
{	
	char buffer[BUFFER_SIZE];
	memcpy(buffer, Payload.c_str(), Payload.length());

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

	Payload = c;

	long ret = c == buffer || i == BUFFER_SIZE ? E_FAIL : atol(buffer);
	return ret;
}

int Protocol::EncodeOpenRequest(char* Buffer, const char* PluginID, int ContextType, const char* Participants, 
		const char* ContextRef, const char* UniqueID, const char* URIParams) 
{
	assert(Buffer != NULL);
	assert(PluginID != NULL);

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
	assert(Buffer != NULL);
	assert(PluginID != NULL);

	return sprintf_s(Buffer, BUFFER_SIZE, "%s|%s|%d" , 
			PluginID, 
			REQ_SHOW_SETTINGS_DLG,
			WndOwner
		);
}

int Protocol::EncodeShutdown(char* Buffer, const char* PluginID)
{
	assert(Buffer != NULL);
	assert(PluginID != NULL);

	return sprintf_s(Buffer, BUFFER_SIZE, "%s|%s" , 
			PluginID, 
			REQ_SHUTDOWN
		);
}

int Protocol::EncodeMessageID(char* Buffer, const long ID, const char* Payload)
{
	assert(Buffer != NULL);
	assert(Payload != NULL);

	return sprintf_s(Buffer, BUFFER_SIZE, "%d|%s", ID, Payload);
}

int Protocol::EncodeResponseThreadAborted(char* Buffer,	const long ID, const char*Payload)
{
	assert(Buffer != NULL);
	assert(Payload != NULL);
	
	return sprintf_s(Buffer, BUFFER_SIZE, "%d|%d|%s", ID, E_ABORT, Payload);
}

int Protocol::EncodeResponseTimeout(char* Buffer, const long ID, const char*Payload)
{
	assert(Buffer != NULL);
	assert(Payload != NULL);
	
	return sprintf_s(Buffer, BUFFER_SIZE, "%d|%d|%s", ID, E_ACCESSDENIED, Payload);
}



