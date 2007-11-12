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
