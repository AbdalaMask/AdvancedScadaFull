using AdvancedScada.DriverBase;
using System;
using System.Linq;

namespace AdvancedScada.IODriverV2.Comm
{
    public interface IDriverAdapterV2 : IDriverAdapter
    {
        void AllSerialPortAdapter(SerialPortAdapter AllSerialPortAdapter);


        void AllEthernetAdapter(EthernetAdapter AllEthernetAdapter);
    }
}
