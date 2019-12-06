using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.Xml;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Management.BLManager
{

    public class TagService : BaseBindingXML
    {


        private static readonly object mutex = new object();
        private static TagService _instance;

        public static TagService GetTagManager()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new TagService();
                }
            }

            return _instance;
        }
        /// <summary>
        ///     Thêm mới gói dữ liệu.
        /// </summary>
        /// <param name="db">gói dữ liệu</param>
        /// <param name="db">gói dữ liệu</param>
        public void Add(DataBlock db, Tag tg)
        {
            try
            {
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                IsExisted(db, tg);
                db.Tags.Add(tg);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Cập nhật gói dữ liệu.
        /// </summary>
        /// <param name="db">Thiết bị</param>
        /// <param name="tg">gói dữ liệu</param>
        public void Update(DataBlock db, Tag tg)
        {
            try
            {
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                IsExisted(db, tg);
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tg.TagId)
                    {
                        item.TagId = tg.TagId;
                        item.TagName = tg.TagName;
                        item.Address = tg.Address;
                        item.DataType = tg.DataType;

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
        /// <param name="db">Thiết bị</param>
        /// <param name="tgId">Mã gói dữ liệu</param>
        public void Delete(DataBlock db, int tgId)
        {
            try
            {
                Tag result = GetByTagId(db, tgId);
                if (result == null)
                {
                    throw new KeyNotFoundException("Tag Id is not found exception");
                }

                db.Tags.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa gói dữ liệu.
        /// </summary>
        /// <param name="db">Thiết bị</param>
        /// <param name="dbName">Tên gói dữ liệu</param>
        public void Delete(DataBlock db, string dbName)
        {
            try
            {
                Tag result = GetByTagName(db, dbName);
                if (result == null)
                {
                    throw new KeyNotFoundException("Tag name is not found exception");
                }

                db.Tags.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa gói dữ liệu.
        /// </summary>
        /// <param name="db">Thiết bị</param>
        /// <param name="tg">gói dữ liệu</param>
        public void Delete(DataBlock db, Tag tg)
        {
            try
            {
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tg.TagId)
                    {
                        db.Tags.Remove(item);
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
        /// <param name="db">Thiết bị</param>
        /// <param name="tg">gói dữ liệu</param>
        /// <returns>gói dữ liệu</returns>
        public Tag IsExisted(DataBlock db, Tag tg)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId != tg.TagId && item.TagName.Equals(tg.TagName))
                    {
                        throw new InvalidOperationException($"Tag name: '{tg.TagName}' is existed");
                    }

                    if (item.TagId != tg.TagId && item.Address.Equals(tg.Address))
                    {
                        throw new InvalidOperationException($"Tag address: '{tg.Address}' is existed");
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
        /// <param name="db">Thiết bị</param>
        /// <param name="tgId">Mã gói dữ liệu</param>
        /// <returns>Gói dữ liệu</returns>
        public Tag GetByTagId(DataBlock db, int tgId)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tgId)
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
        /// <param name="db">Thiết bị</param>
        /// <param name="tgName">Tên gói dữ liệu</param>
        /// <returns>Gói dữ liệu</returns>
        public Tag GetByTagName(DataBlock db, string tgName)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagName.Equals(tgName))
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
        /// <param name="db">Gói dữ liệu</param>
        /// <param name="tgAddress">địa chỉ tag</param>
        /// <returns>Gói dữ liệu</returns>
        public Tag GetByAddress(DataBlock db, ushort tgAddress)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.Address.Equals(tgAddress))
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
        /// <param name="dbNote">XmlNode</param>
        /// <returns>Danh sách gói dữ liệu</returns>
        public List<Tag> GetTags(XmlNode dbNote)
        {
            List<Tag> dbList = new List<Tag>();
            try
            {
                foreach (XmlNode item in dbNote)
                {
                    Tag tg = new Tag
                    {
                        ChannelId = int.Parse(dbNote.Attributes[CHANNEL_ID].Value),
                        DeviceId = int.Parse(dbNote.Attributes[DEVICE_ID].Value),
                        DataBlockId = int.Parse(dbNote.Attributes[DATABLOCK_ID].Value),
                        TagId = int.Parse(item.Attributes[TAG_ID].Value),
                        TagName = item.Attributes[TAG_NAME].Value,
                        Address = item.Attributes[ADDRESS].Value,
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", item.Attributes[DATA_TYPE].Value)),
                        Description = item.Attributes[ChannelService.DESCRIPTION].Value
                    };
                    dbList.Add(tg);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return dbList;
        }


    }
}