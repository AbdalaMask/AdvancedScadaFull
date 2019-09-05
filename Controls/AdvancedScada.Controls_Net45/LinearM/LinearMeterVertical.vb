Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class LinearMeterVertical
    Inherits AnalogMeterBase
    Public Sub New()
        MyBase.New()
        MyBase.BaseImage = My.Resources.LinearMeterVertical
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Me.ImageRatio = CDbl(My.Resources.LinearMeterVertical.Height) / CDbl(My.Resources.LinearMeterVertical.Width)
        Dim width As Single = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.LinearMeterVertical.Width)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.LinearMeterVertical.Height)))
        If (Not (Me.ImageRatio <= 0 Or width <= 0!) And height > 0!) Then
            If (Me.StaticImage IsNot Nothing) Then
                Me.StaticImage.Dispose()
            End If
            Me.StaticImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.LinearMeterVertical.Width) * width), Convert.ToInt32(CSng(My.Resources.LinearMeterVertical.Height) * height))
            Dim graphic As Graphics = Graphics.FromImage(Me.StaticImage)
            graphic.DrawImage(My.Resources.LinearMeterVertical, 0, 0, Me.StaticImage.Width, Me.StaticImage.Height)
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            Me.TextRectangle.X = 0
            Me.TextRectangle.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.005))
            Me.TextRectangle.Width = MyBase.Width
            Me.TextRectangle.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.1))
            If (Me.TextBrush Is Nothing) Then
                Me.TextBrush = New System.Drawing.SolidBrush(MyBase.ForeColor)
            End If
            Dim solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(220, 24, 24, 24))
            Me.stringFormat_0.Alignment = StringAlignment.Near
            Dim num As Integer = 0
            Do
                Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl(MyBase.Width) * 0.52)), Convert.ToInt32(CDbl(MyBase.Height) - CDbl(MyBase.Height) * 0.0794 * CDbl(num) - CDbl(MyBase.Height) * 0.105), CInt(Math.Round(CDbl(MyBase.Width) * 0.3)), CInt(Math.Round(CDbl(MyBase.Height) * 0.07)))
                graphic.DrawString(Conversions.ToString(Me.m_Minimum + (Me.m_Maximum - Me.m_Minimum) / 10 * CDbl(num)), New System.Drawing.Font("Arial", CSng(Convert.ToInt32(CDbl(MyBase.Height) * 0.024)), FontStyle.Bold, GraphicsUnit.Point), solidBrush, rectangle, Me.stringFormat_0)
                num = num + 1
            Loop While num <= 10
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Dim num1 As Integer = Convert.ToInt32(CSng(My.Resources.LinearMeterVerticalNeedle.Width) * width)
            If (num1 <= 0) Then
                num1 = 1
            End If
            Dim num2 As Integer = Convert.ToInt32(CSng(My.Resources.LinearMeterVerticalNeedle.Height) * height)
            If (num2 <= 0) Then
                num2 = 1
            End If
            Me.NeedleImage = New Bitmap(num1, num2)
            graphic = Graphics.FromImage(Me.NeedleImage)
            graphic.DrawImage(My.Resources.LinearMeterVerticalNeedle, 0, 0, num1, num2)
            graphic.Dispose()
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.StaticImage Is Nothing Or Me.TextBrush Is Nothing) And Me.stringFormat_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImage(Me.StaticImage, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                    Me.TextBrush.Color = MyBase.ForeColor
                End If
                graphics.DrawString(MyBase.Text, MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
            End If
            graphics.DrawImage(Me.NeedleImage, Convert.ToInt32(CDbl(MyBase.Width) * 0.32), Convert.ToInt32(CDbl(MyBase.Height) - CDbl(MyBase.Height) * ((Me.m_Value * Me.m_ValueScaleFactor - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum)) * 0.794 - CDbl(MyBase.Height) * 0.072))
        End If
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        MyBase.Invalidate()
    End Sub
End Class
