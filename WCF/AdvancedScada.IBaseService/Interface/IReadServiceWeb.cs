using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdvancedScada.IBaseService
{
    [ServiceContract()]
    public interface IReadServiceWeb
    {
        [FaultContract(typeof(IFaultException))]
        [WebGet(UriTemplate = "GetCollectionTag", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Dictionary<string, Tag> GetCollection();

        [FaultContract(typeof(IFaultException))]
        [WebInvoke(Method = "POST", UriTemplate = "WritePLC?TagName={TagName}&ValueTag={ValueTag}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [OperationContract()]
        int WriteTag(string TagName, dynamic ValueTag);
 
    }
}