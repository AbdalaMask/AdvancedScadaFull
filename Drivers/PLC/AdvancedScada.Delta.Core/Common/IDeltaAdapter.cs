using AdvancedScada.Common;

namespace AdvancedScada.Delta.Common
{
    public interface IDeltaAdapter : IDriverAdapter
    {
        new bool Write(string address, dynamic value);
        bool[] ReadDiscrete(string address, ushort length);
    }
}