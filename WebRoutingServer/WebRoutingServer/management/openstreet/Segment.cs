using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    [DataContract]

    public class Segment
    {
        [DataMember]

        public double distance { get; set; }
        [DataMember]

        public double duration { get; set; }
        [DataMember]

        public Step[] steps { get; set; }
        public Segment()
        {

        }

        public override string ToString()
        {
            string res = "";
            foreach (Step step in steps)
            {
                res = res + step.name + "\n";
            }
            return res;
        }
    }
}
