using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateLibrary.Exceptions
{
    public class TemplateRuntimeException : Exception
    {
        public TemplateRuntimeException() : base() { }
        public TemplateRuntimeException(String message) : base(message) { }
        public TemplateRuntimeException(String message, Exception innerException)
            : base(message,innerException)
        {

        }
    }
}
