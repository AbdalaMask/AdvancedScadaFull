using System.Windows;
using Microsoft.Windows.Design.PropertyEditing;
using AdvancedScada.ImagePicker;
using System.Windows.Forms;

namespace HslScada.Controls.Design
{
    //====================================================================
    public class GraphicPropertyValueEditor : DialogPropertyValueEditor
    {
        private EditorResources res = new EditorResources();

        public GraphicPropertyValueEditor()
        {
            this.InlineEditorTemplate = res["InlineEditorTemplate"] as DataTemplate; ;
        }

        public override void ShowDialog(
            PropertyValue propertyValue,
            IInputElement commandSource)
        {
            var frm = new MainView();
            frm.OnStringImageSelected_Clicked += ImageName1 => { propertyValue.StringValue = ImageName1; };
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
           
        }
    }
}
