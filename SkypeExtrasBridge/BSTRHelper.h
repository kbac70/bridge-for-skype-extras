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
	BSTRHelper(_bstr_t& bstr);
	BSTRHelper(wchar_t* bstr);
	~BSTRHelper();

	static void CharToBSTR(const char* c, _bstr_t& buf);

	const char* c_str() const { return m_str; }
	const int Length() const { return m_str ? sizeof *m_str : 0; }
private:
	char* m_str;
};
