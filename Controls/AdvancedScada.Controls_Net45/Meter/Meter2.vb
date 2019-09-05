Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class Meter2
    Inherits AnalogMeterBase
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private bitmap_4 As Bitmap

    Private float_0 As Single

    Private float_1 As Single

    Private int_2 As Integer

    Private int_3 As Integer

    Private float_2 As Single

    Private rectangle_0 As Rectangle()

    Private stringFormat_1 As StringFormat

    Private matrix_0 As Matrix

    Private decimal_0 As Decimal

    Public Sub New()
        MyBase.New()
        ReDim Me.rectangle_0(10)
        Me.matrix_0 = New Matrix()
        Me.decimal_0 = New Decimal(87, 0, 0, False, 2)
        Me.stringFormat_1 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Far
        }
        MyBase.BaseImage = My.Resources.Frame
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Me.float_2 = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.Frame.Width)))
        If (Me.float_2 > 0! And Me.float_2 <= 100!) Then
            Me.TextRectangle.Y = CInt(Math.Round(CDbl((150! * Me.float_2))))
            Me.TextRectangle.Width = MyBase.Width
            Me.TextRectangle.Height = CInt(Math.Round(CDbl((50! * Me.float_2))))
            Me.TextRectangle.X = 0
            If (Me.TextBrush Is Nothing) Then
                Me.TextBrush = New System.Drawing.SolidBrush(MyBase.ForeColor)
            End If
            Dim num As Integer = 0
            Do
                Me.rectangle_0(num) = New Rectangle()
                num = num + 1
            Loop While num <= 6
            Me.rectangle_0(0) = New Rectangle(CInt(Math.Round(CDbl((6! * Me.float_2)))), CInt(Math.Round(CDbl((171! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(1) = New Rectangle(CInt(Math.Round(CDbl((58! * Me.float_2)))), CInt(Math.Round(CDbl((124! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(2) = New Rectangle(CInt(Math.Round(CDbl((113! * Me.float_2)))), CInt(Math.Round(CDbl((87! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(3) = New Rectangle(CInt(Math.Round(CDbl((169! * Me.float_2)))), CInt(Math.Round(CDbl((62! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(4) = New Rectangle(CInt(Math.Round(CDbl((231! * Me.float_2)))), CInt(Math.Round(CDbl((44! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(5) = New Rectangle(CInt(Math.Round(CDbl((297! * Me.float_2)))), CInt(Math.Round(CDbl((37! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(6) = New Rectangle(CInt(Math.Round(CDbl((362! * Me.float_2)))), CInt(Math.Round(CDbl((44! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(7) = New Rectangle(CInt(Math.Round(CDbl((423! * Me.float_2)))), CInt(Math.Round(CDbl((62! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(8) = New Rectangle(CInt(Math.Round(CDbl((483! * Me.float_2)))), CInt(Math.Round(CDbl((87! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(9) = New Rectangle(CInt(Math.Round(CDbl((535! * Me.float_2)))), CInt(Math.Round(CDbl((124! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.rectangle_0(10) = New Rectangle(CInt(Math.Round(CDbl((584! * Me.float_2)))), CInt(Math.Round(CDbl((171! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Me.int_2 = CInt(Math.Round(CDbl(MyBase.Width) / 2 - CDbl((CSng(My.Resources.Needle.Width) * Me.float_2 / 2!))))
            Me.int_3 = CInt(Math.Round(CDbl((56! * Me.float_2))))
            Me.float_0 = CSng((CDbl(MyBase.Width) / 2))
            Me.float_1 = CSng(My.Resources.Needle.Height) * Me.float_2 + CSng(Me.int_3)
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CSng(My.Resources.MeterScaleBackGround.Width) * Me.float_2), Convert.ToInt32(CSng(My.Resources.MeterScaleBackGround.Height) * Me.float_2))
            Dim matrix0 As Graphics = Graphics.FromImage(Me.bitmap_1)
            '' Math.Abs(Me.m_Minimum) = Math.Abs(Me.m_Maximum)
            matrix0.DrawImage(My.Resources.MeterScaleBackGround, 0!, 0!, CSng(My.Resources.MeterScaleBackGround.Width) * Me.float_2, CSng(My.Resources.MeterScaleBackGround.Height) * Me.float_2)
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(CSng(My.Resources.MeterScale.Width) * Me.float_2), Convert.ToInt32(CSng(My.Resources.MeterScale.Height) * Me.float_2))
            If (Me.matrix_0 IsNot Nothing) Then
                Me.matrix_0.Reset()
            End If
            matrix0.Transform = Me.matrix_0
            matrix0.DrawImage(My.Resources.MeterScale, 40! * Me.float_2, 60! * Me.float_2, CSng(My.Resources.MeterScale.Width) * Me.float_2, CSng(My.Resources.MeterScale.Height) * Me.float_2)
            Dim num1 As Decimal = New Decimal((Me.m_Maximum - Me.m_Minimum) / 10)
            Using font As System.Drawing.Font = New System.Drawing.Font("Arial", 14! * Me.float_2, FontStyle.Regular, GraphicsUnit.Point)
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(245, 45, 45, 45))
                    Dim num2 As Integer = 0
                    Do
                        matrix0.DrawString(Conversions.ToString(Me.m_Minimum + Convert.ToDouble(Decimal.Multiply(New Decimal(num2), num1))), font, solidBrush, Me.rectangle_0(num2), Me.stringFormat_0)
                        num2 = num2 + 1
                    Loop While num2 <= 10
                    If (Math.Abs(Me.m_Minimum) = Math.Abs(Me.m_Maximum)) Then
                        matrix0.DrawString(Conversions.ToString(Me.m_Minimum + Convert.ToDouble(Decimal.Multiply(num1, New Decimal(6L)))), font, solidBrush, Me.rectangle_0(6), Me.stringFormat_0)
                    End If
                End Using
            End Using
            Dim num3 As Integer = Math.Max(Convert.ToInt32(CSng(My.Resources.Needle.Width) * Me.float_2), 1)
            Dim num4 As Integer = Math.Max(Convert.ToInt32(CSng(My.Resources.Needle.Height) * Me.float_2), 1)
            Me.bitmap_4 = New Bitmap(num3, num4)
            Graphics.FromImage(Me.bitmap_4).DrawImage(My.Resources.Needle, 0, 0, Me.bitmap_4.Width, Me.bitmap_4.Height)
            Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.method_2()
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.Frame.Width) * Me.float_2), Convert.ToInt32(CSng(My.Resources.Frame.Height) * Me.float_2))
            matrix0.Dispose()
            matrix0 = Graphics.FromImage(Me.bitmap_0)
            matrix0.DrawImage(My.Resources.Frame, 0!, 0!, CSng(My.Resources.Frame.Width) * Me.float_2, CSng(My.Resources.Frame.Height) * Me.float_2)
            matrix0.Dispose()
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.bitmap_0 IsNot Nothing) Then
                    Me.bitmap_0.Dispose()
                End If
                If (Me.bitmap_2 IsNot Nothing) Then
                    Me.bitmap_2.Dispose()
                End If
                Me.stringFormat_1.Dispose()
                Me.bitmap_3.Dispose()
                Me.matrix_0.Dispose()
                Me.bitmap_4.Dispose()
                Me.bitmap_1.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_2()
        If (Me.bitmap_2 IsNot Nothing) Then
            Me.decimal_0 = New Decimal((Me.m_Value * Me.m_ValueScaleFactor - Me.m_Minimum) * (1 / (Me.m_Maximum - Me.m_Minimum)) * -1.735 + 0.8675)
            Dim matrix0 As Graphics = Graphics.FromImage(Me.bitmap_2)
            Me.matrix_0.Reset()
            matrix0.Clear(Color.Transparent)
            Me.matrix_0.RotateAt(CSng((Convert.ToDouble(Decimal.Multiply(Decimal.Negate(Me.decimal_0), New Decimal(180L))) / 3.14159265358979)), New Point(CInt(Math.Round(CDbl(Me.float_0))), CInt(Math.Round(CDbl(Me.float_1)))))
            matrix0.Transform = Me.matrix_0
            matrix0.DrawImage(Me.bitmap_4, CSng(Me.int_2), CSng(Me.int_3), CSng(My.Resources.Needle.Width) * Me.float_2, CSng(My.Resources.Needle.Height) * Me.float_2)
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_1 Is Nothing) And Me.bitmap_2 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImageUnscaled(Me.bitmap_1, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                    Me.TextBrush.Color = MyBase.ForeColor
                End If
                graphics.DrawString(Me.Text, Me.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_1)
            End If
            graphics.DrawImageUnscaled(Me.bitmap_2, 0, 0)
            graphics.DrawImageUnscaled(Me.bitmap_0, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        Me.method_2()
    End Sub
End Class
