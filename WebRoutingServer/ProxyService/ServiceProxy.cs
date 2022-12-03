using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyService
{
    public class ServiceProxy : IServiceProxy
    {
        public List<JCDContract> getContracts()
        {
            return JCDecauxItem.GetInstance().getContracts();
        }

        public List<JCDStation> getStationsOfContract(JCDContract contract)
        {
            return JCDecauxItem.GetInstance().getStationsOfContract(contract);
        }
    }
    
}
