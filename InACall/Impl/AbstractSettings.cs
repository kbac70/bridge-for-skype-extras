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

namespace InACall.Impl
{
    internal abstract class AbstractSettings : IInACallSettings
    {
        private bool isModified;

        public AbstractSettings()
        {
        }

        protected string NonNullString(string value)
        {
            return value == null ? "" : value;
        }

        public bool IsModified
        {
            get
            {
                return isModified;
            }
        }

        protected void SetModified()
        {
            isModified = true;
        }

        protected void ResetModified()
        {
            isModified = false;
        }


        public abstract bool ShouldChangeMoodText
        {
            get;
            set;
        }

        public abstract string MoodText
        {
            get;
            set;
        }

        public abstract bool ShouldChangeUserStatus
        {
            get;
            set;
        }

        public abstract bool ShouldRemainInvisible
        {
            get;
            set;
        }

        public abstract TUserStatus UserStatus
        {
            get;
            set;
        }

        public abstract bool IsFirstRun
        {
            get;
            set;
        }

        public abstract void Save();
    }
}
