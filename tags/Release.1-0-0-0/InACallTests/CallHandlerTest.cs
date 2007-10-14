using System;
using System.Collections.Generic;
using System.Text;

namespace InACall.Tests
{
    using InACall;
    using NUnit.Framework;
    
    [TestFixture]
    public class CallHandlerTest : AbstractFactoryTestCase
    {
        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
        }

        [TearDown]
        protected override void TearDown()
        {
            
        }

        [Test]
        public void ToBeFilled()
        {
            Assert.IsTrue(true);
        }
    }
}
