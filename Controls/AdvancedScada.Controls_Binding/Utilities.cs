using AdvancedScada.BaseService.Client;
using AdvancedScada.IBaseService;
using System;
using System.Threading;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.Controls_Binding
{
    public enum MotorColor
    {
        Green, Red
    }

    public class Utilities
    {
        private static IReadService client;
        private static readonly object myLockRead = new object();

        public static void Write(string PLCAddressClick, dynamic Value)
        {
            try
            {
                lock (myLockRead)
                {
                    client = ClientDriverHelper.GetInstance().GetReadService();
                    if (client != null)
                    {
                        client.WriteTag(PLCAddressClick, Value);
                    }
                }

                Thread.Sleep(50);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke("WCFChannelFactory", ex.Message);
            }

        }
        public static void DisplayError(Control control, string ErrorMessage)
        {

            EventscadaException?.Invoke(control.Name, ErrorMessage);
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