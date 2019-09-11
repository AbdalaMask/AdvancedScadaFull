Imports System.Drawing

Public Class PilotLight
    Inherits System.Windows.Forms.UserControl

    Private StaticImage As Bitmap
    Private ButtonImage, LightOnImage, LightOffImage As Bitmap
    Private TextRect As New Rectangle
    Private ImageRatio As Single
    Private m As New System.Drawing.Drawing2D.Matrix

#Region "Properties"
    Private m_Value As Single
    Public Property Value() As Boolean
        Get
            Return m_Value
        End Get
        Set(ByVal value As Boolean)
            If value <> m_Value Then
                m_Value = value

                If m_Value Then
                    ButtonImage = LightOnImage
                Else
                    ButtonImage = LightOffImage
                End If
                Me.Invalidate()

                Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Color and type of button
    '*****************************************
    Public Enum ButtonColors
        Red
        Green
    End Enum
    Private m_ButtonColor As ButtonColors = ButtonColors.Green
    Public Property ButtonColor() As ButtonColors
        Get
            Return (m_ButtonColor)
        End Get
        Set(ByVal value As ButtonColors)
            m_ButtonColor = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Text on Legend Plate
    '*****************************************
    Private m_LegendText As String = "Text"
    Public Property LegendText() As String
        Get
            Return m_LegendText
        End Get
        Set(ByVal value As String)
            m_LegendText = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property






    '*****************************************
    '* Property - What to do to bit in PLC
    '*****************************************
    Public Enum OutputTypes
        MomentarySet
        MomentaryReset
        SetTrue
        SetFalse
        Toggle
    End Enum
    Private m_OutputType As OutputTypes = OutputTypes.MomentarySet
    Public Property OutputType() As OutputTypes
        Get
            Return m_OutputType
        End Get
        Set(ByVal value As OutputTypes)
            m_OutputType = value
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


#Region "Events"
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

    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private _backBuffer As Bitmap
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If StaticImage Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)

        g.DrawImage(StaticImage, 0, 0)


        g.DrawImage(ButtonImage, CInt((StaticImage.Width / 2 - ButtonImage.Width / 2)), CInt(StaticImage.Height * 0.43))

        'Copy the back buffer to the screen
        e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub




    '****************************
    '* Event - Mouse Down
    '****************************
    Private Sub MomentaryButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Exit Sub

        ButtonImage = LightOnImage
        Me.Invalidate()


    End Sub


    '****************************
    '* Event - Mouse Up
    '****************************
    Private Sub MomentaryButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp


        ButtonImage = LightOffImage
        Me.Invalidate()



    End Sub





    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Private Sub PolledDataReturned(ByVal Values() As String)
        Try
            Value = Values(0)
        Catch
            If Values(0).Length < 10 Then
                LegendText = "INVALID VALUE!"
            Else
                LegendText = Values(0)
            End If
        End Try
    End Sub





#End Region


    'Private NamePlateRatio As Single = My.Resources.NamePlate.Height / My.Resources.NamePlate.Width
    Private LegendPlateRatio As Single = My.Resources.LegendPlate2.Height / My.Resources.LegendPlate2.Width
    Private LastWidth, LastHeight As Integer
    Private Sub MomentaryButton_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If LastHeight < Me.Height Or LastWidth < Me.Width Then
            If Me.Height / Me.Width > LegendPlateRatio Then
                Me.Width = Me.Height / LegendPlateRatio
            Else
                Me.Height = Me.Width * LegendPlateRatio
            End If
        Else
            If Me.Height / Me.Width > LegendPlateRatio Then
                Me.Height = Me.Width * LegendPlateRatio
            Else
                Me.Width = Me.Height / LegendPlateRatio
            End If
        End If

        LastWidth = Me.Width
        LastHeight = Me.Height

        RefreshImage()
    End Sub

    Private Sub RefreshImage()
        Dim WidthRatio As Single = CSng(Me.Width) / CSng(My.Resources.LegendPlate2.Width)
        Dim HeightRatio As Single = CSng(Me.Height) / CSng(My.Resources.LegendPlate2.Height)

        If WidthRatio < HeightRatio Then
            ImageRatio = WidthRatio
        Else
            ImageRatio = HeightRatio
        End If


        '****************************************************************
        ' Scale the gauge image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        StaticImage = New Bitmap(CInt(My.Resources.LegendPlate2.Width * ImageRatio), CInt(My.Resources.LegendPlate2.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(StaticImage)
        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.LegendPlate2, 0, 0)

        TextRect.X = 5
        TextRect.Width = StaticImage.Width / 1.25 / ImageRatio - 10
        TextRect.Y = 5
        TextRect.Height = StaticImage.Height / 1.25 / ImageRatio * 0.33
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'gr_dest.DrawRectangle(Pens.Black, TextRect)
        Dim b As New SolidBrush(Color.FromArgb(250, 130, 140, 160))
        gr_dest.DrawString(m_LegendText, New Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Point), b, TextRect, sf)


        Dim x() As Int16 = {My.Resources.RedLightOff.Width, My.Resources.GreenLightOff.Width}

        Select m_ButtonColor
            Case ButtonColors.Red
                LightOffImage = New Bitmap(CInt(My.Resources.RedLightOff.Width * ImageRatio * 0.8), CInt(My.Resources.RedLightOff.Height * ImageRatio * 0.8))
                LightOnImage = New Bitmap(CInt(My.Resources.RedLightOn.Width * ImageRatio * 0.8), CInt(My.Resources.RedLightOn.Height * ImageRatio * 0.8))
            Case Else
                LightOffImage = New Bitmap(CInt(My.Resources.GreenLightOff.Width * ImageRatio * 0.8), CInt(My.Resources.GreenLightOff.Height * ImageRatio * 0.8))
                LightOnImage = New Bitmap(CInt(My.Resources.GreenLightOn.Width * ImageRatio * 0.8), CInt(My.Resources.GreenLightOn.Height * ImageRatio * 0.8))
        End Select

        gr_dest = Graphics.FromImage(LightOffImage)
        Dim gr_dest2 As Graphics = Graphics.FromImage(LightOnImage)

        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m
        gr_dest2.Transform = m

        Select m_ButtonColor
            Case ButtonColors.Red
                gr_dest.DrawImage(My.Resources.RedLightOff, 0, 0)
                gr_dest2.DrawImage(My.Resources.RedLightOn, 0, 0)
            Case Else
                gr_dest.DrawImage(My.Resources.GreenLightOff, 0, 0)
                gr_dest2.DrawImage(My.Resources.GreenLightOn, 0, 0)
        End Select

        ButtonImage = LightOffImage

        '* Perform some cleanup
        gr_dest.Dispose()
        gr_dest2.Dispose()

        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)
    End Sub
End Class
