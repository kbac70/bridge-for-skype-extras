using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;

    public delegate void BeforeEventDeletedHandler(IPluginEvent evt);

    /// <summary>
    /// Plugin event wrapper helping with lifetime management
    /// </summary>
    public class PluginEventDecorator : IPluginEvent
    {
        private IPluginEvent evt;

        public event BeforeEventDeletedHandler BeforeDeleted;

        public PluginEventDecorator(IPluginEvent evt)
        {
            Contract.EnsureArgumentNotNull(evt, "evt");

            this.evt = evt;
        }

        #region IPluginEvent Members

        public void Delete()
        {
            if (this.BeforeDeleted != null)
            {
                this.BeforeDeleted(this);
            }
            this.evt.Delete();
        }

        public string Id
        {
            get { return this.evt.Id; }
        }

        #endregion

    }
}
