﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InACall.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
    internal sealed partial class InTheCallSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static InTheCallSettings defaultInstance = ((InTheCallSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new InTheCallSettings())));
        
        public static InTheCallSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShouldChangeMoodText {
            get {
                return ((bool)(this["ShouldChangeMoodText"]));
            }
            set {
                this["ShouldChangeMoodText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("In a Call")]
        public string MoodText {
            get {
                return ((string)(this["MoodText"]));
            }
            set {
                this["MoodText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShouldChangeUserStatus {
            get {
                return ((bool)(this["ShouldChangeUserStatus"]));
            }
            set {
                this["ShouldChangeUserStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cusDoNotDisturb")]
        public global::SKYPE4COMLib.TUserStatus UserStatus {
            get {
                return ((global::SKYPE4COMLib.TUserStatus)(this["UserStatus"]));
            }
            set {
                this["UserStatus"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShouldRemainInvisible {
            get {
                return ((bool)(this["ShouldRemainInvisible"]));
            }
            set {
                this["ShouldRemainInvisible"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsFirstRun {
            get {
                return ((bool)(this["IsFirstRun"]));
            }
            set {
                this["IsFirstRun"] = value;
            }
        }
    }
}
