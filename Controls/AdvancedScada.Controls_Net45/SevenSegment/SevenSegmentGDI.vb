Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms


Public Class SevenSegmentGDI
    Inherits Control
    Private point_0 As Point()

    Private point_1 As Point()

    Private point_2 As Point()

    Private point_3 As Point()

    Private point_4 As Point()

    Private point_5 As Point()

    Private point_6 As Point()

    Private bitmap_0 As Bitmap()

    Private bitmap_1 As Bitmap()

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private float_0 As Single

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bool_0 As Boolean

    Private double_0 As Double

    Private float_1 As Single

    Private float_2 As Single

    Private decimal_0 As Decimal

    Private decimal_1 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Private bool_1 As Boolean

    Private float_3 As Single

    Private float_4 As Single

    Private int_2 As Integer

    Private bitmap_4 As Bitmap

    Private int_3 As Integer

    Private int_4 As Integer

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

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim exStyle As CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    <Category("Numeric Display")>
    Public Property DecimalPosition As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_1) Then
                Me.int_1 = Math.Max(Math.Min(Me.int_0 - 1, value), 0)
                Me.method_3()
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
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_0) Then
                Me.int_0 = Math.Max(Math.Min(50, value), 1)
                Me.method_1()
                Me.method_3()
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

    Public Property ShowOffSegments As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_1 <> value) Then
                Me.bool_1 = value
                Me.method_3()
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
        ReDim Me.point_0(11)
        ReDim Me.point_1(11)
        ReDim Me.point_2(11)
        ReDim Me.point_3(11)
        ReDim Me.point_4(6)
        ReDim Me.point_5(16)
        ReDim Me.point_6(16)
        ReDim Me.bitmap_0(11)
        ReDim Me.bitmap_1(11)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.bool_0 = True
        Me.float_1 = 100.0!
        Me.float_2 = 200.0!
        Me.decimal_0 = Decimal.One
        Me.decimal_1 = Decimal.One
        Me.int_0 = 5
        Me.int_1 = 0
        Me.bool_1 = True
        Me.int_2 = 340
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))
        Me.method_2()
        MyBase.SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.LightGray
        End If
        Me.BackColor = Color.Transparent
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
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.Height <> Me.int_4 Or MyBase.Width <> Me.int_3) Then
            Me.method_1()
            Me.int_3 = MyBase.Width
            Me.int_4 = MyBase.Height
            Me.method_3()
        End If
    End Sub

    Private Sub method_1()
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))
        If (Me.int_4 < MyBase.Height Or Me.int_3 < MyBase.Width) Then
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
        Me.point_0(0) = New Point(75, 110)
        Me.point_0(1) = New Point(47, 415)
        Me.point_0(2) = New Point(98, 471)
        Me.point_0(3) = New Point(157, 417)
        Me.point_0(4) = New Point(182, 125)
        Me.point_0(5) = New Point(88, 21)
        Me.point_0(6) = New Point(88, 21)
        Me.point_0(7) = New Point(85, 27)
        Me.point_0(8) = New Point(82, 32)
        Me.point_0(9) = New Point(80, 38)
        Me.point_0(10) = New Point(79, 44)
        Me.point_0(11) = New Point(74, 110)
        Me.point_1(0) = New Point(580, 471)
        Me.point_1(1) = New Point(641, 415)
        Me.point_1(2) = New Point(668, 110)
        Me.point_1(3) = New Point(673, 44)
        Me.point_1(4) = New Point(673, 44)
        Me.point_1(5) = New Point(674, 38)
        Me.point_1(6) = New Point(673, 31)
        Me.point_1(7) = New Point(671, 26)
        Me.point_1(8) = New Point(669, 20)
        Me.point_1(9) = New Point(557, 123)
        Me.point_1(10) = New Point(531, 417)
        Me.point_1(11) = New Point(580, 471)
        Me.point_2(0) = New Point(96, 499)
        Me.point_2(1) = New Point(35, 555)
        Me.point_2(2) = New Point(8, 858)
        Me.point_2(3) = New Point(3, 924)
        Me.point_2(4) = New Point(3, 924)
        Me.point_2(5) = New Point(2, 930)
        Me.point_2(6) = New Point(3, 937)
        Me.point_2(7) = New Point(5, 942)
        Me.point_2(8) = New Point(7, 948)
        Me.point_2(9) = New Point(119, 845)
        Me.point_2(10) = New Point(145, 553)
        Me.point_2(11) = New Point(96, 499)
        Me.point_3(0) = New Point(602, 858)
        Me.point_3(1) = New Point(629, 555)
        Me.point_3(2) = New Point(578, 499)
        Me.point_3(3) = New Point(519, 553)
        Me.point_3(4) = New Point(493, 847)
        Me.point_3(5) = New Point(586, 949)
        Me.point_3(6) = New Point(586, 949)
        Me.point_3(7) = New Point(590, 943)
        Me.point_3(8) = New Point(593, 937)
        Me.point_3(9) = New Point(595, 931)
        Me.point_3(10) = New Point(597, 924)
        Me.point_3(11) = New Point(602, 858)
        Me.point_4(0) = New Point(515, 430)
        Me.point_4(1) = New Point(171, 430)
        Me.point_4(2) = New Point(111, 485)
        Me.point_4(3) = New Point(161, 540)
        Me.point_4(4) = New Point(505, 540)
        Me.point_4(5) = New Point(565, 485)
        Me.point_4(6) = New Point(515, 430)
        Me.point_5(0) = New Point(475, 858)
        Me.point_5(1) = New Point(133, 858)
        Me.point_5(2) = New Point(21, 962)
        Me.point_5(3) = New Point(21, 962)
        Me.point_5(4) = New Point(26, 965)
        Me.point_5(5) = New Point(31, 966)
        Me.point_5(6) = New Point(37, 968)
        Me.point_5(7) = New Point(43, 968)
        Me.point_5(8) = New Point(109, 968)
        Me.point_5(9) = New Point(483, 968)
        Me.point_5(10) = New Point(549, 968)
        Me.point_5(11) = New Point(549, 968)
        Me.point_5(12) = New Point(554, 968)
        Me.point_5(13) = New Point(560, 967)
        Me.point_5(14) = New Point(565, 965)
        Me.point_5(15) = New Point(570, 962)
        Me.point_5(16) = New Point(475, 858)
        Me.point_6(0) = New Point(192, 110)
        Me.point_6(1) = New Point(543, 110)
        Me.point_6(2) = New Point(655, 6)
        Me.point_6(3) = New Point(655, 6)
        Me.point_6(4) = New Point(650, 4)
        Me.point_6(5) = New Point(645, 2)
        Me.point_6(6) = New Point(639, 0)
        Me.point_6(7) = New Point(633, 0)
        Me.point_6(8) = New Point(567, 0)
        Me.point_6(9) = New Point(193, 0)
        Me.point_6(10) = New Point(127, 0)
        Me.point_6(11) = New Point(127, 0)
        Me.point_6(12) = New Point(121, 0)
        Me.point_6(13) = New Point(115, 2)
        Me.point_6(14) = New Point(109, 4)
        Me.point_6(15) = New Point(103, 7)
        Me.point_6(16) = New Point(197, 110)
    End Sub

    Private Sub method_3()
        Dim graphic As Graphics
        Me.float_5 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))
        Dim width As Single = CSng((CDbl(MyBase.Width) / (CDbl((340 * Me.int_0)) * 1.1)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / 486))
        If (width >= height) Then
            Me.float_3 = CSng(MyBase.Width)
            Me.float_4 = CSng((486 / CDbl((340 * Me.int_0)) * CDbl(MyBase.Width)))
            Me.float_0 = height
        Else
            Me.float_4 = CSng(MyBase.Height)
            If (MyBase.Height <= 0) Then
                Me.float_3 = 1.0!
            Else
                Me.float_3 = CSng((CDbl((340 * Me.int_0)) * 1.1 / 486 * CDbl(MyBase.Height)))
            End If
            Me.float_0 = width
        End If
        If (Me.float_0 > 0.0!) Then
            Me.rectangle_0.X = 0
            Me.rectangle_0.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.04))
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.18))
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            Dim num As Integer = Convert.ToInt32(340.0! * Me.float_0)
            Dim num1 As Integer = Convert.ToInt32(486.0! * Me.float_0)
            Dim num2 As Integer = 0
            Do
                If (Me.bitmap_0(num2) IsNot Nothing) Then
                    Me.bitmap_0(num2).Dispose()
                End If
                Me.bitmap_0(num2) = New Bitmap(num, num1)
                graphic = Graphics.FromImage(Me.bitmap_0(num2))
                graphic.ScaleTransform(0.0925!, 0.095!)
                Me.method_4(graphic, num2, Color.Red)
                num2 = num2 + 1
            Loop While num2 <= 11
            Dim num3 As Integer = 0
            Do
                If (Me.bitmap_1(num3) IsNot Nothing) Then
                    Me.bitmap_1(num3).Dispose()
                End If
                Me.bitmap_1(num3) = New Bitmap(num, num1)
                graphic = Graphics.FromImage(Me.bitmap_1(num3))
                graphic.ScaleTransform(CSng((CDbl(num) / 700)), CSng((CDbl(num) / 700)))
                Me.method_4(graphic, num3, Color.Green)
                num3 = num3 + 1
            Loop While num3 <= 11
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(50.0! * Me.float_0), Convert.ToInt32(50.0! * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_2)
            Dim point() As System.Drawing.Point = {New System.Drawing.Point(CInt(Math.Round(CDbl(Me.bitmap_2.Width) / 2)), 0), New System.Drawing.Point(Me.bitmap_2.Width, CInt(Math.Round(CDbl(Me.bitmap_2.Height) / 2))), New System.Drawing.Point(CInt(Math.Round(CDbl(Me.bitmap_2.Width) / 2)), Me.bitmap_2.Height), New System.Drawing.Point(0, CInt(Math.Round(CDbl(Me.bitmap_2.Height) / 2))), New System.Drawing.Point(CInt(Math.Round(CDbl(Me.bitmap_2.Width) / 2)), 0)}
            graphic.FillPolygon(New SolidBrush(Color.Red), point)
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(50.0! * Me.float_0), Convert.ToInt32(50.0! * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_3)
            graphic.FillPolygon(New SolidBrush(Color.Green), point)
            graphic.Dispose()
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bool_0 = True
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub method_4(ByVal graphics_0 As Graphics, ByVal int_5 As Integer, ByVal color_0 As Color)
        Dim solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(color_0)
        Dim solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(64, 0, 0, 0))
        graphics_0.CompositingQuality = CompositingQuality.HighQuality
        graphics_0.SmoothingMode = SmoothingMode.AntiAlias
        If (int_5 = 0 Or int_5 = 4 Or int_5 = 5 Or int_5 = 6 Or int_5 = 8 Or int_5 = 9) Then
            graphics_0.FillPolygon(solidBrush, Me.point_0)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_0)
        End If
        If (Not (int_5 = 5 Or int_5 = 6 Or int_5 = 10 Or int_5 = 11)) Then
            graphics_0.FillPolygon(solidBrush, Me.point_1)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_1)
        End If
        If (int_5 = 0 Or int_5 = 2 Or int_5 = 6 Or int_5 = 8) Then
            graphics_0.FillPolygon(solidBrush, Me.point_2)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_2)
        End If
        If (Not (int_5 = 2 Or int_5 = 10 Or int_5 = 11)) Then
            graphics_0.FillPolygon(solidBrush, Me.point_3)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_3)
        End If
        If (Not (int_5 = 0 Or int_5 = 1 Or int_5 = 7 Or int_5 = 10)) Then
            graphics_0.FillPolygon(solidBrush, Me.point_4)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_4)
        End If
        If (Not (int_5 = 1 Or int_5 = 4 Or int_5 = 7 Or int_5 = 9 Or int_5 = 10 Or int_5 = 11)) Then
            graphics_0.FillPolygon(solidBrush, Me.point_5)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_5)
        End If
        If (Not (int_5 = 1 Or int_5 = 4 Or int_5 = 10 Or int_5 = 11)) Then
            graphics_0.FillPolygon(solidBrush, Me.point_6)
        ElseIf (Me.bool_1) Then
            graphics_0.FillPolygon(solidBrush1, Me.point_6)
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        Me.stringFormat_0.Alignment = StringAlignment.Center
        Me.stringFormat_0.LineAlignment = StringAlignment.Far
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal eventArgs_0 As EventArgs)
        MyBase.OnFontChanged(eventArgs_0)
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal eventArgs_0 As EventArgs)
        MyBase.OnForeColorChanged(eventArgs_0)
        If (Me.solidBrush_0 IsNot Nothing) Then
            Me.solidBrush_0.Color = MyBase.ForeColor
        Else
            Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        Dim flag As Boolean = False
        If (Not (Me.bitmap_4 Is Nothing Or Me.solidBrush_0 Is Nothing)) Then
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_4)
                graphic.Clear(Me.BackColor)
                If (Me.BackgroundImage IsNot Nothing) Then
                    If (Me.BackgroundImageLayout <> ImageLayout.Stretch) Then
                        graphic.DrawImage(Me.BackgroundImage, 0, 0)
                    Else
                        graphic.DrawImage(Me.BackgroundImage, 0, 0, MyBase.Width, MyBase.Height)
                    End If
                End If
                Dim one As Decimal = Decimal.Divide(Decimal.One, Me.decimal_1)
                If (Decimal.Compare(one, Decimal.Zero) = 0) Then
                    one = Decimal.One
                End If
                Dim num As Long = CLng(Math.Round(Me.double_0 * Convert.ToDouble(one) * Convert.ToDouble(Me.decimal_0)))
                Dim num1 As Long = Convert.ToInt64(Decimal.Divide(New Decimal(num), one))
                If (Not (CDbl(num1) <= Math.Pow(10, CDbl(Me.int_0)) - 1 And CDbl(num1) >= (Math.Pow(10, CDbl((Me.int_0 - 1))) - 1) * -1)) Then
                    Dim int0 As Integer = Me.int_0
                    For i As Integer = 1 To int0
                        graphic.DrawImage(Me.bitmap_0(11), CSng((Me.int_2 * (i - 1))) * Me.float_0, 0.0!)
                    Next

                Else
                    Dim int01 As Integer = Me.int_0
                    For j As Integer = 1 To int01
                        If (num1 >= CLng(0)) Then
                            Dim num2 As Integer = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl((Me.int_0 - j)))))
                            If (num2 > 0 Or j = Me.int_0 Or j > Me.int_0 - Me.int_1) Then
                                flag = True
                            End If
                            If (flag) Then
                                If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                    graphic.DrawImage(Me.bitmap_1(num2), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0.0!)
                                Else
                                    graphic.DrawImage(Me.bitmap_0(num2), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0.0!)
                                End If
                            ElseIf (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                graphic.DrawImage(Me.bitmap_1(10), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0.0!)
                            Else
                                graphic.DrawImage(Me.bitmap_0(10), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0.0!)
                            End If
                            num1 = CLng(Math.Round(CDbl(num1) - CDbl(num2) * Math.Pow(10, CDbl((Me.int_0 - j)))))
                        Else
                            If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                graphic.DrawImage(Me.bitmap_1(11), CSng((Me.int_2 * (j - 1))) * Me.float_0, 0.0!)
                            Else
                                graphic.DrawImage(Me.bitmap_0(11), CSng((Me.int_2 * (j - 1))) * Me.float_0, 0.0!)
                            End If
                            num1 = Math.Abs(num1)
                        End If
                    Next

                End If
                If (Me.int_1 > 0) Then
                    If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                        graphic.DrawImage(Me.bitmap_3, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_0 - Me.int_1) * Me.int_2 - 50)) * Me.float_0)) * 1.1)), 440.0! * Me.float_0)
                    Else
                        graphic.DrawImage(Me.bitmap_2, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_0 - Me.int_1) * Me.int_2 - 50)) * Me.float_0)) * 1.1)), 440.0! * Me.float_0)
                    End If
                End If
                pevent.Graphics.DrawImage(Me.bitmap_4, 0, 0)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        MyBase.OnPaintBackground(pevent)
        Me.bool_0 = False
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal eventArgs_0 As EventArgs)
        If (Me.bitmap_4 IsNot Nothing) Then
            Me.bitmap_4.Dispose()
            Me.bitmap_4 = Nothing
        End If
        Me.method_0(Me, Nothing)
        MyBase.OnSizeChanged(eventArgs_0)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal eventArgs_0 As EventArgs)
        MyBase.OnTextChanged(eventArgs_0)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
