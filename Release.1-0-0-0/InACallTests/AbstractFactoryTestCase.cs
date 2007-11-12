using System;
using System.Collections.Generic;
using System.Text;

namespace InACall.Tests
{
    using InACall;
    using Skype.Extension.Utils;
    using Skype.Extension.Utils.Tests;

    public abstract class AbstractFactoryTestCase
    {
        protected IFactory factory;
        protected SkypeDummy dummy;

        protected virtual void SetUp()
        {
            this.factory = new InACall.InACallFactory();

            //do not connect to skype
            dummy = new SkypeDummy();
            this.factory.init(new SkypeServices(dummy, dummy));
        }

        protected virtual void TearDown()
        {
            dummy = null;
            factory = null;
        }

    }
}
