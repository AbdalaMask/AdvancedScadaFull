using AdvancedScada.DriverBase.Devices;
using HslCommunication;
using System.Data;

namespace AdvancedScada.DriverBase
{
    public interface IDriverAdapter
    {
        bool IsConnected { get; set; }

        bool IsAvailable { get; }

        void Connection();

        void Disconnection();

        ConnectionState GetConnectionState();

        byte[] BuildReadByte(byte station, string address, ushort length);

        byte[] BuildWriteByte(byte station, string address, byte[] value);

        TValue[] Read<TValue>(string address, ushort length);

        TValue[] Read<TValue>(DataBlock db);

        OperateResult<bool[]> ReadDiscrete(string address, ushort length);

        bool Write(string address, dynamic value);

    }
}
