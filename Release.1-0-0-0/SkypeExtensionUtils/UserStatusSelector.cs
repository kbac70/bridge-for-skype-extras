using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;

    [ToolboxBitmap(typeof(resfinder), "Skype.Extension.Utils.SkypeUserStatusSelector.bmp")]
    public partial class UserStatusSelector : UserControl
    {
        const int USER_STATUS_COUNT = 9;

        private readonly Dictionary<TUserStatus, Bitmap> userStatusImages;
        private readonly Dictionary<int, TUserStatus> userStatusIndexes;
        private readonly Dictionary<TUserStatus, string> userStatusNames;

        public event EventHandler StatusChanged;

        public UserStatusSelector()
        {
            InitializeComponent();

            userStatusImages = new Dictionary<TUserStatus, Bitmap>();
            userStatusImages.Add(TUserStatus.cusOnline, UserStatusResources.onlineSmall);
            userStatusImages.Add(TUserStatus.cusSkypeMe, UserStatusResources.skypemeSmall);
            userStatusImages.Add(TUserStatus.cusAway, UserStatusResources.awaySmall);
            userStatusImages.Add(TUserStatus.cusNotAvailable, UserStatusResources.naSmall);
            userStatusImages.Add(TUserStatus.cusDoNotDisturb, UserStatusResources.dndSmall);
            userStatusImages.Add(TUserStatus.cusInvisible, UserStatusResources.invisibleSmall);
            userStatusImages.Add(TUserStatus.cusLoggedOut, UserStatusResources.loggedoutSmall);
            userStatusImages.Add(TUserStatus.cusOffline, UserStatusResources.loggedoutSmall);
            userStatusImages.Add(TUserStatus.cusUnknown, UserStatusResources.unknownSmall);

            userStatusNames = new Dictionary<TUserStatus, string>();
            foreach (TUserStatus userStatus in Enum.GetValues(typeof(TUserStatus)))
            {
                string userStatusName = UserStatusResources.ResourceManager.GetString(
                        Enum.GetName(typeof(TUserStatus), userStatus));

                userStatusNames.Add(userStatus, userStatusName);
            }

            userStatusIndexes = new Dictionary<int, TUserStatus>();
            PopulateUserStatusIndexes(false);

            combo.SelectedIndex = 0;
        }

        private void PopulateUserStatusIndexes(bool shouldIncludeInactiveStatuses)
        {
            int idx = 0;
            userStatusIndexes.Add(idx++, TUserStatus.cusOnline);
            userStatusIndexes.Add(idx++, TUserStatus.cusSkypeMe);
            userStatusIndexes.Add(idx++, TUserStatus.cusAway);
            userStatusIndexes.Add(idx++, TUserStatus.cusNotAvailable);
            userStatusIndexes.Add(idx++, TUserStatus.cusDoNotDisturb);
            userStatusIndexes.Add(idx++, TUserStatus.cusInvisible);
            if (shouldIncludeInactiveStatuses)
            {
                userStatusIndexes.Add(idx++, TUserStatus.cusLoggedOut);
                userStatusIndexes.Add(idx++, TUserStatus.cusOffline);
                userStatusIndexes.Add(idx++, TUserStatus.cusUnknown);
            }

            combo.Items.Clear();
            foreach (TUserStatus userStatus in userStatusIndexes.Values)
            {
                combo.Items.Add(userStatusNames[userStatus]);
            }
        }

        private void combo_DrawItem(object sender, DrawItemEventArgs ea)
        {
            if (combo.DroppedDown)
            {
                ea.DrawBackground();
            }

            if (combo.ContainsFocus)
            {
                ea.DrawFocusRectangle();
            }

            if (userStatusIndexes.ContainsKey(ea.Index))
            {
                TUserStatus userStatus = userStatusIndexes[ea.Index];
                string userStatusText = userStatusNames[userStatus];
                Bitmap image = userStatusImages[userStatus];
                Size imageSize = image.Size;
                Rectangle bounds = ea.Bounds;

                ea.Graphics.DrawString(userStatusText, ea.Font,
                        new SolidBrush(ea.ForeColor), bounds.Left + 2 + imageSize.Width, bounds.Top);
                ea.Graphics.DrawImageUnscaled(image, bounds.Left + 1, bounds.Top);
            }
        }

        public TUserStatus UserStatus
        {
            get 
            {
                if (this.userStatusIndexes.ContainsKey(this.combo.SelectedIndex))
                {
                    return this.userStatusIndexes[this.combo.SelectedIndex];
                }
                else
                {
                    return TUserStatus.cusUnknown;
                }
            }
            set
            {
                Dictionary<int, TUserStatus>.Enumerator enm = userStatusIndexes.GetEnumerator();

                while (enm.MoveNext())
                {
                    if (enm.Current.Value == value)
                    {
                        this.combo.SelectedIndex = enm.Current.Key;
                        break;
                    }
                }
            }
        }

        public string this[TUserStatus userStatus]
        {
            get { return this.userStatusNames[userStatus]; }
            set 
            {
                this.userStatusNames[userStatus] = value;

                Dictionary<int, TUserStatus>.Enumerator e = this.userStatusIndexes.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current.Value == userStatus)
                    {
                        combo.Items[e.Current.Key] = value;
                        break;
                    }
                }
            }
        }

        public bool IncludeInactiveStatuses
        {
            get
            {
                return this.userStatusIndexes.Count == USER_STATUS_COUNT;
            }
            set
            {
                if (this.IncludeInactiveStatuses != value)
                {
                    PopulateUserStatusIndexes(value);
                }
            }
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(sender, e);
            }
        }

    }
}
