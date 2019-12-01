using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;

namespace AdvancedScada.Common.Client
{
    public class TagCollectionClient
    {
        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
    }
    public static class DataBlockCollectionClient
    {


        public static Dictionary<string, DataBlock> DataBlocks { get; set; } = new Dictionary<string, DataBlock>();
    }

}
namespace AdvancedScada.Common
{
    public class TagCollection
    {
        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
    }
    public static class DataBlockCollection
    {

        
        public static Dictionary<string, DataBlock> DataBlocks { get; set; } = new Dictionary<string, DataBlock>();
    }
}