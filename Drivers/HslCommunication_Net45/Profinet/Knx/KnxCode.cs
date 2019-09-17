﻿using System;
using System.Net;

namespace HslCommunication.Profinet.Knx
{
    public class KnxCode
    {
        public delegate void Return_data(byte[] data);
        /// <summary>
        /// 返回需要写入KNX总线的应答报文（应答数据）
        /// </summary>
        public event Return_data Return_data_msg;
        /// <summary>
        /// 返回需要写入的KNX系统的报文（写入数据）
        /// </summary>
        public event Return_data Set_knx_data;
        public delegate void GetData(short addr, byte len, byte[] data);
        /// <summary>
        /// 返回从knx系统得到的数据
        /// </summary>
        public event GetData GetData_msg;
        public byte Sequence_Counter { get; set; }
        public byte Channel { get; set; }
        /// <summary>
        /// 关闭KNX连接
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <param name="IP_PROT">本机IP</param>
        /// <returns></returns>
        private bool is_fresh = false;
        public byte[] Disconnect_knx(byte channel, IPEndPoint IP_PROT)
        {
            var IP = IP_PROT.Address.GetAddressBytes();
            var Prot = BitConverter.GetBytes(IP_PROT.Port);
            byte[] out_buff = new byte[16];
            out_buff[0] = 0X6;//头部长度
            out_buff[1] = 0X10;
            out_buff[2] = 0X02;
            out_buff[3] = 0X09;
            out_buff[4] = 0X00;
            out_buff[5] = 0X10;
            out_buff[6] = channel;
            out_buff[7] = 0X00;
            out_buff[8] = 0X08;
            out_buff[9] = 0X01;
            out_buff[10] = IP[0];
            out_buff[11] = IP[1];
            out_buff[12] = IP[2];
            out_buff[13] = IP[3];
            out_buff[14] = Prot[1];
            out_buff[15] = Prot[0];
            return out_buff;

        }
        /// <summary>
        /// 返回握手报文
        /// </summary>
        /// <param name="IP_PROT">本机ip地址</param>
        /// <returns></returns>
        public byte[] Handshake(IPEndPoint IP_PROT)
        {
            var IP = IP_PROT.Address.GetAddressBytes();
            var Prot = BitConverter.GetBytes(IP_PROT.Port);
            byte[] out_buff = new byte[26];
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x02;
            out_buff[3] = 0x05;
            out_buff[4] = 0x00;
            out_buff[5] = 0x1a;
            out_buff[6] = 0x08;//****
            out_buff[7] = 0x01;
            out_buff[8] = IP[0];
            out_buff[9] = IP[1];
            out_buff[10] = IP[2];
            out_buff[11] = IP[3];
            out_buff[12] = Prot[1];
            out_buff[13] = Prot[0];
            out_buff[14] = 0x08;
            out_buff[15] = 0x01;
            out_buff[16] = IP[0];
            out_buff[17] = IP[1];
            out_buff[18] = IP[2];
            out_buff[19] = IP[3];
            out_buff[20] = Prot[1];
            out_buff[21] = Prot[0];
            out_buff[22] = 0x04;
            out_buff[23] = 0x04;
            out_buff[24] = 0x02;
            out_buff[25] = 0x00;

            return out_buff;
        }
        /// <summary>
        /// KNX报文解析
        /// </summary>
        /// <param name="in_data"></param>
        public void KNX_check(byte[] in_data)
        {
            switch (in_data[2])
            {
                case 2: KNX_serverOF_2(in_data); break;
                case 4: KNX_serverOF_4(in_data); break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 写入数据到KNX系统
        /// </summary>
        /// <param name="addr">地址</param>
        /// <param name="len">长度</param>
        /// <param name="data">数据</param>
        public void Knx_Write(short addr, byte len, byte[] data)
        {
            Byte[] addr_base = BitConverter.GetBytes(addr);
            byte[] out_buff = new byte[(20 + len)];
            byte[] out_buff_len = BitConverter.GetBytes(out_buff.Length);
            if (this.Sequence_Counter + 1 <= 255)
            {
                if (this.is_fresh != false)
                    ++this.Sequence_Counter;
                else
                    this.is_fresh = true;

            }
            else
            {
                this.Sequence_Counter = 0;
            }
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x04;
            out_buff[3] = 0x20;
            out_buff[4] = out_buff_len[1];
            out_buff[5] = out_buff_len[0];
            out_buff[6] = 0x04;
            out_buff[7] = this.Channel;
            out_buff[8] = this.Sequence_Counter;
            out_buff[9] = 0x00;
            out_buff[10] = 0x11;
            out_buff[11] = 0x00;
            out_buff[12] = 0xbc;
            out_buff[13] = 0xe0;
            out_buff[14] = 0x00;
            out_buff[15] = 0x00;
            out_buff[16] = addr_base[1];
            out_buff[17] = addr_base[0];
            out_buff[18] = len;
            out_buff[19] = 0x00;
            if (len == 1)
            {
                var x = BitConverter.GetBytes((data[0] & 0x3f) | 0x80);
                out_buff[20] = x[0];
            }
            else
            {
                out_buff[20] = 0x80;
                for (int i = 2; i <= len; i++)
                {
                    out_buff[(len - 1 + 20)] = data[(i - 2)];
                }
            }
            if (this.Set_knx_data != null)
            {
                Set_knx_data(out_buff);
            }

        }
        /// <summary>
        /// 从KNX获取数据
        /// </summary>
        /// <param name="addr"></param>
        public void Knx_Resd_step1(short addr)
        {
            Byte[] addr_base = BitConverter.GetBytes(addr);
            byte[] out_buff = new byte[21];
            byte[] out_buff_len = BitConverter.GetBytes(out_buff.Length);
            if (this.Sequence_Counter + 1 <= 255)
            {
                if (this.is_fresh != false)
                    ++this.Sequence_Counter;
                else
                    this.is_fresh = true;
            }
            else
            {
                this.Sequence_Counter = 0;
            }
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x04;
            out_buff[3] = 0x20;
            out_buff[4] = out_buff_len[1];
            out_buff[5] = out_buff_len[0];
            out_buff[6] = 0x04;
            out_buff[7] = this.Channel;
            out_buff[8] = this.Sequence_Counter;
            out_buff[9] = 0x00;
            out_buff[10] = 0x11;
            out_buff[11] = 0x00;
            out_buff[12] = 0xbc;
            out_buff[13] = 0xe0;
            out_buff[14] = 0x00;
            out_buff[15] = 0x00;
            out_buff[16] = addr_base[1];
            out_buff[17] = addr_base[0];
            out_buff[18] = 0x01;
            out_buff[19] = 0x00;
            out_buff[20] = 0x00;

            if (this.Set_knx_data != null)
            {
                Return_data_msg(out_buff);
            }


        }
        /// <summary>
        /// 连接保持（每隔1s发送一次到设备）
        /// </summary>
        /// <param name="IP_PROT"></param>
        public void knx_server_is_real(IPEndPoint IP_PROT)
        {
            byte[] out_buff = new byte[16];
            var IP = IP_PROT.Address.GetAddressBytes();
            var Prot = BitConverter.GetBytes(IP_PROT.Port);
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x02;
            out_buff[3] = 0x07;
            out_buff[4] = 0x00;
            out_buff[5] = 0x10;
            out_buff[6] = this.Channel;
            out_buff[7] = 0x00;
            out_buff[8] = 0x08;
            out_buff[9] = 0x01;
            out_buff[10] = IP[0];
            out_buff[11] = IP[1];
            out_buff[12] = IP[2];
            out_buff[13] = IP[3];
            out_buff[14] = Prot[1];
            out_buff[15] = Prot[0];
            if (Return_data_msg != null)
            {
                Return_data_msg(out_buff);
            }

        }
        public short Get_knx_addr(string addr, out bool is_ok)
        {
            short out_addr = 0;
            var x = addr.Split('/');
            if (x.Length == 3)
            {
                int H = int.Parse(x[0]);
                int M = int.Parse(x[1]);
                int L = int.Parse(x[2]);
                if ((H > 31 || M > 7 || L > 255) || (H < 0 || M < 0 || L < 0))
                {
                    Console.WriteLine("地址不合法");
                    is_ok = false;
                    return out_addr;
                }
                else
                {
                    H = H << 11;
                    M = M << 8;
                    var y = H | M | L;
                    out_addr = (short)y;
                    //     Console.WriteLine(y.ToString("X"));
                    is_ok = true;
                    return out_addr;
                }

            }
            else
            {
                Console.WriteLine("地址不合法");
                is_ok = false;
                return out_addr;
            }
        }

        #region Private

        private void KNX_serverOF_2(byte[] in_data)
        {
            switch (in_data[3])
            {
                case 0x05: break;//握手
                case 0x06:
                    Extraction_of_Channel(in_data);
                    break;//握手回复（来自设备）
                case 0x07:
                    Return_status();
                    break;//请求连接状态（来自设备）
                case 0x08: break;//返回连接状态
            }

        }
        /// <summary>
        /// 返回连接状态
        /// </summary>
        private void Return_status()
        {
            byte[] out_buff = new byte[8];
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x02;
            out_buff[3] = 0x08;
            out_buff[4] = 0x00;
            out_buff[5] = 0x08;
            out_buff[6] = this.Channel;
            out_buff[7] = 0x00;
            if (Return_data_msg != null)
            {
                Return_data_msg(out_buff);
            }

        }
        /// <summary>
        /// 从握手回复报文获取通道号
        /// </summary>
        /// <param name="in_data"></param>
        private void Extraction_of_Channel(byte[] in_data)
        {
            this.Channel = in_data[6];
        }

        private void KNX_serverOF_4(byte[] in_data)
        {
            switch (in_data[3])
            {
                case 0x20: Read_com_CEMI(in_data); break;//需要进一步解析
                case 0x21: break;//数据返回确认
            }
        }

        /// <summary>
        /// 解析控制包头和CEMI
        /// </summary>
        private void Read_com_CEMI(byte[] in_data)
        {
            //   this.Sequence_Counter = in_data[8];
            Read_CEMI(in_data);
        }

        /// <summary>
        ///具体解析CEMI 
        /// </summary>
        private void Read_CEMI(byte[] in_data)
        {
            switch (in_data[10])
            {
                case 0x11: break;
                case 0x29: Read_CEMI_29(in_data); break;
                case 0x2e: Read_CEMI_2e(in_data); break;
            }
        }

        private void Read_CEMI_2e(byte[] in_data)
        {
            byte[] out_buff = new byte[10];
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x04;
            out_buff[3] = 0x21;
            out_buff[4] = 0x00;
            out_buff[5] = 0x0a;
            out_buff[6] = 0x04;
            out_buff[7] = this.Channel;
            out_buff[8] = in_data[8];
            out_buff[9] = 0x00;
            if (this.Set_knx_data != null)
            {
                Return_data_msg(out_buff);
            }

        }

        private void Read_CEMI_29(byte[] in_data)
        {
            byte[] out_data;
            var addr = BitConverter.ToInt16(in_data, 16);
            if (in_data[18] > 1)
            {
                out_data = new byte[(in_data[18 - 1])];
                for (int i = 0; i < in_data[18] - 1; i++)
                {
                    out_data[i] = in_data[21 + i];
                }
            }
            else
            {
                out_data = BitConverter.GetBytes(in_data[20] & 0x3f);
            }
            if (GetData_msg != null)
            {
                GetData_msg(addr, in_data[18], out_data);
            }
            Read_setp6(in_data);
        }

        private void Read_setp6(byte[] in_data)
        {
            byte[] out_buff = new byte[10];
            out_buff[0] = 0x06;
            out_buff[1] = 0x10;
            out_buff[2] = 0x04;
            out_buff[3] = 0x21;
            out_buff[4] = 0x00;
            out_buff[5] = 0x0A;
            out_buff[6] = 0x04;
            out_buff[7] = this.Channel;
            out_buff[8] = in_data[8];
            out_buff[9] = 0x00;
            if (Return_data_msg != null)
            {
                Return_data_msg(out_buff);
            }

        }

        #endregion
    }
}