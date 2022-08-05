using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgsParse;

namespace ArgsParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = "--help --name kevin --status single".Split(' ');
            //data = "--help --name kevin --status".Split(' ');
            //data = "--help --status single".Split(' ');
            //data = "--help --name kevin".Split(' ');
            //data = "--help hilfe --name kevin --status single".Split(' ');

            Parser argParser = new Parser(data);

            Option help = new Option("help", false);
            Option name = new Option("name", true, true);
            Option status = new Option("status", true, false, "single");
            Option testNum = new Option("test", true, false, 123);

            argParser.AddOption(help);
            argParser.AddOption(name);
            argParser.AddOption(status);
            argParser.AddOption(testNum);

            try
            {
                argParser.Parse();

                foreach (var key in argParser.OptionParsed.Keys)
                {
                    Console.WriteLine($"{key} => {argParser.OptionParsed[key]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }
    }
}
