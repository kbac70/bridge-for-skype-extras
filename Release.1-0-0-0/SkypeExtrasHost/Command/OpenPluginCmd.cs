using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host.Command
{
    /// <summary>
    /// Author: KBac 
    /// </summary>
    class OpenPluginCmd : AbstractCommand
    {
        public OpenPluginCmd(Factory factory)
            : base(factory)
        {
        }

        public override string Name
        {
            get
            {
                return Request.CMD_OPEN;
            }
        }

        protected override Response SafeExecute(Request args)
        {
            OpenContext oc = factory.NewOpenContext(args);
            ISkypePluginB plugin = factory.PluginInstance;
            plugin.Open(oc);
            return new Response(args);
        }
    }
}
