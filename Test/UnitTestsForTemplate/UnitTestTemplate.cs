using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateLibrary;
using TemplateLibrary.Strategy;
using System.IO;
using TemplateLibrary.Parsers;
using TemplateLibrary.Compilers;
using TemplateLibrary.Exceptions;
using System.Linq;
using System.Text;

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
                template.Render(output, 2, 2);
                Assert.AreEqual("<****><****>", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Loop_Sequence()
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
        [ExpectedException(typeof(TemplateFormatException))]
        public void Can_Identify_False_Loop_Sequence()
        {
            using (var template = new Template(new CSharp(),
                "{%@n%}<{%@ds%}>{%@%}",
                new String[0],
                new Variable("n", ArgumentType.Integer),
                new Variable("ds",ArgumentType.String)))
            using (var output = new StringWriter())
            {
                template.Render(output, 2, 3);
            }
        }

        [TestMethod]
        public void Can_Identify_Loop_And_Code_Sequences()
        {
            using (var template = new Template(new CSharp(),
                "hi{%if(testBool){%}{%@n%}<>{%@%}{%}%}",
                new String[0],
                new Variable("testBool", ArgumentType.Boolean),
                new Variable("n", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, true, 1);
                Assert.AreEqual("hi<>", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateRuntimeException))]
        public void If_Empty_Loop_Sequence()
        {
            using (var template = new Template(new CSharp(),
                "{%@%}{%@%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output, true);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void If_Loop_Sequence_Has_Input_Params_Without_Declaration()
        {
            using (var template = new Template(new CSharp(),
                "{%@%}sd{%@%}",
                new String[0],
                new Variable("n", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, 1);
            }
        }

        [TestMethod]
        public void Can_Identify_Output_For_Variables()
        {
            using (var template = new Template(new CSharp(),
                "{%=i%} and {%= str %} or {%= time%}",
                new String[0],
                new Variable("i", ArgumentType.Integer),
                new Variable("str", ArgumentType.String),
                new Variable("time", ArgumentType.DateTime)))
            using (var output = new StringWriter())
            {
                template.Render(output, 10, "TEST", DateTime.Now.Date);
                Assert.AreEqual("10 and TEST or " + DateTime.Now.Date, output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Two_Loop_Sequence()
        {
            using (var template = new Template(new CSharp(),
                "{%@i%}x{%@%} or {%@j%}y{%@%}",
                new String[0],
                new Variable("i", ArgumentType.Integer),
                new Variable("j", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, 1, 2);
                Assert.AreEqual("x or yy", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void If_Empty_Output_For_Variables()
        {
            using (var template = new Template(new CSharp(),
                "{%=%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void If_Empty_With_Spaces_Output_For_Variables()
        {
            using (var template = new Template(new CSharp(),
                "{%=             %}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        public void Can_Identify_Boolean_Sequence()
        {
            using (var template = new Template(new CSharp(),
                @"{%?s.Equals(""TEST"")%}TRUE{%@3%}!{%@%}{%?%}",
                new String[0],
                new Variable("s", ArgumentType.String)))
            using (var output = new StringWriter())
            {
                template.Render(output, "TEST");
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Two_Boolean_Sequence()
        {
            using (var template = new Template(new CSharp(),
                @"{%?s.Equals(""TEST"")%}TRUE{%?%}{%?test%}!{%?%}",
                new String[0],
                new Variable("test", ArgumentType.Boolean),
                new Variable("s", ArgumentType.String)))
            using (var output = new StringWriter())
            {
                template.Render(output, true, "TEST");
                Assert.AreEqual("TRUE!", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Nested_Boolean_Sequences()
        {
            using (var template = new Template(new CSharp(),
                @"{%?s.Equals(""TEST"")%}TRUE{%?test%}!{%?%}{%?%}",
                new String[0],
                new Variable("test", ArgumentType.Boolean),
                new Variable("s", ArgumentType.String)))
            using (var output = new StringWriter())
            {
                template.Render(output, true, "TEST");
                Assert.AreEqual("TRUE!", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Code_Sequence_Java()
        {
            using (var template = new Template(new Java(),
                "{%output.write(String.valueOf(\"From sequence\"));%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("From sequence", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TemplateFormatException))]
        public void If_Empty_Boolean_Sequence()
        {
            using (var template = new Template(new CSharp(),
                @"{%?%}{%?%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        public void Can_Parse_Java_Code()
        {
            var parser = new JavaTemplateParser();
            using(var output = new StringWriter())
            {
                var res = parser.ParseTemplate(@"hi{%?s.Equals(""test"")%}{%@2%}*{%@%}{%?%}");
                Assert.AreEqual("output.write(String.valueOf(\"hi\"));if(s.Equals(\"test\"))" + 
                    "{for(int ii0=0;ii0<2;ii0++){output.write(String.valueOf(\"*\"));}}", res);
            }
        }

        [TestMethod]
        public void TestServer_With_Passing_Arguments()
        {
            using(var connector = new JaxWSConnector())
            using(var output = new StringWriter())
            {
                ArgumentType.SetJavaTypes();
                Variable variable = new Variable("s", ArgumentType.String);
                variable.Value = "test";
                connector.CompileJavaCode("output.write(s);", 
                    new String[0], output, variable);
                Assert.AreEqual("test", output.ToString());
            }
        }

        [TestMethod]
        public void Server_Can_Compile_Sequences()
        {
            using (var template = new Template(new Java(),
                "{% fo%}{%r(int i = 0; i < 5; i++){ %}*{%}%}", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("*****", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Identify_Bool_Sequence_Java()
        {
            using (var template = new Template(new Java(),
                @"{%?s.equals(""TEST"")%}TRUE{%@3%}!{%@%}{%?%}", 
                new String[0], new Variable("s", ArgumentType.String)))
            using (var output = new StringWriter())
            {
                template.Render(output, "TEST");
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void If_Pass_False_Argument()
        {
            using (var template = new Template(new Java(), 
                @"{%?%}{%?%}", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output, "TEST");
            }
        }

        //[TestMethod]
        //public void Can_Pass_DateTime_Java()
        //{
        //    using (var template = new Template(new Java(),
        //        @"{%=date%}", new String[0],
        //        new Variable("date", ArgumentType.DateTime)))
        //    using (var output = new StringWriter())
        //    {
        //        template.Render(output, DateTime.Now.Date);
        //        Assert.AreEqual(DateTime.Now.Date.ToString(), output.ToString());
        //    }
        //}

        [TestMethod]
        public void Can_Pass_Boolean_Java()
        {
            using (var template = new Template(new Java(),
                @"{%=b%}", new String[0],
                new Variable("b", ArgumentType.Boolean)))
            using (var output = new StringWriter())
            {
                template.Render(output, false);
                Assert.AreEqual("false", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Pass_Double_Java()
        {
            using (var template = new Template(new Java(),
                @"{%=d%}", new String[0],
                new Variable("d", ArgumentType.Double)))
            using (var output = new StringWriter())
            {
                template.Render(output, 1.2);
                Assert.AreEqual("1.2", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Add_Packages_To_Java()
        {
            using (var template = new Template(new Java(),
                @"{%double d = 0; d = Math.sin(d);%}{%=d%}", new String[] { "java.lang.Math" }))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("0.0", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Pass_Long_Java()
        {
            using (var template = new Template(new Java(),
                @"{%=l%}", new String[0],
                new Variable("l", ArgumentType.Long)))
            using (var output = new StringWriter())
            {
                template.Render(output, 301923091203213);
                Assert.AreEqual("301923091203213", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Handle_Simple_Words_In_Java()
        {
            using (var template = new Template(new Java(),
                @"testik",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("testik", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Handle_Complex_Expression()
        {
            using (var template = new Template(new Java(),
                "hi{%if(testBool){%}{%@n%}<>{%@%}{%}%}",
                new String[0],
                new Variable("testBool", ArgumentType.Boolean),
                new Variable("n", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output, true, 1);
                Assert.AreEqual("hi<>", output.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Can_Handle_Error_Java()
        {
            using (var template = new Template(new Java(),
                @"{%@%}{%@%}",
                new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
            }
        }

        [TestMethod]
        public void Can_Handle_Code_Sequence_Java()
        {
            using (var template = new Template(new Java(),
                @"{%@n%}1{%@m%}2{%@%}{%@%}",
                new String[0],
                new Variable("n", ArgumentType.Integer),
                new Variable("m", ArgumentType.Integer)))
            using (var output = new StringWriter())
            {
                template.Render(output,1,1);
                Assert.AreEqual("12", output.ToString());
            }
        }

        [TestMethod]
        public void Can_Handle_Code_Sequence_And_Expression_Java()
        {
            using (var template = new Template(new Java(), 
                @"{%for(int i = 0; i < 3; i++){%}{%=i%}{%}%}", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("012", output.ToString());
            }

        }

        [TestMethod]
        public void If_Pass_Empty_String()
        {
            using(Template template = new Template(new Java(),
                "", new String[0]))
            using(var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("", output.ToString());
            }
        }
    }
}
