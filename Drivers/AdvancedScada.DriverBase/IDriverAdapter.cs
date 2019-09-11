using AdvancedScada.DriverBase.Devices;
using System.Data;

namespace AdvancedScada.DriverBase
{
    public interface IDriverAdapter
    {
        bool IsConnected { get; set; }
        bool Connection();
        bool Disconnection();
        TValue[] Read<TValue>(string address, ushort length);
        bool Write(string address, dynamic value);

    }
}
