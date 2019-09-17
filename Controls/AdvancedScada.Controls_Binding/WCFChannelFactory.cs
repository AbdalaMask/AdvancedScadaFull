using AdvancedScada.BaseService.Client;
using AdvancedScada.IBaseService;
using System;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Controls_Binding
{
    /// <inheritdoc />
    /// <inheritdoc />
    public class WCFChannelFactory
    {
        private static IReadService client;

        static object myLockRead = new object();

        public static void Write(string PLCAddressClick, dynamic Value)
        {
            try
            {
                lock (myLockRead)
                {
                    client = DriverHelper.GetInstance().GetReadService();
                    if (client != null)
                        client.WriteTag(PLCAddressClick, Value);
                }

                Thread.Sleep(50);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke("WCFChannelFactory", ex.Message);
            }

        }




    }
}