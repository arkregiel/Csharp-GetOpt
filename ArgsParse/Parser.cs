using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgsParse
{
    public class Parser
    {
        public List<string> OptionNames;

        public List<Option> OptionList;

        public Dictionary<string, object> OptionParsed;

        public string[] Data;

        public Parser(string[] args)
        {
            Data = args;
            OptionNames = new List<string>();
            OptionList = new List<Option>();
            OptionParsed = new Dictionary<string, object>();
        }

        public void AddOption(Option opt)
        {
            OptionList.Add(opt);
            OptionNames.Add(opt.Name);
        }

        public void Parse(string[] args = null)
        {
            if (args != null)
                Data = args;

            string currentOptionName = null;
            Option currentOption = null;

            foreach (string arg in Data)
            {
                if (currentOptionName == null)
                {
                    if (!OptionNames.Contains(arg))
                        throw new Exception($"Unknown option: {arg}");

                    currentOptionName = arg;
                    currentOption = OptionList.Single(o => o.Name == currentOptionName);

                    if (!currentOption.ExpectsValue)
                    {
                        OptionParsed[currentOptionName] = true;
                        currentOptionName = null;
                        currentOption = null;
                    }
                }
                else
                {
                    if (currentOption.ExpectsValue)
                    {
                        if (OptionNames.Contains(arg))
                            throw new Exception($"Option {currentOption.Name} expects an argument");

                        OptionParsed[currentOptionName] = arg;
                    }
                    else
                    {
                        OptionParsed[currentOptionName] = true;
                    }

                    currentOptionName = null;
                    currentOption = null;
                }
            }

            if (currentOption != null && currentOption.ExpectsValue)
                throw new Exception($"Option {currentOption.Name} expects an argument");

            foreach (Option opt in OptionList)
            {
                if (!OptionParsed.ContainsKey(opt.Name))
                {
                    if (opt.IsRequired)
                        throw new Exception($"Option {opt.Name} is requiered");
                    else
                        OptionParsed[opt.Name] = opt.Value;
                }
            }
        }
    }
}
