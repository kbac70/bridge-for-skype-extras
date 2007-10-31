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

	bool ContainsSystemDll(const char* const lpszDirName) const;
};
