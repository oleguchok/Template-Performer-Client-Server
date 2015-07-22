using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateLibrary.Exceptions;

namespace TemplateLibrary
{
    public class JaxWSConnector : IDisposable
    {
        private TemplateWSService.TemplateWSImplService service = null;

        public void CompileJavaCode(String templateText, String[] packages,
            TextWriter output, params Variable[] parameters)
        {
            try
            {
                String result = "";
                service = new TemplateWSService.TemplateWSImplService();
                TemplateWSService.Variable[] variables = getVaribleFormatForPassing(parameters);
                result = service.getResultOfCompile(templateText, packages, variables);
                if (result == "error")
                    throw new TemplateRuntimeException("It is incorrect code");
                output.Write(result);
            }
            catch(Exception e)
            {
                throw new TemplateRuntimeException("exception", e);
            }
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
