using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateLibrary.Compilers;
using TemplateLibrary.Parsers;

namespace TemplateLibrary.Strategy
{
    public class CSharp : IStrategy
    {
        private TemplateParser parser;
        private CSharpCodeCompiler compiler;

        public CSharp()
        {
            parser = new CSharpTemplateParser();
            compiler = new CSharpCodeCompiler();
            ArgumentType.SetCSharpTypes();
        }

        private string ParseTemplate(string templateText)
        {
            templateText = parser.ParseTemplate(templateText);
            return templateText;
        }

        public void CompileCode(string templateCode, TextWriter output,
            String[] namespaces, params Variable[] parameters)
        {
            templateCode = ParseTemplate(templateCode);
            compiler.Compile(templateCode, output, namespaces, parameters);
        }
    }
}
