using AdvancedScada.DriverBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.IODriverV2.Comm
{
  public   interface  IDriverAdapterV2: IDriverAdapter
    {
        void AllSerialPortAdapter(SerialPortAdapter AllSerialPortAdapter);


        void AllEthernetAdapter(EthernetAdapter AllEthernetAdapter);
    }
}
