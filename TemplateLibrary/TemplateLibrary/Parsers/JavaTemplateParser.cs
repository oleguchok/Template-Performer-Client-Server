using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TemplateLibrary.Exceptions;

namespace TemplateLibrary.Parsers
{
    public class JavaTemplateParser : TemplateParser
    {
        private int nameForLoopVariable = 0;

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
            return "output.write(String.valueOf(" + variable + "));";
        }

        protected override string ReplaceCodeSequence(string templateText)
        {
            Regex regEx = new Regex(codeSequencePattern);
            MatchEvaluator evaluator = new MatchEvaluator(ReplaceCodeSequenceEvaluator);
            return regEx.Replace(templateText, evaluator);
        }

        protected override string ReplaceNestedSequence(string templateText, string regExPattern,
            MatchEvaluator evaluator)
        {
            var result = templateText;
            Regex regEx = new Regex(regExPattern);
            while (regEx.IsMatch(result))
                result = regEx.Replace(result, evaluator);
            return result;
        }

        private String ReplaceBooleanSequenceEvaluator(Match match)
        {
            var s = FindExpressionInSequence(match.Value, @"(?<={%\?).*?(?=%})");
            var b = GetBooleanContents(match.Value, "(?<={%?).*?(?=%})");
            return "if(" + s +
                "){" + b + "}";
        }

        private String GetBooleanContents(String text, String regExPattern)
        {
            int expressionLength = FindExpressionInSequence(text, regExPattern).Length;
            return text.Substring(expressionLength + 3, text.Length - expressionLength - 8);
        }

        private String ReplaceCodeSequenceEvaluator(Match match)
        {
            if (match.Value.Length == 4 ||
                match.Value.Replace(" ", String.Empty) == String.Empty)
                return "";
            return match.Value.Substring(2, match.Length - 4);
        }

        private String ReplaceCustomTextEvaluator(Match match)
        {
            if (match.Value == "")
                return match.Value;
            return "output.write(String.valueOf(\"" + match.Value + "\"));";
        }

        private String ReplaceLoopSequenceEvaluator(Match match)
        {
            var id = (nameForLoopVariable++).ToString();
            return "for(int ii" + id + "=0;ii" + id + "<"
                + FindExpressionInSequence(match.Value, "(?<={%@).*?(?=%})") +
                ";ii" + id + "++){" + GetLoopContents(match.Value, "(?<={%@).*?(?=%})") + "}";
        }

        private String FindExpressionInSequence(String sequence, String regexPattern)
        {
            Regex regEx = new Regex(regexPattern);
            Match match = regEx.Match(sequence);
            if (match.Value.Replace(" ", String.Empty) == String.Empty)
                throw new TemplateFormatException("There is no variable");
            return match.Value;
        }

        private String GetLoopContents(String text, String regExPattern)
        {
            int expressionLength = FindExpressionInSequence(text, regExPattern).Length;
            return text.Substring(expressionLength + 5, text.Length - expressionLength - 10);
        }

        public override string ParseTemplate(string templateText)
        {
            CheckFalseCodeSequence(templateText);
            templateText = ReplaceSimpleCodeSequenceText(templateText, customTextPattern,
                new MatchEvaluator(ReplaceCustomTextEvaluator));
            templateText = ReplaceCodeSequence(templateText);
            templateText = ReplaceNestedSequence(templateText, loopSequencePattern,
                new MatchEvaluator(ReplaceLoopSequenceEvaluator));
            templateText = ReplaceVariableForOutput(templateText);
            templateText = ReplaceNestedSequence(templateText, booleanSequencePattern,
                new MatchEvaluator(ReplaceBooleanSequenceEvaluator));
            return templateText;
        }
    }
}
