using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateLibrary
{
    public sealed class ArgumentType
    {
        public static readonly ArgumentType String = new ArgumentType();
        public static readonly ArgumentType Integer = new ArgumentType();
        public static readonly ArgumentType Long = new ArgumentType();
        public static readonly ArgumentType Boolean = new ArgumentType();
        public static readonly ArgumentType Double = new ArgumentType();
        public static readonly ArgumentType DateTime = new ArgumentType();

        public ArgumentType() { }

        public ArgumentType(String value)
        {
            Value = value;
        }

        public String Value { get; set; }

        public static void SetCSharpTypes()
        {
            ArgumentType.String.Value = "System.String";
            ArgumentType.Integer.Value = "System.Int32";
            ArgumentType.Long.Value = "System.Int64";
            ArgumentType.Boolean.Value = "System.Boolean";
            ArgumentType.Double.Value = "System.Double";
            ArgumentType.DateTime.Value = "System.DateTime";
        }

        public static void SetJavaTypes()
        {
            ArgumentType.String.Value = "java.lang.String";
            ArgumentType.Integer.Value = "java.lang.Integer";
            ArgumentType.Long.Value = "java.lang.Long";
            ArgumentType.Boolean.Value = "java.lang.Boolean";
            ArgumentType.Double.Value = "java.lang.Double";
            ArgumentType.DateTime.Value = "java.util.Calendar";
        }
    }

}
