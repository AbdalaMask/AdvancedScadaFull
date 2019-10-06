using AdvancedScada.DriverBase;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using AdvancedScada.Management.BLManager;
using System;
using System.ServiceModel;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.BaseService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class ReadService : IReadService
    {

         private bool RUN_APPLICATION;
        public IServiceCallback EventDataChanged;
        IODriver driverHelper = null;
        private ChannelService objChannelManager;
        public ReadService()
        {
            objChannelManager = ChannelService.GetChannelManager();
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
        public void Connect(Machine mac)
        {

            try
            {
                EventDataChanged = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                eventLoggingMessage?.Invoke(string.Format("Added Callback Channel: {0}, IP Address: {1}.", mac.MachineName, mac.IPAddress));
                EventChannelCount?.Invoke(1, true);
                RUN_APPLICATION = true;

                ThreadPool.QueueUserWorkItem((th) =>
                {
                    while (RUN_APPLICATION)
                    {
                        try
                        {
                            lock (objCallbackChannels)
                            {
                                if (EventDataChanged != null)
                                {

 
                                        try
                                        {
                                            if (((ICommunicationObject)EventDataChanged).State == CommunicationState.Opened)
                                            {
                                           EventDataChanged.UpdateCollection(XCollection.objConnectionState, TagCollection.Tags);
                                            EventDataChanged.DataTags(TagCollection.Tags);
                                            }

                                            Thread.Sleep(100);

                                        }
                                        catch (Exception ex)
                                        {
                                      
                                        RUN_APPLICATION = false;
                                        eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}| Message Exception: {2}.", mac.MachineName, mac.IPAddress, ex.Message));
                                        EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                                        EventDataChanged = null;
                                        EventChannelCount?.Invoke(1, false);
                                        }
                                    Thread.Sleep(100);

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
                                ServiceDriverHelper.AddLog("+ Event S7ConnectorServiceCallback: " + ex.Message);
                            EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                        }
                        finally
                        {
                            Thread.Sleep(100);
                        }
                    }
                });


                 
            }
            catch (Exception ex)
            {
                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}| Message Exception: {2}.", mac.MachineName, mac.IPAddress, ex.Message));

                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
                throw new FaultException<IFaultException>(new IFaultException(ex.Message));
            }
        }
        private object objCallbackChannels = new object();
        public void Disconnect(Machine mac)
        {

            try
            {
                RUN_APPLICATION = false;
                EventChannelCount?.Invoke(1, false);
                EventDataChanged = null;
                //driverHelper?.Disconnect();
                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel: {0}, IP Address: {1}.", mac.MachineName, mac.IPAddress));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Disconnect-Removed Callback Channel: {0}", ex.Message);
                EventscadaException?.Invoke(this.GetType().Name, ex.Message);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
          
        }

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

