using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TemplateLibrary.Exceptions;

namespace TemplateLibrary.Compilers
{
    public class CSharpCodeCompiler : IDisposable
    {
        private CSharpCodeProvider provider;
        private CompilerParameters compilerParameters;    

        public CSharpCodeCompiler()
        {
            provider = new CSharpCodeProvider();
            compilerParameters = new CompilerParameters();
        }

        public void Compile(String code, TextWriter output, String[] namespaces, 
            params Variable[] parameters)
        {
            DefineCompilerParameters(compilerParameters);
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters,
                InsertCodeInMethodDeclaration(code, namespaces, parameters));
            CheckErrorsInCompile(results);
            MethodInfo method = GetMethodOfCompileProgram(results);
            method.Invoke(null, FormInputParameters(output, parameters));
        }

        private String InsertCodeInMethodDeclaration(String code, String[] namespaces,
            params Variable[] parameters)
        {            
            return FormNamespacesUsing(namespaces) + @"using System;namespace First{" + 
                "class Program{public static void Main(System.IO.TextWriter output" + 
                GetInputParameters(parameters) + "){" + code + "}}}";
        }

        private String GetInputParameters(Variable[] parameters)
        {
            var result = "";
            foreach (Variable param in parameters)
                result += "," + param.ArgumentType.Value + " " + param.Name;
            return result;
        }

        private object[] FormInputParameters(TextWriter output, Variable[] parameters)
        {
            object[] input = new object[parameters.Length + 1];
            input[0] = output;
            int i = 1;
            foreach (Variable param in parameters)
                input[i++] = param.Value;
            return input;
        }

        private void DefineCompilerParameters(CompilerParameters parameters)
        {
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
        }

        private String FormNamespacesUsing(String[] namespaces)
        {
            var result = "";
            foreach (String assembly in namespaces)
                result += "using " + assembly + ";";
            return result;
        }
    
        private void CheckErrorsInCompile(CompilerResults results)
        {
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorText, error.Column));
                }
                
                throw new TemplateFormatException(sb.ToString());
            }
        }

        private MethodInfo GetMethodOfCompileProgram(CompilerResults results)
        {
            Assembly assembly = results.CompiledAssembly;
            Type program = assembly.GetType("First.Program");
            return program.GetMethod("Main");
        }

        public void Dispose()
        {
            provider.Dispose();
        }
    }
}
