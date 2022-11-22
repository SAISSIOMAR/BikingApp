using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    [DataContract]
    public class Feature
    {
        [DataMember]
        public Properties properties { get; set; }

        [DataMember]

        public Geometry geometry { get; set; }
        [DataMember]

        public double[] bbox { get; set; }

        public Feature()
        {
        }

        public override string ToString()
        {
            return properties.ToString();
        }
    }
}
