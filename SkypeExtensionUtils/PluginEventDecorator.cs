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
    using SKYPE4COMLib;

    public delegate void BeforeEventDeletedHandler(IPluginEvent evt);

    /// <summary>
    /// Plugin event wrapper helping with lifetime management
    /// </summary>
    public class PluginEventDecorator : IPluginEvent
    {
        private IPluginEvent evt;

        public event BeforeEventDeletedHandler BeforeDeleted;

        public PluginEventDecorator(IPluginEvent evt)
        {
            Contract.EnsureArgumentNotNull(evt, "evt");

            this.evt = evt;
        }

        #region IPluginEvent Members

        public void Delete()
        {
            if (this.BeforeDeleted != null)
            {
                this.BeforeDeleted(this);
            }
            this.evt.Delete();
        }

        public string Id
        {
            get { return this.evt.Id; }
        }

        #endregion

    }
}
