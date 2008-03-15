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

    class Plugin : AbstractPluginImpl 
    {
        public Plugin(SkypeServices skype)
            : base (skype)
        {
            base.AfterUserLoggedOut += this.OnAfterUserLoggedOut;
        }

        private bool eventClicked;

        public bool HasEventBeenClicked
        {
            get { return eventClicked; }
        }

        protected override void OnSafeSkypeEventItemClicked(IPluginEvent evnt)
        {
            eventClicked = true;
        }

        private bool menuClicked;

        public bool HasMenuBeenClicked
        {
            get { return menuClicked; }
        }

        protected override void OnSafeSkypeMenuItemClicked(IPluginMenuItem pmi,
                IUserCollection users, TPluginContext pluginContext, String contextId)
        {
            menuClicked = true;
        }

        public new bool IsSkypeRunning
        {
            get
            {
                return base.IsSkypeRunning;
            }
        }

        public new void Cleanup()
        {
            base.Cleanup();

        }

        public bool HasMenu()
        {
            return this.customMenus.ContainsKey(MENU_ID);
        }

        public const string MENU_ID = "menu-id";

        public void AddMenu()
        {
            base.AddMenu(MENU_ID, TPluginContext.pluginContextCall, "caption", "hint", "icon", true, TPluginContactType.pluginContactTypeAll, true);
        }

        public bool HasEvent()
        {
            return this.customEvents.ContainsKey(EVENT_ID);
        }

        public const string EVENT_ID = "event-id";

        public void AddEvent()
        {
            base.AddEvent(EVENT_ID, "caption", "hint");
        }

        private bool afterLoggedOut;

        public bool IsUserLoggedOut
        {
            get { return afterLoggedOut; }
        }

        protected void OnAfterUserLoggedOut()
        {
            afterLoggedOut = true;
        }

        public override void ShowSettingsDlg()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    [TestFixture]
    public class AbstractPluginImplTest 
    {
        private MockRepository mocks;
        private SkypeServices services;
        private ISkype skype;
        private SkypeDummy events;
        private PluginMenuItem menu;
        private PluginEvent evt;
        private Client client;

        [SetUp]
        protected void SetUp()
        {
            mocks = new MockRepository();
            events = new SkypeDummy();
            skype = mocks.CreateMock<ISkype>();
            services = new SkypeServices(skype, events);
        }

        [TearDown]
        protected void TearDown()
        {
            mocks = null;
            services = null;
            events = null;
            skype = null;
            menu = null;
            evt = null;
            client = null;
        }

        private Plugin NewPlugin()
        {
            Plugin plugin = new Plugin(services);
            Assert.IsNotNull(plugin);
            return plugin;
        }

        [Test]
        public void TestCtor()
        {
            Assert.IsNull(events._UserStatus);
            Assert.IsNull(events._PluginEventClicked);
            Assert.IsNull(events._PluginMenuItemClicked);
            AbstractPluginImpl plugin = NewPlugin();
            Assert.AreEqual(1, events._UserStatus.GetInvocationList().Length);
            Assert.AreEqual(1, events._PluginEventClicked.GetInvocationList().Length);
            Assert.AreEqual(1, events._PluginMenuItemClicked.GetInvocationList().Length);
        }

        private void SetIsSkypeRunningExpectations()
        {
            if (client == null)
            {
                client = mocks.CreateMock<Client>();
                Expect.Call(client.IsRunning).Return(true).Repeat.AtLeastOnce();
                Expect.Call(skype.Client).Return(client).Repeat.AtLeastOnce();
            }
        }

        [Test]
        public void TestIsSkypeRunning()
        {
            SetIsSkypeRunningExpectations();
            mocks.ReplayAll();

            Assert.IsTrue(NewPlugin().IsSkypeRunning);
            mocks.VerifyAll();
        }

        private void SetAddMenuExpectations()
        {
            SetIsSkypeRunningExpectations();
            Assert.IsNotNull(client);
            menu = mocks.CreateMock<PluginMenuItem>();
            client.CreateMenuItem(Plugin.MENU_ID,TPluginContext.pluginContextCall,"caption","hint","icon",true,TPluginContactType.pluginContactTypeAll,true);
            LastCall.Return(menu);
            //cleanup
            Expect.Call(menu.Id).Return(Plugin.MENU_ID).Repeat.AtLeastOnce();
            menu.Delete();
        }

        [Test]
        public void TestAddMenu()
        {
            SetAddMenuExpectations();
            mocks.ReplayAll();

            Plugin plugin  = NewPlugin();
            Assert.IsFalse(plugin.HasMenu());
            plugin.AddMenu();
            Assert.IsTrue(plugin.HasMenu());
            plugin.Cleanup();
            mocks.VerifyAll();  
        }

        private void SetAddMenuThrowsOnDuplicatesExpectations()
        {
            SetAddMenuExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddMenuThrowsOnDuplicates()
        {
            SetAddMenuThrowsOnDuplicatesExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();
            Assert.IsFalse(plugin.HasMenu());
            plugin.AddMenu();
            Assert.IsTrue(plugin.HasMenu());
            plugin.AddMenu();
        }

        private void SetAddEventExpectations()
        {
            SetIsSkypeRunningExpectations();
            Assert.IsNotNull(client);
            evt = mocks.CreateMock<PluginEvent>();
            client.CreateEvent(Plugin.EVENT_ID, "caption", "hint");
            LastCall.Return(evt);
            //cleanup - remember to have skype app running...
            Expect.Call(evt.Id).Return(Plugin.EVENT_ID).Repeat.AtLeastOnce();
            evt.Delete();
        }

        [Test]
        public void TestAddEvent()
        {
            SetAddEventExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();
            Assert.IsFalse(plugin.HasEvent());
            plugin.AddEvent();
            Assert.IsTrue(plugin.HasEvent());
            plugin.Cleanup();
            mocks.VerifyAll();
        }

        private void SetAddEventThrowsOnDuplicatesExpectations()
        {
            SetAddEventExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddEventThrowsOnDuplicates()
        {
            SetAddEventThrowsOnDuplicatesExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();
            Assert.IsFalse(plugin.HasEvent());
            plugin.AddEvent();
            Assert.IsTrue(plugin.HasEvent());
            plugin.AddEvent();
        }

        private void SetCleanupExpectations()
        {
            //initialize
            SetAddMenuExpectations();
            SetAddEventExpectations();

            SetIsSkypeRunningExpectations();
        }

        [Test]
        public void TestCleanup()
        {
            SetCleanupExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();

            Assert.IsFalse(plugin.HasMenu());
            plugin.AddMenu();
            Assert.IsTrue(plugin.HasMenu());

            Assert.IsFalse(plugin.HasEvent());
            plugin.AddEvent();
            Assert.IsTrue(plugin.HasEvent());

            plugin.Cleanup();
            mocks.VerifyAll();

            //Assert.IsNull(events._UserStatus);
            //Assert.IsNull(events._PluginEventClicked);
            //Assert.IsNull(events._PluginMenuItemClicked);
            Assert.IsFalse(plugin.HasMenu());
            Assert.IsFalse(plugin.HasEvent());
        }

        private void SetMenuClickedExpectations()
        {
            SetAddMenuExpectations();
        }

        [Test]
        public void TestMenuClicked()
        {
            SetMenuClickedExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();
            plugin.AddMenu();
            Assert.IsNotNull(events._PluginMenuItemClicked);
            Assert.IsFalse(plugin.HasMenuBeenClicked);
            Assert.IsNotNull(menu);
            events._PluginMenuItemClicked(menu, mocks.Stub<UserCollection>(), TPluginContext.pluginContextUnknown, "");
            Assert.IsTrue(plugin.HasMenuBeenClicked);
        }

        private void SetEventClickedExpectations()
        {
            SetAddEventExpectations();
        }

        [Test]
        public void TestEventClicked()
        {
            SetEventClickedExpectations();
            mocks.ReplayAll();

            Plugin plugin = NewPlugin();
            plugin.AddEvent();
            Assert.IsNotNull(events._PluginEventClicked);
            Assert.IsFalse(plugin.HasEventBeenClicked);
            Assert.IsNotNull(evt);
            events._PluginEventClicked(evt);
            Assert.IsTrue(plugin.HasEventBeenClicked);
        }

        [Test]
        public void TestSkypeUserStatusChanged()
        {
            Plugin plugin = NewPlugin();

            Assert.IsNotNull(events._UserStatus);
            Assert.IsFalse(plugin.IsUserLoggedOut);
            events._UserStatus(TUserStatus.cusLoggedOut);
            Assert.IsTrue(plugin.IsUserLoggedOut);            
        }

    }
}
