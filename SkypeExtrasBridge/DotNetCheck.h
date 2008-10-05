// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

#include <string>

/**
 * Class wrapping .NET checks
 */
class DotNetCheck
{
public:
	DotNetCheck(void);
	~DotNetCheck(void);
	
	bool IsInstalled();
	bool CheckIsInstalled();

private:
	std::string m_windowsDir;
	std::string m_dotnetDir;
	
	static const std::string DOTNET_DIR;
	static const std::string DOTNET_POLICY_REG;

	bool ContainsSystemDll(const char* const lpszDirName) const;

	bool PolicyFoundInRegistry() const;

	/******************************************************************
	Source:			http://support.microsoft.com/default.aspx/kb/914135
	Function Name:  RegistryGetValue
	Description:    Obtain the value of a registry key.
	Inputs:         HKEY hk - The hk of the key to retrieve
					TCHAR *pszKey - Name of the key to retrieve
					TCHAR *pszValue - The value that will be retrieved
					DWORD dwType - The type of the value that will be retrieved
					LPBYTE data - A buffer to save the retrieved data
					DWORD dwSize - The size of the data retrieved
	Results:        true if it is successful, false otherwise
	******************************************************************/
	bool RegistryGetValue(HKEY hk, const TCHAR * pszKey, const TCHAR * pszValue, DWORD dwType, LPBYTE data, DWORD dwSize) const;
};
