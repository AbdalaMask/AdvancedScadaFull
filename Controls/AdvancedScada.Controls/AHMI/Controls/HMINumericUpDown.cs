using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Controls;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls.Subscription;
using AdvancedScada.Controls_Net45;
using AdvancedScada.DriverBase.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedScada.Controls.AHMI.Controls
{
    public class HMINumericUpDown : NumericUpDown
    {
        private bool startRead;
        private bool mouseControlledDown;
        private bool mouseControlledUp;

        #region Properties

        private decimal m_readWrite;
        [System.ComponentModel.Browsable(false)]
        public decimal ReadWrite
        {
            get
            {
                return this.m_readWrite;
            }
            set
            {
                if (this.m_readWrite != value)
                {
                    this.m_readWrite = value;
                    this.Invalidate();
                }
            }
        }

        private bool m_WriteOnly = true;
        [System.ComponentModel.Description("Prevent the control from reading the PLC address (write only).")]
        public bool AllowWriteOnly
        {
            get
            {
                return this.m_WriteOnly;
            }
            set
            {
                if (this.m_WriteOnly != value)
                {
                    this.m_WriteOnly = value;
                    SubscribeToCommDriver();
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Constructor/Destructor

        public HMINumericUpDown()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SubscribeToEvents();
        }

        //****************************************************************
        //* UserControl overrides dispose to clean up the component list.
        //****************************************************************
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (SubScriptions != null)
                    {
                        SubScriptions.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region PLC Related Properties


        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressReadWrite = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressReadWrite
        {
            get
            {
                return m_PLCAddressReadWrite;
            }
            set
            {
                if (m_PLCAddressReadWrite != value)
                {
                    m_PLCAddressReadWrite = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToCommDriver();
                }
            }
        }

        //*****************************************
        //* Property - Address in PLC to Link to
        //*****************************************
        private string m_PLCAddressVisible = string.Empty;
        [System.ComponentModel.Category("PLC Properties")]
        public string PLCAddressVisible
        {
            get
            {
                return m_PLCAddressVisible;
            }
            set
            {
                if (m_PLCAddressVisible != value)
                {
                    m_PLCAddressVisible = value;

                    //* When address is changed, re-subscribe to new address
                    SubscribeToCommDriver();
                }
            }
        }

        #endregion

        #region Events

        //********************************************************************
        //* When an instance is added to the form, set the comm component
        //* property. If a comm component does not exist, add one to the form
        //********************************************************************
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
            {

            }
            else
            {
                SubscribeToCommDriver();
            }
        }

        //***************************************************************
        //* Event - ValueChanged --> NumericUpDownHMI Value has changed *
        //***************************************************************
        private void NumericUpDownHMI_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.PLCAddressReadWrite != null)
            {
                if (mouseControlledUp)
                {
                    try
                    {
                        Utilities.Write(this.m_PLCAddressReadWrite, base.Value); //Write new value to PLCAddress
                    }
                    catch (PLCDriverException ex)
                    {
                        if (ex.ErrorCode == 1808)
                        {
                            MessageBox.Show("\"" + this.m_PLCAddressReadWrite + "\" PLC Address not found");
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    finally
                    {
                        mouseControlledDown = false;
                        mouseControlledUp = false;
                        startRead = true;
                    }
                }
            }
        }

        private void NumericUpDownHMI_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mouseControlledDown)
            {
                startRead = false;
                mouseControlledDown = true;
            }
        }

        private void NumericUpDownHMI_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseControlledUp)
            {
                mouseControlledUp = true;
                this.NumericUpDownHMI_ValueChanged(this, System.EventArgs.Empty);
            }
        }

        private void NumericUpDownHMI_Leave(object sender, EventArgs e)
        {
            if (!this.ReadOnly)
            {
                mouseControlledUp = true;
                this.NumericUpDownHMI_ValueChanged(this, System.EventArgs.Empty);
            }
        }

        private void NumericUpDownHMI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mouseControlledUp = true;
                this.NumericUpDownHMI_ValueChanged(this, System.EventArgs.Empty);
            }
        }

        #endregion

        #region Subscribing and PLC data receiving

        private SubscriptionHandler SubScriptions;
        //**************************************************
        //* Subscribe to addresses in the Comm(PLC) Driver
        //**************************************************
        private void SubscribeToCommDriver()
        {
            if (!DesignMode & IsHandleCreated)
            {
                //* Create a subscription handler object
                if (SubScriptions == null)
                {
                    SubScriptions = new SubscriptionHandler();
                    SubScriptions.Parent = this;
                }

                //'*************************
                //'* ReadWrite Subscription
                //'*************************
                if (!m_WriteOnly)
                {
                    SubScriptions.SubscribeTo(m_PLCAddressReadWrite, 1, PolledDataReturnedReadWrite);
                    startRead = true;
                }

                SubScriptions.SubscribeAutoProperties();
            }
        }

        private void PolledDataReturnedReadWrite(object sender, SubscriptionHandlerEventArgs e)
        {
            if (e.PLCComEventArgs.ErrorId == 0)
            {
                try
                {
                    if (!m_WriteOnly)
                    {
                        if (startRead && !mouseControlledDown && !mouseControlledUp)
                        {
                            base.Value = decimal.Parse(e.PLCComEventArgs.Values[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion


        //INSTANT C# NOTE: Converted event handler wireups:
        private bool EventsSubscribed = false;
        private void SubscribeToEvents()
        {
            if (EventsSubscribed)
                return;
            else
                EventsSubscribed = true;

            this.ValueChanged += NumericUpDownHMI_ValueChanged;
            this.MouseDown += NumericUpDownHMI_MouseDown;
            base.MouseUp += NumericUpDownHMI_MouseUp;
            this.Leave += NumericUpDownHMI_Leave;
            this.KeyUp += NumericUpDownHMI_KeyUp;
        }

    }

}
