using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.DriverBase
{
   public interface IODriver
    {
        string Name { get; }
        void InitializeService(Channel ch);
        void Connect();
        void Disconnect();
        void WriteTag(string TagName, dynamic Value);
    }
}
