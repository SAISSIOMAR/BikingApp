import com.baeldung.soap.ws.client.generated.ArrayOfStep;
import com.baeldung.soap.ws.client.generated.IServiceItinerary;
import com.baeldung.soap.ws.client.generated.ServiceItinerary;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.List;

public class Service {

    static IServiceItinerary lgbItinerary;

    @BeforeClass
    public static void setup() {
        ServiceItinerary service = new ServiceItinerary();
        lgbItinerary = service.getBasicHttpBindingIServiceItinerary();
    }

    @Test
    public void givenCountryService_whenCountryIndia_thenCapitalIsNewDelhi() {


        ArrayOfStep responseJsonStr = lgbItinerary.getItinerary("114B Av. des Martyrs de la Résistance, 76100 Rouen","1 Rue Albert Dupuis, 76044 Rouen",true);

        System.out.println(responseJsonStr.getStep().get(0).getInstruction());
    }
}
