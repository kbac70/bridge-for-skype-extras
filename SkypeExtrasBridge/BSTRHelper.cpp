// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

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
	assert(c != NULL);

	BSTR bstr = _com_util::ConvertStringToBSTR(c);
	buf.Assign(bstr);
	SysFreeString(bstr);
}

