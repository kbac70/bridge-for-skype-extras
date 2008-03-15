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

namespace InACall.Tests
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils.Tests;

    using Rhino.Mocks;
    using Rhino.Mocks.Impl;
    using Rhino.Mocks.Interfaces;
    using Rhino.Mocks.Constraints;

    class ControllerMocks
    {
        public const string SKYPE_MOOD_TEXT = "skype mood text";
        public const string CUSTOM_MOOD_TEXT = "custom mood text";
        public const TUserStatus SKYPE_USER_STATUS = TUserStatus.cusSkypeMe;
        public const TUserStatus CUSTOM_USER_STATUS = TUserStatus.cusDoNotDisturb;
        public const int CALL_ID = 1;
        public const string USER_HANDLE = "userHandle";

        public readonly MockRepository repo;
        public readonly IInACallSettings settings;
        public readonly ISkype skype;
        public readonly _ISkypeEvents_Event events;
        public readonly User user;
        public readonly Call call;
        public readonly Profile profile;

        public readonly SkypeDummy skypeDummy;

        //TODO investigate why rhinos not working
        //private IEventRaiser userStatusEvent;
        //private IEventRaiser callStatusEvent;
        //private IEventRaiser userMoodEvent;

        public ControllerMocks()
        {
            skypeDummy = new SkypeDummy();
            repo = new MockRepository();
            settings = repo.DynamicMock<IInACallSettings>();
            skype = repo.CreateMock<SKYPE4COMLib.ISkype>();
            events = skypeDummy;// repo.CreateMock<SKYPE4COMLib._ISkypeEvents_Event>();
            user = repo.CreateMock<SKYPE4COMLib.User>();
            call = repo.CreateMock<SKYPE4COMLib.Call>();
            profile = repo.Stub<SKYPE4COMLib.Profile>();
        }

        public void SetupCtorExpectations()
        {
            Expect.Call(skype.CurrentUser).Return(user);

            //TODO investigate why rhinos not working
            //events.UserStatus += null;
            //userStatusEvent = LastCall.GetEventRaiser();
            //LastCall.IgnoreArguments();

            //events.OnlineStatus += null;

            //events.UserMood += null;
            //LastCall.IgnoreArguments();
            //userMoodEvent = LastCall.GetEventRaiser();

            //events.CallStatus += null;
            //LastCall.IgnoreArguments();
            //callStatusEvent = LastCall.GetEventRaiser();
        }

        public void SetupStartCallMoodExpectations(bool shouldChangeMoodText)
        {
            SetupCallExpectations(TCallType.cltOutgoingP2P, shouldChangeMoodText);

            Expect.Call(skype.CurrentUserProfile).Return(profile);
            Expect.Call(settings.ShouldChangeMoodText).Return(shouldChangeMoodText);
            Expect.Call(settings.MoodText).Return(CUSTOM_MOOD_TEXT);
            //Expect.Call(settings.ShouldChangeUserStatus).Return(false);
        }

        public void SetupEndCallMoodExpectations(bool shouldChangeMoodText)
        {
            SetupCallExpectations(TCallType.cltOutgoingP2P, shouldChangeMoodText);
            
            Expect.Call(skype.CurrentUserProfile).Return(profile);
            Expect.Call(settings.ShouldChangeMoodText).Return(shouldChangeMoodText);
        }

        public void SetupCallExpectations(TCallType callType, bool shouldChangeMoodText)
        {
            Expect.Call(call.Type).Return(callType).Repeat.Twice();
            Expect.Call(call.Id).Return(CALL_ID);

            if (shouldChangeMoodText)
            {
                Expect.Call(skype.CurrentUser).Return(user);
                Expect.Call(user.Handle).Return(USER_HANDLE).Repeat.Twice();
            }
        }

        public void SetupUserStatusExpectations(bool shouldChangeUserStatus,
                bool shouldRemainInvisible, TUserStatus userStatus, TCallStatus callStatus)
        {
            Expect.Call(settings.ShouldChangeUserStatus).Return(shouldChangeUserStatus);
            if (userStatus == TUserStatus.cusInvisible)
            {
                Expect.Call(settings.ShouldRemainInvisible).Return(shouldRemainInvisible);
            }
            if (shouldChangeUserStatus &&
                (userStatus == TUserStatus.cusInvisible && !shouldRemainInvisible ||
                 userStatus != TUserStatus.cusInvisible)
                )
            {
                if (callStatus == TCallStatus.clsInProgress)
                {
                    Expect.Call(settings.UserStatus).Return(userStatus);
                }
                skype.ChangeUserStatus(userStatus);//Expect.Call
            }
        }

        public void EmulateAndVerifySkypeAPIConnect()        
        {
            Expect.Call(skype.CurrentUser).Return(user);
            Expect.Call(user.Handle).Return(USER_HANDLE).Repeat.AtLeastOnce();
            Expect.Call(user.MoodText).Return(CUSTOM_MOOD_TEXT).Repeat.Twice();
            Expect.Call(skype.CurrentUserStatus).Return(TUserStatus.cusAway);
            repo.ReplayAll();
            skypeDummy._UserMood(user, SKYPE_MOOD_TEXT);
            repo.VerifyAll();

            skypeDummy._UserStatus(SKYPE_USER_STATUS);

            //TODO investigate why rhinos not working
            //userMoodEvent.Raise(user, SKYPE_MOOD_TEXT);
            //userStatusEvent.Raise(SKYPE_USER_STATUS);
        }

        public void EmulateCall(TCallStatus callStatus)
        {
            skypeDummy._CallStatus(call, callStatus);
            //TODO investigate why rhinos not working
            //callStatusEvent.Raise(call, callStatus);
        }
    }

}
