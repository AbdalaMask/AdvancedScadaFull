using AdvancedScada.DriverBase.Devices;

namespace AdvancedScada.DriverBase
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
