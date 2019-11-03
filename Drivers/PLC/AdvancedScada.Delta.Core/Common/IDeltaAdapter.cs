using AdvancedScada.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Delta.Common
{
    public interface IDeltaAdapter : IDriverAdapter
    {
        new bool Write(string address, dynamic value);
        bool[] ReadDiscrete(string address, ushort length);
    }
}