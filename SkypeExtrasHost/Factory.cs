using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    using Skype.Extension.Utils.PluginB.Host.Command;

    /// <summary>
    /// Author: KBac
    /// </summary>
    class Factory
    {
        private readonly PluginLoader loader;

        public Factory()
        {
            loader = new PluginLoader();
        }

        public ISkypePluginB PluginInstance
        {
            get
            {
                return loader.Plugin;
            }
        }

        public CommandHandler NewCommandHandler()
        {
            return new CommandHandler(this);
        }

        public AbstractCommand NewOpenPluginCommand()
        {
            return new OpenPluginCmd(this);
        }

        public AbstractCommand NewShowSettingsDlgCommand()
        {
            return new ShowPluginSettingsDlgCmd(this);
        }

        public AbstractCommand NewShutdownCommand()
        {
            return new ShutdownPluginCmd(this);
        }

        public OpenContext NewOpenContext(Request context)
        {
            OpenContext result = new OpenContext();
            result.ContextKind = (OpenContextType)int.Parse(context.Params[Request.IDX_OPENCONTEXT_TYPE]);
            result.ContextRef = context.Params[Request.IDX_OPENCONTEXT_CONTEXTREF];
            result.Participants = context.Params[Request.IDX_OPENCONTEXT_PARTCICIPANTS];
            result.UniqueID = context.Params[Request.IDX_OPENCONTEXT_UNIQUEID];
            result.URIParams = context.Params[Request.IDX_OPENCONTEXT_URIPARAMS];

            return result;
        }

    }
}
