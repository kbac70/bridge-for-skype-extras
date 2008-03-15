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

namespace Tests.Runner
{
    using NUnit.ConsoleRunner;
    using System.IO;

    class Program 
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Out.WriteLine(typeof(Program).Assembly.FullName);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Executing NUnit tests:");
            try
            {
                ConsoleOptions options = new ConsoleOptions(args);
                if (args.Length == 0)
                {
                    Console.Out.WriteLine("Command parameters: test.runner.exe <assembly_containing_unit_tests>");
                    Console.Out.WriteLine("Nothing to test");
                }
                else
                {
                    ConsoleUi runner = new ConsoleUi();
                    runner.Execute(options);
                }
            }
            catch (Exception unhandled)
            {
                Console.Out.WriteLine(unhandled.Message);
            }

            Console.Out.WriteLine("- KBac");

            if (System.Diagnostics.Debugger.IsAttached)
            {
                //stop to allow me looking up the console when debugging
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
