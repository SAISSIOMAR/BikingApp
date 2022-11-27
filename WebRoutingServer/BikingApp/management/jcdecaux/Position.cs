using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.jcdecaux
{
    
    public class Position
    {
        
        public double latitude { get; set; }
       
        public double longitude { get; set; }

        public Position()
        {

        }

        public Position(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return "latitude : " + this.latitude + "\n" +
                "longitude : " + this.longitude;
        }

        int x = 0;
    }
}
