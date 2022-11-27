using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace BikingApp
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ServiceItinerary : IServiceItinerary
    {
        public String GetItinerary(string source, string Destination)
        {
            throw new NotImplementedException();
        }
    }
}
