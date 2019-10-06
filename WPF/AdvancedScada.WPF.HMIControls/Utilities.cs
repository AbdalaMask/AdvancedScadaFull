using AdvancedScada.BaseService.Client;
using AdvancedScada.IBaseService;
using System;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.WPF.HMIControls
{
    public sealed class Utilities
    {
        public static IReadService client;

        static object myLockRead = new object();

        public static void Write(string PLCAddressClick, dynamic Value)
        {
            try
            {
                lock (myLockRead)
                {
                    client = ClientDriverHelper.GetInstance().GetReadService();
                    if (client != null)
                        client.WriteTag(PLCAddressClick, Value);
                }

                Thread.Sleep(50);


            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke("Utilities", ex.Message);
            }

        }



        public static object DynamicConverter(string value, Type t)
        {
            if (t == typeof(bool))
            {
                bool boolValue = false;
                if (bool.TryParse(value, out boolValue))
                {
                    return boolValue;
                }
                else
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue))
                    {
                        return System.Convert.ChangeType(intValue, t);
                    }
                    else
                    {
                        throw new Exception("Invalid Conversion of " + value);
                    }
                }
            }
            else
            {
                return Convert.ChangeType(value, t);
            }
        }


    }
}
