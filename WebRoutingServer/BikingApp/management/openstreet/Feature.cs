using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    
    public class Feature
    {
        
        public Properties properties { get; set; }

     

        public Geometry geometry { get; set; }
       

        public Feature()
        {
        }

        public override string ToString()
        {
            return properties.ToString();
        }
    }
}
