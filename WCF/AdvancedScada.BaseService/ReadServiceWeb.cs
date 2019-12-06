using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using static AdvancedScada.Common.XCollection;
namespace AdvancedScada.BaseService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReadServiceWeb : IReadServiceWeb
    {
        private readonly ChannelService objChannelManager;
        private IODriver driverHelper = null;
        public ReadServiceWeb()
        {
            objChannelManager = ChannelService.GetChannelManager();
        }
        private IODriver GetDriver(string ChannelTypes)
        {
            IODriver DriverHelper = null;
            GetIODriver objFunctions = GetIODriver.GetFunctions();
            DriverHelper =
                       objFunctions.GetAssembly($@"\AdvancedScada.{ChannelTypes}.Core.dll",
                           $"AdvancedScada.{ChannelTypes}.Core.IODriverHelper");
            return DriverHelper;
        }
        public Dictionary<string, Tag> GetCollection()
        {
            try
            {
                return TagCollection.Tags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tag> GetTags()
        {
            List<Tag> result = null;
            try
            {

                result = TagCollection.Tags.Values.ToList<Tag>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        public int WriteTag(string tagName, dynamic Value)
        {
            try
            {

                if (objChannelManager == null)
                {
                    return 0;
                }

                string[] strArrays = tagName.Split('.');
                string str = $"{strArrays[0]}.{strArrays[1]}";
                foreach (Channel Channels in objChannelManager.Channels)
                {
                    foreach (Device dv in Channels.Devices)
                    {
                        bool bEquals = $"{Channels.ChannelName}.{dv.DeviceName}".Equals(str);
                        if (bEquals)
                        {
                            driverHelper = GetDriver(Channels.ChannelTypes);

                            driverHelper?.WriteTag(tagName, Value);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));
            }
            return 1;

        }
    }
}

