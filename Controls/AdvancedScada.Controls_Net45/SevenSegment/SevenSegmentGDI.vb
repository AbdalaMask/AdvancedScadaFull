Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class SevenSegmentGDI
    Inherits Control
    Private bitmap_0 As Bitmap()

    Private bitmap_1 As Bitmap()

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private float_0 As Single

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private double_0 As Double

    Private float_1 As Single

    Private float_2 As Single

    Private decimal_0 As Decimal

    Private decimal_1 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Private bool_0 As Boolean

    Private int_2 As Integer

    Private bitmap_4 As Bitmap

    Private int_3 As Integer

    Private int_4 As Integer

    Private float_3 As Single

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
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_1) Then
                Me.int_1 = Math.Max(Math.Min(Me.int_0 - 1, value), 0)
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
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_0) Then
                Me.int_0 = Math.Max(Math.Min(50, value), 1)
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

    Public Property ShowOffSegments As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                Me.method_2()
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

    <Category("Numeric Display")>
    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.decimal_0
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        ReDim Me.bitmap_0(11)
        ReDim Me.bitmap_1(11)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.float_1 = 100!
        Me.float_2 = 200!
        Me.decimal_0 = Decimal.One
        Me.decimal_1 = Decimal.One
        Me.int_0 = 5
        Me.int_1 = 0
        Me.bool_0 = True
        Me.int_2 = 340
        Me.float_3 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))

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
                If (Me.bitmap_3 IsNot Nothing) Then
                    Me.bitmap_3.Dispose()
                End If
                Me.solidBrush_0.Dispose()
                Me.stringFormat_0.Dispose()
                Me.bitmap_4.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Shared Sub DrawDigit(ByVal graphics_0 As Graphics, ByVal int_5 As Integer, ByVal color_0 As Color, ByVal showOffSegments As Boolean, ByVal widthScale As Double, ByVal heightScale As Double)
        Dim point(11) As System.Drawing.Point
        Dim pointArray(11) As System.Drawing.Point
        Dim point1(11) As System.Drawing.Point
        Dim pointArray1(11) As System.Drawing.Point
        Dim point2(6) As System.Drawing.Point
        Dim pointArray2(16) As System.Drawing.Point
        Dim point3(16) As System.Drawing.Point
        point(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 75), Convert.ToInt32(heightScale * 110))
        point(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 47), Convert.ToInt32(heightScale * 415))
        point(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 98), Convert.ToInt32(heightScale * 471))
        point(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 157), Convert.ToInt32(heightScale * 417))
        point(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 182), Convert.ToInt32(heightScale * 125))
        point(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 88), Convert.ToInt32(heightScale * 21))
        point(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 88), Convert.ToInt32(heightScale * 21))
        point(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 85), Convert.ToInt32(heightScale * 27))
        point(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 82), Convert.ToInt32(heightScale * 32))
        point(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 80), Convert.ToInt32(heightScale * 38))
        point(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 79), Convert.ToInt32(heightScale * 44))
        point(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 74), Convert.ToInt32(heightScale * 110))
        pointArray(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 580), Convert.ToInt32(heightScale * 471))
        pointArray(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 641), Convert.ToInt32(heightScale * 415))
        pointArray(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 668), Convert.ToInt32(heightScale * 110))
        pointArray(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 673), Convert.ToInt32(heightScale * 44))
        pointArray(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 673), Convert.ToInt32(heightScale * 44))
        pointArray(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 674), Convert.ToInt32(heightScale * 38))
        pointArray(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 673), Convert.ToInt32(heightScale * 31))
        pointArray(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 671), Convert.ToInt32(heightScale * 26))
        pointArray(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 669), Convert.ToInt32(heightScale * 20))
        pointArray(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 557), Convert.ToInt32(heightScale * 123))
        pointArray(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 531), Convert.ToInt32(heightScale * 417))
        pointArray(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 580), Convert.ToInt32(heightScale * 471))
        point1(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 96), Convert.ToInt32(heightScale * 499))
        point1(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 35), Convert.ToInt32(heightScale * 555))
        point1(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 8), Convert.ToInt32(heightScale * 858))
        point1(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 3), Convert.ToInt32(heightScale * 924))
        point1(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 3), Convert.ToInt32(heightScale * 924))
        point1(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 2), Convert.ToInt32(heightScale * 930))
        point1(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 3), Convert.ToInt32(heightScale * 937))
        point1(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 5), Convert.ToInt32(heightScale * 942))
        point1(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 7), Convert.ToInt32(heightScale * 948))
        point1(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 119), Convert.ToInt32(heightScale * 845))
        point1(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 145), Convert.ToInt32(heightScale * 553))
        point1(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 96), Convert.ToInt32(heightScale * 499))
        pointArray1(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 602), Convert.ToInt32(heightScale * 858))
        pointArray1(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 629), Convert.ToInt32(heightScale * 555))
        pointArray1(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 578), Convert.ToInt32(heightScale * 499))
        pointArray1(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 519), Convert.ToInt32(heightScale * 553))
        pointArray1(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 493), Convert.ToInt32(heightScale * 847))
        pointArray1(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 586), Convert.ToInt32(heightScale * 949))
        pointArray1(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 586), Convert.ToInt32(heightScale * 949))
        pointArray1(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 590), Convert.ToInt32(heightScale * 943))
        pointArray1(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 593), Convert.ToInt32(heightScale * 937))
        pointArray1(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 595), Convert.ToInt32(heightScale * 931))
        pointArray1(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 597), Convert.ToInt32(heightScale * 924))
        pointArray1(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 602), Convert.ToInt32(heightScale * 858))
        point2(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 515), Convert.ToInt32(heightScale * 430))
        point2(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 171), Convert.ToInt32(heightScale * 430))
        point2(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 111), Convert.ToInt32(heightScale * 485))
        point2(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 161), Convert.ToInt32(heightScale * 540))
        point2(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 505), Convert.ToInt32(heightScale * 540))
        point2(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 565), Convert.ToInt32(heightScale * 485))
        point2(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 515), Convert.ToInt32(heightScale * 430))
        pointArray2(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 475), Convert.ToInt32(heightScale * 858))
        pointArray2(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 133), Convert.ToInt32(heightScale * 858))
        pointArray2(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 21), Convert.ToInt32(heightScale * 962))
        pointArray2(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 21), Convert.ToInt32(heightScale * 962))
        pointArray2(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 26), Convert.ToInt32(heightScale * 965))
        pointArray2(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 31), Convert.ToInt32(heightScale * 966))
        pointArray2(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 37), Convert.ToInt32(heightScale * 968))
        pointArray2(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 43), Convert.ToInt32(heightScale * 968))
        pointArray2(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 109), Convert.ToInt32(heightScale * 968))
        pointArray2(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 483), Convert.ToInt32(heightScale * 968))
        pointArray2(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 549), Convert.ToInt32(heightScale * 968))
        pointArray2(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 549), Convert.ToInt32(heightScale * 968))
        pointArray2(12) = New System.Drawing.Point(Convert.ToInt32(widthScale * 554), Convert.ToInt32(heightScale * 968))
        pointArray2(13) = New System.Drawing.Point(Convert.ToInt32(widthScale * 560), Convert.ToInt32(heightScale * 967))
        pointArray2(14) = New System.Drawing.Point(Convert.ToInt32(widthScale * 565), Convert.ToInt32(heightScale * 965))
        pointArray2(15) = New System.Drawing.Point(Convert.ToInt32(widthScale * 570), Convert.ToInt32(heightScale * 962))
        pointArray2(16) = New System.Drawing.Point(Convert.ToInt32(widthScale * 475), Convert.ToInt32(heightScale * 858))
        point3(0) = New System.Drawing.Point(Convert.ToInt32(widthScale * 192), Convert.ToInt32(heightScale * 110))
        point3(1) = New System.Drawing.Point(Convert.ToInt32(widthScale * 543), Convert.ToInt32(heightScale * 110))
        point3(2) = New System.Drawing.Point(Convert.ToInt32(widthScale * 655), Convert.ToInt32(heightScale * 6))
        point3(3) = New System.Drawing.Point(Convert.ToInt32(widthScale * 655), Convert.ToInt32(heightScale * 6))
        point3(4) = New System.Drawing.Point(Convert.ToInt32(widthScale * 650), Convert.ToInt32(heightScale * 4))
        point3(5) = New System.Drawing.Point(Convert.ToInt32(widthScale * 645), Convert.ToInt32(heightScale * 2))
        point3(6) = New System.Drawing.Point(Convert.ToInt32(widthScale * 639), Convert.ToInt32(heightScale * 0))
        point3(7) = New System.Drawing.Point(Convert.ToInt32(widthScale * 633), Convert.ToInt32(heightScale * 0))
        point3(8) = New System.Drawing.Point(Convert.ToInt32(widthScale * 567), Convert.ToInt32(heightScale * 0))
        point3(9) = New System.Drawing.Point(Convert.ToInt32(widthScale * 193), Convert.ToInt32(heightScale * 0))
        point3(10) = New System.Drawing.Point(Convert.ToInt32(widthScale * 127), Convert.ToInt32(heightScale * 0))
        point3(11) = New System.Drawing.Point(Convert.ToInt32(widthScale * 127), Convert.ToInt32(heightScale * 0))
        point3(12) = New System.Drawing.Point(Convert.ToInt32(widthScale * 121), Convert.ToInt32(heightScale * 0))
        point3(13) = New System.Drawing.Point(Convert.ToInt32(widthScale * 115), Convert.ToInt32(heightScale * 2))
        point3(14) = New System.Drawing.Point(Convert.ToInt32(widthScale * 109), Convert.ToInt32(heightScale * 4))
        point3(15) = New System.Drawing.Point(Convert.ToInt32(widthScale * 103), Convert.ToInt32(heightScale * 7))
        point3(16) = New System.Drawing.Point(Convert.ToInt32(widthScale * 197), Convert.ToInt32(heightScale * 110))
        Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(color_0)
            Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(64, 0, 0, 0))
                graphics_0.CompositingQuality = CompositingQuality.HighQuality
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias
                If (int_5 = 0 Or int_5 = 4 Or int_5 = 5 Or int_5 = 6 Or int_5 = 8 Or int_5 = 9) Then
                    graphics_0.FillPolygon(solidBrush, point)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, point)
                End If
                If (Not (int_5 = 5 Or int_5 = 6 Or int_5 = 10 Or int_5 = 11)) Then
                    graphics_0.FillPolygon(solidBrush, pointArray)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, pointArray)
                End If
                If (int_5 = 0 Or int_5 = 2 Or int_5 = 6 Or int_5 = 8) Then
                    graphics_0.FillPolygon(solidBrush, point1)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, point1)
                End If
                If (Not (int_5 = 2 Or int_5 = 10 Or int_5 = 11)) Then
                    graphics_0.FillPolygon(solidBrush, pointArray1)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, pointArray1)
                End If
                If (Not (int_5 = 0 Or int_5 = 1 Or int_5 = 7 Or int_5 = 10)) Then
                    graphics_0.FillPolygon(solidBrush, point2)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, point2)
                End If
                If (Not (int_5 = 1 Or int_5 = 4 Or int_5 = 7 Or int_5 = 9 Or int_5 = 10 Or int_5 = 11)) Then
                    graphics_0.FillPolygon(solidBrush, pointArray2)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, pointArray2)
                End If
                If (Not (int_5 = 1 Or int_5 = 4 Or int_5 = 10 Or int_5 = 11)) Then
                    graphics_0.FillPolygon(solidBrush, point3)
                ElseIf (showOffSegments) Then
                    graphics_0.FillPolygon(solidBrush1, point3)
                End If
            End Using
        End Using
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.Height <> Me.int_4 Or MyBase.Width <> Me.int_3) Then
            Me.method_1()
            Me.int_3 = MyBase.Width
            Me.int_4 = MyBase.Height
            Me.method_2()
        End If
    End Sub

    Private Sub method_1()
        Me.float_3 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))
        If (Me.int_4 < MyBase.Height Or Me.int_3 < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.float_3)) Then
                MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.float_3))))
            Else
                MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.float_3))))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.float_3)) Then
            MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.float_3))))
        Else
            MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.float_3))))
        End If
    End Sub

    Private Sub method_2()
        Dim graphic As Graphics
        Me.float_3 = CSng((486 / (CDbl((340 * Me.int_0)) * 1.1)))
        Dim width As Single = CSng((CDbl(MyBase.Width) / (CDbl((340 * Me.int_0)) * 1.1)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / 486))
        If (width >= height) Then
            Me.float_0 = height
        Else
            Me.float_0 = width
        End If
        If (Me.float_0 > 0!) Then
            Me.rectangle_0.X = 0
            Me.rectangle_0.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.04))
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.18))
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New System.Drawing.SolidBrush(MyBase.ForeColor)
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
                graphic.ScaleTransform(0.0925!, 0.095!)
                SevenSegmentGDI.DrawDigit(graphic, num2, Color.Red, Me.bool_0, 1, 1)
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
                SevenSegmentGDI.DrawDigit(graphic, num3, Color.Green, Me.bool_0, 1, 1)
                num3 = num3 + 1
            Loop While num3 <= 11
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(50! * Me.float_0), Convert.ToInt32(50! * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_2)
            Dim point(4) As System.Drawing.Point
            Dim num4 As Integer = Convert.ToInt32(CDbl(Me.bitmap_2.Width) / 2)
            Dim num5 As Integer = Convert.ToInt32(CDbl(Me.bitmap_2.Height) / 2)
            point(0) = New System.Drawing.Point(num4, 0)
            point(1) = New System.Drawing.Point(Me.bitmap_2.Width, num5)
            point(2) = New System.Drawing.Point(num4, Me.bitmap_2.Height)
            point(3) = New System.Drawing.Point(0, num5)
            point(4) = New System.Drawing.Point(num4, 0)
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.Red)
                graphic.FillPolygon(solidBrush, point)
            End Using
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(50! * Me.float_0), Convert.ToInt32(50! * Me.float_0))
            Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_3)
                Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.Green)
                    graphic1.FillPolygon(solidBrush1, point)
                End Using
            End Using
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
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
                    For i As Integer = 1 To int0 Step 1
                        graphic.DrawImage(Me.bitmap_0(11), CSng((Me.int_2 * (i - 1))) * Me.float_0, 0!)
                    Next

                Else
                    Dim int01 As Integer = Me.int_0
                    For j As Integer = 1 To int01 Step 1
                        If (num1 >= 0L) Then
                            Dim num2 As Integer = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl((Me.int_0 - j)))))
                            If (num2 > 0 Or j = Me.int_0 Or j > Me.int_0 - Me.int_1) Then
                                flag = True
                            End If
                            If (flag) Then
                                If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                    graphic.DrawImage(Me.bitmap_1(num2), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                                Else
                                    graphic.DrawImage(Me.bitmap_0(num2), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                                End If
                            ElseIf (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                graphic.DrawImage(Me.bitmap_1(10), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                            Else
                                graphic.DrawImage(Me.bitmap_0(10), CSng(Convert.ToInt32(CDbl((Me.int_2 * (j - 1))) * 1.1)) * Me.float_0, 0!)
                            End If
                            num1 = CLng(Math.Round(CDbl(num1) - CDbl(num2) * Math.Pow(10, CDbl((Me.int_0 - j)))))
                        Else
                            If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                                graphic.DrawImage(Me.bitmap_1(11), CSng((Me.int_2 * (j - 1))) * Me.float_0, 0!)
                            Else
                                graphic.DrawImage(Me.bitmap_0(11), CSng((Me.int_2 * (j - 1))) * Me.float_0, 0!)
                            End If
                            num1 = Math.Abs(num1)
                        End If
                    Next

                End If
                If (Me.int_1 > 0) Then
                    If (Not (Me.double_0 >= CDbl(Me.float_1) And Me.double_0 <= CDbl(Me.float_2))) Then
                        graphic.DrawImage(Me.bitmap_3, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_0 - Me.int_1) * Me.int_2 - 50)) * Me.float_0)) * 1.1)), 440! * Me.float_0)
                    Else
                        graphic.DrawImage(Me.bitmap_2, CSng(Convert.ToInt32(CDbl((CSng(((Me.int_0 - Me.int_1) * Me.int_2 - 50)) * Me.float_0)) * 1.1)), 440! * Me.float_0)
                    End If
                End If
                painte.Graphics.DrawImage(Me.bitmap_4, 0, 0)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        MyBase.OnPaintBackground(pevent)
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
