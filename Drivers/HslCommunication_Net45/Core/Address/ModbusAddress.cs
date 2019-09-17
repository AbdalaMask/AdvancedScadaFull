using System.Text;

namespace HslCommunication.Core.Address
{
    /// <summary>
    /// Modbus协议地址格式，可以携带站号，功能码，地址信息
    /// </summary>
    public class ModbusAddress : DeviceAddressBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ModbusAddress()
        {
            Station = -1;
            Function = -1;
            Address = 0;
        }

        /// <summary>
        /// 实例化一个默认的对象，使用默认的地址初始化
        /// </summary>
        /// <param name="address">传入的地址信息，支持富地址，例如s=2;x=3;100</param>
        public ModbusAddress(string address)
        {
            Station = -1;
            Function = -1;
            Address = 0;
            Parse(address);
        }

        /// <summary>
        /// 实例化一个默认的对象，使用默认的地址初始化
        /// </summary>
        /// <param name="address">传入的地址信息，支持富地址，例如s=2;x=3;100</param>
        /// <param name="function">默认的功能码信息</param>
        public ModbusAddress(string address, byte function)
        {
            Station = -1;
            Function = function;
            Address = 0;
            Parse(address);
        }

        /// <summary>
        /// 实例化一个默认的对象，使用默认的地址初始化
        /// </summary>
        /// <param name="address">传入的地址信息，支持富地址，例如s=2;x=3;100</param>
        /// <param name="station">站号信息</param>
        /// <param name="function">默认的功能码信息</param>
        public ModbusAddress(string address, byte station, byte function)
        {
            Station = -1;
            Function = function;
            Station = station;
            Address = 0;
            Parse(address);
        }

        /// <summary>
        /// 实例化一个默认的对象，使用默认的地址初始化
        /// </summary>
        /// <param name="station">站号信息</param>
        /// <param name="function">功能码信息</param>
        /// <param name="address">地址信息</param>
        public ModbusAddress(byte station, byte function, ushort address)
        {
            Station = -1;
            Function = function;
            Address = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 站号信息
        /// </summary>
        public int Station { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public int Function { get; set; }

        #endregion

        #region Analysis Address

        /// <summary>
        /// 解析Modbus的地址码
        /// </summary>
        /// <param name="address">地址数据信息</param>
        public override void Parse(string address)
        {
            if (address.IndexOf(';') < 0)
            {
                // 正常地址，功能码03
                Address = ushort.Parse(address);
            }
            else
            {
                // 带功能码的地址
                string[] list = address.Split(';');
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i][0] == 's' || list[i][0] == 'S')
                    {
                        // 站号信息
                        this.Station = byte.Parse(list[i].Substring(2));
                    }
                    else if (list[i][0] == 'x' || list[i][0] == 'X')
                    {
                        this.Function = byte.Parse(list[i].Substring(2));
                    }
                    else
                    {
                        this.Address = ushort.Parse(list[i]);
                    }
                }
            }
        }

        #endregion

        #region Address Operate

        /// <summary>
        /// 地址新增指定的数
        /// </summary>
        /// <param name="value">数据值信息</param>
        /// <returns>新增后的地址信息</returns>
        public ModbusAddress AddressAdd(int value)
        {
            return new ModbusAddress()
            {
                Station = this.Station,
                Function = this.Function,
                Address = (ushort)(this.Address + value),
            };
        }

        /// <summary>
        /// 地址新增1
        /// </summary>
        /// <returns>新增后的地址信息</returns>
        public ModbusAddress AddressAdd()
        {
            return AddressAdd(1);
        }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>地址表示形式</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Station >= 0) sb.Append("s=" + Station + ";");
            if (Function >= 1) sb.Append("x=" + Function + ";");
            sb.Append(Address.ToString());

            return sb.ToString();
        }

        #endregion
    }
}
