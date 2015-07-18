using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary.Strategy
{
    public interface IStrategy
    {
        void RenderCode(TextWriter output, params object[] parametres);

        void ParseTemplate(String templateText);

    }
}
