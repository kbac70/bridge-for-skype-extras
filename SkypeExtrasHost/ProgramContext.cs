using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    using System.Windows.Forms;
    using System.Threading;
    using System.Diagnostics;

    class ProgramContext : ApplicationContext
    {
        private readonly bool isDebugging;
        private readonly PipeListenerThread thread;
        private bool isTerminated;

        public ProgramContext(string[] args)
        {
            Contract.EnsureArgumentNotNull(args, "args");

            Process thisProcess = Process.GetCurrentProcess();

            if (args.Length > 0)
            {
                isDebugging = args[0].Equals("DEBUG");

                if (thisProcess.MainModule.FileName.Contains(args[0])|| IsDebugging)
                {
                    Application.ThreadException += OnUnhandledException;
                    Application.ThreadExit += OnThreadExit;
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    
                    if (!IsDebugging)
                    {
                        Windows.ShowWindow(thisProcess.MainWindowHandle, Windows.SW_HIDE);
                    }
             
                    thread = new PipeListenerThread(this);
                    thread.Start();

                }
                else
                {
                    ShowInfo();
                    Terminate();
                }
            }
            else
            {
                ShowInfo();
                Terminate();
            }
        }

        public bool IsDebugging
        {
            get
            {
                return isDebugging;
            }
        }

        private void OnThreadExit(object sender, EventArgs e)
        {
            if (!IsTerminated)
            {
                //user has closed the app, so abort the listener and quit
                isTerminated = true;
                thread.Abort();
            }
            else
            {
                //terminated method has been called, wait till thread is done
                thread.Join();
            }
        }

        public static void ShowError(Exception e)
        {
            if (SkypeServices.IsSkypeRunning)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace,
                        "Unhandled Error Occured",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                   );
            }
        }

        private void OnUnhandledException(object sender, ThreadExceptionEventArgs args)
        {
            ShowError(args.Exception);
        }

        private void ShowInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("KBac Labs - .Net Extras Host for Skype");
            Console.WriteLine(typeof(ProgramContext).Assembly.FullName);
            Console.WriteLine("The application is designed to be invoked by xtrsbrdg.dll loaded by SkypePM.exe");
            Console.WriteLine("Press any key to continue...");
            Console.WriteLine("");
            Console.ReadKey();
        }

        public bool IsTerminated 
        { 
            get 
            { 
                return isTerminated; 
            } 
        }

        public void Terminate()
        {
            if (!IsTerminated)
            {
                isTerminated = true;
                ExitThread();
            }
        }
    }
}
