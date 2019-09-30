using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;
using System.ServiceModel;

namespace AdvancedScada.IBaseService
{
    [ServiceContract]
    public interface IServiceCallback
    {

        [OperationContract(IsOneWay = true)]
        void DataTags(Dictionary<string, Tag> Tags);

        [OperationContract(IsOneWay = true)]
        void UpdateCollection(ConnectionState status, Dictionary<string, Tag> collection);
    }
}