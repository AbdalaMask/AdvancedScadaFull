using AdvancedScada;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedScada.Controls.AHMI.Display
{
    public class HMILabelWerk : HMILabel
    {
        public HMILabelWerk()
        {
            base.Value = "لا يعمل";
        }
        protected override void OnvalueChanged(EventArgs e)
        {
            base.OnvalueChanged(e);
            if (this.Text == "False")
            {
                this.Text = "لا يعمل";
            }
            else
            {
                this.Text = "يعمل";
            }
        }

    }
}
