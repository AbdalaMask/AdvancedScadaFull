using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Windows.Design.Metadata;
using Microsoft.Windows.Design.PropertyEditing;
// The ProvideMetadata assembly-level attribute indicates to designers
// that this assembly contains a class that provides an attribute table. 
[assembly: ProvideMetadata(typeof(AdvancedScada.WPF.HMIControls.Design.Metadata))]
namespace AdvancedScada.WPF.HMIControls.Design
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
                    (typeof(AdvancedScada.WPF.HMIControls.Display.HMILable),
                    "PLCAddressValue",
                    PropertyValueEditor.CreateEditorAttribute(
                        typeof(FileBrowserDialogPropertyValueEditor)));
                builder.AddCustomAttributes
                 (typeof(AdvancedScada.WPF.HMIControls.SevenSegment.HMISevenSegmentsStack),
                 "PLCAddressValue",
                 PropertyValueEditor.CreateEditorAttribute(
                     typeof(FileBrowserDialogPropertyValueEditor)));
                builder.AddCustomAttributes
                 (typeof(AdvancedScada.WPF.HMIControls.Motor.HMIMotor),
                 "PLCAddressValue",
                 PropertyValueEditor.CreateEditorAttribute(
                     typeof(FileBrowserDialogPropertyValueEditor)));
                builder.AddCustomAttributes
                 (typeof(AdvancedScada.WPF.HMIControls.Motor.WaterPump),
                 "PLCAddressValue",
                 PropertyValueEditor.CreateEditorAttribute(
                     typeof(FileBrowserDialogPropertyValueEditor)));
                builder.AddCustomAttributes
                 (typeof(ButtonAll.HMIAnnunciator),
                 "PLCAddressValue",
                 PropertyValueEditor.CreateEditorAttribute(
                     typeof(FileBrowserDialogPropertyValueEditor)));

                builder.AddCustomAttributes
                   (typeof(AdvancedScada.WPF.HMIControls.ImageAll.HMIImageContainerSvg),
                   "GraphicAllOff",
                   PropertyValueEditor.CreateEditorAttribute(
                       typeof(GraphicPropertyValueEditor)), new ReadOnlyAttribute(true));
                builder.AddCustomAttributes
                  (typeof(AdvancedScada.WPF.HMIControls.ImageAll.HMIImageContainerSvg),
                  "GraphicSelect1",
                  PropertyValueEditor.CreateEditorAttribute(
                      typeof(GraphicPropertyValueEditor)), new ReadOnlyAttribute(true));

                builder.AddCustomAttributes
                  (typeof(AdvancedScada.WPF.HMIControls.ImageAll.HMIImageContainerSvg),
                  "GraphicSelect2",
                  PropertyValueEditor.CreateEditorAttribute(
                      typeof(GraphicPropertyValueEditor)), new ReadOnlyAttribute(true));



                return builder.CreateTable();
            }
        }
    }
}
