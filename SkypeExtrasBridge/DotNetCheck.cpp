// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#include "stdafx.h"
#include "DotNetCheck.h"
#include "Shellapi.h"
#include "SystemInfo.h"

DotNetCheck::DotNetCheck(void)
{
	const int DN_BUFFER_SIZE = 4096;
	char buffer[DN_BUFFER_SIZE];
	ZeroMemory(buffer, DN_BUFFER_SIZE);
	if (GetWindowsDirectory(buffer, DN_BUFFER_SIZE))
	{
		this->m_windowsDir = buffer;
	}

	m_dotnetDir = m_windowsDir;
	m_dotnetDir.append(DOTNET_DIR);
}

DotNetCheck::~DotNetCheck(void)
{
}

const std::string DotNetCheck::DOTNET_DIR = "\\Microsoft.NET\\Framework\\";
const std::string DotNetCheck::DOTNET_POLICY_REG = "Software\\Microsoft\\.NETFramework\\Policy";

bool DotNetCheck::PolicyFoundInRegistry() const 
{
	return RegistryGetValue(HKEY_LOCAL_MACHINE, DOTNET_POLICY_REG.c_str(), NULL, NULL, NULL, 0);	
}

bool DotNetCheck::IsInstalled() 
{
	SystemInfo systemInfo;
	if (systemInfo.GetWindowsVersion() >= WindowsVersion::WinVista)
	{
		return true;//PolicyFoundInRegistry();
	}

	if (m_windowsDir.length() == 0)
		return false;

	const std::string versionDir = "v";

	std::string frameworkDir(m_dotnetDir);
	frameworkDir.append("*");

	WIN32_FIND_DATA findFileData;	
	HANDLE hFind = FindFirstFile(frameworkDir.c_str(), &findFileData);
	
	if (hFind == INVALID_HANDLE_VALUE) 
	{
		return false;
	}

	bool ret = false;
	// List all the files and directories in the .NET framework directory.
    while (FindNextFile(hFind, &findFileData) != 0) 
    {
		if (findFileData.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY &&
			findFileData.cFileName && findFileData.cFileName[0] == 'v' &&
			ContainsSystemDll(findFileData.cFileName)
		    )
		{
			ret = true;
			break;
		}
	}
    
	FindClose(hFind);
	return ret;
}

bool DotNetCheck::ContainsSystemDll(const char* const lpszDirName) const 
{
	assert(lpszDirName);

	static const std::string SystemDll = "\\System.dll";
	
	std::string SystemFileName(m_dotnetDir);
	SystemFileName.append(lpszDirName);
	SystemFileName.append(SystemDll);

	WIN32_FIND_DATA findFileData;
	HANDLE hFind = FindFirstFile(SystemFileName.c_str(), &findFileData);
	
	if (hFind == INVALID_HANDLE_VALUE) 
	{
		return false;
	}
    
	FindClose(hFind);
	return true;
}

bool DotNetCheck::RegistryGetValue(HKEY hk, const TCHAR * pszKey, const TCHAR * pszValue, DWORD dwType, LPBYTE data, DWORD dwSize) const
{
    HKEY hkOpened;

    // Try to open the key.
    if (RegOpenKeyEx(hk, pszKey, 0, KEY_READ, &hkOpened) != ERROR_SUCCESS)
    {
        return false;
    }

	if (data != NULL && dwSize != NULL) 
	{
		// If the key was opened, try to retrieve the value.
		if (RegQueryValueEx(hkOpened, pszValue, 0, &dwType, (LPBYTE)data, &dwSize) != ERROR_SUCCESS)
		{
			RegCloseKey(hkOpened);
			return false;
		}
	}

    // Clean up.
    RegCloseKey(hkOpened);

    return true;
}

bool DotNetCheck::CheckIsInstalled()
{
	if (IsInstalled())
		return true;

	if (IDYES == MessageBox( NULL,
			"The plugin requires .NET runtime to be installed.\n Would you like to install it now?",
			"Confirm",
			MB_YESNO | MB_ICONQUESTION | MB_TASKMODAL | MB_TOPMOST
			)
		)
	{
		SHELLEXECUTEINFO ShExecInfo = {0};
		ShExecInfo.cbSize = sizeof(SHELLEXECUTEINFO);
		ShExecInfo.fMask = SEE_MASK_NOCLOSEPROCESS;
		ShExecInfo.hwnd = NULL;
		ShExecInfo.lpVerb = NULL;
		ShExecInfo.lpFile = "http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en";		
		ShExecInfo.lpParameters = "";	
		ShExecInfo.lpDirectory = NULL;
		ShExecInfo.nShow = SW_SHOW;
		ShExecInfo.hInstApp = NULL;	
		
		ShellExecuteEx(&ShExecInfo);
		
		WaitForSingleObject(ShExecInfo.hProcess, INFINITE);
	}

	return IsInstalled();
}

