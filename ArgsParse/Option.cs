using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgsParse
{
    public class Option
    {
        public string Name = string.Empty;

        private object OptionValue;

        public object Value
        {
            get
            {
                return OptionValue;
            }

            set
            {
                object val;
                if (value is string || value is null)
                    val = value;
                else
                    val = value.ToString();
                OptionValue = FromString(val as string);
            }
        }

        public bool IsRequired = false;

        public bool ExpectsValue = true;

        public Option(string optionName, bool expectsValue, bool isOptionRequired = false, object defaultValue = null)
        {
            Name = "--" + optionName;
            ExpectsValue = expectsValue;
            IsRequired = isOptionRequired;
            Value = defaultValue;
        }

        public virtual object FromString(string val)
        {
            return val;
        }
    }
}
