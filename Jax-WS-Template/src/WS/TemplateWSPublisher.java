package WS;

import javax.xml.ws.Endpoint;

public class TemplateWSPublisher {
    public static void main(String[] argv) {
        Object implementor = new TemplateWSImpl();
        String address = "http://localhost:9000/TemplateWSImpl";
        Endpoint.publish(address, implementor);
    }
}
