using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Siemens.Core.Common
{
  public  interface IPLCS7Adapter : IDriverAdapter
    {
        object ReadStruct(DataBlock structType, ushort length);

        new bool Write(string address, dynamic value);
    }
}
