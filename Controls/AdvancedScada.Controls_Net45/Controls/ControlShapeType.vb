Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<ToolboxItem(False)>
Public Class ControlShapeType
    Inherits Control
    Public shapeType_0 As ShapeType

    Private int_0 As Integer

    Private int_1 As Integer

    Private int_2 As Integer

    Private int_3 As Integer

    Private int_4 As Integer

    Private int_5 As Integer

    Public Sub New(ByVal shapeType_1 As ShapeType)
        MyBase.New()
        Me.int_4 = 20
        Me.int_5 = 20
        Me.shapeType_0 = shapeType_1
        Dim length As Integer = [Enum].GetValues(GetType(ShapeType)).GetLength(0)
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        num1 = CInt(Math.Sqrt(CDbl(length)))
        num = length / num1
        If (length Mod num > 0) Then
            num = num + 1
        End If
        Me.int_0 = num1
        Me.int_1 = num
        Me.int_2 = Me.int_1 * Me.int_4 + (Me.int_1 - 1) * 6 + 8
        Me.int_3 = Me.int_0 * Me.int_5 + (Me.int_0 - 1) * 6 + 8
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        Dim enumerator As IEnumerator = Nothing
        If (e.Button.Equals(System.Windows.Forms.MouseButtons.Left) AndAlso e.X <= Me.int_2 AndAlso e.Y <= Me.int_3) Then
            Dim x As Integer = e.X
            Dim y As Integer = e.Y
            Dim int3 As Integer = y / (Me.int_3 / Me.int_0) * Me.int_1 + x / (Me.int_2 / Me.int_1) Mod Me.int_1
            Dim num As Integer = 0
            Try
                enumerator = [Enum].GetValues(GetType(ShapeType)).GetEnumerator()
                While enumerator.MoveNext()
                    Dim [integer] As ShapeType = DirectCast(Conversions.ToInteger(enumerator.Current), ShapeType)
                    If (num = int3) Then
                        Me.shapeType_0 = [integer]
                        SendKeys.Send("{ENTER}")
                    End If
                    num = num + 1
                End While
            Finally
                If (TypeOf enumerator Is IDisposable) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim enumerator As IEnumerator = Nothing
        Dim bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height, e.Graphics)
        Dim graphic As Graphics = Graphics.FromImage(bitmap)
        graphic.FillRectangle(Brushes.LightGray, New Rectangle(0, 0, bitmap.Width, bitmap.Height))
        e.Graphics.FillRectangle(Brushes.LightGray, New Rectangle(0, 0, MyBase.Width, MyBase.Height))
        Dim int1 As Integer = 4
        Dim num As Integer = 4
        Dim num1 As Integer = 0
        Try
            enumerator = [Enum].GetValues(GetType(ShapeType)).GetEnumerator()
            While enumerator.MoveNext()
                Dim [integer] As ShapeType = DirectCast(Conversions.ToInteger(enumerator.Current), ShapeType)
                Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                BasicShape.smethod_0(graphicsPath, [integer], Me.int_4, Me.int_5, 2)
                graphic.FillRectangle(Brushes.LightGray, 0, 0, bitmap.Width, bitmap.Height)
                graphic.FillPath(Brushes.Yellow, graphicsPath)
                graphic.DrawPath(Pens.Red, graphicsPath)
                e.Graphics.DrawImage(bitmap, int1, num, New Rectangle(New Point(0, 0), New System.Drawing.Size(Me.int_4 + 1, Me.int_5 + 1)), GraphicsUnit.Pixel)
                If (Me.shapeType_0.Equals(Me.shapeType_0)) Then
                    e.Graphics.DrawRectangle(Pens.Red, New Rectangle(New Point(int1 - 2, num - 2), New System.Drawing.Size(Me.int_4 + 4, Me.int_5 + 4)))
                End If
                num1 = num1 + 1
                int1 = num1 Mod Me.int_1 * Me.int_4
                int1 = int1 + num1 Mod Me.int_1 * 6 + 4
                num = num1 / Me.int_1 * Me.int_5
                num = num + num1 / Me.int_1 * 6 + 4
            End While
        Finally
            If (TypeOf enumerator Is IDisposable) Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
    End Sub
End Class