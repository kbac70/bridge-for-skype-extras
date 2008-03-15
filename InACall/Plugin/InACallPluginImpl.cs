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

namespace InACall.Plugin
{
    using Skype.Extension.Utils;
    using SKYPE4COMLib;
    using System.Windows.Forms;
    using System.Resources;

    /// <summary>
    /// Plugin setting up skype so that when end-user clicks the plugin settings menu,
    /// she is presented with the plugin configuration dialog. 
    /// </summary>
    public class InACallPluginImpl : AbstractPluginImpl, ISkypePluginA, ISkypePluginB
    {
        const string MENU_ID = "InACallPlugin";

        private readonly IFactory factory;
        private readonly IController skypeController;

        private volatile bool settingsDlgVisible;

        /// <summary>
        /// C-tor used for PluginB instantiations
        /// </summary>
        /// <param name="factory">InACall factory instance</param>
        public InACallPluginImpl(IFactory factory)
            : base (factory.getSkypeServices())
        {
            this.factory = factory;
            this.skypeController = factory.newController();

            ResourceManager resourceManager = Globalization.GetResourceManager<Strings>(services.Skype);
        }

        /// <summary>
        /// C-tor used for PluginA instantiations
        /// </summary>
        /// <param name="factory">InACall factory instance</param>
        /// <param name="iconFileName">Location of the icon to be associated with the menu</param>
        public InACallPluginImpl(IFactory factory, string iconFileName)
                : base (factory.getSkypeServices())
        {
            this.factory = factory;
            this.skypeController = factory.newController();

            ResourceManager resourceManager = Globalization.GetResourceManager<Strings>(services.Skype);

            //initialize menu for PluginA
            AddMenu(MENU_ID, 
                    TPluginContext.pluginContextTools,
                    GetMenuCaption(resourceManager),
                    GetMenuHint(resourceManager), 
                    iconFileName, 
                    true, 
                    TPluginContactType.pluginContactTypeUnknown, 
                    true
               );
        }

        private string GetMenuCaption(ResourceManager rm)
        {
            return rm.GetString("mnuSettings");
        }

        private string GetMenuHint(ResourceManager rm)
        {
            return rm.GetString("mnuSettingsHint");
        }

        #region ISkypePluginA Members

        public override void ShowSettingsDlg()
        {
            if (!settingsDlgVisible)
            {
                settingsDlgVisible = true;

                try
                {
                    SettingsDlg settingsDlg = new SettingsDlg(skypeController, factory);

                    if (settingsDlg.ShowDialog() == DialogResult.OK)
                    {
                        skypeController.Settings.Save();
                    }
                }
                finally
                {
                    settingsDlgVisible = false;
                }
            }
        }
        #endregion


        protected override void OnSafeSkypeMenuItemClicked(IPluginMenuItem pmi,
                IUserCollection users, TPluginContext pluginContext, string contextId)
        {
            //the best we can do when trying to keep in sync to the UI language
            //as there is no language changed event
            ResourceManager resourceManager = Globalization.GetResourceManager<Strings>(services.Skype);
            pmi.Caption = GetMenuCaption(resourceManager);
            pmi.Hint = GetMenuHint(resourceManager);

            this.ShowSettingsDlg();
        }

        protected override void OnSafeSkypeEventItemClicked(IPluginEvent evnt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region ISkypePluginB Members

        public void Open(OpenContext Context)
        {
            if (Context.ContextKind == OpenContextType.ctTools ||
                Context.ContextKind == OpenContextType.ctNone)
            {
                ShowSettingsDlg();
            }
        }

        public void ShowSettingsDlg(uint WindowOwner)
        {
            ShowSettingsDlg();
        }

        //public override void Shutdown()
        //{
        //    base.Shutdown();
        //}

        #endregion
    }
}
