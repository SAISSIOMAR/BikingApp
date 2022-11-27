using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRoutingServer.management.jcdecaux
{
    public class Station
    {
        public int number { get; set; }
        public String contractName { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public Position position { get; set; }
        public Boolean banking { get; set; }
        public Boolean bonus { get; set; }

        public String status { get; set; }
        public Boolean connected { get; set; }
        public Boolean overflow { get; set; }
        public String shape { get; set; }
        public Stand overflowStands { get; set; }
        public Stand totalStands { get; set; }
        public Stand mainStands { get; set; }


        public Station(int number, string contractName, string name, string address, Position position, bool banking, bool bonus)
        {
            this.number = number;
            this.contractName = contractName;
            this.name = name;
            this.address = address;
            this.position = position;
            this.banking = banking;
            this.bonus = bonus;
        }

        public Station(int number, string contractName, string name, string address, Position position, bool banking, bool bonus, string status, bool connected, bool overflow, string shape, Stand overflowStands, Stand totalStands, Stand mainStands) : this(number, contractName, name, address, position, banking, bonus)
        {
            this.status = status;
            this.connected = connected;
            this.overflow = overflow;
            this.shape = shape;
            this.overflowStands = overflowStands;
            this.totalStands = totalStands;
            this.mainStands = mainStands;
        }

        public Station()
        {

        }

        public override string ToString()
        {
            return "name : " + this.name + "\n" +
                "contract_name : " + this.contractName + "\n" +
                "number : " + this.number + "\n" +
                "position : " + this.position + "\n" +
                "banking : " + this.banking + "\n" +
                "bonus : " + this.bonus + "\n" +
                "address : " + this.address + "\n" +
            "status : " + this.status + "\n" +
               "connected : " + this.connected + "\n" +
               "overflow : " + this.overflow + "\n" +
               "shape : " + this.shape + "\n" +
               "overflowStands : " + this.overflowStands + "\n" +
               "totalStands : " + this.totalStands + "\n" +
              "mainStands : " + this.mainStands + "\n";


        }


    }
}