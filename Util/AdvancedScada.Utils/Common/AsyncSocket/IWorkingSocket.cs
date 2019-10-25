using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.AsyncSocket
{
    public interface IWorkingSocket
    {
        long ID
        {
            get;
            set;
        }
        object SessionData
        {
            get;
            set;
        }
        void SendWaitReceive(byte[] Data);
        void SendToQueue(byte[] Data);
        void WaitReceive();
        void Disconnect();
    }
}
