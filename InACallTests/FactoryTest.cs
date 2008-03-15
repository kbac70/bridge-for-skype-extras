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
    public class FactoryTest : AbstractFactoryTestCase
    {

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
        }

        [TearDown]
        protected override void TearDown()
        {
            base.TearDown();
        }

        [Test]
        public void TestNewSettings()
        {
            IInACallSettings settings = factory.newSettings();
            Assert.IsNotNull(settings);
        }

        [Test]
        public void TestNewController()
        {
            IController controller = factory.newController();
            Assert.IsNotNull(controller);
        }

        [Test]
        public void TestGetSkypeInstance()
        {
            SkypeServices skypeServices = factory.getSkypeServices();
            Assert.IsNotNull(skypeServices);
            Assert.AreSame(skypeServices, factory.getSkypeServices());
        }

    }

}
