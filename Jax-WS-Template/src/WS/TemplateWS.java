package WS;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;

@WebService
@SOAPBinding(style = SOAPBinding.Style.RPC)
public interface TemplateWS {

    @WebMethod
    public String getResultOfCompile(@WebParam(name = "code") String templateCode,
                                     @WebParam(name = "packages")String[] packages,
                                     @WebParam(name = "variables")Variable...args);
}
