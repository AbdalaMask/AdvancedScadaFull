using AdvancedScada.Common;
using AdvancedScada.Common.Client;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.BaseService.Client
{


    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ReadServiceCallbackClient : IServiceCallback
    {

        public static bool LoadTagCollection()
        {

            var objChannelManager = ChannelService.GetChannelManager();

            try
            {

                var xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
                if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile))
                {
                    return false;
                }

             
                if (TagCollectionClient.Tags.Count > 1 && objChannelManager.Channels.Count>1)
                {
                    return true;
                }
                objChannelManager.Channels.Clear();
                TagCollectionClient.Tags.Clear();
                var channels = objChannelManager.GetChannels(xmlFile);

                foreach (var ch in channels)
                    foreach (var dv in ch.Devices)
                    {

                        foreach (var db in dv.DataBlocks)
                            foreach (var tg in db.Tags)
                                TagCollectionClient.Tags.Add(
                                    $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tg.TagName}", tg);
                    }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("ReadServiceCallbackClient", ex.Message);
            }


            return true;
        }


        
        [OperationContract(IsOneWay = true)]
        public void UpdateCollection(ConnectionState status, Dictionary<string, Tag> Tags)
        {
            eventConnectionChanged?.Invoke(status);
            var tagsClient = TagCollectionClient.Tags;
            if (tagsClient == null) throw new ArgumentNullException(nameof(tagsClient));
            foreach (var author in Tags)
                if (tagsClient.ContainsKey(author.Key))
                {
                    tagsClient[author.Key].Value = author.Value.Value;
                    tagsClient[author.Key].TimeSpan = author.Value.TimeSpan;
                }

        }
    }
}