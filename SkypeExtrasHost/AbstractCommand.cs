using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    /// <summary>
    /// Author: KBac
    /// </summary>
    abstract class AbstractCommand
    {
        protected readonly Factory factory;

        public AbstractCommand(Factory factory)
        {
            Contract.EnsureArgumentNotNull(factory, "factory");
            this.factory = factory;
        }

        public abstract string Name
        {
            get;
        }

        public Response Execute(Request args)
        {
            if (args.IsValid)
            {
                try
                {
                    if (factory.PluginInstance != null)
                    {
                        return SafeExecute(args);
                    }
                    else
                    {
                        return Response.Failed(args);
                    }
                }
                catch(Exception e)
                {
                    return Response.UnexpectedError(args, e);
                }
            }
            else
            {
                return Response.InvalidArguments(args);
            }
        }

        protected abstract Response SafeExecute(Request request);
    }
}
