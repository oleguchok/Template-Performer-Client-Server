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
            var result = parser.ParseTemplate("ff");
            Assert.AreEqual("output.Write(\"ff\");", result);
        }

        [TestMethod]
        public void Can_Compile_CSharp_Code()
        {
            using(var compiler = new CSharpCodeCompiler())
            using(var output = new StringWriter())
            {
                compiler.Compile("output.Write(\"hi\");", output, new String[0], new Variable[0]);
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
        [ExpectedException(typeof(TemplateFormatException),
            "Template must not contain code sequence\"{%\" or \"%}\"")]
        public void Incorrect_CSharp_Template_Code()
        {
            using(var template = new Template(new CSharp(), "{%}", new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException),
            "Template must not contain code sequence\"{%\" or \"%}\"")]
        public void Check_That_Code_Doesnt_Contain_False_Code_Sequence()
        {
            using(var template = new Template(new CSharp(),"%{%%}%}", new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        public void Can_Identify_Code_Sequence()
        {
            using(var template = new Template(new CSharp(),"{%output.Write(\"From sequence\");%}",
                new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("From sequence", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Parse_Code_Sequence_Correctly()
        {
            var parser = new CSharpTemplateParser();
            String result = parser.ParseTemplate("{%s%}");
            Assert.AreEqual("s", result);
        }

        [TestMethod]
        public void Can_Handle_Empty_String()
        {
            using(var template = new Template(new CSharp(),"", new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Execute_Code_Sequence_And_Custom_Text()
        {
            using (var template = new Template(new CSharp(), 
                "{% fo%}{%r(int i = 0; i < 5; i++){ %}*{%}%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("*****", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Parse_Two_Custom_Texts()
        {
            using(var template = new Template(new CSharp(),"hi{%%}test",new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("hitest", output.ToString());
            }
        }

        [TestMethod]
        public void Add_Int_Parameter_In_CSharp()
        {
            using(var template = new Template(new CSharp(), "{%while(count > 0){%}x{%count--;}%}", 
                new String[0], new Variable[]{ new Variable("count", ArgumentType.Integer)}))
            using(var output = new StringWriter())
            {
                template.Render(output, 2);
                Assert.AreEqual("xx", output.ToString());
            }
        }

        [TestMethod]
        public void Add_Bool_Long_String_Params_In_CSharp()
        {
            using(var template = new Template(new CSharp(),
                @"{%if(action)%}*{%while(count > 0){%}x{%count--;}output.Write(str);%}",
                new String[0], new Variable[]
                {
                    new Variable("action", ArgumentType.Boolean),
                    new Variable("count", ArgumentType.Long),
                    new Variable("str", ArgumentType.String)
                }))
            using(var output = new StringWriter())
            {
                template.Render(output, true, 2, "test");
                Assert.AreEqual("*xxtest", output.ToString());
            }
        }

        [TestMethod]
        public void Add_DateTime_Double_In_CSharp()
        {
            using(var template = new Template(new CSharp(),
                @"{%output.Write(n*2);output.Write(time);%}",
                new String[0], new Variable[]
                {
                    new Variable("n", ArgumentType.Double),
                    new Variable("time", ArgumentType.DateTime)
                }))
            using(var output = new StringWriter())
            {
                template.Render(output, 2.1, DateTime.Now.Date);
                Assert.AreEqual("4,2" + DateTime.Now.Date.ToString(), output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void Add_Incorrect_Input_Data_CSharp()
        {
            using(var template = new Template(new CSharp(), @"{%while(i > 0){%}*{%i--;}%}",
                new String[0], new Variable[]{new Variable("i", ArgumentType.String)}))
            using(var output = new StringWriter())
            {
                template.Render(output, "test");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void Add_Incorrect_Type_Of_Input_Data_CSharp()
        {
            using (var template = new Template(new CSharp(), @"{%while(i > 0){%}*{%i--;}%}",
                new String[0], new Variable[] { new Variable("i", ArgumentType.Integer) }))
            using (var output = new StringWriter())
            {
                template.Render(output, "test");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateRuntimeException))]
        public void Check_Setting_Of_Parameters_If_Params_Doesnt_Set()
        {
            using(var template = new Template(new CSharp(), "", new String[0],
                new Variable[] {new Variable("n", ArgumentType.Integer)}))
            using(var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateRuntimeException))]
        public void Check_Setting_Of_Parameters_If_Variables_Doesnt_Set()
        {
            using (var template = new Template(new CSharp(), "", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output, 1);
            }
        }

        [TestMethod]
        public void Can_Identify_Nested_Loop_Sequence()
        {
            using (var template = new Template(new CSharp(),
                "{%@n%}<{%@ n + m %}*{%@%}>{%@%}",
                new String[0],
                new Variable("n", ArgumentType.Integer),
                new Variable("m", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, 2, 1);
                Assert.AreEqual("<***><***>", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identity_Loop_Sequence()
        {
            using (var template = new Template(new CSharp(),
                "{%@n%}<>{%@%}",
                new String[0],
                new Variable("n", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, 2);
                Assert.AreEqual("<><>", output.ToString());
            }
        }

        [TestMethod]
        public void TestServer()
        {

        }
    }
}
