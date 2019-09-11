Imports System.Drawing

Public Class DigitalPanelMeter
    Inherits System.Windows.Forms.UserControl

    '* These are used to keep prescaled images in memory. Scaling in Paint event is expensive operation
    Private StaticImage As Bitmap
    Private LED(11) As Bitmap
    Private DecimalImage As Bitmap


    Private ImageRatio As Single

    Private TextRectangle As New Rectangle
    Private NumberLocations(5) As Rectangle
    Private sfCenter, sfCenterBottom, sfRight, sfLeft As New StringFormat



#Region "Properties"
    Private m_Value As Single
    Public Property Value() As Single
        Get
            Return m_Value
        End Get
        Set(ByVal value As Single)
            If value <> m_Value Then
                '* Limit the value within the Min-Max range
                'm_Value = Math.Max(Math.Min(value, 9999), -999)
                m_Value = value

                '*Minimize the redraw region to help the speed 
                Me.Invalidate(New Rectangle(StaticImage.Width * 0.08, StaticImage.Height * 0.19, StaticImage.Width * 0.65, StaticImage.Height * 0.4))
            End If
        End Set
    End Property

    Private m_ValueScaleFactor As Decimal = 1
    Public Property ValueScaleFactor() As Decimal
        Get
            Return m_ValueScaleFactor
        End Get
        Set(ByVal value As Decimal)
            m_ValueScaleFactor = value
            Me.Invalidate(New Rectangle(StaticImage.Width * 0.08, StaticImage.Height * 0.19, StaticImage.Width * 0.65, StaticImage.Height * 0.4))
        End Set
    End Property

    Private m_Text As String = "Text"
    Public Property GaugeText() As String
        Get
            Return m_Text
        End Get
        Set(ByVal value As String)
            m_Text = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property



    Private m_DecimalPos As Integer = 0
    Public Property DecimalPosition() As Integer
        Get
            Return m_DecimalPos
        End Get
        Set(ByVal value As Integer)
            '* make sure the range is an increment of 10
            m_DecimalPos = Math.Max(Math.Min(3, value), 0)
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property






    '* This is necessary to make the background draw correctly
    '*  http://www.bobpowell.net/transcontrols.htm
    '*part of the transparent background code
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 32
            Return cp
            Return MyBase.CreateParams
        End Get
    End Property
