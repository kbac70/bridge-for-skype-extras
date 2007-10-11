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

    class Windows
    {
        public const DWORD STD_INPUT_HANDLE = unchecked((UInt32)(-10));
        public const DWORD STD_OUTPUT_HANDLE = unchecked((UInt32)(-11));
        public const DWORD STD_ERROR_HANDLE = unchecked((UInt32)(-12));

        [DllImport("Kernel32", SetLastError = true)]
        public static extern HANDLE GetStdHandle(DWORD nStdHandle);

        [DllImport("Kernel32.dll")]
        public static extern unsafe bool ReadFile(
                IntPtr hFile,             // handle to the file  
                void* pBuffer,            // data buffer
                int NumberOfBytesToRead,  // number of bytes to read
                int* pNumberOfBytesRead,  // number of bytes read
                int Overlapped            // overlapped buffer
        );

        [DllImport("Kernel32.dll")]
        public static extern unsafe bool WriteFile(
                IntPtr hFile,               // handle to the file  
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
    }
}
