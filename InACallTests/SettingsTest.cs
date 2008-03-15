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
