using System;
using System.Net;
using System.Net.Sockets;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.IODriverV2.Comm
{
    public class EthernetAdapter : IDisposable
    {
        private const int READ_BUFFER_SIZE = 2048; // .

        private const int WRITE_BUFFER_SIZE = 2048; // .

        private byte[] bufferReceiver;
        private byte[] bufferSender;
        private int ConntectTimeout = 3000;

        private readonly string IP = "127.0.0.1";

        //private IPEndPoint server = null;

        private Socket mSocket;
        private readonly int Port = 502;


        public EthernetAdapter(string ip = "127.0.0.1", int port = 502)
        {
            IP = ip;
            Port = port;
        }

        public EthernetAdapter(string ip, short port, int conntectTimeout)
            : this(ip, port)
        {
            SetTimeout(conntectTimeout);
        }

        public bool Connect()
        {
            try
            {
                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                bufferReceiver = new byte[READ_BUFFER_SIZE];
                bufferSender = new byte[WRITE_BUFFER_SIZE];
                mSocket.SendBufferSize = READ_BUFFER_SIZE;
                mSocket.ReceiveBufferSize = WRITE_BUFFER_SIZE;
                var server = new IPEndPoint(IPAddress.Parse(IP), Port);
                mSocket.Connect(server);
                ////this.mSocket.NoDelay = false;
                //newsock.BeginConnect(server, new AsyncCallback(Connected), newsock);
                return true;
            }
            catch (SocketException ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return false;
            }
        }

        public void Close()
        {
            if (mSocket == null) return;
            if (mSocket.Connected) mSocket.Close();
        }

        public void SetTimeout(int conntectTimeout)
        {
            ConntectTimeout = conntectTimeout;
        }

        public int Write(byte[] frame)
        {
            try
            {
                return mSocket.Send(frame, frame.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                return 0;
            }
        }

        public byte[] Read()
        {
            try
            {
                var ns = new NetworkStream(mSocket);

                if (ns.CanRead)
                {
                    var rs = mSocket.Receive(bufferReceiver, bufferReceiver.Length, SocketFlags.None);
                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);

            }
            return bufferReceiver;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                mSocket = null;
                mSocket.Dispose();
            }
        }
        ~EthernetAdapter()
        {
            Dispose(false);
        }
    }
}
