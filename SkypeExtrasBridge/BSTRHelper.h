// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

class _bstr_t;

/**
 * Helper class to deal with char types transformation.
 * @Author KBac
 */
class BSTRHelper
{
public:
	BSTRHelper();
	BSTRHelper(_bstr_t& BStr);
	BSTRHelper(wchar_t* BStr);
	~BSTRHelper();

	static void CharToBSTR(const char* CharArray, _bstr_t& Buffer);

	const char* c_str() const { return m_str; }
	const int Length() const { return m_str ? sizeof *m_str : 0; }
private:
	char* m_str;
};
