Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Globalization

Public Class GaugeCompact
    Inherits MeterCompact
    Private int_5 As Integer

    Private int_6 As Integer

    Private int_7 As Integer

    Private int_8 As Integer

    Private float_1 As Single

    Private float_2 As Single

    Private int_9 As Integer

    Private int_10 As Integer

    Private int_11 As Integer

    Public Property TickMarkInset As Integer
        Get
            Return Me.int_11
        End Get
        Set(ByVal value As Integer)
            If (Me.int_11 <> value) Then
                Me.int_11 = Math.Min(Math.Max(value, 0), 150)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.float_1 = 135!
        Me.float_2 = 405!
    End Sub

    Protected Overrides Sub CreateStaticImage()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            If (Me.StaticImage IsNot Nothing) Then
                Me.StaticImage.Dispose()
            End If
            Me.StaticImage = New Bitmap(MyBase.Width, MyBase.Height)
            Me.int_5 = MyBase.BorderWidth * 2
            Me.int_6 = MyBase.BorderWidth * 2
            Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                Using graphic As Graphics = Graphics.FromImage(Me.StaticImage)
                    graphicsPath.AddEllipse(0, 0, MyBase.Width, MyBase.Height)
                    If (MyBase.BorderWidth > 0) Then
                        MyBase.Region = Nothing
                    Else
                        MyBase.Region = New System.Drawing.Region(graphicsPath)
                    End If
                    graphic.SmoothingMode = SmoothingMode.AntiAlias
                    Me.int_7 = MyBase.Width - Me.int_5 * 2
                    Me.int_8 = MyBase.Height - Me.int_6 * 2
                    Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.int_5, Me.int_6, Me.int_7 - Me.int_11, Me.int_8 - Me.int_11)
                    MyBase.DrawColorBands(graphic, CSng(MyBase.Width) / 2! - CSng(MyBase.MajorTickLength) * 0.35! - CSng(MyBase.BorderWidth) * 2! - CSng(Me.int_11) / 2!, CSng(MyBase.MajorTickLength) * 0.8!, 225!, 270!, Convert.ToInt32(Math.Floor(CDbl(MyBase.Height) / 2)))
                    Me.method_2(graphic, rectangle, Convert.ToInt32(CDbl(Me.int_7) / 2 + CDbl(Me.int_5)), Convert.ToInt32(CDbl(Me.int_8) / 2 + CDbl(Me.int_6)))
                    Me.method_1(graphic)
                    If (MyBase.BorderWidth > 0) Then
                        BeveledButtonDisplay.Draw3DBorder(Me, graphic, Me.m_BorderColor, MyBase.BorderWidth)
                    End If
                End Using
            End Using
            Me.CreateNeedle(CSng((CDbl(MyBase.Width) / 2 - CDbl((MyBase.BorderWidth * 2)) - CDbl(MyBase.MajorTickLength) / 2 - CDbl(Me.int_11) / 2)))
            MyBase.Invalidate()
        End If
    End Sub

    Public Function GetRadian(ByVal theta As Single) As Single
        Return theta * 3.14159274! / 180!
    End Function

    Private Sub method_1(ByVal graphics_0 As Graphics)
        Dim sizeF As System.Drawing.SizeF = graphics_0.MeasureString(Me.Text, Me.Font)
        Dim rectangleF As System.Drawing.RectangleF = New System.Drawing.RectangleF(0!, CSng(CInt(Math.Round(CDbl((Me.int_8 * 5)) / 8))) - sizeF.Height / 2!, CSng(MyBase.Width), sizeF.Height + 2!)
        If (Not ((Me.BackColor = Color.Transparent) And Me.BackgroundImage Is Nothing)) Then
            graphics_0.TextRenderingHint = TextRenderingHint.AntiAlias
        Else
            graphics_0.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
        End If
        Using stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat()
            Using solidBrush As Brush = New System.Drawing.SolidBrush(Me.ForeColor)
                stringFormat.Alignment = StringAlignment.Center
                stringFormat.LineAlignment = StringAlignment.Center
                graphics_0.DrawString(Me.Text, Me.Font, solidBrush, rectangleF, stringFormat)
            End Using
        End Using
    End Sub

    Private Sub method_2(ByVal graphics_0 As Graphics, ByVal rectangle_1 As System.Drawing.Rectangle, ByVal int_12 As Integer, ByVal int_13 As Integer)
        Dim majorTickDivisions As Integer = MyBase.MajorTickDivisions + 1
        Dim minorTickDivisions As Integer = MyBase.MinorTickDivisions
        Dim radian As Single = Me.GetRadian(Me.float_1)
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(rectangle_1.Left + 0, rectangle_1.Top + 0, rectangle_1.Width - 0, rectangle_1.Height - 0)
        Dim width As Single = CSng(rectangle.Width) / 2! - 0!
        Dim float2 As Single = Me.float_2 - Me.float_1
        Dim [single] As Single = Me.GetRadian(float2 / CSng(((majorTickDivisions - 1) * (minorTickDivisions + 1))))
        If (Not ((Me.BackColor = Color.Transparent) And Me.BackgroundImage Is Nothing)) Then
            graphics_0.TextRenderingHint = TextRenderingHint.AntiAlias
        Else
            graphics_0.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
        End If
        graphics_0.SmoothingMode = SmoothingMode.AntiAlias
        Dim minimum As Double = MyBase.Minimum
        Using pen As System.Drawing.Pen = New System.Drawing.Pen(MyBase.MajorTickColor, 2!)
            Dim num As Integer = majorTickDivisions
            For i As Integer = 0 To num Step 1
                Me.int_5 = Convert.ToInt32(CDbl(int_12) + CDbl(width) * Math.Cos(CDbl(radian)))
                Me.int_6 = Convert.ToInt32(CDbl(int_13) + CDbl(width) * Math.Sin(CDbl(radian)))
                Dim int12 As Single = CSng((CDbl(int_12) + CDbl((width - CSng(MyBase.MajorTickLength))) * Math.Cos(CDbl(radian))))
                Dim int13 As Single = CSng((CDbl(int_13) + CDbl((width - CSng(MyBase.MajorTickLength))) * Math.Sin(CDbl(radian))))
                graphics_0.DrawLine(pen, CSng(Me.int_5), CSng(Me.int_6), int12, int13)
                If (MyBase.ShowLabels) Then
                    Dim sizeF As System.Drawing.SizeF = graphics_0.MeasureString(minimum.ToString(MyBase.NumericFormat, CultureInfo.CurrentCulture), Me.Font)
                    Dim int121 As Single = CSng((CDbl(int_12) + CDbl((width - CSng(MyBase.MajorTickLength))) * Math.Cos(CDbl(radian))))
                    Dim int131 As Single = CSng((CDbl(int_13) + CDbl((width - CSng(MyBase.MajorTickLength))) * Math.Sin(CDbl(radian))))
                    Dim height As Single = Convert.ToSingle(CDbl((sizeF.Height / 2!)) * Math.Tan(CDbl(radian)))
                    Dim single1 As Single = Convert.ToSingle(Math.Tan(CDbl(radian)) * CDbl((sizeF.Height / 2!)))
                    Dim single2 As Single = Convert.ToSingle(Math.Tan(CDbl((-radian))))
                    If (Math.Abs(sizeF.Height / 2! / single2) >= Math.Abs(sizeF.Width / 2!)) Then
                        height = sizeF.Width / 2!
                        single1 = sizeF.Width / 2! * single2
                    Else
                        single1 = sizeF.Height / 2!
                        height = sizeF.Height / 2! / single2
                        If (CDbl(radian) >= 3.14159265358979 And CDbl(radian) <= 4.71238898038469 Or CDbl(radian) > 6.28318530717959) Then
                            height *= -1!
                            single1 *= -1!
                        End If
                    End If
                    If (CDbl(radian) > 4.71238898038469) Then
                        single1 *= -1!
                        height *= -1!
                    End If
                    Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl((int121 - sizeF.Width / 2! + height)))), CInt(Math.Round(CDbl((int131 - sizeF.Height / 2! - single1)))), CInt(Math.Round(CDbl((sizeF.Width + 2!)))), CInt(Math.Round(CDbl((sizeF.Height + 2!)))))
                    Using solidBrush As Brush = New System.Drawing.SolidBrush(Me.ForeColor)
                        Using stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat(StringFormatFlags.NoClip)
                            stringFormat.Alignment = StringAlignment.Center
                            stringFormat.LineAlignment = StringAlignment.Center
                            graphics_0.DrawString(minimum.ToString(MyBase.NumericFormat, CultureInfo.CurrentCulture), Me.Font, solidBrush, rectangle1, stringFormat)
                        End Using
                    End Using
                End If
                minimum += CDbl(CSng(((MyBase.Maximum - MyBase.Minimum) / CDbl((majorTickDivisions - 1)))))
                minimum = CDbl(CSng(Math.Round(minimum, 2)))
                If (i = majorTickDivisions - 1) Then
                    Exit For
                End If
                Dim num1 As Integer = minorTickDivisions
                For j As Integer = 0 To num1 Step 1
                    radian += [single]
                    Me.int_5 = CInt(Math.Round(CDbl(CSng((CDbl(int_12) + CDbl(width) * Math.Cos(CDbl(radian)))))))
                    Me.int_6 = CInt(Math.Round(CDbl(CSng((CDbl(int_13) + CDbl(width) * Math.Sin(CDbl(radian)))))))
                    int12 = CSng((CDbl(int_12) + (CDbl(width) - CDbl(MyBase.MajorTickLength) * 0.7) * Math.Cos(CDbl(radian))))
                    int13 = CSng((CDbl(int_13) + (CDbl(width) - CDbl(MyBase.MajorTickLength) * 0.7) * Math.Sin(CDbl(radian))))
                    Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(MyBase.MinorTickColor, 1!)
                        graphics_0.DrawLine(pen1, CSng(Me.int_5), CSng(Me.int_6), int12, int13)
                    End Using
                Next

            Next

        End Using
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        If (MyBase.Width < 100) Then
            MyBase.Width = 100
        End If
        If (Me.int_9 <> MyBase.Width) Then
            MyBase.Height = MyBase.Width
            Me.int_10 = MyBase.Width
        End If
        If (Me.int_10 <> MyBase.Height) Then
            MyBase.Width = MyBase.Height
            Me.int_9 = MyBase.Width
        End If
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub PaintActiveContent(ByVal graphics As System.Drawing.Graphics)
        graphics.SmoothingMode = SmoothingMode.AntiAlias
        MyBase.DrawNeedle(graphics, -225! + MyBase.ConvertValueToAngle(MyBase.Value, 270!), 0, CInt(Math.Round(CDbl((0 - MyBase.Height)) / 2)))
    End Sub
End Class
