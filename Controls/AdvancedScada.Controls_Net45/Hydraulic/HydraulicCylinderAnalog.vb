Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class HydraulicCylinderAnalog
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private float_0 As Single

    Private rectangle_0 As Rectangle

    Private solidBrush_0 As SolidBrush

    Private stringFormat_0 As StringFormat

    Private double_0 As Double

    Private double_1 As Double

    Private double_2 As Double

    Protected m_ValueScaleFactor As Double

    Private rotateFlipType_0 As RotateFlipType

    Private bool_0 As Boolean

    Private bitmap_3 As Bitmap

    Private bool_1 As Boolean

    Private bitmap_4 As Bitmap

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    Public Property MaxValue As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            Me.double_2 = value
        End Set
    End Property

    Public Property MinValue As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            Me.double_1 = value
        End Set
    End Property

    Public Property Rotation As RotateFlipType
        Get
            Return Me.rotateFlipType_0
        End Get
        Set(ByVal value As RotateFlipType)
            Me.rotateFlipType_0 = value
            Me.bool_0 = True
            Me.method_1()
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_0) Then
                Me.double_0 = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ValueScaleFactor As Double
        Get
            Return Me.m_ValueScaleFactor
        End Get
        Set(ByVal value As Double)
            If (value <> Me.m_ValueScaleFactor) Then
                Me.m_ValueScaleFactor = value
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.double_2 = 100
        Me.m_ValueScaleFactor = 1
        Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipNone
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.LightGray
        End If
    End Sub

    <DebuggerNonUserCode>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (Me.bitmap_3 IsNot Nothing) Then
                Me.bitmap_3.Dispose()
            End If
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            If (Me.stringFormat_0 IsNot Nothing) Then
                Me.stringFormat_0.Dispose()
            End If
            If (Me.solidBrush_0 IsNot Nothing) Then
                Me.solidBrush_0.Dispose()
            End If
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            If (Me.bitmap_2 IsNot Nothing) Then
                Me.bitmap_2.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        If (MyBase.Parent IsNot Nothing) Then
            Dim visible As Boolean = MyBase.Visible
            MyBase.Visible = False
            Me.Refresh()
            Me.bitmap_4 = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height)
            Dim width As Integer = MyBase.Parent.Width
            Dim clientSize As System.Drawing.Size = MyBase.Parent.ClientSize
            Dim num As Integer = CInt(Math.Round(CDbl((width - clientSize.Width)) / 2))
            Dim height As Integer = MyBase.Parent.Height
            clientSize = MyBase.Parent.ClientSize
            Dim height1 As Integer = height - clientSize.Height - num
            Dim bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(MyBase.Parent.Width, MyBase.Parent.Height)
            Try
                MyBase.Parent.DrawToBitmap(bitmap, New Rectangle(0, 0, MyBase.Parent.Width, MyBase.Parent.Height))
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                ProjectData.ClearProjectError()
            End Try
            Dim width1 As Integer = MyBase.Width - 1
            Dim num1 As Integer = 0
            Do
                Dim height2 As Integer = MyBase.Height - 1
                Dim num2 As Integer = 0
                Do
                    Dim bitmap4 As System.Drawing.Bitmap = Me.bitmap_4
                    Dim location As Point = MyBase.Location
                    Dim x As Integer = num1 + location.X + num
                    location = MyBase.Location
                    bitmap4.SetPixel(num1, num2, bitmap.GetPixel(x, num2 + location.Y + height1))
                    num2 = num2 + 1
                Loop While num2 <= height2
                num1 = num1 + 1
            Loop While num1 <= width1
            MyBase.Visible = visible
        End If
    End Sub

    Private Sub method_1()
        Dim width As Single = CSng(MyBase.Width) / CSng((My.Resources.HydraulicCylinder.Width + 400))
        Dim height As Single = CSng(MyBase.Height) / CSng(My.Resources.HydraulicCylinder.Height)
        If (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone) Then
            width = CSng(MyBase.Width) / CSng(My.Resources.HydraulicCylinder.Height)
            height = CSng(MyBase.Height) / CSng((My.Resources.HydraulicCylinder.Width + 400))
        End If
        If (width >= height) Then
            Me.float_0 = height
        Else
            Me.float_0 = width
        End If
        If (Me.float_0 > 0!) Then
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CSng((My.Resources.HydraulicCylinder.Width + 400)) * Me.float_0), Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Height) * Me.float_0))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            graphic.DrawImage(My.Resources.HydraulicCylinder, 400! * Me.float_0, 0!, CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Width) * Me.float_0)), CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Height) * Me.float_0)))
            Me.bitmap_1.RotateFlip(Me.rotateFlipType_0)
            If (Me.bitmap_2 IsNot Nothing) Then
                Me.bitmap_2.Dispose()
            End If
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(CSng((My.Resources.HydraulicCylinder.Width + 400)) * Me.float_0), Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Height) * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.HydraulicCylinderRod, 0!, 91! * Me.float_0, CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinderRod.Width) * Me.float_0)), CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinderRod.Height) * Me.float_0)))
            Me.bitmap_2.RotateFlip(Me.rotateFlipType_0)
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng((My.Resources.HydraulicCylinder.Width + 400)) * Me.float_0), Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Height) * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.HydraulicCylinderRod, 285! * Me.float_0, 91! * Me.float_0, CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinderRod.Width) * Me.float_0)), CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinderRod.Height) * Me.float_0)))
            graphic.DrawImage(My.Resources.HydraulicCylinder, 400! * Me.float_0, 0!, CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Width) * Me.float_0)), CSng(Convert.ToInt32(CSng(My.Resources.HydraulicCylinder.Height) * Me.float_0)))
            Me.bitmap_0.RotateFlip(Me.rotateFlipType_0)
            graphic.Dispose()
            If (Me.rotateFlipType_0 = RotateFlipType.Rotate180FlipNone Or Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipX) Then
                Me.rectangle_0.X = 0
                Me.rectangle_0.Y = 1
                Me.rectangle_0.Width = CInt(Math.Round(CDbl(Me.bitmap_1.Width) * 0.55))
                Me.rectangle_0.Height = Me.bitmap_1.Height - 2
            ElseIf (Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX) Then
                Me.rectangle_0.Y = 0
                Me.rectangle_0.X = 1
                Me.rectangle_0.Width = Me.bitmap_1.Width - 2
                Me.rectangle_0.Height = CInt(Math.Round(CDbl(Me.bitmap_1.Height) * 0.55))
            ElseIf (Not (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX)) Then
                Me.rectangle_0.X = CInt(Math.Round(CDbl(Me.bitmap_1.Width) * 0.45))
                Me.rectangle_0.Y = 1
                Me.rectangle_0.Width = CInt(Math.Round(CDbl(Me.bitmap_1.Width) * 0.55))
                Me.rectangle_0.Height = Me.bitmap_1.Height - 2
            Else
                Me.rectangle_0.Y = CInt(Math.Round(CDbl(Me.bitmap_1.Height) * 0.45))
                Me.rectangle_0.X = 1
                Me.rectangle_0.Width = Me.bitmap_1.Width - 2
                Me.rectangle_0.Height = CInt(Math.Round(CDbl(Me.bitmap_1.Height) * 0.55))
            End If
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            If (Me.bitmap_3 IsNot Nothing) Then
                Me.bitmap_3.Dispose()
            End If
            Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.solidBrush_0 IsNot Nothing) Then
            Me.solidBrush_0.Color = MyBase.ForeColor
        Else
            Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnLocationChanged(ByVal e As EventArgs)
        MyBase.OnLocationChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim num As Integer
        Dim rectangle0 As System.Drawing.Rectangle = Me.rectangle_0
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_1 Is Nothing Or Me.bitmap_3 Is Nothing Or Me.solidBrush_0 Is Nothing Or (rectangle0 = rectangle)) And Me.stringFormat_0 IsNot Nothing) Then
            If (Me.bitmap_4 Is Nothing) Then
                Me.method_0()
            End If
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_3)
                graphic.DrawImage(Me.bitmap_4, 0, 0)
                Dim double2 As Double = Me.double_2 - Me.double_1
                Dim num1 As Double = Math.Max(Math.Min(Me.double_0 - Me.double_1, Me.double_2), Me.double_1) / double2
                Dim float0 As Double = CDbl((Me.float_0 * 280! - Me.float_0 * 10!))
                If (Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate180FlipX) Then
                    num = CInt(Math.Round((1 - num1) * float0 + CDbl(Me.float_0)))
                    graphic.DrawImage(Me.bitmap_2, num, 0)
                ElseIf (Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX) Then
                    num = CInt(Math.Round((1 - num1) * float0 + CDbl(Me.float_0)))
                    num = num * -1
                    graphic.DrawImage(Me.bitmap_2, 0, num)
                ElseIf (Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone) Then
                    num = CInt(Math.Round((1 - num1) * float0 + CDbl(Me.float_0)))
                    num = num * -1
                    graphic.DrawImage(Me.bitmap_2, 0, num)
                ElseIf (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone) Then
                    num = CInt(Math.Round((1 - num1) * float0 + CDbl(Me.float_0)))
                    graphic.DrawImage(Me.bitmap_2, 0, num)
                ElseIf (Me.rotateFlipType_0 = RotateFlipType.Rotate180FlipNone Or Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipX) Then
                    num = CInt(Math.Round((1 - num1) * float0 + CDbl(Me.float_0)))
                    num = num * -1
                    graphic.DrawImage(Me.bitmap_2, num, 0)
                End If
                graphic.DrawImage(Me.bitmap_1, 0, 0)
                If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                    If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                        Me.solidBrush_0.Color = MyBase.ForeColor
                    End If
                    graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
                End If
                painte.Graphics.DrawImage(Me.bitmap_3, 0, 0)
                Me.bool_1 = Me.double_0 <> 0
            End Using
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        If (Me.bool_0) Then
            MyBase.OnPaintBackground(pevent)
            Me.bool_0 = False
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.method_1()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
