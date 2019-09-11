Imports System.Drawing

Public Class Gauge
    Inherits System.Windows.Forms.UserControl

    '* These are used to keep prescaled images in memory. Scaling in Paint event is expensive operation
    Private GaugeImage As Bitmap
    Private Needle As Bitmap
    Private NeedleShadow As Bitmap
    Private LED(9) As Bitmap

    '* Center point of needle rotation
    Private Cx As Single
    Private Cy As Single
    Private Xoff, YOff As Integer

    '*Hypotnuses
    Private ImageRatio As Single

    Private TextRectangle As New Rectangle
    Private NumberLocations(10) As Rectangle
    Private sfCenter, sfCenterBottom, sfRight, sfLeft As New StringFormat


    '* Angle to rotate 
    Private m_Angle As Single

#Region "Properties"
    Private m_Value As Single
    Private m_ScaledValue As Single
    Public Property Value() As Single
        Get
            Return m_Value
        End Get
        Set(ByVal value As Single)
            If value <> m_Value Then
                '* Limit the value within the Min-Max range
                m_Value = Math.Max(Math.Min(value, m_MaxValue / m_ValueScaleFactor), m_MinValue)

                '* Scale the value to an angle
                m_Angle = ((m_Value * m_ValueScaleFactor - m_MinValue) * (4.71 / ((m_MaxValue - m_MinValue)))) * -1

                '*Minimize the redraw region to help the speed 
                Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
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

            '* Limit the value within the Min-Max range
            m_Value = Math.Max(Math.Min(m_Value, m_MaxValue / m_ValueScaleFactor), m_MinValue)

            '* Scale the value to an angle
            m_Angle = ((m_Value * m_ValueScaleFactor - m_MinValue) * (4.71 / ((m_MaxValue - m_MinValue)))) * -1

            Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
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

    Private m_MaxValue As Integer = 100
    Public Property MaxValue() As Integer
        Get
            Return m_MaxValue
        End Get
        Set(ByVal value As Integer)
            '* make sure the range is an increment of 10
            m_MaxValue = Math.Ceiling((value - m_MinValue) / 10) * 10 + m_MinValue
            'm_MaxValue = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Private m_MinValue As Integer = 0
    Public Property MinValue() As Integer
        Get
            Return m_MinValue
        End Get
        Set(ByVal value As Integer)
            m_MinValue = value
            MaxValue = m_MaxValue
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




    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private NotificationID As Integer


    Private Sub PolledDataReturned(ByVal Values() As String)
        Try
            Value = Values(0)
        Catch
            Value = m_MaxValue
            GaugeText = "Invalid Value"
        End Try
    End Sub


    '* This is part of the transparent background code
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
        If GaugeImage Is Nothing Then Exit Sub

        If _backBuffer Is Nothing Then
            _backBuffer = New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)
        End If
        Dim g As Graphics = Graphics.FromImage(_backBuffer)


        g.DrawImage(GaugeImage, 0, 0)

        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        g.Transform = m

        Dim d As Integer
        Dim SegWidth As Integer = 35
        Dim WorkValue As Single = m_Value * m_ValueScaleFactor
        '* Limit to 3 digits
        If WorkValue > 999 Then WorkValue -= Math.Floor(WorkValue / 1000) * 1000
        For i = 1 To 3
            d = CInt(Math.Floor(WorkValue / 10 ^ (3 - i)))
            g.DrawImage(LED(d), 250 + SegWidth * (i - 1), 355)
            WorkValue -= d * 10 ^ (3 - i)
        Next


        '* Set the needle to start at the 0 position
        Dim AngleAdjusted As Single = m_Angle + 0.78

        m.Reset()
        m.Translate((Xoff + 8) * ImageRatio, (YOff + 8) * ImageRatio)
        m.RotateAt(-AngleAdjusted * 180 / Math.PI, New Point(Cx * ImageRatio, Cy * ImageRatio))
        g.Transform = m
        g.DrawImage(NeedleShadow, 0, 0)

        m.Reset()
        m.Translate(Xoff * ImageRatio, YOff * ImageRatio)
        m.RotateAt(-AngleAdjusted * 180 / Math.PI, New Point(Cx * ImageRatio, Cy * ImageRatio))
        g.Transform = m
        g.DrawImage(Needle, 0, 0)


        'Copy the back buffer to the screen
        e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0)
    End Sub

    Private LastWidth, LastHeight As Integer

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        If Not (_backBuffer Is Nothing) Then
            _backBuffer.Dispose()
            _backBuffer = Nothing
        End If

        MyBase.OnSizeChanged(e)
    End Sub

    Private Sub Gauge1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        '* Is the size increasing or decreasing?
        If LastHeight < Me.Height Or LastWidth < Me.Width Then
            If Me.Height >= Me.Width Then
                Me.Width = Me.Height
            Else
                Me.Height = Me.Width
            End If
        Else
            If Me.Height >= Me.Width Then
                Me.Height = Me.Width
            Else
                Me.Width = Me.Height
            End If
        End If

        LastWidth = Me.Width
        LastHeight = Me.Height

        RefreshImage()
    End Sub

    Private Sub RefreshImage()
        '************************************************************
        '* Calculate the size ratio of the original t resized image
        '************************************************************
        Dim Gw As Integer = My.Resources.GaugeSilverNoNeedle.Width
        Dim Gh As Integer = My.Resources.GaugeSilverNoNeedle.Height
        Dim WidthRatio As Single = Me.Width / My.Resources.GaugeSilverNoNeedle.Width
        Dim HeightRatio As Single = Me.Height / My.Resources.GaugeSilverNoNeedle.Height

        If WidthRatio < HeightRatio Then
            y = Me.Height
            If Me.Height > 0 And My.Resources.GaugeSilverNoNeedle.Height > 0 Then
                x = My.Resources.GaugeSilverNoNeedle.Width / My.Resources.GaugeSilverNoNeedle.Height * Me.Height
            Else
                x = 1
            End If
            ImageRatio = WidthRatio * 1.25
        Else
            x = Me.Width
            y = My.Resources.GaugeSilverNoNeedle.Height / My.Resources.GaugeSilverNoNeedle.Width * Me.Width
            ImageRatio = HeightRatio * 1.25
        End If
        'ImageRatio = 1

        Dim h As Integer = My.Resources.GaugeNeedle.Height
        Dim Height As Integer = My.Resources.GaugeNeedle.Height * ImageRatio
        Dim Width As Integer = My.Resources.GaugeNeedle.Width * ImageRatio

        '* Center point of rotatin for needle image
        Cx = 202
        Cy = CSng(My.Resources.GaugeNeedle.Height) / 2.0! - 2

        '* Location of needle image within gauge image
        Xoff = 95
        'Xoff = My.Resources.GaugeNoNeedle.Width / 2 - Cx
        YOff = 290


        '************************************************
        '* Create a text rectangle and align to center
        '************************************************
        TextRectangle.X = 220
        TextRectangle.Y = 192
        TextRectangle.Width = 156
        TextRectangle.Height = 64

        sfCenterBottom.Alignment = StringAlignment.Center
        sfCenterBottom.LineAlignment = StringAlignment.Far

        '***********************************************
        '* Create location rectangles for gauge numbers
        '***********************************************
        sfLeft.Alignment = StringAlignment.Near
        sfCenter.Alignment = StringAlignment.Center
        sfRight.Alignment = StringAlignment.Far
        NumberLocations(0) = Nothing
        NumberLocations(0) = New Rectangle(180, 378, 120, 80)
        NumberLocations(1) = Nothing
        NumberLocations(1) = New Rectangle(136, 324, 120, 80)
        NumberLocations(2) = Nothing
        NumberLocations(2) = New Rectangle(160 / 1.25, 316 / 1.25, 120, 80)
        NumberLocations(3) = Nothing
        NumberLocations(3) = New Rectangle(195 / 1.25, 241 / 1.25, 120, 80)
        NumberLocations(4) = Nothing
        NumberLocations(4) = New Rectangle(255 / 1.25, 189 / 1.25, 120, 80)
        NumberLocations(5) = Nothing
        NumberLocations(5) = New Rectangle(300 / 1.25, 155 / 1.25, 120, 80)
        NumberLocations(6) = Nothing
        NumberLocations(6) = New Rectangle(340 / 1.25, 189 / 1.25, 120, 80)
        NumberLocations(7) = Nothing
        NumberLocations(7) = New Rectangle(402 / 1.25, 241 / 1.25, 120, 80)
        NumberLocations(8) = Nothing
        NumberLocations(8) = New Rectangle(435 / 1.25, 316 / 1.25, 120, 80)
        NumberLocations(9) = Nothing
        NumberLocations(9) = New Rectangle(422 / 1.25, 405 / 1.25, 120, 80)
        NumberLocations(10) = Nothing
        NumberLocations(10) = New Rectangle(380 / 1.25, 473 / 1.25, 120, 80)



        '****************************************************************
        ' Scale the gauge image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If GaugeImage IsNot Nothing Then GaugeImage.Dispose()

        GaugeImage = New Bitmap(Me.Width, Me.Height)

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(GaugeImage)
        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.GaugeSilverNoNeedle, 0, 0)


        Dim increment As Integer = (m_MaxValue - m_MinValue) / 10
        Dim f As New Font("Arial", 28, FontStyle.Regular, GraphicsUnit.Point)
        Dim b As New SolidBrush(Color.FromArgb(250, 35, 35, 35))


        gr_dest.DrawString(m_Text, New Font("Arial", 24, FontStyle.Regular, GraphicsUnit.Point), b, TextRectangle, sfCenterBottom)

        gr_dest.DrawString(m_MinValue, f, b, NumberLocations(0), sfLeft)
        gr_dest.DrawString(m_MinValue + increment, f, b, NumberLocations(1), sfLeft)
        gr_dest.DrawString(m_MinValue + increment * 2, f, b, NumberLocations(2), sfLeft)
        gr_dest.DrawString(m_MinValue + increment * 3, f, b, NumberLocations(3), sfLeft)
        gr_dest.DrawString(m_MinValue + increment * 4, f, b, NumberLocations(4), sfLeft)
        gr_dest.DrawString(m_MinValue + increment * 5, f, b, NumberLocations(5), sfCenter)
        gr_dest.DrawString(m_MinValue + increment * 6, f, b, NumberLocations(6), sfRight)
        gr_dest.DrawString(m_MinValue + increment * 7, f, b, NumberLocations(7), sfRight)
        gr_dest.DrawString(m_MinValue + increment * 8, f, b, NumberLocations(8), sfRight)
        gr_dest.DrawString(m_MinValue + increment * 9, f, b, NumberLocations(9), sfRight)
        gr_dest.DrawString(m_MaxValue, f, b, NumberLocations(10), sfRight)

        '* TEST TO CHECK LED Readout
        gr_dest.FillRectangle(Brushes.Black, 247, 352, 103, 55)

        'TextRenderer.DrawText(gr_dest, "Test String", New Font("Arial", 36 * ImageRatio, FontStyle.Regular, GraphicsUnit.Point), NumberLocations(10), Color.Beige)
        ' Display the result.

        '****************************************************************
        ' Scale the needle image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If Needle IsNot Nothing Then Needle.Dispose()
        Needle = New Bitmap(My.Resources.GaugeNeedle.Width, My.Resources.GaugeNeedle.Height)

        ' Make a Graphics object for the result Bitmap.
        gr_dest = Graphics.FromImage(Needle)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.GaugeNeedle, 0, 0)

        '****************************************************************
        ' Scale the needle shadow image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If NeedleShadow IsNot Nothing Then NeedleShadow.Dispose()
        NeedleShadow = New Bitmap(My.Resources.GaugeNeedleShadow.Width, My.Resources.GaugeNeedleShadow.Height)

        ' Make a Graphics object for the result Bitmap.
        gr_dest = Graphics.FromImage(NeedleShadow)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.GaugeNeedleShadow, 0, 0)


        '****************************************************************
        ' Create Scaled LED images so it will draw faster in Paint event
        '****************************************************************
        'm.Reset()
        Dim LEDWidth, LEDHeight As Integer
        LEDWidth = CInt(My.Resources.LED7Segement0.Width / 1.25)
        LEDHeight = CInt(My.Resources.LED7Segement0.Height / 1.25)
        'm.Scale(ImageRatio, ImageRatio)

        For i As Integer = 0 To 9
            If LED(i) IsNot Nothing Then LED(i).Dispose()
            LED(i) = New Bitmap(LEDWidth, LEDHeight)
            gr_dest = Graphics.FromImage(LED(i))

            Select Case i
                Case 0 : gr_dest.DrawImage(My.Resources.LED7Segement0, 0, 0)
                Case 1 : gr_dest.DrawImage(My.Resources.LED7Segement1, 0, 0)
                Case 2 : gr_dest.DrawImage(My.Resources.LED7Segement2, 0, 0)
                Case 3 : gr_dest.DrawImage(My.Resources.LED7Segement3, 0, 0)
                Case 4 : gr_dest.DrawImage(My.Resources.LED7Segement4, 0, 0)
                Case 5 : gr_dest.DrawImage(My.Resources.LED7Segement5, 0, 0)
                Case 6 : gr_dest.DrawImage(My.Resources.LED7Segement6, 0, 0)
                Case 7 : gr_dest.DrawImage(My.Resources.LED7Segement7, 0, 0)
                Case 8 : gr_dest.DrawImage(My.Resources.LED7Segement8, 0, 0)
                Case 9 : gr_dest.DrawImage(My.Resources.LED7Segement9, 0, 0)
            End Select
        Next

        '* Perform some cleanup
        gr_dest.Dispose()


        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)
    End Sub
End Class
