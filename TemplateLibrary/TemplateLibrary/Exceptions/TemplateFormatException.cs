using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary.Exceptions
{
    public class TemplateFormatException : Exception
    {
        public TemplateFormatException() : base() { }

        public TemplateFormatException(String message) : base(message) { }

        public TemplateFormatException(String message, params object[] args)
            : base(string.Format(message, args)) { }
    }
}
