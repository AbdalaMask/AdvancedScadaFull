Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class DigitalDisplayControl
    Inherits Control

    Private _digitColor As Color = Color.Red
    <Browsable(True), DefaultValue("Color.GreenYellow")>
    Public Property DigitColor() As Color
        Get
            Return _digitColor
        End Get
        Set(ByVal value As Color)
            _digitColor = value
            Invalidate()
        End Set
    End Property

    Private _value As String = "00000"
    <Browsable(True), DefaultValue("00000")>
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
            Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        Me.BackColor = Color.Transparent
        Me.Size = New System.Drawing.Size(352, 144)
        Me.DoubleBuffered = True

    End Sub

    Private Sub DigitalGauge_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

        Dim sevenSegmentHelper As New SevenSegmentHelper(e.Graphics)

        Dim digitSizeF As SizeF = sevenSegmentHelper.GetStringSize(_value, Font)
        Dim scaleFactor As Single = Math.Min(ClientSize.Width \ digitSizeF.Width, ClientSize.Height \ digitSizeF.Height)
        Dim font_Renamed As New Font(Font.FontFamily, scaleFactor * Font.SizeInPoints)
        digitSizeF = sevenSegmentHelper.GetStringSize(_value, font_Renamed)

        Using brush As New SolidBrush(_digitColor)
            Using lightBrush As New SolidBrush(Color.FromArgb(20, _digitColor))
                sevenSegmentHelper.DrawDigits(_value, font_Renamed, brush, lightBrush, (ClientSize.Width - digitSizeF.Width) \ 2, (ClientSize.Height - digitSizeF.Height) \ 2)
            End Using
        End Using
    End Sub
End Class

