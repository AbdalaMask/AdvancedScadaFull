using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Windows.Design.Metadata;
using Microsoft.Windows.Design.PropertyEditing;

// The ProvideMetadata assembly-level attribute indicates to designers
// that this assembly contains a class that provides an attribute table. 
[assembly: ProvideMetadata(typeof(HslScada.Controls.Design.Metadata))]
namespace HslScada.Controls.Design
{
    // Container for any general design-time metadata to initialize.
    // Designers look for a type in the design-time assembly that 
    // implements IProvideAttributeTable. If found, designers instantiate 
    // this class and access its AttributeTable property automatically.
    internal class Metadata : IProvideAttributeTable
    {
        // Accessed by the designer to register any design-time metadata.
        public AttributeTable AttributeTable
        {
            get
            {
                AttributeTableBuilder builder = new AttributeTableBuilder();

                builder.AddCustomAttributes
                    (typeof(HMIControl.HMILable),
                    "PLCAddressValue",
                    PropertyValueEditor.CreateEditorAttribute(
                        typeof(FileBrowserDialogPropertyValueEditor)));

                return builder.CreateTable();
            }
        }
    }
}
