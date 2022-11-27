using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
   
    public class Itinerary
    {
       
        public Feature[] features { get; set; }

        public double[] bbox { get; set; }
        public Itinerary()
        {

        }

        public override string ToString()
        {
            string res = "";
            foreach (Feature step in features)
            {
                res = res + step + "\n";
            }
            return res;
        }
    }
}
