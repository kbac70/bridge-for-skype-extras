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
    using InACall.Impl;
    using InACall.Plugin;

    public class InACallFactory : IFactory
    {
        #region IFactory members
        /// <summary>
        /// Call newSettings method to create new instance of class implementing Settings interface.
        /// </summary>
        /// <returns>New instance of file backed settings provider</returns>
        public IInACallSettings newSettings()
        {
            return new Impl.PersistentSettings();
        }
        /// <summary>
        /// 
        /// </summary>
        private SkypeServices services;
        /// <summary>
        /// Call this method to acquire access to the skype application
        /// </summary>
        /// <returns>New instance of the skype application</returns>
        public SkypeServices getSkypeServices()
        {
            if (services == null)
            {
                services = new SkypeServices(new SKYPE4COMLib.Skype());
            }
            return services;
        }
        /// <summary>
        /// Call this method to acquire access to the skype application
        /// </summary>
        /// <param name="skype">object implementing skype interfaces</param>
        public void init(SkypeServices skype)
        {
            if (this.services == null)
            {
                this.services = skype;
            }
        }
        /// <summary>
        /// Call this method to create new instance of the controller.
        /// </summary>
        /// <param name="settings">Instance of settings provider</param>
        /// <param name="skype">Instance of skype</param>
        /// <returns></returns>
        public IController newController(IInACallSettings settings, SkypeServices skype)
        {
            return new Impl.SkypeController(settings, skype);
        }
        /// <summary>
        /// Call this method to create new instance of the controller.
        /// </summary>
        /// <param name="settings">Required object, instance of Settings interface</param>
        /// <returns>New instance of controller implementation</returns>
        public IController newController(IInACallSettings settings)
        {
            return new Impl.SkypeController(settings, getSkypeServices());
        }
        /// <summary>
        /// Call this methid to create new preconfigured instance of a Controller
        /// </summary>
        /// <returns>New instance of controller implementation, pre-configured with  valid
        /// instance of Settings implementation</returns>
        public IController newController()
        {
            return new Impl.SkypeController(newSettings(), getSkypeServices());
        }
        #endregion
    }
}
