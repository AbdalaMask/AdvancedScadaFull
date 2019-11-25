Imports System.Drawing

Public Class Tank
    Inherits System.Windows.Forms.UserControl

    Private BarValue As Single = 20
    Private TankImage As Bitmap
    Private m As New System.Drawing.Drawing2D.Matrix

    Private TextRectangle As New Rectangle
    Private sfCenter, sfCenterTop As New StringFormat

#Region "Properties"
    Private m_Value As Single
    Private ScaledValue As Single
    Private ScaleFactor As Single

    Public Property Value() As Single
        Get
            Return m_Value
        End Get
        Set(ByVal value As Single)
            If value <> m_Value Then
                '* Limit the value within the Min-Max range
                m_Value = Math.Max(Math.Min(value, m_MaxValue), m_MinValue)

                CalculateScaledValue()

                '*Minimize the redraw region to help the speed 
                Me.Invalidate(New Rectangle(TankImage.Width * 0.44, TankImage.Height * 0.1, TankImage.Width * 0.14, TankImage.Height * 0.55))
                'Me.Invalidate()
            End If
        End Set
    End Property

    Private m_Text As String = String.Empty
    Public Property LegendText() As String
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

            CalculateScaledValue()

            'm_MaxValue = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Private m_MinValue As Integer
    Public Property MinValue() As Integer
        Get
            Return m_MinValue
        End Get
        Set(ByVal value As Integer)
            m_MinValue = value
            MaxValue = m_MaxValue

            CalculateScaledValue()

            RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Private m_TankContentColor As New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal, Color.Aqua)
    'Private m_TankContentColor As New SolidBrush(Color.FromArgb(220, 40, 80, 200))
    Public Property TankContentColor() As Color
        Get
            Return m_TankContentColor.BackgroundColor
        End Get
        Set(ByVal value As Color)
            If m_TankContentColor IsNot Nothing Then m_TankContentColor.Dispose()
            'Dim ForeColor As New SolidBrush(Color.FromArgb(240, Math.Max(value.R - 20, 0), 20, 10))
            m_TankContentColor = New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal, Color.FromArgb(240, Math.Max(value.R - 40, 0), Math.Max(value.G - 40, 0), Math.Max(value.B - 40, 0)), value)
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
        TankImage.Dispose()
        _backBuffer.Dispose()
        m.Dispose()
        m_TankContentColor.Dispose()

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
            If Values(0).Length < 10 Then
                LegendText = "INVALID VALUE!"
            Else
                LegendText = Values(0)
            End If
        End Try
    End Sub


    Private Sub CalculateScaledValue()
        ScaleFactor = 1 / (m_MaxValue - m_MinValue) * ImageRatio * 438
        ScaledValue = m_Value * ScaleFactor
    End Sub


    '* This is part of the transparent background code and it stops flicker
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub


    Private _backBuffer As Bitmap
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If TankImage Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)

        g.DrawImage(TankImage, 0, 0)

        If m_TankContentColor IsNot Nothing Then
            g.FillRectangle(m_TankContentColor, 289 * ImageRatio, 130 * ImageRatio + m_MaxValue * ScaleFactor - m_Value * ScaleFactor, 55 * ImageRatio, ScaledValue)
        End If
        '        MyBase.OnPaint(e)

        'Copy the back buffer to the screen
        e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        If Not (_backBuffer Is Nothing) Then
            _backBuffer.Dispose()
            _backBuffer = Nothing
        End If

        MyBase.OnSizeChanged(e)

        If Me.Parent IsNot Nothing Then
            Me.Parent.Invalidate()
        End If
    End Sub


    Private ImageRatio As Single
    Private Sub RefreshImage()
        '************************************************************
        '* Calculate the size ratio of the original t resized image
        '************************************************************
        Dim WidthRatio As Single = Me.Width / My.Resources.TankWithWindow.Width
        Dim HeightRatio As Single = Me.Height / My.Resources.TankWithWindow.Height
        Dim x, y As Integer

        If WidthRatio < HeightRatio Then
            y = Me.Height
            If Me.Height > 0 And My.Resources.TankWithWindow.Height > 0 Then
                x = My.Resources.TankWithWindow.Width / My.Resources.TankWithWindow.Height * Me.Height
            Else
                x = 1
            End If
            ImageRatio = WidthRatio / 0.75
        Else
            x = Me.Width
            y = My.Resources.TankWithWindow.Height / My.Resources.TankWithWindow.Width * Me.Width
            ImageRatio = HeightRatio / 0.75
        End If



        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        'If TankImage IsNot Nothing Then TankImage.Dispose()
        'TankImage = New Bitmap(CInt(My.Resources.TankWithWindow.Width * ImageRatio), CInt(My.Resources.TankWithWindow.Height * ImageRatio))
        If TankImage IsNot Nothing Then TankImage.Dispose()
        TankImage = New Bitmap(CInt(My.Resources.TankWithWindow.Width * ImageRatio), CInt(My.Resources.TankWithWindow.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(TankImage)
        m.Reset()
        m.Scale(ImageRatio, ImageRatio)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.TankWithWindow, 0, 0)


        '************************************************
        '* Create a text rectangle and align to center
        '************************************************
        'm.Reset()
        'gr_dest.Transform = m
        TextRectangle.X = 0
        TextRectangle.Y = 50
        TextRectangle.Width = (My.Resources.TankWithWindow.Width) / 1.25
        TextRectangle.Height = My.Resources.TankWithWindow.Height

        sfCenterTop.Alignment = StringAlignment.Center
        sfCenterTop.LineAlignment = StringAlignment.Near

        Dim b As New SolidBrush(Color.FromArgb(250, 20, 20, 20))
        gr_dest.DrawString(m_Text, New Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Point), b, TextRectangle, sfCenterTop)
        'gr_dest.DrawRectangle(Pens.Black, TextRectangle)

        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)

        gr_dest.Dispose()

        Me.Invalidate()
    End Sub


    Private Sub Tank_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        CalculateScaledValue()
        RefreshImage()
    End Sub
End Class
