Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms


Public Class SevenSegment2
        Inherits Control
        Public Event ValueChanged As EventHandler
#Region "«·„ €Ì—« "
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

        Private SegWidth As Integer

        Private _backBuffer As Bitmap

        Private LastWidth As Integer

        Private LastHeight As Integer

        Private StaticImageRatio As Single
#End Region
#Region "«·Œ’«∆’"
        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim exStyle As CreateParams = MyBase.CreateParams
                exStyle.ExStyle = exStyle.ExStyle Or 32
                Return exStyle
            End Get
        End Property
        <Category("Numeric Display")>
        Public Property DecimalPosition() As Integer
            Get
                Return m_DecimalPosition
            End Get
            Set(ByVal value As Integer)
                If (value <> m_DecimalPosition) Then
                    m_DecimalPosition = Math.Max(Math.Min(m_NumberOfDigits - 1, value), 0)
                    RefreshImage()
                End If
            End Set
        End Property

        <Browsable(False)>
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        Public Property ForecolorHighLimitValue() As Double
            Get

                Return m_ForecolorHighLimitValue
            End Get
            Set(ByVal value As Double)
                If (m_ForecolorHighLimitValue <> value) Then
                    m_ForecolorHighLimitValue = value
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        Public Property ForeColorInLimits() As Color
            Get

                Return m_ForeColorInLimits
            End Get
            Set(ByVal value As Color)
                If (m_ForeColorInLimits <> value) Then
                    m_ForeColorInLimits = value
                    RefreshImage()
                End If
            End Set
        End Property

        Public Property ForecolorLowLimitValue() As Double
            Get

                Return m_ForecolorLowLimitValue
            End Get
            Set(ByVal value As Double)
                If (m_ForecolorLowLimitValue <> value) Then
                    m_ForecolorLowLimitValue = value
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        Public Property ForeColorOverHighLimit() As Color
            Get

                Return m_ForeColorOverHighLimit
            End Get
            Set(ByVal value As Color)
                If (m_ForeColorOverHighLimit <> value) Then
                    m_ForeColorOverHighLimit = value
                    RefreshImage()
                End If
            End Set
        End Property

        Public Property ForeColorUnderLowLimit() As Color
            Get

                Return m_ForeColorUnderLowLimit
            End Get
            Set(ByVal value As Color)
                If (m_ForeColorUnderLowLimit <> value) Then
                    m_ForeColorUnderLowLimit = value
                    RefreshImage()
                End If
            End Set
        End Property

        <Category("Numeric Display")>
        Public Property NumberOfDigits() As Integer
            Get
                Return m_NumberOfDigits
            End Get
            Set(ByVal value As Integer)
                If (value <> m_NumberOfDigits) Then
                    m_NumberOfDigits = Math.Max(Math.Min(50, value), 1)
                    AdjustSize()
                    RefreshImage()
                End If
            End Set
        End Property

        <Category("Numeric Display")>
        Public Property ResolutionOfLastDigit() As Decimal
            Get
                Return m_ResolutionOfLastDigit
            End Get
            Set(ByVal value As Decimal)
                If (Decimal.Compare(value, Decimal.Zero) <> 0) Then
                    m_ResolutionOfLastDigit = value
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        Public Property ShowOffSegments() As Boolean
            Get

                Return m_ShowOffSegments
            End Get
            Set(ByVal value As Boolean)
                If (m_ShowOffSegments <> value) Then
                    m_ShowOffSegments = value
                    RefreshImage()
                End If
            End Set
        End Property

        <Category("Numeric Display")>
        Public Property Value() As Double
            Get
                Return m_Value
            End Get
            Set(ByVal value As Double)
                If (value <> m_Value) Then
                    m_Value = value
                    MyBase.Invalidate()
                    OnValueChanged(EventArgs.Empty)
                End If
            End Set
        End Property
