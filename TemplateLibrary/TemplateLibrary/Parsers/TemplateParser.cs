using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateLibrary
{
    public abstract class TemplateParser
    {
        protected const String customTextPattern = @"((?<={%.*?%})|^)(?!%}|{%)(.*?)((?={%.*?%})|$)";

       
    }
}
