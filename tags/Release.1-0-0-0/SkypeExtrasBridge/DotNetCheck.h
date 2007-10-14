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
	void CheckIsInstalled();

private:
	std::string m_windowsDir;
};
