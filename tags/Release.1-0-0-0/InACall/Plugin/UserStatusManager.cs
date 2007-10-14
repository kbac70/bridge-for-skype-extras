using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Skype.Extension.StatusManager
{
    using SKYPE4COMLib;
    using System.Resources;

    public enum UserStatusOperationType
    {
        DoNotChangeStatus = 0,
        AllowChangingStatus = 1
    }

    public partial class UserStatusManager : UserControl
    {
        public event EventHandler StatusChanged;
        public event EventHandler OperationTypeChanged;

        public UserStatusManager()
        {
            InitializeComponent();

            this.cbxSkypeUserStatus.StatusChanged += this.OnUserStatusChanged;
        }

        private void OnUserStatusChanged(object sender, EventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(sender, e);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxSkypeUserStatus.Enabled = this.rbtChangeUserStatus.Checked;
            this.chkRemainInvisible.Enabled = this.cbxSkypeUserStatus.Enabled;

            if (OperationTypeChanged != null)
            {
                OperationTypeChanged(sender, e);
            }
        }

        public TUserStatus UserStatus
        {
            get { return this.cbxSkypeUserStatus.UserStatus; }
            set { this.cbxSkypeUserStatus.UserStatus = value; }
        }

        public bool ShouldRemainInvisible
        {
            get { return this.chkRemainInvisible.Checked; }
            set { this.chkRemainInvisible.Checked = value; }
        }

        public UserStatusOperationType OperationType
        {
            get
            {
                if (this.rbtChangeUserStatus.Checked)
                {
                    return UserStatusOperationType.AllowChangingStatus;
                }
                else
                {
                    return UserStatusOperationType.DoNotChangeStatus;
                }
            }
            set
            {
                this.rbtChangeUserStatus.Checked = value == UserStatusOperationType.AllowChangingStatus;
                this.rbtNoUserStatusChange.Checked = !this.rbtChangeUserStatus.Checked;
            }
        }

        public string DoNotChangeStatusText
        {
            get { return this.rbtNoUserStatusChange.Text; }
            set { this.rbtNoUserStatusChange.Text = value; }
        }

        public string AllowChangeStatusText
        {
            get { return this.rbtChangeUserStatus.Text; }
            set { this.rbtChangeUserStatus.Text = value; }
        }

        public string RemainInvisibleText
        {
            get { return this.chkRemainInvisible.Text; }
            set { this.chkRemainInvisible.Text = value; }
        }

        public string this[TUserStatus userStatus]
        {
            get { return this.cbxSkypeUserStatus[userStatus]; }
            set { this.cbxSkypeUserStatus[userStatus] = value; }
        }

    }
}
