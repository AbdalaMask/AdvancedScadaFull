using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.AsyncSocket
{
    public interface IProcessor : IDisposable
    {
        void Init(IWorkingSocket Sock);
        void NextProcessor(IProcessor NextProcessor);
        void PushData(byte[] Data, int ReadCount);
    }
}
