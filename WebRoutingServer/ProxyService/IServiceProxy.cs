using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ProxyService
{
    [ServiceContract]
    public interface IServiceProxy
    {
        [OperationContract]
        List<JCDContract> getContracts();
        
        [OperationContract]
        List<JCDStation> getStationsOfContract(JCDContract contract);
    }
}
