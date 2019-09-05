Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class DigitalPanelMeter
    Inherits Control
    ' Events
    Public Event ValueChanged As EventHandler

    ' Methods
    Public Sub New()

        MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or (ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint))), True)
        Me.BackColor = Color.Transparent
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.LightGray
        End If
        Me.AdjustSize()
        Me.sf = New StringFormat
        Me.sf.Alignment = StringAlignment.Center
        Me.sf.LineAlignment = StringAlignment.Far


    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
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
                Me.TextBrush.Dispose()
                Me.sf.Dispose()
                Me._backBuffer.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub RatioLock()
        If ((MyBase.Height <> Me.LastHeight) Or (MyBase.Width <> Me.LastWidth)) Then
            Me.AdjustSize()
            Me.LastWidth = MyBase.Width
            Me.LastHeight = MyBase.Height
            Me.RefreshImage()
        End If
    End Sub

    Private Sub AdjustSize()
        Me.StaticImageRatio = CSng((CDbl(My.Resources.DigitalPanelMeter.Height) / CDbl((My.Resources.DigitalPanelMeter.Width + ((Me.m_NumberOfDigits - 4) * Me.SegWidth)))))
        If ((Me.LastHeight < MyBase.Height) Or (Me.LastWidth < MyBase.Width)) Then
            If ((CDbl(MyBase.Height) / CDbl(MyBase.Width)) > Me.StaticImageRatio) Then
                MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.StaticImageRatio))))
            Else
                MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * Me.StaticImageRatio))))
            End If
        ElseIf ((CDbl(MyBase.Height) / CDbl(MyBase.Width)) > Me.StaticImageRatio) Then
            MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * Me.StaticImageRatio))))
        Else
            MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.StaticImageRatio))))
        End If
    End Sub

    Private Sub RefreshImage()
        Me.StaticImageRatio = CSng((CDbl(My.Resources.DigitalPanelMeter.Height) / CDbl((My.Resources.DigitalPanelMeter.Width + ((Me.m_NumberOfDigits - 4) * Me.SegWidth)))))
        Dim num As Single = CSng((CDbl(MyBase.Width) / CDbl((My.Resources.DigitalPanelMeter.Width + (Me.m_NumberOfDigits - (4 * Me.SegWidth))))))
        Dim num2 As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.DigitalPanelMeter.Height)))
        If (num < num2) Then
            Me.y = MyBase.Height
            If ((MyBase.Height > 0) And (My.Resources.DigitalPanelMeter.Height > 0)) Then
                Me.x = CSng(((CDbl((My.Resources.DigitalPanelMeter.Width + (Me.m_NumberOfDigits - (4 * Me.SegWidth)))) / CDbl(My.Resources.DigitalPanelMeter.Height)) * MyBase.Height))
            Else
                Me.x = 1.0!
            End If
            Me.ImageRatio = num
        Else
            Me.x = MyBase.Width
            Me.y = CSng(((CDbl(My.Resources.DigitalPanelMeter.Height) / CDbl((My.Resources.DigitalPanelMeter.Width + (Me.m_NumberOfDigits - (4 * Me.SegWidth))))) * MyBase.Width))
            Me.ImageRatio = num2
        End If
        If (Me.ImageRatio > 0!) Then
            If (Me.StaticImage IsNot Nothing) Then
                Me.StaticImage.Dispose()
            End If
            Me.StaticImage = New Bitmap(Convert.ToInt32(CSng(((My.Resources.DigitalPanelMeter.Width + ((Me.m_NumberOfDigits - 4) * Me.SegWidth)) * Me.ImageRatio))), Convert.ToInt32(CSng((My.Resources.DigitalPanelMeter.Height * Me.ImageRatio))))
            Dim graphics As Graphics = Graphics.FromImage(Me.StaticImage)
            graphics.DrawImage(My.Resources.DigitalPanelMeter, CSng((Convert.ToInt32(CInt(((Me.m_NumberOfDigits - 4) * Me.SegWidth))) * Me.ImageRatio)), CSng(0!), CSng((My.Resources.DigitalPanelMeter.Width * Me.ImageRatio)), CSng((My.Resources.DigitalPanelMeter.Height * Me.ImageRatio)))
            If (Me.m_NumberOfDigits > 4) Then
                graphics.DrawImage(My.Resources.DigitalPanelMeterLeftHalf, 0!, 0!, (My.Resources.DigitalPanelMeterLeftHalf.Width * Me.ImageRatio), CSng(Convert.ToInt32(CSng((My.Resources.DigitalPanelMeterLeftHalf.Height * Me.ImageRatio)))))
            End If
            Me.TextRectangle.X = 0
            Me.TextRectangle.Y = CInt(Math.Round(CDbl((MyBase.Height * 0.04))))
            Me.TextRectangle.Width = MyBase.Width
            Me.TextRectangle.Height = CInt(Math.Round(CDbl((MyBase.Height * 0.18))))
            If (Me.TextBrush Is Nothing) Then
                Me.TextBrush = New SolidBrush(MyBase.ForeColor)
            End If
            Dim width As Integer = CInt(Math.Round(CDbl((Convert.ToInt32(CSng((My.Resources.LED7Segment8Red.Width * Me.ImageRatio))) * 0.7))))
            Dim height As Integer = CInt(Math.Round(CDbl((Convert.ToInt32(CSng((My.Resources.LED7Segment8Red.Height * Me.ImageRatio))) * 0.7))))
            Dim index As Integer = 0
            Do
                If (Me.LED(index) IsNot Nothing) Then
                    Me.LED(index).Dispose()
                End If
                Me.LED(index) = New Bitmap(width, height)
                graphics = Graphics.FromImage(Me.LED(index))
                Select Case index
                    Case 0
                        graphics.DrawImage(My.Resources.LED7Segment0Red, 0, 0, width, height)
                        Exit Select
                    Case 1
                        graphics.DrawImage(My.Resources.LED7Segment1Red, 0, 0, width, height)
                        Exit Select
                    Case 2
                        graphics.DrawImage(My.Resources.LED7Segment2Red, 0, 0, width, height)
                        Exit Select
                    Case 3
                        graphics.DrawImage(My.Resources.LED7Segment3Red, 0, 0, width, height)
                        Exit Select
                    Case 4
                        graphics.DrawImage(My.Resources.LED7Segment4Red, 0, 0, width, height)
                        Exit Select
                    Case 5
                        graphics.DrawImage(My.Resources.LED7Segment5Red, 0, 0, width, height)
                        Exit Select
                    Case 6
                        graphics.DrawImage(My.Resources.LED7Segment6Red, 0, 0, width, height)
                        Exit Select
                    Case 7
                        graphics.DrawImage(My.Resources.LED7Segment7Red, 0, 0, width, height)
                        Exit Select
                    Case 8
                        graphics.DrawImage(My.Resources.LED7Segment8Red, 0, 0, width, height)
                        Exit Select
                    Case 9
                        graphics.DrawImage(My.Resources.LED7Segment9Red, 0, 0, width, height)
                        Exit Select
                    Case 10
                        graphics.DrawImage(My.Resources.LED7SegmentOffRed, 0, 0, width, height)
                        Exit Select
                    Case 11
                        graphics.DrawImage(My.Resources.LED7Segment_Red, 0, 0, width, height)
                        Exit Select
                End Select
                index += 1
            Loop While (index <= 11)
            Me.DecimalImage = New Bitmap(Convert.ToInt32(CSng((My.Resources.LED7SegmentDecimalRed.Width * Me.ImageRatio))), Convert.ToInt32(CSng((My.Resources.LED7SegmentDecimalRed.Height * Me.ImageRatio))))
            graphics = Graphics.FromImage(Me.DecimalImage)
            graphics.DrawImage(My.Resources.LED7SegmentDecimalRed, 0, 0, Convert.ToInt32(CSng((My.Resources.LED7SegmentDecimalRed.Width * Me.ImageRatio))), Convert.ToInt32(CSng((My.Resources.LED7SegmentDecimalRed.Height * Me.ImageRatio))))
            graphics.Dispose()

            If (Me._backBuffer IsNot Nothing) Then
                Me._backBuffer.Dispose()
            End If
            Me._backBuffer = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Not (((Me.StaticImage Is Nothing) Or (Me._backBuffer Is Nothing)) Or (Me.TextBrush Is Nothing)) Then
            Using graphics As Graphics = Graphics.FromImage(Me._backBuffer)
                graphics.Clear(MyBase.BackColor)
                graphics.FillRectangle(New SolidBrush(Me.BackColor), 0, 0, MyBase.Width, MyBase.Height)
                graphics.DrawImage(Me.StaticImage, 0, 0)
                If Not String.IsNullOrEmpty(MyBase.Text) Then
                    If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                        Me.TextBrush.Color = MyBase.ForeColor
                    End If
                    graphics.DrawString(MyBase.Text, MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.sf)
                End If
                Try
                    Dim one As Decimal = Decimal.Divide(Decimal.One, Me.m_Resolution)
                    If (Decimal.Compare(one, Decimal.Zero) = 0) Then
                        one = Decimal.One
                    End If
                    Dim num3 As Long = CLng(Math.Round(CDbl((((Me.m_Value + Convert.ToDouble(Me.m_ValueScaleOffset)) * Convert.ToDouble(one)) * Convert.ToDouble(Me.m_ValueScaleFactor)))))
                    Dim num4 As Long = Convert.ToInt64(Decimal.Divide(New Decimal(num3), one))
                    If ((num4 <= (Math.Pow(10, CDbl(Me.m_NumberOfDigits)) - 1)) And (num4 >= ((Math.Pow(10, CDbl((Me.m_NumberOfDigits - 1))) - 1) * -1))) Then
                        Dim num5 As Integer = Me.m_NumberOfDigits
                        Dim i As Integer = 1
                        Do While (i <= num5)
                            If (num4 < 0) Then
                                graphics.DrawImage(Me.LED(11), CSng(((&H4B + (Me.SegWidth * (i - 1))) * Me.ImageRatio)), CSng((65.0! * Me.ImageRatio)))
                                num4 = Math.Abs(num4)
                            Else
                                Dim flag4 As Boolean
                                Dim num As Long = Convert.ToInt64(Math.Floor(CDbl((CDbl(num4) / Math.Pow(10, CDbl((Me.m_NumberOfDigits - i)))))))
                                If (((num > 0) Or (i = Me.m_NumberOfDigits)) Or (i > (Me.m_NumberOfDigits - Me.m_DecimalPos))) Then
                                    flag4 = True
                                End If
                                If flag4 Then
                                    graphics.DrawImage(Me.LED(CInt(num)), CSng(((&H4B + (Me.SegWidth * (i - 1))) * Me.ImageRatio)), CSng((65.0! * Me.ImageRatio)))
                                Else
                                    graphics.DrawImage(Me.LED(10), CSng(((&H4B + (Me.SegWidth * (i - 1))) * Me.ImageRatio)), CSng((65.0! * Me.ImageRatio)))
                                End If
                                num4 = (num4 - Convert.ToInt64(CDbl((num * Math.Pow(10, CDbl((Me.m_NumberOfDigits - i)))))))
                            End If
                            i += 1
                        Loop
                    Else
                        Dim num7 As Integer = Me.m_NumberOfDigits
                        Dim i As Integer = 1
                        Do While (i <= num7)
                            graphics.DrawImage(Me.LED(11), CSng(((&H4B + (Me.SegWidth * (i - 1))) * Me.ImageRatio)), CSng((65.0! * Me.ImageRatio)))
                            i += 1
                        Loop
                    End If
                Catch ex As Exception

                    Dim num9 As Integer = Me.m_NumberOfDigits
                    Dim i As Integer = 1
                    Do While (i <= num9)
                        graphics.DrawImage(Me.LED(11), CSng(((&H4B + (Me.SegWidth * (i - 1))) * Me.ImageRatio)), CSng((65.0! * Me.ImageRatio)))
                        i += 1
                    Loop

                End Try
                If (Me.m_DecimalPos > 0) Then
                    graphics.DrawImage(Me.DecimalImage, CSng(((((Me.m_NumberOfDigits - Me.m_DecimalPos) * Me.SegWidth) + &H37) * Me.ImageRatio)), CSng((160.0! * Me.ImageRatio)))
                End If
                e.Graphics.DrawImage(Me._backBuffer, 0, 0)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal eventArgs_0 As EventArgs)
        If (Me._backBuffer IsNot Nothing) Then
            Me._backBuffer.Dispose()
            Me._backBuffer = Nothing
        End If
        Me.RatioLock()
        MyBase.OnSizeChanged(eventArgs_0)
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub


    ' Properties
    <Category("Numeric Display")>
    Public Property Value As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If Not (value = Me.m_Value) Then
                Me.m_Value = value
                MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl((Me.StaticImage.Width * 0.08)))), CInt(Math.Round(CDbl((Me.StaticImage.Height * 0.1)))), CInt(Math.Round(CDbl((Me.StaticImage.Width * 0.85)))), CInt(Math.Round(CDbl((Me.StaticImage.Height * 0.8))))))
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.m_ValueScaleFactor
        End Get
        Set(ByVal value As Decimal)
            If (Decimal.Compare(Me.m_ValueScaleFactor, value) <> 0) Then
                Me.m_ValueScaleFactor = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ValueScaleOffset As Decimal
        Get
            Return Me.m_ValueScaleOffset
        End Get
        Set(ByVal value As Decimal)
            If (Decimal.Compare(Me.m_ValueScaleOffset, value) <> 0) Then
                Me.m_ValueScaleOffset = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property Resolution As Decimal
        Get
            Return Me.m_Resolution
        End Get
        Set(ByVal value As Decimal)
            If (Decimal.Compare(value, Decimal.Zero) <> 0) Then
                Me.m_Resolution = value
                If (Me.StaticImage IsNot Nothing) Then
                    MyBase.Invalidate()
                End If
            End If
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(True)>
    Public Overrides Property [Text] As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (Me.TextBrush Is Nothing) Then
                Me.TextBrush = New SolidBrush(value)
            Else
                Me.TextBrush.Color = value
            End If
            MyBase.ForeColor = value
            MyBase.Invalidate()
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property NumberOfDigits As Integer
        Get
            Return Me.m_NumberOfDigits
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.m_NumberOfDigits) Then
                Me.m_NumberOfDigits = Math.Max(Math.Min(6, value), 4)
                Me.AdjustSize()
                Me.RefreshImage()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property DecimalPosition As Integer
        Get
            Return Me.m_DecimalPos
        End Get
        Set(ByVal value As Integer)
            Me.m_DecimalPos = Math.Max(Math.Min((Me.m_NumberOfDigits - 1), value), 0)
            MyBase.Invalidate()
        End Set
    End Property


    ' Fields
    Private StaticImage As Bitmap
    Private LED As Bitmap() = New Bitmap(12 - 1) {}
    Private DecimalImage As Bitmap
    Private ImageRatio As Single
    Private TextRectangle As Rectangle = New Rectangle
    Private sf As StringFormat
    Private TextBrush As SolidBrush
    Private x As Single
    Private y As Single
    Private m_Value As Double
    Private m_ValueScaleFactor As Decimal = Decimal.One
    Private m_ValueScaleOffset As Decimal
    Private m_Resolution As Decimal = Decimal.One
    Private m_NumberOfDigits As Integer = 5
    Private m_DecimalPos As Integer = 0
    Private SegWidth As Integer = &H55
    Private _backBuffer As Bitmap
    Private LastWidth As Integer
    Private LastHeight As Integer
    Private StaticImageRatio As Single = CSng((CDbl(My.Resources.DigitalPanelMeter.Height) / CDbl(My.Resources.DigitalPanelMeter.Width)))
End Class
