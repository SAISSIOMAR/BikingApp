using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ProxyService
{
    internal class JCDecauxItem
    {
        private const string CacheItem = "contracts";
        private static string apiUrl = "https://api.jcdecaux.com/vls/v3/";
        private static string apikey = "894af26c2e703588ce385d3676ed52b166f9e904";
        private static JCDecauxItem instance;
        private static string query = "apiKey=894af26c2e703588ce385d3676ed52b166f9e904";
        private static string response = JCDecauxAPICall("contracts").Result;
        

        public static async Task<string> JCDecauxAPICall(string url = "", string query = "")
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl + url + "?apiKey=894af26c2e703588ce385d3676ed52b166f9e904" + "&" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static JCDecauxItem GetInstance()
        {
            if (instance == null)
            {
                instance = new JCDecauxItem();
            }
            return instance;
        }

        public  List<JCDContract> getContracts()
        {
            GenericProxyCache<List<JCDContract>> proxyCache = new GenericProxyCache<List<JCDContract>>();
            List<JCDContract> contracts = proxyCache.Get(CacheItem);
            if (contracts == null)
            {
                string response = JCDecauxAPICall("contracts").Result;
                contracts = JsonSerializer.Deserialize<List<JCDContract>>(response);
                proxyCache.Set("contracts", contracts,180);
            }
            return contracts;
        }


        public  List<JCDStation> getStationsOfContract(JCDContract contract)
        {
            GenericProxyCache<List<JCDStation>> proxyCache = new GenericProxyCache<List<JCDStation>>();
            List<JCDStation> stations = proxyCache.Get("stations");
            if (stations == null)
            {
                string respons = JCDecauxAPICall("stations", "contract=" + contract.name).Result;
                stations = JsonSerializer.Deserialize<List<JCDStation>>(respons);
                proxyCache.Set("stations", stations,180);
            }

            return stations;
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