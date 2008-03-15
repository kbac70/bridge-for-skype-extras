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

    using Rhino.Mocks;

    [TestFixture]    
    public class PluginMenuItemDecoratorTest
    {
        private IPluginMenuItem pluginMenu;
        private MockRepository mocks;
        private bool deletedCalled;

        [SetUp]
        protected void SetUp()
        {
            mocks = new MockRepository();
            pluginMenu = mocks.CreateMock<IPluginMenuItem>();
            deletedCalled = false;
        }

        [TearDown]
        protected void TearDown()
        {
            mocks = null;
            pluginMenu = null;
        }

        private PluginMenuItemDecorator NewDecorator()
        {
            PluginMenuItemDecorator decorator = new PluginMenuItemDecorator(pluginMenu);
            Assert.IsNotNull(decorator);
            return decorator;
        }

        [Test]
        public void TestCTor()
        {
            mocks.ReplayAll();
            PluginMenuItemDecorator decorator = NewDecorator();
            mocks.VerifyAll();
        }

        [Test]
        public void TestCaption()
        {
            const string CAPTION = "caption";
            pluginMenu.Caption = CAPTION;
            mocks.ReplayAll();

            PluginMenuItemDecorator decorator = NewDecorator();
            decorator.Caption = CAPTION;
            mocks.VerifyAll();
        }

        [Test]
        public void TestEnabled()
        {
            const bool ENABLED = true;
            pluginMenu.Enabled = ENABLED;
            mocks.ReplayAll();

            PluginMenuItemDecorator decorator = NewDecorator();
            decorator.Enabled = ENABLED;
            mocks.VerifyAll();
        }

        [Test]
        public void TestHint()
        {
            const string HINT = "hint";
            pluginMenu.Hint = HINT;
            mocks.ReplayAll();

            PluginMenuItemDecorator decorator = NewDecorator();
            decorator.Hint = HINT;
            mocks.VerifyAll();
        }

        [Test]
        public void TestId()
        {
            const string ID = "ID";
            Expect.Call(pluginMenu.Id).Return(ID);
            mocks.ReplayAll();

            PluginMenuItemDecorator decorator = NewDecorator();
            Assert.AreEqual(ID, decorator.Id);
            mocks.VerifyAll();
        }

        private void OnBeforeMenuDeleted(IPluginMenuItem menu)
        {
            this.deletedCalled = true;
        }

        [Test]
        public void TestDelete()
        {
            pluginMenu.Delete();
            mocks.ReplayAll();

            PluginMenuItemDecorator decorator = NewDecorator();
            decorator.BeforeDeleted += this.OnBeforeMenuDeleted;
            Assert.IsFalse(this.deletedCalled);
            decorator.Delete();
            Assert.IsTrue(this.deletedCalled);
            mocks.VerifyAll();
        }


    }
}
