using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.AsyncSocket
{
    public class PackageProcessor : IProcessor
    {
        private int packagelength;

        private IWorkingSocket Conn;
        private IProcessor nextprocessor;

        private MemoryStream IOBuffer;

        public Func<byte[], int> AnalyzeLength;
        public event Action<byte[], IWorkingSocket> PackageReceived;

        public PackageProcessor()
        {
            IOBuffer = new MemoryStream();
        }


        #region IProcessor 成员

        public void Init(IWorkingSocket Sock)
        {
            Conn = Sock;
        }

        public void NextProcessor(IProcessor NextProcessor)
        {
            nextprocessor = NextProcessor;
        }

        public void PushData(byte[] Data, int ReadCount)
        {
            if (ReadCount > 0)
            {
                IOBuffer.Write(Data, 0, ReadCount);
            }
            if (packagelength == 0)
            {
                int length = 0;
                if (AnalyzeLength != null)
                {
                    length = AnalyzeLength(IOBuffer.ToArray());
                }
                if (length != 0)
                {
                    packagelength = length;
                    if (IOBuffer.Length >= packagelength)
                    {
                        ProcessData();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (IOBuffer.Length >= packagelength)
                {
                    ProcessData();
                }
            }
        }

        private void ProcessData()
        {
            byte[] data = new byte[packagelength];
            IOBuffer.Position = 0;
            IOBuffer.Read(data, 0, packagelength);
            if (nextprocessor != null)
            {
                nextprocessor.PushData(data, data.Length);
            }
            if (PackageReceived != null)
            {
                PackageReceived(data, Conn);
            }
            IOBuffer.SubBytes(packagelength);
            packagelength = 0;
            if (IOBuffer.Length > 0)
            {
                PushData(null, 0);
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (nextprocessor != null)
            {
                using (nextprocessor) { }
            }
            using (IOBuffer) { }
            nextprocessor = null;
            Conn = null;
        }

        #endregion
    }
}
