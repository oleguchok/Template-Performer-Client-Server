package WS;

import compiler.CodeCompiler;

import javax.jws.WebService;

@WebService()
public class TemplateWSImpl implements TemplateWS {

    private CodeCompiler compiler;

    public TemplateWSImpl()
    {
        compiler = new CodeCompiler();
    }

    @Override
    public String getResultOfCompile(String templateCode, String[] packages, Variable...args)
    {
        String result = "";
        try {
            result = compiler.getResultOfComiling(templateCode, packages, args);
        }
        catch (Exception e){

        }
        System.out.println(result);
        return result;
    }
}
