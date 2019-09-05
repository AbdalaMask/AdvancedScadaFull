Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Public Class LinearMeterH
    Inherits LinearMeterBase
    Private rectangle_0 As Rectangle

    Private blend_0 As Blend

    Public Sub New()
        MyBase.New()
        Me.blend_0 = New Blend()
        MyBase.BackColor = Color.FromArgb(255, 255, 255)
        MyBase.ForeColor = Color.Black
        Me.rectangle_0 = New Rectangle()
        Me.blend_0.Factors = New Single() {0.5!, 0.85!, 0.2!}
        Me.blend_0.Positions = New Single() {Nothing, 0.3!, 1!}
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
        Dim sizeF As System.Drawing.SizeF
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        Dim y As Integer = 0
        If (MyBase.Width > 0 Or MyBase.Height > 0) Then
            Me.StaticImage = New Bitmap(MyBase.Width, MyBase.Height)
            If (Me.m_CenterTargetValue) Then
                Dim mTolerancePlus As Double = Me.m_TolerancePlus * 4
                If (Me.m_ToleranceMinus > Me.m_TolerancePlus) Then
                    mTolerancePlus = Me.m_ToleranceMinus * 4
                End If
                Me.m_Maximum = CDbl(CInt(Math.Floor(Me.m_TargetValue + mTolerancePlus)))
                Me.m_Minimum = CDbl(CInt(Math.Ceiling(Me.m_TargetValue - mTolerancePlus)))
                If (Me.m_Maximum = Me.m_Minimum) Then
                    Me.m_Maximum += Me.m_Minimum
                End If
            End If
            Dim graphic As Graphics = Graphics.FromImage(Me.StaticImage)
            If ((Me.BackColor = Color.Transparent) Or Me.BackgroundImage IsNot Nothing) Then
                graphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
            End If
            If (Me.m_BorderWidth > 0) Then
                BeveledButtonDisplay.Draw3DBorder(Me, graphic, Me.m_BorderColor, Me.m_BorderWidth)
            End If
            Using stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat()
                If (Not String.IsNullOrEmpty(Me.Text)) Then
                    rectangle = New System.Drawing.Rectangle(Me.m_BorderWidth * 2, Me.m_BorderWidth * 2 + 1, MyBase.Width - Me.m_BorderWidth * 4 - 1, MyBase.Height - Me.m_BorderWidth * 4 - 2)
                    stringFormat.Alignment = StringAlignment.Center
                    stringFormat.LineAlignment = StringAlignment.Near
                    graphic.DrawString(Me.Text, Me.Font, Me.TextBrush, rectangle, stringFormat)
                End If
                sizeF = graphic.MeasureString(Me.Text, Me.Font, rectangle.Width)
                If (Not (Me.m_BorderWidth > 0 And Not String.IsNullOrEmpty(Me.Text))) Then
                    sizeF.Height = 0!
                Else
                    Dim r As Byte = Me.ForeColor.R
                    Dim g As Byte = Me.ForeColor.G
                    Dim foreColor As Color = Me.ForeColor
                    graphic.DrawLine(New System.Drawing.Pen(Color.FromArgb(80, CInt(r), CInt(g), CInt(foreColor.B)), 2!), CSng((Me.m_BorderWidth * 2)), sizeF.Height + CSng((Me.m_BorderWidth * 2)) + 2!, CSng((MyBase.Width - Me.m_BorderWidth * 2)), sizeF.Height + CSng((Me.m_BorderWidth * 2)) + 2!)
                End If
            End Using
            Dim num2 As Integer = Convert.ToInt32(CDbl(MyBase.Height) * 0.12)
            Dim num3 As Integer = Convert.ToInt32(CDbl(MyBase.Height) * 0.08)
            Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.ForeColor, 2!)
                Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(Me.ForeColor, 1!)
                    Dim sizeF1 As System.Drawing.SizeF = graphic.MeasureString(Conversions.ToString(Me.m_Maximum), Me.Font)
                    Dim num4 As Integer = CInt(Math.Ceiling(CDbl((sizeF1.Width / 2!))))
                    sizeF1 = graphic.MeasureString(Conversions.ToString(Me.m_Minimum), Me.Font)
                    Dim num5 As Integer = CInt(Math.Ceiling(CDbl((sizeF1.Width / 2!))))
                    Me.BarRectangle = New System.Drawing.Rectangle() With
                    {
                        .X = Me.m_BorderWidth * 2 + num5,
                        .Y = CInt(Math.Round(CDbl((sizeF.Height + CSng(Me.Font.Height) + CSng((Me.m_BorderWidth * 2)) + CSng(num2) + 5!)))),
                        .Width = MyBase.Width - Me.m_BorderWidth * 4 - num5 - num4,
                        .Height = CInt(Math.Round(CDbl((CSng((MyBase.Height - Me.m_BorderWidth * 4)) - sizeF.Height - CSng(num2) - CSng(Me.Font.Height) - 8!))))
                    }
                    graphic.FillRectangle(New SolidBrush(Me.m_FillAreaBackcolor), Me.BarRectangle)
                    If (Me.m_ShowValidRangeMarker) Then
                        Dim num6 As Integer = Me.method_2(Math.Max(Me.m_TargetValue - Me.m_ToleranceMinus, Me.m_Minimum))
                        Dim num7 As Integer = Me.method_2(Math.Min(Me.m_TargetValue + Me.m_TolerancePlus, Me.m_Maximum)) - num6
                        If (num7 < 5) Then
                            num7 = 5
                        End If
                        If (Math.Abs(num7) > 0) Then
                            graphic.FillRectangle(New SolidBrush(ControlPaint.Dark(Me.m_FillColorInRange, 0.1!)), num6, Me.BarRectangle.Y - 2 - num3, num7, num3)
                        End If
                    End If
                    graphic.DrawLine(pen, New Point(Me.BarRectangle.X + 1, Me.BarRectangle.Y - 2), New Point(Me.BarRectangle.X + 1, Me.BarRectangle.Y - 2 - num2))
                    Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.BarRectangle.X - num5 - 1, Me.BarRectangle.Y - Me.Font.Height - num2 - 4, num5 * 2 + 2, Me.Font.Height + 4)
                    Me.stringFormat_0.LineAlignment = StringAlignment.Center
                    Me.stringFormat_0.Alignment = StringAlignment.Center
                    graphic.DrawString(Conversions.ToString(Me.m_Minimum), Me.Font, Me.TextBrush, rectangle1, Me.stringFormat_0)
                    If (Me.m_MajorDivisions > 0) Then
                        While num < Me.m_MajorDivisions
                            num1 = CInt(Math.Round(CDbl(Me.BarRectangle.X) + CDbl(Me.BarRectangle.Width) / CDbl(Me.m_MajorDivisions) * CDbl((num + 1)) - 1))
                            y = Me.BarRectangle.Y - 2
                            Dim num8 As Integer = y - num2
                            graphic.DrawLine(pen, New Point(num1, y), New Point(num1, num8))
                            Dim str As String = Conversions.ToString(Me.m_Minimum + CDbl((num + 1)) * ((Me.m_Maximum - Me.m_Minimum) / CDbl(Me.m_MajorDivisions)))
                            sizeF1 = graphic.MeasureString(str, Me.Font)
                            Dim num9 As Integer = CInt(Math.Ceiling(CDbl((sizeF1.Width / 2!))))
                            rectangle1 = New System.Drawing.Rectangle(num1 - num9, num8 - Me.Font.Height - 2, num9 * 2, Me.Font.Height + 2)
                            graphic.DrawString(str, Me.Font, Me.TextBrush, rectangle1, Me.stringFormat_0)
                            If (Me.m_MinorDivisions > 1) Then
                                Dim num10 As Integer = 0
                                While num10 < Me.m_MinorDivisions - 1
                                    Dim num11 As Integer = CInt(Math.Round(CDbl(Me.BarRectangle.X) + CDbl(Me.BarRectangle.Width) / CDbl((Me.m_MajorDivisions * Me.m_MinorDivisions)) * CDbl((num10 + 1)) + CDbl(Me.BarRectangle.Width) / CDbl(Me.m_MajorDivisions) * CDbl(num) - 1))
                                    graphic.DrawLine(pen1, New Point(num11, Me.BarRectangle.Y - 2), New Point(num11, Me.BarRectangle.Y - 2 - num3))
                                    num10 = num10 + 1
                                End While
                            End If
                            num = num + 1
                        End While
                        graphic.DrawLine(pen1, Convert.ToInt32(Me.BarRectangle.X), y, num1, y)
                    End If
                End Using
            End Using
            MyBase.Invalidate()
        End If
    End Sub

    Private Function method_2(ByVal double_0 As Double) As Integer
        Dim mMaximum As Double = Me.m_Maximum - Me.m_Minimum
        If (mMaximum = 0) Then
            mMaximum = 1
        End If
        Dim double0 As Double = (double_0 - Me.m_Minimum) / mMaximum
        Dim num As Integer = CInt(Math.Round(CDbl(Me.BarRectangle.Width) * double0))
        Return Me.BarRectangle.X + num
    End Function

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim blend0 As LinearGradientBrush
        Dim str As String
        MyBase.OnPaint(painte)
        Dim graphics As System.Drawing.Graphics = painte.Graphics
        Dim num As Double = Math.Max(Math.Min(Me.Value, Me.m_Maximum / Me.m_ValueScaleFactor), Me.m_Minimum / Me.m_ValueScaleFactor)
        Me.rectangle_0.Height = Me.BarRectangle.Height - 2
        Me.rectangle_0.Y = Me.BarRectangle.Y + 1
        Dim num1 As Integer = CInt(Math.Round(Math.Max(Me.m_Maximum - Me.m_Minimum, 1)))
        Me.rectangle_0.Width = Convert.ToInt32((num * Me.m_ValueScaleFactor - Me.m_Minimum) / CDbl(num1) * CDbl((Me.BarRectangle.Width - 2)))
        Dim x As Integer = Me.BarRectangle.X + Me.rectangle_0.Width + 1
        If (Me.m_FillType = LinearMeterBase.FillTypeOption.WideBand) Then
            Me.rectangle_0.Width = Math.Max(2, CInt(Math.Round(CDbl(MyBase.Width) * 0.03)) * 2)
            Me.rectangle_0.X = CInt(Math.Round(CDbl(x) - CDbl(Me.rectangle_0.Width) / 2))
        ElseIf (Me.m_FillType <> FillTypeOption.NarrowBand) Then
            Me.rectangle_0.X = Me.BarRectangle.X + 1
        Else
            Me.rectangle_0.Width = 4
            Me.rectangle_0.X = x - 2
        End If
        If (Me.rectangle_0.Width > 0 And Me.rectangle_0.Height > 0) Then
            blend0 = If(Not (Me.m_Value * Me.m_ValueScaleFactor >= Me.m_TargetValue - Me.m_ToleranceMinus And Me.m_Value * Me.m_ValueScaleFactor <= Me.m_TargetValue + Me.m_TolerancePlus), New LinearGradientBrush(Me.rectangle_0, BeveledButtonDisplay.GetRelativeColor(Me.m_FillColor, 0.5), BeveledButtonDisplay.GetRelativeColor(Me.m_FillColor, 2), 90!, False), New LinearGradientBrush(Me.rectangle_0, BeveledButtonDisplay.GetRelativeColor(Me.m_FillColorInRange, 0.5), BeveledButtonDisplay.GetRelativeColor(Me.m_FillColorInRange, 1.5), 90!, False))
            blend0.Blend = Me.blend_0
            graphics.FillRectangle(blend0, Me.rectangle_0)
        End If
        If (Me.m_FillType <> FillTypeOption.Fill) Then
            graphics.DrawLine(New Pen(Color.Black, 2!), x, Me.rectangle_0.Y, x, Me.rectangle_0.Y + Me.rectangle_0.Height - 1)
        End If
        If (Me.m_ShowValue) Then
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            If (String.IsNullOrEmpty(Me.m_NumericFormat)) Then
                str = Conversions.ToString(Me.m_Value)
            Else
                Try
                    str = Me.m_Value.ToString(Me.m_NumericFormat)
                Catch exception As System.Exception
                    ProjectData.SetProjectError(exception)
                    str = "NumericFormat Invalid"
                    ProjectData.ClearProjectError()
                End Try
            End If
            If (Me.m_FillType <> FillTypeOption.Fill) Then
                Me.TextRectangle.X = Me.rectangle_0.X + Me.rectangle_0.Width + 1
                Me.TextRectangle.Y = Me.BarRectangle.Y
                Me.TextRectangle.Height = Me.BarRectangle.Height
                Dim sizeF As System.Drawing.SizeF = graphics.MeasureString(str, Me.Font)
                Me.TextRectangle.Width = CInt(Math.Round(CDbl((sizeF.Width + 2!))))
                If (Me.TextRectangle.X + Me.TextRectangle.Width > Me.BarRectangle.X + Me.BarRectangle.Width And CDbl((Me.TextRectangle.X + Me.TextRectangle.Width)) <= CDbl((Me.BarRectangle.X + Me.BarRectangle.Width)) + CDbl(Me.rectangle_0.Width) / 2 + 1) Then
                    Me.TextRectangle.X = Me.BarRectangle.X + Me.BarRectangle.Width - Me.TextRectangle.Width
                End If
                If (CDbl((Me.TextRectangle.X + Me.TextRectangle.Width)) > CDbl((Me.BarRectangle.X + Me.BarRectangle.Width)) + CDbl(Me.rectangle_0.Width) / 2 + 1) Then
                    Me.TextRectangle.X = Me.rectangle_0.X - Me.TextRectangle.Width - 1
                End If
                Me.stringFormat_0.LineAlignment = StringAlignment.Center
                Me.stringFormat_0.Alignment = StringAlignment.Center
                graphics.DrawString(str, Me.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
            ElseIf (CSng(Me.rectangle_0.Width) <= graphics.MeasureString(str, Me.Font).Width) Then
                Me.stringFormat_0.Alignment = StringAlignment.Near
                graphics.DrawString(str, Me.Font, Me.TextBrush, Me.BarRectangle, Me.stringFormat_0)
            Else
                Me.stringFormat_0.Alignment = StringAlignment.Far
                graphics.DrawString(str, Me.Font, Me.TextBrush, Me.rectangle_0, Me.stringFormat_0)
            End If
        End If
    End Sub
End Class
