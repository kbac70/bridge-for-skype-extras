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

namespace Skype.Extension.Utils
{
    /// <summary>
    /// Interface defining the required functionality for the Skype plugin of type B (binary/dll)
    /// </summary>
    public interface ISkypePluginB
    {
        /// <summary>
        /// This command opens the plugin within the context defined by the context parameter.
        /// </summary>
        /// <param name="Context">see OpenContext type for more details</param>
        void Open(OpenContext Context); 
        /// <summary>
        /// This command shows settings dialog 
        /// </summary>
        /// <param name="WindowOwner">Handle of the window owning the plugin window</param>
        void ShowSettingsDlg(uint WindowOwner); 
        /// <summary>
        /// This command finalizes the work of plugin. After its execution the plugin will get unloaded from the memory.
        /// Plugin has up to 2 secs to finalize. When unsuccessful it is going to be forcefully terminated by the bridge dll
        /// </summary>
        void Shutdown();
    }
}
