namespace Skype.Extension.StatusManager
{
    partial class UserStatusManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbtNoUserStatusChange = new System.Windows.Forms.RadioButton();
            this.rbtChangeUserStatus = new System.Windows.Forms.RadioButton();
            this.chkRemainInvisible = new System.Windows.Forms.CheckBox();
            this.cbxSkypeUserStatus = new Skype.Extension.Utils.UserStatusSelector();
            this.SuspendLayout();
            // 
            // rbtNoUserStatusChange
            // 
            this.rbtNoUserStatusChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtNoUserStatusChange.AutoSize = true;
            this.rbtNoUserStatusChange.Checked = true;
            this.rbtNoUserStatusChange.Location = new System.Drawing.Point(3, 0);
            this.rbtNoUserStatusChange.Name = "rbtNoUserStatusChange";
            this.rbtNoUserStatusChange.Size = new System.Drawing.Size(150, 17);
            this.rbtNoUserStatusChange.TabIndex = 0;
            this.rbtNoUserStatusChange.TabStop = true;
            this.rbtNoUserStatusChange.Text = "Do not change user status";
            this.rbtNoUserStatusChange.UseVisualStyleBackColor = true;
            this.rbtNoUserStatusChange.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbtChangeUserStatus
            // 
            this.rbtChangeUserStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtChangeUserStatus.AutoSize = true;
            this.rbtChangeUserStatus.Location = new System.Drawing.Point(3, 18);
            this.rbtChangeUserStatus.Name = "rbtChangeUserStatus";
            this.rbtChangeUserStatus.Size = new System.Drawing.Size(128, 17);
            this.rbtChangeUserStatus.TabIndex = 1;
            this.rbtChangeUserStatus.Text = "Change user status to";
            this.rbtChangeUserStatus.UseVisualStyleBackColor = true;
            this.rbtChangeUserStatus.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // chkRemainInvisible
            // 
            this.chkRemainInvisible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRemainInvisible.AutoSize = true;
            this.chkRemainInvisible.Enabled = false;
            this.chkRemainInvisible.Location = new System.Drawing.Point(23, 67);
            this.chkRemainInvisible.Name = "chkRemainInvisible";
            this.chkRemainInvisible.Size = new System.Drawing.Size(102, 17);
            this.chkRemainInvisible.TabIndex = 3;
            this.chkRemainInvisible.Text = "Remain invisible";
            this.chkRemainInvisible.UseVisualStyleBackColor = true;
            // 
            // cbxSkypeUserStatus
            // 
            this.cbxSkypeUserStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSkypeUserStatus.Enabled = false;
            this.cbxSkypeUserStatus.Location = new System.Drawing.Point(23, 41);
            this.cbxSkypeUserStatus.Name = "cbxSkypeUserStatus";
            this.cbxSkypeUserStatus.Size = new System.Drawing.Size(148, 22);
            this.cbxSkypeUserStatus.TabIndex = 2;
            this.cbxSkypeUserStatus.UserStatus = SKYPE4COMLib.TUserStatus.cusOnline;
            // 
            // UserStatusManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cbxSkypeUserStatus);
            this.Controls.Add(this.chkRemainInvisible);
            this.Controls.Add(this.rbtChangeUserStatus);
            this.Controls.Add(this.rbtNoUserStatusChange);
            this.Name = "UserStatusManager";
            this.Size = new System.Drawing.Size(172, 87);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtNoUserStatusChange;
        private System.Windows.Forms.RadioButton rbtChangeUserStatus;
        private System.Windows.Forms.CheckBox chkRemainInvisible;
        private Skype.Extension.Utils.UserStatusSelector cbxSkypeUserStatus;
    }
}
