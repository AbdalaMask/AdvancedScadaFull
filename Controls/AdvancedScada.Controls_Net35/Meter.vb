Imports System.Drawing

Public Class Meter
    Inherits System.Windows.Forms.UserControl

    '* These are used to keep prescaled images in memory. Scaling in Paint event is expensive operation
    Private StaticImage As Bitmap
    Private MeterBaseImage As Bitmap
    Private Needle As Bitmap
    Private LED(9) As Bitmap

    '* Center point of needle rotation
    Private Cx As Single
    Private Cy As Single
    Private Xoff, YOff As Integer
    Private ZeroCentered As Boolean

    Private ImageRatio As Single

    Private TextRectangle As New Rectangle
    Private NumberLocations(6) As Rectangle
    Private sfCenter, sfCenterBottom, sfRight, sfLeft As New StringFormat



#Region "Properties"
    Private m_Value As Decimal
    '* Angle to rotate 
    Private m_Angle As Decimal
    Public Property Value() As Decimal
        Get
            Return m_Value
        End Get
        Set(ByVal value As Decimal)
            Dim savedValue = m_Value
            '* Limit the value within the Min-Max range
            m_Value = Math.Max(Math.Min(value, m_MaxValue / m_ValueScaleFactor), m_MinValue / m_ValueScaleFactor)

            '* Scale the value to an angle and offset 
            If ZeroCentered Then
                m_Angle = ((m_Value * m_ValueScaleFactor - m_MinValue) * (1.5 / ((m_MaxValue - m_MinValue)))) * -1 + 0.75
            Else
                m_Angle = ((m_Value * m_ValueScaleFactor - m_MinValue) * (1.25 / ((m_MaxValue - m_MinValue)))) * -1 + 0.625
            End If

            '*Minimize the redraw region to help the speed 
            If savedValue <> m_Value Then
                Me.Invalidate(New Rectangle(0, 0, StaticImage.Width * 0.77, StaticImage.Height * 0.5))
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
            value = m_Value
            Me.Invalidate(New Rectangle(StaticImage.Width * 0.12, StaticImage.Height * 0.14, StaticImage.Width * 0.76, StaticImage.Height * 0.4))
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

    Private m_MaxValue As Decimal = 100
    Public Property MaxValue() As Decimal
        Get
            Return m_MaxValue
        End Get
        Set(ByVal value As Decimal)
            '* make sure the range is an increment of 10
            'm_MaxValue = Math.Ceiling((value - m_MinValue) / 10) * 10 + m_MinValue
            m_MaxValue = value

            '* Make sure the needle position is updated
            Me.Value = m_Value

            RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Private m_MinValue As Decimal = 0
    Public Property MinValue() As Decimal
        Get
            Return m_MinValue
        End Get
        Set(ByVal value As Decimal)
            m_MinValue = value

            If m_MinValue >= m_MaxValue Then m_MaxValue = m_MinValue + 1
            MaxValue = m_MaxValue

            '* Make sure the needle position is updated
            Me.Value = m_Value

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



    '* This is part of the transparent background code and it stops flicker
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        'MyBase.OnPaintBackground(e)
    End Sub

    'Dim x, y As Single
    Dim m As New System.Drawing.Drawing2D.Matrix


    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private _backBuffer As Bitmap
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If StaticImage Is Nothing Then Exit Sub

        If _backBuffer Is Nothing Then
            _backBuffer = New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)
        End If
        Dim g As Graphics = Graphics.FromImage(_backBuffer)


        g.DrawImage(StaticImage, 0, 0)

        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        g.Transform = m



        m.Reset()
        'm.Scale(ImageRatio, ImageRatio)
        m.Translate(Xoff * ImageRatio, YOff * ImageRatio)
        m.RotateAt(-m_Angle * 180 / Math.PI, New Point(Cx * ImageRatio, Cy * ImageRatio))
        g.Transform = m

        g.DrawImage(Needle, 0, 0)

        m.Reset()
        g.Transform = m
        g.DrawImage(MeterBaseImage, 1 * ImageRatio, 115 * ImageRatio)


        'Copy the back buffer to the screen
        e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0)
    End Sub


    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub





    Private Sub PolledDataReturned(ByVal Values() As String)
        Try
            Value = Values(0)
        Catch
            Value = m_MaxValue
            If Values(0).Length < 10 Then
                GaugeText = "INVALID VALUE!"
            Else
                GaugeText = Values(0)
            End If
        End Try
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
        'If LastHeight < Me.Height Or LastWidth < Me.Width Then
        '    If Me.Height >= Me.Width Then
        '        Me.Width = Me.Height
        '    Else
        '        Me.Height = Me.Width
        '    End If
        'Else
        '    If Me.Height >= Me.Width Then
        '        Me.Height = Me.Width
        '    Else
        '        Me.Width = Me.Height
        '    End If
        'End If

        'LastWidth = Me.Width
        'LastHeight = Me.Height

        RefreshImage()
    End Sub


    Private Sub RefreshImage()
        '************************************************************
        '* Calculate the size ratio of the original t resized image
        '************************************************************
        Dim WidthRatio As Single = Me.Width / My.Resources.MeterNoNeedle2.Width
        Dim HeightRatio As Single = Me.Height / My.Resources.MeterNoNeedle2.Height

        If WidthRatio < HeightRatio Then
            ImageRatio = WidthRatio * 1.25
        Else
            ImageRatio = HeightRatio * 1.25
        End If
        'ImageRatio = 1

        Dim h As Integer = My.Resources.MeterNeedle.Height
        Dim Height As Integer = My.Resources.MeterNeedle.Height * ImageRatio
        Dim Width As Integer = My.Resources.MeterNeedle.Width * ImageRatio

        '* Center point of rotatin for needle image
        Cx = My.Resources.MeterNeedle.Width / 2
        Cy = CSng(My.Resources.MeterNeedle.Height) + 40

        '* Location of needle image within gauge image
        Xoff = 123
        'Xoff = My.Resources.MeterNoNeedle.Width / 2 - Cx
        YOff = 45


        '************************************************
        '* Create a text rectangle and align to center
        '************************************************
        TextRectangle.X = 55
        TextRectangle.Y = 80
        TextRectangle.Width = 140
        TextRectangle.Height = 30

        sfCenterBottom.Alignment = StringAlignment.Center
        sfCenterBottom.LineAlignment = StringAlignment.Far

        '***********************************************
        '* Create location rectangles for gauge numbers
        '***********************************************
        sfLeft.Alignment = StringAlignment.Near
        sfCenter.Alignment = StringAlignment.Center
        sfRight.Alignment = StringAlignment.Far
        For i As Integer = 0 To 6
            NumberLocations(i) = Nothing
        Next

        If ZeroCentered Then
            NumberLocations(0) = New Rectangle(0 / 1.25, 85 / 1.25, 42, 20)
            NumberLocations(1) = New Rectangle(40 / 1.25, 60 / 1.25, 42, 20)
            NumberLocations(2) = New Rectangle(80 / 1.25, 42 / 1.25, 42, 20)
            NumberLocations(3) = New Rectangle(125 / 1.25, 38 / 1.25, 50, 20)
            NumberLocations(4) = New Rectangle(170 / 1.25, 42 / 1.25, 50, 20)
            NumberLocations(5) = New Rectangle(210 / 1.25, 60 / 1.25, 50, 20)
            NumberLocations(6) = New Rectangle(248 / 1.25, 85 / 1.25, 50, 20)
        Else
            NumberLocations(0) = New Rectangle(12 / 1.25, 73 / 1.25, 42, 20)
            NumberLocations(1) = New Rectangle(60 / 1.25, 50 / 1.25, 42, 20)
            NumberLocations(2) = New Rectangle(104 / 1.25, 38 / 1.25, 42, 20)
            NumberLocations(3) = New Rectangle(150 / 1.25, 38 / 1.25, 50, 20)
            NumberLocations(4) = New Rectangle(200 / 1.25, 50 / 1.25, 50, 20)
            NumberLocations(5) = New Rectangle(248 / 1.25, 73 / 1.25, 50, 20)
        End If


        '****************************************************************
        ' Scale the gauge image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        'GaugeImage = New Bitmap(Me.Width, Me.Height)
        StaticImage = New Bitmap(CInt(My.Resources.MeterNoNeedle2.Width * ImageRatio), CInt(My.Resources.MeterNoNeedle2.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(StaticImage)
        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        '* Check if it is a 0 centered scale
        If Math.Abs(m_MinValue) = Math.Abs(m_MaxValue) Then
            gr_dest.DrawImage(My.Resources.MeterNoNeedleCenterScale, 0, 0)
            ZeroCentered = True
        Else
            gr_dest.DrawImage(My.Resources.MeterNoNeedle2, 0, 0)
            ZeroCentered = False
        End If


        Dim increment As Decimal = (m_MaxValue - m_MinValue) / 5
        If ZeroCentered Then increment = (m_MaxValue - m_MinValue) / 6

        Dim f As New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point)
        Dim b As New SolidBrush(Color.FromArgb(245, 45, 45, 45))


        gr_dest.DrawString(m_Text, New Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Point), b, TextRectangle, sfCenterBottom)
        'gr_dest.DrawRectangle(Pens.Black, TextRectangle)

        gr_dest.DrawString(m_MinValue, f, b, NumberLocations(0), sfCenter)
        gr_dest.DrawString(m_MinValue + increment, f, b, NumberLocations(1), sfCenter)
        gr_dest.DrawString(m_MinValue + increment * 2, f, b, NumberLocations(2), sfCenter)
        If Not ZeroCentered Then
            gr_dest.DrawString(m_MinValue + increment * 3, f, b, NumberLocations(3), sfCenter)
        Else
            gr_dest.DrawString("0", f, b, NumberLocations(3), sfCenter)
        End If
        gr_dest.DrawString(m_MinValue + increment * 4, f, b, NumberLocations(4), sfCenter)
        gr_dest.DrawString(m_MinValue + increment * 5, f, b, NumberLocations(5), sfCenter)

        If ZeroCentered Then gr_dest.DrawString(m_MinValue + increment * 6, f, b, NumberLocations(6), sfCenter)

        'TextRenderer.DrawText(gr_dest, "Test String", New Font("Arial", 36 * ImageRatio, FontStyle.Regular, GraphicsUnit.Point), NumberLocations(10), Color.Beige)
        ' Display the result.
        'GaugeImage = bm_dest

        '****************************************************************
        ' Scale the needle image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If Needle IsNot Nothing Then Needle.Dispose()
        Needle = New Bitmap(My.Resources.MeterNeedle.Width, My.Resources.MeterNeedle.Height)

        ' Make a Graphics object for the result Bitmap.
        gr_dest = Graphics.FromImage(Needle)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.MeterNeedle, 0, 0)


        '****************************************************************
        ' Scale the needle image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If MeterBaseImage IsNot Nothing Then MeterBaseImage.Dispose()
        MeterBaseImage = New Bitmap(My.Resources.MeterBase.Width, My.Resources.MeterBase.Height)

        ' Make a Graphics object for the result Bitmap.
        gr_dest.Dispose()
        gr_dest = Graphics.FromImage(MeterBaseImage)
        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.MeterBase, 0, 0)

        gr_dest.Dispose()
    End Sub
End Class
