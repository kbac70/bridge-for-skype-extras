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
