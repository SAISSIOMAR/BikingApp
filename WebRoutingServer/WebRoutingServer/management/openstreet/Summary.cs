using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    [DataContract]

    public class Summary
    {
        [DataMember]

        public double distance { get; set; }
        [DataMember]

        public double duration { get; set; }

        public Summary()
        {

        }

    }
}