#End Region
#Region "«·„‘Ìœ« "
        Public Sub New()
            MyBase.New()
            ReDim Seg0(11)
            ReDim Seg1(11)
            ReDim Seg2(11)
            ReDim Seg3(11)
            ReDim Seg4(6)
            ReDim Seg5(16)
            ReDim Seg6(16)
            ReDim LED(11)
            ReDim RedLED(11)
            ReDim GreenLED(11)
            m_ForeColorInLimits = Color.White
            m_ForeColorOverHighLimit = Color.Red
            m_ForeColorUnderLowLimit = Color.Yellow
            m_ForecolorHighLimitValue = 999999
            m_ForecolorLowLimitValue = -999999
            m_ResolutionOfLastDigit = Decimal.One
            m_NumberOfDigits = 5
            m_DecimalPosition = 0
            m_ShowOffSegments = True
            SegWidth = 340
            StaticImageRatio = CSng((486 / (340 * m_NumberOfDigits * 1.1)))
            MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
            Initialize()
            MyBase.BackColor = Color.Transparent
            AdjustSize()
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If (disposing) Then
                    StaticImage.Dispose()
                    Dim length As Integer = RedLED.Length - 1
                    Dim i As Integer = 0
                    Do
                        If (RedLED(i) IsNot Nothing) Then
                            RedLED(i).Dispose()
                        End If
                        If (GreenLED(i) IsNot Nothing) Then
                            GreenLED(i).Dispose()
                        End If
                        i = i + 1
                    Loop While i <= length
                    If (GreenDecimalImage IsNot Nothing) Then
                        GreenDecimalImage.Dispose()
                    End If
                    _backBuffer.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub
