using AdvancedScada.Common;

namespace AdvancedScada.LSIS.Common
{
    public interface ILSISAdapter : IDriverAdapter
    {
        new bool Write(string address, dynamic value);
        bool[] ReadDiscrete(string address, ushort length);
    }
}