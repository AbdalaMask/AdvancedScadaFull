using AdvancedScada.ImagePicker;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AdvancedScada.Controls_Binding.DialogEditor
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class GraphicDialogEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Indicates that this editor can display a Form-based interface.
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Attempts to obtain an IWindowsFormsEditorService.
            var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (ReferenceEquals(edSvc, null)) return null;

            var frm = new MainView();
            frm.OnImagSelected_Clicked += ImageName1 => { value = ImageName1; };
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();



            // If OK was not pressed, return the original value
            return value;
        }
    }
    public class GraphicDialogEditorString : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Indicates that this editor can display a Form-based interface.
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Attempts to obtain an IWindowsFormsEditorService.
            var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (ReferenceEquals(edSvc, null)) return null;
            var frm = new MainView();
            frm.OnImagSVGSelected_Clicked += ImageName1 => { value = ImageName1; };
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            // If OK was not pressed, return the original value
            return value;
        }
    }
}