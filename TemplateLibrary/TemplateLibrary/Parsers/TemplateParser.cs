using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateLibrary.Parsers;

namespace TemplateLibrary
{
    public abstract class TemplateParser
    {
        protected const String customTextPattern = @"((?<={%.*?%})|^)(?!%}|{%)(.*?)((?={%.*?%})|$)";
        protected const String codeSequencePattern = @"{%([^?@=].*?|)%}";
        protected const String loopSequencePattern = @"{%@.*?%}(?>(?!{%@.*?%}|{%@%}).|" + 
            "{%@.*?%}(?<Depth>)|{%@%}(?<-Depth>))*(?(Depth)(?!)){%@%}";
        protected const String variableForOutputPattern = @"{%=.*?%}";
        protected const String booleanSequencePattern = @"{%\?.*?%}(?>(?!{%\?.*?%}|{%\?%}).|" +
            @"{%\?.*?%}(?<Depth>)|{%\?%}(?<-Depth>))*(?(Depth)(?!)){%\?%}";

        protected abstract String ReplaceCustomText(String templateText);
        protected abstract String ReplaceVariableForOutput(String templateText);
        protected abstract String ReplaceCodeSequence(String templateText);
        protected abstract String ReplaceNestedSequence(String templateText, String regExPattern,
            MatchEvaluator evaluator);
        public abstract String ParseTemplate(String templateText);

    }
}
