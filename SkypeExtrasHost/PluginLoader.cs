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
    using Skype.Extension.Utils;

    /// <summary>
    /// Author: KBac
    /// </summary>
    class PluginLoader
    {
        const string DLL_XTRS = "xtrsbrdg.dll";
        const string DLL_UTILS = "Extras.Utils.dll";
        const string DLL_INTEROP = "Interop.SKYPE4COMLib.dll";
        const string FACTORY_TYPE = "Skype.Extension.Utils.IPluginFactory";

        public PluginLoader()
        {
        }

        private bool PluginTypeFilter(Type m, object filterCriteria)
        {
            return m.ToString().Equals(filterCriteria.ToString());
        }

        protected void LoadPlugin()
        {
	        string path = AppDomain.CurrentDomain.BaseDirectory;
	        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

        	foreach(System.IO.FileInfo fileInfo in di.GetFiles("*.dll"))
	        {
                if (!fileInfo.Name.Equals(DLL_XTRS)&&
                    !fileInfo.Name.Equals(DLL_UTILS)&&
                    !fileInfo.Name.Equals(DLL_INTEROP)
                    )
		        {
                    try
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(fileInfo.FullName);
                        foreach (System.Type t in assembly.GetTypes())
                        {
                            if (!t.IsAbstract && !t.IsInterface && t.IsPublic && t.IsVisible)
                            {
                                System.Reflection.TypeFilter pluginFilter =
                                        new System.Reflection.TypeFilter(PluginTypeFilter);
                                Type[] plugins = t.FindInterfaces(pluginFilter, FACTORY_TYPE);

                                if (plugins.Length > 0)
                                {
                                    try
                                    {
                                        IPluginFactory factory = (IPluginFactory)assembly.CreateInstance(t.FullName);
                                        if (factory != null)
                                        {
                                            plugin = factory.newPluginB();
                                            return;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //failed to intantiate the type - proceed to the next one
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //failed to load assembly - proceed to the next one
                    }
                }
	        }
        }

        public bool IsPluginLoaded
        {
            get
            {
                return plugin != null;
            }
        }

        public ISkypePluginB Plugin
        {
            get
            {
                if (!IsPluginLoaded)
                { 
                    LoadPlugin();
                }

                return plugin;
            }
        }

        private ISkypePluginB plugin;
    }
}