#End Region

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************


    '* This is part of the transparent background code and it stops flicker
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        'MyBase.OnPaintBackground(e)
    End Sub

    Dim x, y As Single
    Dim m As New System.Drawing.Drawing2D.Matrix


    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private _backBuffer As Bitmap
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If StaticImage Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)


        g.DrawImage(StaticImage, 0, 0)


        '* Draw each of the digits
        Dim d As Integer
        Dim SegWidth As Integer = 110
        Dim WorkValue As Single = m_Value * m_ValueScaleFactor
        Dim DigitsStarted As Boolean
        If WorkValue <= 9999 And WorkValue >= -999 Then
            For i = 1 To 4
                If WorkValue < 0 Then
                    g.DrawImage(LED(11), (100 + SegWidth * (i - 1)) * ImageRatio, 85 * ImageRatio)
                    WorkValue = Math.Abs(WorkValue)
                Else

                    d = CInt(Math.Floor(WorkValue / 10 ^ (4 - i)))

                    '* Determine when to use Blank(all off) or zero
                    If d > 0 Or i = 4 Or i > (4 - m_DecimalPos) Then DigitsStarted = True

                    If DigitsStarted Then
                        g.DrawImage(LED(d), (100 + SegWidth * (i - 1)) * ImageRatio, 85 * ImageRatio)
                    Else
                        g.DrawImage(LED(10), (100 + SegWidth * (i - 1)) * ImageRatio, 85 * ImageRatio)
                    End If

                    WorkValue -= d * 10 ^ (4 - i)
                End If
            Next
        Else
            '* Draw all -'s
            For i = 1 To 4
                g.DrawImage(LED(11), (100 + SegWidth * (i - 1)) * ImageRatio, 85 * ImageRatio)
            Next
        End If


        '* Draw the decimal point
        If m_DecimalPos > 0 Then
            g.DrawImage(DecimalImage, ((4 - m_DecimalPos) * 110 + 80) * ImageRatio, 192 * ImageRatio)
        End If

        'Copy the back buffer to the screen
        e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub


    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub




    Private LastWidth, LastHeight As Integer

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        If Not (_backBuffer Is Nothing) Then
            _backBuffer.Dispose()
            _backBuffer = Nothing
        End If

        MyBase.OnSizeChanged(e)
    End Sub

    Private StaticImageRatio As Single = My.Resources.LEDPanelMeter.Height / My.Resources.LEDPanelMeter.Width
    Private Sub Gauge1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        '* Is the size increasing or decreasing?
        If LastHeight < Me.Height Or LastWidth < Me.Width Then
            If Me.Height / Me.Width > StaticImageRatio Then
                Me.Width = Me.Height / StaticImageRatio
            Else
                Me.Height = Me.Width * StaticImageRatio
            End If
        Else
            If Me.Height / Me.Width > StaticImageRatio Then
                Me.Height = Me.Width * StaticImageRatio
            Else
                Me.Width = Me.Height / StaticImageRatio
            End If
        End If

        LastWidth = Me.Width
        LastHeight = Me.Height

        'LastWidth = Me.Width
        'LastHeight = Me.Height

        RefreshImage()

        ''* Tighten up the control size to match the image
        'Me.Width = StaticImage.Width
        'Me.Height = StaticImage.Height
    End Sub

    Private Sub RefreshImage()
        '************************************************************
        '* Calculate the size ratio of the original t resized image
        '************************************************************
        Dim WidthRatio As Single = Me.Width / My.Resources.LEDPanelMeter.Width
        Dim HeightRatio As Single = Me.Height / My.Resources.LEDPanelMeter.Height

        If WidthRatio < HeightRatio Then
            y = Me.Height
            If Me.Height > 0 And My.Resources.MeterNoNeedle2.Height > 0 Then
                x = My.Resources.LEDPanelMeter.Width / My.Resources.LEDPanelMeter.Height * Me.Height
            Else
                x = 1
            End If
            ImageRatio = WidthRatio * 1.25
        Else
            x = Me.Width
            y = My.Resources.LEDPanelMeter.Height / My.Resources.LEDPanelMeter.Width * Me.Width
            ImageRatio = HeightRatio * 1.25
        End If



        '****************************************************************
        ' Scale the gauge image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        'Dim bm_dest As New Bitmap(Me.Width, Me.Height)
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        StaticImage = New Bitmap(CInt(My.Resources.LEDPanelMeter.Width * ImageRatio), CInt(My.Resources.LEDPanelMeter.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(StaticImage)
        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.LEDPanelMeter, 0, 0)


        '************************************************
        '* Create a text rectangle and align to center
        '************************************************
        TextRectangle.X = My.Resources.LEDPanelMeter.Width * 0.05
        TextRectangle.Y = 30
        TextRectangle.Width = My.Resources.LEDPanelMeter.Width * 0.7
        TextRectangle.Height = 40

        sfCenterBottom.Alignment = StringAlignment.Center
        sfCenterBottom.LineAlignment = StringAlignment.Far

        Dim f As New Font("Arial", 24, FontStyle.Regular, GraphicsUnit.Point)
        Dim b As New SolidBrush(Color.FromArgb(245, 100, 100, 100))


        gr_dest.DrawString(m_Text, f, b, TextRectangle, sfCenterBottom)
        'gr_dest.DrawRectangle(Pens.Tomato, TextRectangle)


        'TextRenderer.DrawText(gr_dest, "Test String", New Font("Arial", 36 * ImageRatio, FontStyle.Regular, GraphicsUnit.Point), NumberLocations(10), Color.Beige)
        ' Display the result.
        'GaugeImage = bm_dest


        '****************************************************************
        ' Create Scaled LED images so it will draw faster in Paint event
        '****************************************************************
        'm.Reset()
        Dim LEDWidth, LEDHeight As Integer
        LEDWidth = CInt(My.Resources.LED7Segment8Red.Width * ImageRatio / 1.25)
        LEDHeight = CInt(My.Resources.LED7Segment8Red.Height * ImageRatio / 1.25)
        Dim x1 As Single = ImageRatio
        'm.Scale(ImageRatio, ImageRatio)
        'gr_dest.Transform = m

        For i As Integer = 0 To 11
            If LED(i) IsNot Nothing Then LED(i).Dispose()
            LED(i) = New Bitmap(LEDWidth, LEDHeight)
            gr_dest = Graphics.FromImage(LED(i))
            gr_dest.Transform = m

            Select Case i
                Case 0 : gr_dest.DrawImage(My.Resources.LED7Segment0Red, 0, 0)
                Case 1 : gr_dest.DrawImage(My.Resources.LED7Segment1Red, 0, 0)
                Case 2 : gr_dest.DrawImage(My.Resources.LED7Segment2Red, 0, 0)
                Case 3 : gr_dest.DrawImage(My.Resources.LED7Segment3Red, 0, 0)
                Case 4 : gr_dest.DrawImage(My.Resources.LED7Segment4Red, 0, 0)
                Case 5 : gr_dest.DrawImage(My.Resources.LED7Segment5Red, 0, 0)
                Case 6 : gr_dest.DrawImage(My.Resources.LED7Segment6Red, 0, 0)
                Case 7 : gr_dest.DrawImage(My.Resources.LED7Segment7Red, 0, 0)
                Case 8 : gr_dest.DrawImage(My.Resources.LED7Segment8Red, 0, 0)
                Case 9 : gr_dest.DrawImage(My.Resources.LED7Segment9Red, 0, 0)
                Case 10 : gr_dest.DrawImage(My.Resources.LED7SegmentOffRed, 0, 0)
                Case 11 : gr_dest.DrawImage(My.Resources.LED7Segment_Red, 0, 0)
            End Select
        Next

        '* Draw the decimal point to the bitmap
        DecimalImage = New Bitmap(CInt(My.Resources.LED7SegmentDecimalRed.Width * ImageRatio / 1.25), CInt(My.Resources.LED7SegmentDecimalRed.Height * ImageRatio / 1.25))
        gr_dest = Graphics.FromImage(DecimalImage)
        gr_dest.Transform = m
        gr_dest.DrawImage(My.Resources.LED7SegmentDecimalRed, 0, 0)

        '* Perform some cleanup
        gr_dest.Dispose()


        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)
    End Sub
End Class
