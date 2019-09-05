Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Public Class RotationValueEditor
    Inherits UITypeEditor
    Private iwindowsFormsEditorService_0 As IWindowsFormsEditorService

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (context IsNot Nothing And context.Instance IsNot Nothing And provider IsNot Nothing) Then
            Me.iwindowsFormsEditorService_0 = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (Me.iwindowsFormsEditorService_0 IsNot Nothing) Then
                Dim rotationValueEditorForm As RotationValueEditorForm = New RotationValueEditorForm()
                Dim [single] As RotationScale = DirectCast(value, RotationScale)
                rotationValueEditorForm.ValueAtMaxCCWTextBox.Text = Conversions.ToString([single].RotationMaxValueCcw)
                rotationValueEditorForm.ValueAtMaxCWTextBox.Text = Conversions.ToString([single].RotationMaxValueCw)
                rotationValueEditorForm.CCWRotationTextBox.Text = Conversions.ToString([single].RotationCcwAngle)
                rotationValueEditorForm.CWRotationTextBox.Text = Conversions.ToString([single].RotationCwAngle)
                rotationValueEditorForm.XPositionTextBox.Text = Conversions.ToString([single].XPosition)
                rotationValueEditorForm.YPositionTextBox.Text = Conversions.ToString([single].YPosition)
                If (Me.iwindowsFormsEditorService_0.ShowDialog(rotationValueEditorForm) = DialogResult.OK) Then
                    [single].RotationMaxValueCcw = Conversions.ToSingle(rotationValueEditorForm.ValueAtMaxCCWTextBox.Text)
                    [single].RotationMaxValueCw = Conversions.ToSingle(rotationValueEditorForm.ValueAtMaxCWTextBox.Text)
                    [single].RotationCcwAngle = Conversions.ToSingle(rotationValueEditorForm.CCWRotationTextBox.Text)
                    [single].RotationCwAngle = Conversions.ToSingle(rotationValueEditorForm.CWRotationTextBox.Text)
                    [single].XPosition = Conversions.ToInteger(rotationValueEditorForm.XPositionTextBox.Text)
                    [single].YPosition = Conversions.ToInteger(rotationValueEditorForm.YPositionTextBox.Text)
                End If
            End If
        End If
        Return value
    End Function

    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Dim uITypeEditorEditStyle As System.Drawing.Design.UITypeEditorEditStyle
        uITypeEditorEditStyle = If(Not (context IsNot Nothing And context.Instance IsNot Nothing), MyBase.GetEditStyle(context), System.Drawing.Design.UITypeEditorEditStyle.Modal)
        Return uITypeEditorEditStyle
    End Function

    Protected Overridable Sub SetEditorProps(ByVal editingInstance As RotationScale, ByVal editor As RotationValueEditorForm)
        editor.ValueAtMaxCCWTextBox.Text = Conversions.ToString(editingInstance.RotationMaxValueCcw)
    End Sub
End Class
