using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.AsyncSocket
{
    public class HandShakeProcessor : IProcessor
    {
        private bool Handshakeok;
        private IWorkingSocket conn;
        private IProcessor processor;
        private List<Data> Steps;
        private MemoryStream IObufer;
        private Data CurrentStep;
        private int CurrentIndex;

        public void AddStep(Data Step)
        {
            Steps.Add(Step);
        }

        public HandShakeProcessor()
        {
            Steps = new List<Data>();
            IObufer = new MemoryStream();
        }

        #region IProcessor 成员

        public void Init(IWorkingSocket Sock)
        {
            conn = Sock;
        }

        public void NextProcessor(IProcessor NextProcessor)
        {
            processor = NextProcessor;
            processor.Init(conn);
        }

        public void PushData(byte[] Data, int ReadCount)
        {
            if (Handshakeok)
            {
                if (processor != null)
                {
                    processor.PushData(Data, ReadCount);
                }
            }
            else
            {
                if (ReadCount > 0)
                {
                    IObufer.Write(Data, 0, ReadCount);
                }
                if (CurrentStep == null)
                {
                    CurrentStep = Steps[CurrentIndex];
                }
                int len = CurrentStep.WaitData.Length;
                if (IObufer.Length > len)
                {
                    IObufer.Read(CurrentStep.WaitData, 0, len);
                    byte[] reply = CurrentStep.Process(CurrentStep.WaitData);
                    //conn.SendToQueue(reply);
                    IObufer.SubBytes(len);
                    CurrentIndex++;
                    byte[] nextData = IObufer.ToArray();
                    if (CurrentIndex == Steps.Count)
                    {
                        Handshakeok = true;
                        if (IObufer.Length > 0)
                        {
                            if (processor != null)
                            {
                                processor.PushData(nextData, nextData.Length);
                            }
                        }
                        return;
                    }
                    else
                    {
                        CurrentStep = Steps[CurrentIndex];
                    }
                    PushData(new byte[0], 0);
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            //throw new NotImplementedException();
            if (processor != null)
            {
                using (processor) { }
                processor = null;
            }
            using (IObufer) { }
            Steps.Clear();
            Steps = null;
            conn = null;
            CurrentStep = null;
        }

        #endregion

        public class Data
        {
            public byte[] WaitData { get; set; }

            public Func<Byte[], Byte[]> Process { get; set; }
        }
    }
}
