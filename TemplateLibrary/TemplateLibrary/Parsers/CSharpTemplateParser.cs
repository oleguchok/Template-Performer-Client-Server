using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TemplateLibrary.Parsers
{
    public class CSharpTemplateParser : TemplateParser
    {
        public override String ReplaceCustomText(String templateText)
        {
            Regex regEx = new Regex(customTextPattern);
            MatchCollection customTextMatches = regEx.Matches(templateText);
            foreach(Match match in customTextMatches)
            {
                templateText = ReplaceCustomWordByPositionOfMatch(templateText,
                    match);
            }
            return templateText;
        }

        private String ReplaceCustomWordByPositionOfMatch(String text, Match match)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Remove(match.Index, match.Length);
            sb.Insert(match.Index,"output.Write(\"" + match.Value + "\");");
            return sb.ToString();
        }
    }
}
