Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.Windows.Forms

Public Class BarLevelGraphic
    Inherits AnalogMeterBase
    Private stringFormat_1 As StringFormat

    Private stringFormat_2 As StringFormat

    Private stringFormat_3 As StringFormat

    Private stringFormat_4 As StringFormat

    Private float_0 As Single

    Private float_1 As Single

    Private string_0 As String

    Private bitmap_0 As Bitmap

    Private bool_0 As Boolean

    Private sizeF_0 As SizeF

    Private point_0 As Point

    Private string_1 As String

    Private fillDirectionOption_0 As BarLevelGraphic.FillDirectionOption

    Private brush_0 As Brush

    Private color_0 As Color

    Private brush_1 As Brush

    Private color_1 As Color

    Private brush_2 As Brush

    Private color_2 As Color

    Private double_0 As Double

    Private double_1 As Double

    Private color_3 As Color

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Public Property BarColorHighLimitValue As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (Me.double_0 <> value) Then
                Me.double_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property BarColorInLimits As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            If ((Me.color_0 <> value) Or Me.brush_0 Is Nothing) Then
                Me.color_0 = value
                If (Me.brush_0 IsNot Nothing) Then
                    Me.brush_0.Dispose()
                End If
                Me.brush_0 = New SolidBrush(value)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BarColorLowLimitValue As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            If (Me.double_1 <> value) Then
                Me.double_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property BarColorOverHighLimit As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            If ((Me.color_1 <> value) Or Me.brush_1 Is Nothing) Then
                Me.color_1 = value
                If (Me.brush_1 IsNot Nothing) Then
                    Me.brush_1.Dispose()
                End If
                Me.brush_1 = New SolidBrush(value)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BarColorUnderLowLimit As Color
        Get
            Return Me.color_2
        End Get
        Set(ByVal value As Color)
            If ((Me.color_2 <> value) Or Me.brush_2 Is Nothing) Then
                Me.color_2 = value
                If (Me.brush_2 IsNot Nothing) Then
                    Me.brush_2.Dispose()
                End If
                Me.brush_2 = New SolidBrush(value)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BarImage As Bitmap
        Get
            Return Me.bitmap_0
        End Get
        Set(ByVal value As Bitmap)
            'If (Me.bitmap_0 <> value) Then
            '    Me.bitmap_0 = value
            '    Me.color_3 = Color.Empty
            '    Me.OnValueChanged(EventArgs.Empty)
            '    MyBase.Invalidate()
            'End If
        End Set
    End Property

    Public Property BarOffset As Point
        Get
            Return Me.point_0
        End Get
        Set(ByVal value As Point)
            If (Me.point_0 <> value) Then
                Me.point_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property BarScale As SizeF
        Get
            Return Me.sizeF_0
        End Get
        Set(ByVal value As SizeF)
            If (Me.sizeF_0 <> value) Then
                Me.sizeF_0 = value
                If (Me.sizeF_0.Width <= 0!) Then
                    Me.sizeF_0.Width = 1!
                End If
                If (Me.sizeF_0.Height <= 0!) Then
                    Me.sizeF_0.Height = 1!
                End If
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BarVisible As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property FillDirection As BarLevelGraphic.FillDirectionOption
        Get
            Return Me.fillDirectionOption_0
        End Get
        Set(ByVal value As BarLevelGraphic.FillDirectionOption)
            If (value <> Me.fillDirectionOption_0) Then
                Me.fillDirectionOption_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property NumericFormat As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    Public Property TextSuffix As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            Me.string_0 = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.stringFormat_1 = New StringFormat()
        Me.stringFormat_2 = New StringFormat()
        Me.stringFormat_3 = New StringFormat()
        Me.stringFormat_4 = New StringFormat()
        Me.bitmap_0 = My.Resources.JaggedBar
        Me.bool_0 = True
        Me.sizeF_0 = New SizeF(1!, 1!)
        Me.point_0 = New Point(0, 0)
        Me.fillDirectionOption_0 = BarLevelGraphic.FillDirectionOption.Up
        Me.color_0 = Color.DarkGreen
        Me.color_1 = Color.Red
        Me.color_2 = Color.Yellow
        Me.double_0 = 999999
        Me.double_1 = -999999
        Me.ForeColor = Color.WhiteSmoke
        Me.stringFormat_2 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Near
        }
        Me.stringFormat_1 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        Me.stringFormat_3 = New StringFormat() With
        {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center
        }
        Me.stringFormat_4 = New StringFormat() With
        {
            .Alignment = StringAlignment.Far,
            .LineAlignment = StringAlignment.Center
        }
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Me.TextRectangle.X = 1
        Me.TextRectangle.Y = 1
        Me.TextRectangle.Width = MyBase.Width - 2
        Me.TextRectangle.Height = MyBase.FontHeight + 4
        Me.method_2()
        Me.method_3(Me.color_0)
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.brush_0 IsNot Nothing) Then
                    Me.brush_0.Dispose()
                End If
                If (Me.brush_2 IsNot Nothing) Then
                    Me.brush_2.Dispose()
                End If
                If (Me.brush_1 IsNot Nothing) Then
                    Me.brush_1.Dispose()
                End If
                If (Me.stringFormat_1 IsNot Nothing) Then
                    Me.stringFormat_1.Dispose()
                End If
                If (Me.stringFormat_2 IsNot Nothing) Then
                    Me.stringFormat_2.Dispose()
                End If
                If (Me.stringFormat_3 IsNot Nothing) Then
                    Me.stringFormat_3.Dispose()
                End If
                If (Me.stringFormat_4 IsNot Nothing) Then
                    Me.stringFormat_4.Dispose()
                End If
                If (Me.TextBrush IsNot Nothing) Then
                    Me.TextBrush.Dispose()
                End If
                If (Me.bitmap_1 IsNot Nothing) Then
                    Me.bitmap_1.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_2()
        Dim [single] As Single = Convert.ToSingle(1 / (Me.m_Maximum - Me.m_Minimum) * (Me.m_Value * MyBase.ValueScaleFactor - Me.m_Minimum))
        If (Me.bitmap_1 IsNot Nothing) Then
            Me.float_0 = CSng(Me.bitmap_1.Height) * [single]
            Me.float_1 = CSng(Me.bitmap_1.Width) * [single]
        End If
    End Sub

    Private Sub method_3(ByVal color_4 As Color)
        Dim rectangle As System.Drawing.Rectangle
        Using imageAttribute As ImageAttributes = New ImageAttributes()
            Try
                imageAttribute.SetColorMatrix(New ColorMatrix(New Single()() {New Single() {CSng((CDbl(color_4.R) / 255)), Nothing, Nothing, Nothing, Nothing}, New Single() {Nothing, CSng((CDbl(color_4.G) / 255)), Nothing, Nothing, Nothing}, New Single() {Nothing, Nothing, CSng((CDbl(color_4.B) / 255)), Nothing, Nothing}, New Single() {Nothing, Nothing, Nothing, 1!, Nothing}, New Single() {Nothing, Nothing, Nothing, Nothing, 1!}}))
                rectangle = If(Me.bitmap_0 Is Nothing, New System.Drawing.Rectangle(0, 0, Convert.ToInt32(CSng(MyBase.Width) * Me.sizeF_0.Width), Convert.ToInt32(CSng(MyBase.Height) * Me.sizeF_0.Height)), New System.Drawing.Rectangle(0, 0, Convert.ToInt32(CSng(Me.bitmap_0.Width) * Me.sizeF_0.Width), Convert.ToInt32(CSng(Me.bitmap_0.Height) * Me.sizeF_0.Height)))
                If (rectangle.Width > 0 And rectangle.Height > 0) Then
                    Me.bitmap_1 = New Bitmap(rectangle.Width, rectangle.Height)
                    Using graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
                        If (Me.bitmap_0 Is Nothing) Then
                            graphic.FillRectangle(New SolidBrush(color_4), 0, 0, rectangle.Width, rectangle.Height)
                        Else
                            graphic.DrawImage(Me.bitmap_0, rectangle, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height, GraphicsUnit.Pixel, imageAttribute)
                        End If
                    End Using
                    Me.bitmap_2 = New Bitmap(rectangle.Width, rectangle.Height)
                    If (Me.bitmap_0 IsNot Nothing) Then
                        Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
                            graphic1.DrawImage(Me.bitmap_0, New System.Drawing.Rectangle(0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height), New System.Drawing.Rectangle(0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height), GraphicsUnit.Pixel)
                        End Using
                    End If
                End If
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                ProjectData.ClearProjectError()
            End Try
        End Using
        Me.color_3 = color_4
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.TextBrush IsNot Nothing) Then
            Me.TextBrush.Color = MyBase.ForeColor
        Else
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
        If (Me.TextBrush IsNot Nothing AndAlso Me.bool_0) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            Dim str As String = Convert.ToString(Me.m_Value * Me.m_ValueScaleFactor, CultureInfo.CurrentCulture)
            If (Not String.IsNullOrEmpty(Me.string_1) And Not MyBase.DesignMode) Then
                Try
                    Me.m_Value.ToString(CultureInfo.CurrentCulture)
                    Dim mValue As Double = CDbl(CSng(Me.m_Value)) * Me.m_ValueScaleFactor
                    str = mValue.ToString(Me.string_1)
                Catch exception As System.Exception
                    ProjectData.SetProjectError(exception)
                    str = "Check NumericFormat and variable type"
                    ProjectData.ClearProjectError()
                End Try
            End If
            If (Me.bitmap_1 Is Nothing) Then
                Me.method_3(Me.color_0)
            End If
            If (Me.bitmap_0 IsNot Nothing) Then
                graphics.DrawImage(Me.bitmap_2, Me.point_0.X, Me.point_0.Y, Me.bitmap_2.Width, Me.bitmap_2.Height)
            End If
            Select Case Me.fillDirectionOption_0
                Case BarLevelGraphic.FillDirectionOption.Down
                    Me.TextRectangle.Y = Math.Min(Me.TextRectangle.Y, MyBase.Height - MyBase.FontHeight - 2)
                    If (Me.bitmap_1 IsNot Nothing) Then
                        Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, 0, Me.bitmap_1.Width, CInt(Math.Round(CDbl(Me.float_0))))
                        Dim rectangle2 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.point_0.X, Me.point_0.Y, Me.bitmap_1.Width, CInt(Math.Round(CDbl(Me.float_0))))
                        graphics.DrawImage(Me.bitmap_1, rectangle2, rectangle1, GraphicsUnit.Pixel)
                    End If
                    Me.TextRectangle.Y = Math.Max(Me.TextRectangle.Y, 0)
                    graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_2)
                    Exit Select
                Case BarLevelGraphic.FillDirectionOption.Left
                    If (Me.bitmap_1 IsNot Nothing) Then
                        Dim rectangle3 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Convert.ToInt32(CSng(Me.bitmap_1.Width) - Me.float_1), 0, Convert.ToInt32(Me.float_1), Me.bitmap_1.Height)
                        Dim rectangle4 As System.Drawing.Rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl((CSng(Me.bitmap_1.Width) - Me.float_1 + CSng(Me.point_0.X))))), Me.point_0.Y, CInt(Math.Round(CDbl(Me.float_1))), Me.bitmap_1.Height)
                        graphics.DrawImage(Me.bitmap_1, rectangle4, rectangle3, GraphicsUnit.Pixel)
                    End If
                    If (CInt(Math.Round(CDbl(graphics.MeasureString(String.Concat(Conversions.ToString(Me.m_Value), Me.string_0), MyBase.Font).Width))) >= Convert.ToInt32(Me.float_1)) Then
                        graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, New System.Drawing.Rectangle(1, 1, MyBase.Width - 2, MyBase.Height - 2), Me.stringFormat_4)
                        Exit Select
                    Else
                        graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, rectangle, Me.stringFormat_3)
                        Exit Select
                    End If
                Case BarLevelGraphic.FillDirectionOption.Right
                    If (Me.bitmap_1 IsNot Nothing) Then
                        Dim rectangle5 As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, 0, Convert.ToInt32(Me.float_1), Me.bitmap_1.Height)
                        Dim rectangle6 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.point_0.X, Me.point_0.Y, Convert.ToInt32(Me.float_1), Me.bitmap_1.Height)
                        graphics.DrawImage(Me.bitmap_1, rectangle6, rectangle5, GraphicsUnit.Pixel)
                    End If
                    If (CInt(Math.Round(CDbl(graphics.MeasureString(String.Concat(Conversions.ToString(Me.m_Value), Me.string_0), MyBase.Font).Width))) >= Convert.ToInt32(Me.float_1)) Then
                        graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, New System.Drawing.Rectangle(1, 1, MyBase.Width - 2, MyBase.Height - 2), Me.stringFormat_3)
                        Exit Select
                    Else
                        graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, rectangle, Me.stringFormat_4)
                        Exit Select
                    End If
                Case Else
                    If (Me.bitmap_1 IsNot Nothing) Then
                        Dim rectangle7 As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, Me.bitmap_1.Height - Convert.ToInt32(Me.float_0), Me.bitmap_1.Width, Convert.ToInt32(Me.float_0))
                        Dim rectangle8 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.point_0.X, Me.bitmap_1.Height - Convert.ToInt32(Me.float_0) + Me.point_0.Y, Me.bitmap_1.Width, CInt(Math.Round(CDbl(Me.float_0))))
                        graphics.DrawImage(Me.bitmap_1, rectangle8, rectangle7, GraphicsUnit.Pixel)
                    End If
                    Me.TextRectangle.Y = Math.Max(Me.TextRectangle.Y, 0)
                    graphics.DrawString(String.Concat(str, Me.string_0), MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_2)
                    Exit Select
            End Select
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        If (MyBase.Parent IsNot Nothing) Then
            MyBase.Parent.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        Me.method_2()
        If (If(Me.Value <= Me.BarColorHighLimitValue, False, Me.color_3 <> Me.color_1)) Then
            Me.method_3(Me.color_1)
        End If
        If (If(Me.Value >= Me.BarColorLowLimitValue, False, Me.color_3 <> Me.color_2)) Then
            Me.method_3(Me.color_2)
        End If
        If (If(Me.Value > Me.BarColorHighLimitValue OrElse Me.Value < Me.BarColorLowLimitValue, False, Me.color_3 <> Me.color_0)) Then
            Me.method_3(Me.color_0)
        End If
        MyBase.Invalidate()
    End Sub

    Public Enum FillDirectionOption
        Up
        Down
        Left
        Right
    End Enum
End Class
