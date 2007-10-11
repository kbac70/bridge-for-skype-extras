using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
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
