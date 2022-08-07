using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgsParse
{
    public abstract class Option
    {
        public string Name
        {
            get;
            private set;
        }

        private object OptionValue;

        public object Value
        {
            get
            {
                return OptionValue;
            }

            set
            {
                if (value is string)
                    OptionValue = FromString(value as string);
                else
                    OptionValue = value;
            }
        }

        public bool IsRequired = false;

        public bool ExpectsValue = true;

        public Option(string optionName, bool expectsValue, bool isOptionRequired = false, object defaultValue = null)
        {
            Name = optionName;
            ExpectsValue = expectsValue;
            IsRequired = isOptionRequired;
            Value = defaultValue;
        }

        public abstract object FromString(string val);
    }

    public class Option<T> : Option
    {
        public Option(string optionName, bool expectsValue = true, bool isOptionRequired = false, T defaultValue = default(T)) :
            base(optionName, expectsValue, isOptionRequired, defaultValue) {}

        public override object FromString(string val)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                throw new InvalidOperationException($"The type {typeof(T).FullName} does not support converting from string");
            return converter.ConvertFrom(val);
        }
    }

}
