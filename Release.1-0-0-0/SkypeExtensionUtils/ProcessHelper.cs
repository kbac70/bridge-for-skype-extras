using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using System.Diagnostics;
    /// <summary>
    /// Utility class helping with processes
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// Helps ensuring that the current process is a singleton
        /// </summary>
        /// <returns>Running instance of the current process</returns>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file. 
                    if (System.Reflection.Assembly.GetEntryAssembly().Location.
                         Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //Return the other process instance.  
                        return process;
                    }
                }
            }
            //No other instance was found, return null.  
            return null;
        }

    }
}
