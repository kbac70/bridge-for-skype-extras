using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace InACall.Impl
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;

    internal class SkypeController : IController
    {
        protected readonly IInACallSettings userSettings;

        protected readonly SkypeServices services;

        protected string userDisplayName;

        private IInACallSettings disenagagedSettings;

        private bool isEngaged;

        private bool isEnabled;

        public SkypeController(IInACallSettings userSettings, SkypeServices skype)
        {
            this.userSettings = userSettings;
            this.services = skype;
            this.isEnabled = true;

            Properties.InTheCallSettings.Default.PropertyChanged +=
                    new PropertyChangedEventHandler(OnSettingsChanged);

            this.disenagagedSettings = new VolatileSettings(userSettings);

            this.disenagagedSettings.UserStatus = this.services.Skype.CurrentUserStatus;
            this.services.Events.UserStatus += this.OnSkypeUserStatusChanged;
            this.services.Events.OnlineStatus += this.OnSkypeOnlineStatusChanged;

            IUser user = this.services.Skype.CurrentUser;
            this.userDisplayName = user.DisplayName;
            this.disenagagedSettings.MoodText = user != null ? user.RichMoodText : "";
            this.services.Events.UserMood += this.OnSkypeUserMoodChanged;

            this.services.Events.CallStatus += this.OnSkypeCallStatusChanged;            
        }

        ~SkypeController()
        {
            isEnabled = false;
            CallEnded();
        }

        public SkypeServices Services
        {
            get
            {
                return this.services;
            }
        }

        public IInACallSettings Settings
        {
            get 
            { 
                return this.userSettings; 
            }
        }

        public bool Enabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled == value) return;

                if (isEnabled && isEngaged)
                {
                    CallEnded();
                }

                isEnabled = value;
            }
        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (isEngaged)
            {
            }
        }

        private bool isCurrentUser(User user)
        {
            return user.DisplayName.Equals(userDisplayName);
        }

        private void OnSkypeUserStatusChanged(TUserStatus status)
        {
            if (status == TUserStatus.cusLoggedOut)
            {
                isEngaged = false;
            }
            else if (!isEngaged) 
            {
                this.disenagagedSettings.UserStatus = status;
            }
        }

        private void OnSkypeOnlineStatusChanged(User user, TOnlineStatus status)
        {
            if (isCurrentUser(user) && !isEngaged)
            {
            }
        }

        private void OnSkypeUserMoodChanged(User user, String mood)
        {
            if (isCurrentUser(user) && !isEngaged)
            {
                this.disenagagedSettings.MoodText = mood;
            }
        }

        private void OnSkypeCallStatusChanged(Call call, TCallStatus status)
        {
            switch (call.Type)
            {
                case TCallType.cltIncomingP2P:
                    {
                        if (status == TCallStatus.clsRinging)
                        {
                            hintBusyCalling(call); 
                        }
                        break;
                    }
                case TCallType.cltOutgoingP2P:
                    {
                        if (status == TCallStatus.clsInProgress)
                        {
                            CallStarted();
                        } 
                        else if (status == TCallStatus.clsFinished || 
                            status == TCallStatus.clsRefused ||
                            status == TCallStatus.clsFailed )
                        {
                            CallEnded();                        
                        }
                        break;
                    }
            }
        }

        private void CallStarted()
        {
            this.isEngaged = true;
            IndicateBusyCalling();
        }

        private void CallEnded()
        {
            if (isEngaged)
            {
                isEngaged = false;
                IndicateBusyCalling();
            }
        }

        private void hintBusyCalling(Call call)
        {

        }

        private void IndicateBusyCalling()
        {
            if (!isEnabled)
            {
                return;
            }

            IInACallSettings settings = isEngaged ? userSettings : disenagagedSettings;
            
            if (settings.ShouldChangeMoodText)
            {
                services.Skype.CurrentUserProfile.RichMoodText = settings.MoodText;
            }

            if (settings.ShouldChangeUserStatus &&
                ((disenagagedSettings.UserStatus == TUserStatus.cusInvisible && !userSettings.ShouldRemainInvisible) ||
                  disenagagedSettings.UserStatus != TUserStatus.cusInvisible))
            {
                services.Skype.ChangeUserStatus(settings.UserStatus);
            }
        }
    }
}
