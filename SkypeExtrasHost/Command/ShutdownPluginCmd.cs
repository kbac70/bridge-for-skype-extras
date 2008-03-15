using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host.Command
{
    /// <summary>
    /// Author: KBac
    /// </summary>
    class ShutdownPluginCmd : AbstractCommand
    {
        public ShutdownPluginCmd(Factory factory)
            : base(factory)
        {
        }

        public override string Name
        {
            get
            {
                return Request.CMD_SHUTDOWN;
            }
        }

        protected override Response SafeExecute(Request args)
        {
            factory.PluginInstance.Shutdown();
            return new Response(args);
        }
    }
}
