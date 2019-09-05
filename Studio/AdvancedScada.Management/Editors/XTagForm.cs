using AdvancedScada.DriverBase.Devices;
using System.Windows.Forms;


namespace AdvancedScada.Management.Editors
{
    public partial class XTagForm : UserControl
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