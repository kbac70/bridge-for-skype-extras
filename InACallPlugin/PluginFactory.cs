using System;
using System.Collections.Generic;
using System.Text;

namespace InACall.Plugin
{
    using Skype.Extension.Utils;

    /// <summary>
    /// Public class which is used by the:
    /// - XtrsHost when instantiting the ISkypePluginB implementations (project needs to compiled as class library)
    /// - PluginAContext when instantiating ISkypePLuginA implementations (project needs to be compiled as console application with PluginProgram.Main as its main entry)
    /// </summary>
    public class PluginFactory : IPluginFactory
    {
        public const string INACALL_NS = "InACall.Plugin";
        const string RES_ICON_FILENAME = "icon.png";
        const string ICON_FILENAME = "InACall.png";

        public PluginFactory()
        {
            this.factory = new InACallFactory();
        }

        #region IPluginFactory Members

        public ISkypePluginA newPluginA()
        {
            return new InACallPluginImpl(
                    factory,
                    AbstractPluginImpl.ExtractPictureFromResourcesToFile(
                            INACALL_NS,
                            ICON_FILENAME,
                            RES_ICON_FILENAME
                     )
                );
        }

        public ISkypePluginB newPluginB()
        {
            return new InACallPluginImpl(factory);
        }

        #endregion

        private readonly InACallFactory factory;
    }
}
