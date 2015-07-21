package compiler;

import WS.Variable;

import javax.tools.*;
import java.io.*;
import java.lang.reflect.Method;
import java.net.MalformedURLException;
import java.net.URI;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.Arrays;
import java.util.Locale;

public class CodeCompiler
{
    /** where shall the compiled class be saved to (should exist already) */
    private static String classOutputFolder = "d:/";

    public static class MyDiagnosticListener implements DiagnosticListener<JavaFileObject>
    {
        public void report(Diagnostic<? extends JavaFileObject> diagnostic)
        {

            System.out.println("Line Number->" + diagnostic.getLineNumber());
            System.out.println("code->" + diagnostic.getCode());
            System.out.println("Message->"
                    + diagnostic.getMessage(Locale.ENGLISH));
            System.out.println("Source->" + diagnostic.getSource());
            System.out.println(" ");
        }
    }

    /** java File Object represents an in-memory java source file <br>
     * so there is no need to put the source file on hard disk  **/
    public static class InMemoryJavaFileObject extends SimpleJavaFileObject
    {
        private String contents = null;

        public InMemoryJavaFileObject(String className, String contents) throws Exception
        {
            super(URI.create("string:///" + className.replace('.', '/')
                    + Kind.SOURCE.extension), Kind.SOURCE);
            this.contents = contents;
        }

        public CharSequence getCharContent(boolean ignoreEncodingErrors)
                throws IOException
        {
            return contents;
        }
    }

    /** Get a simple Java File Object ,<br>
     * It is just for demo, content of the source code is dynamic in real use case */
    private static JavaFileObject getJavaFileObject(String templateCode, String[] packages,
                                                    Variable...variables)
    {
        StringBuilder contents = new StringBuilder(
                "package math;" + getPackages(packages) +
                        "public class Code {"
                        + "public static void main(java.io.PrintWriter output"
                        + getVariables(variables) + "){"
                        + templateCode + "}}"
        );
        JavaFileObject so = null;
        try
        {
            so = new InMemoryJavaFileObject("math.Code", contents.toString());
        }
        catch (Exception exception)
        {
            exception.printStackTrace();
        }
        return so;
    }

    /** compile your files by JavaCompiler */
    public static void compile(Iterable<? extends JavaFileObject> files)
    {
        //get system compiler:
        JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();

        // for compilation diagnostic message processing on compilation WARNING/ERROR
        MyDiagnosticListener c = new MyDiagnosticListener();
        StandardJavaFileManager fileManager = compiler.getStandardFileManager(c,
                Locale.ENGLISH,
                null);
        //specify classes output folder
        Iterable options = Arrays.asList("-d", classOutputFolder);
        JavaCompiler.CompilationTask task = compiler.getTask(null, fileManager,
                c, options, null,
                files);
        Boolean result = task.call();
        if (result == true)
        {
            System.out.println("Succeeded");
        }
    }

    /** run class from the compiled byte code file by URLClassloader */
    public static String runIt()
    {
        // Create a File object on the root of the directory
        // containing the class file
        File file = new File(classOutputFolder);

        try
        {
            // Convert File to a URL
            URL url = file.toURI().toURL(); // file:/classes/demo
            URL[] urls = new URL[] { url };

            // Create a new class loader with the directory
            ClassLoader loader = new URLClassLoader(urls);

            // Load in the class; Class.childclass should be located in
            // the directory file:/class/demo/
            Class thisClass = loader.loadClass("math.Code");
            printWriter = new PrintWriter(outputFile);
            Class params[] = new Class[parameters.length+1];
            params[0] = PrintWriter.class;
            for(int i = 1; i < params.length; i++)
                params[i] = parameters[i-1].getValue().getClass();
            Object paramsObj[] = new Object[parameters.length + 1];
            paramsObj[0] = printWriter;
            for(int i = 1; i < params.length; i++)
                paramsObj[i] = parameters[i-1].getValue();
            Object instance = thisClass.newInstance();
            Method thisMethod = thisClass.getDeclaredMethod("main", params);

            thisMethod.invoke(instance, paramsObj);
        }
        catch (MalformedURLException e)
        {
        }
        catch (ClassNotFoundException e)
        {
        }
        catch (Exception ex)
        {
            ex.printStackTrace();
        }
        finally {
            return "l";
        }
    }

    private static String code;
    private static String[] packages;
    private static Variable[] parameters;
    private static final File outputFile = new File("d:/classes/text.txt");
    private static PrintWriter printWriter;

    public CodeCompiler()
    {
        outputFile.getParentFile().mkdirs();

    }

    public void loadParameters(String code, String[] packages, Variable...params)
    {
        this.code = code;
        this.packages = packages;
        if (params == null)
            this.parameters = new Variable[0];
        else
            this.parameters = params;
    }

    private static String getPackages(String[] packages)
    {
        String result = "";
        for(String pack : packages)
            result += "import " + pack + ";\n";
        return  result;
    }

    private static String getVariables(Variable[] variables) {
        String result = "";
        for(Variable variable : variables)
            result += "," + variable.getType() + " " + variable.getName();
        return  result;
    }

    public String getResultOfComiling(String code, String[] packages,
                                             Variable...variables)
    {
        loadParameters(code, packages, variables);

        //1.Construct an in-memory java source file from your dynamic code
        JavaFileObject file = getJavaFileObject(code, packages, parameters);
        Iterable<? extends JavaFileObject> files = Arrays.asList(file);

        //2.Compile your files by JavaCompiler
        compile(files);

        //3.Load your class by URLClassLoader, then instantiate the instance, and call method by reflection
        return runIt();
    }

    public static void main(String[] args) {

        CodeCompiler cc = new CodeCompiler();
        String result = cc.getResultOfComiling("output.write(\"s\");System.out.println(\"!\");output.close();",
                new String[0]);
        System.out.println(result);
    }
}
