using AdvancedScada.DriverBase.Devices;

namespace AdvancedScada.Common
{
    public interface IODriver
    {
        string Name { get; }
        void InitializeService(Channel ch);
        void Connect();
        void Disconnect();
        void WriteTag(string TagName, dynamic Value);
    }
}
