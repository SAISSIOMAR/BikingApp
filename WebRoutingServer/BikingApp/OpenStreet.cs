using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebRoutingServer.management.openstreet;

namespace WebRoutingServer
{
    internal class OpenStreet
    {
        private static string apiUrl = "https://api.openrouteservice.org/";
        private static string query = "apiKey=5b3ce3597851110001cf6248b1f72132e01b4c559d8134ad67624834";
        
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
            HttpResponseMessage response = await client.GetAsync(url + "?" + query );
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
