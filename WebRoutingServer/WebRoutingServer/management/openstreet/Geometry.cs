using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    [DataContract]
    public class Geometry
    {
        [DataMember]

        public List<List<double>> coordinates { get; set; }
        [DataMember]

        public string type { get; set; }

        public Geometry() { }
    }
}
