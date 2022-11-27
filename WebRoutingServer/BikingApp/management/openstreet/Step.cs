using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.openstreet
{
    

    public class Step
    {
     

        public double distance { get; set; }
        

        public double duration { get; set; }
        

        public int type { get; set; }
      

        public string instruction { get; set; }
       

        public string name { get; set; }

        public Step() { }


    }
}
