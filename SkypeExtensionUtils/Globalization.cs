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

namespace Skype.Extension.Utils
{
    using SKYPE4COMLib;
    using System.Globalization;
    using System.Threading;
    using System.Resources;

    /// <summary>
    /// Utility class with CultureInfo management
    /// </summary>
    public class Globalization
    {
        public static CultureInfo MatchCurrentUICulture(ISkype skype)
        {
            Contract.EnsureArgumentNotNull(skype, "skype");

            return ChangeLanguage(GetCutlureInfo(skype));
        }

        public static CultureInfo ChangeLanguage(CultureInfo cultureInfo)
        {
            Contract.EnsureArgumentNotNull(cultureInfo, "cultureInfo");

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return cultureInfo;
        }

        public static CultureInfo GetCutlureInfo(ISkype skype)
        {
            Contract.EnsureArgumentNotNull(skype, "skype");

            string isoCode = skype.Settings.Language;
            return ISOCodeToCultureInfo(isoCode);
        }
            
        public static CultureInfo ISOCodeToCultureInfo(string isoCode)
        {
            Contract.EnsureArgumentNotNull(isoCode, "isoCode");

            const string DEFAULT_CULTURE = "";
            string cultureInfoName = DEFAULT_CULTURE;

            if (isoCode == null || isoCode == "")
            {
            }
            else if (isoCode.Equals("gb"))
            {
                cultureInfoName = "en-GB"; 
            }
            else if (isoCode.Equals("us"))
            {
                cultureInfoName = "en-US";
            }
            else if (isoCode.Equals("pl"))
            {
                cultureInfoName = "pl-PL";
            }
            else if (isoCode.Equals("es"))
            {
                cultureInfoName = "es-ES";
            }
            //...

            return new CultureInfo(cultureInfoName);
        }
        /// <summary>
        /// Retrieves relevant ResourceManager associated with the Type T
        /// </summary>
        /// <typeparam name="T">type T which comes from the assembly being associated with sattelites</typeparam>
        /// <param name="skype">valid instance of ISkype implementation</param>
        /// <returns>instance of the ResourceManager</returns>
        public static ResourceManager GetResourceManager<T>(ISkype skype)
        {
            Contract.EnsureArgumentNotNull(skype, "skype");
            CultureInfo cultureInfo = Globalization.MatchCurrentUICulture(skype);

            Type resources = typeof(T);
            ResourceManager result = cultureInfo.Name == "" ?
                    new ResourceManager(resources) :
                    new ResourceManager(resources.Name, resources.Assembly);

            return result;
        }

    }
}
