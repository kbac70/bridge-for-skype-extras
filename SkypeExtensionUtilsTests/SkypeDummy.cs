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

namespace Skype.Extension.Utils.Tests
{
    using SKYPE4COMLib;

    public class SkypeDummy : ISkype, _ISkypeEvents_Event
    {
        #region ISkype Members

        public CallCollection ActiveCalls
        {
            get { return null; }
        }

        public ChatCollection ActiveChats
        {
            get { return null; }
        }

        public IFileTransferCollection ActiveFileTransfers
        {
            get { return null; }
        }

        public string ApiWrapperVersion
        {
            get { return null; }
        }

        public int AsyncSearchUsers(string Target)
        {
            return 0;
        }

        public void Attach(int Protocol, bool Wait)
        {
            
        }

        public TAttachmentStatus AttachmentStatus
        {
            get { return TAttachmentStatus.apiAttachAvailable; }
        }

        public ChatCollection BookmarkedChats
        {
            get { return null; }
        }

        public bool Cache
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public void ChangeUserStatus(TUserStatus newVal)
        {
            
        }

        public ChatCollection Chats
        {
            get { return null; }
        }

        public void ClearCallHistory(string Username, TCallHistory Type)
        {
            
        }

        public void ClearChatHistory()
        {
            
        }

        public void ClearVoicemailHistory()
        {
            
        }

        public Client Client
        {
            get { return null; }
        }

        public bool CommandId
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public ConferenceCollection Conferences
        {
            get { return null; }
        }

        public TConnectionStatus ConnectionStatus
        {
            get { return TConnectionStatus.conConnecting; }
        }

        public Conversion Convert
        {
            get { return null; }
        }

        public Chat CreateChatMultiple(UserCollection pMembers)
        {
            return null;
        }

        public Chat CreateChatWith(string Username)
        {
            return null;
        }

        public Group CreateGroup(string GroupName)
        {
            return null;
        }

        public SmsMessage CreateSms(TSmsMessageType MessageType, string TargetNumbers)
        {
            return null;
        }

        public User CurrentUser
        {
            get { return null; }
        }

        public string CurrentUserHandle
        {
            get { return null; }
        }

        public Profile CurrentUserProfile
        {
            get { return null; }
        }

        public TUserStatus CurrentUserStatus
        {
            get
            {
                return TUserStatus.cusAway;
            }
            set
            {
                
            }
        }

        public GroupCollection CustomGroups
        {
            get { return null; }
        }

        public void DeleteGroup(int GroupId)
        {
            
        }

        public void EnableApiSecurityContext(TApiSecurityContext Context)
        {
            
        }

        public IFileTransferCollection FileTransfers
        {
            get { return null; }
        }

        public string FriendlyName
        {
            set { }
        }

        public UserCollection Friends
        {
            get { return null; }
        }

        public GroupCollection Groups
        {
            get { return null; }
        }

        public GroupCollection HardwiredGroups
        {
            get { return null; }
        }

        public CallCollection MissedCalls
        {
            get { return null; }
        }

        public ChatCollection MissedChats
        {
            get { return null; }
        }

        public ChatMessageCollection MissedMessages
        {
            get { return null; }
        }

        public SmsMessageCollection MissedSmss
        {
            get { return null; }
        }

        public VoicemailCollection MissedVoicemails
        {
            get { return null; }
        }

        public bool Mute
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public Call PlaceCall(string Target, string Target2, string Target3, string Target4)
        {
            return null;
        }

        public int Protocol
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public ChatCollection RecentChats
        {
            get { return null; }
        }

        public void ResetCache()
        {
            
        }

        public UserCollection SearchForUsers(string Target)
        {
            return null;
        }

        public void SendCommand(Command pCommand)
        {
            
        }

        public ChatMessage SendMessage(string Username, string Text)
        {
            return null;
        }

        public SmsMessage SendSms(string TargetNumbers, string MessageText, string ReplyToNumber)
        {
            return null;
        }

