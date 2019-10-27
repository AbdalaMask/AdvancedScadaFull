using AdvancedScada.Common;
using System.ServiceModel;

namespace AdvancedScada.IBaseService
{

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceCallback))]
    public interface IReadService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(Machine mac);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Machine mac);

        [OperationContract(IsOneWay = true)]
        void WriteTag(string tagName, dynamic value);
    }
}