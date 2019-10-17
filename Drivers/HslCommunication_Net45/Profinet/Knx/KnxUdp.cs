using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HslCommunication.Profinet.Knx
{
    public class KnxUdp
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public KnxUdp( )
        {
            KNX_CODE = new KnxCode( );
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// 远程ip地址
        /// </summary>
        public IPEndPoint RouEndpoint
        {
            get => _rouEndpoint;
            set => _rouEndpoint = value;
        }

        /// <summary>
        /// 本机IP地址
        /// </summary>
        public IPEndPoint LocalEndpoint
        {
            get => _localEndpoint;
            set => _localEndpoint = value;
        }

        /// <summary>
        /// Knx的指令类
        /// </summary>
        public KnxCode KNX_CODE
        {
            get => kNX_CODE;
            set => kNX_CODE = value;
        }

        #endregion

        #region Connect DisConnect

        /// <summary>
        /// 和KNX网络进行握手并开始监听
        /// </summary>
        public void Connect_knx( )
        {
            udpClient = new UdpClient( LocalEndpoint ) { Client = { DontFragment = true, SendBufferSize = 0, ReceiveTimeout = stateRequestTimerInterval * 2 } };
            udpClient.Send( KNX_CODE.Handshake( LocalEndpoint ), 26, RouEndpoint );//发送握手报文
            udpClient.BeginReceive( new AsyncCallback( ReceiveCallback ), null );//开启监听
            KNX_CODE.Return_data_msg += KNX_CODE_Return_data_msg;
            KNX_CODE.GetData_msg += KNX_CODE_GetData_msg;
            KNX_CODE.Set_knx_data += KNX_CODE_Set_knx_data;
            // Thread.Sleep( 1000 );
            // KNX_CODE.knx_server_is_real(LocalEndpoint);
        }

        /// <summary>
        /// 保持KNX连接
        /// </summary>
        public void Keep_Connection( )
        {
            KNX_CODE.knx_server_is_real( LocalEndpoint );
            // Thread.Sleep( 80000 );
        }

        /// <summary>
        /// 断开和Knx的连接
        /// </summary>
        public void DisConnectKnx( )
        {
            if (KNX_CODE.Channel != 0)
            {
                var x = KNX_CODE.Disconnect_knx( KNX_CODE.Channel, LocalEndpoint );
                udpClient.Send( x, x.Length, RouEndpoint );
            }

        }

        #endregion

        /// <summary>
        /// 将报文写入KNX系统
        /// </summary>
        /// <param name="addr">地址</param>
        /// <param name="len">长度</param>
        /// <param name="data">数据</param>
        public void Set_knx_data( short addr, byte len, byte[] data )
        {
            KNX_CODE.Knx_Write( addr, len, data );
        }
        /// <summary>
        /// 读取指定KNX组地址
        /// </summary>
        /// <param name="addr">地址</param>
        public void Read_knx_data( short addr )
        {
            KNX_CODE.Knx_Resd_step1( addr );
            KNX_CODE.knx_server_is_real( LocalEndpoint );
        }
        private void KNX_CODE_Set_knx_data( byte[] data )
        {
            udpClient.Send( data, data.Length, RouEndpoint );
        }

        private void KNX_CODE_GetData_msg( short addr, byte len, byte[] data )
        {
            Console.WriteLine( "收到数据 地址：" + addr + " 长度:" + len + "数据：" + BitConverter.ToString( data ) );
        }

        
        private void KNX_CODE_Return_data_msg( byte[] data )
        {
            udpClient.Send( data, data.Length, RouEndpoint );
        }

        private void ReceiveCallback( IAsyncResult iar )
        {
            byte[] receiveData = udpClient.EndReceive( iar, ref _rouEndpoint );
            Console.WriteLine( "收到报文 {0}", BitConverter.ToString( receiveData ) );
            KNX_CODE.KNX_check( receiveData );
            udpClient.BeginReceive( new AsyncCallback( ReceiveCallback ), null );
        }

        #region Private Member

        private IPEndPoint _localEndpoint;
        private IPEndPoint _rouEndpoint;
        private KnxCode kNX_CODE;
        private UdpClient udpClient;
        private const int stateRequestTimerInterval = 60000;

        #endregion
    }
}
