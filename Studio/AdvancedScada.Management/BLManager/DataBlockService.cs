﻿using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Management.BLManager
{

    public class DataBlockService : BaseBindingXML
    {

        private static readonly object mutex = new object();
        private static DataBlockService _instance;

        private readonly TagService objTagManager;
        public DataBlockService()
        {
            objTagManager = new TagService();
        }

        public static DataBlockService GetDataBlockManager()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new DataBlockService();
                }
            }

            return _instance;
        }
        /// <summary>
        ///     Thêm mới gói dữ liệu.
        /// </summary>
        /// <param name="dv">gói dữ liệu</param>
        /// <param name="dv">gói dữ liệu</param>
        public void Add(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                DataBlock fDv = IsExisted(dv, db);
                if (fDv != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                dv.DataBlocks.Add(db);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
        public void Add(Channel ch, Device dv, DataBlock db)
        {
            Device result = null;
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId && item.DeviceName.Equals(dv.DeviceName))
                    {
                        result = item;
                        break;
                    }
                }

                DataBlock fDv = IsExisted(result, db);
                if (fDv != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                result.DataBlocks.Add(db);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
        /// <summary>
        ///     Cập nhật gói dữ liệu.
        /// </summary>
        /// <param name="dv">Thiết bị</param>
        /// <param name="db">gói dữ liệu</param>
        public void Update(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                DataBlock fCh = IsExisted(dv, db);
                if (fCh != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId == db.DataBlockId)
                    {
                        item.ChannelId = db.ChannelId;
                        item.DeviceId = db.DeviceId;
                        item.DataBlockId = db.DataBlockId;
                        item.DataBlockName = db.DataBlockName;
                        item.Description = db.Description;
                        item.TypeOfRead = db.TypeOfRead;
                        item.StartAddress = db.StartAddress;
                        item.MemoryType = db.MemoryType;
                        item.Length = db.Length;
                        item.IsArray = db.IsArray;
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
        ///     Xóa gói dữ liệu.
        /// </summary>
        /// <param name="dv">Thiết bị</param>
        /// <param name="dbId">Mã gói dữ liệu</param>
        public void Delete(Device dv, int dbId)
        {
            try
            {
                DataBlock result = GetByDataBlockId(dv, dbId);
                if (result == null)
                {
                    throw new KeyNotFoundException("DataBlock Id is not found exception");
                }

                dv.DataBlocks.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa gói dữ liệu.
        /// </summary>
        /// <param name="dv">Thiết bị</param>
        /// <param name="dbName">Tên gói dữ liệu</param>
        public void Delete(Device dv, string dbName)
        {
            try
            {
                DataBlock result = GetByDataBlockName(dv, dbName);
                if (result == null)
                {
                    throw new KeyNotFoundException("DataBlock name is not found exception");
                }

                dv.DataBlocks.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa gói dữ liệu.
        /// </summary>
        /// <param name="dv">Thiết bị</param>
        /// <param name="db">gói dữ liệu</param>
        public void Delete(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId == db.DataBlockId)
                    {
                        dv.DataBlocks.Remove(item);
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
        ///     Phương thức kiểm tra gói dữ liệu đã tồn tại chưa?
        /// </summary>
        /// <param name="dv">Thiết bị</param>
        /// <param name="db">gói dữ liệu</param>
        /// <returns>gói dữ liệu</returns>
        public DataBlock IsExisted(Device dv, DataBlock db)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId != db.DataBlockId && item.DataBlockName.Equals(db.DataBlockName))
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
        ///     Tìm kiếm gói dữ liệu theo mã gói dữ liệu.
        /// </summary>
        /// <param name="ch">Thiết bị</param>
        /// <param name="chId">Mã gói dữ liệu</param>
        /// <returns>Gói dữ liệu</returns>
        public DataBlock GetByDataBlockId(Device ch, int chId)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in ch.DataBlocks)
                {
                    if (item.DataBlockId == chId)
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
        ///     Tìm kiếm gói dữ liệu theo tên gói dữ liệu.
        /// </summary>
        /// <param name="ch">Thiết bị</param>
        /// <param name="chName">Tên gói dữ liệu</param>
        /// <returns>Gói dữ liệu</returns>
        public DataBlock GetByDataBlockName(Device ch, string chName)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in ch.DataBlocks)
                {
                    if (item.DataBlockName.Equals(chName))
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
        ///     Hàm đọc danh sách các gói dữ liệu.
        /// </summary>
        /// <param name="dvNode">XmlNode</param>
        /// <returns>Danh sách gói dữ liệu</returns>
        public List<DataBlock> GetDataBlocks(XmlNode dvNode)
        {
            List<DataBlock> dbList = new List<DataBlock>();
            try
            {
                foreach (XmlNode dbNote in dvNode)
                {
                    DataBlock db = new DataBlock
                    {
                        ChannelId = int.Parse(dbNote.Attributes[CHANNEL_ID].Value),
                        DeviceId = int.Parse(dbNote.Attributes[DEVICE_ID].Value),
                        DataBlockId = int.Parse(dbNote.Attributes[DATABLOCK_ID].Value),
                        DataBlockName = dbNote.Attributes[DATABLOCK_NAME].Value,
                        TypeOfRead = $"{dbNote.Attributes[TypeOfRead].Value}",
                        StartAddress = ushort.Parse(dbNote.Attributes[START_ADDRESS].Value),
                        MemoryType = $"{dbNote.Attributes[MemoryType].Value}",
                        Length = ushort.Parse(dbNote.Attributes[LENGTH].Value),
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", dbNote.Attributes[DATA_TYPE].Value)),
                        IsArray = bool.Parse(dbNote.Attributes[Is_Array].Value),
                        Description = dbNote.Attributes[ChannelService.DESCRIPTION].Value,
                        Tags = objTagManager.GetTags(dbNote)
                    };
                    dbList.Add(db);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return dbList;
        }

        public DataBlock Copy(DataBlock source, Device target)
        {
            DataBlock dataBlock = source.CopyObject<DataBlock>();
            try
            {

                if (dataBlock != null)
                {
                    //dataBlock.ChannelId =(int) target.ChannelId;
                    dataBlock.DeviceId = target.DeviceId;
                    DataBlockService dataBlockService = new DataBlockService();
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        dataBlock.ChannelId = dataBlock.ChannelId;
                        dataBlock.DeviceId = dataBlock.DeviceId;
                        dataBlock.DataBlockId = GetNewIdByIds(dataBlock.ChannelId, target);
                        dataBlock.DataBlockName = $"{dataBlock.DataBlockName}New";

                        TagService tagService = new TagService();
                        dataBlock.Tags = source.Tags;
                        foreach (Tag tag in dataBlock.Tags)
                        {
                            tag.ChannelId = dataBlock.ChannelId;
                            tag.DeviceId = dataBlock.DeviceId;
                            tag.DataBlockId = dataBlock.DataBlockId;

                        }
                        transactionScope.Complete();
                    }
                }
                return dataBlock;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return dataBlock;
        }

        private int GetNewIdByIds(int channelId, Device dv)
        {
            short GetInt = 0;
            try
            {



                int max = dv.DataBlocks.Max(r => r.DataBlockId);
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