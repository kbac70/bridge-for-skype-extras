using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// IHostUIServices interface specification as per Skype documentation
    /// </summary>
    [Guid("D9245AA5-E9E2-4C5B-B1AA-02621D035174")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IHostUIServices
    {
        uint GetDialogOwner();
        bool InstallSPARC(string fileName, string itemID);
        bool OrderLicense(string drmID);
        bool CancelInvitation(uint invitation);
        void ShowItemMenu(string itemID, int x, int y);
        void OpenItem(string itemID);
        void RefreshList(int delayMSec);
        void CallTo(string contactHandle);
        void OpenChat(string contactHandle, string msg);
        void ShowInfo(string contactHandle);
        uint Invite(string itemID, IOutgoingInvitationClient client);
        uint InviteEX(string itemID, IOutgoingInvitationClient client, InviteContext inviteContext);
    }
}
