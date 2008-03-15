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

namespace InACall.Impl
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;

    internal class SkypeController : IController
    {
        protected const string INVALID_USER_HANLDE = "";

        protected readonly IInACallSettings engagedSettings;

        private readonly IInACallSettings disenagagedSettings;

        protected readonly SkypeServices services;

        private readonly Dictionary<int, ICall> activeCalls;

        protected String ownerHandle;

        public SkypeController(IInACallSettings userSettings, SkypeServices skype)
        {
            this.engagedSettings = userSettings;
            this.services = skype;
            this.ownerHandle = INVALID_USER_HANLDE;
            this.activeCalls = new Dictionary<int,ICall>();

            Properties.InTheCallSettings.Default.PropertyChanged +=
                    new PropertyChangedEventHandler(OnSettingsChanged);

            IUser dummy = services.Skype.CurrentUser; //request permission to use skype API...

            this.disenagagedSettings = new VolatileSettings(userSettings);

            this.services.Events.UserStatus += this.OnSkypeUserStatusChanged;
            this.services.Events.OnlineStatus += this.OnSkypeOnlineStatusChanged;
            this.services.Events.UserMood += this.OnSkypeUserMoodChanged;
            this.services.Events.CallStatus += this.OnSkypeCallStatusChanged;            
        }

        ~SkypeController()
        {
            Disengage(); 
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
                return this.engagedSettings; 
            }
        }

        public bool Engaged
        {
            get
            {
                return this.activeCalls.Count > 0;
            }
        }

        private void Disengage()
        {
            if (Engaged)
            {
                activeCalls.Clear();
                ProcessCall();
            }

        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private void retrieveCurrentUser()
        {
            if (this.ownerHandle == INVALID_USER_HANLDE)
            {
                IUser user = this.services.Skype.CurrentUser;
                if (user != null)
                {
                    this.ownerHandle = user.Handle;
                    this.disenagagedSettings.MoodText = user.MoodText;
                    this.disenagagedSettings.UserStatus = this.services.Skype.CurrentUserStatus;
                }
            }
        }

        private bool IsOwner(User user)
        {
            retrieveCurrentUser();
            return user != null && user.Handle != null && this.ownerHandle == user.Handle;
        }

        private void OnSkypeUserStatusChanged(TUserStatus status)
        {
            if (status == TUserStatus.cusLoggedOut)
            {
                Disengage();
                this.ownerHandle = INVALID_USER_HANLDE;
            }
            else if (!Engaged) 
            {
                this.disenagagedSettings.UserStatus = status;
            }
        }

        private void OnSkypeOnlineStatusChanged(User user, TOnlineStatus status)
        {
            if (IsOwner(user) && !Engaged)
            {
            }
        }

        private void OnSkypeUserMoodChanged(User user, String mood)
        {
            if (IsOwner(user) && !Engaged)
            {
                this.disenagagedSettings.MoodText = user.MoodText;
            }            
        }

        private void OnSkypeCallStatusChanged(Call call, TCallStatus status)
        {
            if (call != null && call.Type != TCallType.cltUnknown)
            {
                if (call.Type == TCallType.cltIncomingP2P)
                {
                    if (status == TCallStatus.clsRinging)
                    {
                        IssueHint(call); 
                    }
                }

                lock (this)
                {
                    if (status == TCallStatus.clsInProgress)
                    {
                        this.activeCalls.Add(call.Id, call);
                        ProcessCall();

                    }
                    else if (status == TCallStatus.clsFinished ||
                        status == TCallStatus.clsRefused ||
                        status == TCallStatus.clsFailed)                        
                    {
                        activeCalls.Remove(call.Id);
                        if (!Engaged)
                        {
                            ProcessCall();
                        }
                    }
                }
            }
        }

        private void IssueHint(Call call)
        {

        }

        private void ProcessCall()
        {
            IInACallSettings settings = Engaged ? engagedSettings : disenagagedSettings;
            
            if (settings.ShouldChangeMoodText)
            {
                if (IsOwner(services.Skype.CurrentUser))
                {
                    services.Skype.CurrentUserProfile.RichMoodText = settings.MoodText;
                }
            }

            if (settings.ShouldChangeUserStatus &&
                ((disenagagedSettings.UserStatus == TUserStatus.cusInvisible && !engagedSettings.ShouldRemainInvisible) ||
                  disenagagedSettings.UserStatus != TUserStatus.cusInvisible))
            {
                services.Skype.ChangeUserStatus(settings.UserStatus);
            }
        }
    }
}
