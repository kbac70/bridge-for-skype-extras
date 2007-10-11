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
