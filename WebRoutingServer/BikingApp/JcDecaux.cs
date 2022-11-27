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
using WebRoutingServer.management.openstreet;


namespace WebRoutingServer
{
    public  class JcDecaux
    {
        private static string  apiUrl = "https://api.jcdecaux.com/vls/v3/";
        private static string apikey = "894af26c2e703588ce385d3676ed52b166f9e904";
        private static string query = "apiKey=894af26c2e703588ce385d3676ed52b166f9e904"  ;
        private static string response = JCDecauxAPICall(apiUrl,query).Result;
        List<JCDContract> allContracts = JsonSerializer.Deserialize<List<JCDContract>>(response);



        
        
        private List<JCDStation> getStationsOfContract(JCDContract contract)
        {
            string response = JCDecauxAPICall("stations", "contract=" + contract.name).Result;
            return JsonSerializer.Deserialize<List<JCDStation>>(response);
        }
        private JCDStation getClosestStation(Feature feature, List<JCDStation> stations)
        {
            JCDStation closestStation = null;
            double closestDistanceFromStation = 0;
            foreach (JCDStation station in stations)
            {
                double distanceCalculated = Distance(feature.geometry.coordinates, station.position.ToDoubleArray());
                if (closestStation == null || distanceCalculated <= closestDistanceFromStation)
                {
                    closestStation = station;
                    closestDistanceFromStation = distanceCalculated;
                }
            }
            return closestStation;
        }
        private double Distance(List<double> coordinates, double[] position)
        {
            double x = coordinates[0] - position[0];
            double y = coordinates[1] - position[1];
            return Math.Sqrt(x * x + y * y);
        }
        public List<JCDStation> getClosestStations(Feature feature)
        {
            List<JCDStation> closestStations = new List<JCDStation>();
            foreach (JCDContract contract in allContracts)
            {
                List<JCDStation> stations = getStationsOfContract(contract);
                closestStations.Add(getClosestStation(feature, stations));
            }
            return closestStations;
        }

        static async Task<string> JCDecauxAPICall(string url, string query)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "?" + query); // attendre qu'une requete asyncrone soi finis 
            response.EnsureSuccessStatusCode(); //throws exception 
            return await response.Content.ReadAsStringAsync(); //return les villes
        }
        

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
    }

    public class Position
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
     
        public double[] ToDoubleArray()
        {
            double[] res = new double[2];
            res[0] = latitude;
            res[1] = longitude;
            return res;
        }
    }
}
