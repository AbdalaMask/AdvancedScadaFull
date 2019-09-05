using System;
using System.Linq;
using System.Windows.Forms;
using AdvancedScada.Studio;

namespace AdvancedScada.Studio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStudio());
        }
    }
}
