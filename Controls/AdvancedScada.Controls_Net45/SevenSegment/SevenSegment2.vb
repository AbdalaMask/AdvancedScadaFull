Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class SevenSegment2
    Inherits Control

#Region "„ €Ì—« "
    Private Seg0 As Point()

    Private Seg1 As Point()

    Private Seg2 As Point()

    Private Seg3 As Point()

    Private Seg4 As Point()

    Private Seg5 As Point()

    Private Seg6 As Point()

    Private LED As Bitmap()

    Private RedLED As Bitmap()

    Private GreenLED As Bitmap()

    Private StaticImage As Bitmap

    Private RedDecimalImage As Bitmap

    Private GreenDecimalImage As Bitmap

    Private ImageRatio As Single

    Private m_ForeColorInLimits As Color

    Private m_ForeColorOverHighLimit As Color

    Private m_ForeColorUnderLowLimit As Color

    Private m_ForecolorHighLimitValue As Double

    Private m_ForecolorLowLimitValue As Double

    Private m_Value As Double

    Private m_ResolutionOfLastDigit As Decimal

    Private m_NumberOfDigits As Integer

    Private m_DecimalPosition As Integer

    Private m_ShowOffSegments As Boolean

    Private x As Integer

    Private m_InsetPercent As Integer

    Private m_TextCenterLocation As Point

    Private SegWidth As Integer

    Private _backBuffer As Bitmap

    Private LastWidth As Integer

    Private LastHeight As Integer

    Private BackgroundNeedsRefreshed As Boolean

    Private StaticImageRatio As Double
#End Region
#Region "Œ’«∆’"
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
            Return Me.m_DecimalPosition
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.m_DecimalPosition) Then
                Me.m_DecimalPosition = Math.Max(Math.Min(Me.m_NumberOfDigits - 1, value), 0)
                Me.RefreshImage()
            End If
        End Set
    End Property

    <Browsable(False)>
    Public Overrides Property ForeColor As Color

    Public Property ForecolorHighLimitValue As Double
        Get
            Return Me.m_ForecolorHighLimitValue
        End Get
        Set(ByVal value As Double)
            If (Me.m_ForecolorHighLimitValue <> value) Then
                Me.m_ForecolorHighLimitValue = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property ForeColorInLimits As Color
        Get
            Return Me.m_ForeColorInLimits
        End Get
        Set(ByVal value As Color)
            If (Me.m_ForeColorInLimits <> value) Then
                Me.m_ForeColorInLimits = value
                Me.RefreshImage()
            End If
        End Set
    End Property

    Public Property ForecolorLowLimitValue As Double
        Get
            Return Me.m_ForecolorLowLimitValue
        End Get
        Set(ByVal value As Double)
            If (Me.m_ForecolorLowLimitValue <> value) Then
                Me.m_ForecolorLowLimitValue = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property ForeColorOverHighLimit As Color
        Get
            Return Me.m_ForeColorOverHighLimit
        End Get
        Set(ByVal value As Color)
            If (Me.m_ForeColorOverHighLimit <> value) Then
                Me.m_ForeColorOverHighLimit = value
                Me.RefreshImage()
            End If
        End Set
    End Property

    Public Property ForeColorUnderLowLimit As Color
        Get
            Return Me.m_ForeColorUnderLowLimit
        End Get
        Set(ByVal value As Color)
            If (Me.m_ForeColorUnderLowLimit <> value) Then
                Me.m_ForeColorUnderLowLimit = value
                Me.RefreshImage()
            End If
        End Set
    End Property

    Public Property InsetPercent As Integer
        Get
            Return Me.m_InsetPercent
        End Get
        Set(ByVal value As Integer)
            Me.m_InsetPercent = Math.Min(Math.Max(0, value), 45)
            Me.AdjustSize()
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property NumberOfDigits As Integer
        Get
            Return Me.m_NumberOfDigits
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.m_NumberOfDigits) Then
                Me.m_NumberOfDigits = Math.Max(Math.Min(50, value), 1)
                Me.AdjustSize()
                Me.RefreshImage()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ResolutionOfLastDigit As Decimal
        Get
            Return Me.m_ResolutionOfLastDigit
        End Get
        Set(ByVal value As Decimal)
            If (Decimal.Compare(value, Decimal.Zero) <> 0) Then
                Me.m_ResolutionOfLastDigit = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property ShowOffSegments As Boolean
        Get
            Return Me.m_ShowOffSegments
        End Get
        Set(ByVal value As Boolean)
            If (Me.m_ShowOffSegments <> value) Then
                Me.m_ShowOffSegments = value
                Me.RefreshImage()
            End If
        End Set
    End Property

    Public Property TextCenterLocation As Point
        Get
            Return Me.m_TextCenterLocation
        End Get
        Set(ByVal value As Point)
            Me.m_TextCenterLocation = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property TextForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (value <> MyBase.ForeColor) Then
                MyBase.ForeColor = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property Value As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If (value <> Me.m_Value) Then
                Me.m_Value = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property
