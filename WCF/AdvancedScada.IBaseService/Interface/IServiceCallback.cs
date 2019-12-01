using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;
using System.ServiceModel;

namespace AdvancedScada.IBaseService
{
    [ServiceContract]
    public interface IServiceCallback
    {

        [OperationContract(IsOneWay = true)]
        void UpdateCollection(ConnectionState status, Dictionary<string, Tag> collection);
        [OperationContract(IsOneWay = true)]
        void UpdateCollectionDataBlock(ConnectionState status, Dictionary<string, DataBlock> collection);
    }
}