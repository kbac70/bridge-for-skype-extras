using System;
using System.Collections.Generic;
using System.Text;

namespace InACall.Tests
{
    using Rhino.Mocks;
    using Rhino.Mocks.Interfaces;
    using NUnit.Framework;

    using System.Runtime.InteropServices;

    public delegate void BlahHandler(object sender, EventArgs e);

    public interface BlahEventProvider
    {
        event BlahHandler Blah;
    }

    [TestFixture]
    public class EventTest
    {
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            mocks = null;
        }

        private void OnEvent(object sender, EventArgs e)
        {
        }

        private void OnEvent2(object sender, EventArgs e)
        {
        }


        [Test]
        public void TestEventSubscription()
        {
            BlahEventProvider events = mocks.CreateMock <BlahEventProvider>();
            events.Blah += null; 
            IEventRaiser r = LastCall.GetEventRaiser();
            LastCall.IgnoreArguments();
            mocks.ReplayAll();

            events.Blah += this.OnEvent2;
            mocks.VerifyAll();
        }
    }
}
