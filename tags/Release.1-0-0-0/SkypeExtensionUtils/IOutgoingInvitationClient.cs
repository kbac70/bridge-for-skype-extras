using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using System.Runtime.InteropServices;
    /// <summary>
    /// IOutgoingInvitationClient interface specification as per Skype documentation
    /// </summary>
    public interface IOutgoingInvitationClient
    {
        void InvitationCompleted(uint invitation, string itemID, bool succeeded, OpenContext openContext);
    }
}
