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
        protected const String codeSequencePattern = @"{%[^?@=].*?%}";

        protected abstract String ReplaceCustomText(String templateText);

        protected abstract String ReplaceCodeSequence(String templateText);

        public abstract String ParseTemplate(String templateText);

    }
}
