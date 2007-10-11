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
        public void TestEnabled()
        {
            Assert.IsTrue(controller.Enabled);
            controller.Enabled = false;
            Assert.IsFalse(controller.Enabled);
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
            Assert.IsNotNull(CreateAndVerifyController(mocks));
        }

        delegate void CallSetupHandler(bool isControllerEnabled);

        private void ValidateCustomMoodText(ControllerMocks mocks, 
                CallSetupHandler callSetupHandler, bool isControllerEnabled, 
                string expectedMood, TCallStatus callStatus)
        {
            mocks.repo.BackToRecordAll();
            callSetupHandler(isControllerEnabled);
            mocks.repo.ReplayAll();
            mocks.EmulateCall(callStatus);
            Assert.AreEqual(expectedMood, mocks.profile.RichMoodText);
            mocks.repo.VerifyAll();
        }


        private void ValidateCustomUserStatus(ControllerMocks mocks,
                CallSetupHandler callSetupHandler, bool isControllerEnabled, 
                bool shouldChangeUserStatus, bool shouldRemainInvisible, 
                TCallStatus callStatus, TUserStatus userStatus)
        {
            mocks.repo.BackToRecordAll();
            callSetupHandler(isControllerEnabled);
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

        private void ValidateMoodTextWorking(bool shouldEnableController, 
                string startMoodText, string endMoodText)
        {
            ControllerMocks mocks = new ControllerMocks();
            IController controller = CreateAndVerifyController(mocks);
            mocks.repo.BackToRecordAll();
            mocks.EmulateAndVerifySkypeAPIConnect();

            Assert.IsNull(mocks.profile.RichMoodText);

            controller.Enabled = shouldEnableController;
            Assert.AreEqual(shouldEnableController, controller.Enabled);
            //start the call
            ValidateCustomMoodText(mocks,
                    mocks.SetupStartCallMoodExpectations,
                    controller.Enabled,
                    startMoodText,
                    TCallStatus.clsInProgress
                );
            //end the call
            ValidateCustomMoodText(mocks,
                    mocks.SetupEndCallMoodExpectations,
                    controller.Enabled,
                    endMoodText,
                    TCallStatus.clsFinished
                );
        }

        [Test]
        public void ValidateMoodTextForEnabledController()
        {
            ValidateMoodTextWorking(true, 
                    ControllerMocks.CUSTOM_MOOD_TEXT, 
                    ControllerMocks.SKYPE_MOOD_TEXT
                );
        }

        [Test]
        public void ValidateMoodTextForDisabledController()
        {
            ValidateMoodTextWorking(false,
                    null,
                    null
                );
        }

        [Test]
        public void ValidateUserStatusWorking()
        {
            ControllerMocks mocks = new ControllerMocks();
            IController controller = CreateAndVerifyController(mocks);
            mocks.repo.BackToRecordAll();
            mocks.EmulateAndVerifySkypeAPIConnect();

            Assert.IsTrue(controller.Enabled);

            //start the call
            ValidateCustomUserStatus(mocks,
                    mocks.SetupStartCallMoodExpectations,
                    controller.Enabled,
                    true,
                    true,
                    TCallStatus.clsInProgress,
                    ControllerMocks.CUSTOM_USER_STATUS
                );
            //end the call
            ValidateCustomUserStatus(mocks,
                    mocks.SetupEndCallMoodExpectations,
                    controller.Enabled,
                    true,
                    true,
                    TCallStatus.clsFinished,
                    ControllerMocks.SKYPE_USER_STATUS
                );
        }

    }
}
