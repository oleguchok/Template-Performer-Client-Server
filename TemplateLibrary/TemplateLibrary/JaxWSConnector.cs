using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary
{
    public class JaxWSConnector : IDisposable
    {
        private TemplateWSService.TemplateWSImplService service = null;

        public String GetResultOfCompile(String templateText, String[] packages,
            params Variable[] parameters)
        {
            String result = "";
            service = new TemplateWSService.TemplateWSImplService();
            TemplateWSService.Variable[] variables = getVaribleFormatForPassing(parameters);            
            result = service.getResultOfCompile(templateText, packages, variables);
            return result;
        }

        private TemplateWSService.Variable[] getVaribleFormatForPassing(Variable[] parameters)
        {
            TemplateWSService.Variable[] javaVariables = new TemplateWSService.Variable[
                parameters.Length];
            TemplateWSService.Variable variable;
            for(int i = 0; i < parameters.Length; i++)
            {
                variable = new TemplateWSService.Variable();
                variable.name = parameters[i].Name;
                variable.type = parameters[i].ArgumentType.Value;
                variable.value = parameters[i].Value;
                javaVariables[i] = variable;
            }
            return javaVariables;
        }

        public void Dispose()
        {
            service.Dispose();
        }
    }
}
