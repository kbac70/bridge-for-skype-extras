#include "stdafx.h"
#include "EnumProc.h"
#include <Psapi.h>

////////////////////////////////////////////////////////////////
// MSDN Magazine -- July 2002 -- See EnumProc.cpp
// NOTE: Code condensed to save space; full version available online.
//

CWindowIterator::CWindowIterator(DWORD nAlloc)
{
   assert(nAlloc>0);
   m_current = m_count = 0;
   m_hwnds = new HWND [nAlloc];
   m_nAlloc = nAlloc;
}

CWindowIterator::~CWindowIterator()
{
   delete [] m_hwnds;
}

HWND CWindowIterator::First()
{
   ::EnumWindows(EnumProc, (LPARAM)this);
   m_current = 0;
   return Next();
}

// Static proc passes to virtual fn.
BOOL CALLBACK CWindowIterator::EnumProc(HWND hwnd, LPARAM lp)
{
   return ((CWindowIterator*)lp)->OnEnumProc(hwnd);
}

// Virtual proc: add HWND to array if OnWindow says OK
BOOL CWindowIterator::OnEnumProc(HWND hwnd)
{
   if (OnWindow(hwnd)) {
      if (m_count < m_nAlloc)
         m_hwnds[m_count++] = hwnd;
   }
   return TRUE; // keep looking
}

CMainWindowIterator::CMainWindowIterator(DWORD pid, DWORD nAlloc)
   : CWindowIterator(nAlloc)
{
   m_pid = pid;
}

CMainWindowIterator::~CMainWindowIterator()
{
}

// virtual override: is this window a main window of my process?
BOOL CMainWindowIterator::OnWindow(HWND hwnd)
{
   if (GetWindowLong(hwnd,GWL_STYLE) & WS_VISIBLE) {
      DWORD pidwin;
	  DWORD tidwind = GetWindowThreadProcessId(hwnd, &pidwin);
      if (pidwin==m_pid && OnProcessWindow(hwnd, tidwind))
         return TRUE;
   }
   return FALSE;
}

CProcessIterator::CProcessIterator()
{
   m_pids = NULL;
}

CProcessIterator::~CProcessIterator()
{
   delete [] m_pids;
}

// Get first process: Call EnumProcesses to get all, return first one.
DWORD CProcessIterator::First()
{
   m_current = (DWORD)-1;
   m_count = 0;
   DWORD nalloc = 1024;
   do {
      delete [] m_pids;
      m_pids = new DWORD [nalloc];
      if (EnumProcesses(m_pids, nalloc*sizeof(DWORD), &m_count)) {
         m_count /= sizeof(DWORD);
         m_current = 1; // important: skip IDLE process with pid=0.
      }
   } while (nalloc <= m_count);

   return Next();
}

CProcessModuleIterator::CProcessModuleIterator(DWORD pid)
{
   m_hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
      FALSE, pid);
}

CProcessModuleIterator::~CProcessModuleIterator()
{
   CloseHandle(m_hProcess);
   delete [] m_hModules;
}

// Get first module: Call EnumProcesseModules to get all, return first
HMODULE CProcessModuleIterator::First()
{
   m_count = 0;
   m_current = (DWORD)-1; 
   m_hModules = NULL;
   if (m_hProcess) {
      DWORD nalloc = 1024;
      do {
         delete [] m_hModules;
         m_hModules = new HMODULE [nalloc];
         if (EnumProcessModules(m_hProcess, m_hModules,
            nalloc*sizeof(DWORD), &m_count)) {
            m_count /= sizeof(HMODULE);
            m_current = 0;
         }
      } while (nalloc <= m_count);
   }
   return Next();
}
