using AdvancedScada.DriverBase.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
namespace AdvancedScada.DriverBase
{
    public static class ObjectExtension
    {
        public static T CopyObject<T>(this object objSource)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objSource);
                memoryStream.Position = 0L;
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
    public static class TagCollection
    {
        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
    }
    public static class DeviceCollection
    {
        public static Dictionary<string, Device> Devices { get; set; } = new Dictionary<string, Device>();
    }
}
