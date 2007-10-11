using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;
    using Skype.Extension.Utils;
    using System.Threading;

    /// <summary>
    /// Application context responsible for setting up the plugin context.
    /// Context awaits for plugin to fire AfterUserLoggedOut event to terminate execution.
    /// </summary>
    public class SkypePluginAContext : ApplicationContext, ISkypePluginA
    {
        private readonly IPluginFactory factory;
        private readonly AbstractPluginImpl pluginImpl;
        private readonly EventWaitHandle openPluginEvent;
        private readonly Thread pluginEventsWatcher;

        private bool shouldWatchPluginEvents;
        private bool isTerminated;

        public SkypePluginAContext(IPluginFactory pluginFactory)
        {
            //initiate objects
            this.factory = pluginFactory;
            this.ThreadExit += this.OnThreadExited;

            this.shouldWatchPluginEvents = ProcessHelper.RunningInstance() == null;

            if (shouldWatchPluginEvents)
            {
                this.openPluginEvent = new EventWaitHandle(false,
                        EventResetMode.ManualReset, EventWaitHandleName);

                pluginEventsWatcher = new Thread(this.AwaitOpenEvent);
                pluginEventsWatcher.Priority = ThreadPriority.Lowest;
                pluginEventsWatcher.Start();

                this.pluginImpl = factory.newPluginA() as AbstractPluginImpl;
                this.pluginImpl.AfterUserLoggedOut += this.OnAfterUserLoggedOut;
            }
            else
            {
                //skype extras manager is calling open
                try
                {
                    this.openPluginEvent = EventWaitHandle.OpenExisting(this.EventWaitHandleName);
                    if (openPluginEvent != null)
                    {
                        openPluginEvent.Set();
                    }
                }
                catch (Exception)
                {
                    //nop
                }
                finally
                {
                    isTerminated = true;
                }
            }
        }

        private void Exit()
        {
            ExitThread();
        }

        protected string EventWaitHandleName
        {
            get
            {
                return "Global\\" + System.Reflection.Assembly.GetEntryAssembly().FullName;
            }
        }

        private void AwaitOpenEvent()
        {
            while (shouldWatchPluginEvents)
            {
                openPluginEvent.WaitOne();
                if (shouldWatchPluginEvents) //otherwise it was woken up to leave the thread
                {
                    ShowSettingsDlg();
                }
                openPluginEvent.Reset();
            }
        }

        protected virtual void InitializePlugin()
        {
        }

        private void OnAfterUserLoggedOut()
        {
            Exit();
        }

        private void OnThreadExited(object sender, EventArgs e)
        {
            if (shouldWatchPluginEvents)
            {
                shouldWatchPluginEvents = false;
                openPluginEvent.Set();
                pluginEventsWatcher.Join();
            }

            isTerminated = true;
        }

        public bool IsTerminated
        {
            get
            {
                return isTerminated;
            }
        }

        #region ISkypePluginA Members

        public void ShowSettingsDlg()
        {
            this.pluginImpl.ShowSettingsDlg();
        }

        #endregion
    }
}
