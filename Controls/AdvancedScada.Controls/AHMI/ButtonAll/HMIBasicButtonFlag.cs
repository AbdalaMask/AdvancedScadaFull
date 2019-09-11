using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvancedScada;
using AdvancedScada.Controls_Net45;
using AdvancedScada.Controls.AHMI.ButtonAll;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.ButtonAll
{
    public class HMIBasicButtonFlag : HMIbutton
    {
        public enum FlagSET
        {
            Flag1 = 1,
            Flag2 = 2,
            Flag4 = 4,
            Flag8 = 8,
            Flag16 = 16,
            Flag32 = 32,
            Flag64 = 64,
            Flag128 = 128,
            Flag256 = 256,
            Flag512 = 512,
            Flag1024 = 1024

        }
        private int m_Flag = (int)FlagSET.Flag1;
        public FlagSET Flag
        {
            get
            {
                return (FlagSET)m_Flag;
            }
            set
            {
                m_Flag = (int)value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (PLCAddressClick != null && string.Compare(PLCAddressClick, string.Empty) != 0 && Enabled &&
                PLCAddressClick != null)
                try
                {
                    if (OutputType == MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet)
                    {
                         AdvancedScada.Controls.Utilities.Write(PLCAddressClick, "1");
                        if (m_MinimumHoldTime > 0) MinHoldTimer.Enabled = true;
                        if (m_MaximumHoldTime > 0) MaxHoldTimer.Enabled = true;
                    }
                    else if (OutputType == MfgControl.AdvancedHMI.Controls.OutputType.MomentaryReset)
                    {
                         AdvancedScada.Controls.Utilities.Write(PLCAddressClick, "0");
                        if (m_MinimumHoldTime > 0) MinHoldTimer.Enabled = true;
                        if (m_MaximumHoldTime > 0) MaxHoldTimer.Enabled = true;
                    }

                    else if (OutputType == MfgControl.AdvancedHMI.Controls.OutputType.SetTrue)
                    {
                         AdvancedScada.Controls.Utilities.Write(PLCAddressClick, "1");
                    }

                    else if (OutputType == MfgControl.AdvancedHMI.Controls.OutputType.SetFalse)
                    {
                         AdvancedScada.Controls.Utilities.Write(PLCAddressClick, "0");
                    }

                    else if (OutputType == MfgControl.AdvancedHMI.Controls.OutputType.Toggle)
                    {
                        bool CurrentValue = Convert.ToBoolean(Value);
                        if (CurrentValue)
                             AdvancedScada.Controls.Utilities.Write(PLCAddressClick, "0");
                        else
                             AdvancedScada.Controls.Utilities.Write(PLCAddressClick, $"{m_Flag}");
                    }
                }
                catch (Exception)
                {
                }

            //this.Invalidate();
        }
    }
}
