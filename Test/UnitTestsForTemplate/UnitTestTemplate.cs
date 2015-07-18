using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateLibrary;
using TemplateLibrary.Strategy;
using System.IO;
using TemplateLibrary.Parsers;

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

            Assert.AreEqual("System.IO.TextWriter(\"ff\");", result);
        }

        [TestMethod]
        public void TestServer()
        {

        }
    }
}
