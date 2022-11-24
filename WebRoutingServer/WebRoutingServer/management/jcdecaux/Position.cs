using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.jcdecaux
{
    [DataContract]
    public class Position
    {
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
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
