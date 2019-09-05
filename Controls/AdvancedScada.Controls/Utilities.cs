using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using AdvancedScada;
using AdvancedScada.BaseService;
using AdvancedScada.BaseService.Client;
using AdvancedScada.IBaseService;
using AdvancedScada.IBaseService.Common;
using AdvancedScada.Utils;
using static AdvancedScada.IBaseService.Common.XCollection;
using AdvancedScada.Controls;
using AdvancedScada;

namespace AdvancedScada.Controls
{
    public enum MotorColor
    {
        Green, Red
    }
    /// <inheritdoc />
    public sealed class  Utilities
    {
        public static IReadService client;

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

                EventscadaException?.Invoke(" AdvancedScada.Controls.Utilities", ex.Message);
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