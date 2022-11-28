using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebRoutingServer
{
    internal class OpenStreet
    {
        private static string apiUrl = "https://api.openrouteservice.org/";
        private static string apiKey = "apiKey=5b3ce3597851110001cf6248b1f72132e01b4c559d8134ad67624834";
        
        public List<Feature> getOSMFeatureFromStrAddress(string address)
        {
            string query = "text=" + address;
            string url = "geocode/search";
            string response = OSMAPICall(url, query).Result;
            JsonElement jsonFeatures = JsonDocument.Parse(response).RootElement.GetProperty("features");
            List<Feature> listFeatures = JsonSerializer.Deserialize<List<Feature>>(jsonFeatures);
            return listFeatures;
        }

        
        public static async Task<string> OSMAPICall(string url = "", string query = "")
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl + url + "?api_key=" + apiKey + "&" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public List<List<Itinerary>> getPath(JCDStation startingStation , JCDStation destinationStation, Feature startingFeature, Feature destinationFeature)
        {


            List<List<Itinerary>> itineraries = new List<List<Itinerary>>();
            List<Itinerary> footPath = getDirectionFromOriginToDestinationFootWalking(startingFeature,destinationFeature); ;

            if (startingStation == null || destinationStation == null)
            {
                itineraries.Add(footPath);
                return itineraries;
            }

            List<Itinerary> itinerariesFromOriginToStation = getDirectionFromOriginToStation(startingFeature, destinationStation);
            List<Itinerary> itinerariesFromStationToStation = getDirectionFromStationToStation(startingStation, destinationStation);
            List<Itinerary> itinerariesFromStationToDestination = getDirectionFromStationToDestination(destinationStation, destinationFeature);


            if (needFoot(itinerariesFromOriginToStation, itinerariesFromStationToDestination, footPath))
            {
                itineraries.Add(footPath);
            }
            else
            {
                itineraries.Add(itinerariesFromOriginToStation);
                itineraries.Add(itinerariesFromOriginToStation);
                itineraries.Add(itinerariesFromOriginToStation);
            }
            return itineraries;

        }

        public bool needFoot(List<Itinerary> startingToStation, List<Itinerary> stationToDest, List<Itinerary> startingToDest)
        {
            double bikePath = startingToStation[0].distance + stationToDest[0].distance;
            double walkingPath = startingToDest[0].distance;
            return bikePath >= walkingPath;

        }
        public List<Itinerary> getDirectionFromStationToStation(JCDStation starting, JCDStation destination)
        {
            Double[] startingCoordinates = starting.position.ToDoubleArray();
            Double[] destinationCoordinates = destination.position.ToDoubleArray();
            return getItinerariesFromApi(startingCoordinates, destinationCoordinates, "cycling-regular");
        }

        public List<Itinerary> getDirectionFromStationToDestination(JCDStation closestStationsDestination, Feature featureDestination)
        {
            Double[] destinationStationCoordinates = closestStationsDestination.position.ToDoubleArray();
            Double[] destinationCoordinates = featureDestination.geometry.coordinates;
            return getItinerariesFromApi(destinationStationCoordinates, destinationCoordinates, "foot-walking");
        }

        public List<Itinerary> getDirectionFromOriginToStation(Feature featureOrigin, JCDStation closestStationsOrigin)
        {
            Double[] startingCoordinates = featureOrigin.geometry.coordinates;
            Double[] startingStationCoordinates = closestStationsOrigin.position.ToDoubleArray();
            return getItinerariesFromApi(startingCoordinates, startingStationCoordinates, "foot-walking");
        }
        public List<Itinerary> getDirectionFromOriginToDestinationFootWalking(Feature startingFeature, Feature destinationFeature)
        {
            Double[] startingCoordinates = startingFeature.geometry.coordinates;
            Double[] destinationCoordinates = destinationFeature.geometry.coordinates;
            return getItinerariesFromApi(startingCoordinates, destinationCoordinates, "foot-walking");
        }


        public List<Itinerary> getItinerariesFromApi(Double[] startingCoordinates, Double[] destinationCoordinates, string profile)
        {
            string starting = startingCoordinates[0].ToString().Replace(',', '.') + "," + startingCoordinates[1].ToString().Replace(',', '.');
            string destination = destinationCoordinates[0].ToString().Replace(',', '.') + "," + destinationCoordinates[1].ToString().Replace(',', '.');
            string query = "start=" + starting + "&end=" + destination;
            string url = "v2/directions/" + profile;
            string response = OSMAPICall(url, query).Result;
            JsonElement segments = JsonDocument.Parse(response).RootElement.GetProperty("features")[0].GetProperty("properties").GetProperty("segments");
            List<Itinerary> listSegments = JsonSerializer.Deserialize<List<Itinerary>>(segments);
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
    }



}
