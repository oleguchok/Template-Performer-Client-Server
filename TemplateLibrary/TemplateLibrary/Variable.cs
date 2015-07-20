using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateLibrary
{
    public class Variable
    {
        String name;
        ArgumentType argumentType;
        object value;

        public Variable(String name, ArgumentType type)
        {
            this.name = name;
            this.argumentType = type;
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
