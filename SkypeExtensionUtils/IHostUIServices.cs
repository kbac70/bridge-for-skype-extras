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
