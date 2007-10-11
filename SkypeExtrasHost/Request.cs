using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    using System.Collections;
    using System.Collections.Generic;

    class RequestParams
    {
        public RequestParams(IList<string> args)
        {
            this.args = args;
        }

        public string this[int idx]
        {
            get { return args[idx]; }
        }

        private readonly IList<string> args;
    }

    class Request
    {
        public const string CMD_OPEN = "Open";
        public const string CMD_SHOW_SETTINGS_DIALOG = "ShowSettingsDlg";
        public const string CMD_SHUTDOWN = "Shutdown";

        public const char SEPARATOR = '|';

        public const int IDX_MSG_ID = 0;
        public const int IDX_PLUGIN_ID = 1;
        public const int IDX_CMD = 2;

        public const int IDX_OPENCONTEXT_TYPE = 0;
        public const int IDX_OPENCONTEXT_PARTCICIPANTS = 1;
        public const int IDX_OPENCONTEXT_CONTEXTREF = 2;
        public const int IDX_OPENCONTEXT_UNIQUEID = 3;
        public const int IDX_OPENCONTEXT_URIPARAMS = 4;

        public const int IDX_OWNERHANDLE = 0;

        public Request(string cmd)
        {
            this.cmd = cmd;
            this.args = cmd.Split(SEPARATOR);
            
            IList<string> parms = new List<string>();
            for(int i = 3; i < args.Length; i++)
            {
                parms.Add(args[i].Trim());
            }
            this.parms = new RequestParams(parms);
        }
        public bool IsValid 
        { 
            get 
            { 
                return args.Length >= 3; 
            } 
        }
        public string ID 
        { 
            get 
            { 
                return args.Length > 0 ? args[0] : ""; 
            } 
        }
        public string PluginID 
        { 
            get 
            { 
                return args.Length > 1 ? args[1] : ""; 
            } 
        }
        public string Name 
        { 
            get 
            { 
                return args.Length > 2 ? args[2] : ""; 
            } 
        }
        public RequestParams Params 
        { 
            get 
            { 
                return parms; 
            } 
        }
        public string Raw 
        { 
            get 
            { 
                return cmd; 
            } 
        }
        public override string ToString()
        {
            return string.Format("ID: {0}; Name: {1}; Raw: {2}", ID, Name, cmd);
        }
        private readonly RequestParams parms;
        private readonly string cmd;
        private readonly string[] args;
    }

}
