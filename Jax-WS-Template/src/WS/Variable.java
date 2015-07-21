package WS;


import javax.xml.bind.annotation.*;

/**
 * Created by Oleg on 21.07.15.
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Variable")
public class Variable {

    @XmlElement(name = "name")
    private String name;

    @XmlElement(name="type")
    private String type;

    @XmlElement(name="value")
    private Object value;

    public String getName(){
        return name;
    }

    public void setName(String value){
        name = value;
    }

    public String getType(){
        return type;
    }

    public void setType(String value){
        type = value;
    }

    public Object getValue(){
        return value;
    }

    public void setValue(Object value){
        this.value = value;
    }
}
