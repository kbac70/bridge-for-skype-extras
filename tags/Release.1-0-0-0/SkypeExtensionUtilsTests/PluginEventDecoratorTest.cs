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
