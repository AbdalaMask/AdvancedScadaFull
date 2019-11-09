using AdvancedScada.Common;
using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.Management.BLManager;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.BaseService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class ReadService : IReadService
    {
        private List<IServiceCallback> listCallbackChannels = new List<IServiceCallback>();
        IODriver driverHelper = null;
        private ChannelService objChannelManager;
        public ReadService()
        {
            objChannelManager = ChannelService.GetChannelManager();
            ThreadPool.QueueUserWorkItem((th) =>
            {
                while (true)
                {
                    try
                    {
                        lock (listCallbackChannels)
                        {
                            if (listCallbackChannels.Count > 0)
                            {
                                
                                foreach (IServiceCallback item in listCallbackChannels)
                                {
                                    try
                                    {
                                        if (((ICommunicationObject)item).State == CommunicationState.Opened)
                                            item.UpdateCollection(XCollection.objConnectionState, TagCollection.Tags);
                                        Thread.Sleep(100);

                                    }
                                    catch (Exception ex)
                                    {
                                        if (listCallbackChannels.Remove(item))
                                        {
                                            eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0} | Exception: {1}", item.GetHashCode(), ex.Message));
                                            EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                                            EventChannelCount?.Invoke(1, false);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null)
                            EventscadaException?.Invoke(this.GetType().Name, "+ Event ConnectorServiceCallback: " + ex.Message);
                    }
                    finally
                    {
                        Thread.Sleep(100);
                    }
                }
            });
        }
        private IODriver GetDriver(string ChannelTypes)
        {
            IODriver DriverHelper = null;
            var objFunctions = GetIODriver.GetFunctions();
            DriverHelper =
                       objFunctions.GetAssembly($@"\AdvancedScada.{ChannelTypes}.Core.dll",
                           $"AdvancedScada.{ChannelTypes}.Core.IODriverHelper");
            return DriverHelper;
        }
        /// <summary>
        /// Phương thức client kết nối vào Server.
        /// </summary>
        /// <param name="mac">Đối tượng máy người sử dụng đang dùng.</param>
        public void Connect(Machine mac)
        {
            try
            {
                lock (listCallbackChannels)
                {
                    EventChannelCount?.Invoke(1, true);
                    IServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                    if (!listCallbackChannels.Contains(callbackChannel))
                    {
                        listCallbackChannels.Add(callbackChannel);
                        eventLoggingMessage?.Invoke(string.Format("Added Callback Channel: {0}", callbackChannel.GetHashCode()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));
            }
        }

        /// <summary>
        /// Phương thức client ngắt kết nối vào Server.
        /// </summary>
        /// <param name="mac">Đối tượng máy người sử dụng đang dùng.</param>
        public void Disconnect(Machine mac)
        {
            try
            {
                lock (listCallbackChannels)
                {
                    if (listCallbackChannels.Count > 0)
                    {
                        IServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                        if (listCallbackChannels.Contains(callbackChannel))
                        {
                            if (listCallbackChannels.Remove(callbackChannel))
                            {
                                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}", callbackChannel.GetHashCode()));
                                EventChannelCount?.Invoke(1, false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventLoggingMessage?.Invoke(string.Format("Disconnect-Removed Callback Channel: {0}", ex.Message));
            }
            finally
            {
                GC.SuppressFinalize((object)this);
            }
        }

        /// <summary>
        /// Phương thức ghi giá trị vào của thiết bị(ví dụ: Tag của PLC).
        /// </summary>
        /// <param name="data">byte[]</param>
        public void WriteTag(string tagName, dynamic value)
        {
            try
            {
                if (objChannelManager == null) return;

                var strArrays = tagName.Split('.');
                var str = $"{strArrays[0]}.{strArrays[1]}";
                foreach (var Channels in objChannelManager.Channels)
                {
                    foreach (var dv in Channels.Devices)
                    {
                        var bEquals = $"{Channels.ChannelName}.{dv.DeviceName}".Equals(str);
                        if (bEquals)
                        {
                            driverHelper = GetDriver(Channels.ChannelTypes);

                            driverHelper?.WriteTag(tagName, value);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));
            }

        }
    }
}

