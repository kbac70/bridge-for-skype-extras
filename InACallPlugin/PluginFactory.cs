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

namespace InACall.Plugin
{
    using Skype.Extension.Utils;

    /// <summary>
    /// Public class which is used by the:
    /// - XtrsHost when instantiting the ISkypePluginB implementations (project needs to compiled as class library)
    /// - PluginAContext when instantiating ISkypePLuginA implementations (project needs to be compiled as console application with PluginProgram.Main as its main entry)
    /// </summary>
    public class PluginFactory : IPluginFactory
    {
        public const string INACALL_NS = "InACall.Plugin";
        const string RES_ICON_FILENAME = "icon.png";
        const string ICON_FILENAME = "InACall.png";

        public PluginFactory()
        {
            this.factory = new InACallFactory();
        }

        #region IPluginFactory Members

        public ISkypePluginA newPluginA()
        {
            return new InACallPluginImpl(
                    factory,
                    AbstractPluginImpl.ExtractPictureFromResourcesToFile(
                            System.Reflection.Assembly.GetAssembly(typeof(InACallPluginImpl)),
                            INACALL_NS,
                            ICON_FILENAME,
                            RES_ICON_FILENAME
                     )
                );
        }

        public ISkypePluginB newPluginB()
        {
            return new InACallPluginImpl(factory);
        }

        #endregion

        private readonly InACallFactory factory;
    }
}
