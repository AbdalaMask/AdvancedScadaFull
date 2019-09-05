Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Public Class AutoToDoubleEditor
    Inherits UITypeEditor
    Private iwindowsFormsEditorService_0 As IWindowsFormsEditorService

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim obj As Object
        If (context IsNot Nothing And context.Instance IsNot Nothing And provider IsNot Nothing) Then
            Me.iwindowsFormsEditorService_0 = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (Me.iwindowsFormsEditorService_0 IsNot Nothing) Then
                Using listBox As System.Windows.Forms.ListBox = New System.Windows.Forms.ListBox()
                    AddHandler listBox.Click, New EventHandler(AddressOf Me.method_0)
                    listBox.Items.Add("Auto")
                    listBox.SetSelected(1, True)
                    listBox.Height = 20
                    Me.iwindowsFormsEditorService_0.DropDownControl(listBox)
                    If (Operators.ConditionalCompareObjectNotEqual(listBox.SelectedItem, String.Empty, False)) Then
                        obj = Double.NaN
                        Return obj
                    End If
                End Using
            End If
        End If
        obj = value
        Return obj
    End Function

    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return If(If(context Is Nothing, True, context.Instance Is Nothing), MyBase.GetEditStyle(context), UITypeEditorEditStyle.DropDown)
    End Function

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.iwindowsFormsEditorService_0 IsNot Nothing) Then
            Me.iwindowsFormsEditorService_0.CloseDropDown()
        End If
    End Sub
End Class
