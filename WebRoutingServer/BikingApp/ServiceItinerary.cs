using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebRoutingServer;
using BikingApp.ServiceProxyCache;

namespace BikingApp
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ServiceItinerary : IServiceItinerary
    {
        public List<Step> getItinerary(string source, string destination,bool start)
        {
            List<Feature> featuresOrigin = OpenStreet.GetInstance().getOSMFeatureFromStrAddress(source);
            List<Feature> featuresDestination = OpenStreet.GetInstance().getOSMFeatureFromStrAddress(destination);
            if (start || (featuresOrigin.Count == 1 && featuresDestination.Count == 1)){
                Feature featSource = featuresOrigin.First();
                Feature featDestination = featuresDestination.First();
                JCDContract contract = JcDecaux.GetInstance().GetContratForPosition(source);//1
                JCDContract contract2 = JcDecaux.GetInstance().GetContratForPosition(destination);//2
                //for test just commente 1 and 2 and replace contract with getcontract(0) li hiya rouen because for now we don't have getcontract by position
                JCDStation st = JcDecaux.GetInstance().getClosestStation(featSource, JcDecaux.getStationsOfContract(contract), start);
                JCDStation st2 = JcDecaux.GetInstance().getClosestStation(featDestination, JcDecaux.getStationsOfContract(contract), !start);
                ActiveMq.getInstance().send(OpenStreet.GetInstance().getPath(st, st2, featSource, featDestination));
                return OpenStreet.GetInstance().getPath(st, st2, featSource, featDestination);
            }
            return null;
        }
    }
}
