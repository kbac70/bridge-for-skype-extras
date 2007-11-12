using System;
using System.Collections.Generic;
using System.Text;

using SKYPE4COMLib;

namespace InACall
{
    public interface IInACallSettings
    {
        /// <summary>
        /// Use its result to detect a setting change
        /// </summary>
        /// <returns>True when any of the properties has changed its value, false otherwise</returns>
        bool IsModified
        {
            get;
        }
        /// <summary>
        /// Property definition, when true, enabling MoodText change when on call
        /// </summary>
        bool ShouldChangeMoodText
        {
            get;
            set;
        }
        /// <summary>
        /// Property definition allowing to define custom mood text to be setup wduring the call
        /// </summary>
        string MoodText
        {
            get;
            set;
        }
        /// <summary>
        /// Property definition, when true, enabling user status change when on call
        /// </summary>
        bool ShouldChangeUserStatus
        {
            get;
            set;
        }

        /// <summary>
        /// When true, no action should be taken when user is invisible
        /// </summary>
        bool ShouldRemainInvisible
        {
            get;
            set;
        }

        /// <summary>
        /// Property defining the desired user status to be displayed when on call
        /// </summary>
        TUserStatus UserStatus
        {
            get;
            set;
        }
        /// <summary>
        /// Persists the settings valuses into a storage
        /// </summary>
        void Save();
    }
}
