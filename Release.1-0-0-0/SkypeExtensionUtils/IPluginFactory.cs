using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    /// <summary>
    /// Factory interface which is to be implemented by the public class from within
    /// assembly to be loaded into XtrsHost.exe as the plugin.
    /// It is your responsibility to handle the dependencies other than:
    /// - Interop.SKYPE4COMLib.dll
    /// - Extras.Utils.dll
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Method instantiting ISkypePluginA implementations
        /// </summary>
        /// <returns>Object implementing ISkypePluginA</returns>
        ISkypePluginA newPluginA();
        /// <summary>
        /// Method instantiting ISkypePluginB implementations
        /// </summary>
        /// <returns>Object implementing ISkypePluginB</returns>
        ISkypePluginB newPluginB();
    }
}
