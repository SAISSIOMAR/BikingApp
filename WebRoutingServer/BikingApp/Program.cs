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

            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            string[] fruits = { "apple", "passionfruit", "banana", "mango",
                      "orange", "blueberry", "grape", "strawberry" };


            ServiceItinerary si = new ServiceItinerary();

            List<Step> s1 = si.getItinerary("111B Av. des Martyrs de la Résistance, 76100 Rouen", "1 Rue Albert Dupuis, 76044 Rouen", true);

            List<Step> s2 = new List<Step>();
            //s2 = si.getItinerary(string position,string destination, true);
            String message1 = "";
            foreach (Step s in s1)
            {
                message1 = message1 + "\n" + s.instruction.ToString();
            }
            Console.WriteLine(message1);

            Console.ReadLine();


            // ActiveMq.getInstance().send(message1);












        }
    }
}