#End Region
#Region "ÿ—ﬁ"

        Private Sub _Resize(ByVal sender As Object, ByVal e As EventArgs)
            If (MyBase.Height <> LastHeight OrElse MyBase.Width <> LastWidth) Then
                AdjustSize()
                LastWidth = MyBase.Width
                LastHeight = MyBase.Height
                RefreshImage()
            End If
        End Sub
        Private Sub AdjustSize()
            StaticImageRatio = CSng((486 / (340 * m_NumberOfDigits * 1.1)))
            If (LastHeight < MyBase.Height OrElse LastWidth < MyBase.Width) Then
                If (MyBase.Height / MyBase.Width <= StaticImageRatio) Then
                    MyBase.Height = CInt(Math.Round(CSng(MyBase.Width) * StaticImageRatio))
                Else
                    MyBase.Width = CInt(Math.Round(CSng(MyBase.Height) / StaticImageRatio))
                End If
            ElseIf (MyBase.Height / MyBase.Width <= StaticImageRatio) Then
                MyBase.Width = CInt(Math.Round(CSng(MyBase.Height) / StaticImageRatio))
            Else
                MyBase.Height = CInt(Math.Round(CSng(MyBase.Width) * StaticImageRatio))
            End If
        End Sub
        Private Sub RefreshImage()
            StaticImageRatio = CSng((486 / (340 * m_NumberOfDigits * 1.1)))
            Dim WidthRatio As Single = CDbl(MyBase.Width) / (CDbl((340 * m_NumberOfDigits)) * 1.1)
            Dim HeightRatio As Single = CDbl(MyBase.Height) / 486
            If (WidthRatio >= HeightRatio) Then
                ImageRatio = HeightRatio
            Else
                ImageRatio = WidthRatio
            End If
            If (ImageRatio > 0.0!) Then
                Dim LastWidth As Integer = Convert.ToInt32(340.0! * ImageRatio)
                Dim LastHeight As Integer = Convert.ToInt32(486.0! * ImageRatio)
                Dim i As Integer
                For i = 0 To 11
                    If (LED(i) IsNot Nothing) Then
                        LED(i).Dispose()
                    End If
                    LED(i) = New Bitmap(LastWidth, LastHeight)
                    Using gr_dest As Graphics = Graphics.FromImage(LED(i))
                        gr_dest.ScaleTransform(CDbl(LastWidth) / 700, CDbl(LastWidth) / 700)
                        SetNumber(gr_dest, i, m_ForeColorOverHighLimit, m_ShowOffSegments)
                    End Using
                Next

                Dim y As Integer
                For y = 0 To 11
                    If (RedLED(y) IsNot Nothing) Then
                        RedLED(y).Dispose()
                    End If
                    RedLED(y) = New Bitmap(LastWidth, LastHeight)
                    Using gr_dest1 As Graphics = Graphics.FromImage(RedLED(y))
                        gr_dest1.ScaleTransform(CDbl(LastWidth) / 700, CDbl(LastWidth) / 700)
                        SetNumber(gr_dest1, y, m_ForeColorInLimits, m_ShowOffSegments)
                    End Using
                Next

                Dim x As Integer
                For x = 0 To 11
                    If (GreenLED(x) IsNot Nothing) Then
                        GreenLED(x).Dispose()
                    End If
                    GreenLED(x) = New Bitmap(LastWidth, LastHeight)
                    Using gr_dest2 As Graphics = Graphics.FromImage(GreenLED(x))
                        gr_dest2.ScaleTransform(CDbl(LastWidth) / 700, CDbl(LastWidth) / 700)
                        SetNumber(gr_dest2, x, ForeColorUnderLowLimit, m_ShowOffSegments)
                    End Using
                Next

                StaticImage = New Bitmap(Convert.ToInt32(75.0! * ImageRatio), Convert.ToInt32(75.0! * ImageRatio))
                Dim point() As Point = {New Point(Math.Round(CDbl(StaticImage.Width) / 2), 0), New Point(StaticImage.Width, Math.Round(CDbl(StaticImage.Height) / 2)), New Point(Math.Round(CDbl(StaticImage.Width) / 2), StaticImage.Height), New Point(0, Math.Round(CDbl(StaticImage.Height) / 2)), New Point(Math.Round(CDbl(StaticImage.Width) / 2), 0)}
                Using gr_dest3 As Graphics = Graphics.FromImage(StaticImage)
                    Using solidBrush As SolidBrush = New SolidBrush(m_ForeColorOverHighLimit)
                        gr_dest3.FillPolygon(solidBrush, point)
                    End Using
                End Using

                GreenDecimalImage = New Bitmap(Convert.ToInt32(75.0! * ImageRatio), Convert.ToInt32(75.0! * ImageRatio))
                Using gr_dest4 As Graphics = Graphics.FromImage(GreenDecimalImage)
                    Using solidBrush1 As SolidBrush = New SolidBrush(m_ForeColorUnderLowLimit)
                        gr_dest4.FillPolygon(solidBrush1, point)
                    End Using
                End Using

                RedDecimalImage = New Bitmap(Convert.ToInt32(75.0! * ImageRatio), Convert.ToInt32(75.0! * ImageRatio))
                Using gr_dest5 As Graphics = Graphics.FromImage(RedDecimalImage)
                    Using solidBrush2 As SolidBrush = New SolidBrush(m_ForeColorInLimits)
                        gr_dest5.FillPolygon(solidBrush2, point)
                    End Using
                End Using

                If (_backBuffer IsNot Nothing) Then
                    _backBuffer.Dispose()
                End If
                _backBuffer = New Bitmap(MyBase.Width, MyBase.Height)
                MyBase.Invalidate()
            End If
        End Sub
        Private Sub Initialize()
            Seg0(0) = New Point(75, 110)
            Seg0(1) = New Point(47, 415)
            Seg0(2) = New Point(98, 471)
            Seg0(3) = New Point(157, 417)
            Seg0(4) = New Point(182, 125)
            Seg0(5) = New Point(88, 21)
            Seg0(6) = New Point(88, 21)
            Seg0(7) = New Point(85, 27)
            Seg0(8) = New Point(82, 32)
            Seg0(9) = New Point(80, 38)
            Seg0(10) = New Point(79, 44)
            Seg0(11) = New Point(74, 110)
            Seg1(0) = New Point(580, 471)
            Seg1(1) = New Point(641, 415)
            Seg1(2) = New Point(668, 110)
            Seg1(3) = New Point(673, 44)
            Seg1(4) = New Point(673, 44)
            Seg1(5) = New Point(674, 38)
            Seg1(6) = New Point(673, 31)
            Seg1(7) = New Point(671, 26)
            Seg1(8) = New Point(669, 20)
            Seg1(9) = New Point(557, 123)
            Seg1(10) = New Point(531, 417)
            Seg1(11) = New Point(580, 471)
            Seg2(0) = New Point(96, 499)
            Seg2(1) = New Point(35, 555)
            Seg2(2) = New Point(8, 858)
            Seg2(3) = New Point(3, 924)
            Seg2(4) = New Point(3, 924)
            Seg2(5) = New Point(2, 930)
            Seg2(6) = New Point(3, 937)
            Seg2(7) = New Point(5, 942)
            Seg2(8) = New Point(7, 948)
            Seg2(9) = New Point(119, 845)
            Seg2(10) = New Point(145, 553)
            Seg2(11) = New Point(96, 499)
            Seg3(0) = New Point(602, 858)
            Seg3(1) = New Point(629, 555)
            Seg3(2) = New Point(578, 499)
            Seg3(3) = New Point(519, 553)
            Seg3(4) = New Point(493, 847)
            Seg3(5) = New Point(586, 949)
            Seg3(6) = New Point(586, 949)
            Seg3(7) = New Point(590, 943)
            Seg3(8) = New Point(593, 937)
            Seg3(9) = New Point(595, 931)
            Seg3(10) = New Point(597, 924)
            Seg3(11) = New Point(602, 858)
            Seg4(0) = New Point(515, 430)
            Seg4(1) = New Point(171, 430)
            Seg4(2) = New Point(111, 485)
            Seg4(3) = New Point(161, 540)
            Seg4(4) = New Point(505, 540)
            Seg4(5) = New Point(565, 485)
            Seg4(6) = New Point(515, 430)
            Seg5(0) = New Point(475, 858)
            Seg5(1) = New Point(133, 858)
            Seg5(2) = New Point(21, 962)
            Seg5(3) = New Point(21, 962)
            Seg5(4) = New Point(26, 965)
            Seg5(5) = New Point(31, 966)
            Seg5(6) = New Point(37, 968)
            Seg5(7) = New Point(43, 968)
            Seg5(8) = New Point(109, 968)
            Seg5(9) = New Point(483, 968)
            Seg5(10) = New Point(549, 968)
            Seg5(11) = New Point(549, 968)
            Seg5(12) = New Point(554, 968)
            Seg5(13) = New Point(560, 967)
            Seg5(14) = New Point(565, 965)
            Seg5(15) = New Point(570, 962)
            Seg5(16) = New Point(475, 858)
            Seg6(0) = New Point(192, 110)
            Seg6(1) = New Point(543, 110)
            Seg6(2) = New Point(655, 6)
            Seg6(3) = New Point(655, 6)
            Seg6(4) = New Point(650, 4)
            Seg6(5) = New Point(645, 2)
            Seg6(6) = New Point(639, 0)
            Seg6(7) = New Point(633, 0)
            Seg6(8) = New Point(567, 0)
            Seg6(9) = New Point(193, 0)
            Seg6(10) = New Point(127, 0)
            Seg6(11) = New Point(127, 0)
            Seg6(12) = New Point(121, 0)
            Seg6(13) = New Point(115, 2)
            Seg6(14) = New Point(109, 4)
            Seg6(15) = New Point(103, 7)
            Seg6(16) = New Point(197, 110)
        End Sub
        Friend Shared Function SetALL(ByVal Number As Integer) As Point()
            Dim PrSetALL() As Point
            Select Case Number
                Case 0
                    PrSetALL = New Point() {New Point(75, 110), New Point(47, 415), New Point(98, 471), New Point(157, 417), New Point(182, 125), New Point(88, 21), New Point(88, 21), New Point(85, 27), New Point(82, 32), New Point(80, 38), New Point(79, 44), New Point(74, 110)}
                    Return PrSetALL
                    Exit Select
                Case 1
                    PrSetALL = New Point() {New Point(580, 471), New Point(641, 415), New Point(668, 110), New Point(673, 44), New Point(673, 44), New Point(674, 38), New Point(673, 31), New Point(671, 26), New Point(669, 20), New Point(557, 123), New Point(531, 417), New Point(580, 471)}
                    Return PrSetALL
                    Exit Select
                Case 2
                    PrSetALL = New Point() {New Point(96, 499), New Point(35, 555), New Point(8, 858), New Point(3, 924), New Point(3, 924), New Point(2, 930), New Point(3, 937), New Point(5, 942), New Point(7, 948), New Point(119, 845), New Point(145, 553), New Point(96, 499)}
                    Return PrSetALL
                    Exit Select
                Case 3
                    PrSetALL = New Point() {New Point(602, 858), New Point(629, 555), New Point(578, 499), New Point(519, 553), New Point(493, 847), New Point(586, 949), New Point(586, 949), New Point(590, 943), New Point(593, 937), New Point(595, 931), New Point(597, 924), New Point(602, 858)}
                    Return PrSetALL
                    Exit Select
                Case 4
                    PrSetALL = New Point() {New Point(515, 430), New Point(171, 430), New Point(111, 485), New Point(161, 540), New Point(505, 540), New Point(565, 485), New Point(515, 430)}
                    Return PrSetALL
                    Exit Select
                Case 5
                    PrSetALL = New Point() {New Point(475, 858), New Point(133, 858), New Point(21, 962), New Point(21, 962), New Point(26, 965), New Point(31, 966), New Point(37, 968), New Point(43, 968), New Point(109, 968), New Point(483, 968), New Point(549, 968), New Point(549, 968), New Point(554, 968), New Point(560, 967), New Point(565, 965), New Point(570, 962), New Point(475, 858)}
                    Return PrSetALL
                    Exit Select
                Case 6

                    PrSetALL = New Point() {New Point(192, 110), New Point(543, 110), New Point(655, 6), New Point(655, 6), New Point(650, 4), New Point(645, 2), New Point(639, 0), New Point(633, 0), New Point(567, 0), New Point(193, 0), New Point(127, 0), New Point(127, 0), New Point(121, 0), New Point(115, 2), New Point(109, 4), New Point(103, 7), New Point(197, 110)}
                    Return PrSetALL
                    Exit Select
            End Select


            Return Nothing
        End Function
        Friend Shared Sub SetNumber(ByVal G0 As Graphics, ByVal m_intCount As Integer, ByVal m_color As Color, ByVal m_bool As Boolean)
            Using solidBrush As SolidBrush = New SolidBrush(m_color)
                Using solidBrush1 As SolidBrush = New SolidBrush(Color.FromArgb(40, m_color.R, m_color.G, m_color.B))
                    G0.CompositingQuality = CompositingQuality.HighQuality
                    G0.SmoothingMode = SmoothingMode.AntiAlias
                    If (m_intCount = 0 OrElse m_intCount = 4 OrElse m_intCount = 5 OrElse m_intCount = 6 OrElse m_intCount = 8 OrElse m_intCount = 9) Then
                        G0.FillPolygon(solidBrush, SetALL(0))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(0))
                    End If
                    If (Not (m_intCount = 5 OrElse m_intCount = 6 OrElse m_intCount = 10 OrElse m_intCount = 11)) Then
                        G0.FillPolygon(solidBrush, SetALL(1))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(1))
                    End If
                    If (m_intCount = 0 OrElse m_intCount = 2 OrElse m_intCount = 6 OrElse m_intCount = 8) Then
                        G0.FillPolygon(solidBrush, SetALL(2))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(2))
                    End If
                    If (Not (m_intCount = 2 OrElse m_intCount = 10 OrElse m_intCount = 11)) Then
                        G0.FillPolygon(solidBrush, SetALL(3))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(3))
                    End If
                    If (Not (m_intCount = 0 OrElse m_intCount = 1 OrElse m_intCount = 7 OrElse m_intCount = 10)) Then
                        G0.FillPolygon(solidBrush, SetALL(4))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(4))
                    End If
                    If (Not (m_intCount = 1 OrElse m_intCount = 4 OrElse m_intCount = 7 OrElse m_intCount = 9 OrElse m_intCount = 10 OrElse m_intCount = 11)) Then
                        G0.FillPolygon(solidBrush, SetALL(5))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(5))
                    End If
                    If (Not (m_intCount = 1 OrElse m_intCount = 4 OrElse m_intCount = 10 OrElse m_intCount = 11)) Then
                        G0.FillPolygon(solidBrush, SetALL(6))
                    ElseIf (m_bool) Then
                        G0.FillPolygon(solidBrush1, SetALL(6))
                    End If
                End Using
            End Using
        End Sub
