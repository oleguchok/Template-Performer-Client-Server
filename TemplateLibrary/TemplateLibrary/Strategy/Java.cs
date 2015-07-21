using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TemplateLibrary.Strategy
{
    public class Java : IStrategy
    {
        private TemplateParser parser;
        
        public Java()
        {
            ArgumentType.SetJavaTypes();
        }

        public void CompileCode(string templateCode, TextWriter output,
            string[] namespaces, params Variable[] parametres)
        {
            throw new NotImplementedException();
        }
    }
}
