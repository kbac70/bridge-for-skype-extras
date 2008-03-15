// Copyright 2007 InACall Skype Plugin by KBac Labs 
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this product except in compliance with the License. You may obtain a copy of the License at 
//	http://www.apache.org/licenses/LICENSE-2.0 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.

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
