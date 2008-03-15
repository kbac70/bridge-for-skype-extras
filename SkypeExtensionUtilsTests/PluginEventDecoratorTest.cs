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
    using Skype.Extension.Utils;

    using SKYPE4COMLib;
    using NUnit.Framework;

    using Rhino.Mocks;

    [TestFixture]
    public class PluginEventDecoratorTest
    {
        private IPluginEvent pluginEvent;
        private MockRepository mocks;
        private bool deletedCalled;

        [SetUp]
        protected void SetUp()
        {
            mocks = new MockRepository();
            pluginEvent = mocks.CreateMock<IPluginEvent>();
            deletedCalled = false;
        }

        [TearDown]
        protected void TearDown()
        {
            mocks = null;
            pluginEvent = null;
        }

        private PluginEventDecorator NewDecorator()
        {
            PluginEventDecorator decorator = new PluginEventDecorator(pluginEvent);
            Assert.IsNotNull(decorator);
            return decorator;
        }

        [Test]
        public void TestCtor()
        {
            mocks.ReplayAll();
            NewDecorator();
            mocks.VerifyAll();
        }

        [Test]
        public void TestGetId()
        {
            const string ID = "1";
            Expect.Call(pluginEvent.Id).Return(ID);

            mocks.ReplayAll();
            PluginEventDecorator decorator = NewDecorator();
            Assert.AreEqual(ID, decorator.Id);
            mocks.VerifyAll();
        }

        private void OnBeforeEventDeleted(IPluginEvent evt)
        {
            this.deletedCalled = true;
        }


        [Test]
        public void TestDelete()
        {
            PluginEventDecorator decorator = NewDecorator();
            decorator.BeforeDeleted += this.OnBeforeEventDeleted;
            pluginEvent.Delete();
            mocks.ReplayAll();

            Assert.IsFalse(this.deletedCalled);
            decorator.Delete();
            Assert.IsTrue(this.deletedCalled);
            mocks.VerifyAll();

        }
    }
}
