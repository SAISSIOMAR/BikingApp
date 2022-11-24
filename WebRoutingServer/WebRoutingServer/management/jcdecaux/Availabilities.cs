using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.jcdecaux
{
    public class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }
        public int bikmechanicalBikeses { get; set; }
        public int electricalBikes { get; set; }
        public int electricalInternalBatteryBikes { get; set; }
        public int electricalRemovableBatteryBikes { get; set; }

        public Availabilities()
        {

        }

        public Availabilities(int bikes, int stands, int bikmechanicalBikeses, int electricalBikes, int electricalInternalBatteryBikes, int electricalRemovableBatteryBikes)
        {
            this.bikes = bikes;
            this.stands = stands;
            this.bikmechanicalBikeses = bikmechanicalBikeses;
            this.electricalBikes = electricalBikes;
            this.electricalInternalBatteryBikes = electricalInternalBatteryBikes;
            this.electricalRemovableBatteryBikes = electricalRemovableBatteryBikes;
        }

        public override string ToString()
        {
            return "bikes : " + this.bikes + "\n" +
                "stands : " + this.stands + "\n" +
                "bikmechanicalBikeses : " + this.bikmechanicalBikeses + "\n" +
                "electricalBikes : " + this.electricalBikes + "\n" +
                "electricalInternalBatteryBikes : " + this.electricalInternalBatteryBikes + "\n" +
                "electricalRemovableBatteryBikes : " + this.electricalRemovableBatteryBikes + "\n";
        }
    }
}
