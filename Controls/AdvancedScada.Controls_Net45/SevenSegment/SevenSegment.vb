Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class SevenSegment
    Inherits Control
    Private bitmap_0 As Bitmap()

    Private bitmap_1 As Bitmap()

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private float_0 As Single

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bool_0 As Boolean

    Private Const int_0 As Integer = 340

    Private Const int_1 As Integer = 486

    Private double_0 As Double

    Private float_1 As Single

    Private float_2 As Single

    Private decimal_0 As Decimal

    Private decimal_1 As Decimal

    Private int_2 As Integer

    Private int_3 As Integer

    Private float_3 As Single

    Private float_4 As Single

    Private int_4 As Integer

    Private bitmap_4 As Bitmap

    Private int_5 As Integer

    Private int_6 As Integer

    Private float_5 As Single

    <Category("Numeric Display")>
    Public Property _ValueScaleFactor As Decimal
        Get
            Return Me.decimal_0
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    <Category("Numeric Display")>
    Public Property DecimalPosition As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_3) Then
                Me.int_3 = Math.Max(Math.Min(Me.int_2 - 1, value), 0)
                Me.method_2()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property MaxValueForRed As Single
        Get
            Return Me.float_2
        End Get
        Set(ByVal value As Single)
            If (value <> Me.float_2) Then
                Me.float_2 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property MinValueForRed As Single
        Get
            Return Me.float_1
        End Get
        Set(ByVal value As Single)
            If (value <> Me.float_1) Then
                Me.float_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property NumberOfDigits As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_2) Then
                Me.int_2 = Math.Max(Math.Min(50, value), 1)
                Me.method_1()
                Me.method_2()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property Resolution As Decimal
        Get
            Return Me.decimal_1
        End Get
        Set(ByVal value As Decimal)
            If (Decimal.Compare(value, Decimal.Zero) <> 0) Then
                Me.decimal_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
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

    Public Sub New()
        MyBase.New()
        ReDim Me.bitmap_0(11)
        ReDim Me.bitmap_1(11)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.bool_0 = True
        Me.float_1 = 100!
        Me.float_2 = 200!
        Me.decimal_0 = Decimal.One
        Me.decimal_1 = Decimal.One
        Me.int_2 = 5
        Me.int_3 = 0
        Me.int_4 = 340
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_2)) * 1.1)))

        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.ForeColor = Color.LightGray
        Me.method_1()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                Me.bitmap_2.Dispose()
                Dim length As Integer = CInt(Me.bitmap_0.Length) - 1
                Dim num As Integer = 0
                Do
                    If (Me.bitmap_0(num) IsNot Nothing) Then
                        Me.bitmap_0(num).Dispose()
                    End If
                    If (Me.bitmap_1(num) IsNot Nothing) Then
                        Me.bitmap_1(num).Dispose()
                    End If
                    num = num + 1
                Loop While num <= length
                Me.solidBrush_0.Dispose()
                Me.stringFormat_0.Dispose()
                Me.bitmap_4.Dispose()
                If (Me.bitmap_3 IsNot Nothing) Then
                    Me.bitmap_3.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.Height <> Me.int_6 Or MyBase.Width <> Me.int_5) Then
            Me.method_1()
            Me.int_5 = MyBase.Width
            Me.int_6 = MyBase.Height
            Me.method_2()
        End If
    End Sub

    Private Sub method_1()
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_2)) * 1.1)))
        If (Me.int_6 < MyBase.Height Or Me.int_5 < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.float_5)) Then
                MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.float_5))))
            Else
                MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.float_5))))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.float_5)) Then
            MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.float_5))))
        Else
            MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.float_5))))
        End If
    End Sub

    Private Sub method_2()
        Dim graphic As Graphics
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_2)) * 1.1)))
        Dim width As Single = CSng((CDbl(MyBase.Width) / (CDbl((340 * Me.int_2)) * 1.1)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / 486))
        If (width >= height) Then
            Me.float_3 = CSng(MyBase.Width)
            Me.float_4 = CSng((486 / CDbl((340 * Me.int_2)) * CDbl(MyBase.Width)))
            Me.float_0 = height
        Else
            Me.float_4 = CSng(MyBase.Height)
            If ((MyBase.Height > 0 And 1) = 0) Then
                Me.float_3 = 1!
            Else
                Me.float_3 = CSng((CDbl((340 * Me.int_2)) * 1.1 / 486 * CDbl(MyBase.Height)))
            End If
            Me.float_0 = width
        End If
        If (Me.float_0 > 0!) Then
            Me.rectangle_0.X = 0
            Me.rectangle_0.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.04))
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.18))
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            Dim num As Integer = Convert.ToInt32(340! * Me.float_0)
            Dim num1 As Integer = Convert.ToInt32(486! * Me.float_0)
            Dim num2 As Integer = 0
            Do
                If (Me.bitmap_0(num2) IsNot Nothing) Then
                    Me.bitmap_0(num2).Dispose()
                End If
                Me.bitmap_0(num2) = New Bitmap(num, num1)
                graphic = Graphics.FromImage(Me.bitmap_0(num2))
                graphic.ScaleTransform(CSng((0.5 * CDbl(Me.float_0))), CSng((0.5 * CDbl(Me.float_0))))
                SevenSegment2.FillPolygon(graphic, num2, Color.Red, True)
                num2 = num2 + 1
            Loop While num2 <= 11
            Dim num3 As Integer = 0
            Do
                If (Me.bitmap_1(num3) IsNot Nothing) Then
                    Me.bitmap_1(num3).Dispose()
                End If
                Me.bitmap_1(num3) = New Bitmap(num, num1)
                graphic = Graphics.FromImage(Me.bitmap_1(num3))
                graphic.ScaleTransform(CSng((0.5 * CDbl(Me.float_0))), CSng((0.5 * CDbl(Me.float_0))))
                SevenSegment2.FillPolygon(graphic, num3, Color.Green, True)
                num3 = num3 + 1
            Loop While num3 <= 11
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(CSng(My.Resources.RedDecimal.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.RedDecimal.Height) * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.RedDecimal, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(CSng(My.Resources.GreenDecimal.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.GreenDecimal.Height) * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(My.Resources.GreenDecimal, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            graphic.Dispose()
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bool_0 = True
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        Me.stringFormat_0.Alignment = StringAlignment.Center
        Me.stringFormat_0.LineAlignment = StringAlignment.Far
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        MyBase.Invalidate()
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

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim flag As Boolean = False
        If (Me.bitmap_4 IsNot Nothing And Me.solidBrush_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            If (Me.BackgroundImage IsNot Nothing) Then
                If (Me.BackgroundImageLayout <> ImageLayout.Stretch) Then
                    graphics.DrawImage(Me.BackgroundImage, 0, 0)
                Else
                    graphics.DrawImage(Me.BackgroundImage, 0, 0, MyBase.Width, MyBase.Height)
                End If
            End If
            Dim one As Decimal = Decimal.Divide(Decimal.One, Me.decimal_1)
            If (Decimal.Compare(one, Decimal.Zero) = 0) Then
                one = Decimal.One
            End If
            Dim num As Long = CLng(Math.Round(Me.double_0 * Convert.ToDouble(one) * Convert.ToDouble(Me.decimal_0)))
            Dim num1 As Long = Convert.ToInt64(Decimal.Divide(New Decimal(num), one))
            If (Not (CDbl(num1) <= Math.Pow(10, CDbl(Me.int_2)) - 1 And CDbl(num1) >= (Math.Pow(10, CDbl((Me.int_2 - 1))) - 1) * -1)) Then
                Dim int2 As Integer = Me.int_2
                For i As Integer = 1 To int2 Step 1
                    graphics.DrawImage(Me.bitmap_0(11), CSng((Me.int_4 * (i - 1))) * Me.float_0, 0!)
                Next

            Else
                Dim int21 As Integer = Me.int_2
                For j As Integer = 1 To int21 Step 1
                    If (num1 >= 0L) Then
                        Dim num2 As Integer = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl((Me.int_2 - j)))))
                        If (num2 > 0 Or j = Me.int_2 Or j > Me.int_2 - Me.int_3) Then
                            flag = True
                        End If
                        If (flag) Then
                            If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                graphics.DrawImage(Me.bitmap_1(num2), CSng(Convert.ToInt32(CDbl((Me.int_4 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                            Else
                                graphics.DrawImage(Me.bitmap_0(num2), CSng(Convert.ToInt32(CDbl((Me.int_4 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                            End If
                        ElseIf (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                            graphics.DrawImage(Me.bitmap_1(10), CSng(Convert.ToInt32(CDbl((Me.int_4 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                        Else
                            graphics.DrawImage(Me.bitmap_0(10), CSng(Convert.ToInt32(CDbl((Me.int_4 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                        End If
                        num1 = CLng(Math.Round(CDbl(num1) - CDbl(num2) * Math.Pow(10, CDbl((Me.int_2 - j)))))
                    Else
                        If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                            graphics.DrawImage(Me.bitmap_1(11), CSng((Me.int_4 * (j - 1))) * Me.float_0, 0!)
                        Else
                            graphics.DrawImage(Me.bitmap_0(11), CSng((Me.int_4 * (j - 1))) * Me.float_0, 0!)
                        End If
                        num1 = Math.Abs(num1)
                    End If
                Next

            End If
            If (Me.int_3 > 0) Then
                If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                    graphics.DrawImage(Me.bitmap_3, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_2 - Me.int_3) * Me.int_4 - 50)) * Me.float_0)) * 1.1)), 440! * Me.float_0)
                Else
                    graphics.DrawImage(Me.bitmap_2, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_2 - Me.int_3) * Me.int_4 - 50)) * Me.float_0)) * 1.1)), 440! * Me.float_0)
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.bitmap_4 IsNot Nothing) Then
            Me.bitmap_4.Dispose()
            Me.bitmap_4 = Nothing
        End If
        Me.method_0(Me, Nothing)
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
