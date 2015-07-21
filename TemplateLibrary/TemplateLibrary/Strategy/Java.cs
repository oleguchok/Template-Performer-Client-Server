using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateLibrary.Parsers;

namespace TemplateLibrary.Strategy
{
    public class Java : IStrategy
    {
        private TemplateParser parser;
        
        public Java()
        {
            parser = new JavaTemplateParser();
            ArgumentType.SetJavaTypes();
        }

        public void CompileCode(string templateCode, TextWriter output,
            string[] namespaces, params Variable[] parametres)
        {
            templateCode = parser.ParseTemplate(templateCode);
        }
    }
}
