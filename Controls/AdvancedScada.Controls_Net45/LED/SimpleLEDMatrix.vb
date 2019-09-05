Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms


Public Class SimpleLEDMatrix
    Inherits Control



    Private DotPatterns() As ULong

    Private m_valueInput As Char

    Private m_alpha As Integer

    <Browsable(True), DefaultValue(0), Description("Opacity value for inactive dots on the matrix display (valid values 0 to 255)."), RefreshProperties(RefreshProperties.All)>
    Public Property DM_Alpha() As Integer
        Get
            Return Me.m_alpha
        End Get
        Set(ByVal value As Integer)
            If Not Versioned.IsNumeric(value) Then
                value = 0
            End If
            If value < 0 Then
                value = 0
            End If
            If value > 255 Then
                value = 255
            End If
            If Me.m_alpha <> value Then
                Me.m_alpha = value
            End If
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Description("Constant value used to maintain the ratio between width and height."), RefreshProperties(RefreshProperties.All)>
    Public ReadOnly Property SizeScaleCoefficient() As Single
        Get
            Return 1.76296294F
        End Get
    End Property

    <Browsable(True), Description("ASCII character to show on the matrix display (any standard keyboard character is valid)."), RefreshProperties(RefreshProperties.All)>
    Public Property Value() As Char
        Get
            Return Me.m_valueInput
        End Get
        Set(ByVal value As Char)
            If Me.m_valueInput <> value Then
                Me.m_valueInput = value
            End If
            Me.Invalidate()
        End Set
    End Property



    Public Sub New()
        Dim simpleLEDMatrix As SimpleLEDMatrix = Me
        AddHandler MyBase.Click, AddressOf simpleLEDMatrix.DotMatrix_Click
        Dim simpleLEDMatrix1 As SimpleLEDMatrix = Me

        Me.DotPatterns = New ULong(127) {}
        Me.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.BackColor = Color.Transparent
        'INSTANT VB NOTE: The variable size was renamed since Visual Basic does not handle local variables named the same as class members well:
        Dim size_Renamed As New Size(135, 238)
        Me.Size = size_Renamed
        size_Renamed = New Size(28, 49)
        Me.MinimumSize = size_Renamed
        size_Renamed = New Size(300, 529)
        Me.MaximumSize = size_Renamed
        Me.DotPatterns(32) = Convert.ToUInt64("0000000000000000000000000000000000000000", 2)
        Me.DotPatterns(33) = Convert.ToUInt64("0000000100000000010000100001000010000100", 2)
        Me.DotPatterns(34) = Convert.ToUInt64("0000000000000000000000000010011001010010", 2)
        Me.DotPatterns(35) = Convert.ToUInt64("0000000000010101111101010010101111101010", 2)
        Me.DotPatterns(36) = Convert.ToUInt64("0000000100011111010001110001011111000100", 2)
        Me.DotPatterns(37) = Convert.ToUInt64("0000011000110010001000100010001001100011", 2)
        Me.DotPatterns(38) = Convert.ToUInt64("0000010110010011010100010001010010100010", 2)
        Me.DotPatterns(39) = Convert.ToUInt64("0000000000000000000000000000100010000100", 2)
        Me.DotPatterns(40) = Convert.ToUInt64("0000001000001000010000100001000010001000", 2)
        Me.DotPatterns(41) = Convert.ToUInt64("0000000010001000010000100001000010000010", 2)
        Me.DotPatterns(42) = Convert.ToUInt64("0000000000000001000101010111110101010001", 2)
        Me.DotPatterns(43) = Convert.ToUInt64("0000000000000000010001110001000000000000", 2)
        Me.DotPatterns(44) = Convert.ToUInt64("0001000100001000000000000000000000000000", 2)
        Me.DotPatterns(45) = Convert.ToUInt64("0000000000000000000001110000000000000000", 2)
        Me.DotPatterns(46) = Convert.ToUInt64("0000000100000000000000000000000000000000", 2)
        Me.DotPatterns(47) = Convert.ToUInt64("0000000000000010001000100010001000000000", 2)
        Me.DotPatterns(48) = Convert.ToUInt64("0000001110100011001110101110011000101110", 2)
        Me.DotPatterns(49) = Convert.ToUInt64("0000001110001000010000100001100010000100", 2)
        Me.DotPatterns(50) = Convert.ToUInt64("0000011111000100010001000100001000101110", 2)
        Me.DotPatterns(51) = Convert.ToUInt64("0000001110100011000001110100001000101110", 2)
        Me.DotPatterns(52) = Convert.ToUInt64("0000001000010001111101001010100110001000", 2)
        Me.DotPatterns(53) = Convert.ToUInt64("0000001111100001000001111000010000111111", 2)
        Me.DotPatterns(54) = Convert.ToUInt64("0000001110100011000101111000010000101110", 2)
        Me.DotPatterns(55) = Convert.ToUInt64("0000000001000100010001000100001000011111", 2)
        Me.DotPatterns(56) = Convert.ToUInt64("0000001110100011000101110100011000101110", 2)
        Me.DotPatterns(57) = Convert.ToUInt64("0000001110100001000011110100011000101110", 2)
        Me.DotPatterns(58) = Convert.ToUInt64("0000000000000000010000000001000000000000", 2)
        Me.DotPatterns(59) = Convert.ToUInt64("0001000100001000000000100000000000000000", 2)
        Me.DotPatterns(60) = Convert.ToUInt64("0000000000010000010000010001000100000000", 2)
        Me.DotPatterns(61) = Convert.ToUInt64("0000000000000000111000000011100000000000", 2)
        Me.DotPatterns(62) = Convert.ToUInt64("0000000000000100010001000001000001000000", 2)
        Me.DotPatterns(63) = Convert.ToUInt64("0000000100000000010001000100001000101110", 2)
        Me.DotPatterns(64) = Convert.ToUInt64("0000001110000011110110101111011000101110", 2)
        Me.DotPatterns(65) = Convert.ToUInt64("0000010001100011111110001100010101000100", 2)
        Me.DotPatterns(66) = Convert.ToUInt64("0000001111100011000101111100011000101111", 2)
        Me.DotPatterns(67) = Convert.ToUInt64("0000001110100010000100001000011000101110", 2)
        Me.DotPatterns(68) = Convert.ToUInt64("0000001111100011000110001100011000101111", 2)
        Me.DotPatterns(69) = Convert.ToUInt64("0000011111000010000101111000010000111111", 2)
        Me.DotPatterns(70) = Convert.ToUInt64("0000000001000010000101111000010000111111", 2)
        Me.DotPatterns(71) = Convert.ToUInt64("0000001110100011000111101000011000101110", 2)
        Me.DotPatterns(72) = Convert.ToUInt64("0000010001100011000111111100011000110001", 2)
        Me.DotPatterns(73) = Convert.ToUInt64("0000001110001000010000100001000010001110", 2)
        Me.DotPatterns(74) = Convert.ToUInt64("0000001110100011000110000100001000010000", 2)
        Me.DotPatterns(75) = Convert.ToUInt64("0000010001010010010100011001010100110001", 2)
        Me.DotPatterns(76) = Convert.ToUInt64("0000011111000010000100001000010000100001", 2)
        Me.DotPatterns(77) = Convert.ToUInt64("0000010001100011000110001101011101110001", 2)
        Me.DotPatterns(78) = Convert.ToUInt64("0000010001100011100110101100111000110001", 2)
        Me.DotPatterns(79) = Convert.ToUInt64("0000001110100011000110001100011000101110", 2)
        Me.DotPatterns(80) = Convert.ToUInt64("0000000001000010000101111100011000101111", 2)
        Me.DotPatterns(81) = Convert.ToUInt64("0000010110010011010110001100011000101110", 2)
        Me.DotPatterns(82) = Convert.ToUInt64("0000010001100011000101111100011000101111", 2)
        Me.DotPatterns(83) = Convert.ToUInt64("0000001110100011000001110000011000101110", 2)
        Me.DotPatterns(84) = Convert.ToUInt64("0000000100001000010000100001000010011111", 2)
        Me.DotPatterns(85) = Convert.ToUInt64("0000001110100011000110001100011000110001", 2)
        Me.DotPatterns(86) = Convert.ToUInt64("0000000100010101000110001100011000110001", 2)
        Me.DotPatterns(87) = Convert.ToUInt64("0000010001110111010110001100011000110001", 2)
        Me.DotPatterns(88) = Convert.ToUInt64("0000010001100010101000100010101000110001", 2)
        Me.DotPatterns(89) = Convert.ToUInt64("0000000100001000010001010100011000110001", 2)
        Me.DotPatterns(90) = Convert.ToUInt64("0000011111000010001000100010001000011111", 2)
        Me.DotPatterns(91) = Convert.ToUInt64("0000001110000100001000010000100001001110", 2)
        Me.DotPatterns(92) = Convert.ToUInt64("0000000000100000100000100000100000100000", 2)
        Me.DotPatterns(93) = Convert.ToUInt64("0000001110010000100001000010000100001110", 2)
        Me.DotPatterns(94) = Convert.ToUInt64("0000000000000000000000000100010101000100", 2)
        Me.DotPatterns(95) = Convert.ToUInt64("0000011111000000000000000000000000000000", 2)
        Me.DotPatterns(96) = Convert.ToUInt64("0000000000000000000000000000000010000010", 2)
        Me.DotPatterns(97) = Convert.ToUInt64("0000010110010010111001000001110000000000", 2)
        Me.DotPatterns(98) = Convert.ToUInt64("0000001101100111000110011011010000100001", 2)
        Me.DotPatterns(99) = Convert.ToUInt64("0000001110100010000110001011100000000000", 2)
        Me.DotPatterns(100) = Convert.ToUInt64("0000010110110011000111001101101000010000", 2)
        Me.DotPatterns(101) = Convert.ToUInt64("0000011110000011111110001011100000000000", 2)
        Me.DotPatterns(102) = Convert.ToUInt64("0000001110001000010011110001000010011000", 2)
        Me.DotPatterns(103) = Convert.ToUInt64("0111010000111101000111001101100000000000", 2)
        Me.DotPatterns(104) = Convert.ToUInt64("0000010001100011000110011011010000100001", 2)
        Me.DotPatterns(105) = Convert.ToUInt64("0000001110001000010000100001100000000100", 2)
        Me.DotPatterns(106) = Convert.ToUInt64("0011001000010000100001000011000000001000", 2)
        Me.DotPatterns(107) = Convert.ToUInt64("0000010001010010011101001100010000100001", 2)
        Me.DotPatterns(108) = Convert.ToUInt64("0000011110001000010000100001000010000110", 2)
        Me.DotPatterns(109) = Convert.ToUInt64("0000010101101011010110101010100000000000", 2)
        Me.DotPatterns(110) = Convert.ToUInt64("0000010001100011000110011011010000000000", 2)
        Me.DotPatterns(111) = Convert.ToUInt64("0000001110100011000110001011100000000000", 2)
        Me.DotPatterns(112) = Convert.ToUInt64("0000100001011111000110011011010000000000", 2)
        Me.DotPatterns(113) = Convert.ToUInt64("1000010000111101000110001101100000000000", 2)
        Me.DotPatterns(114) = Convert.ToUInt64("0000000001000010000110011011010000000000", 2)
        Me.DotPatterns(115) = Convert.ToUInt64("0000001111100000111000001111100000000000", 2)
        Me.DotPatterns(116) = Convert.ToUInt64("0000001100000100001000010011110001000010", 2)
        Me.DotPatterns(117) = Convert.ToUInt64("0000010110110011000110001100010000000000", 2)
        Me.DotPatterns(118) = Convert.ToUInt64("0000000100010101000110001100010000000000", 2)
        Me.DotPatterns(119) = Convert.ToUInt64("0000001010101011010110101101010000000000", 2)
        Me.DotPatterns(120) = Convert.ToUInt64("0000010001010100010001010100010000000000", 2)
        Me.DotPatterns(121) = Convert.ToUInt64("0001100100001000101001010100010000000000", 2)
        Me.DotPatterns(122) = Convert.ToUInt64("0000011111100100010001001111110000000000", 2)
        Me.DotPatterns(123) = Convert.ToUInt64("0000001000001000010000010001000010001000", 2)
        Me.DotPatterns(124) = Convert.ToUInt64("0000000100001000010000000001000010000100", 2)
        Me.DotPatterns(125) = Convert.ToUInt64("0000000010001000010001000001000010000010", 2)
        Me.DotPatterns(126) = Convert.ToUInt64("0000000000000000100110110000000000000000", 2)
        If (MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0)) Then
            Me.ForeColor = Color.Red
        End If
    End Sub



    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub DotMatrix_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Focus()
    End Sub

    Private Sub DotMatrix_Resize(ByVal sender As Object, ByVal e As EventArgs)
        Me.Height = CInt(Math.Truncate(Math.Round(CDbl(CSng(1.76296294F * CSng(Me.Width))))))
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        Dim num As Integer = 0
        Do
            Dim num1 As Short = 0
            Do
                Dim num2 As Long = CLng(Math.Truncate(Math.Round(Math.Pow(2, CDbl((num * 5) + num1)))))
                Dim rectangle As New Rectangle(Convert.ToInt32(CDbl(num1) * (CDbl(Me.Width) / 5)), Convert.ToInt32(CSng(num) * (CSng(Me.Width) / 4.5F)), Convert.ToInt32(CDbl(Me.Width) / 5 - 1), Convert.ToInt32(CDbl(Me.Width) / 5 - 1))
                Dim graphicsPath As New GraphicsPath()
                graphicsPath.AddEllipse(rectangle)
                Dim pathGradientBrush As New PathGradientBrush(graphicsPath)
                If (CLng(Me.DotPatterns(Convert.ToInt32(Me.m_valueInput))) And num2) <= CLng(0) Then
                    rectangle.Inflate(-3, -3)
                    e.Graphics.DrawEllipse(New Pen(Color.FromArgb(45, Me.ForeColor), 2.0F), rectangle)
                    If Me.BackColor <> Color.Transparent Then
                        e.Graphics.FillEllipse(New SolidBrush(Color.FromArgb(255 - Me.m_alpha, Me.BackColor)), rectangle)
                    Else
                        e.Graphics.FillEllipse(New SolidBrush(Color.FromArgb(255 - Me.m_alpha, ControlPaint.DarkDark(Me.ForeColor))), rectangle)
                    End If
                Else
                    rectangle.Inflate(-1, -1)
                    e.Graphics.FillEllipse(New SolidBrush(Color.Black), rectangle)
                    e.Graphics.DrawEllipse(New Pen(Color.FromArgb(185, Me.ForeColor), 2.0F), rectangle)
                    pathGradientBrush.CenterColor = ControlPaint.Light(Me.ForeColor)
                    Dim colorArray() As Color = {Color.FromArgb(25, ControlPaint.Light(Me.ForeColor))}
                    pathGradientBrush.SurroundColors = colorArray
                    e.Graphics.FillEllipse(pathGradientBrush, rectangle)
                End If
                num1 = CShort(num1 + 1)
            Loop While num1 <= 4
            num += 1
        Loop While num <= 7
    End Sub
End Class

