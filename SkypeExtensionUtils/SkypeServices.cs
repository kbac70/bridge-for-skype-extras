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

using SKYPE4COMLib;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;
    using System.Runtime.InteropServices;

    using HANDLE = System.IntPtr;

    /// <summary>
    /// Thin wrapper class to improve testability of the project 
    /// by avoiding coupling to the Skype co-class. Pass mocks to
    /// the c-tor to enjoy TDD ;)
    /// </summary>
    public class SkypeServices
    {
        private readonly ISkype skype;
        private readonly _ISkypeEvents_Event events;

        public SkypeServices(ISkype skype, _ISkypeEvents_Event events)
        {
            this.skype = skype;
            this.events = events;
        }

        public SkypeServices(Skype skype) : this(skype, skype)
        {
        }

        public ISkype Skype
        {
            get
            {
                return this.skype;
            }
        }

        public _ISkypeEvents_Event Events
        {
            get
            {
                return this.events;
            }
        }

        public static bool IsSkypeRunning
        {
            get
            {
                return FindWindow("tSkMainForm.UnicodeClass", null).ToInt64() > 0;
            }
        }

        [DllImport("user32.dll")]
        private static extern HANDLE FindWindow(string lpClassName, string lpWindowName);
    }
}
