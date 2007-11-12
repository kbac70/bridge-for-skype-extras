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
