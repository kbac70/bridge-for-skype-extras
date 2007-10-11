using System;
using System.Collections.Generic;
using System.Text;

using SKYPE4COMLib;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;

    /// <summary>
    /// Thin wrapper class to improve testability of the project 
    /// by avoiding coupling to the Skype co-class. Pass mocks to
    /// the c-tor to enjoy TDD ;)
    /// </summary>
    public sealed class SkypeServices
    {
        private readonly ISkype skype;
        private readonly _ISkypeEvents_Event events;

        public SkypeServices(ISkype skype, _ISkypeEvents_Event events)
        {
            this.skype = skype;
            this.events = events;
        }

        public SkypeServices(Skype skype) : this(skype, skype)
        {
        }

        public ISkype Skype
        {
            get
            {
                return this.skype;
            }
        }

        public _ISkypeEvents_Event Events
        {
            get
            {
                return this.events;
            }
        }
    }
}
