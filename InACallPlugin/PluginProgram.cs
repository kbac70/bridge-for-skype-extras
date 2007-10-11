using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InACall.Plugin
{
    using Skype.Extension.Utils;
    using InACall;

    static class PluginProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            SkypePluginAContext ctx = new SkypePluginAContext(new PluginFactory());
            if (!ctx.IsTerminated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(ctx);
            }
        }
    }
}