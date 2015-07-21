using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateLibrary.Exceptions;
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

        protected String ReplaceSimpleCodeSequenceText(String templateText, String regexPattern,
            MatchEvaluator evaluator)
        {
            Regex regEx = new Regex(customTextPattern);
            return regEx.Replace(templateText, evaluator);
        }
        protected abstract String ReplaceVariableForOutput(String templateText);
        protected abstract String ReplaceCodeSequence(String templateText);
        protected abstract String ReplaceNestedSequence(String templateText, String regExPattern,
            MatchEvaluator evaluator);
        public abstract String ParseTemplate(String templateText);

        protected void CheckFalseCodeSequence(String templateText)
        {
            if (!IsItContainTrueCodeSequence(templateText))
                throw new TemplateFormatException("Template must not contain " +
                    "code sequence\"{%\" or \"%}\"");
        }

        private bool IsItContainTrueCodeSequence(String text)
        {
            int i = 0, countOfCodeSeq = 0;
            while (i < text.Length - 1)
            {
                if (text.Substring(i, 2) == "{%" || text.Substring(i, 2) == "%}")
                {
                    countOfCodeSeq++;
                    i += 2;
                }
                else
                    i++;
            }
            return countOfCodeSeq % 2 == 0;
        }

    }
}
