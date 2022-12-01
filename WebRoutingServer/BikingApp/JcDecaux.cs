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

namespace WebRoutingServer
{
    public  class JcDecaux
    {
        private static string  apiUrl = "https://api.jcdecaux.com/vls/v3/";
        private static string apikey = "894af26c2e703588ce385d3676ed52b166f9e904";
        private static JcDecaux instance;
        private static string query = "apiKey=894af26c2e703588ce385d3676ed52b166f9e904"  ;
        private static string response = JCDecauxAPICall("contracts").Result;
        private static List<JCDContract> contracts = JsonSerializer.Deserialize<List<JCDContract>>(response);

        public static async Task<string> JCDecauxAPICall(string url = "", string query = "")
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl + url + "?apiKey=894af26c2e703588ce385d3676ed52b166f9e904" + "&" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
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
            return contracts;
        }


        //getallstations
        public static List<JCDStation> getAllStations()
        {
            List<JCDStation> stations = new List<JCDStation>();
            foreach (JCDContract contract in contracts)
            {
                string response = JCDecauxAPICall("stations", "contract=" + contract.name).Result;
                List<JCDStation> contractStations = JsonSerializer.Deserialize<List<JCDStation>>(response);
                stations.AddRange(contractStations);
            }
            return stations;
        }

        
        public static  List<JCDStation> getStationsOfContract(JCDContract contract)
        {
            string response = JCDecauxAPICall("stations", "contract=" + contract.name).Result;
            return JsonSerializer.Deserialize<List<JCDStation>>(response);
        }
        public JCDStation getClosestStation(Feature feature, List<JCDStation> stations,bool start)
        {
            JCDStation closestStation = null;
            double closestDistanceFromStation = 0;
            foreach (JCDStation station in stations)
            {   
                
                double distanceCalculated = Distance(feature.geometry.coordinates, station.position.ToDoubleArray());
                int standsOrBikesAvailability = start ? station.totalStands.availabilities.bikes : station.totalStands.availabilities.stands;
                if (closestStation == null || (distanceCalculated <= closestDistanceFromStation && standsOrBikesAvailability > 0))
                {
                    closestStation = station;
                    closestDistanceFromStation = distanceCalculated;
                }
            }
            return closestStation;
        }

        

       





        private double Distance(double[] coordinates, double[] position)
        {
            double x = coordinates[0] - position[0];
            double y = coordinates[1]- position[1];
            return Math.Sqrt(x * x + y * y);
        }
        // get contract by name and get closest station by contract

        //get contract by name 
        public JCDContract getContractByName(string name)
        {
            foreach (JCDContract contract in contracts)
            {
                if (contract.name == name)
                {
                    return contract;
                }
            }
            return null;
        }

        public  JCDContract GetContratForPosition(Feature feature)
        {
            JCDContract contract = null;
            List<JCDContract> contracts = getContracts();
            foreach (JCDContract c in contracts)
            {
                List<JCDStation> stations = getStationsOfContract(c);
                JCDStation station = getClosestStation(feature, stations, true);
                if (station.position.latitude == feature.geometry.coordinates[0] && station.position.longitude == feature.geometry.coordinates[1]
)
                {
                    contract = c;
                    break;
                }
            }
            return contract;
        }
        //get closest station by contract

        //Find the JC Decaux contract associated with the given origin/destination













    }
    public class JCDContract
    {
        public string name { get; set; }
    }

    public class JCDStation
    {
        public int number { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public Stands totalStands { get; set; }
        public string contractName { get; set; }
    }

    public class Position
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
     
        public double[] ToDoubleArray()
        {
            double[] res = new double[2];
            res[1] = latitude;
            res[0] = longitude;
            return res;
        }
    }
    public class Stands
    {
        public Availability availabilities { get; set; }
        public int capacity { get; set; }
    }

    public class Availability
    {
        public int bikes { get; set; }
        public int stands { get; set; }
    }
}
