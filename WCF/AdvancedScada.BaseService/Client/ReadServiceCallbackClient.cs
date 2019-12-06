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

            ChannelService objChannelManager = ChannelService.GetChannelManager();

            try
            {

                string xmlFile = objChannelManager.ReadKey(objChannelManager.XML_NAME_DEFAULT);
                if (string.IsNullOrEmpty(xmlFile) || string.IsNullOrWhiteSpace(xmlFile))
                {
                    return false;
                }


                if (TagCollectionClient.Tags.Count > 1 && objChannelManager.Channels.Count > 1)
                {
                    return true;
                }
                objChannelManager.Channels.Clear();
                TagCollectionClient.Tags.Clear();
                List<Channel> channels = objChannelManager.GetChannels(xmlFile);

                foreach (Channel ch in channels)
                {
                    foreach (Device dv in ch.Devices)
                    {

                        foreach (DataBlock db in dv.DataBlocks)
                        {
                            DataBlockCollectionClient.DataBlocks.Add($"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}", db);
                            foreach (Tag tg in db.Tags)
                            {
                                TagCollectionClient.Tags.Add(
                                    $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tg.TagName}", tg);
                            }
                        }

                    }
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
            Dictionary<string, Tag> tagsClient = TagCollectionClient.Tags;
            if (tagsClient == null)
            {
                throw new ArgumentNullException(nameof(tagsClient));
            }

            foreach (KeyValuePair<string, Tag> author in Tags)
            {
                if (tagsClient.ContainsKey(author.Key))
                {
                    tagsClient[author.Key].Value = author.Value.Value;
                    tagsClient[author.Key].TimeSpan = author.Value.TimeSpan;
                }
            }
        }
        [OperationContract(IsOneWay = true)]
        public void UpdateCollectionDataBlock(ConnectionState status, Dictionary<string, DataBlock> DataBlocks)
        {
            eventConnectionChanged?.Invoke(status);
            Dictionary<string, DataBlock> DataBlocksClient = DataBlockCollectionClient.DataBlocks;
            if (DataBlocksClient == null)
            {
                throw new ArgumentNullException(nameof(DataBlocksClient));
            }

            foreach (KeyValuePair<string, DataBlock> author in DataBlocks)
            {
                if (DataBlocksClient.ContainsKey(author.Key))
                {

                    DataBlocksClient[author.Key].ChannelId = author.Value.ChannelId;
                    DataBlocksClient[author.Key].DeviceId = author.Value.DeviceId;
                    DataBlocksClient[author.Key].DataBlockId = author.Value.DataBlockId;
                    DataBlocksClient[author.Key].DataBlockName = author.Value.DataBlockName;
                    DataBlocksClient[author.Key].DataType = author.Value.DataType;
                    DataBlocksClient[author.Key].Length = author.Value.Length;
                    DataBlocksClient[author.Key].StartAddress = author.Value.StartAddress;
                    DataBlocksClient[author.Key].MemoryType = author.Value.MemoryType;
                    DataBlocksClient[author.Key].IsArray = author.Value.IsArray;
                    DataBlocksClient[author.Key].Tags = author.Value.Tags;
                    List<Tag> list = DataBlocksClient[author.Key].Tags;
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].Value = author.Value.Tags[i].Value;
                        list[i].TimeSpan = author.Value.Tags[i].TimeSpan;

                    }

                }
            }
        }
    }
}