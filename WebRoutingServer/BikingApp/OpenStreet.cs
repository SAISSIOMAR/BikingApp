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
            List<Step> footPath = getDirectionFromOriginToDestinationFootWalking(startingFeature,destinationFeature);
            

            if ((startingStation == null || destinationStation == null)||(startingStation==destinationStation))
            {
                return footPath;
            }
            List<Step> bikingPath = getDirections(startingStation, destinationStation, startingFeature, destinationFeature);
            if (needFoot( bikingPath,footPath))
            {
                return footPath ;
            }

            return bikingPath ;

        }



        //get direction from origin to destination

        public double distance(List<Step> steps)
        {
            double distance = 0;
            foreach (Step step in steps)
            {
                distance += step.distance;
            }
            return distance;
        }

        public bool needFoot(List<Step> bikepath, List<Step> footpath)
        {
            return distance(bikepath) >= distance(footpath);

        }
        public List<Step> getDirectionFromStationToStation(JCDStation s1, JCDStation s2)
        {
           
            return getItinerariesFromApi(s1.position.ToDoubleArray(), s2.position.ToDoubleArray(), "foot-walking");
        }


        public List<Step> getDirections(JCDStation startingStation, JCDStation destinationStation, Feature startingFeature, Feature destinationFeature)
        {
            List<Step> res1 = getItinerariesFromApi(startingFeature.geometry.coordinates, startingStation.position.ToDoubleArray(), "foot-walking");
            List<Step> res2 = getItinerariesFromApi(startingStation.position.ToDoubleArray(), destinationStation.position.ToDoubleArray(), "cycling-regular");
            List<Step> res3 = getItinerariesFromApi(destinationStation.position.ToDoubleArray(),destinationFeature.geometry.coordinates, "foot-walking");
            res1.AddRange(res2);
            res1.AddRange(res3);
            return res1;
            
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
    // methode that converts list<Step> to Itinerary
    }
    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }



}
