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
        private int nameForLoopVariable = 0;

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
            templateText = ReplaceLoopSequence(templateText);
            templateText = ReplaceVariableForOutput(templateText);
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

        protected override string ReplaceLoopSequence(string templateText)
        {
            var result = templateText;
            Regex regEx = new Regex(loopSequencePattern);
            MatchEvaluator evaluator = new MatchEvaluator(ReplaceLoopSequenceEvaluator);
            while (regEx.IsMatch(result))
            {
                result = regEx.Replace(result, evaluator);
            }
            return result;
        }

        private String ReplaceLoopSequenceEvaluator(Match match)
        {
            var id = (nameForLoopVariable++).ToString();
            return "for(int ii" + id + "=0;ii" + id + "<" 
                + FindExpressionInLoopSequence(match.Value) + ";ii" 
                + id  + "++){" + GetLoopContents(match.Value) + "}";
        }

        private String FindExpressionInLoopSequence(String sequence)
        {
            Regex regEx = new Regex("(?<={%@).*?(?=%})");
            Match match = regEx.Match(sequence);
            if (match.Value.Replace(" ", String.Empty) == String.Empty)
                throw new TemplateFormatException("There is no loop variable");
            return match.Value;
        }

        private String GetLoopContents(String text)
        {
            int expressionLength = FindExpressionInLoopSequence(text).Length;
            return text.Substring(expressionLength + 5, text.Length - expressionLength - 10);
        }

        protected override string ReplaceVariableForOutput(string templateText)
        {
            Regex regEx = new Regex(variableForOutputPattern);
            MatchEvaluator evaluator = new MatchEvaluator(ReplaceVariableForOutputEvaluator);
            return regEx.Replace(templateText, evaluator);
        }

        private String ReplaceVariableForOutputEvaluator(Match match)
        {
            String variable = match.Value.Substring(3, match.Value.Length - 5);
            variable = variable.Replace(" ", String.Empty);
            if (variable == String.Empty)
                throw new TemplateFormatException("There is no variable for output");
            return "output.Write(" + variable + ");";
        }
    }
}