        public Voicemail SendVoicemail(string Username)
        {
            return null;
        }

        public Settings Settings
        {
            get { return null; }
        }

        public bool SilentMode
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public SmsMessageCollection Smss
        {
            get { return null; }
        }

        public int Timeout
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public UserCollection UsersWaitingAuthorization
        {
            get { return null; }
        }

        public string Version
        {
            get { return null; }
        }

        public VoicemailCollection Voicemails
        {
            get { return null; }
        }

        public bool get_ApiSecurityContextEnabled(TApiSecurityContext Context)
        {
            return false;
        }

        public Application get_Application(string Name)
        {
            return null;
        }

        public Call get_Call(int Id)
        {
            return null;
        }

        public CallCollection get_Calls(string Target)
        {
            return null;
        }

        public Chat get_Chat(string Name)
        {
            return null;
        }

        public Command get_Command(int Id, string Command, string Reply, bool Block, int Timeout)
        {
            return null;
        }

        public Conference get_Conference(int Id)
        {
            return null;
        }

        public Voicemail get_Greeting(string Username)
        {
            return null;
        }

        public ChatMessage get_Message(int Id)
        {
            return null;
        }

        public ChatMessageCollection get_Messages(string Target)
        {
            return null;
        }

        public bool get_Privilege(string Name)
        {
            return false;
        }

        public string get_Profile(string Property)
        {
            return null;
        }

        public string get_Property(string ObjectType, string ObjectId, string PropName)
        {
            return null;
        }

        public User get_User(string Username)
        {
            return null;
        }

        public string get_Variable(string Name)
        {
            return null;
        }

        public Voicemail get_Voicemail(int Id)
        {
            return null;
        }

        public void set_Profile(string Property, string pVal)
        {
            
        }

        public void set_Property(string ObjectType, string ObjectId, string PropName, string pVal)
        {
            
        }

        public void set_Variable(string Name, string pVal)
        {
            
        }

        #region 3.7 Members


        public Chat CreateChatUsingBlob(string Blob)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Chat FindChatUsingBlob(string Blob)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public UserCollection FocusedContacts
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region 3.8 Members


        public string PredictiveDialerCountry
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #endregion

        #region _ISkypeEvents_Event Members

        event _ISkypeEvents_ApplicationConnectingEventHandler _ISkypeEvents_Event.ApplicationConnecting
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ApplicationDatagramEventHandler _ISkypeEvents_Event.ApplicationDatagram
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ApplicationReceivingEventHandler _ISkypeEvents_Event.ApplicationReceiving
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ApplicationSendingEventHandler _ISkypeEvents_Event.ApplicationSending
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ApplicationStreamsEventHandler _ISkypeEvents_Event.ApplicationStreams
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_AsyncSearchUsersFinishedEventHandler _ISkypeEvents_Event.AsyncSearchUsersFinished
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_AttachmentStatusEventHandler _ISkypeEvents_Event.AttachmentStatus
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_AutoAwayEventHandler _ISkypeEvents_Event.AutoAway
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_CallDtmfReceivedEventHandler _ISkypeEvents_Event.CallDtmfReceived
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_CallHistoryEventHandler _ISkypeEvents_Event.CallHistory
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_CallInputStatusChangedEventHandler _ISkypeEvents_Event.CallInputStatusChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_CallSeenStatusChangedEventHandler _ISkypeEvents_Event.CallSeenStatusChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        public _ISkypeEvents_CallStatusEventHandler _CallStatus;
        event _ISkypeEvents_CallStatusEventHandler _ISkypeEvents_Event.CallStatus
        {
            add { _CallStatus += value; }
            remove { _CallStatus -= value; }
        }

