using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Common.AsyncSocket
{
    public class AsyncSocket : IDisposable, IWorkingSocket
    {
        private bool connected;

        private bool processsending;

        private byte[] readbuffer;

        private Socket worker;

        public Action<AsyncSocket, byte[], int> Received;

        public Action<AsyncSocket> Sended;

        public Action<AsyncSocket> Connected;

        public event Action<long> Disconnected;

        public event Action<Exception, string> OnError;

        public event Action<IWorkingSocket, Exception> OnUserException;

        private SocketAsyncEventArgs SendArg;

        private SocketAsyncEventArgs ReadArg;

        private Queue<byte[]> sendqueue;

        private AutoResetEvent connectlock;
        private AutoResetEvent readlock;
        private AutoResetEvent writelock;

        public long ID
        {
            get;
            set;
        }

        public object SessionData
        {
            get;
            set;
        }

        private IProcessor prosessor;

        public IProcessor Processor
        {
            get
            {
                return prosessor;
            }
            set
            {
                prosessor = value;
                prosessor.Init(this);
            }
        }

        public AsyncSocket(Socket AcceptSocket, int BufferSize)
        {
            worker = AcceptSocket;
            InitSocket(BufferSize);
            connected = true;
        }

        public AsyncSocket(IPEndPoint BindLocal, int BufferSize)
        {
            worker = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            worker.Bind(BindLocal);
            InitSocket(BufferSize);
        }

        private void InitSocket(int BufferSize)
        {
            worker.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
            worker.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000);
            sendqueue = new Queue<byte[]>();
            readbuffer = new byte[BufferSize];
            SendArg = new SocketAsyncEventArgs();
            SendArg.Completed += new EventHandler<SocketAsyncEventArgs>(SendArg_Completed);
            ReadArg = new SocketAsyncEventArgs();
            ReadArg.SetBuffer(readbuffer, 0, readbuffer.Length);
            ReadArg.Completed += new EventHandler<SocketAsyncEventArgs>(ReadArg_Completed);
            connectlock = new AutoResetEvent(true);
            readlock = new AutoResetEvent(true);
            writelock = new AutoResetEvent(true);
        }

        public void ConnectTo(IPEndPoint remote)
        {
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            arg.RemoteEndPoint = remote;
            arg.Completed += new EventHandler<SocketAsyncEventArgs>(arg_Completed);
            bool RaisEvent = false;
            try
            {
                RaisEvent = worker.ConnectAsync(arg);
            }
            catch (Exception ex)
            {
                RaisError(ex, "connect error");
            }
            if (!RaisEvent)
            {
                ProcessConnect(worker, arg);
            }

        }

        void arg_Completed(object sender, SocketAsyncEventArgs e)
        {
            //throw new NotImplementedException();
            ProcessConnect(sender, e);
        }

        void ProcessConnect(object sender, SocketAsyncEventArgs e)
        {
            connectlock.WaitOne();
            if (e.LastOperation == SocketAsyncOperation.Connect)
            {
                if (e.SocketError == SocketError.Success)
                {
                    try
                    {
                        connected = true;
                        if (Connected != null)
                        {
                            Connected(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        RaisError(ex, "user program error");
                    }
                }
                else
                {
                    RaisError(null, "socket error:" + e.SocketError.ToString());
                }
            }
            else
            {
                RaisError(null, "wrong operation:" + e.LastOperation.ToString());
            }
            connectlock.Set();
        }

        public void SendWaitReceive(byte[] Data)
        {
            SendData(Data);
            WaitReceive();
        }

        public void SendToQueue(byte[] Data)
        {
            lock (sendqueue)
            {
                sendqueue.Enqueue(Data);
            }
        }

        public void WaitReceive()
        {
            if (connected)
            {
                if (worker.Connected)
                {
                    bool RaisEvent = false;
                    try
                    {
                        ReadArg.UserToken = worker;
                        RaisEvent = worker.ReceiveAsync(ReadArg);
                    }
                    catch (Exception ex)
                    {
                        RaisError(ex, "");
                    }
                    if (!RaisEvent)
                    {
                        ProcessRead(worker, ReadArg);
                    }
                }
                else
                {
                    RaisError(null, "lose connection");
                }
            }
        }

        private void SendData(byte[] Data)
        {
            if (worker.Connected)
            {
                if (worker.Poll(-1, SelectMode.SelectWrite))
                {
                    bool RaisEvent = false;
                    try
                    {
                        processsending = true;
                        SendArg.UserToken = worker;
                        SendArg.SetBuffer(Data, 0, Data.Length);
                        RaisEvent = worker.SendAsync(SendArg);
                    }
                    catch (Exception ex)
                    {
                        RaisError(ex, "");
                    }
                    if (!RaisEvent)
                    {
                        ProcessRead(worker, SendArg);
                    }
                }
                else
                {
                    RaisError(null, "can not write");
                }
            }
            else
            {
                RaisError(null, "lose connection");
            }
        }



        void ReadArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            //throw new NotImplementedException();
            readlock.WaitOne();
            ProcessRead(sender, e);
            readlock.Set();
        }

        void SendArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            //throw new NotImplementedException();
            writelock.WaitOne();
            ProcessSend(sender, e);
            writelock.Set();
        }

        void ProcessRead(object sender, SocketAsyncEventArgs e)
        {
            if (connected)
            {
                if (e.BytesTransferred == 0)
                {
                    Close();
                    return;
                }
                if (worker.Connected)
                {
                    if (e.LastOperation == SocketAsyncOperation.Receive && e.SocketError == SocketError.Success)
                    {
                        if (Processor != null)
                        {
                            try
                            {
                                lock (Processor)
                                {
                                    Processor.PushData(readbuffer, e.BytesTransferred);
                                }
                            }
                            catch (Exception ex)
                            {
                                RaisError(ex, "user program error in received");
                            }
                        }
                        if (Received != null)
                        {
                            try
                            {
                                Received(this, readbuffer, e.BytesTransferred);
                            }
                            catch (Exception ex)
                            {
                                UserException(this, ex);
                            }
                        }
                        WaitReceive();
                        lock (sendqueue)
                        {
                            if (sendqueue.Count > 0 && (!processsending))
                            {
                                SendData(sendqueue.Dequeue());
                            }
                        }
                    }
                    else
                    {
                        RaisError(null, "last operation not read :" + e.LastOperation.ToString());
                    }
                }
                else
                {
                    RaisError(null, "socket error:" + e.SocketError.ToString());
                }
            }
        }

        void ProcessSend(object sender, SocketAsyncEventArgs e)
        {
            processsending = false;
            if (e.LastOperation == SocketAsyncOperation.Send)
            {
                if (e.SocketError == SocketError.Success)
                {
                    try
                    {
                        if (Sended != null)
                        {
                            Sended(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        RaisError(ex, "user program error in sended");
                    }
                    lock (sendqueue)
                    {
                        if (sendqueue.Count > 0 && (!processsending))
                        {
                            SendData(sendqueue.Dequeue());
                        }
                        else
                        {
                            if (!connected)
                            {
                                Close();
                            }
                        }
                    }
                }
                else
                {
                    RaisError(null, "socket error:" + e.SocketError.ToString());
                }
            }
            else
            {
                RaisError(null, "last operation not send :" + e.LastOperation.ToString());
            }

        }

        public void Disconnect()
        {
            connected = false;
        }

        private void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            using (worker)
            {
                using (ReadArg) { }
                using (SendArg) { }
                if (Processor != null)
                {
                    using (Processor) { }
                }
                if (worker.Connected)
                {
                    worker.Disconnect(false);
                    worker.Close();
                }
            }
            if (Disconnected != null)
            {
                Disconnected(ID);
            }
        }

        private void UserException(IWorkingSocket Conn, Exception ex)
        {
            try
            {
                if (OnUserException != null)
                {
                    OnUserException(this, ex);
                }
            }
            catch (Exception subex)
            {
                RaisError(subex, "error on exception");
            }
        }

        private void RaisError(Exception ex, string Message)
        {
            try
            {
                Close();
                if (OnError != null)
                {
                    OnError(ex, Message);
                }
            }
            catch { }

        }
    }
}
