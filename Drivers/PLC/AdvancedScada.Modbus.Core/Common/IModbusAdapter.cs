using AdvancedScada.Common;

namespace AdvancedScada.Modbus.Common
{
    public interface IModbusAdapter : IDriverAdapter
    {
        new bool Write(string address, dynamic value);
        bool[] ReadDiscrete(string address, ushort length);
    }
}