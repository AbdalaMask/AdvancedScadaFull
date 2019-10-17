using AdvancedScada.DriverBase.Devices;
using System.Collections.Generic;
namespace AdvancedScada.DriverBase
{
    public static class TagCollection
    {
        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
    }

}
