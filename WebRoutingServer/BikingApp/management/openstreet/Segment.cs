using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    

    public class Segment
    {
       

        public double distance { get; set; }
     

        public double duration { get; set; }
       

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
