using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.AsyncSocket
{
    public class TokenProcessor : IProcessor
    {
        private IWorkingSocket conn;
        private IProcessor processor;
        private MemoryStream Buffer;
        private byte token;

        public event Action<IWorkingSocket, byte[]> OnToken;

        public TokenProcessor(byte Token)
        {
            token = Token;
        }

        public void Init(IWorkingSocket Sock)
        {
            //throw new NotImplementedException();
            conn = Sock;
            Buffer = new MemoryStream();
        }

        public void NextProcessor(IProcessor NextProcessor)
        {
            //throw new NotImplementedException();
            processor = NextProcessor;
            processor.Init(conn);
        }

        public void PushData(byte[] Data, int ReadCount)
        {
            if (ReadCount < 1)
            {
                return;
            }
            Buffer.Write(Data, 0, ReadCount);
            byte[] temp = Buffer.ToArray();
            int index = 0;
            byte current = 0;
            int startposition = 0;
            while (true)
            {
                current = temp[index];
                if (current == token)
                {
                    byte[] result = new byte[index - startposition + 1];
                    Array.Copy(temp, startposition, result, 0, result.Length);
                    if (processor != null)
                    {
                        processor.PushData(result, result.Length);
                    }
                    if (OnToken != null)
                    {
                        OnToken(conn, result);
                    }
                    startposition = index + 1;
                }
                index++;
                if (index == temp.Length)
                {
                    break;
                }
            }
            Buffer.SubBytes(startposition);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            if (processor != null)
            {
                using (processor) { }
            }
            conn = null;
            processor = null;
            using (Buffer) { }
        }
    }
}
