using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    [DataContract]
    public class Properties
    {
        [DataMember]
        public Segment[] segments { get; set; }
        public Properties() { }


    }
}
