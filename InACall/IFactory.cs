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
