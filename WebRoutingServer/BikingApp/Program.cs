using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WebRoutingServer;

namespace BikingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            //Be careful to run Visual Studio as Admistrator or to allow VS to open new port netsh command. 
            // Example : netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
            Uri httpUrl = new Uri("http://localhost:8090/BikingAppServer/ServiceItinerary");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(ServiceItinerary), httpUrl);

            // Multiple end points can be added to the Service using AddServiceEndpoint() method.
            // Host.Open() will run the service, so that it can be used by any client.

            // Example adding :
            // Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/SimpleCalculator");
            // ServiceHost host = new ServiceHost(typeof(MyCalculatorService.SimpleCalculator), httpUrl, tcpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IServiceItinerary), new WSHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            foreach (JCDContract item in JcDecaux.getContracts())
            {
                Console.WriteLine(item.name);
            }
            JCDStation st = JcDecaux.GetInstance().getClosestStation(OpenStreet.GetInstance().getOSMFeatureFromStrAddress("1865 Chem. de la Nerthe, 13016 Marseille").First(),
                JcDecaux.getStationsOfContract(JcDecaux.getContracts()[23]),true);
            JCDStation st2 = JcDecaux.GetInstance().getClosestStation(OpenStreet.GetInstance().getOSMFeatureFromStrAddress("317 Bd du Redon, 13009 Marseille").First(),
                JcDecaux.getStationsOfContract(JcDecaux.getContracts()[23]),false);
            
            Feature feat = OpenStreet.GetInstance().getOSMFeatureFromStrAddress("Pl. de la Cathédrale, 76000 Rouen").First();
            Feature feat1 = OpenStreet.GetInstance().getOSMFeatureFromStrAddress("317 Bd du Redon, 13009 Marseille").First();
            
           
        
            Console.WriteLine(JcDecaux.GetInstance().GetContratForPosition(feat).name); ;

        }
    }
}
