﻿using System;
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
            MatchCollection customTextMatches = regEx.Matches(templateText);
            foreach(Match match in customTextMatches)
            {
                if (match.Value != "")
                    templateText = ReplaceCustomWordByPositionOfMatch(templateText,
                        match);
            }
            return templateText;
        }

        private bool IsItContainTrueCodeSequence(String text)
        {
            int countFirst = text.Split(new string[]{"{%"}, 
                StringSplitOptions.None).Count() - 1;
            int countSecond = text.Split(new string[] { "%}" },
                StringSplitOptions.None).Count() - 1;
            return countFirst == countSecond;
        }

        private String ReplaceCustomWordByPositionOfMatch(String text, Match match)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Remove(match.Index, match.Length);
            sb.Insert(match.Index,"output.Write(\"" + match.Value + "\");");
            return sb.ToString();
        }

        public override string ParseTemplate(string templateText)
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

        protected override string ReplaceCodeSequence(string templateText)
        {
            Regex regEx = new Regex(codeSequencePattern);
            MatchCollection matches = regEx.Matches(templateText);
            foreach (Match match in matches)
                templateText = ReplaceCodeSequenceByPositionOfMatch(templateText,
                    match);
            return templateText;
        }

        private string ReplaceCodeSequenceByPositionOfMatch(string text, Match match)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Remove(match.Index + match.Length - 2,2);
            sb.Remove(match.Index, 2);
            return sb.ToString();
        }
    }
}
