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
    /// <summary>
    /// Author: KBac
    /// </summary>
    class CommandHandler
    {
        private readonly Dictionary<string, AbstractCommand> commands;
        private readonly Factory factory;

        public CommandHandler(Factory factory)
        {
            Contract.EnsureArgumentNotNull(factory, "factory");
            this.factory = factory;
            commands = new Dictionary<string, AbstractCommand>();
        }

        public void RegisterCommand(AbstractCommand cmd)
        {
            Contract.EnsureArgumentNotNull(cmd, "cmd");

            if (!commands.ContainsKey(cmd.Name) && cmd.Name != null)
            {
                commands.Add(cmd.Name, cmd);
            }
        }

        public void UnregisterCommand(AbstractCommand cmd)
        {
            Contract.EnsureArgumentNotNull(cmd, "cmd");
            commands.Remove(cmd.Name);
        }

        public Response ExecuteCommand(Request args)
        {
            if (commands.ContainsKey(args.Name))
            {
                AbstractCommand cmd = commands[args.Name];
                return cmd.Execute(args);
            }

            return Response.NoImplementation(args);
        }
    }
}
