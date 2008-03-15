// Copyright 2007 InACall Skype Plugin by KBac Labs 
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this product except in compliance with the License. You may obtain a copy of the License at 
//	http://www.apache.org/licenses/LICENSE-2.0 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    using System.Runtime.InteropServices;
    using System.IO;
    using Microsoft.Win32.SafeHandles;

    // aliases of the Win32 types to Framework types
    using HANDLE = System.IntPtr;
    using DWORD = System.UInt32;

    /// <summary>
    /// Author: KBac
    /// </summary>
    class Windows
    {
        public const DWORD STD_INPUT_HANDLE = unchecked((UInt32)(-10));
        public const DWORD STD_OUTPUT_HANDLE = unchecked((UInt32)(-11));
        public const DWORD STD_ERROR_HANDLE = unchecked((UInt32)(-12));

        [DllImport("Kernel32", SetLastError = true)]
        public static extern HANDLE GetStdHandle(DWORD nStdHandle);

        [DllImport("Kernel32.dll")]
        public static extern unsafe bool ReadFile(
                HANDLE hFile,             // handle to the file  
                void* pBuffer,            // data buffer
                int NumberOfBytesToRead,  // number of bytes to read
                int* pNumberOfBytesRead,  // number of bytes read
                int Overlapped            // overlapped buffer
        );

        [DllImport("Kernel32.dll")]
        public static extern unsafe bool WriteFile(
                HANDLE hFile,               // handle to the file  
                void* pBuffer,              // data buffer
                int nNumberOfBytesToWrite,  // number of bytes to write
                int* lpNumberOfBytesWritten,// number of bytes read
                int Overlapped              // overlapped buffer
        );

        [DllImport("user32.dll")]
        public static extern HANDLE FindWindow(string lpClassName, string lpWindowName);


        public const int SW_HIDE = 0;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(HANDLE hWnd, int nCmdShow);


        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public DWORD lParam;
            public DWORD wParam;
            public uint message;
            public HANDLE hwnd;
        };

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        public enum HookType : int
        {
            WH_MSGFILTER = -1,
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12
        };

        public enum CbtHookActionType : int
        {
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
        };

        [DllImport("user32.dll")]
        public static extern HANDLE SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hInstance, int threadId);
        
        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(HANDLE hHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(HANDLE hHook, int nCode, IntPtr wParam, IntPtr lParam);

    }
}
