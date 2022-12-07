package main;

import com.baeldung.soap.ws.client.generated.ArrayOfStep;
import com.baeldung.soap.ws.client.generated.IServiceItinerary;
import com.baeldung.soap.ws.client.generated.ServiceItinerary;
import org.apache.activemq.ActiveMQConnection;

import javax.jms.JMSException;
import java.util.Scanner;
import java.util.logging.Logger;

public class MessageReceiver {






    // URL of the JMS server
    private static String url = ActiveMQConnection.DEFAULT_BROKER_URL;
    // default broker URL is : tcp://localhost:61616"
    static Logger logger = Logger.getLogger(MessageReceiver.class.getName());
    // Name of the queue we will receive messages from
    private static String subject = "test";

    public static void main(String[] args) throws JMSException {
        Scanner sc = new Scanner(System.in);
        System.out.println("Please enter your current position");
        String source = sc.nextLine();

        System.out.println("Please enter your destination");
        String destination = sc.nextLine();


        IServiceItinerary lgbItinerary;
        ServiceItinerary service = new ServiceItinerary();
        lgbItinerary = service.getBasicHttpBindingIServiceItinerary();

        ArrayOfStep responseJsonStr = lgbItinerary.getItinerary(source,destination,true);

        //System.out.println(responseJsonStr.getStep().get(0).getInstruction().getValue());




        // call onmessage method

        ActiveMqReceiver.activeMq2();






    }


}