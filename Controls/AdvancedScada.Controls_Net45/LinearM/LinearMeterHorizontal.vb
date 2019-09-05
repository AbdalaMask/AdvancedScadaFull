Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class LinearMeterHorizontal
    Inherits AnalogMeterBase
    Public Sub New()
        MyBase.New()
        MyBase.BaseImage = My.Resources.LinearMeterBase
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Dim rectangle As System.Drawing.Rectangle
        If (MyBase.Height > 0 And MyBase.Width > 0) Then
            Me.ImageRatio = CDbl(My.Resources.LinearMeterBase.Height) / CDbl(My.Resources.LinearMeterBase.Width)
            Dim width As Single = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.LinearMeterBase.Width)))
            Dim height As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.LinearMeterBase.Height)))
            Me.ImageRatio = 1
            If (Not (Me.ImageRatio <= 0 Or MyBase.Width <= 0) And height > 0!) Then
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
                Me.StaticImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.LinearMeterBase.Width) * width), Convert.ToInt32(CSng(My.Resources.LinearMeterBase.Height) * height))
                Dim graphic As Graphics = Graphics.FromImage(Me.StaticImage)
                graphic.DrawImage(My.Resources.LinearMeterBase, 0, 0, Me.StaticImage.Width, Me.StaticImage.Height)
                Me.stringFormat_0.Alignment = StringAlignment.Center
                Me.stringFormat_0.LineAlignment = StringAlignment.Center
                Me.TextRectangle.X = 0
                Me.TextRectangle.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.04))
                Me.TextRectangle.Width = MyBase.Width
                Me.TextRectangle.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.3))
                If (Me.TextBrush Is Nothing) Then
                    Me.TextBrush = New System.Drawing.SolidBrush(MyBase.ForeColor)
                End If
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(220, 24, 24, 24))
                    Dim num As Integer = 0
                    Do
                        rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl(MyBase.Width) * 0.09 * CDbl(num) - CDbl(MyBase.Width) * 0.012)), Convert.ToInt32(CDbl(MyBase.Height) * 0.52), CInt(Math.Round(CDbl(MyBase.Width) * 0.12)), CInt(Math.Round(CDbl(MyBase.Height) * 0.2)))
                        graphic.DrawString(Conversions.ToString(Me.m_Minimum + (Me.m_Maximum - Me.m_Minimum) / 10 * CDbl(num)), New System.Drawing.Font("Arial", CSng(Convert.ToInt32(CDbl(MyBase.Height) * 0.1)), FontStyle.Bold, GraphicsUnit.Point), solidBrush, rectangle, Me.stringFormat_0)
                        num = num + 1
                    Loop While num <= 9
                    Me.stringFormat_0.Alignment = StringAlignment.Far
                    rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl(MyBase.Width) * 0.88)), Convert.ToInt32(CDbl(MyBase.Height) * 0.52), CInt(Math.Round(CDbl(MyBase.Width) * 0.1)), CInt(Math.Round(CDbl(MyBase.Height) * 0.2)))
                    graphic.DrawString(Conversions.ToString(Me.m_Minimum + (Me.m_Maximum - Me.m_Minimum) / 10 * 10), New System.Drawing.Font("Arial", CSng(Convert.ToInt32(CDbl(MyBase.Height) * 0.1)), FontStyle.Bold, GraphicsUnit.Point), solidBrush, rectangle, Me.stringFormat_0)
                    Me.stringFormat_0.Alignment = StringAlignment.Center
                End Using
                Dim num1 As Integer = Convert.ToInt32(CSng(My.Resources.LinearMeterNeedle.Width) * width)
                If (num1 <= 0) Then
                    num1 = 1
                End If
                Dim num2 As Integer = Convert.ToInt32(CSng(My.Resources.LinearMeterNeedle.Height) * height)
                If (num2 <= 0) Then
                    num2 = 1
                End If
                Me.NeedleImage = New Bitmap(num1, num2)
                graphic = Graphics.FromImage(Me.NeedleImage)
                graphic.DrawImage(My.Resources.LinearMeterNeedle, 0, 0, num1, num2)
                graphic.Dispose()
                MyBase.Invalidate()
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.StaticImage IsNot Nothing And Me.TextBrush IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImage(Me.StaticImage, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                    Me.TextBrush.Color = MyBase.ForeColor
                End If
                graphics.DrawString(MyBase.Text, MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
            End If
            If (Me.NeedleImage IsNot Nothing) Then
                graphics.DrawImage(Me.NeedleImage, Convert.ToInt32(CDbl(MyBase.Width) * ((Me.m_Value * Me.m_ValueScaleFactor - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum)) * 0.9 + CDbl(MyBase.Width) * 0.046), Convert.ToInt32(CDbl(MyBase.Height) * 0.35))
            End If
        End If
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        MyBase.Invalidate()
    End Sub
End Class
