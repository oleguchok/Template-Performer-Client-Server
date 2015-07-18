using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateLibrary.Parsers;

namespace TemplateLibrary.Strategy
{
    public class CSharp : IStrategy
    {
        private TemplateParser parser;

        public CSharp()
        {
            parser = new CSharpTemplateParser();
        }

        public void RenderCode(TextWriter output, params object[] parametres)
        {
            
        }

        public void ParseTemplate(string templateText)
        {
            templateText = parser.ReplaceCustomText(templateText);
        }


        string IStrategy.ParseTemplate(string templateText)
        {
            throw new NotImplementedException();
        }

        public void CompileCode(string templateCode)
        {
            throw new NotImplementedException();
        }
    }
}
