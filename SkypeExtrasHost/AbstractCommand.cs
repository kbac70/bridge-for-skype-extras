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
