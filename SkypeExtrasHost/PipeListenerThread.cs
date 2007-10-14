using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    using System.Threading;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.IO;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Author: KBac 
    /// </summary>
    class PipeListenerThread
    {
        private const int MAX_BUFFER_SIZE = 4096;
        private readonly IntPtr hStdIn;
        private readonly IntPtr hStdOut;
        private readonly System.Text.ASCIIEncoding encoding;
        private readonly Factory factory;
        private readonly CommandHandler handler;
        private readonly Thread thread;
        private readonly ProgramContext program;

        public PipeListenerThread(ProgramContext Program)
        {
            Contract.EnsureArgumentNotNull(Program, "Program");

            this.program = Program;

            hStdIn = Windows.GetStdHandle(Windows.STD_INPUT_HANDLE);
            hStdOut = Windows.GetStdHandle(Windows.STD_OUTPUT_HANDLE);
            // Assume that an ASCII file is being read.
            encoding = new System.Text.ASCIIEncoding();

            factory = new Factory();
            handler = factory.NewCommandHandler();
            handler.RegisterCommand(factory.NewOpenPluginCommand());
            handler.RegisterCommand(factory.NewShowSettingsDlgCommand());
            handler.RegisterCommand(factory.NewShutdownCommand());

            thread = new Thread(this.Listen);
            thread.Priority = ThreadPriority.BelowNormal;
        }

        public void Start()
        {
            thread.Start();
        }

        public void Abort()
        {
            thread.Abort();
        }

        public void Join()
        {
            thread.Join();
        }

        private void Listen()
        {
            byte[] buffer = new byte[MAX_BUFFER_SIZE];
            int bytesRead;

            try
            {
                do
                {
                    bytesRead = ReadFromPipe(hStdIn, buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string content = encoding.GetString(buffer, 0, bytesRead);
                        Request request = new Request(content);
                        ProcessRequest(request);

                        if (request.Name.StartsWith(Request.CMD_SHUTDOWN))
                        {
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(10);//msec
                }
                while (bytesRead > 0 && !program.IsTerminated);
            }
            catch (Exception e)
            {
                ProgramContext.ShowError(e);
            }

            program.Terminate();
        }


        private void ProcessRequest(Request request)
        {
            if (program.IsDebugging)
            {
                MessageBox.Show(request.Raw, "Request");
            }

            Response response = handler.ExecuteCommand(request);

            byte[] buffer = encoding.GetBytes(response.Raw);
            int bytesWritten;
            bytesWritten = WriteToPipe(hStdOut, buffer, 0, buffer.Length);

            if (response.Error != null)
            {
                ProgramContext.ShowError(response.Error);
            }
            else
            {
                if (program.IsDebugging)
                {
                    MessageBox.Show(response.Raw, "Response");
                }
            }
        }

        private static unsafe int ReadFromPipe(IntPtr handle, byte[] buffer, int index, int count)
        {
            int n = 0;
            fixed (byte* p = buffer)
            {
                if (!Windows.ReadFile(handle, p + index, count, &n, 0))
                {
                    return 0;
                }
            }
            return n;
        }

        private static unsafe int WriteToPipe(IntPtr handle, byte[] buffer, int index, int count)
        {
            int n = 0;
            fixed (byte* p = buffer)
            {
                if (!Windows.WriteFile(handle, p + index, count, &n, 0))
                {
                    return 0;
                }
            }
            return n;
        }

    }
}
