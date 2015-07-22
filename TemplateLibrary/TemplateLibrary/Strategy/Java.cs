using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TemplateLibrary.Parsers;

namespace TemplateLibrary.Strategy
{
    public class Java : IStrategy, IDisposable
    {
        private TemplateParser parser;
        private JaxWSConnector connector;
        
        public Java()
        {
            parser = new JavaTemplateParser();
            connector = new JaxWSConnector();
            ArgumentType.SetJavaTypes();
        }

        public void CompileCode(string templateCode, TextWriter output,
            string[] namespaces, params Variable[] parameters)
        {
            templateCode = parser.ParseTemplate(templateCode);
            connector.CompileJavaCode(templateCode, namespaces, output, parameters);
        }

        public void Dispose()
        {
            connector.Dispose();
        }
    }
}
