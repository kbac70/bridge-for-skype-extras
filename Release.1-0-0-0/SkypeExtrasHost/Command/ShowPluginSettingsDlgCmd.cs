using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host.Command
{
    /// <summary>
    /// Author: KBac 
    /// </summary>
    class ShowPluginSettingsDlgCmd : AbstractCommand
    {
        public ShowPluginSettingsDlgCmd(Factory factory)
            : base(factory)
        {
        }

        public override string Name
        {
            get
            {
                return Request.CMD_SHOW_SETTINGS_DIALOG;
            }
        }

        protected override Response SafeExecute(Request args)
        {
            factory.PluginInstance.ShowSettingsDlg(uint.Parse(args.Params[Request.IDX_OWNERHANDLE]));
            return new Response(args);
        }
    }
}
