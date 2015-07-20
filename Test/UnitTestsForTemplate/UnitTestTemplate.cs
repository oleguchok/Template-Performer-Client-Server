using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateLibrary;
using TemplateLibrary.Strategy;
using System.IO;
using TemplateLibrary.Parsers;
using TemplateLibrary.Compilers;
using TemplateLibrary.Exceptions;

namespace UnitTestsForTemplate
{
    [TestClass]
    public class UnitTestTemplate
    {
        [TestMethod]
        public void Can_Identify_Simple_String()
        {
            using (var template = new Template(new CSharp(), "aba", new String[0])) 
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("aba", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Replase_Custom_Words_In_CSharp_Code()
        {
            var parser = new CSharpTemplateParser();
            var result = parser.ReplaceCustomText("ff");

            Assert.AreEqual("output.Write(\"ff\");", result);
        }

        [TestMethod]
        public void Can_Compile_CSharp_Code()
        {
            using(var compiler = new CSharpCodeCompiler())
            using(var output = new StringWriter())
            {
                compiler.Compile("output.Write(\"hi\");", output, new String[0]);
                Assert.AreEqual("hi", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Add_CSharp_Assemblies_In_Code()
        {
            var code = "StringBuilder sb = new StringBuilder(\"test\");" +
                "sb.Remove(3,1);output.Write(sb.ToString());";
            using(var compiler = new CSharpCodeCompiler())
            using(var output = new StringWriter())
            {
                compiler.Compile(code, output, new String[1] { "System.Text" });
                Assert.AreEqual("tes", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException),
            "Assembly was not found.")]
        public void If_Add_Non_Existent_Assembly()
        {
            using(var compiler = new CSharpCodeCompiler())
            using(var output = new StringWriter())
            {
                compiler.Compile("output.Write(\"s\"", output, new String[] { "ololo" });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException),
            "Name doesn't exist in this context.")]
        public void If_Code_For_Compiler_Is_Incorrect()
        {
            using(var compiler = new CSharpCodeCompiler())
            using(var output = new StringWriter())
            {
                compiler.Compile("INCORRECT;", output, new String[0]);
            }
        }

        [TestMethod]
        public void Incorrect_CSharp_Template_Code()
        {
            using(var template = new Template(new CSharp(), "{%}", new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        public void TestServer()
        {

        }
    }
}
