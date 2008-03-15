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
    using InACall;
    using Skype.Extension.Utils;
    using SKYPE4COMLib;

    using NUnit.Framework;

    [TestFixture]
    public class ControllerTest : AbstractFactoryTestCase
    {
        private IController controller;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            controller = factory.newController();
        }

        [TearDown]
        protected override void TearDown()
        {
            
        }

        [Test]
        public void TestSettings()
        {
            IInACallSettings settings = controller.Settings;
            Assert.IsNotNull(settings);
//            Assert.IsNotInstanceOfType(typeof(InTheCall.Impl.PersistentSettings), settings);
        }

        [Test]
        public void TestSkype()
        {
            ISkype skype = controller.Services.Skype;
            Assert.IsNotNull(skype);
            Assert.AreSame(skype, dummy);
        }

        private IController CreateAndVerifyController(ControllerMocks mocks)
        {
            mocks.SetupCtorExpectations();
            mocks.repo.ReplayAll();

            IFactory factory = new InACallFactory();
            factory.init(new SkypeServices(mocks.skype, mocks.events));
            IController controller = factory.newController(mocks.settings);

            mocks.repo.VerifyAll();

            //TODO investigate why rhinos not working
            //Assert.AreEqual(1, mocks.skypeDummy._CallStatus.GetInvocationList().Length);
            //Assert.AreEqual(1, mocks.skypeDummy._OnlineStatus.GetInvocationList().Length);
            //Assert.AreEqual(1, mocks.skypeDummy._UserMood.GetInvocationList().Length);
            //Assert.AreEqual(1, mocks.skypeDummy._UserStatus.GetInvocationList().Length);

            return controller;
        }

        [Test]
        public void ValidateConstructorExpectations()
        {
            ControllerMocks mocks = new ControllerMocks();
            IController controller = CreateAndVerifyController(mocks);
            Assert.IsNotNull(controller);
        }

        delegate void CallSetupHandler(bool shouldChangeMoodText);

        private void ValidateCustomMoodText(ControllerMocks mocks, 
                CallSetupHandler callSetupHandler, bool shouldChangeMoodText,
                string expectedMood, TCallStatus callStatus)
        {
            mocks.repo.BackToRecordAll();
            callSetupHandler(shouldChangeMoodText);
            mocks.repo.ReplayAll();
            mocks.EmulateCall(callStatus);
            Assert.AreEqual(expectedMood, mocks.profile.RichMoodText);
            mocks.repo.VerifyAll();
        }


        private void ValidateCustomUserStatus(ControllerMocks mocks,
                CallSetupHandler callSetupHandler, bool shouldChangeMoodText,
                bool shouldChangeUserStatus, bool shouldRemainInvisible, 
                TCallStatus callStatus, TUserStatus userStatus)
        {
            mocks.repo.BackToRecordAll();
            callSetupHandler(shouldChangeMoodText);
            mocks.SetupUserStatusExpectations(shouldChangeUserStatus,
                    shouldRemainInvisible,
                    userStatus,
                    callStatus
                );
            mocks.repo.ReplayAll();
            mocks.EmulateCall(callStatus);
            Assert.IsNotNull(mocks.profile.RichMoodText);
            mocks.repo.VerifyAll();
        }

        private void ValidateMoodTextWorking(bool shouldChangeMoodText,
            string startMoodText, string endMoodText)
        {
            ControllerMocks mocks = new ControllerMocks();
            IController controller = CreateAndVerifyController(mocks);
            mocks.repo.BackToRecordAll();
            mocks.EmulateAndVerifySkypeAPIConnect();

            Assert.IsNull(mocks.profile.RichMoodText);

            //start the call
            ValidateCustomMoodText(mocks, 
                    mocks.SetupStartCallMoodExpectations,
                    shouldChangeMoodText,
                    startMoodText,
                    TCallStatus.clsInProgress
                );
            //end the call
            ValidateCustomMoodText(mocks, 
                    mocks.SetupEndCallMoodExpectations,
                    shouldChangeMoodText,
                    endMoodText,
                    TCallStatus.clsFinished
                );
        }

        [Test]
        public void ValidateMoodTextManagement()
        {
            ValidateMoodTextWorking(
                    true, //shouldChangeMoodText
                    ControllerMocks.CUSTOM_MOOD_TEXT,
                    ControllerMocks.CUSTOM_MOOD_TEXT
                );
        }

        [Test]
        public void ValidateUserStatusWorking()
        {
            ControllerMocks mocks = new ControllerMocks();
            IController controller = CreateAndVerifyController(mocks);
            mocks.repo.BackToRecordAll();
            mocks.EmulateAndVerifySkypeAPIConnect();

            //start the call
            ValidateCustomUserStatus(mocks,
                    mocks.SetupStartCallMoodExpectations,
                    true, // shouldChangeMoodText
                    true,
                    true,
                    TCallStatus.clsInProgress,
                    ControllerMocks.CUSTOM_USER_STATUS
                );
            //end the call
            ValidateCustomUserStatus(mocks,
                    mocks.SetupEndCallMoodExpectations,
                    true, // shouldChangeMoodText
                    true,
                    true,
                    TCallStatus.clsFinished,
                    ControllerMocks.SKYPE_USER_STATUS
                );
        }

    }
}
