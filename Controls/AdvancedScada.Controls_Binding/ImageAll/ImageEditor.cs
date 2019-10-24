using Scada.Scheme.Model.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace AdvancedScada.Controls_Binding.ImageAll
{
    /// <summary>
    /// Editor of images for PropertyGrid
    /// <para>Редактор изображений для PropertyGrid</para>
    /// </summary>
    public class ImageEditor : UITypeEditor
    {
        /// <summary>
        /// Директория, из которой открывались изображения
        /// </summary>
        public static string ImageDir = "";


    

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorSvc = provider == null ? null :
                (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (context != null && context.Instance != null && editorSvc != null)
            {
               

                
                        FrmImageDialog frmImageDialog =
                            new FrmImageDialog(ImageDir) { ImageDir = ImageDir };

                        if (editorSvc.ShowDialog(frmImageDialog) == DialogResult.OK)
                        {
                            value = frmImageDialog.SelectedImageName;
                            ImageDir = frmImageDialog.ImageDir;
                        }
               
                
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