#End Region
#Region "„‘Ìœ« "
    Public Sub New()
        MyBase.New()
        ReDim Me.Seg0(11)
        ReDim Me.Seg1(11)
        ReDim Me.Seg2(11)
        ReDim Me.Seg3(11)
        ReDim Me.Seg4(6)
        ReDim Me.Seg5(16)
        ReDim Me.Seg6(16)
        ReDim Me.LED(11)
        ReDim Me.RedLED(11)
        ReDim Me.GreenLED(11)
        Me.m_ForeColorInLimits = Color.White
        Me.m_ForeColorOverHighLimit = Color.Red
        Me.m_ForeColorUnderLowLimit = Color.Yellow
        Me.m_ForecolorHighLimitValue = 999999
        Me.m_ForecolorLowLimitValue = -999999
        Me.m_ResolutionOfLastDigit = Decimal.One
        Me.m_NumberOfDigits = 5
        Me.m_DecimalPosition = 0
        Me.m_ShowOffSegments = True
        Me.SegWidth = 340
        Me.StaticImageRatio = 486 / (CDbl((340 * Me.m_NumberOfDigits)) * 1.1)

        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.Initialize()
        MyBase.BackColor = Color.Transparent
        MyBase.ForeColor = Color.White
        Me.AdjustSize()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                Dim length As Integer = CInt(Me.LED.Length) - 1
                Dim num As Integer = 0
                Do
                    If (Me.LED(num) IsNot Nothing) Then
                        Me.LED(num).Dispose()
                    End If
                    If (Me.GreenLED(num) IsNot Nothing) Then
                        Me.GreenLED(num).Dispose()
                    End If
                    num = num + 1
                Loop While num <= length
                If (Me.GreenDecimalImage IsNot Nothing) Then
                    Me.GreenDecimalImage.Dispose()
                End If
                If (Me.RedDecimalImage IsNot Nothing) Then
                    Me.RedDecimalImage.Dispose()
                End If
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
                If (Me._backBuffer IsNot Nothing) Then
                    Me._backBuffer.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region
