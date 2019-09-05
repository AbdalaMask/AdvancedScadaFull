Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Security.Permissions
Imports System.Windows.Forms.Design

Public Class ShapeTypeEditor
    Inherits UITypeEditor
    Public Sub New()
        MyBase.New()
    End Sub

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim shapeType0 As Object
        If (CObj(value.[GetType]()) = CObj(GetType(ShapeType))) Then
            Dim service As IWindowsFormsEditorService = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (service IsNot Nothing) Then
                Dim control0 As ControlShapeType = New ControlShapeType(DirectCast(Conversions.ToInteger(value), ShapeType))
                service.DropDownControl(control0)
                If (CObj(value.[GetType]()) <> CObj(GetType(ShapeType))) Then
                    GoTo Label1
                End If
                shapeType0 = control0.shapeType_0
                Return shapeType0
            End If
Label1:
            shapeType0 = value
        Else
            shapeType0 = value
        End If
        Return shapeType0
    End Function

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Overrides Function GetPaintValueSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Overrides Sub PaintValue(ByVal paintValuee As PaintValueEventArgs)
        Dim bounds As Rectangle = paintValuee.Bounds
        Dim width As Integer = bounds.Width + 4
        bounds = paintValuee.Bounds
        Dim bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(width, bounds.Height + 4, paintValuee.Graphics)
        Dim graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bitmap)
        Dim [integer] As ShapeType = DirectCast(Conversions.ToInteger(paintValuee.Value), ShapeType)
        Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
        bounds = paintValuee.Bounds
        Dim num As Integer = bounds.Width - 5
        bounds = paintValuee.Bounds
        BasicShape.smethod_0(graphicsPath, [integer], num, bounds.Height - 5, 2)
        graphic.FillPath(Brushes.Yellow, graphicsPath)
        graphic.DrawPath(Pens.Red, graphicsPath)
        Dim graphics As System.Drawing.Graphics = paintValuee.Graphics
        Dim point As System.Drawing.Point = New System.Drawing.Point(0, 0)
        Dim width1 As Integer = paintValuee.Bounds.Width
        bounds = paintValuee.Bounds
        graphics.DrawImage(bitmap, 3, 3, New Rectangle(point, New Size(width1, bounds.Height)), GraphicsUnit.Pixel)
    End Sub
End Class
