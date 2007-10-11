using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// Enum indicating that a call to open plugin is initiated by
    /// </summary>
    public enum OpenContextType : int
    {
        /// <summary>
        /// 0: 
        /// </summary>
        ctNone = 0,
        /// <summary>
        /// 1: navigation to URI
        /// </summary>
        ctURI = 1,
        /// <summary>
        /// 2: click on tools menu in client
        /// </summary>
        ctTools = 2,
        /// <summary>
        /// 3: click on contact's context menu in client
        /// </summary>
        ctContact = 3,
        /// <summary>
        /// 4: click on do-more menu in chat window, ContextRef refers to chat id
        /// </summary>
        ctChat = 4,
        /// <summary>
        /// 5: click on call context menu in client, ContextRef refers to call id
        /// </summary>
        ctCall = 5,
        /// <summary>
        /// 6: click on menu in user profile
        /// </summary>
        ctMyself = 6,
        /// <summary>
        /// 7: incoming invitation
        /// </summary>
        ctInvitee = 7,
        /// <summary>
        /// 8: loading on startup, usually plugin should not display a UI when started in this mode
        /// </summary>
        ctStartup = 8,
        /// <summary>
        /// 9: click on plugin in Plugin manager window
        /// </summary>
        ctManager = 9,
        /// <summary>
        /// 10: click on event in client
        /// </summary>
        ctEvent = 10,
    }

    /// <summary>
    /// OpenContext structure as defined by the Skype specification
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpenContext
    {
        /// <summary>
        /// See OpenContextType enumeration
        /// </summary>
        public OpenContextType ContextKind;
        /// <summary>
        /// Comma separated list of user handles with host as leftmost. This can be empty string
        /// </summary>
        public string Participants;
        /// <summary>
        /// Skype object id corresponding to the context. Eg Call Id, Chat Id or Contact Id. This can be empty string
        /// </summary>
        public string ContextRef;
        /// <summary>
        /// A UniqueID of invitation thread. Use this to distinguish different sessions of same participants. This can be empty string
        /// </summary>
        public string UniqueID;
        //{Version 3 entires - Skype 3.1 - see also IHostCapabilities below}
        /// <summary>
        /// URI parameters (name1=value1&name2=value2...). This can be empty string
        /// </summary>
        public string URIParams;      
    }
}
