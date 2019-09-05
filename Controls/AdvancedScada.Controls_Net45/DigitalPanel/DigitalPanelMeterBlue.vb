Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace DigitalPanel
    <DefaultEvent("")>
    Public Class DigitalPanelMeterBlue
        Inherits AnalogMeterBase
        ' Methods
        Public Sub New()
            Me.ForeColor = Color.Black
            Me.BackColor = Color.FromArgb(&HFF, &H6A, 140, &HFF)
            MyBase.Maximum = 0
            MyBase.Minimum = 0
            MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or (ControlStyles.SupportsTransparentBackColor Or (ControlStyles.ResizeRedraw Or ControlStyles.UserPaint)))), True)
        End Sub

        Protected Overrides Sub CreateStaticImage()
            If Not ((MyBase.Width <= 0) Or (MyBase.Height <= 0)) Then
                Dim g As Graphics
                If (MyBase.StaticImage IsNot Nothing) Then
                    MyBase.StaticImage.Dispose()
                End If
                MyBase.StaticImage = New Bitmap(MyBase.Width, MyBase.Height)
                Graphics.FromImage(MyBase.StaticImage).DrawImage(My.Resources.ClearBackgroundFrame, 0, 0, MyBase.StaticImage.Width, MyBase.StaticImage.Height)
                Me.TextRectangle.X = 0
                Me.TextRectangle.Y = CInt(Math.Round(CDbl((MyBase.Height * 0.02))))
                Me.TextRectangle.Width = MyBase.Width
                Me.TextRectangle.Height = CInt(Math.Round(CDbl((MyBase.Height * 0.17))))
                If (MyBase.TextBrush Is Nothing) Then
                    MyBase.TextBrush = New SolidBrush(MyBase.ForeColor)
                End If
                MyBase.ImageRatio = (CDbl(MyBase.Height) / CDbl(My.Resources.ClearBackgroundFrame.Height))
                Dim width As Integer = Convert.ToInt32(CDbl((198 * MyBase.ImageRatio)))
                Dim height As Integer = Convert.ToInt32(CDbl((280 * MyBase.ImageRatio)))
                Dim index As Integer = 0
                Do
                    If (Me.LED(index) IsNot Nothing) Then
                        Me.LED(index).Dispose()
                    End If
                    Me.LED(index) = New Bitmap(width, height)
                    g = Graphics.FromImage(Me.LED(index))
                    g.ScaleTransform(CSng((0.27 * MyBase.ImageRatio)), CSng((0.27 * MyBase.ImageRatio)))
                    SevenSegment2.FillPolygon(g, index, Me.m_LEDColor, True)
                    index += 1
                Loop While (index <= 11)
                Me.DecimalImage = New Bitmap(Convert.ToInt32(CDbl(((My.Resources.BlueSevenSegmentDot.Width * MyBase.ImageRatio) * 1.3))), Convert.ToInt32(CDbl(((My.Resources.BlueSevenSegmentDot.Height * MyBase.ImageRatio) * 1.3))))
                g = Graphics.FromImage(Me.DecimalImage)
                g.DrawImage(My.Resources.BlueSevenSegmentDot, 0, 0, Me.DecimalImage.Width, Me.DecimalImage.Height)
                g.Dispose()
                MyBase.Invalidate()
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing Then
                    If (Me.DecimalImage IsNot Nothing) Then
                        Me.DecimalImage.Dispose()
                    End If
                    Dim num As Integer = (Me.LED.Length - 1)
                    Dim i As Integer = 0
                    Do While (i <= num)
                        If (Me.LED(i) IsNot Nothing) Then
                            Me.LED(i).Dispose()
                        End If
                        i += 1
                    Loop
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            If (Not MyBase.StaticImage Is Nothing) Then
                Dim g As Graphics = e.Graphics
                g.DrawImage(MyBase.StaticImage, 0, 0)
                If Not String.IsNullOrEmpty(MyBase.Text) Then
                    If (MyBase.TextBrush Is Nothing) Then
                        MyBase.TextBrush = New SolidBrush(MyBase.ForeColor)
                    ElseIf (MyBase.TextBrush.Color <> MyBase.ForeColor) Then
                        MyBase.TextBrush.Color = MyBase.ForeColor
                    End If
                    g.DrawString(MyBase.Text, MyBase.Font, MyBase.TextBrush, MyBase.TextRectangle, MyBase.stringFormat_0)
                End If
                Dim one As Decimal = Decimal.Divide(Decimal.One, Me.m_Resolution)
                If (Decimal.Compare(one, Decimal.Zero) = 0) Then
                    one = Decimal.One
                End If
                Dim num3 As Long = Convert.ToInt64(Decimal.Divide(New Decimal(CLng(Math.Round(CDbl(((MyBase.m_Value * Convert.ToDouble(one)) * MyBase.m_ValueScaleFactor))))), one))
                Dim y As Integer = Convert.ToInt32(CDbl((0.24 * MyBase.Height)))
                Dim num5 As Integer = Convert.ToInt32(CDbl((Me.LED(0).Width * 1.15)))
                Dim num6 As Single = CSng((((My.Resources.ClearBackgroundFrame.Width * 1) / CDbl(My.Resources.ClearBackgroundFrame.Height)) / ((MyBase.StaticImage.Width * 1) / CDbl(MyBase.StaticImage.Height))))
                Dim num7 As Integer = Convert.ToInt32(CDbl(((MyBase.Width * 0.8) / (Me.LED(0).Width * 1.15))))
                If ((num3 <= (Math.Pow(10, CDbl(num7)) - 1)) And (num3 >= ((Math.Pow(10, CDbl((num7 - 1))) - 1) * -1))) Then
                    Dim num8 As Integer = num7
                    Dim i As Integer = 1
                    Do While (i <= num8)
                        If (num3 < 0) Then
                            g.DrawImage(Me.LED(11), (Convert.ToInt32(CDbl((MyBase.StaticImage.Width * 0.1))) + (num5 * (i - 1))), y)
                            num3 = Math.Abs(num3)
                        Else
                            Dim flag As Boolean
                            Dim index As Integer = Convert.ToInt32(Math.Floor(CDbl((CDbl(num3) / Math.Pow(10, CDbl((num7 - i)))))))
                            If (((index > 0) Or (i = num7)) Or (i > (num7 - Me.m_DecimalPos))) Then
                                flag = True
                            End If
                            If flag Then
                                g.DrawImage(Me.LED(index), (Convert.ToInt32(CDbl((MyBase.StaticImage.Width * 0.1))) + (num5 * (i - 1))), y)
                            Else
                                g.DrawImage(Me.LED(10), (Convert.ToInt32(CDbl((MyBase.StaticImage.Width * 0.1))) + (num5 * (i - 1))), y)
                            End If
                            num3 = CLng(Math.Round(CDbl((num3 - (index * Math.Pow(10, CDbl((num7 - i))))))))
                        End If
                        i += 1
                    Loop
                Else
                    Dim num10 As Integer = num7
                    Dim i As Integer = 1
                    Do While (i <= num10)
                        g.DrawImage(Me.LED(11), (Convert.ToInt32(CDbl((MyBase.StaticImage.Width * 0.1))) + (num5 * (i - 1))), y)
                        i += 1
                    Loop
                End If
                If (Me.m_DecimalPos > 0) Then
                    g.DrawImage(Me.DecimalImage, (((num7 - Me.m_DecimalPos) * num5) + Convert.ToInt32(CDbl((MyBase.StaticImage.Width * 0.072)))), Convert.ToInt32(CDbl((MyBase.Height * 0.77))))
                End If
            End If
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            If ((MyBase.Height <> Me.LastHeight) Or (MyBase.Width <> Me.LastWidth)) Then
                Me.LastWidth = MyBase.Width
                Me.LastHeight = MyBase.Height
                Me.CreateStaticImage()
            End If
            MyBase.OnSizeChanged(e)
        End Sub

        Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
            MyBase.OnValueChanged(e)
            MyBase.Invalidate()
        End Sub


        ' Properties
        <Category("Numeric Display")>
        Public Property Resolution As Decimal
            Get
                Return Me.m_Resolution
            End Get
            Set(ByVal value As Decimal)
                If (Decimal.Compare(value, Decimal.Zero) <> 0) Then
                    Me.m_Resolution = value
                    If (MyBase.StaticImage IsNot Nothing) Then
                        MyBase.Invalidate()
                    End If
                End If
            End Set
        End Property

        <Category("Numeric Display")>
        Public Property DecimalPosition As Integer
            Get
                Return Me.m_DecimalPos
            End Get
            Set(ByVal value As Integer)
                Me.m_DecimalPos = Math.Max(Math.Min(&H63, value), 0)
                MyBase.Invalidate()
            End Set
        End Property

        Public Property BacklightColor As ColorSelect
            Get
                Return Me.m_BackLightColor
            End Get
            Set(ByVal value As ColorSelect)
                If (Me.m_BackLightColor <> value) Then
                    Me.m_BackLightColor = value
                    Me.CreateStaticImage()
                End If
            End Set
        End Property

        Public Property LEDColor As Color
            Get
                Return Me.m_LEDColor
            End Get
            Set(ByVal value As Color)
                If (Me.m_LEDColor <> value) Then
                    Me.m_LEDColor = value
                    Me.CreateStaticImage()
                End If
            End Set
        End Property


        ' Fields
        Private LED As Bitmap() = New Bitmap(12 - 1) {}
        Private DecimalImage As Bitmap
        Private m_Resolution As Decimal = Decimal.One
        Private m_DecimalPos As Integer = 0
        Private m_BackLightColor As ColorSelect = ColorSelect.Blue
        Private m_LEDColor As Color = Color.FromArgb(&HFF, &H30, &H30, &H68)
        Private LastWidth As Integer
        Private LastHeight As Integer

        ' Nested Types
        Public Enum ColorSelect
            ' Fields
            Blue = 0
            Yellow = 1
            Green = 2
        End Enum
    End Class
End Namespace