#Region "«⁄«œ…  ⁄—Ì› «·«Õœ«À"
    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim flag As Boolean = False
        If (Me._backBuffer IsNot Nothing) Then
            Using graphic As Graphics = Graphics.FromImage(Me._backBuffer)
                graphic.Clear(Me.BackColor)
                If (Me.BackgroundImage IsNot Nothing) Then
                    If (Me.BackgroundImageLayout <> ImageLayout.Stretch) Then
                        graphic.DrawImage(Me.BackgroundImage, 0, 0)
                    Else
                        graphic.DrawImage(Me.BackgroundImage, 0, 0, MyBase.Width, MyBase.Height)
                    End If
                End If
                Dim double2 As Double = Me.m_Value * Math.Pow(10, CDbl(Me.m_DecimalPosition))
                If (Decimal.Compare(Me.m_ResolutionOfLastDigit, Decimal.Zero) <> 0) Then
                    double2 = Convert.ToDouble(Decimal.Multiply(New Decimal(CInt(Math.Round(double2 / Convert.ToDouble(Me.m_ResolutionOfLastDigit)))), Me.m_ResolutionOfLastDigit))
                End If
                Dim double21 As Boolean = Me.m_Value >= Me.m_ForecolorLowLimitValue
                Dim flag1 As Boolean = Me.m_Value >= Me.m_ForecolorHighLimitValue
                If (Not (double2 < Math.Pow(10, CDbl(Me.m_NumberOfDigits)) And double2 > -Math.Pow(10, CDbl((Me.m_NumberOfDigits - 1))))) Then
                    Dim int0 As Integer = Me.m_NumberOfDigits
                    For i As Integer = 1 To int0 Step 1
                        graphic.DrawImage(Me.GreenLED(11), CInt(Math.Round(CDbl((CSng((Me.SegWidth * (i - 1))) * Me.ImageRatio)) * 1.1)) + Me.x, Me.x)
                    Next

                Else
                    Dim num As Integer = Me.m_NumberOfDigits
                    Dim num1 As Integer = 1
                    Do
                        If (double2 >= 0) Then
                            Dim num2 As Integer = Convert.ToInt32(Math.Floor(double2 / Math.Pow(10, CDbl((Me.m_NumberOfDigits - num1)))))
                            If (num2 > 0 Or num1 = Me.m_NumberOfDigits Or num1 > Me.m_NumberOfDigits - Me.m_DecimalPosition) Then
                                flag = True
                            End If
                            If (flag) Then
                                If (flag1) Then
                                    graphic.DrawImage(Me.LED(num2), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                                ElseIf (Not double21) Then
                                    graphic.DrawImage(Me.GreenLED(num2), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                                Else
                                    graphic.DrawImage(Me.RedLED(num2), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                                End If
                            ElseIf (flag1) Then
                                graphic.DrawImage(Me.LED(10), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            ElseIf (Not double21) Then
                                graphic.DrawImage(Me.GreenLED(10), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            Else
                                graphic.DrawImage(Me.RedLED(10), CSng(Convert.ToInt32(CDbl((Me.SegWidth * (num1 - 1))) * 1.1)) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            End If
                            double2 = double2 - CDbl(num2) * Math.Pow(10, CDbl((Me.m_NumberOfDigits - num1)))
                        Else
                            If (flag1) Then
                                graphic.DrawImage(Me.LED(11), CSng((Me.SegWidth * (num1 - 1))) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            ElseIf (Not double21) Then
                                graphic.DrawImage(Me.GreenLED(11), CSng((Me.SegWidth * (num1 - 1))) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            Else
                                graphic.DrawImage(Me.RedLED(11), CSng((Me.SegWidth * (num1 - 1))) * Me.ImageRatio + CSng(Me.x), CSng(Me.x))
                            End If
                            double2 = Math.Abs(double2)
                        End If
                        num1 = num1 + 1
                    Loop While num1 <= num
                    If (Me.m_DecimalPosition > 0) Then
                        If (flag1) Then
                            graphic.DrawImage(Me.StaticImage, CSng((Convert.ToInt32(CDbl((CSng(((Me.m_NumberOfDigits - Me.m_DecimalPosition) * Me.SegWidth - 70)) * Me.ImageRatio)) * 1.1) + Me.x)), 400.0! * Me.ImageRatio + CSng(Me.x))
                        ElseIf (Not double21) Then
                            graphic.DrawImage(Me.GreenDecimalImage, CSng((Convert.ToInt32(CDbl((CSng(((Me.m_NumberOfDigits - Me.m_DecimalPosition) * Me.SegWidth - 70)) * Me.ImageRatio)) * 1.1) + Me.x)), 400.0! * Me.ImageRatio + CSng(Me.x))
                        Else
                            graphic.DrawImage(Me.RedDecimalImage, CSng((Convert.ToInt32(CDbl((CSng(((Me.m_NumberOfDigits - Me.m_DecimalPosition) * Me.SegWidth - 70)) * Me.ImageRatio)) * 1.1) + Me.x)), 400.0! * Me.ImageRatio + CSng(Me.x))
                        End If
                    End If
                End If
                If (Not String.IsNullOrEmpty(Me.Text)) Then
                    Dim sizeF As System.Drawing.SizeF = graphic.MeasureString(Me.Text, Me.Font)
                    Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl((CSng(Me.m_TextCenterLocation.X) - sizeF.Width / 2.0!)))), CInt(Math.Round(CDbl((CSng(Me.m_TextCenterLocation.Y) - sizeF.Height / 2.0!)))), CInt(Math.Ceiling(CDbl(sizeF.Width))), CInt(Math.Ceiling(CDbl((sizeF.Height + 2.0!)))))
                    graphic.DrawString(Me.Text, Me.Font, New SolidBrush(MyBase.ForeColor), rectangle)
                End If
                If (If(painte Is Nothing, False, painte.Graphics IsNot Nothing)) Then
                    painte.Graphics.DrawImage(Me._backBuffer, 0, 0)
                End If
            End Using
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me._backBuffer IsNot Nothing) Then
            Me._backBuffer.Dispose()
            Me._backBuffer = Nothing
        End If
        Me._Resize(Me, Nothing)
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub
#End Region
#Region "ÿ—ﬁ"
    Private Sub _Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (Not Me.BackgroundNeedsRefreshed AndAlso MyBase.Height <> Me.LastHeight Or MyBase.Width <> Me.LastWidth) Then
            Me.BackgroundNeedsRefreshed = True
            Me.AdjustSize()
            Me.LastWidth = MyBase.Width
            Me.LastHeight = MyBase.Height
            Me.BackgroundNeedsRefreshed = False
            Me.RefreshImage()
        End If
    End Sub

    Private Sub AdjustSize()
        Me.StaticImageRatio = 486 / (CDbl((340 * Me.m_NumberOfDigits)) * 1.1)
        Me.x = Math.Max(CInt(Math.Round(CDbl((MyBase.Height * Me.m_InsetPercent)) / 200)), CInt(Math.Round(CDbl((MyBase.Width * Me.m_InsetPercent)) / 200)))
        If (Me.LastHeight < MyBase.Height Or Me.LastWidth < MyBase.Width) Then
            If (CDbl((MyBase.Height - Me.x * 2)) / CDbl((MyBase.Width - Me.x * 2)) <= Me.StaticImageRatio) Then
                MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width - Me.x * 2)) * Me.StaticImageRatio)) + Me.x * 2
            Else
                MyBase.Width = CInt(Math.Round(CDbl((MyBase.Height - Me.x * 2)) / Me.StaticImageRatio)) + Me.x * 2
            End If
        ElseIf (CDbl((MyBase.Height - Me.x * 2)) / CDbl((MyBase.Width - Me.x * 2)) <= Me.StaticImageRatio) Then
            MyBase.Width = CInt(Math.Round(CDbl((MyBase.Height - Me.x * 2)) / Me.StaticImageRatio)) + Me.x * 2
        Else
            MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width - Me.x * 2)) * Me.StaticImageRatio)) + Me.x * 2
        End If
    End Sub

    Private Sub RefreshImage()
        Me.StaticImageRatio = 486 / (CDbl((340 * Me.m_NumberOfDigits)) * 1.1)
        Dim width As Single = CSng((CDbl((MyBase.Width - Me.x * 2)) / (CDbl((340 * Me.m_NumberOfDigits)) * 1.1)))
        Dim height As Single = CSng((CDbl((MyBase.Height - Me.x * 2)) / 486))
        If (width >= height) Then
            Me.ImageRatio = height
        Else
            Me.ImageRatio = width
        End If
        If (Me.ImageRatio > 0!) Then
            Dim num As Integer = Convert.ToInt32(340.0! * Me.ImageRatio)
            Dim num1 As Integer = Convert.ToInt32(486.0! * Me.ImageRatio)
            Dim num2 As Integer = 0
            Do
                If (Me.LED(num2) IsNot Nothing) Then
                    Me.LED(num2).Dispose()
                End If
                Me.LED(num2) = New Bitmap(num, num1)
                Using graphic As Graphics = Graphics.FromImage(Me.LED(num2))
                    graphic.ScaleTransform(CSng((CDbl(num) / 700)), CSng((CDbl(num) / 700)))
                    SevenSegment2.FillPolygon(graphic, num2, Me.m_ForeColorOverHighLimit, Me.m_ShowOffSegments)
                End Using
                num2 = num2 + 1
            Loop While num2 <= 11
            Dim num3 As Integer = 0
            Do
                If (Me.RedLED(num3) IsNot Nothing) Then
                    Me.RedLED(num3).Dispose()
                End If
                Me.RedLED(num3) = New Bitmap(num, num1)
                Using graphic1 As Graphics = Graphics.FromImage(Me.RedLED(num3))
                    graphic1.ScaleTransform(CSng((CDbl(num) / 700)), CSng((CDbl(num) / 700)))
                    SevenSegment2.FillPolygon(graphic1, num3, Me.m_ForeColorInLimits, Me.m_ShowOffSegments)
                End Using
                num3 = num3 + 1
            Loop While num3 <= 11
            Dim num4 As Integer = 0
            Do
                If (Me.GreenLED(num4) IsNot Nothing) Then
                    Me.GreenLED(num4).Dispose()
                End If
                Me.GreenLED(num4) = New Bitmap(num, num1)
                Using graphic2 As Graphics = Graphics.FromImage(Me.GreenLED(num4))
                    graphic2.ScaleTransform(CSng((CDbl(num) / 700)), CSng((CDbl(num) / 700)))
                    SevenSegment2.FillPolygon(graphic2, num4, Me.ForeColorUnderLowLimit, Me.m_ShowOffSegments)
                End Using
                num4 = num4 + 1
            Loop While num4 <= 11
            Me.StaticImage = New Bitmap(Convert.ToInt32(75.0! * Me.ImageRatio), Convert.ToInt32(75.0! * Me.ImageRatio))
            Dim point() As System.Drawing.Point = {New System.Drawing.Point(CInt(Math.Round(CDbl(Me.StaticImage.Width) / 2)), 0), New System.Drawing.Point(Me.StaticImage.Width, CInt(Math.Round(CDbl(Me.StaticImage.Height) / 2))), New System.Drawing.Point(CInt(Math.Round(CDbl(Me.StaticImage.Width) / 2)), Me.StaticImage.Height), New System.Drawing.Point(0, CInt(Math.Round(CDbl(Me.StaticImage.Height) / 2))), New System.Drawing.Point(CInt(Math.Round(CDbl(Me.StaticImage.Width) / 2)), 0)}
            Using graphic3 As Graphics = Graphics.FromImage(Me.StaticImage)
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.m_ForeColorOverHighLimit)
                    graphic3.FillPolygon(solidBrush, point)
                End Using
            End Using
            Me.GreenDecimalImage = New Bitmap(Convert.ToInt32(75.0! * Me.ImageRatio), Convert.ToInt32(75.0! * Me.ImageRatio))
            Using graphic4 As Graphics = Graphics.FromImage(Me.GreenDecimalImage)
                Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.m_ForeColorUnderLowLimit)
                    graphic4.FillPolygon(solidBrush1, point)
                End Using
            End Using
            Me.RedDecimalImage = New Bitmap(Convert.ToInt32(75.0! * Me.ImageRatio), Convert.ToInt32(75.0! * Me.ImageRatio))
            Using graphic5 As Graphics = Graphics.FromImage(Me.RedDecimalImage)
                Using solidBrush2 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.m_ForeColorInLimits)
                    graphic5.FillPolygon(solidBrush2, point)
                End Using
            End Using
            If (Me._backBuffer IsNot Nothing) Then
                Me._backBuffer.Dispose()
            End If
            Me._backBuffer = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub Initialize()
        Me.Seg0(0) = New Point(75, 110)
        Me.Seg0(1) = New Point(47, 415)
        Me.Seg0(2) = New Point(98, 471)
        Me.Seg0(3) = New Point(157, 417)
        Me.Seg0(4) = New Point(182, 125)
        Me.Seg0(5) = New Point(88, 21)
        Me.Seg0(6) = New Point(88, 21)
        Me.Seg0(7) = New Point(85, 27)
        Me.Seg0(8) = New Point(82, 32)
        Me.Seg0(9) = New Point(80, 38)
        Me.Seg0(10) = New Point(79, 44)
        Me.Seg0(11) = New Point(74, 110)
        Me.Seg1(0) = New Point(580, 471)
        Me.Seg1(1) = New Point(641, 415)
        Me.Seg1(2) = New Point(668, 110)
        Me.Seg1(3) = New Point(673, 44)
        Me.Seg1(4) = New Point(673, 44)
        Me.Seg1(5) = New Point(674, 38)
        Me.Seg1(6) = New Point(673, 31)
        Me.Seg1(7) = New Point(671, 26)
        Me.Seg1(8) = New Point(669, 20)
        Me.Seg1(9) = New Point(557, 123)
        Me.Seg1(10) = New Point(531, 417)
        Me.Seg1(11) = New Point(580, 471)
        Me.Seg2(0) = New Point(96, 499)
        Me.Seg2(1) = New Point(35, 555)
        Me.Seg2(2) = New Point(8, 858)
        Me.Seg2(3) = New Point(3, 924)
        Me.Seg2(4) = New Point(3, 924)
        Me.Seg2(5) = New Point(2, 930)
        Me.Seg2(6) = New Point(3, 937)
        Me.Seg2(7) = New Point(5, 942)
        Me.Seg2(8) = New Point(7, 948)
        Me.Seg2(9) = New Point(119, 845)
        Me.Seg2(10) = New Point(145, 553)
        Me.Seg2(11) = New Point(96, 499)
        Me.Seg3(0) = New Point(602, 858)
        Me.Seg3(1) = New Point(629, 555)
        Me.Seg3(2) = New Point(578, 499)
        Me.Seg3(3) = New Point(519, 553)
        Me.Seg3(4) = New Point(493, 847)
        Me.Seg3(5) = New Point(586, 949)
        Me.Seg3(6) = New Point(586, 949)
        Me.Seg3(7) = New Point(590, 943)
        Me.Seg3(8) = New Point(593, 937)
        Me.Seg3(9) = New Point(595, 931)
        Me.Seg3(10) = New Point(597, 924)
        Me.Seg3(11) = New Point(602, 858)
        Me.Seg4(0) = New Point(515, 430)
        Me.Seg4(1) = New Point(171, 430)
        Me.Seg4(2) = New Point(111, 485)
        Me.Seg4(3) = New Point(161, 540)
        Me.Seg4(4) = New Point(505, 540)
        Me.Seg4(5) = New Point(565, 485)
        Me.Seg4(6) = New Point(515, 430)
        Me.Seg5(0) = New Point(475, 858)
        Me.Seg5(1) = New Point(133, 858)
        Me.Seg5(2) = New Point(21, 962)
        Me.Seg5(3) = New Point(21, 962)
        Me.Seg5(4) = New Point(26, 965)
        Me.Seg5(5) = New Point(31, 966)
        Me.Seg5(6) = New Point(37, 968)
        Me.Seg5(7) = New Point(43, 968)
        Me.Seg5(8) = New Point(109, 968)
        Me.Seg5(9) = New Point(483, 968)
        Me.Seg5(10) = New Point(549, 968)
        Me.Seg5(11) = New Point(549, 968)
        Me.Seg5(12) = New Point(554, 968)
        Me.Seg5(13) = New Point(560, 967)
        Me.Seg5(14) = New Point(565, 965)
        Me.Seg5(15) = New Point(570, 962)
        Me.Seg5(16) = New Point(475, 858)
        Me.Seg6(0) = New Point(192, 110)
        Me.Seg6(1) = New Point(543, 110)
        Me.Seg6(2) = New Point(655, 6)
        Me.Seg6(3) = New Point(655, 6)
        Me.Seg6(4) = New Point(650, 4)
        Me.Seg6(5) = New Point(645, 2)
        Me.Seg6(6) = New Point(639, 0)
        Me.Seg6(7) = New Point(633, 0)
        Me.Seg6(8) = New Point(567, 0)
        Me.Seg6(9) = New Point(193, 0)
        Me.Seg6(10) = New Point(127, 0)
        Me.Seg6(11) = New Point(127, 0)
        Me.Seg6(12) = New Point(121, 0)
        Me.Seg6(13) = New Point(115, 2)
        Me.Seg6(14) = New Point(109, 4)
        Me.Seg6(15) = New Point(103, 7)
        Me.Seg6(16) = New Point(197, 110)
    End Sub

    Friend Shared Function segmentPoint0() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(75, 110), New System.Drawing.Point(47, 415), New System.Drawing.Point(98, 471), New System.Drawing.Point(157, 417), New System.Drawing.Point(182, 125), New System.Drawing.Point(88, 21), New System.Drawing.Point(88, 21), New System.Drawing.Point(85, 27), New System.Drawing.Point(82, 32), New System.Drawing.Point(80, 38), New System.Drawing.Point(79, 44), New System.Drawing.Point(74, 110)}
        Return point
    End Function

    Friend Shared Function segmentPoint1() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(580, 471), New System.Drawing.Point(641, 415), New System.Drawing.Point(668, 110), New System.Drawing.Point(673, 44), New System.Drawing.Point(673, 44), New System.Drawing.Point(674, 38), New System.Drawing.Point(673, 31), New System.Drawing.Point(671, 26), New System.Drawing.Point(669, 20), New System.Drawing.Point(557, 123), New System.Drawing.Point(531, 417), New System.Drawing.Point(580, 471)}
        Return point
    End Function

    Friend Shared Function segmentPoint2() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(96, 499), New System.Drawing.Point(35, 555), New System.Drawing.Point(8, 858), New System.Drawing.Point(3, 924), New System.Drawing.Point(3, 924), New System.Drawing.Point(2, 930), New System.Drawing.Point(3, 937), New System.Drawing.Point(5, 942), New System.Drawing.Point(7, 948), New System.Drawing.Point(119, 845), New System.Drawing.Point(145, 553), New System.Drawing.Point(96, 499)}
        Return point
    End Function

    Friend Shared Function segmentPoint3() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(602, 858), New System.Drawing.Point(629, 555), New System.Drawing.Point(578, 499), New System.Drawing.Point(519, 553), New System.Drawing.Point(493, 847), New System.Drawing.Point(586, 949), New System.Drawing.Point(586, 949), New System.Drawing.Point(590, 943), New System.Drawing.Point(593, 937), New System.Drawing.Point(595, 931), New System.Drawing.Point(597, 924), New System.Drawing.Point(602, 858)}
        Return point
    End Function

    Friend Shared Function segmentPoint4() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(515, 430), New System.Drawing.Point(171, 430), New System.Drawing.Point(111, 485), New System.Drawing.Point(161, 540), New System.Drawing.Point(505, 540), New System.Drawing.Point(565, 485), New System.Drawing.Point(515, 430)}
        Return point
    End Function

    Friend Shared Function segmentPoint5() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(475, 858), New System.Drawing.Point(133, 858), New System.Drawing.Point(21, 962), New System.Drawing.Point(21, 962), New System.Drawing.Point(26, 965), New System.Drawing.Point(31, 966), New System.Drawing.Point(37, 968), New System.Drawing.Point(43, 968), New System.Drawing.Point(109, 968), New System.Drawing.Point(483, 968), New System.Drawing.Point(549, 968), New System.Drawing.Point(549, 968), New System.Drawing.Point(554, 968), New System.Drawing.Point(560, 967), New System.Drawing.Point(565, 965), New System.Drawing.Point(570, 962), New System.Drawing.Point(475, 858)}
        Return point
    End Function

    Friend Shared Function segmentPoint6() As System.Drawing.Point()
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(192, 110), New System.Drawing.Point(543, 110), New System.Drawing.Point(655, 6), New System.Drawing.Point(655, 6), New System.Drawing.Point(650, 4), New System.Drawing.Point(645, 2), New System.Drawing.Point(639, 0), New System.Drawing.Point(633, 0), New System.Drawing.Point(567, 0), New System.Drawing.Point(193, 0), New System.Drawing.Point(127, 0), New System.Drawing.Point(127, 0), New System.Drawing.Point(121, 0), New System.Drawing.Point(115, 2), New System.Drawing.Point(109, 4), New System.Drawing.Point(103, 7), New System.Drawing.Point(197, 110)}
        Return point
    End Function

    Friend Shared Sub FillPolygon(ByVal g As Graphics, ByVal Digit As Integer, ByVal DigitColor As Color, ByVal ShowOffSegments As Boolean)
        Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(DigitColor)
            Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(40, CInt(DigitColor.R), CInt(DigitColor.G), CInt(DigitColor.B)))
                g.CompositingQuality = CompositingQuality.HighQuality
                g.SmoothingMode = SmoothingMode.AntiAlias
                If (Digit = 0 Or Digit = 4 Or Digit = 5 Or Digit = 6 Or Digit = 8 Or Digit = 9) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint0())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint0())
                End If
                If (Not (Digit = 5 Or Digit = 6 Or Digit = 10 Or Digit = 11)) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint1())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint1())
                End If
                If (Digit = 0 Or Digit = 2 Or Digit = 6 Or Digit = 8) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint2())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint2())
                End If
                If (Not (Digit = 2 Or Digit = 10 Or Digit = 11)) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint3())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint3())
                End If
                If (Not (Digit = 0 Or Digit = 1 Or Digit = 7 Or Digit = 10)) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint4())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint4())
                End If
                If (Not (Digit = 1 Or Digit = 4 Or Digit = 7 Or Digit = 9 Or Digit = 10 Or Digit = 11)) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint5())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint5())
                End If
                If (Not (Digit = 1 Or Digit = 4 Or Digit = 10 Or Digit = 11)) Then
                    g.FillPolygon(solidBrush, SevenSegment2.segmentPoint6())
                ElseIf (ShowOffSegments) Then
                    g.FillPolygon(solidBrush1, SevenSegment2.segmentPoint6())
                End If
            End Using
        End Using
    End Sub
#End Region
    Public Event ValueChanged As EventHandler

End Class
