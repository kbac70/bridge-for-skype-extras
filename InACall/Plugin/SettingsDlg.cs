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
using System.Resources;
using System.Globalization;
using System.Reflection;

[assembly: NeutralResourcesLanguageAttribute("en", UltimateResourceFallbackLocation.MainAssembly)]
namespace InACall.Plugin
{
    using SKYPE4COMLib;
    using InACall;
    using Skype.Extension.Utils;
    using Skype.Extension.StatusManager;


    public partial class SettingsDlg : Form
    {
        private readonly IController controller;
        private readonly IFactory factory;
        private string defaultMoodText;

        public SettingsDlg(IController controller, IFactory factory)
        {
            InitializeComponent();

            this.controller = controller;
            this.factory = factory;

            rbtNoMoodChange.Focus();
       
            PopulateUserStatusNames();

            MatchLanguage();
        }


        private string NameText(Control c)
        {
            return c.Name + "Text";
        }

        public void MatchLanguage()
        {
            ResourceManager resourceManager = Globalization.GetResourceManager<Strings>(
                controller.Services.Skype);
            
            this.Text = resourceManager.GetString("SettingsFormText");
            this.btnCancel.Text = resourceManager.GetString(NameText(btnCancel));
            this.btnOk.Text = resourceManager.GetString(NameText(btnOk));
            this.rbtNoMoodChange.Text = resourceManager.GetString(NameText(rbtNoMoodChange));
            this.rbtAllowMoodChange.Text = resourceManager.GetString(NameText(rbtAllowMoodChange));
            this.userStatusManager.DoNotChangeStatusText = resourceManager.GetString("DoNotChangeStatusText");
            this.userStatusManager.AllowChangeStatusText = resourceManager.GetString("AllowChangeStatusText");
            this.userStatusManager.RemainInvisibleText = resourceManager.GetString("RemainInvisibleText");            
            this.header.Text = resourceManager.GetString(NameText(header));
            this.linkLabel.Text = resourceManager.GetString(NameText(linkLabel));
            this.defaultMoodText = resourceManager.GetString("DefaultMoodText");
        }

        private void PopulateUserStatusNames()
        {
            foreach (TUserStatus userStatus in Enum.GetValues(typeof(TUserStatus)))
            {
                userStatusManager[userStatus] = controller.Services.Skype.Convert.UserStatusToText(userStatus);
            }

            userStatusManager.UserStatus = controller.Services.Skype.CurrentUserStatus;
        }

        private void picLine_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(header.ForeColor), new Point(0, 2), new Point(picLine.Width, 2));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            InACall.IInACallSettings settings = controller.Settings;

            settings.ShouldChangeMoodText = this.rbtAllowMoodChange.Checked;
            settings.MoodText = this.txtMood.Text;
            settings.ShouldChangeUserStatus =
                    this.userStatusManager.OperationType == UserStatusOperationType.AllowChangingStatus;
            settings.UserStatus = this.userStatusManager.UserStatus;
            settings.ShouldRemainInvisible = this.userStatusManager.ShouldRemainInvisible;
            settings.IsFirstRun = false;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //const string EMPTY_RTF = "{\\rtf1\\ansi\\ansicpg1252\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1033\\f0\\fs17\\par\r\n}\r\n";
            InACall.IInACallSettings settings = controller.Settings;

            this.rbtAllowMoodChange.Checked = settings.ShouldChangeMoodText;
            this.rbtNoMoodChange.Checked = !this.rbtAllowMoodChange.Checked;
            if (settings.IsFirstRun)// || settings.MoodText.Equals(EMPTY_RTF)))
            {
                //Bitmap bmp = Image.FromHbitmap(Properties.Resources.phone.GetHbitmap());
                //Clipboard.SetDataObject(bmp);
                txtMood.Text = this.defaultMoodText;
                //txtMood.SelectionStart = txtMood.Text.Length;
                //txtMood.Paste();
            }
            else
            {
                this.txtMood.Text = settings.MoodText;
            }
            this.userStatusManager.OperationType = settings.ShouldChangeUserStatus ? 
                    UserStatusOperationType.AllowChangingStatus : 
                    UserStatusOperationType.DoNotChangeStatus;
            this.userStatusManager.UserStatus = settings.UserStatus;
            this.userStatusManager.ShouldRemainInvisible = settings.ShouldRemainInvisible;

            Assembly assembly = Assembly.GetExecutingAssembly();
            lblVersion.Text = assembly.GetName().Version.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void rbtMoodChange_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMood.Enabled = rbtAllowMoodChange.Checked;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gadgets.kbac70.googlepages.com/inacall");
        }

        private void SettingsDlg_Shown(object sender, EventArgs e)
        {
            
        }
    }
}