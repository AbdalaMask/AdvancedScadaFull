using AdvancedScada.DriverBase.Devices;
using System.Windows.Forms;

namespace AdvancedScada.Management.Editors
{
    public partial class XDataBlockForm : UserControl
    {
        public Channel ch;
        public DataBlock db;
        public Device dv;
        public EventDataBlockChanged eventDataBlockChanged = null;
        public XDataBlockForm()
        {
            InitializeComponent();
        }
    }
}