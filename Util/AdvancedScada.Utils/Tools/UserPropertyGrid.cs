using AdvancedScada.Common;
using System;
using System.Windows.Forms;

namespace HslScada.Studio.Tools
{
    public partial class UserPropertyGrid : UserControl
    {
        public UserPropertyGrid()
        {
            InitializeComponent();
        }

        private void UserPropertyGrid_Load(object sender, EventArgs e)
        {
            XCollection.EventPvGridChannelGet += EventPvGridChannel;

        }


        private void EventPvGridChannel(object Value, bool Visible)
        {
            PvGridChannel.SelectedObject = Value;
        }
    }
}
