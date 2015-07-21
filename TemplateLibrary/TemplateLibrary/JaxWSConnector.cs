using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary
{
    public class JaxWSConnector : IDisposable
    {
        private TemplateWSService.TemplateWSService service = null;

        public String GetResultOfCompile(String templateText, String[] packages,
            params Variable[] parameters)
        {
            String result = "";
            service = new TemplateWSService.TemplateWSService();
            result = service.getResultOfCompile(templateText, packages);
            return result;
        }

        public void Dispose()
        {
            service.Dispose();
        }
    }
}
