using AdvancedScada.Common;
using AdvancedScada.DriverBase.Devices;
using ComponentFactory.Krypton.Toolkit;

namespace AdvancedScada.Management.Editors
{
    public partial class XDataBlockForm : KryptonForm
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