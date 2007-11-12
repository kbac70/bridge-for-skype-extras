using System;
using System.Collections.Generic;
using System.Text;

using SKYPE4COMLib;

namespace InACall.Impl
{
    internal abstract class AbstractSettings : IInACallSettings
    {
        private bool isModified;

        public AbstractSettings()
        {
        }

        protected string NonNullString(string value)
        {
            return value == null ? "" : value;
        }

        public bool IsModified
        {
            get
            {
                return isModified;
            }
        }

        protected void SetModified()
        {
            isModified = true;
        }

        protected void ResetModified()
        {
            isModified = false;
        }


        public abstract bool ShouldChangeMoodText
        {
            get;
            set;
        }

        public abstract string MoodText
        {
            get;
            set;
        }

        public abstract bool ShouldChangeUserStatus
        {
            get;
            set;
        }

        public abstract bool ShouldRemainInvisible
        {
            get;
            set;
        }

        public abstract TUserStatus UserStatus
        {
            get;
            set;
        }

        public abstract void Save();
    }
}
