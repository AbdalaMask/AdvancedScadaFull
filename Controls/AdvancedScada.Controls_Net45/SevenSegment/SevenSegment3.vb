Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms


Public Class SevenSegment3
    Inherits Control
    Public Event ValueChanged As EventHandler
#Region "متغيرات"
    '* These are used to keep prescaled images in memory. Scaling in Paint event is expensive operation

    Private RedLED() As Bitmap

    Private GreenLED() As Bitmap

    Private RedDecimalImage As Bitmap

    Private GreenDecimalImage As Bitmap

    Private ImageRatio As Single

    Private TextRectangle As Rectangle

    Private sf As StringFormat

    Private TextBrush As SolidBrush

    Private BackgroundNeedsRefreshed As Boolean

    Private m_Value As Double

    Private m_MinValueForRed As Single

    Private m_MaxValueForRed As Single

    Private m_ValueScaleFactor As Decimal

    Private m_Resolution As Decimal

    Private m_NumberOfDigits As Integer

    Private m_DecimalPos As Integer

    Private x As Single

    Private y As Single

    Private SegWidth As Integer

    Private _backBuffer As Bitmap

    Private LastWidth As Integer

    Private LastHeight As Integer

    Private StaticImageRatio As Single
#End Region
#Region "خصائص"
    <Category("Numeric Display")>
    Public Property _ValueScaleFactor() As Decimal
        Get
            Return Me.m_ValueScaleFactor
        End Get
        Set(ByVal value As Decimal)
            Me.m_ValueScaleFactor = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim createParams_Renamed As CreateParams = MyBase.CreateParams
            createParams_Renamed.ExStyle = createParams_Renamed.ExStyle Or 32
            Return createParams_Renamed
        End Get
    End Property

    <Category("Numeric Display")>
    Public Property DecimalPosition() As Integer
        Get
            Return Me.m_DecimalPos
        End Get
        Set(ByVal value As Integer)
            If value <> Me.m_DecimalPos Then
                Me.m_DecimalPos = Math.Max(Math.Min(Me.m_NumberOfDigits - 1, value), 0)
                Me.RefreshImage()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property MaxValueForRed() As Single
        Get
            Return Me.m_MaxValueForRed
        End Get
        Set(ByVal value As Single)
            If value <> Me.m_MaxValueForRed Then
                Me.m_MaxValueForRed = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property MinValueForRed() As Single
        Get
            Return Me.m_MinValueForRed
        End Get
        Set(ByVal value As Single)
            If value <> Me.m_MinValueForRed Then
                Me.m_MinValueForRed = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property NumberOfDigits() As Integer
        Get
            Return Me.m_NumberOfDigits
        End Get
        Set(ByVal value As Integer)
            If value <> Me.m_NumberOfDigits Then
                Me.m_NumberOfDigits = Math.Max(Math.Min(50, value), 1)
                Me.AdjustSize()
                Me.RefreshImage()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property Resolution() As Decimal
        Get
            Return Me.m_Resolution
        End Get
        Set(ByVal value As Decimal)
            If Decimal.Compare(value, Decimal.Zero) <> 0 Then
                Me.m_Resolution = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property Value() As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If value <> Me.m_Value Then
                Me.m_Value = value
                Me.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

#End Region
#Region "مشيدات"
    Public Sub New()

        Me.RedLED = New Bitmap(12) {}
        Me.GreenLED = New Bitmap(12) {}
        Me.TextRectangle = New Rectangle()
        Me.sf = New StringFormat()
        Me.BackgroundNeedsRefreshed = True
        Me.m_MinValueForRed = 100.0F
        Me.m_MaxValueForRed = 200.0F
        Me.m_ValueScaleFactor = Decimal.One
        Me.m_Resolution = Decimal.One
        Me.m_NumberOfDigits = 5
        Me.m_DecimalPos = 0
        Me.SegWidth = My.Resources.RedZero.Width
        Me.StaticImageRatio = CSng(CDbl(My.Resources.DigitalPanelMeter.Height) / (CDbl(My.Resources.DigitalPanelMeter.Width) * 1.1))
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.ForeColor = Color.LightGray
        MyBase.BackColor = Color.Transparent
        Me.AdjustSize()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                Me.RedDecimalImage.Dispose()
                Dim length As Integer = (CInt(Me.RedLED.Length)) - 1
                For i As Integer = 0 To length
                    If Me.RedLED(i) IsNot Nothing Then
                        Me.RedLED(i).Dispose()
                    End If
                    If Me.GreenLED(i) IsNot Nothing Then
                        Me.GreenLED(i).Dispose()
                    End If
                Next i
                Me.TextBrush.Dispose()
                Me.sf.Dispose()
                Me._backBuffer.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region
