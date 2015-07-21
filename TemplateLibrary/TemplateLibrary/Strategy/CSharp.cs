using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateLibrary.Compilers;
using TemplateLibrary.Parsers;

namespace TemplateLibrary.Strategy
{
    public class CSharp : IStrategy, IDisposable
    {
        private TemplateParser parser;
        private CSharpCodeCompiler compiler;

        public CSharp()
        {
            parser = new CSharpTemplateParser();
            compiler = new CSharpCodeCompiler();
            ArgumentType.SetCSharpTypes();
        }

        public void CompileCode(string templateCode, TextWriter output,
            String[] namespaces, params Variable[] parameters)
        {
            templateCode = parser.ParseTemplate(templateCode);
            compiler.Compile(templateCode, output, namespaces, parameters);
        }

        public void Dispose()
        {
            compiler.Dispose();
        }
    }
}
