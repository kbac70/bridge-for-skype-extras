using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using System.Runtime.InteropServices;
    /// <summary>
    /// InviteContext structure specification as per Skype documentation
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct InviteContext
    {
        public uint MinParticipiant;
        public uint MaxParticipiant;
        public string CustomMsg;
        public string UniqueID;
    }
}