#Region "اعادة تعريف الاحداث"
    Protected Overrides Sub OnCreateControl()
        Me.sf.Alignment = StringAlignment.Center
        Me.sf.LineAlignment = StringAlignment.Far
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If Me.TextBrush IsNot Nothing Then
            Me.TextBrush.Color = MyBase.ForeColor
        Else
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim flag As Boolean = False
        If Not (Me._backBuffer Is Nothing Or Me.TextBrush Is Nothing) Then
            Using graphic As Graphics = Graphics.FromImage(Me._backBuffer)
                graphic.Clear(Me.BackColor)
                If Me.BackgroundImage IsNot Nothing Then
                    If Me.BackgroundImageLayout <> ImageLayout.Stretch Then
                        graphic.DrawImage(Me.BackgroundImage, 0, 0)
                    Else
                        graphic.DrawImage(Me.BackgroundImage, 0, 0, Me.Width, Me.Height)
                    End If
                End If
                Dim one As Decimal = Decimal.Divide(Decimal.One, Me.m_Resolution)
                If Decimal.Compare(one, Decimal.Zero) = 0 Then
                    one = Decimal.One
                End If
                Dim num As Long = CLng(Math.Truncate(Math.Round(Me.m_Value * Convert.ToDouble(one) * Convert.ToDouble(Me.m_ValueScaleFactor))))
                Dim num1 As Long = Convert.ToInt64(Decimal.Divide(New Decimal(num), one))
                If Not (CDbl(num1) <= Math.Pow(10, CDbl(Me.m_NumberOfDigits)) - 1 And CDbl(num1) >= (Math.Pow(10, CDbl(Me.m_NumberOfDigits - 1)) - 1) * -1) Then
                    Dim mNumberOfDigits As Integer = Me.m_NumberOfDigits
                    For i As Integer = 1 To mNumberOfDigits
                        graphic.DrawImage(Me.RedLED(11), CSng(Me.SegWidth * (i - 1)) * Me.ImageRatio, 0.0F)
                    Next i
                Else
                    Dim mNumberOfDigits1 As Integer = Me.m_NumberOfDigits
                    For j As Integer = 1 To mNumberOfDigits1
                        If num1 >= CLng(0) Then
                            Dim num2 As Integer = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl(Me.m_NumberOfDigits - j))))
                            If num2 > 0 Or j = Me.m_NumberOfDigits Or j > Me.m_NumberOfDigits - Me.m_DecimalPos Then
                                flag = True
                            End If
                            If flag Then
                                If Not (Me.m_Value >= CDbl(Me.m_MinValueForRed) And Me.m_Value <= CDbl(Me.m_MaxValueForRed)) Then
                                    graphic.DrawImage(Me.GreenLED(num2), CSng(Convert.ToInt32(CDbl(Me.SegWidth * (j - 1)) * 1.1)) * Me.ImageRatio, 0.0F)
                                Else
                                    graphic.DrawImage(Me.RedLED(num2), CSng(Convert.ToInt32(CDbl(Me.SegWidth * (j - 1)) * 1.1)) * Me.ImageRatio, 0.0F)
                                End If
                            ElseIf Not (Me.m_Value >= CDbl(Me.m_MinValueForRed) And Me.m_Value <= CDbl(Me.m_MaxValueForRed)) Then
                                graphic.DrawImage(Me.GreenLED(10), CSng(Convert.ToInt32(CDbl(Me.SegWidth * (j - 1)) * 1.1)) * Me.ImageRatio, 0.0F)
                            Else
                                graphic.DrawImage(Me.RedLED(10), CSng(Convert.ToInt32(CDbl(Me.SegWidth * (j - 1)) * 1.1)) * Me.ImageRatio, 0.0F)
                            End If
                            num1 = CLng(Math.Truncate(Math.Round(CDbl(num1) - CDbl(num2) * Math.Pow(10, CDbl(Me.m_NumberOfDigits - j)))))
                        Else
                            If Not (Me.m_Value >= CDbl(Me.m_MinValueForRed) And Me.m_Value <= CDbl(Me.m_MaxValueForRed)) Then
                                graphic.DrawImage(Me.GreenLED(11), CSng(Me.SegWidth * (j - 1)) * Me.ImageRatio, 0.0F)
                            Else
                                graphic.DrawImage(Me.RedLED(11), CSng(Me.SegWidth * (j - 1)) * Me.ImageRatio, 0.0F)
                            End If
                            num1 = Math.Abs(num1)
                        End If
                    Next j
                End If
                If Me.m_DecimalPos > 0 Then
                    If Not (Me.m_Value >= CDbl(Me.m_MinValueForRed) And Me.m_Value <= CDbl(Me.m_MaxValueForRed)) Then
                        graphic.DrawImage(Me.GreenDecimalImage, CSng(Convert.ToInt32(CDbl(CSng(((Me.m_NumberOfDigits - Me.m_DecimalPos) * Me.SegWidth) - 50) * Me.ImageRatio) * 1.1)), 440.0F * Me.ImageRatio)
                    Else
                        graphic.DrawImage(Me.RedDecimalImage, CSng(Convert.ToInt32(CDbl(CSng(((Me.m_NumberOfDigits - Me.m_DecimalPos) * Me.SegWidth) - 50) * Me.ImageRatio) * 1.1)), 440.0F * Me.ImageRatio)
                    End If
                End If
                e.Graphics.DrawImage(Me._backBuffer, 0, 0)
            End Using
        End If
    End Sub



    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Me._backBuffer IsNot Nothing Then
            Me._backBuffer.Dispose()
            Me._backBuffer = Nothing
        End If
        Me._Resize(Me, Nothing)
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)

        RaiseEvent ValueChanged(Me, e)

    End Sub
