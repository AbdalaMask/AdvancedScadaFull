using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using ComponentFactory.Krypton.Toolkit;

namespace AdvancedScada.Management.Editors
{
    public partial class XTagForm : KryptonForm
    {
        public Channel ch;
        public DataBlock db;
        public Device dv;
        public EventTagChanged eventTagChanged = null;
        public Tag tg;
        public XTagForm()
        {
            InitializeComponent();
        }
    }
}