using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.ServiceModel.Configuration;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics.Contracts;
using BikingApp.ServiceProxyCache;
using static BikingApp.ServiceProxyCache.Position;
using System.ServiceModel;

namespace WebRoutingServer
{
    public class JcDecaux
    {
       
        private static JcDecaux instance;
      
        static ServiceProxyClient proxy = new ServiceProxyClient();

        public JcDecaux()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = 99999900;
            basicHttpBinding.MaxBufferSize = 99999900;
        }

        public static JcDecaux GetInstance()
        {
            if (instance == null)
            {
                instance = new JcDecaux();
            }
            return instance;
        }

        public static List<JCDContract> getContracts()
        {

            return proxy.getContracts().ToList();
        }


        public static List<JCDStation> getStationsOfContract(JCDContract contract)
        {
            return proxy.getStationsOfContract(contract).ToList();
        }
        public JCDStation getClosestStation(Feature feature, List<JCDStation> stations, bool start)
        {
            JCDStation closestStation = null;
            double closestDistanceFromStation = 0;
            foreach (JCDStation station in stations)
            {

                double distanceCalculated = Distance(feature.geometry.coordinates, ToDoubleArray(station.position));
                int standsOrBikesAvailability = start ? station.totalStands.availabilities.bikes : station.totalStands.availabilities.stands;
                if (closestStation == null || (distanceCalculated <= closestDistanceFromStation && standsOrBikesAvailability > 0))
                {
                    closestStation = station;
                    closestDistanceFromStation = distanceCalculated;
                }
            }
            return closestStation;
        }


        public double[] ToDoubleArray(Position pos)
        {
            double[] res = new double[2];
            
                res[1] = pos.latitude;
                res[0] = pos.longitude;
            
       
            return res;
        }

        private double Distance(double[] coordinates, double[] position)
        {
            double x = coordinates[0] - position[0];
            double y = coordinates[1] - position[1];
            return Math.Sqrt(x * x + y * y);
        }
        // get contract by name and get closest station by contract

        //get contract by name 
        public JCDContract getContractByName(string name)
        {
            foreach (JCDContract contract in getContracts())
            {
                if (contract.name == name)
                {
                    return contract;
                }
            }
            return null;
        }



        public JCDContract GetContratForPosition(string adress)
        {
            List<JCDContract> contracts = getContracts();
            foreach (JCDContract c in contracts)
            {
                if (OpenStreet.GetInstance().getOSMFeatureFromStrAddress(adress).First().properties.locality.ToLower() == c.name)
                {
                    return c;
                }
                
            }
            return null;
        }
    }
}
  
    

    

