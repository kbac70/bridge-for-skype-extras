#ifndef SKYPEPLUGINB_H
#define SKYPEPLUGINB_H

#include "StdAfx.h"
#include "ole2.h"

#ifndef DLLIMPORTS
	#pragma message("-> automatic exports from skypepluginb")
	#define DLLAPI __declspec(dllexport)
#else
	#pragma message("-> automatic link to skypepluginb.LIB")
	#define DLLAPI __declspec(dllimport)
#endif

	
#endif//SKYPEPLUGINB_H