using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateLibrary.Exceptions;
using TemplateLibrary.Strategy;

namespace TemplateLibrary
{
    public class Template : IDisposable
    {
        private IStrategy _strategy;
        private String templateText;
        private String[] namespaces;
        private Variable[] variables;

        public Template(IStrategy strategy, String templateText, String[] namespaces,
                        params Variable[] variables)
        {
            _strategy = strategy;
            this.templateText = templateText;
            this.namespaces = namespaces;
            this.variables = variables;
        }

        public IStrategy Strategy
        {
            set { _strategy = value; }
        }

        public void Render(TextWriter output, params object[] parametres)
        {
            SetValuesOfParameters(parametres);
            _strategy.CompileCode(this.templateText, output, this.namespaces,
                this.variables);
                
        }

        private void SetValuesOfParameters(object[] param)
        {
            if ((param.Count() == 0 && variables.Count() != 0) ||
                (variables.Count() == 0 && param.Count() != 0))
                throw new TemplateRuntimeException("There are no input parameters",
                    new ArgumentException());
            if (variables.Count() == param.Count())
                for (int i = 0; i < param.Count(); i++)
                    variables[i].Value = param[i];
        }

        public void Dispose()
        {
           
        }

    }
}