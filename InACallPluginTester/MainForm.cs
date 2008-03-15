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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using InACall;
using InACall.Plugin;
using Skype.Extension.Utils;
using SKYPE4COMLib;

namespace InACall.Plugin.Tester
{
    public partial class frmMain : Form
    {
        private IInACallSettings settings;

        private IController controller;

        private IFactory factory;

        public frmMain()
        {
            InitializeComponent();
            
            factory = new InACallFactory();
            settings = factory.newSettings();

        }

        private void chkShouldChangeMoodText_CheckedChanged(object sender, EventArgs e)
        {
            grpShouldChangeMoodText.Enabled = chkShouldChangeMoodText.Checked;
            if (chkShouldChangeMoodText.Enabled)
            {
                settings.ShouldChangeMoodText = chkShouldChangeMoodText.Checked;
                log("ShouldChangeMoodText changed to " + settings.ShouldChangeMoodText.ToString());
            }
        }

        private void chkShouldChangeUserStatus_CheckedChanged(object sender, EventArgs e)
        {
            grpShouldChangeUserStatus.Enabled = chkShouldChangeUserStatus.Checked;
            if (chkShouldChangeUserStatus.Enabled)
            {
                settings.ShouldChangeUserStatus = chkShouldChangeUserStatus.Checked;
                log("ShouldChangeUserStatus changed to " + settings.ShouldChangeUserStatus.ToString());
            }
        }


        private void LoadSettings()
        {
            chkShouldChangeMoodText.Checked = settings.ShouldChangeMoodText;
            txtMood.Text = settings.MoodText;
            chkShouldRemainInvisible.Checked = settings.ShouldRemainInvisible;
            chkShouldChangeUserStatus.Checked = settings.ShouldChangeUserStatus;
            userStatusSelector.UserStatus = settings.UserStatus;

            chkShouldRemainInvisible.Enabled = true;

            log("settings loaded");
        }

        private void log(String msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
            txtLog.ScrollToCaret();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            settings.ShouldChangeMoodText = chkShouldChangeMoodText.Checked;
            settings.MoodText = txtMood.Text;

            settings.ShouldRemainInvisible = chkShouldRemainInvisible.Checked;

            settings.ShouldChangeUserStatus = chkShouldChangeMoodText.Checked;
            settings.UserStatus = userStatusSelector.UserStatus;

            settings.Save();

            log("settings saved");
        }

        private void btnController_Click(object sender, EventArgs e)
        {
            this.controller = factory.newController(settings);
            log("busy calling started");
            btnController.Enabled = false;
        }

        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();

            btnSaveSettings.Enabled = true;
            chkShouldChangeMoodText.Enabled = true;
            chkShouldChangeUserStatus.Enabled = true;
            btnController.Enabled = true;
        }

        private void cmbShouldChangeUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkShouldChangeUserStatus.Enabled)
            {
                settings.UserStatus = userStatusSelector.UserStatus;
                log("UserStatus changed");
            }
        }

    }
}