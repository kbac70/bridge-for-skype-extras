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
    /// <summary>
    /// Utility class making enforcing the design by contract
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Throws ArgumentNullException when o is null
        /// </summary>
        /// <param name="o">object to check against null</param>
        public static void EnsureArgumentNotNull(object o)
        {
            EnsureArgumentNotNull(o, null);
        }
        /// <summary>
        /// Throws ArgumentNullException when o is null informaing about the param name
        /// </summary>
        /// <param name="o">object to check against null</param>
        /// <param name="argName">object name</param>
        public static void EnsureArgumentNotNull(object o, string argName)
        {
            if (o == null)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}
