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
            string[] data = "--help --name kevin --status single --test2 69".Split(' ');
            //data = "--help --name kevin --status".Split(' ');
            //data = "--help --status single".Split(' ');
            //data = "--help --name kevin".Split(' ');
            //data = "--help hilfe --name kevin --status single".Split(' ');

            Parser argParser = new Parser();

            Option help = new Option<bool>("help", false);
            Option name = new Option<string>("name", true, true);
            Option status = new Option<string>("status", true, false, "single");
            Option testNum = new Option<int>("test", true, false, 123);
            Option test2Num = new Option<int>("test2", true, true, 123);
            Option test3Num = new Option<int>("test3", true, false);
            Option nulltest = new Option<string>("null", true, false, null);
            Option abcd = new Option<string>("abcd", true, false, "A");

            argParser.AddOption(help);
            argParser.AddOption(name);
            argParser.AddOption(status);
            argParser.AddOption(testNum);
            argParser.AddOption(test2Num);
            argParser.AddOption(test3Num);
            argParser.AddOption(nulltest);
            argParser.AddOption(abcd);

            try
            {
                argParser.Parse(data);

                foreach (var option in argParser.Options.Values)
                {
                    Console.WriteLine($"{option.Name} => {option.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
