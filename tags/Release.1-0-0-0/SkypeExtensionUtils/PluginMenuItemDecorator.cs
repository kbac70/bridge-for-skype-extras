using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;

    public delegate void BeforeMenuDeletedHandler(IPluginMenuItem menu);

    /// <summary>
    /// Plugin menu wrapper helping with lifetime management
    /// </summary>
    public class PluginMenuItemDecorator : IPluginMenuItem
    {
        private IPluginMenuItem menu;

        public event BeforeMenuDeletedHandler BeforeDeleted;

        public PluginMenuItemDecorator(IPluginMenuItem menu)
        {
            Contract.EnsureArgumentNotNull(menu, "menu");

            this.menu = menu;
        }

        #region IPluginMenuItem Members

        public string Caption
        {
            set { menu.Caption = value; }
        }

        public void Delete()
        {
            if (this.BeforeDeleted != null)
            {
                this.BeforeDeleted(this);
            }
            this.menu.Delete();
        }

        public bool Enabled
        {
            set { menu.Enabled = value; }
        }

        public string Hint
        {
            set { menu.Hint = value; }
        }

        public string Id
        {
            get { return menu.Id; }
        }

        #endregion
    }
}
