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
            initializeFactory();
        }

        protected virtual void TearDown()
        {
            dummy = null;
            factory = null;
        }

        protected void initializeFactory()
        {
            this.factory.init(new SkypeServices(dummy, dummy));
        }

    }
}