#End Region
#Region "«⁄«œ…  ⁄—Ì› «·«Õœ«À"

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim DigitsStarted As Boolean = False
            If (_backBuffer IsNot Nothing) Then
                Using g As Graphics = Graphics.FromImage(_backBuffer)
                    g.Clear(BackColor)
                    If (BackgroundImage IsNot Nothing) Then
                        If (BackgroundImageLayout <> ImageLayout.Stretch) Then
                            g.DrawImage(BackgroundImage, 0, 0)
                        Else
                            g.DrawImage(BackgroundImage, 0, 0, MyBase.Width, MyBase.Height)
                        End If
                    End If
                    Dim one As Double = m_Value * Math.Pow(10, m_DecimalPosition)
                    If (Decimal.Compare(m_ResolutionOfLastDigit, Decimal.Zero) <> 0) Then
                        one = Convert.ToDouble(Decimal.Multiply(New Decimal(CInt(Math.Round(one / Convert.ToDouble(m_ResolutionOfLastDigit)))), m_ResolutionOfLastDigit))
                    End If
                    Dim m_DecimalPos As Boolean = m_Value >= m_ForecolorLowLimitValue
                    Dim m_ForecolorHigh As Boolean = m_Value >= m_ForecolorHighLimitValue
                    If (Not (one < Math.Pow(10, m_NumberOfDigits) AndAlso one > -Math.Pow(10, m_NumberOfDigits - 1))) Then
                        Dim int0 As Integer = m_NumberOfDigits
                        For i As Integer = 1 To int0
                            g.DrawImage(GreenLED(11), CInt(Math.Round(CSng((SegWidth * (i - 1))) * ImageRatio * 1.1)), 0)
                        Next i

                    Else
                        Dim mNumberOfDigits As Integer = m_NumberOfDigits
                        Dim i As Integer = 1
                        Do
                            If (one >= 0) Then
                                Dim j As Integer = Convert.ToInt32(Math.Floor(one / Math.Pow(10, m_NumberOfDigits - i)))
                                If (j > 0 OrElse i = m_NumberOfDigits OrElse i > m_NumberOfDigits - m_DecimalPosition) Then
                                    DigitsStarted = True
                                End If
                                If (DigitsStarted) Then
                                    If (m_ForecolorHigh) Then
                                        g.DrawImage(LED(j), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                    ElseIf (Not m_DecimalPos) Then
                                        g.DrawImage(GreenLED(j), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                    Else
                                        g.DrawImage(RedLED(j), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                    End If
                                ElseIf (m_ForecolorHigh) Then
                                    g.DrawImage(LED(10), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                ElseIf (Not m_DecimalPos) Then
                                    g.DrawImage(GreenLED(10), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                Else
                                    g.DrawImage(RedLED(10), Convert.ToInt32(CDbl((SegWidth * (i - 1))) * 1.1) * ImageRatio, 0.0!)
                                End If
                                one = one - j * Math.Pow(10, m_NumberOfDigits - i)
                            Else
                                If (m_ForecolorHigh) Then
                                    g.DrawImage(LED(11), SegWidth * (i - 1) * ImageRatio, 0.0!)
                                ElseIf (Not m_DecimalPos) Then
                                    g.DrawImage(GreenLED(11), SegWidth * (i - 1) * ImageRatio, 0.0!)
                                Else
                                    g.DrawImage(RedLED(11), SegWidth * (i - 1) * ImageRatio, 0.0!)
                                End If
                                one = Math.Abs(one)
                            End If
                            i = i + 1
                        Loop While i <= mNumberOfDigits
                        If (m_DecimalPosition > 0) Then
                            If (m_ForecolorHigh) Then
                                g.DrawImage(StaticImage, Convert.ToInt32(CDbl((CSng(((m_NumberOfDigits - m_DecimalPosition) * SegWidth - 70)) * ImageRatio)) * 1.1), 400.0! * ImageRatio)
                            ElseIf (Not m_DecimalPos) Then
                                g.DrawImage(GreenDecimalImage, Convert.ToInt32(CDbl((CSng(((m_NumberOfDigits - m_DecimalPosition) * SegWidth - 70)) * ImageRatio)) * 1.1), 400.0! * ImageRatio)
                            Else
                                g.DrawImage(RedDecimalImage, Convert.ToInt32(CDbl((CSng(((m_NumberOfDigits - m_DecimalPosition) * SegWidth - 70)) * ImageRatio)) * 1.1), 400.0! * ImageRatio)
                            End If
                        End If
                    End If
                    If (If(e Is Nothing, False, e.Graphics IsNot Nothing)) Then
                        e.Graphics.DrawImage(_backBuffer, 0, 0)
                    End If
                End Using
            End If
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            If (_backBuffer IsNot Nothing) Then
                _backBuffer.Dispose()
                _backBuffer = Nothing
            End If
            _Resize(Me, Nothing)
            MyBase.OnSizeChanged(e)
        End Sub

        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            RaiseEvent ValueChanged(Me, e)
        End Sub
#End Region



    End Class
 