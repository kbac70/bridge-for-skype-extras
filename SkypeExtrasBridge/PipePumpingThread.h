#pragma once

#include <string>
#include <queue>
#include <map>

/**
 * Thread wrapper class which is reposponsible for pumping the messages through the anonymous pipe.
 * @Author: KBac
 */
class PipePumpingThread
{
public:
	static const long INVALID_ID;
public:
	PipePumpingThread( std::string& PluginID);
	~PipePumpingThread();
	/**
	 * Call write request method to push the content of the payload into the queue to make it
	 * available to the thread for dispatching. Therefore this method is asynchronous.
	 * @Return automatically generated unigue message id
	 */
	long WriteRequest(const std::string& Payload);
	/**
	 * Call sync write request method synchronously push the content of the payload into the pipe
	 * and wait for the response to the request.
	 * @Return response to the request
	 */
	std::string SyncWriteRequest(const std::string& Payload);
	/**
	 * Call read request method try and read the content of the pipe looking for response 
	 * associated with the request id
	 */
	void ReadResponse(const long MessageID, const std::string& Response);
	/**
	 * @Return true when there is a request awating dispatching
	 */
	bool HasRequest();
	/**
	 * Call this method to pop the request from the queue of requests awaiting dispatch
	 */
	std::string NextRequest();
	/**
	 * Call this methid to check if the response for the request ID has arrived and is available 
	 * to be picked up
	 */
	bool HasResponse(const long MessageID);
	/**
	 * Call this methid to retrieve the response for the particular request id.
	 * @Return response when available, empty string otherwise
	 */
	std::string GetResponse(const long MessageID);
	/**
	 * @Return true when thread is to be terminated
	 */
	bool isTerminated() const { return m_terminated; }
	/**
	 * @Return plugin ID wrapeed by the BSTRHelper
	 */
	const std::string& GetID() const { return m_id; }
	/**
	 * @Return Emergency Shutdown Command and set terminated flag
	 */
	std::string& Abort();
private:
	static long msgID;
	volatile bool m_terminated;
	bool m_IsSuspended;

	DWORD m_dwThreadID;
	HANDLE  m_hThread;
	std::string m_id;

	std::string m_abortRequest;

	std::queue<std::string> m_outQ;
	std::map<long, std::string> m_inQ;

	void Resume();
};
