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
using System.ComponentModel;

using SKYPE4COMLib;

namespace InACall.Impl
{
    internal class PersistentSettings : AbstractSettings
    {
        public PersistentSettings()
        {
            Properties.InTheCallSettings.Default.PropertyChanged += 
                    new PropertyChangedEventHandler(OnPropertyChanged);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetModified();
        }

        public override bool ShouldChangeMoodText
        {
            get 
            {
                return Properties.InTheCallSettings.Default.ShouldChangeMoodText;
            }
            set
            {
                Properties.InTheCallSettings.Default.ShouldChangeMoodText = value;
            }
        }

        public override string MoodText
        {
            get
            {
                return Properties.InTheCallSettings.Default.MoodText;
            }
            set
            {
                Properties.InTheCallSettings.Default.MoodText = NonNullString(value);
            }
        }

        public override bool ShouldChangeUserStatus
        {
            get
            {
                return Properties.InTheCallSettings.Default.ShouldChangeUserStatus;
            }
            set
            {
                Properties.InTheCallSettings.Default.ShouldChangeUserStatus = value;
            }
        }

        public override bool ShouldRemainInvisible
        {
            get
            {
                return Properties.InTheCallSettings.Default.ShouldRemainInvisible;
            }
            set
            {
                Properties.InTheCallSettings.Default.ShouldRemainInvisible = value;
            }
        }

        public override TUserStatus UserStatus
        {
            get
            {
                return Properties.InTheCallSettings.Default.UserStatus;
            }
            set
            {
                Properties.InTheCallSettings.Default.UserStatus = value;
            }
        }

        public override void Save()
        {
            if (IsModified)
            {
                Properties.InTheCallSettings.Default.Save();
                ResetModified();
            }
        }

        public override bool IsFirstRun
        {
            get
            {
                return Properties.InTheCallSettings.Default.IsFirstRun;
            }
            set
            {
                Properties.InTheCallSettings.Default.IsFirstRun = false;
            }
        }

    }
}
