using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Management.BLManager

{

    public class DeviceService : BaseBindingXML
    {

        private static readonly object mutex = new object();
        private static DeviceService _instance;

        private readonly DataBlockService objDataBlockManager;
        public DeviceService()
        {
            objDataBlockManager = new DataBlockService();
        }

        public static DeviceService GetDeviceManager()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new DeviceService();
                }
            }

            return _instance;
        }
        /// <summary>
        ///     Thêm mới thiết bị.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="ch">Thiết bị</param>
        public void Add(Channel ch, Device dv)
        {
            try
            {
                if (dv == null)
                {
                    throw new NullReferenceException("The device is null reference exception");
                }

                Device fDv = IsExisted(ch, dv);
                if (fDv != null)
                {
                    throw new Exception($"Device name: '{dv.DeviceName}' is existed");
                }

                ch.Devices.Add(dv);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Cập nhật thiết bị.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="dv">Thiết bị</param>
        public void Update(Channel ch, Device dv)
        {
            try
            {
                if (dv == null)
                {
                    throw new NullReferenceException("The Device is null reference exception");
                }

                Device fCh = IsExisted(ch, dv);
                if (fCh != null)
                {
                    throw new Exception($"Device name: '{dv.DeviceName}' is existed");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId)
                    {
                        item.DeviceId = dv.DeviceId;
                        item.DeviceName = dv.DeviceName;
                        item.SlaveId = dv.SlaveId;
                        item.Description = dv.Description;
                        item.DataBlocks = dv.DataBlocks;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="chId">Mã thiết bị</param>
        public void Delete(Channel ch, int chId)
        {
            try
            {
                Device result = GetByDeviceId(ch, chId);
                if (result == null)
                {
                    throw new KeyNotFoundException("Device Id is not found exception");
                }

                ch.Devices.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="chName">Tên thiết bị</param>
        public void Delete(Channel ch, string chName)
        {
            try
            {
                Device result = GetByDeviceName(ch, chName);
                if (result == null)
                {
                    throw new KeyNotFoundException("Device name is not found exception");
                }

                ch.Devices.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa thiết bị.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="dv">thiết bị</param>
        public void Delete(Channel ch, Device dv)
        {
            try
            {
                if (dv == null)
                {
                    throw new NullReferenceException("The Device is null reference exception");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId)
                    {
                        ch.Devices.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Phương thức kiểm tra thiết bị đã tồn tại chưa?
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="dv">Thiết bị</param>
        /// <returns>Thiết bị</returns>
        public Device IsExisted(Channel ch, Device dv)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId != dv.DeviceId && item.DeviceName.Equals(dv.DeviceName))
                    {
                        result = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }

        /// <summary>
        ///     Tìm kiếm kênh theo mã thiết bị.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="chId">Mã kênh</param>
        /// <returns>Thiết bị</returns>
        public Device GetByDeviceId(Channel ch, int chId)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == chId)
                    {
                        result = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }

        /// <summary>
        ///     Tìm kiếm kênh theo tên thiết bị.
        /// </summary>
        /// <param name="ch">Kênh</param>
        /// <param name="chName">Tên kênh</param>
        /// <returns>Thiết bị</returns>
        public Device GetByDeviceName(Channel ch, string chName)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceName.Equals(chName))
                    {
                        result = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }

        /// <summary>
        ///     Hàm đọc danh sách các thiết bị.
        /// </summary>
        /// <param name="chNode">XmlNode</param>
        /// <returns>Danh sách thiết bị</returns>
        public List<Device> GetDevices(XmlNode chNode)
        {
            List<Device> dvList = new List<Device>();
            try
            {
                foreach (XmlNode dvNode in chNode)
                {
                    Device newDevice = new Device
                    {
                        DeviceId = int.Parse(dvNode.Attributes[DEVICE_ID].Value),
                        DeviceName = dvNode.Attributes[DEVICE_NAME].Value,
                        SlaveId = short.Parse(dvNode.Attributes[SLAVE_ID].Value),
                        Description = dvNode.Attributes[ChannelService.DESCRIPTION].Value,
                        DataBlocks = objDataBlockManager.GetDataBlocks(dvNode)
                    };
                    dvList.Add(newDevice);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return dvList;
        }

        public Device Copy(Device source, Channel target)
        {
            Device device = source.CopyObject<Device>();
            try
            {

                if (device != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        DataBlockService dataBlockService = new DataBlockService();
                        device.DataBlocks = device.DataBlocks;
                        device.ChannelId = target.ChannelId;
                        device.DeviceId = GetNewId(target);

                        device.DeviceName = $"{device.DeviceName}New";

                        TagService tagService = new TagService();
                        if (device.DataBlocks != null)
                        {
                            foreach (DataBlock dataBlock in device.DataBlocks)
                            {
                                dataBlock.Tags = dataBlock.Tags;
                                dataBlock.ChannelId = (int)device.ChannelId;
                                dataBlock.DeviceId = device.DeviceId;

                                foreach (Tag tag in dataBlock.Tags)
                                {
                                    tag.ChannelId = dataBlock.ChannelId;
                                    tag.DeviceId = dataBlock.DeviceId;
                                    tag.DataBlockId = dataBlock.DataBlockId;

                                }
                            }
                        }
                        transactionScope.Complete();
                    }
                }
                return device;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return device;
        }

        private int GetNewId(Channel channel)
        {
            short GetInt = 0;
            try
            {



                int max = channel.Devices.Max(r => r.DeviceId);
                GetInt = (short)(max + 1);
                return GetInt;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return GetInt;
        }
    }
}