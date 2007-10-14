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
