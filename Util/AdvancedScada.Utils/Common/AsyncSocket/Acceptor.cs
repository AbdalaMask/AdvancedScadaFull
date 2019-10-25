/*----------------------------------------------------------------
// Copyright (C) 2011 
// 版权所有。 
//
// 文件名：Acceptor.cs
// 文件功能描述：
//
// 
// 创建标识：陈奕昆 2011-5-12
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
 * 
 * Server
 * Acceptor acp = new Acceptor(8888);
            acp.OnError = (ex, str) =>
            {
                Console.WriteLine("Server Error:" + str);
                if (ex != null)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            acp.Accepted = (sock,id,buffersize) =>
            {
                Console.WriteLine(id + " Connected");
                AsyncSocket sk = new AsyncSocket(sock, buffersize);
                sk.ID = id;
                sk.OnError += new Action<Exception, string>(sk_OnError);
                sk.Received += new Action<AsyncSocket, byte[], int>(receivedData);
                sk.Disconnected += new Action<long>(sk_Disconnected);
                TokenProcessor Proc = new TokenProcessor(0x0A);
                Proc.OnToken += new Action<IWorkingSocket, byte[]>(Proc_OnLine);
                sk.Processor = Proc;
                sk.WaitReceive();
            };

            acp.WaitAsyncAccept();
 * 
 * 
 * Client
 * string Data = "Hello i am a string\r\n";
            string ip = "127.0.0.1";

            AsyncSocket sock = null;
            for (int i = 0; i < 1000; i++)
            {
                sock = new AsyncSocket(new IPEndPoint(IPAddress.Parse(ip), 0), 1024);
                sock.OnError += new Action<Exception, string>(sock_OnError);
                sock.Connected = socket =>
                {
                    socket.Disconnected += new Action<long>(socket_Disconnected);
                    Console.WriteLine("Connected");
                    TokenProcessor proc = new TokenProcessor(0x0A);
                    proc.OnToken += new Action<IWorkingSocket, byte[]>(proc_OnLine);
                    socket.Processor = proc;
                    socket.SendWaitReceive(Encoding.UTF8.GetBytes(Data));
                };
                sock.ID = i;
                sock.ConnectTo(new IPEndPoint(IPAddress.Parse(ip), 8888));
            }
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Common.AsyncSocket
{
    public class Acceptor
    {
        private long counter;
        private Socket acceptSock;
        private IPEndPoint local;
        private AutoResetEvent acceptlock;

        public int BufferSize { get; set; }
        public int BlockSize { get; set; }
        public bool Working { get; set; }

        public Action<Socket, long, int> Accepted;
        public Action<Exception, string> OnError;

        public Acceptor(int port) : this(new IPEndPoint(IPAddress.Any, port)) { }
        public Acceptor(IPEndPoint localhost) : this(localhost, 4096) { }
        public Acceptor(IPEndPoint localhost, int buffersize) : this(localhost, buffersize, 200) { }
        public Acceptor(IPEndPoint localhost, int buffersize,int blocksize)
        {
            local = localhost;
            BufferSize = buffersize;
            BlockSize = blocksize;
            Initacceptor(local);
        }

        private void Initacceptor(IPEndPoint localhost)
        {
            acceptSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            acceptSock.Bind(localhost);
            acceptlock = new AutoResetEvent(true);
        }

        /// <summary>
        /// 监听
        /// </summary>
        public void WaitAccept()
        {
            Socket AcceptedSocket;
            acceptSock.Listen(BlockSize);
            Working = true;
            while (Working)
            {
                try
                {
                    AcceptedSocket = acceptSock.Accept();
                    counter++;
                    if (Accepted != null)
                    {
                        Accepted(AcceptedSocket, counter, BufferSize);
                    }
                }
                catch (Exception ex)
                {
                    RaisError(ex, "accept exception");
                }
            }
        }

        /// <summary>
        /// 监听异步socket
        /// </summary>
        public void WaitAsyncAccept()
        {
            acceptSock.Listen(BlockSize);
            Working = true;
            AcceptAsync();
        }

        /// <summary>
        /// 初始化异步Socket
        /// </summary>
        private void AcceptAsync()
        {
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            arg.Completed += new EventHandler<SocketAsyncEventArgs>(arg_Completed);
            bool RaisEvent = false;
            try
            {
                RaisEvent = acceptSock.AcceptAsync(arg);
            }
            catch (Exception ex)
            {
                RaisError(ex, "accept error");
                Close();
            }
            if (!RaisEvent)
            {
                ProcessAccept(acceptSock, arg);
            }
        }

        void arg_Completed(object sender, SocketAsyncEventArgs e)
        {
            //throw new NotImplementedException();
            acceptlock.WaitOne();
            ProcessAccept(sender, e);
            acceptlock.Set();
        }

        private void ProcessAccept(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                using (e)
                {
                    if (Working)
                    {
                        if (e.SocketError == SocketError.Success)
                        {
                            try
                            {
                                Accepted(e.AcceptSocket, counter, BufferSize);
                                counter++;
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
                        AcceptAsync();
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Stop();
                RaisError(ex, "normal excption");
            }
        }

        public void Stop()
        {
            Working = false;
            acceptSock.Close();
            Initacceptor(local);
        }
        public void Close()
        {
            Working = false;
            acceptSock.Close();
        }

        private void RaisError(Exception ex,string Message)
        {
            try
            {
                if (OnError != null)
                {
                    OnError(ex, Message);
                }
            }
            catch { }
        }

    }
}
