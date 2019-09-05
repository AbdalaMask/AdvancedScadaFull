Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class Thermometer
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private rectangle_0 As Rectangle

    Private rectangle_1 As Rectangle

    Private pathGradientBrush_0 As PathGradientBrush

    Private linearGradientBrush_0 As LinearGradientBrush

    Private double_0 As Double

    Private double_1 As Double

    Private double_2 As Double

    Private color_0 As Color

    Public Property FillColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            If (Me.color_0 <> value) Then
                Me.color_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            If (Me.double_1 <> value) Then
                Me.double_1 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (Me.double_0 <> value) Then
                Me.double_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Me.method_0()
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_2) Then
                Me.double_2 = value
                Me.method_1()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.double_0 = 0
        Me.double_1 = 100
        Me.color_0 = Color.Red
        Me.rectangle_0 = New Rectangle(0, 0, 10, 10)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            If (Me.linearGradientBrush_0 IsNot Nothing) Then
                Me.linearGradientBrush_0.Dispose()
            End If
            If (Me.pathGradientBrush_0 IsNot Nothing) Then
                Me.pathGradientBrush_0.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Shared Function GetRelativeColor(ByVal color As System.Drawing.Color, ByVal intensity As Double) As System.Drawing.Color
        intensity = Math.Max(intensity, 0)
        Dim color1 As System.Drawing.Color = System.Drawing.Color.FromArgb(CInt(Math.Round(Math.Min(CDbl((color.R + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.G + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.B + 1)) * intensity, 255))))
        Return color1
    End Function

    Private Sub method_0()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
                Using stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat()
                    Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                        Using font As System.Drawing.Font = New System.Drawing.Font(Me.Font.FontFamily, CSng((CDbl(MyBase.Height) * 0.025)), FontStyle.Regular, GraphicsUnit.Point)
                            graphic.DrawImage(My.Resources.ThermometerStatic, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
                            stringFormat.LineAlignment = StringAlignment.Center
                            stringFormat.Alignment = StringAlignment.Far
                            Dim double1 As Double = (Me.double_1 - Me.double_0) / 10
                            Dim rectangle(10) As System.Drawing.Rectangle
                            Dim num As Integer = 0
                            Do
                                rectangle(num) = New System.Drawing.Rectangle(0, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * (0.75 - CDbl(num) * 0.065))), CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.48)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.05)))
                                graphic.DrawString(Convert.ToString(Me.double_0 + double1 * CDbl(num)), font, New System.Drawing.SolidBrush(Me.ForeColor), rectangle(num), stringFormat)
                                num = num + 1
                            Loop While num <= 10
                            stringFormat.LineAlignment = StringAlignment.Near
                            stringFormat.Alignment = StringAlignment.Center
                            Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.01)), Me.bitmap_0.Width, Me.bitmap_0.Height)
                            graphic.DrawString(MyBase.Text, Me.Font, solidBrush, rectangle1, stringFormat)
                        End Using
                    End Using
                End Using
            End Using
            Me.method_1()
            Me.linearGradientBrush_0 = New LinearGradientBrush(Me.rectangle_0, Color.FromArgb(0, 0, 180), Color.FromArgb(0, 0, 180), 0!)
            Dim colorBlend As System.Drawing.Drawing2D.ColorBlend = New System.Drawing.Drawing2D.ColorBlend() With
            {
                .Colors = New Color() {Thermometer.GetRelativeColor(Me.color_0, 0.5), Me.color_0, Thermometer.GetRelativeColor(Me.color_0, 0.5)},
                .Positions = New Single() {Nothing, 0.5!, 1!}
            }
            Me.linearGradientBrush_0.InterpolationColors = colorBlend
            Me.rectangle_1 = New System.Drawing.Rectangle(CInt(Math.Round(CDbl(MyBase.Width) * 0.63)), CInt(Math.Round(CDbl(MyBase.Height) * 0.865)), CInt(Math.Round(CDbl(MyBase.Width) * 0.2)), CInt(Math.Round(CDbl(MyBase.Height) * 0.05)))
            Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath.AddEllipse(Me.rectangle_1)
            Me.pathGradientBrush_0 = New PathGradientBrush(graphicsPath) With
            {
                .SurroundColors = New Color() {Thermometer.GetRelativeColor(Me.color_0, 0.5)},
                .CenterColor = Me.color_0
            }
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub method_1()
        Dim num As Double = Math.Min(Me.double_2, Me.double_1)
        Dim double0 As Double = (num - Me.double_0) / (Me.double_1 - Me.double_0)
        Dim height As Double = CDbl(MyBase.Height) * 0.88
        Dim height1 As Double = CDbl(MyBase.Height) * 0.65 * double0 + CDbl(MyBase.Height) * 0.105
        If (height1 < 1) Then
            height1 = 1
        End If
        Me.rectangle_0.Height = CInt(Math.Round(height1))
        Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.125))
        Me.rectangle_0.X = CInt(Math.Round(CDbl(MyBase.Width) * 0.67))
        Me.rectangle_0.Y = CInt(Math.Round(height - height1))
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        If (Me.bitmap_1 IsNot Nothing Or Me.bitmap_0 IsNot Nothing Or Me.linearGradientBrush_0 IsNot Nothing) Then
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
                graphic.SmoothingMode = SmoothingMode.AntiAlias
                graphic.DrawImage(Me.bitmap_0, 0, 0)
                graphic.FillRectangle(Me.linearGradientBrush_0, Me.rectangle_0)
                graphic.FillEllipse(Me.pathGradientBrush_0, Me.rectangle_1)
            End Using
            painte.Graphics.DrawImageUnscaled(Me.bitmap_1, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
