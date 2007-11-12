using System;
using System.Collections.Generic;
using System.Text;

namespace InACall
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;

    public interface IFactory
    {
        /// <summary>
        /// Call newSettings method to create new instance of class implementing Settings interface.
        /// </summary>
        /// <returns>New instance of file backed settings provider</returns>
        IInACallSettings newSettings();
        /// <summary>
        /// Call this method to acquire access to the skype application
        /// </summary>
        /// <returns>New isntance of the skype application</returns>
        SkypeServices getSkypeServices();
        /// <summary>
        /// Call this method to initialize the skype application
        /// </summary>
        /// <param name="skype">object implementing skype interfaces</param>
        void init(SkypeServices skype);
        /// <summary>
        /// Call this method to create new instance of the controller associated with the 
        /// passed in skype instance.
        /// </summary>
        /// <param name="settings">Instance of settings provider</param>
        /// <param name="skype">Instance of skype</param>
        /// <returns></returns>
        IController newController(IInACallSettings settings, SkypeServices skype);
        /// <summary>
        /// Call this method to create new instance of the controller.
        /// </summary>
        /// <param name="settings">Required object, instance of Settings interface</param>
        /// <returns>New instance of controller implementation</returns>
        IController newController(IInACallSettings settings);
        /// <summary>
        /// Call this methid to create new preconfigured instance of a Controller
        /// </summary>
        /// <returns>New instance of controller implementation, pre-configured with  valid
        /// instance of Settings implementation</returns>
        IController newController();
    }
}
