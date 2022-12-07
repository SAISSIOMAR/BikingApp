using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BikingApp.ServiceProxyCache;
using static BikingApp.ServiceProxyCache.Position;

namespace WebRoutingServer
{
    internal class OpenStreet
    {
        private static OpenStreet instance;
        private static string apiUrl = "https://api.openrouteservice.org/";

        public static OpenStreet GetInstance()
        {
            if (instance == null)
            {
                instance = new OpenStreet();
            }
            return instance;
        }
        //getfeaturefromstring
        
        public List<Feature> getOSMFeatureFromStrAddress(string address)
        {
            string query = "text=" + address;
            string url = "geocode/search";
            string response = OSMAPICall(url, query).Result;
            JsonElement jsonFeatures = JsonDocument.Parse(response).RootElement.GetProperty("features");
            List<Feature> listFeatures = JsonSerializer.Deserialize<List<Feature>>(jsonFeatures);
            return listFeatures;
        }
        //

        public static async Task<string> OSMAPICall(string url = "", string query = "")
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl + url + "?api_key=5b3ce3597851110001cf6248b1f72132e01b4c559d8134ad67624834" + "&" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public List<Step> getPath(JCDStation startingStation , JCDStation destinationStation, Feature startingFeature, Feature destinationFeature)
        {
            List<Step> step;
            List<Step> footPath = getDirectionFromOriginToDestinationFootWalking(startingFeature,destinationFeature);
            List<Step> bikingPath = getDirections(startingStation, destinationStation, startingFeature, destinationFeature);

            
          
            return bikingPath ;

        }
        public double[] ToDoubleArray(Position pos)
        {
            double[] res = new double[2];
            if (pos != null)
            {
                res[1] = pos.latitude;
                res[0] = pos.longitude;
            }

            return res;
        }



        //get direction from origin to destination

        public double distance(List<Step> steps)
        {
            double distance = 0;
            foreach (Step step in steps)
            {
                distance += step.duration;
            }
            return distance;
        }

        public bool needFoot(List<Step> bikepath, List<Step> footpath)
        {
            return distance(bikepath) >= distance(footpath);

        }
        public List<Step> getDirectionFromStationToStation(JCDStation s1, JCDStation s2)
        {
           
            return getItinerariesFromApi(ToDoubleArray(s1.position), ToDoubleArray(s2.position), "foot-walking");
        }


        public List<Step> getDirections(JCDStation startingStation, JCDStation destinationStation, Feature startingFeature, Feature destinationFeature)
        {
            Console.WriteLine("------------------------------------------------------------");
            List<Step> res1 = getItinerariesFromApi(startingFeature.geometry.coordinates, ToDoubleArray(startingStation.position), "foot-walking");
            List<Step> res2 = getItinerariesFromApi(ToDoubleArray(startingStation.position), ToDoubleArray(destinationStation.position), "foot-walking");
            List<Step> res3 = getItinerariesFromApi(ToDoubleArray(destinationStation.position),destinationFeature.geometry.coordinates, "foot-walking");
            List<Step> st = new List<Step>();
            st.Add(new Step("walk"));
            for (int i = 0; i < res1.Count; i++)
            {
               
                
                st.Add(res1[i]);
            }
            for (int i = 0; i < res2.Count; i++)
            {
                st.Add(new Step("*****************************************************"+res2.Count+"*********************************************"));
                st.Add(res2[i]);
                st.Add(new Step("*****************************************************" + res2.Count + "*********************************************"));
            }
            st.Add(new Step("walk"));
            for (int i = 0; i < res3.Count; i++)
            {
                
                st.Add(res3[i]);
            }
            //res1.AddRange(res2);
            //es1.AddRange(res3);
            
            return st;
            
        }
        
        public List<Step> getDirectionFromOriginToDestinationFootWalking(Feature startingFeature, Feature destinationFeature)
        {
            
            Double[] startingCoordinates = startingFeature.geometry.coordinates;
            Double[] destinationCoordinates = destinationFeature.geometry.coordinates;
           
            return getItinerariesFromApi(startingCoordinates, destinationCoordinates, "foot-walking");
        }

       public List<Step> getItinerariesFromApi(Double[] startingCoordinates, Double[] destinationCoordinates, string profile)
        {
            string starting = startingCoordinates[0].ToString().Replace(',', '.') + "," + startingCoordinates[1].ToString().Replace(',', '.');
            string destination = destinationCoordinates[0].ToString().Replace(',', '.') + "," + destinationCoordinates[1].ToString().Replace(',', '.');
            string query = "start=" + starting + "&end=" + destination;
            string url = "v2/directions/" + profile;
            string response = OSMAPICall(url, query).Result;

            JsonElement segments = JsonDocument.Parse(response).RootElement.GetProperty("features")[0].GetProperty("properties").GetProperty("segments")[0].GetProperty("steps");
            List<Step> listSegments = JsonSerializer.Deserialize<List<Step>>(segments);
            return listSegments;
        }
        

    }

public class Feature
    {
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }

    }

    public class Geometry
    {
        public string type { get; set; }
        public Double[] coordinates { get; set; }
    }

    public class Properties
    {
        public string label { get; set; }
        public string locality { get; set; }
    }

    public class Itinerary
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    
    
}

public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }

        public Step()
        {
            distance = 0;
            duration = 0;
            instruction = "";
            name = "";
        }
        public Step(string ins){
            instruction = ins;
        }
        // methode that converts list<Step> to Itinerary
    }
    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }



}
