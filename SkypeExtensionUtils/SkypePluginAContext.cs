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
