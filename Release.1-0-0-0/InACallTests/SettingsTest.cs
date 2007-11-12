using System;
using System.Collections.Generic;
using System.Text;

namespace InACall.Tests
{
    using InACall;
    using NUnit.Framework;
    using SKYPE4COMLib;

    [TestFixture]
    public class SettingsTest : AbstractFactoryTestCase
    {
        private InACall.IInACallSettings settings;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            settings = factory.newSettings();
        }

        [TearDown]
        protected override void TearDown()
        {

        }

        [Test]
        public void TestIsModified()
        {
            Assert.IsFalse(settings.IsModified);

        }

        [Test]
        public void TestShouldChangeMoodText()
        {
            bool shouldChangeMoodText = settings.ShouldChangeMoodText;
            Assert.IsFalse(settings.IsModified);
            
            try
            {
                settings.ShouldChangeMoodText = !shouldChangeMoodText;
                Save();
            }
            finally
            {
                settings.ShouldChangeMoodText = shouldChangeMoodText;
                Save();
            }
        }

        [Test]
        public void TestMoodText()
        {
            string moodText = settings.MoodText;
            Assert.IsFalse(settings.IsModified);

            try
            {
                settings.MoodText = "<>!!";
                Save();
            }
            finally
            {
                settings.MoodText = moodText;
                Save();
            }
        }

        [Test]
        public void TestShouldRemainInvisible()
        {
            bool shouldRemainInvisible = settings.ShouldRemainInvisible;
            Assert.IsFalse(settings.IsModified);

            try
            {
                settings.ShouldRemainInvisible = !shouldRemainInvisible;
                Save();
            }
            finally
            {
                settings.ShouldRemainInvisible = shouldRemainInvisible;
                Save();
            }
 
        }

        [Test]
        public void TestUserStatus()
        {
            TUserStatus userStatus = settings.UserStatus;
            Assert.IsFalse(settings.IsModified);

            try
            {
                settings.UserStatus = TUserStatus.cusUnknown;
                Save();
            }
            finally
            {
                settings.UserStatus = userStatus;
                Save();
            }
        }

        protected void Save()
        {
            Assert.IsTrue(settings.IsModified);
            settings.Save();
            Assert.IsFalse(settings.IsModified);
        }

    }
}
