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
