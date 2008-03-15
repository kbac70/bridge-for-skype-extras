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
    class VolatileSettings : AbstractSettings
    {
        private IInACallSettings userSettings;

        private string moodText;

        private TUserStatus userStatus;


        public VolatileSettings(IInACallSettings userSettings)
        {
            this.userSettings = userSettings;
            this.moodText = "";
        }

        public override bool ShouldChangeMoodText
        {
            get
            {
                return userSettings.ShouldChangeMoodText;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override string MoodText
        {
            get
            {
                return moodText;
            }
            set
            {
                value = NonNullString(value);

                if (!moodText.Equals(value))
                {
                    SetModified();
                }
                moodText = value;
            }
        }

        public override bool ShouldChangeUserStatus
        {
            get
            {
                return userSettings.ShouldChangeUserStatus;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override TUserStatus UserStatus
        {
            get
            {
                return userStatus;
            }
            set
            {
                if (userStatus != value)
                {
                    SetModified();
                }

                userStatus = value;
            }
        }

        public override bool ShouldRemainInvisible
        {
            get
            {
                return userSettings.ShouldRemainInvisible;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override void Save()
        {
            throw new NotSupportedException();
        }

        public override bool IsFirstRun
        {
            get
            {
                return userSettings.IsFirstRun;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

    }
}
