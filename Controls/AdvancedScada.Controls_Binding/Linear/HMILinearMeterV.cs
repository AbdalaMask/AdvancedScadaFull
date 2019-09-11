using AdvancedScada.DriverBase;
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
    [ToolboxBitmap(typeof(HMILinearMeterV), "HMI7Segment.ico")]
    [Designer(typeof(HMILinearMeterVDesigner))]
    public class HMILinearMeterV : MfgControl.AdvancedHMI.Controls.LinearMeterVertical
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
                    var bd = new Binding("Value", TagCollection.Tags[_TagName], "Value", true);
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

    internal class HMILinearMeterVDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new HMILinearMeterVListItem(this));
                }

                return actionLists;
            }
        }
    }

    internal class HMILinearMeterVListItem : DesignerActionList
    {
        private readonly HMILinearMeterV _HMILinearMeterV;

        public HMILinearMeterVListItem(HMILinearMeterVDesigner owner)
            : base(owner.Component)
        {
            _HMILinearMeterV = (HMILinearMeterV)owner.Component;
        }


        public string TagName
        {
            get { return _HMILinearMeterV.TagName; }
            set { _HMILinearMeterV.TagName = value; }
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
            frm.OnTagSelected_Clicked += tagName => { SetProperty(_HMILinearMeterV, "TagName", tagName); };
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