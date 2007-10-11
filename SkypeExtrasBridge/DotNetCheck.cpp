#include "stdafx.h"
#include "DotNetCheck.h"
#include "Shellapi.h"

DotNetCheck::DotNetCheck(void)
{
	const int DN_BUFFER_SIZE = 4096;
	char buffer[DN_BUFFER_SIZE];
	ZeroMemory(buffer, DN_BUFFER_SIZE);
	if (GetWindowsDirectory(buffer, DN_BUFFER_SIZE))
	{
		this->windowsDir = buffer;
	}
}

DotNetCheck::~DotNetCheck(void)
{
}

bool DotNetCheck::IsInstalled()
{
	if (windowsDir.length() == 0)
		return false;

	const std::string netDir = "\\Microsoft.NET\\Framework\\*";
	const std::string versionDir = "v";

	std::string frameworkDir = windowsDir;
	frameworkDir.append(netDir);

	WIN32_FIND_DATA findFileData;	
	HANDLE hFind = FindFirstFile(frameworkDir.c_str(), &findFileData);
	
	if (hFind == INVALID_HANDLE_VALUE) 
		return false;

	bool ret = false;
	// List all the files in the directory.
    while (FindNextFile(hFind, &findFileData) != 0) 
    {
		if (findFileData.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY)
		{
			if (findFileData.cFileName && findFileData.cFileName[0] == 'v')
			{
				ret = true;
				break;
			}
		}
	}
    
	FindClose(hFind);
	return ret;
}

void DotNetCheck::CheckIsInstalled()
{
	if (IsInstalled())
		return;

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
		
		WaitForSingleObject(ShExecInfo.hProcess,INFINITE);
	}
}

