using AdvancedScada.DriverBase.Devices;
using ComponentFactory.Krypton.Toolkit;

namespace AdvancedScada.Management.Editors
{
    public partial class XDeviceForm : KryptonForm
    {
        public Channel ch = null;
        public Device dv = null;
        public EventDeviceChanged eventDeviceChanged = null;
        public XDeviceForm()
        {
            InitializeComponent();
        }
    }
}