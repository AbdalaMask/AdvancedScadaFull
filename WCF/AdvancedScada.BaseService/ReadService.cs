using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.IODriver;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class ReadService : IReadService
    {

        private List<IServiceCallback> listCallbackChannels = new List<IServiceCallback>();

        public IServiceCallback EventDataChanged;
        DriverHelper driverHelper = new DriverHelper();

        public ReadService()
        {
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
                                        {
                                            item.UpdateCollection(DriverHelper.objConnectionState, TagCollection.Tags);
                                            item.DataTags(TagCollection.Tags);
                                        }

                                        Thread.Sleep(100);

                                    }
                                    catch (Exception ex)
                                    {
                                        if (listCallbackChannels.Remove(item))
                                        {
                                            DriverService.AddLog(string.Format("Removed Callback Channel: {0} | Exception: {1}", item.GetHashCode(), ex.Message));
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
                            DriverService.AddLog("+ Event S7ConnectorServiceCallback: " + ex.Message);
                    }
                    finally
                    {
                        Thread.Sleep(100);
                    }
                }
            });
        }
        public void Connect(Machine mac)
        {

            try
            {
                lock (listCallbackChannels)
                {
                    IServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                    if (!listCallbackChannels.Contains(callbackChannel))
                    {
                        listCallbackChannels.Add(callbackChannel);
                        DriverService.AddLog(string.Format("Added Callback Channel: {0}", callbackChannel.GetHashCode()));
                        eventLoggingMessage?.Invoke(string.Format("Added Callback Channel: {0}, IP Address: {1}.", mac.MachineName, mac.IPAddress));
                        EventChannelCount?.Invoke(1, true);
                    }
                }
            }
            catch (Exception ex)
            {
                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}| Message Exception: {2}.", mac.MachineName, mac.IPAddress, ex.Message));

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));
            }
        }

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
                                DriverService.AddLog(string.Format("Removed Callback Channel: {0}", callbackChannel.GetHashCode()));
                                EventChannelCount?.Invoke(1, false);
                                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}.", mac.MachineName, mac.IPAddress));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DriverService.AddLog(string.Format("Disconnect-Removed Callback Channel: {0}", ex.Message));
                EventChannelCount?.Invoke(1, false);
                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}.", mac.MachineName, mac.IPAddress));

            }
            finally
            {
                GC.SuppressFinalize((object)this);
            }
        }

        public void WriteTag(string tagName, dynamic value)
        {
            try
            {
                driverHelper?.WriteTag(tagName, value);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));

            }

        }
    }
}

