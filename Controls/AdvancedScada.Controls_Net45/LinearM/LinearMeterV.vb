Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Namespace LinearM
    Public Class LinearMeterV
        Inherits LinearMeterBase
        ' Methods
        Public Sub New()
            MyBase.BackColor = Color.FromArgb(&HFF, &HFF, &HFF)
            MyBase.ForeColor = Color.Black
            Me.rectangle_0 = New Rectangle
            Me.blend_0.Factors = New Single() {0.5!, 0.85!, 0.2!}
            Dim singleArray1 As Single() = New Single(3 - 1) {}
            singleArray1(1) = 0.3!
            singleArray1(2) = 1!
            Me.blend_0.Positions = singleArray1
        End Sub

        Protected Overrides Sub CreateStaticImage()
            If Not ((MyBase.Width <= 0) And (MyBase.Height <= 0)) Then
                Try
                    Dim rectangle As Rectangle
                    If ((MyBase.StaticImage IsNot Nothing) AndAlso ((MyBase.StaticImage.Width <> MyBase.Width) Or (MyBase.StaticImage.Height <> MyBase.Height))) Then
                        MyBase.StaticImage.Dispose()
                        MyBase.StaticImage = Nothing
                    End If
                    If (MyBase.StaticImage Is Nothing) Then
                        MyBase.StaticImage = New Bitmap(MyBase.Width, MyBase.Height)
                    End If

                    If MyBase.m_CenterTargetValue Then
                        Dim num5 As Double = (MyBase.m_TolerancePlus * 4)
                        If (MyBase.m_ToleranceMinus > MyBase.m_TolerancePlus) Then
                            num5 = (MyBase.m_ToleranceMinus * 4)
                        End If
                        MyBase.m_Maximum = (MyBase.m_TargetValue + num5)
                        MyBase.m_Minimum = (MyBase.m_TargetValue - num5)
                        If (MyBase.m_Maximum = MyBase.m_Minimum) Then
                            Me.m_Maximum += Me.m_Minimum
                        End If
                    End If
                    Dim graphics As Graphics = Graphics.FromImage(MyBase.StaticImage)
                    If ((Me.BackColor = Color.Transparent) And (Me.BackgroundImage IsNot Nothing)) Then
                        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                    Else
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias
                    End If
                    graphics.Clear(Me.BackColor)
                    If (MyBase.m_BorderWidth > 0) Then
                        BeveledButtonDisplay.Draw3DBorder(Me, graphics, MyBase.m_BorderColor, MyBase.m_BorderWidth)
                    End If
                    Dim format As New StringFormat
                    If Not String.IsNullOrEmpty(Me.Text) Then
                        rectangle = New Rectangle((MyBase.m_BorderWidth * 2), ((MyBase.m_BorderWidth * 2) + 1), ((MyBase.Width - (MyBase.m_BorderWidth * 4)) - 1), ((MyBase.Height - (MyBase.m_BorderWidth * 4)) - 2))
                        format.Alignment = StringAlignment.Center
                        format.LineAlignment = StringAlignment.Near
                        graphics.DrawString(Me.Text, Me.Font, MyBase.TextBrush, rectangle, format)
                    End If
                    Dim ef As SizeF = graphics.MeasureString(Me.Text, Me.Font, rectangle.Width)
                    If ((MyBase.m_BorderWidth > 0) And Not String.IsNullOrEmpty(Me.Text)) Then
                        graphics.DrawLine(New Pen(Color.FromArgb(80, Me.ForeColor.R, Me.ForeColor.G, Me.ForeColor.B), 2!), CSng((MyBase.m_BorderWidth * 2)), ((ef.Height + (MyBase.m_BorderWidth * 2)) + 2!), CSng((MyBase.Width - (MyBase.m_BorderWidth * 2))), ((ef.Height + (MyBase.m_BorderWidth * 2)) + 2!))
                    End If
                    Dim num As Integer = Convert.ToInt32(CDbl((MyBase.Width * 0.12)))
                    Dim width As Integer = Convert.ToInt32(CDbl((MyBase.Width * 0.08)))
                    Dim pen As New Pen(Me.ForeColor, 2!)
                    Dim pen2 As New Pen(Me.ForeColor, 1!)
                    Dim ef2 As SizeF = graphics.MeasureString(Conversions.ToString(MyBase.m_Maximum), Me.Font, rectangle.Width)
                    Dim ef3 As SizeF = graphics.MeasureString(Conversions.ToString(MyBase.m_Minimum), Me.Font, rectangle.Width)
                    Dim num3 As Integer = CInt(Math.Ceiling(CDbl(Math.Max(ef2.Width, ef3.Width))))
                    Dim x As Integer = ((MyBase.m_BorderWidth * 2) + 2)
                    MyBase.BarRectangle = New Rectangle(x, CInt(Math.Round(CDbl(((ef.Height + Math.Ceiling(CDbl((ef2.Height / 2!)))) + (MyBase.m_BorderWidth * 2))))), (((((MyBase.Width - num) - num3) - x) - (MyBase.BorderWidth * 2)) - 4), CInt(Math.Round(CDbl((((MyBase.Height - (MyBase.m_BorderWidth * 4)) - ef.Height) - Math.Ceiling(CDbl(ef2.Height)))))))
                    graphics.FillRectangle(New SolidBrush(MyBase.m_FillAreaBackcolor), MyBase.BarRectangle)
                    If MyBase.m_ShowValidRangeMarker Then
                        Dim targetValue As Double = MyBase.m_TargetValue
                        If MyBase.m_ScaleTargetValue Then
                            targetValue = (MyBase.m_TargetValue * MyBase.m_ValueScaleFactor)
                        End If
                        Dim height As Integer = (Me.method_2(Math.Max((targetValue - MyBase.m_ToleranceMinus), MyBase.m_Minimum)) - Me.method_2(Math.Min((targetValue + MyBase.m_TolerancePlus), MyBase.m_Maximum)))
                        If (height > 0) Then
                            graphics.FillRectangle(New SolidBrush(BeveledButtonDisplay.GetRelativeColor(MyBase.m_FillColorInRange, 0.7)), ((Me.BarRectangle.X + Me.BarRectangle.Width) + 2), Me.method_2(Math.Min((targetValue + MyBase.m_TolerancePlus), MyBase.m_Maximum)), width, height)
                        End If
                    End If
                    graphics.DrawLine(pen, New Point(((Me.BarRectangle.X + Me.BarRectangle.Width) + 2), Me.BarRectangle.Y), New Point(((Me.BarRectangle.X + Me.BarRectangle.Width) + num), Me.BarRectangle.Y))
                    Dim layoutRectangle As New Rectangle((((Me.BarRectangle.X + Me.BarRectangle.Width) + num) + 2), (Me.BarRectangle.Y - Me.Font.Height), MyBase.Width, (Me.Font.Height * 2))
                    format.LineAlignment = StringAlignment.Center
                    format.Alignment = StringAlignment.Near
                    graphics.DrawString(Conversions.ToString(MyBase.m_Maximum), Me.Font, MyBase.TextBrush, layoutRectangle, format)
                    If (MyBase.m_MajorDivisions > 0) Then
                        Dim num8 As Integer
                        Dim num10 As Integer
                        Dim num12 As Integer
                        Do While (num8 < MyBase.m_MajorDivisions)
                            num10 = CInt(Math.Round(CDbl(((Me.BarRectangle.Y + ((CDbl(Me.BarRectangle.Height) / CDbl(MyBase.m_MajorDivisions)) * (num8 + 1))) - 1))))
                            num12 = ((Me.BarRectangle.X + Me.BarRectangle.Width) + 2)
                            Dim num13 As Integer = ((Me.BarRectangle.X + Me.BarRectangle.Width) + num)
                            graphics.DrawLine(pen, New Point(num12, num10), New Point(num13, num10))
                            layoutRectangle = New Rectangle((num13 + 2), (num10 - Me.Font.Height), (MyBase.Width - num13), (Me.Font.Height * 2))
                            graphics.DrawString(Conversions.ToString(CDbl((MyBase.m_Maximum - ((num8 + 1) * ((MyBase.m_Maximum - MyBase.m_Minimum) / CDbl(MyBase.m_MajorDivisions)))))), Me.Font, MyBase.TextBrush, layoutRectangle, format)
                            If (MyBase.m_MinorDivisions > 1) Then
                                Dim i As Integer
                                For i = 0 To (MyBase.m_MinorDivisions - 1) - 1
                                    Dim y As Integer = CInt(Math.Round(CDbl((((Me.BarRectangle.Y + ((CDbl(Me.BarRectangle.Height) / CDbl((MyBase.m_MajorDivisions * MyBase.m_MinorDivisions))) * (i + 1))) + ((CDbl(Me.BarRectangle.Height) / CDbl(MyBase.m_MajorDivisions)) * num8)) - 1))))
                                    graphics.DrawLine(pen2, New Point(((Me.BarRectangle.X + Me.BarRectangle.Width) + 2), y), New Point(((Me.BarRectangle.X + Me.BarRectangle.Width) + width), y))
                                Next i
                            End If
                            num8 += 1
                        Loop
                        graphics.DrawLine(pen2, num12, Convert.ToInt32(Me.BarRectangle.Y), num12, num10)
                    End If
                    MyBase.Invalidate
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    ProjectData.ClearProjectError
                End Try
            End If
        End Sub

        Private Function method_2(ByVal double_0 As Double) As Integer
            Dim num2 As Double = (MyBase.m_Maximum - MyBase.m_Minimum)
            Dim num3 As Double = ((double_0 - MyBase.m_Minimum) / num2)
            Dim num4 As Integer = CInt(Math.Round(CDbl((Me.BarRectangle.Height * num3))))
            Return ((Me.BarRectangle.Y + Me.BarRectangle.Height) - num4)
        End Function

        Protected Overrides Sub OnPaint(ByVal paintEventArgs_0 As PaintEventArgs)
            MyBase.OnPaint(paintEventArgs_0)
            Dim graphics As Graphics = paintEventArgs_0.Graphics
            graphics.SmoothingMode = SmoothingMode.None
            Dim num As Double = Math.Max(Math.Min(Me.Value, (MyBase.m_Maximum / MyBase.m_ValueScaleFactor)), (MyBase.m_Minimum / MyBase.m_ValueScaleFactor))
            Me.rectangle_0.Width = (Me.BarRectangle.Width - 2)
            Me.rectangle_0.X = (Me.BarRectangle.X + 1)
            Me.rectangle_0.Height = Convert.ToInt32(CDbl(((((num * MyBase.m_ValueScaleFactor) - MyBase.m_Minimum) / (MyBase.m_Maximum - MyBase.m_Minimum)) * (Me.BarRectangle.Height - 2))))
            Dim num2 As Integer = (((((Me.BarRectangle.Y + 1) + Me.BarRectangle.Height) - 1) - Me.rectangle_0.Height) - 1)
            If (MyBase.m_FillType = FillTypeOption.WideBand) Then
                Me.rectangle_0.Height = Math.Max(2, (CInt(Math.Round(CDbl((MyBase.Height * 0.03)))) * 2))
                Me.rectangle_0.Y = CInt(Math.Round(CDbl((num2 - (CDbl(Me.rectangle_0.Height) / 2)))))
            ElseIf (MyBase.m_FillType = FillTypeOption.NarrowBand) Then
                Me.rectangle_0.Height = 4
                Me.rectangle_0.Y = (num2 - 2)
            Else
                Me.rectangle_0.Y = num2
            End If
            If ((Me.rectangle_0.Height > 0) And (Me.rectangle_0.Width > 0)) Then
                Dim brush As LinearGradientBrush
                Dim targetValue As Double = MyBase.m_TargetValue
                If MyBase.m_ScaleTargetValue Then
                    targetValue = (MyBase.m_TargetValue * MyBase.m_ValueScaleFactor)
                End If
                If (((MyBase.m_Value * MyBase.m_ValueScaleFactor) >= (targetValue - MyBase.m_ToleranceMinus)) And ((MyBase.m_Value * MyBase.m_ValueScaleFactor) <= (targetValue + MyBase.m_TolerancePlus))) Then
                    brush = New LinearGradientBrush(Me.rectangle_0, BeveledButtonDisplay.GetRelativeColor(MyBase.m_FillColorInRange, 0.5), BeveledButtonDisplay.GetRelativeColor(MyBase.m_FillColorInRange, 1.5), 0!, False)
                Else
                    brush = New LinearGradientBrush(Me.rectangle_0, BeveledButtonDisplay.GetRelativeColor(MyBase.m_FillColor, 0.5), BeveledButtonDisplay.GetRelativeColor(MyBase.m_FillColor, 2), 0!, False)
                End If
                brush.Blend = Me.blend_0
                graphics.FillRectangle(brush, Me.rectangle_0)
            End If
            If (MyBase.m_FillType > FillTypeOption.Fill) Then
                graphics.DrawLine(New Pen(Color.Black, 2!), Me.rectangle_0.X, num2, ((Me.rectangle_0.X + Me.rectangle_0.Width) - 1), num2)
            End If
            If MyBase.m_ShowValue Then
                Dim str As String
                MyBase.stringFormat_0.Alignment = StringAlignment.Center
                If Not String.IsNullOrEmpty(MyBase.m_NumericFormat) Then
                    Try
                        str = Me.m_Value.ToString(MyBase.m_NumericFormat)
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
                        str = "NumericFormat Invalid"
                        ProjectData.ClearProjectError
                    End Try
                Else
                    str = Conversions.ToString(MyBase.m_Value)
                End If
                If (MyBase.m_FillType = FillTypeOption.Fill) Then
                    If (Me.rectangle_0.Height > Me.Font.Height) Then
                        MyBase.stringFormat_0.LineAlignment = StringAlignment.Near
                        graphics.DrawString(str, Me.Font, MyBase.TextBrush, Me.rectangle_0, MyBase.stringFormat_0)
                    Else
                        MyBase.stringFormat_0.LineAlignment = StringAlignment.Far
                        graphics.DrawString(str, Me.Font, MyBase.TextBrush, MyBase.BarRectangle, MyBase.stringFormat_0)
                    End If
                Else
                    Me.TextRectangle.X = (Me.BarRectangle.X + 1)
                    Me.TextRectangle.Y = ((Me.rectangle_0.Y - Me.Font.Height) - 2)
                    Me.TextRectangle.Height = (Me.Font.Height + 2)
                    Me.TextRectangle.Width = Me.BarRectangle.Width
                    If ((Me.TextRectangle.Y < Me.BarRectangle.Y) And (Me.TextRectangle.Y >= (Me.BarRectangle.Y - 5))) Then
                        Me.TextRectangle.Y = Me.BarRectangle.Y
                    End If
                    If (Me.TextRectangle.Y < (Me.BarRectangle.Y - 5)) Then
                        Me.TextRectangle.Y = ((Me.rectangle_0.Y + Me.rectangle_0.Height) + 1)
                    End If
                    MyBase.stringFormat_0.LineAlignment = StringAlignment.Center
                    MyBase.stringFormat_0.Alignment = StringAlignment.Center
                    graphics.DrawString(str, Me.Font, MyBase.TextBrush, MyBase.TextRectangle, MyBase.stringFormat_0)
                End If
            End If
        End Sub


        ' Fields
        Private rectangle_0 As Rectangle
        Private blend_0 As Blend = New Blend
    End Class
End Namespace

