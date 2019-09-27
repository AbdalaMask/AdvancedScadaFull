using AdvancedScada.DriverBase.Client;
using AdvancedScada.Monitor;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AdvancedScada.Controls_Binding.Linear
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(HMILinearMeterH), "HMI7Segment.ico")]
    [Designer(typeof(HMILinearMeterHDesigner))]
    public class HMILinearMeterH : MfgControl.AdvancedHMI.Controls.LinearMeterHorizontal
    {




        #region propartas

        private string _TagName;

        [Category("Link TagName")]
        [Browsable(true)]
        public string TagName
        {
            get { return _TagName; }

            set
            {
                _TagName = value;
                try
                {
                    if (string.IsNullOrEmpty(_TagName) || string.IsNullOrWhiteSpace(_TagName) ||
                        Licenses.LicenseManager.IsInDesignMode) return;
                    var bd = new Binding("Value", TagCollectionClient.Tags[_TagName], "Value", true);
                    if (DataBindings.Count > 0) DataBindings.Clear();
                    DataBindings.Add(bd);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion
    }

    internal class HMILinearMeterHDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new HMILinearMeterHListItem(this));
                }

                return actionLists;
            }
        }
    }

    internal class HMILinearMeterHListItem : DesignerActionList
    {
        private readonly HMILinearMeterH _HMILinearMeterH;

        public HMILinearMeterHListItem(HMILinearMeterHDesigner owner)
            : base(owner.Component)
        {
            _HMILinearMeterH = (HMILinearMeterH)owner.Component;
        }


        public string TagName
        {
            get { return _HMILinearMeterH.TagName; }
            set { _HMILinearMeterH.TagName = value; }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            var items = new DesignerActionItemCollection();
            items.Add(new DesignerActionTextItem("HMI Professional Edition", "HMI Professional Edition"));
            items.Add(new DesignerActionMethodItem(this, "ShowTagDesignerForm", "Choote Tag"));
            items.Add(new DesignerActionPropertyItem("TagName", "TagName"));

            return items;
        }

        public void ShowTagDesignerForm()
        {
            var frm = new MonitorForm(TagName);
            frm.OnTagSelected_Clicked += tagName => { SetProperty(_HMILinearMeterH, "TagName", tagName); };
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        public void SetProperty(Control control, string propertyName, object value)
        {
            var pd = TypeDescriptor.GetProperties(control)[propertyName];
            pd.SetValue(control, value);
        }
    }
}