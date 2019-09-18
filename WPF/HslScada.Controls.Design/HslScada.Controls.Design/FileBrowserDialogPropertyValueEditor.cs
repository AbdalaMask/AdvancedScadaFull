using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Windows.Design.Metadata;
using Microsoft.Windows.Design.PropertyEditing;
using Microsoft.Win32;
using AdvancedScada.Monitor;

namespace HslScada.Controls.Design
{
    public class FileBrowserDialogPropertyValueEditor : DialogPropertyValueEditor
    {
        private EditorResources res = new EditorResources();

        public FileBrowserDialogPropertyValueEditor()
        {
            this.InlineEditorTemplate = res["InlineEditorTemplate"] as DataTemplate; ;
        }

        public override void ShowDialog(
            PropertyValue propertyValue,
            IInputElement commandSource)
        {
            
            using (var form = new MonitorForm())
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    propertyValue.StringValue = form.lblSelectedTagName.Text;
            }
          
        }
    }
}
