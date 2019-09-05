Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class Meter
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private float_0 As Single

    Private float_1 As Single

    Private int_0 As Integer

    Private int_1 As Integer

    Private bool_0 As Boolean

    Private float_2 As Single

    Private rectangle_0 As Rectangle

    Private rectangle_1 As Rectangle()

    Private stringFormat_0 As StringFormat

    Private stringFormat_1 As StringFormat

    Private double_0 As Double

    Private decimal_0 As Decimal

    Private decimal_1 As Decimal

    Private decimal_2 As Decimal

    Private decimal_3 As Decimal

    Private matrix_0 As Matrix

    Private bitmap_3 As Bitmap

    Private solidBrush_0 As SolidBrush

    Private int_2 As Integer

    Private int_3 As Integer

    Public Property MaxValue As Decimal
        Get
            Return Me.decimal_2
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_2 = value
            Me.Value = Me.double_0
            Me.method_0()
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property MinValue As Decimal
        Get
            Return Me.decimal_3
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_3 = value
            If (Decimal.Compare(Me.decimal_3, Me.decimal_2) >= 0) Then
                Me.decimal_2 = Decimal.Add(Me.decimal_3, Decimal.One)
            End If
            Me.MaxValue = Me.decimal_2
            Me.Value = Me.double_0
            Me.method_0()
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_0) Then
                Me.double_0 = Math.Max(Math.Min(value, Convert.ToDouble(Decimal.Divide(Me.decimal_2, Me.decimal_1))), Convert.ToDouble(Decimal.Divide(Me.decimal_3, Me.decimal_1)))
                Me.method_0()
                MyBase.Invalidate(New Rectangle(0, 0, CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.85)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.58))))
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.decimal_1
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_1 = value
            value = New Decimal(Me.double_0)
            Me.method_0()
            MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.12)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.14)), CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.76)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.4))))
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        ReDim Me.rectangle_1(6)
        Me.stringFormat_0 = New StringFormat()
        Me.stringFormat_1 = New StringFormat()
        Me.decimal_0 = New Decimal(6125, 0, 0, False, 4)
        Me.decimal_1 = Decimal.One
        Me.decimal_2 = New Decimal(100L)
        Me.decimal_3 = New Decimal()
        Me.matrix_0 = New Matrix()
        Me.solidBrush_0 = New SolidBrush(Color.Black)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.solidBrush_0 IsNot Nothing) Then
                    Me.solidBrush_0.Dispose()
                End If
                If (Me.bitmap_3 IsNot Nothing) Then
                    Me.bitmap_3.Dispose()
                End If
                If (Me.bitmap_0 IsNot Nothing) Then
                    Me.bitmap_0.Dispose()
                End If
                If (Me.bitmap_2 IsNot Nothing) Then
                    Me.bitmap_2.Dispose()
                End If
                If (Me.bitmap_1 IsNot Nothing) Then
                    Me.bitmap_1.Dispose()
                End If
                Me.stringFormat_0.Dispose()
                Me.stringFormat_1.Dispose()
                Me.matrix_0.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        If (Not Me.bool_0) Then
            Me.decimal_0 = New Decimal((Me.double_0 * Convert.ToDouble(Me.decimal_1) - Convert.ToDouble(Me.decimal_3)) * (1.25 / Convert.ToDouble(Decimal.Subtract(Me.decimal_2, Me.decimal_3))) * -1 + 0.625)
        Else
            Me.decimal_0 = New Decimal((Me.double_0 * Convert.ToDouble(Me.decimal_1) - Convert.ToDouble(Me.decimal_3)) * (1.5 / Convert.ToDouble(Decimal.Subtract(Me.decimal_2, Me.decimal_3))) * -1 + 0.75)
        End If
    End Sub

    Private Sub method_1()
        Me.float_2 = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.MeterNoNeedle2.Width)))
        If (Me.float_2 > 0!) Then
            Me.float_0 = CSng((CDbl(MyBase.Width) / 2))
            Me.float_1 = CSng((CDbl(MyBase.Height) * 0.886))
            Me.int_0 = CInt(Math.Round(CDbl(MyBase.Width) / 2 - 2))
            Me.int_1 = CInt(Math.Round(CDbl((45! * Me.float_2))))
            Me.rectangle_0.Y = CInt(Math.Round(CDbl((75! * Me.float_2))))
            Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.6))
            Me.rectangle_0.Height = CInt(Math.Round(CDbl((50! * Me.float_2))))
            Me.rectangle_0.X = CInt(Math.Round(CDbl((MyBase.Width - Me.rectangle_0.Width)) / 2))
            Me.stringFormat_1.Alignment = StringAlignment.Center
            Me.stringFormat_1.LineAlignment = StringAlignment.Far
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New System.Drawing.SolidBrush(MyBase.ForeColor)
            End If
            Dim num As Integer = 0
            Do
                Me.rectangle_1(num) = New Rectangle()
                num = num + 1
            Loop While num <= 6
            If (Not Me.bool_0) Then
                Me.rectangle_1(0) = New Rectangle(CInt(Math.Round(CDbl((30! * Me.float_2)))), CInt(Math.Round(CDbl((73! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(1) = New Rectangle(CInt(Math.Round(CDbl((75! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(2) = New Rectangle(CInt(Math.Round(CDbl((120! * Me.float_2)))), CInt(Math.Round(CDbl((38! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(3) = New Rectangle(CInt(Math.Round(CDbl((170! * Me.float_2)))), CInt(Math.Round(CDbl((38! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(4) = New Rectangle(CInt(Math.Round(CDbl((215! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(5) = New Rectangle(CInt(Math.Round(CDbl((255! * Me.float_2)))), CInt(Math.Round(CDbl((73! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            Else
                Me.rectangle_1(0) = New Rectangle(0, CInt(Math.Round(CDbl((85! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(1) = New Rectangle(CInt(Math.Round(CDbl((40! * Me.float_2)))), CInt(Math.Round(CDbl((60! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(2) = New Rectangle(CInt(Math.Round(CDbl((80! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(3) = New Rectangle(CInt(Math.Round(CDbl((125! * Me.float_2)))), CInt(Math.Round(CDbl((38! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(4) = New Rectangle(CInt(Math.Round(CDbl((170! * Me.float_2)))), CInt(Math.Round(CDbl((42! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(5) = New Rectangle(CInt(Math.Round(CDbl((210! * Me.float_2)))), CInt(Math.Round(CDbl((60! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
                Me.rectangle_1(6) = New Rectangle(CInt(Math.Round(CDbl((248! * Me.float_2)))), CInt(Math.Round(CDbl((85! * Me.float_2)))), CInt(Math.Round(CDbl((50! * Me.float_2)))), CInt(Math.Round(CDbl((20! * Me.float_2)))))
            End If
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.MeterNoNeedle2.Width) * Me.float_2), Convert.ToInt32(CSng(My.Resources.MeterNoNeedle2.Height) * Me.float_2))
            Dim matrix0 As Graphics = Graphics.FromImage(Me.bitmap_0)
            If (Decimal.Compare(Math.Abs(Me.decimal_3), Math.Abs(Me.decimal_2)) <> 0) Then
                matrix0.DrawImage(My.Resources.MeterNoNeedle2, 0!, 0!, CSng(My.Resources.MeterNoNeedle2.Width) * Me.float_2, CSng(My.Resources.MeterNoNeedle2.Height) * Me.float_2)
                Me.bool_0 = False
            Else
                matrix0.DrawImage(My.Resources.MeterNoNeedleCenterScale, 0!, 0!, CSng(My.Resources.MeterNoNeedle2.Width) * Me.float_2, CSng(My.Resources.MeterNoNeedle2.Height) * Me.float_2)
                Me.bool_0 = True
            End If
            Dim num1 As Decimal = Decimal.Divide(Decimal.Subtract(Me.decimal_2, Me.decimal_3), New Decimal(5L))
            If (Me.bool_0) Then
                num1 = Decimal.Divide(Decimal.Subtract(Me.decimal_2, Me.decimal_3), New Decimal(6L))
            End If
            Using font As System.Drawing.Font = New System.Drawing.Font("Arial", 11! * Me.float_2, FontStyle.Regular, GraphicsUnit.Point)
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(245, 45, 45, 45))
                    matrix0.DrawString(Conversions.ToString(Me.decimal_3), font, solidBrush, Me.rectangle_1(0), Me.stringFormat_0)
                    matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, num1)), font, solidBrush, Me.rectangle_1(1), Me.stringFormat_0)
                    matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, Decimal.Multiply(num1, New Decimal(2L)))), font, solidBrush, Me.rectangle_1(2), Me.stringFormat_0)
                    If (Me.bool_0) Then
                        matrix0.DrawString("0", font, solidBrush, Me.rectangle_1(3), Me.stringFormat_0)
                    Else
                        matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, Decimal.Multiply(num1, New Decimal(3L)))), font, solidBrush, Me.rectangle_1(3), Me.stringFormat_0)
                    End If
                    matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, Decimal.Multiply(num1, New Decimal(4L)))), font, solidBrush, Me.rectangle_1(4), Me.stringFormat_0)
                    matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, Decimal.Multiply(num1, New Decimal(5L)))), font, solidBrush, Me.rectangle_1(5), Me.stringFormat_0)
                    If (Me.bool_0) Then
                        matrix0.DrawString(Conversions.ToString(Decimal.Add(Me.decimal_3, Decimal.Multiply(num1, New Decimal(6L)))), font, solidBrush, Me.rectangle_1(6), Me.stringFormat_0)
                    End If
                End Using
            End Using
            Dim num2 As Integer = Math.Max(Convert.ToInt32(CSng(My.Resources.MeterNeedle.Width) * Me.float_2), 1)
            Dim num3 As Integer = Math.Max(Convert.ToInt32(CSng(My.Resources.MeterNeedle.Height) * Me.float_2), 1)
            Me.bitmap_2 = New Bitmap(num2, num3)
            Me.bitmap_2 = New Bitmap(num2, num3)
            matrix0 = Graphics.FromImage(Me.bitmap_2)
            matrix0.Transform = Me.matrix_0
            matrix0.DrawImage(My.Resources.MeterNeedle, 0!, 0!, CSng(My.Resources.MeterNeedle.Width) * Me.float_2, CSng(My.Resources.MeterNeedle.Height) * Me.float_2)
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CSng(My.Resources.MeterBase.Width) * Me.float_2), Convert.ToInt32(CSng(My.Resources.MeterBase.Height) * Me.float_2))
            matrix0.Dispose()
            matrix0 = Graphics.FromImage(Me.bitmap_1)
            matrix0.DrawImage(My.Resources.MeterBase, 0!, 0!, CSng(My.Resources.MeterBase.Width) * Me.float_2, CSng(My.Resources.MeterBase.Height) * Me.float_2)
            matrix0.Dispose()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_1 Is Nothing) And Me.bitmap_2 IsNot Nothing) Then
            If (Me.bitmap_3 Is Nothing) Then
                Me.bitmap_3 = New Bitmap(MyBase.ClientSize.Width, MyBase.ClientSize.Height)
            End If
            Dim matrix0 As Graphics = Graphics.FromImage(Me.bitmap_3)
            matrix0.DrawImageUnscaled(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                matrix0.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_1)
            End If
            Me.matrix_0.RotateAt(CSng((Convert.ToDouble(Decimal.Multiply(Decimal.Negate(Me.decimal_0), New Decimal(180L))) / 3.14159265358979)), New Point(CInt(Math.Round(CDbl(Me.float_0))), CInt(Math.Round(CDbl(Me.float_1)))))
            matrix0.Transform = Me.matrix_0
            matrix0.DrawImage(Me.bitmap_2, Me.int_0, Me.int_1)
            Me.matrix_0.Reset()
            matrix0.Transform = Me.matrix_0
            matrix0.DrawImageUnscaled(Me.bitmap_1, 1, CInt(Math.Round(CDbl((135! * Me.float_2)))))
            painte.Graphics.DrawImageUnscaled(Me.bitmap_3, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Dim width As Single = CSng((CDbl(My.Resources.MeterNoNeedle2.Width) / CDbl(My.Resources.MeterNoNeedle2.Height)))
        If (Me.int_3 <> MyBase.Height Or Me.int_2 <> MyBase.Width) Then
            Dim size As System.Drawing.Size = New System.Drawing.Size(CInt(Math.Round(CDbl((CSng(MyBase.Height) * width)))), MyBase.Height)
            MyBase.Size = size
            Me.method_1()
            Me.int_2 = MyBase.Width
            Me.int_3 = MyBase.Height
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.bitmap_3 IsNot Nothing) Then
            Me.bitmap_3.Dispose()
            Me.bitmap_3 = Nothing
        End If
        Me.method_1()
        MyBase.OnSizeChanged(e)
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
