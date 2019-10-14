using AdvancedScada.BaseService.Client;
using AdvancedScada.IBaseService;
using System;
using System.Threading;
using static AdvancedScada.IBaseService.Common.XCollection;

namespace AdvancedScada.Controls_Binding
{
    public enum MotorColor
    {
        Green, Red
    }

    public class Utilities
    {
        private static IReadService client;

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

                EventscadaException?.Invoke("WCFChannelFactory", ex.Message);
            }

        }

        private delegate void SetLabelTextInvoker(System.Windows.Forms.Control label, string Text);
        public static void SetLabelText(System.Windows.Forms.Control Label, string Text)
        {
            if (Label.InvokeRequired == true)
            {
                Label.Invoke(new SetLabelTextInvoker(SetLabelText), Label, Text);
            }
            else
            {
                Label.Text = Text;
            }
        }



    }
}