Public Class SevenSegmentHelper
    Private _graphics As Graphics

    ' Indicates what segments are illuminated for all 10 digits
    Private Shared _segmentData(,) As Byte = {
        {1, 1, 1, 0, 1, 1, 1},
        {0, 0, 1, 0, 0, 1, 0},
        {1, 0, 1, 1, 1, 0, 1},
        {1, 0, 1, 1, 0, 1, 1},
        {0, 1, 1, 1, 0, 1, 0},
        {1, 1, 0, 1, 0, 1, 1},
        {1, 1, 0, 1, 1, 1, 1},
        {1, 0, 1, 0, 0, 1, 0},
        {1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 0, 1, 1}
    }

    ' Points that define each of the seven segments
    Private ReadOnly _segmentPoints(6)() As Point

    Public Sub New(ByVal graphics As Graphics)
        Me._graphics = graphics
        _segmentPoints(0) = New Point() {
            New Point(3, 2),
            New Point(39, 2),
            New Point(31, 10),
            New Point(11, 10)
        }
        _segmentPoints(1) = New Point() {
            New Point(2, 3),
            New Point(10, 11),
            New Point(10, 31),
            New Point(2, 35)
        }
        _segmentPoints(2) = New Point() {
            New Point(40, 3),
            New Point(40, 35),
            New Point(32, 31),
            New Point(32, 11)
        }
        _segmentPoints(3) = New Point() {
            New Point(3, 36),
            New Point(11, 32),
            New Point(31, 32),
            New Point(39, 36),
            New Point(31, 40),
            New Point(11, 40)
        }
        _segmentPoints(4) = New Point() {
            New Point(2, 37),
            New Point(10, 41),
            New Point(10, 61),
            New Point(2, 69)
        }
        _segmentPoints(5) = New Point() {
            New Point(40, 37),
            New Point(40, 69),
            New Point(32, 61),
            New Point(32, 41)
        }
        _segmentPoints(6) = New Point() {
            New Point(11, 62),
            New Point(31, 62),
            New Point(39, 70),
            New Point(3, 70)
        }
    End Sub

    Public Function GetStringSize(ByVal text As String, ByVal font As Font) As SizeF
        Dim sizef As New SizeF(0, _graphics.DpiX * font.SizeInPoints / 72)

        For i As Integer = 0 To text.Length - 1
            If Char.IsDigit(text.Chars(i)) Then
                sizef.Width += 42 * _graphics.DpiX * font.SizeInPoints / 72 / 72
            ElseIf text.Chars(i) = ":"c OrElse text.Chars(i) = "."c Then
                sizef.Width += 12 * _graphics.DpiX * font.SizeInPoints / 72 / 72
            End If
        Next i
        Return sizef
    End Function

    Public Sub DrawDigits(ByVal text As String, ByVal font As Font, ByVal brush As Brush, ByVal brushLight As Brush, ByVal x As Single, ByVal y As Single)
        For cnt As Integer = 0 To text.Length - 1
            ' For digits 0-9
            If Char.IsDigit(text.Chars(cnt)) Then
                x = DrawDigit(AscW(text.Chars(cnt)) - AscW("0"c), font, brush, brushLight, x, y)
                ' For colon :
            ElseIf text.Chars(cnt) = ":"c Then
                x = DrawColon(font, brush, x, y)
                ' For dot .
            ElseIf text.Chars(cnt) = "."c Then
                x = DrawDot(font, brush, x, y)
            End If
        Next cnt
    End Sub

    Private Function DrawDigit(ByVal num As Integer, ByVal font As Font, ByVal brush As Brush, ByVal brushLight As Brush, ByVal x As Single, ByVal y As Single) As Single
        For cnt As Integer = 0 To _segmentPoints.Length - 1
            If _segmentData(num, cnt) = 1 Then
                FillPolygon(_segmentPoints(cnt), font, brush, x, y)
            Else
                FillPolygon(_segmentPoints(cnt), font, brushLight, x, y)
            End If
        Next cnt
        Return x + 42 * _graphics.DpiX * font.SizeInPoints / 72 / 72
    End Function

    Private Function DrawDot(ByVal font As Font, ByVal brush As Brush, ByVal x As Single, ByVal y As Single) As Single
        Dim dotPoints(0)() As Point

        dotPoints(0) = New Point() {
            New Point(2, 64),
            New Point(6, 61),
            New Point(10, 64),
            New Point(6, 69)
        }

        For cnt As Integer = 0 To dotPoints.Length - 1
            FillPolygon(dotPoints(cnt), font, brush, x, y)
        Next cnt
        Return x + 12 * _graphics.DpiX * font.SizeInPoints / 72 / 72
    End Function

    Private Function DrawColon(ByVal font As Font, ByVal brush As Brush, ByVal x As Single, ByVal y As Single) As Single
        Dim colonPoints(1)() As Point

        colonPoints(0) = New Point() {
            New Point(2, 21),
            New Point(6, 17),
            New Point(10, 21),
            New Point(6, 25)
        }
        colonPoints(1) = New Point() {
            New Point(2, 51),
            New Point(6, 47),
            New Point(10, 51),
            New Point(6, 55)
        }

        For cnt As Integer = 0 To colonPoints.Length - 1
            FillPolygon(colonPoints(cnt), font, brush, x, y)
        Next cnt
        Return x + 12 * _graphics.DpiX * font.SizeInPoints / 72 / 72
    End Function

    Private Sub FillPolygon(ByVal polygonPoints() As Point, ByVal font As Font, ByVal brush As Brush, ByVal x As Single, ByVal y As Single)
        Dim polygonPointsF(polygonPoints.Length - 1) As PointF

        For cnt As Integer = 0 To polygonPoints.Length - 1
            polygonPointsF(cnt).X = x + polygonPoints(cnt).X * _graphics.DpiX * font.SizeInPoints / 72 / 72
            polygonPointsF(cnt).Y = y + polygonPoints(cnt).Y * _graphics.DpiY * font.SizeInPoints / 72 / 72
        Next cnt
        _graphics.FillPolygon(brush, polygonPointsF)
    End Sub
End Class

