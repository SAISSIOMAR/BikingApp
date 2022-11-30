using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebRoutingServer;

namespace BikingApp
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ServiceItinerary : IServiceItinerary
    {
        public List<Step> getItinerary(string source, string Destination)
        {
            // get contract by name pour source et destination
            // get closest station by contract
            // get itinerary
            // return itinerary
            return null;
        }
    }
}
