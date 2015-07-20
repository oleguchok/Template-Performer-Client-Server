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
        private CompilerParameters parameters;    

        public CSharpCodeCompiler()
        {
            provider = new CSharpCodeProvider();
            parameters = new CompilerParameters();
        }

        public void Compile(String code, TextWriter output, String[] namespaces)
        {
            DefineCompilerParameters(parameters);
            CompilerResults results = provider.CompileAssemblyFromSource(parameters,
                InsertCodeInMethodDeclaration(code, namespaces));
            CheckErrorsInCompile(results);
            MethodInfo method = GetMethodOfCompileProgram(results);
            method.Invoke(null, new object[]{ output });
        }

        private String InsertCodeInMethodDeclaration(String code, String[] namespaces)
        {
            return FormNamespacesUsing(namespaces) + @"using System;namespace First{class Program{public static " +
                "void Main(System.IO.TextWriter output){" + code + "}}}";
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