#End Region
#Region "طرق"
    Private Sub _Resize(ByVal sender As Object, ByVal e As EventArgs)
        If Me.Height <> Me.LastHeight Or Me.Width <> Me.LastWidth Then
            Me.AdjustSize()
            Me.LastWidth = Me.Width
            Me.LastHeight = Me.Height
            Me.RefreshImage()
        End If
    End Sub
    Private Sub AdjustSize()
        Me.StaticImageRatio = CSng(CDbl(My.Resources.RedZero.Height) / (CDbl(My.Resources.RedZero.Width * Me.m_NumberOfDigits) * 1.1))
        If Me.LastHeight < Me.Height Or Me.LastWidth < Me.Width Then
            If CDbl(Me.Height) / CDbl(Me.Width) <= CDbl(Me.StaticImageRatio) Then
                Me.Height = CInt(Math.Truncate(Math.Round(CDbl(CSng(CSng(Me.Width) * Me.StaticImageRatio)))))
            Else
                Me.Width = CInt(Math.Truncate(Math.Round(CDbl(CSng(CSng(Me.Height) / Me.StaticImageRatio)))))
            End If
        ElseIf CDbl(Me.Height) / CDbl(Me.Width) <= CDbl(Me.StaticImageRatio) Then
            Me.Width = CInt(Math.Truncate(Math.Round(CDbl(CSng(CSng(Me.Height) / Me.StaticImageRatio)))))
        Else
            Me.Height = CInt(Math.Truncate(Math.Round(CDbl(CSng(CSng(Me.Width) * Me.StaticImageRatio)))))
        End If
    End Sub
    Private Sub RefreshImage()
        Dim gr_dest As Graphics
        Me.StaticImageRatio = CSng(CDbl(My.Resources.RedZero.Height) / (CDbl(My.Resources.RedZero.Width * Me.m_NumberOfDigits) * 1.1))
        '************************************************************
        '* Calculate the size ratio of the original t resized image
        '************************************************************
        Dim WidthRatio As Single = CSng(CDbl(Me.Width) / (CDbl(My.Resources.RedZero.Width * Me.m_NumberOfDigits) * 1.1))
        Dim HeightRatio As Single = CSng(CDbl(Me.Height) / CDbl(My.Resources.RedZero.Height))
        '========================================================
        If WidthRatio >= HeightRatio Then
            Me.x = CSng(Me.Width)
            Me.y = CSng(CDbl(My.Resources.RedZero.Height) / CDbl(My.Resources.RedZero.Width * Me.m_NumberOfDigits) * CDbl(Me.Width))
            Me.ImageRatio = HeightRatio
        Else
            Me.y = CSng(Me.Height)
            If Not (Me.Height > 0 And My.Resources.LED7Segment0Red.Height > 0) Then
                Me.x = 1.0F
            Else
                Me.x = CSng(CDbl(My.Resources.RedZero.Width * Me.m_NumberOfDigits) * 1.1 / CDbl(My.Resources.RedZero.Height) * CDbl(Me.Height))
            End If
            Me.ImageRatio = WidthRatio
        End If
        '==============================================================
        '************************************************
        '* Create a text rectangle and align to center
        '************************************************
        If Me.ImageRatio > 0.0F Then
            Me.TextRectangle.X = 0
            Me.TextRectangle.Y = CInt(Math.Truncate(Math.Round(CDbl(Me.Height) * 0.04)))
            Me.TextRectangle.Width = Me.Width
            Me.TextRectangle.Height = CInt(Math.Truncate(Math.Round(CDbl(Me.Height) * 0.18)))
            If Me.TextBrush Is Nothing Then
                Me.TextBrush = New SolidBrush(MyBase.ForeColor)
            End If
            '============================================================
            '* If number of digits is more than default image size (4), then draw left meter over right

            Dim LEDWidth As Integer = Convert.ToInt32(CSng(My.Resources.RedEight.Width) * Me.ImageRatio)
            Dim LEDHeight As Integer = Convert.ToInt32(CSng(My.Resources.RedEight.Height) * Me.ImageRatio)

            '****************************************************************
            ' Create Scaled LED images so it will draw faster in Paint event
            '****************************************************************
            Dim i As Integer = 0
            '****************************************
            '* Draw each of the RED digits
            '****************************************
            For i = 0 To 11
                If Me.RedLED(i) IsNot Nothing Then
                    Me.RedLED(i).Dispose()
                End If
                Me.RedLED(i) = New Bitmap(LEDWidth, LEDHeight)
                gr_dest = Graphics.FromImage(Me.RedLED(i))
                gr_dest.ScaleTransform(0.5 * ImageRatio, 0.5 * ImageRatio)
                SevenSegment2.SetNumber(gr_dest, i, Color.Red, True)

            Next

            '****************************************
            '* Draw each of the Green  digits
            '****************************************
            Dim r As Integer = 0
            For r = 0 To 11
                If Me.GreenLED(r) IsNot Nothing Then
                    Me.GreenLED(r).Dispose()
                End If
                Me.GreenLED(r) = New Bitmap(LEDWidth, LEDHeight)
                gr_dest = Graphics.FromImage(Me.GreenLED(r))
                gr_dest.ScaleTransform(0.5 * ImageRatio, 0.5 * ImageRatio)
                SevenSegment2.SetNumber(gr_dest, r, Color.Green, True)
            Next
            '* Draw the decimal point to the bitmap

            Me.RedDecimalImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.RedDecimal.Width) * Me.ImageRatio), Convert.ToInt32(CSng(My.Resources.RedDecimal.Height) * Me.ImageRatio))
            gr_dest = Graphics.FromImage(Me.RedDecimalImage)
            gr_dest.DrawImage(My.Resources.RedDecimal, 0, 0, Me.RedDecimalImage.Width, Me.RedDecimalImage.Height)
            Me.GreenDecimalImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.GreenDecimal.Width) * Me.ImageRatio), Convert.ToInt32(CSng(My.Resources.GreenDecimal.Height) * Me.ImageRatio))
            gr_dest = Graphics.FromImage(Me.GreenDecimalImage)
            gr_dest.DrawImage(My.Resources.GreenDecimal, 0, 0, Me.GreenDecimalImage.Width, Me.GreenDecimalImage.Height)


            '* Perform some cleanup
            gr_dest.Dispose()
            If Me._backBuffer IsNot Nothing Then
                Me._backBuffer.Dispose()
            End If
            Me._backBuffer = New Bitmap(Me.Width, Me.Height)

            Me.Invalidate()
        End If
    End Sub
#End Region
End Class
