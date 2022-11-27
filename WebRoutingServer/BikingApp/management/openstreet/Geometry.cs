using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    
    public class Geometry
    {
     

        public List<double> coordinates { get; set; }
      

        public string type { get; set; }

        public Geometry() { }
    }
}
