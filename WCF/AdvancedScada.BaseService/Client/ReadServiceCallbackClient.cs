using AdvancedScada.DriverBase.Client;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using AdvancedScada.IBaseService;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService.Client
{
    public delegate void EventDeviceStateChanged(Device dv);

    [CallbackBehavior(UseSynchronizationContext = true)]
    public class ReadServiceCallbackClient : IServiceCallback
    {
        public static event EventDeviceStateChanged eventDeviceStateChanged;
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

                objChannelManager.Channels.Clear();
                TagCollectionClient.Tags.Clear();
                DeviceCollectionClient.Devices.Clear();
                var channels = objChannelManager.GetChannels(xmlFile);

                foreach (var ch in channels)
                    foreach (var dv in ch.Devices)
                    {
                        DeviceCollectionClient.Devices.Add($"{ch.ChannelName}.{dv.DeviceName}", dv);
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
        public void DataDevices(Dictionary<string, Device> Devices)
        {
            var DevicesClient = DeviceCollectionClient.Devices;
            if (DevicesClient == null) throw new ArgumentNullException(nameof(DevicesClient));
            foreach (var dv in Devices)
                if (DevicesClient.ContainsKey(dv.Key) && DevicesClient[dv.Key].DeviceState != dv.Value.DeviceState)
                {
                    DevicesClient[dv.Key].ChannelId = dv.Value.ChannelId;
                    DevicesClient[dv.Key].DeviceId = dv.Value.DeviceId;
                    DevicesClient[dv.Key].DeviceName = dv.Value.DeviceName;
                    DevicesClient[dv.Key].SlaveId = dv.Value.SlaveId;
                    DevicesClient[dv.Key].Status = dv.Value.Status;
                    DevicesClient[dv.Key].DeviceState = dv.Value.DeviceState;
                    DevicesClient[dv.Key].IsActived = dv.Value.IsActived;

                    if (ReadServiceCallbackClient.eventDeviceStateChanged != null)
                    {
                        ReadServiceCallbackClient.eventDeviceStateChanged(dv.Value);
                    }

                }


        }

        public void DataTags(Dictionary<string, Tag> Tags)
        {
            var tagsClient = TagCollectionClient.Tags;
            if (tagsClient == null) throw new ArgumentNullException(nameof(tagsClient));
            foreach (var author in Tags)
                if (tagsClient.ContainsKey(author.Key))
                {
                    tagsClient[author.Key].Value = author.Value.Value;
                    tagsClient[author.Key].Checked = author.Value.Checked;
                    tagsClient[author.Key].Enabled = author.Value.Enabled;
                    tagsClient[author.Key].ValueSelect1 = author.Value.ValueSelect1;
                    tagsClient[author.Key].ValueSelect2 = author.Value.ValueSelect2;
                    tagsClient[author.Key].Visible = author.Value.Visible;
                    tagsClient[author.Key].Timestamp = author.Value.Timestamp;
                }
        }
    }
}