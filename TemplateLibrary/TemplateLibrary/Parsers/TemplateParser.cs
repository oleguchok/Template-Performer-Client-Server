using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemplateLibrary.Parsers;

namespace TemplateLibrary
{
    public abstract class TemplateParser
    {
        protected const String customTextPattern = @"((?<={%.*?%})|^)(?!%}|{%)(.*?)((?={%.*?%})|$)";

        protected abstract String ReplaceCustomText(String templateText);

        public abstract String ParseTemplate(String templateText);

    }
}
