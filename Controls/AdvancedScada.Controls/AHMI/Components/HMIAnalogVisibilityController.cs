
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using AdvancedScada;
 
 
using AdvancedScada.DriverBase.Comm;
using AdvancedScada.Controls.AHMI.Components;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;

namespace AdvancedScada.Controls.AHMI.Components
{
    public class HMIAnalogVisibilityController : HMIDataSubscriber
    {
        #region Properties
        private System.Windows.Forms.Control m_TargetObject;
        public System.Windows.Forms.Control TargetObject
        {
            get
            {
                return m_TargetObject;
            }
            set
            {
                m_TargetObject = value;
            }
        }


        public enum CompareTypeEnum
        {
            AboveTarget,
            EqualToTarget,
            BelowTarget
        }
        private CompareTypeEnum m_ValueCompareType = CompareTypeEnum.AboveTarget;
        public CompareTypeEnum ValueCompareType
        {
            get
            {
                return m_ValueCompareType;
            }
            set
            {
                m_ValueCompareType = value;
            }
        }

        private double m_ValueTarget = 1000;
        public double ValueTarget
        {
            get
            {
                return m_ValueTarget;
            }
            set
            {
                m_ValueTarget = value;
            }
        }
        #endregion

        #region Events
        protected override void OnDataChanged(PlcComEventArgs e)
        {
            base.OnDataChanged(e);

            if (m_TargetObject != null)
            {
                if (e != null && e.Values != null && e.Values.Count > 0)
                {
                    double v = 0;
                    //*V3.99y
                    if (double.TryParse(e.Values[0], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.CurrentInfo, out v))
                    {
                        if (m_ValueCompareType == CompareTypeEnum.AboveTarget)
                        {
                            m_TargetObject.Visible = (v > m_ValueTarget);
                        }
                        else if (m_ValueCompareType == CompareTypeEnum.EqualToTarget)
                        {
                            m_TargetObject.Visible = (v == m_ValueTarget);
                        }
                        else if (m_ValueCompareType == CompareTypeEnum.BelowTarget)
                        {
                            m_TargetObject.Visible = (v < m_ValueTarget);
                        }
                    }
                }
            }
        }
        #endregion

    }

}