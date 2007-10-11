#include "stdafx.h"
#include "BSTRHelper.h"
#include "comutil.h"

BSTRHelper::BSTRHelper(_bstr_t& bstr) : m_str(NULL)
{
	m_str = _com_util::ConvertBSTRToString(bstr);
}

BSTRHelper::BSTRHelper(wchar_t* bstr) : m_str(NULL)
{
	m_str = bstr? _com_util::ConvertBSTRToString(bstr) : NULL;
}

BSTRHelper::BSTRHelper() : m_str(NULL)
{
}

BSTRHelper::~BSTRHelper()
{
	if (m_str)
		delete[] m_str;
}

void BSTRHelper::CharToBSTR(const char* c, _bstr_t& buf)
{
	BSTR bstr = _com_util::ConvertStringToBSTR(c);
	buf.Assign(bstr);
	SysFreeString(bstr);
}

