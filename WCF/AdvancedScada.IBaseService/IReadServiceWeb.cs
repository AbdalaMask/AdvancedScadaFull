using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdvancedScada.IBaseService
{
    [ServiceContract]
    public interface IReadServiceWeb
    {

        [OperationContract]
        [FaultContract(typeof(IFaultException))]
        [WebInvoke(Method = "GET", UriTemplate = "GetCollection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Dictionary<string, Tag> GetCollection();


    }
}