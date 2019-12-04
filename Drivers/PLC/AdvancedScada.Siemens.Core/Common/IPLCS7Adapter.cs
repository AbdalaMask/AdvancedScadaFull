using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Linq;

namespace AdvancedScada.Siemens.Core.Common
{
    public interface IPLCS7Adapter : IDriverAdapter
    {
        void ReadStruct(DataBlock structType, ushort length);

        new bool Write(string address, dynamic value);
    }
}
