using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Skype.Extension.Utils.PluginB.Host
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Author: KBac 
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramContext ctx = new ProgramContext(args);

            if (!ctx.IsTerminated)
            {
                Application.Run(ctx);
            }
        }
    }
}
