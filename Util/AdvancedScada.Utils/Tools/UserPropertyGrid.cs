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
            XCollection.EventPvGridDeviceGet += EventPvGridDevice;
            XCollection.EventPvGridDataBlockGet += EventPvGridDataBlock;
        }

        private void EventPvGridDataBlock(object Value, bool Visible)
        {
            PvGridDataBlock.SelectedObject = Value;
        }

        private void EventPvGridDevice(object Value, bool Visible)
        {
            PvGridDevice.SelectedObject = Value;
        }

        private void EventPvGridChannel(object Value, bool Visible)
        {
            PvGridChannel.SelectedObject = Value;
        }
    }
}
