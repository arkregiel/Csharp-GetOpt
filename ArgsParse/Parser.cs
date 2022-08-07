using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgsParse
{
    public class Parser
    {
        public Dictionary<string, Option> Options;
        public List<string> OptionNamePrefixes = new List<string>();

        public Parser(List<string> optionNamePrefixes = null)
        {
            /* if no path prefixes were specified, use the default, sane list */
            if (optionNamePrefixes == null)
                optionNamePrefixes = new List<string>() { "--", "-", "/" };

            Options = new Dictionary<string, Option>();

            /* convert to a list to clone it */
            OptionNamePrefixes = optionNamePrefixes.ToList();
        }

        public void AddOption(Option opt)
        {
            if (Options.ContainsKey(opt.Name))
                throw new ArgumentException($"Option {opt.Name} already exists");
            Options.Add(opt.Name, opt);
        }

        public void Parse(string[] args)
        {
            Option currentOption = null;
            List<Option> foundOptions = new List<Option>();

            foreach (string arg in args)
            {
                if (currentOption == null)
                {
                    foreach (string prefix in OptionNamePrefixes)
                    {
                        if (arg.StartsWith(prefix) && 
                            Options.ContainsKey(arg.Substring(prefix.Length)))
                        {
                            currentOption = Options[arg.Substring(prefix.Length)];
                            break;
                        }
                    }

                    if (currentOption == null)
                        throw new Exception($"Unknown option: {arg}");

                    if (!currentOption.ExpectsValue)
                    {
                        currentOption.Value = true;
                        foundOptions.Add(currentOption);
                        currentOption = null;
                    }
                }
                else
                {
                    if (currentOption.ExpectsValue)
                    {
                        foreach (string prefix in OptionNamePrefixes)
                        {
                            if (arg.StartsWith(prefix))
                                throw new Exception($"Option {currentOption.Name} expects a value.");
                        }

                        currentOption.Value = arg;
                    }
                    else
                    {
                        currentOption.Value = true;
                    }

                    foundOptions.Add(currentOption);
                    currentOption = null;
                }
            }

            if (currentOption != null && currentOption.ExpectsValue)
                throw new ArgumentException($"Option {currentOption.Name} expects an argument");

            foreach (Option opt in Options.Values)
            {
                if (opt.IsRequired && !foundOptions.Contains(opt))
                    throw new ArgumentException($"Option {opt.Name} is requiered");  
            }
        }
    }
}
