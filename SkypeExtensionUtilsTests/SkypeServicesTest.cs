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

namespace Skype.Extension.Utils.Tests
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class SkypeServicesTest
    {
        private SkypeDummy skype;
        private SkypeDummy events;

        [SetUp]
        protected void SetUp()
        {
            skype = new SkypeDummy();
            events = new SkypeDummy();
        }

        [TearDown]
        protected void TearDown()
        {
            skype = null;
            events = null;
        }

        [Test]
        public void TestGetSkype()
        {
            SkypeServices services = new SkypeServices(skype, events);
            ISkype s = services.Skype;
            Assert.IsNotNull(s);
            Assert.AreSame(skype, s);
        }

        [Test]
        public void TestGetEvents()
        {
            SkypeServices services = new SkypeServices(skype, events);
            _ISkypeEvents_Event e = services.Events;
            Assert.IsNotNull(e);
            Assert.AreSame(events, e);
        }

    }
}
