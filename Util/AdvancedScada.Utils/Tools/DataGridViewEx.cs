using System.Windows.Forms;

namespace AdvancedScada.Utils.Tools
{
    public class DataGridViewEx : DataGridView
    {
        public DataGridViewEx()
        {
            // if not remote desktop session then enable double-buffering optimization
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
                DoubleBuffered = true;
        }
    }
}
