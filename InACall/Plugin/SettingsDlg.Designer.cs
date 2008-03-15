// Copyright 2007 InACall Skype Plugin by KBac Labs 
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this product except in compliance with the License. You may obtain a copy of the License at 
//	http://www.apache.org/licenses/LICENSE-2.0 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.

namespace InACall.Plugin
{
    partial class SettingsDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDlg));
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.txtMood = new System.Windows.Forms.RichTextBox();
            this.rbtAllowMoodChange = new System.Windows.Forms.RadioButton();
            this.rbtNoMoodChange = new System.Windows.Forms.RadioButton();
            this.userStatusManager = new Skype.Extension.StatusManager.UserStatusManager();
            this.picChangeUserStatus = new System.Windows.Forms.PictureBox();
            this.picChangeMoodText = new System.Windows.Forms.PictureBox();
            this.picLine = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.header = new System.Windows.Forms.TextBox();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.grpBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeUserStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeMoodText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).BeginInit();
            this.grpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBody
            // 
            this.grpBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBody.BackColor = System.Drawing.SystemColors.Window;
            this.grpBody.Controls.Add(this.txtMood);
            this.grpBody.Controls.Add(this.rbtAllowMoodChange);
            this.grpBody.Controls.Add(this.rbtNoMoodChange);
            this.grpBody.Controls.Add(this.userStatusManager);
            this.grpBody.Controls.Add(this.picChangeUserStatus);
            this.grpBody.Controls.Add(this.picChangeMoodText);
            this.grpBody.Controls.Add(this.picLine);
            this.grpBody.Location = new System.Drawing.Point(6, 34);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(265, 174);
            this.grpBody.TabIndex = 9;
            this.grpBody.TabStop = false;
            // 
            // txtMood
            // 
            this.txtMood.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMood.Enabled = false;
            this.txtMood.Location = new System.Drawing.Point(70, 51);
            this.txtMood.Multiline = false;
            this.txtMood.Name = "txtMood";
            this.txtMood.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtMood.Size = new System.Drawing.Size(159, 21);
            this.txtMood.TabIndex = 3;
            this.txtMood.Text = "";
            // 
            // rbtAllowMoodChange
            // 
            this.rbtAllowMoodChange.AutoSize = true;
            this.rbtAllowMoodChange.Location = new System.Drawing.Point(50, 29);
            this.rbtAllowMoodChange.Name = "rbtAllowMoodChange";
            this.rbtAllowMoodChange.Size = new System.Drawing.Size(63, 17);
            this.rbtAllowMoodChange.TabIndex = 2;
            this.rbtAllowMoodChange.Text = "<L10N>";
            this.rbtAllowMoodChange.UseVisualStyleBackColor = true;
            this.rbtAllowMoodChange.CheckedChanged += new System.EventHandler(this.rbtMoodChange_CheckedChanged);
            // 
            // rbtNoMoodChange
            // 
            this.rbtNoMoodChange.AutoSize = true;
            this.rbtNoMoodChange.Checked = true;
            this.rbtNoMoodChange.Location = new System.Drawing.Point(50, 11);
            this.rbtNoMoodChange.Name = "rbtNoMoodChange";
            this.rbtNoMoodChange.Size = new System.Drawing.Size(63, 17);
            this.rbtNoMoodChange.TabIndex = 1;
            this.rbtNoMoodChange.TabStop = true;
            this.rbtNoMoodChange.Text = "<L10N>";
            this.rbtNoMoodChange.UseVisualStyleBackColor = true;
            this.rbtNoMoodChange.CheckedChanged += new System.EventHandler(this.rbtMoodChange_CheckedChanged);
            // 
            // userStatusManager
            // 
            this.userStatusManager.AllowChangeStatusText = "<L10N>";
            this.userStatusManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userStatusManager.BackColor = System.Drawing.Color.Transparent;
            this.userStatusManager.DoNotChangeStatusText = "<L10N>";
            this.userStatusManager.Location = new System.Drawing.Point(47, 84);
            this.userStatusManager.Name = "userStatusManager";
            this.userStatusManager.OperationType = Skype.Extension.StatusManager.UserStatusOperationType.AllowChangingStatus;
            this.userStatusManager.RemainInvisibleText = "<L10N>";
            this.userStatusManager.ShouldRemainInvisible = false;
            this.userStatusManager.Size = new System.Drawing.Size(196, 87);
            this.userStatusManager.TabIndex = 4;
            this.userStatusManager.UserStatus = SKYPE4COMLib.TUserStatus.cusDoNotDisturb;
            // 
            // picChangeUserStatus
            // 
            this.picChangeUserStatus.Image = global::InACall.Plugin.Properties.Resources.status;
            this.picChangeUserStatus.Location = new System.Drawing.Point(9, 84);
            this.picChangeUserStatus.Name = "picChangeUserStatus";
            this.picChangeUserStatus.Size = new System.Drawing.Size(32, 32);
            this.picChangeUserStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picChangeUserStatus.TabIndex = 14;
            this.picChangeUserStatus.TabStop = false;
            // 
            // picChangeMoodText
            // 
            this.picChangeMoodText.Image = global::InACall.Plugin.Properties.Resources.mood;
            this.picChangeMoodText.InitialImage = null;
            this.picChangeMoodText.Location = new System.Drawing.Point(9, 12);
            this.picChangeMoodText.Name = "picChangeMoodText";
            this.picChangeMoodText.Size = new System.Drawing.Size(32, 32);
            this.picChangeMoodText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picChangeMoodText.TabIndex = 13;
            this.picChangeMoodText.TabStop = false;
            // 
            // picLine
            // 
            this.picLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picLine.Location = new System.Drawing.Point(7, 74);
            this.picLine.Name = "picLine";
            this.picLine.Size = new System.Drawing.Size(248, 6);
            this.picLine.TabIndex = 12;
            this.picLine.TabStop = false;
            this.picLine.Paint += new System.Windows.Forms.PaintEventHandler(this.picLine_Paint);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(115, 227);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "<L10N>";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(196, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "<L10N>";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpHeader
            // 
            this.grpHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHeader.Controls.Add(this.header);
            this.grpHeader.Location = new System.Drawing.Point(6, -1);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(265, 41);
            this.grpHeader.TabIndex = 4;
            this.grpHeader.TabStop = false;
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.SystemColors.Control;
            this.header.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.header.HideSelection = false;
            this.header.Location = new System.Drawing.Point(6, 9);
            this.header.Multiline = true;
            this.header.Name = "header";
            this.header.ReadOnly = true;
            this.header.Size = new System.Drawing.Size(235, 26);
            this.header.TabIndex = 10;
            this.header.TabStop = false;
            this.header.Text = "<L10N>";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.LinkColor = System.Drawing.Color.DimGray;
            this.linkLabel.Location = new System.Drawing.Point(4, 233);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(37, 12);
            this.linkLabel.TabIndex = 10;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "<L10N>";
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(207, 215);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(62, 9);
            this.lblVersion.TabIndex = 11;
            this.lblVersion.Text = "@";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SettingsDlg
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(278, 262);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<L10N>";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.Shown += new System.EventHandler(this.SettingsDlg_Shown);
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeUserStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChangeMoodText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).EndInit();
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBody;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picLine;
        private System.Windows.Forms.PictureBox picChangeUserStatus;
        private System.Windows.Forms.PictureBox picChangeMoodText;
        private System.Windows.Forms.GroupBox grpHeader;
        private Skype.Extension.StatusManager.UserStatusManager userStatusManager;
        private System.Windows.Forms.RadioButton rbtAllowMoodChange;
        private System.Windows.Forms.RadioButton rbtNoMoodChange;
        private System.Windows.Forms.TextBox header;
        private System.Windows.Forms.RichTextBox txtMood;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label lblVersion;
    }
}

