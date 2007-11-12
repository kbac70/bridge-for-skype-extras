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
