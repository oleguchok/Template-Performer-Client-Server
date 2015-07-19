using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateLibrary.Strategy;

namespace TemplateLibrary
{
    public class Template : IDisposable
    {
        private IStrategy _strategy;
        private String templateText;
        private String[] namespaces;

        public Template(IStrategy strategy, String templateText, String[] namespaces,
                        params Variable[] variables)
        {
            _strategy = strategy;
            this.templateText = templateText;
            this.namespaces = namespaces;
        }

        public IStrategy Strategy
        {
            set { _strategy = value; }
        }

        public void Render(TextWriter output, params object[] parametres)
        {
            _strategy.CompileCode(this.templateText, output, this.namespaces,
                parametres);
        }

        public void Dispose()
        {
           
        }

    }
}