#pragma once

class BSTRHelper;
class _bstr_t;

#include "comutil.h"
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
	PipePumpingThread( _bstr_t& id);
	~PipePumpingThread();
	/**
	 * Call write request method to push the content of the payload into the queue to make it
	 * available to the thread for dispatching. Therefore this method is asynchronous.
	 * @Return automatically generated unigue message id
	 */
	long WriteRequest(const _bstr_t& payload);
	/**
	 * Call sync write request method synchronously push the content of the payload into the pipe
	 * and wait for the response to the request.
	 * @Return response to the request
	 */
	_bstr_t SyncWriteRequest(const _bstr_t& payload);
	/**
	 * Call read request method try and read the content of the pipe looking for response 
	 * associated with the request id
	 */
	void ReadResponse(const long id, const _bstr_t& response);
	/**
	 * @Return true when there is a request awating dispatching
	 */
	bool HasRequest();
	/**
	 * Call this method to pop the request from the queue of requests awaiting dispatch
	 */
	_bstr_t NextRequest();
	/**
	 * Call this methid to check if the response for the request ID has arrived and is available 
	 * to be picked up
	 */
	bool HasResponse(const long id);
	/**
	 * Call this methid to retrieve the response for the particular request id.
	 * @Return response when available, empty string otherwise
	 */
	_bstr_t GetResponse(const long id);
	/**
	 * @Return true when thread is to be terminated
	 */
	bool isTerminated() const { return m_terminated; }
	/**
	 * @Return plugin ID wrapeed by the BSTRHelper
	 */
	const BSTRHelper& GetID() const { return *m_id; }
private:
	static long msgID;
	volatile bool m_terminated;
	bool isSuspended;

	DWORD dwThreadID;
	HANDLE  hThread;
	BSTRHelper* m_id;

	std::queue<_bstr_t> m_outQ;
	std::map<long, _bstr_t> m_inQ;

	void Resume();
};
