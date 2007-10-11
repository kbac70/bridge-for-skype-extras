#pragma once

class _bstr_t;

#define BUFFER_SIZE 4096

/**
 * Class encapsulating the protocol used when communicating with the child process.
 * It is reponsible for de/serialization of messages
 */
class Protocol
{
public:
	static const char SEPARATOR;//<sep>
	static const char EOL;//<eol>
	static const long INVALID_ID;
	static const char*REQ_OPEN;
	static const char*REQ_SHOW_SETTINGS_DLG;
	static const char*REQ_SHUTDOWN;
	/////////////////////////////////////////////////////////
	//REQUEST Format:
	//
	//<msgID><sep><pluginID><sep><request><sep><payload><sep><eol>
	//
	/////////////////////////////////////////////////////////	
	static int EncodeMessage(char* Buffer, 
			_bstr_t& Payload			//<payload><sep><eol>
		);	
	static int EncodeOpenRequest(char* Buffer, 
			const char* PluginID,		//<pluginID><sep>	
			const int ContextType,		//<REQ_OPEN><sep><open_context_type><sep>
			const char* Participants,	//<participants><sep>
			const char* ContextRef,		//<context_ref><sep>
			const char* UniqueID,		//<unique_id><sep>
			const char* URIParams		//<uri_params>
		);
	static int EncodeShowSettingsDlg(char* Buffer,
			const char* PluginID,		//<pluginID><sep>
			const unsigned int WndOwner //<REQ_SHOW_SETTINGS_DLG><sep><wnd_ownder>
		);
	static int EncodeShutdown(char* Buffer,
			const char* PluginID		//<pluginID><sep><REQ_SHUTDOWN>
		);
	static int EncodeMessageID(char* Buffer,
			const long ID,				//<msgID><sep>
			const char*Payload			//<payload>
			);
	/////////////////////////////////////////////////////////
	//RESPONSE Format:
	//
	//<msgID><sep><hresult><sep><payload><eol>
	//
	/////////////////////////////////////////////////////////
	static long ExtractMessageID(char* Buffer, 
			_bstr_t& Payload			//<msgID><sep><payload><eol>
		);
	static long ExtractHResult(_bstr_t& Payload);

public:
	Protocol(void);
	~Protocol(void);
private:
};
