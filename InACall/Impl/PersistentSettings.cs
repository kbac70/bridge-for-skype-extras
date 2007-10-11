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

    }
}
