using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.jcdecaux
{
    public class Stand
    {

        public Availabilities availabilities { get; set; }
        public int capacity { get ; set; }

        public Stand()
        {

        }

        public Stand(Availabilities availabilities, int capacity)
        {
            this.availabilities = availabilities;
            this.capacity = capacity;
        }

        public override string ToString()
        {
            return "availabilities : " + this.availabilities + "\n" +
                "capacity : " + this.capacity;
        }
    }
}
