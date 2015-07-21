package WS;

import compiler.CodeCompiler;

import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.xml.ws.Endpoint;

@WebService()
public class TemplateWS {

    private CodeCompiler compiler;

    public TemplateWS()
    {
        compiler = new CodeCompiler();
    }

    @WebMethod
    public String getResultOfCompile(String templateCode, String[] packages)
    {
        return templateCode;
    }

    public static void main(String[] argv) {
        Object implementor = new TemplateWS ();
        String address = "http://localhost:9000/TemplateWS";
        Endpoint.publish(address, implementor);
    }

}
