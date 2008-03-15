// Copyright 2007 InACall Skype Plugin by KBac Labs
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this product except in compliance with the License. You may obtain a copy of the License at
//	http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

#pragma once

////////////////////////////////////////////////////////////////
// MSDN Magazine -- July 2002
// If this code works, it was written by Paul DiLascia.
// If not, I don't know who wrote it.
// Compiles with Visual Studio 6.0 and Visual Studio .NET
// Runs in Windows XP and probably Windows 2000 too.
// NOTE: Code condensed to save space; full version available online.

// Iterate the top-level windows. Encapsulates ::EnumWindows.
class CWindowIterator {
protected:
   HWND* m_hwnds;          // array of hwnds for this PID
   DWORD m_nAlloc;         // size of array
   DWORD m_count;          // number of HWNDs found
   DWORD m_current;        // current HWND
   static BOOL CALLBACK EnumProc(HWND hwnd, LPARAM lp);
   // virtual enumerator
   virtual BOOL OnEnumProc(HWND hwnd);
   // override to filter different kinds of windows
   virtual BOOL OnWindow(HWND ) {return TRUE;}
public:
   CWindowIterator(DWORD nAlloc=1024);
   ~CWindowIterator();

   DWORD GetCount() { return m_count; }
   HWND First();
   HWND Next() {
      return m_hwnds && m_current < m_count ? m_hwnds[m_current++] : NULL;
   }
};

// Iterate the top-level windows in a process.
class CMainWindowIterator : public CWindowIterator  {
protected:
   DWORD m_pid;                     // process id
   virtual BOOL OnWindow(HWND hwnd);
   // when window for a process is found notify children aboud its threadid
   virtual BOOL OnProcessWindow(HWND, DWORD) { return TRUE; }
public:
   CMainWindowIterator(DWORD pid, DWORD nAlloc=1024);
   ~CMainWindowIterator();
};

// Iterator system processes. Always skips first (IDLE) process with PID=0.
class CProcessIterator {
protected:
   DWORD*   m_pids;        // array of procssor IDs
   DWORD    m_count;       // size of array
   DWORD    m_current;     // next array item
public:
   CProcessIterator();
   ~CProcessIterator();
   DWORD GetCount() { return m_count; }
   DWORD First();
   DWORD Next() {
      return m_pids && m_current < m_count ? m_pids[m_current++] : 0;
   }
};

// Iterate the modules in a process. First module is always main EXE.
class CProcessModuleIterator {
protected:
   HANDLE   m_hProcess;       // process handle
   HMODULE* m_hModules;       // array of module handles
   DWORD    m_count;          // size of array
   DWORD    m_current;        // next module handle
public:
   CProcessModuleIterator(DWORD pid);
   ~CProcessModuleIterator();
   HANDLE GetProcessHandle()  { return m_hProcess; }
   DWORD GetCount()           { return m_count; }
   HMODULE First();
   HMODULE Next() {
      return m_hProcess && m_current < m_count ? m_hModules[m_current++] : 0;
   }
};
