using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WebRoutingServer;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using BikingApp.ServiceProxyCache;


namespace BikingApp
{
    class Program
    {
        

        
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            //Be careful to run Visual Studio as Admistrator or to allow VS to open new port netsh command. 
            // Example : netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
            Uri httpUrl = new Uri("http://localhost:8090/IServiceItinerary/ServiceItinerary");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(ServiceItinerary), httpUrl);

            // Multiple end points can be added to the Service using AddServiceEndpoint() method.
            // Host.Open() will run the service, so that it can be used by any client.

            // Example adding :
            // Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/SimpleCalculator");
            // ServiceHost host = new ServiceHost(typeof(MyCalculatorService.SimpleCalculator), httpUrl, tcpUrl);

            //Add a service endpoint
            //host.AddServiceEndpoint(typeof(IServiceItinerary), new WSHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            ServiceItinerary si = new ServiceItinerary();
            List<Step> s1 = si.getItinerary("40 Rue de Lillebonne, 76000 Rouen", "Halle Saint-Exupéry, 24 Bd Gambetta, 76000 Rouen", true);
            foreach (Step s in s1)
            {
                Console.WriteLine(s.instruction);
            }




            //s2 = si.getItinerary(string position,string destination, true);

            Console.ReadLine();


            // ActiveMq.getInstance().send(message1);












        }
    }
}
