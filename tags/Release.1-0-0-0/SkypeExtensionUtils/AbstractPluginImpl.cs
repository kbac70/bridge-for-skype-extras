using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;
    using System.Windows.Forms;
    using System.IO;
    using System.Resources;
    using System.Reflection;

    public delegate void AfterUserLoggedOutHandler();

    /// <summary>
    /// Abstract plugin responsible for hooking up the Skype menus. 
    /// It awaits for skype to fire the client logged out to notify descendants.
    /// </summary>
    public abstract class AbstractPluginImpl
    {
        protected readonly SkypeServices services;

        protected readonly Dictionary<string, IPluginMenuItem> customMenus;
        protected readonly Dictionary<string, IPluginEvent> customEvents;
        
        public event AfterUserLoggedOutHandler AfterUserLoggedOut;

        /// <summary>
        /// Call this utitlity to extract picture resource into a file within 
        /// your assembly's directory. Skype requires access to the file containing a 
        /// pictogram to be displayed on the menu of the plugin
        /// </summary>
        /// <param name="assembly">The assembly which the resource is embedded in</param>
        /// <param name="nameSpaceName">The namespace within which the file resource has been defined</param>
        /// <param name="pictureFileName">The desired file name</param>
        /// <param name="resourceName">The name of the resource</param>
        /// <returns>Full file name with drive and directory information</returns>
        public static string ExtractPictureFromResourcesToFile(Assembly assembly, 
                string nameSpaceName,  string pictureFileName, string resourceName)
        {
            Contract.EnsureArgumentNotNull(nameSpaceName, "nameSpaceName");
            Contract.EnsureArgumentNotNull(pictureFileName, "pictureFileName");
            Contract.EnsureArgumentNotNull(resourceName, "resourceName");

            FileInfo fi = new FileInfo(assembly.Location);
            fi = new FileInfo(fi.DirectoryName + Path.DirectorySeparatorChar + pictureFileName);

            if (!fi.Exists)
            {
                try
                {
                    string[] r = assembly.GetManifestResourceNames();
                    System.IO.Stream fromStream = assembly.GetManifestResourceStream(
                            nameSpaceName + ".Resources." + resourceName);
                    if (fromStream == null)
                    {
                        throw new FileLoadException(nameSpaceName + ".Resources." + resourceName);
                    }

                    FileStream toStream = File.Create(fi.FullName);

                    BinaryReader br = new BinaryReader(fromStream);
                    BinaryWriter bw = new BinaryWriter(toStream);
                    try
                    {
                        bw.Write(br.ReadBytes((int)fromStream.Length));
                        bw.Flush();
                    }
                    finally
                    {
                        bw.Close();
                        br.Close();
                    }

                    fi.Refresh();
                }
                finally
                {
                    fi.Refresh();
                    if (fi.Exists)
                    {
                        fi.Delete(); //invalid content
                    }
                }
            }

            return fi.FullName;
        }

        /// <summary>
        /// Call this utitlity to extract picture resource into a file within 
        /// your assembly's directory. Skype requires access to the file containing a 
        /// pictogram to be displayed on the menu of the plugin.
        /// NB: It that the calling assembly hosts the resource!
        /// </summary>
        /// <param name="nameSpaceName">The namespace within which the file resource has been defined</param>
        /// <param name="pictureFileName">The desired file name</param>
        /// <param name="resourceName">The name of the resource</param>
        /// <returns>Full file name with drive and directory information</returns>
        public static string ExtractPictureFromResourcesToFile(string nameSpaceName, 
                string pictureFileName, string resourceName)
        {
            //extract icon if not exists
            Assembly assembly = System.Reflection.Assembly.GetCallingAssembly();
            return ExtractPictureFromResourcesToFile(assembly, nameSpaceName, pictureFileName, resourceName);
        }


        protected AbstractPluginImpl(SkypeServices skype)
        {
            Contract.EnsureArgumentNotNull(skype, "skype");

            this.services = skype;

            this.customMenus = new Dictionary<string, IPluginMenuItem>();
            this.customEvents = new Dictionary<string, IPluginEvent>();

            services.Events.UserStatus += this.OnSkypeUserStatusChanged;
            services.Events.PluginEventClicked += this.OnSkypeEventClicked;
            services.Events.PluginMenuItemClicked += this.OnSkypeMenuItemClicked;
        }


        ~AbstractPluginImpl()
        {
            this.Cleanup();
        }

        protected void AddMenu(string id, TPluginContext pluginContext, 
                string caption, string hint, string iconFileName, bool enabled,
                TPluginContactType contactType, bool shouldUseMulptipleContacts)
        {
            Contract.EnsureArgumentNotNull(id, "id");
            Contract.EnsureArgumentNotNull(caption, "caption");
            Contract.EnsureArgumentNotNull(hint, "hint");
            Contract.EnsureArgumentNotNull(iconFileName, "iconFileName");

            lock (customMenus)
            {
                if (this.customMenus.ContainsKey(id))
                {
                    throw new ArgumentException("id");
                }

                if (services.Skype.Client.IsRunning)
                {
                    PluginMenuItemDecorator menu = new PluginMenuItemDecorator(
                        services.Skype.Client.CreateMenuItem(id,
                            pluginContext,
                            caption,
                            hint,
                            iconFileName,
                            enabled,
                            contactType,
                            shouldUseMulptipleContacts
                        )
                    );

                    menu.BeforeDeleted += this.OnBeforeMenuDeleted;
                    this.customMenus.Add(id, menu);
                }
            }
        }

        private void OnBeforeMenuDeleted(IPluginMenuItem menu)
        {
            lock (customMenus)
            {
                customMenus.Remove(menu.Id);
            }
        }

        protected void AddEvent(string id, string caption, string hint)
        {
            Contract.EnsureArgumentNotNull(id, "id");
            Contract.EnsureArgumentNotNull(caption, "caption");
            Contract.EnsureArgumentNotNull(hint, "hint");

            lock (this.customEvents)
            {
                if (this.customEvents.ContainsKey(id))
                {
                    throw new ArgumentException("id");
                }

                if (services.Skype.Client.IsRunning)
                {
                    PluginEventDecorator evt = new PluginEventDecorator(
                            services.Skype.Client.CreateEvent(id, caption, hint)
                        );

                    evt.BeforeDeleted += this.OnBeforeEventDeleted;
                    this.customEvents.Add(id, evt);

                }
            }
        }

        private void OnBeforeEventDeleted(IPluginEvent evt)
        {
            lock (customEvents)
            {
                customEvents.Remove(evt.Id);
            }
        }

        private void OnSkypeEventClicked(IPluginEvent evnt)
        {
            if (customEvents.ContainsKey(evnt.Id))
            {
                OnSafeSkypeEventItemClicked(evnt);
            }
        }

        /// <summary>
        /// Override this method to provide with implementation of your event clicked event handler.
        /// This method is safe to throw unhandled exceptions as all of them will
        /// be handled by the plugin call stack.
        /// </summary>
        /// <param name="evnt">Clicked Event Data</param>
        protected abstract void OnSafeSkypeEventItemClicked(IPluginEvent evnt);

        private void OnSkypeMenuItemClicked(IPluginMenuItem pmi,
                IUserCollection users, TPluginContext pluginContext, String contextId)
        {
            if (customMenus.ContainsKey(pmi.Id))
            {
                OnSafeSkypeMenuItemClicked(pmi, users, pluginContext, contextId);
            }
        }

        /// <summary>
        /// Override this method to provide with implementation of your menu clicked event handler.
        /// This method is safe to throw unhandled exceptions as all of them will
        /// be handled by the plugin call stack.
        /// </summary>
        /// <param name="pmi">Plugin manu item data</param>
        /// <param name="users">Users the action is applying to</param>
        /// <param name="pluginContext">Context of the action</param>
        /// <param name="contextId">Id of the context</param>
        protected abstract void OnSafeSkypeMenuItemClicked(IPluginMenuItem pmi,
                IUserCollection users, TPluginContext pluginContext, String contextId);


        private void OnSkypeUserStatusChanged(TUserStatus status)
        {
            if (status == TUserStatus.cusLoggedOut)
            {
                try
                {
                    this.AfterUserLoggedOut();  
                }
                finally
                {
                    Cleanup();
                }
            }
        }

        protected bool IsSkypeRunning
        {
            get
            {
                bool isRunning = false;
                try
                {
                    isRunning = SkypeServices.IsSkypeRunning && this.services.Skype.Client.IsRunning;
                }
                catch (Exception) //COM failure
                {
                }
                return isRunning;
            }
        }

        protected void Cleanup()
        {
            if (IsSkypeRunning)
            {

                //services.Events.UserStatus -= this.OnSkypeUserStatusChanged;
                //services.Events.PluginEventClicked -= this.OnSkypeEventClicked;
                //services.Events.PluginMenuItemClicked -= this.OnSkypeMenuItemClicked;

                while (customMenus.Count > 0)
                {
                    Dictionary<string, IPluginMenuItem>.Enumerator enm = customMenus.GetEnumerator();
                    if (enm.MoveNext())
                    {
                        IPluginMenuItem menu = enm.Current.Value;
                        menu.Delete();
                    }
                }

                while (customEvents.Count > 0)
                {
                    Dictionary<string, IPluginEvent>.Enumerator enm = customEvents.GetEnumerator();
                    if (enm.MoveNext())
                    {
                        IPluginEvent evt = enm.Current.Value;
                        evt.Delete();
                    }
                }
            }
        }

        /// <summary>
        /// Override this method to provide implementation responsible with showing the 
        /// relevant settings dialog box as per user request
        /// </summary>
        public abstract void ShowSettingsDlg();

        /// <summary>
        /// Override this method to provide with the custom code responsible with uninitialization of the
        /// plugin in case of the user request to shutdown
        /// </summary>
        public virtual void Shutdown()
        {
            this.Cleanup();
        }

    }
}
