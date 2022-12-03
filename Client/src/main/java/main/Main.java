package main;

import com.baeldung.soap.ws.client.generated.ArrayOfStep;
import com.baeldung.soap.ws.client.generated.IServiceItinerary;
import com.baeldung.soap.ws.client.generated.ServiceItinerary;
;

public class Main {
    public static void main(String[] args) {
       IServiceItinerary lgbItinerary;
       ServiceItinerary service = new ServiceItinerary();
       lgbItinerary = service.getBasicHttpBindingIServiceItinerary();
       ArrayOfStep responseJsonStr = lgbItinerary.getItinerary("114B Av. des Martyrs de la Résistance, 76100 Rouen","1 Rue Albert Dupuis, 76044 Rouen",true);
       System.out.println(responseJsonStr.getStep().get(0).getInstruction().getValue());


    }
}
