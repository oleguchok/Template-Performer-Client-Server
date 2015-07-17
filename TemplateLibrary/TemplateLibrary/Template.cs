using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateLibrary.Strategy;

namespace TemplateLibrary
{   
    public class Template
    {
        private IStrategy _strategy;

        public Template(IStrategy strategy, String templateText, String[] namespaces,
                        params Variable[] variables)
        {
            _strategy = strategy;
        }

        public IStrategy Strategy
        {
            set { _strategy = value; }
        }
    }
}
