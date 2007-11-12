using System;
using System.Collections.Generic;
using System.Text;

namespace InACall
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;

    public interface IController
    {
        /// <summary>
        /// Readonly property returning valid instance of object implementing settings interface
        /// </summary>
        IInACallSettings Settings
        {
            get;
        }
        /// <summary>
        /// Readonly property returning valid instance of skype being controlled by the controller isntance
        /// </summary>
        SkypeServices Services
        {
            get;
        }
        /// <summary>
        /// Change this property to false for the controller to stop executing
        /// </summary>
        bool Enabled
        {
            get;
            set;
        }
    }
}