        event _ISkypeEvents_CommandEventHandler _ISkypeEvents_Event.Command
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ConnectionStatusEventHandler _ISkypeEvents_Event.ConnectionStatus
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ContactsFocusedEventHandler _ISkypeEvents_Event.ContactsFocused
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_ErrorEventHandler _ISkypeEvents_Event.Error
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_FileTransferStatusChangedEventHandler _ISkypeEvents_Event.FileTransferStatusChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_GroupDeletedEventHandler _ISkypeEvents_Event.GroupDeleted
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_GroupExpandedEventHandler _ISkypeEvents_Event.GroupExpanded
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_GroupUsersEventHandler _ISkypeEvents_Event.GroupUsers
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_GroupVisibleEventHandler _ISkypeEvents_Event.GroupVisible
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_MessageHistoryEventHandler _ISkypeEvents_Event.MessageHistory
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_MessageStatusEventHandler _ISkypeEvents_Event.MessageStatus
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_MuteEventHandler _ISkypeEvents_Event.Mute
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        public _ISkypeEvents_OnlineStatusEventHandler _OnlineStatus;
        event _ISkypeEvents_OnlineStatusEventHandler _ISkypeEvents_Event.OnlineStatus
        {
            add { _OnlineStatus += value; }
            remove { _OnlineStatus -= value; }
        }
        public _ISkypeEvents_PluginEventClickedEventHandler _PluginEventClicked;
        event _ISkypeEvents_PluginEventClickedEventHandler _ISkypeEvents_Event.PluginEventClicked
        {
            add { _PluginEventClicked += value; }
            remove { _PluginEventClicked -= value; }
        }

        public _ISkypeEvents_PluginMenuItemClickedEventHandler _PluginMenuItemClicked;
        event _ISkypeEvents_PluginMenuItemClickedEventHandler _ISkypeEvents_Event.PluginMenuItemClicked
        {
            add { _PluginMenuItemClicked += value; }
            remove { _PluginMenuItemClicked -= value; }
        }

        event _ISkypeEvents_ReplyEventHandler _ISkypeEvents_Event.Reply
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_SmsMessageStatusChangedEventHandler _ISkypeEvents_Event.SmsMessageStatusChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_SmsTargetStatusChangedEventHandler _ISkypeEvents_Event.SmsTargetStatusChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        public _ISkypeEvents_UserMoodEventHandler _UserMood;
        event _ISkypeEvents_UserMoodEventHandler _ISkypeEvents_Event.UserMood
        {
            add { _UserMood += value; }
            remove { _UserMood -= value; }
        }

        public _ISkypeEvents_UserStatusEventHandler _UserStatus;
        event _ISkypeEvents_UserStatusEventHandler _ISkypeEvents_Event.UserStatus
        {
            add { _UserStatus += value; }
            remove { _UserStatus -= value; }
        }

        event _ISkypeEvents_VoicemailStatusEventHandler _ISkypeEvents_Event.VoicemailStatus
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        event _ISkypeEvents_WallpaperChangedEventHandler _ISkypeEvents_Event.WallpaperChanged
        {
            add { throw new Exception("The method or operation is not implemented."); }
            remove { throw new Exception("The method or operation is not implemented."); }
        }

        #region 3.7 Members


        public event _ISkypeEvents_CallTransferStatusChangedEventHandler CallTransferStatusChanged;

        public event _ISkypeEvents_CallVideoReceiveStatusChangedEventHandler CallVideoReceiveStatusChanged;

        public event _ISkypeEvents_CallVideoSendStatusChangedEventHandler CallVideoSendStatusChanged;

        public event _ISkypeEvents_CallVideoStatusChangedEventHandler CallVideoStatusChanged;

        public event _ISkypeEvents_ChatMemberRoleChangedEventHandler ChatMemberRoleChanged;

        public event _ISkypeEvents_ChatMembersChangedEventHandler ChatMembersChanged;

        #endregion

        #region 3.8 Members


        public event _ISkypeEvents_SilentModeStatusChangedEventHandler SilentModeStatusChanged;

        public event _ISkypeEvents_UILanguageChangedEventHandler UILanguageChanged;

        public event _ISkypeEvents_UserAuthorizationRequestReceivedEventHandler UserAuthorizationRequestReceived;

        #endregion
 

        #endregion



   }
}