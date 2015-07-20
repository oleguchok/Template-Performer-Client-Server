using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateLibrary.Exceptions;

namespace TemplateLibrary.Parsers
{
    public class CSharpTemplateParser : TemplateParser
    {
        protected override String ReplaceCustomText(String templateText)
        {
            Regex regEx = new Regex(customTextPattern);
            MatchEvaluator evaluator = new MatchEvaluator(ReplaceCustomTextEvaluator);
            return regEx.Replace(templateText, evaluator);
        }

        private String ReplaceCustomTextEvaluator(Match match)
        {
            if (match.Value == "")
                return match.Value;
            return "output.Write(\"" + match.Value + "\");";
        }

        private String ReplaceCodeSequenceEvaluator(Match match)
        {
            if (match.Value.Length == 4)
                return "";
            return match.Value.Substring(2, match.Length - 4);
        }

        private bool IsItContainTrueCodeSequence(String text)
        {
            int i = 0, countOfCodeSeq = 0;
            while(i  < text.Length - 1)
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

        public override string ParseTemplate(String templateText)
        {
            CheckFalseCodeSequence(templateText);
            templateText = ReplaceCustomText(templateText);
            templateText = ReplaceCodeSequence(templateText);
            return templateText;
        }

        private void CheckFalseCodeSequence(String templateText)
        {
            if (!IsItContainTrueCodeSequence(templateText))
                throw new TemplateFormatException("Template must not contain " +
                    "code sequence\"{%\" or \"%}\"");
        }

        protected override String ReplaceCodeSequence(String templateText)
        {
            Regex regEx = new Regex(codeSequencePattern);
            MatchEvaluator evaluator = new MatchEvaluator(ReplaceCodeSequenceEvaluator);
            return regEx.Replace(templateText, evaluator);
        }
    }
}
