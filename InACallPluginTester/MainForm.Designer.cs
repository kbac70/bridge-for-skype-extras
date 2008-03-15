namespace InACall.Plugin.Tester
{
    partial class frmMain
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
            this.grpShouldChangeMoodText = new System.Windows.Forms.GroupBox();
            this.txtMood = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShouldChangeMoodText = new System.Windows.Forms.CheckBox();
            this.grpShouldChangeUserStatus = new System.Windows.Forms.GroupBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnController = new System.Windows.Forms.Button();
            this.chkShouldChangeUserStatus = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnLoadSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShouldRemainInvisible = new System.Windows.Forms.CheckBox();
            this.userStatusSelector = new Skype.Extension.Utils.UserStatusSelector();
            this.grpShouldChangeMoodText.SuspendLayout();
            this.grpShouldChangeUserStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShouldChangeMoodText
            // 
            this.grpShouldChangeMoodText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpShouldChangeMoodText.Controls.Add(this.txtMood);
            this.grpShouldChangeMoodText.Controls.Add(this.label1);
            this.grpShouldChangeMoodText.Enabled = false;
            this.grpShouldChangeMoodText.Location = new System.Drawing.Point(12, 12);
            this.grpShouldChangeMoodText.Name = "grpShouldChangeMoodText";
            this.grpShouldChangeMoodText.Size = new System.Drawing.Size(369, 87);
            this.grpShouldChangeMoodText.TabIndex = 0;
            this.grpShouldChangeMoodText.TabStop = false;
            // 
            // txtMood
            // 
            this.txtMood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMood.Location = new System.Drawing.Point(9, 36);
            this.txtMood.Multiline = true;
            this.txtMood.Name = "txtMood";
            this.txtMood.Size = new System.Drawing.Size(354, 40);
            this.txtMood.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mood Text";
            // 
            // chkShouldChangeMoodText
            // 
            this.chkShouldChangeMoodText.AutoSize = true;
            this.chkShouldChangeMoodText.Enabled = false;
            this.chkShouldChangeMoodText.Location = new System.Drawing.Point(18, 12);
            this.chkShouldChangeMoodText.Name = "chkShouldChangeMoodText";
            this.chkShouldChangeMoodText.Size = new System.Drawing.Size(144, 17);
            this.chkShouldChangeMoodText.TabIndex = 3;
            this.chkShouldChangeMoodText.Text = "ShouldChangeMoodText";
            this.chkShouldChangeMoodText.UseVisualStyleBackColor = true;
            this.chkShouldChangeMoodText.CheckedChanged += new System.EventHandler(this.chkShouldChangeMoodText_CheckedChanged);
            // 
            // grpShouldChangeUserStatus
            // 
            this.grpShouldChangeUserStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpShouldChangeUserStatus.Controls.Add(this.userStatusSelector);
            this.grpShouldChangeUserStatus.Enabled = false;
            this.grpShouldChangeUserStatus.Location = new System.Drawing.Point(12, 128);
            this.grpShouldChangeUserStatus.Name = "grpShouldChangeUserStatus";
            this.grpShouldChangeUserStatus.Size = new System.Drawing.Size(369, 53);
            this.grpShouldChangeUserStatus.TabIndex = 2;
            this.grpShouldChangeUserStatus.TabStop = false;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Location = new System.Drawing.Point(87, 21);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 4;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnController
            // 
            this.btnController.Enabled = false;
            this.btnController.Location = new System.Drawing.Point(280, 203);
            this.btnController.Name = "btnController";
            this.btnController.Size = new System.Drawing.Size(95, 23);
            this.btnController.TabIndex = 5;
            this.btnController.Text = "Control Skype";
            this.btnController.UseVisualStyleBackColor = true;
            this.btnController.Click += new System.EventHandler(this.btnController_Click);
            // 
            // chkShouldChangeUserStatus
            // 
            this.chkShouldChangeUserStatus.AutoSize = true;
            this.chkShouldChangeUserStatus.Enabled = false;
            this.chkShouldChangeUserStatus.Location = new System.Drawing.Point(18, 124);
            this.chkShouldChangeUserStatus.Name = "chkShouldChangeUserStatus";
            this.chkShouldChangeUserStatus.Size = new System.Drawing.Size(156, 17);
            this.chkShouldChangeUserStatus.TabIndex = 8;
            this.chkShouldChangeUserStatus.Text = "ShouldChangeOnlineStatus";
            this.chkShouldChangeUserStatus.UseVisualStyleBackColor = true;
            this.chkShouldChangeUserStatus.CheckedChanged += new System.EventHandler(this.chkShouldChangeUserStatus_CheckedChanged);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 240);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(369, 188);
            this.txtLog.TabIndex = 9;
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.Location = new System.Drawing.Point(9, 21);
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Size = new System.Drawing.Size(72, 23);
            this.btnLoadSettings.TabIndex = 10;
            this.btnLoadSettings.Text = "Load";
            this.btnLoadSettings.UseVisualStyleBackColor = true;
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadSettings);
            this.groupBox1.Controls.Add(this.btnSaveSettings);
            this.groupBox1.Location = new System.Drawing.Point(12, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 50);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // chkShouldRemainInvisible
            // 
            this.chkShouldRemainInvisible.AutoSize = true;
            this.chkShouldRemainInvisible.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkShouldRemainInvisible.Enabled = false;
            this.chkShouldRemainInvisible.Location = new System.Drawing.Point(18, 105);
            this.chkShouldRemainInvisible.Name = "chkShouldRemainInvisible";
            this.chkShouldRemainInvisible.Size = new System.Drawing.Size(133, 17);
            this.chkShouldRemainInvisible.TabIndex = 5;
            this.chkShouldRemainInvisible.Text = "ShouldRemainInvisible";
            this.chkShouldRemainInvisible.UseVisualStyleBackColor = true;
            // 
            // userStatusSelector
            // 
            this.userStatusSelector.IncludeInactiveStatuses = false;
            this.userStatusSelector.Location = new System.Drawing.Point(6, 19);
            this.userStatusSelector.Name = "userStatusSelector";
            this.userStatusSelector.Size = new System.Drawing.Size(357, 22);
            this.userStatusSelector.TabIndex = 2;
            this.userStatusSelector.UserStatus = SKYPE4COMLib.TUserStatus.cusOnline;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 440);
            this.Controls.Add(this.chkShouldChangeUserStatus);
            this.Controls.Add(this.chkShouldRemainInvisible);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnController);
            this.Controls.Add(this.chkShouldChangeMoodText);
            this.Controls.Add(this.grpShouldChangeUserStatus);
            this.Controls.Add(this.grpShouldChangeMoodText);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "Busy Calling UI Test";
            this.grpShouldChangeMoodText.ResumeLayout(false);
            this.grpShouldChangeMoodText.PerformLayout();
            this.grpShouldChangeUserStatus.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpShouldChangeMoodText;
        private System.Windows.Forms.TextBox txtMood;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpShouldChangeUserStatus;
        private System.Windows.Forms.CheckBox chkShouldChangeMoodText;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnController;
        private System.Windows.Forms.CheckBox chkShouldChangeUserStatus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnLoadSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShouldRemainInvisible;
        private Skype.Extension.Utils.UserStatusSelector userStatusSelector;
    }
}

