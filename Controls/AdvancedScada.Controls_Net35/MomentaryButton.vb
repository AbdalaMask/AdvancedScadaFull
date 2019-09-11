Imports System.Drawing

Public Class MomentaryButton
    Inherits System.Windows.Forms.UserControl

    Private StaticImage As Bitmap
    Private ButtonImage, ButtonUpImage, ButtonDownImage As Bitmap
    Private TextRect As New Rectangle
    Private ImageRatio As Single
    Private m As New System.Drawing.Drawing2D.Matrix

#Region "Properties"
    '*****************************************
    '* Property - Color and type of button
    '*****************************************
    Public Enum ButtonColors
        Red
        Green
        Blue
        Black
        BlackSelector
        RedMushroom
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

        'm.Translate(StaticImage.Width / 2 - ButtonImage.Width * ImageRatio / 2, StaticImage.Height * 0.68 - ButtonImage.Height * ImageRatio / 2.5)
        'e.Graphics.DrawImage(ButtonImage, NamePlateImage.Width / ImageRatio / 2.5! - ButtonImage.Width * ImageRatio / 2.5!, NamePlateImage.Height * 0.6! - (ButtonImage.Height * ImageRatio) / 2.0!)
        g.DrawImage(ButtonImage, CInt(StaticImage.Width / 2 - ButtonImage.Width / 2), CInt(StaticImage.Height * 0.68 - ButtonImage.Height / 2))

        'Copy the back buffer to the screen
        e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub




    '****************************
    '* Event - Mouse Down
    '****************************
    Private Sub MomentaryButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ButtonImage = ButtonDownImage
        Me.Invalidate()


    End Sub


    '****************************
    '* Event - Mouse Up
    '****************************
    Private Sub MomentaryButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        ButtonImage = ButtonUpImage
        Me.Invalidate()


    End Sub




    Dim OriginalText As String

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
        'StaticImage = New Bitmap(CInt(My.Resources.NamePlate.Width * ImageRatio), CInt(My.Resources.NamePlate.Height * ImageRatio))
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


        Select Case m_ButtonColor
            Case ButtonColors.Red
                ButtonUpImage = New Bitmap(CInt(My.Resources.RedButton.Width * ImageRatio), CInt(My.Resources.RedButton.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.RedButtonDown.Width * ImageRatio), CInt(My.Resources.RedButtonDown.Height * ImageRatio))
            Case ButtonColors.BlackSelector
                ButtonUpImage = New Bitmap(CInt(My.Resources.SelectorSwitchLeft.Width * ImageRatio), CInt(My.Resources.SelectorSwitchLeft.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.SelectorSwitchRight.Width * ImageRatio), CInt(My.Resources.SelectorSwitchRight.Height * ImageRatio))
            Case ButtonColors.Blue
                ButtonUpImage = New Bitmap(CInt(My.Resources.BlueButton.Width * ImageRatio), CInt(My.Resources.BlueButton.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.BlueButtonDown.Width * ImageRatio), CInt(My.Resources.BlueButtonDown.Height * ImageRatio))
            Case ButtonColors.Black
                ButtonUpImage = New Bitmap(CInt(My.Resources.BlackButton.Width * ImageRatio), CInt(My.Resources.BlackButton.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.BlackButtonDown.Width * ImageRatio), CInt(My.Resources.BlackButtonDown.Height * ImageRatio))
            Case ButtonColors.RedMushroom
                ButtonUpImage = New Bitmap(CInt(My.Resources.EstopButton.Width * ImageRatio), CInt(My.Resources.EstopButton.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.EstopButtonDown.Width * ImageRatio), CInt(My.Resources.EstopButtonDown.Height * ImageRatio))
            Case Else
                ButtonUpImage = New Bitmap(CInt(My.Resources.GreenButton.Width * ImageRatio), CInt(My.Resources.GreenButton.Height * ImageRatio))
                ButtonDownImage = New Bitmap(CInt(My.Resources.GreenButtonDown.Width * ImageRatio), CInt(My.Resources.GreenButtonDown.Height * ImageRatio))
        End Select

        gr_dest = Graphics.FromImage(ButtonUpImage)
        Dim gr_dest2 As Graphics = Graphics.FromImage(ButtonDownImage)

        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m
        gr_dest2.Transform = m

        Select Case m_ButtonColor
            Case ButtonColors.Red
                gr_dest.DrawImage(My.Resources.RedButton, 0, 0)
                gr_dest2.DrawImage(My.Resources.RedButtonDown, 0, 0)
            Case ButtonColors.BlackSelector
                gr_dest.DrawImage(My.Resources.SelectorSwitchLeft, 0, 0)
                gr_dest2.DrawImage(My.Resources.SelectorSwitchRight, 0, 0)
            Case ButtonColors.Blue
                gr_dest.DrawImage(My.Resources.BlueButton, 0, 0)
                gr_dest2.DrawImage(My.Resources.BlueButtonDown, 0, 0)
            Case ButtonColors.Black
                gr_dest.DrawImage(My.Resources.BlackButton, 0, 0)
                gr_dest2.DrawImage(My.Resources.BlackButtonDown, 0, 0)
            Case ButtonColors.RedMushroom
                gr_dest.DrawImage(My.Resources.EstopButton, 0, 0)
                gr_dest2.DrawImage(My.Resources.EstopButtonDown, 0, 0)
            Case Else
                gr_dest.DrawImage(My.Resources.GreenButton, 0, 0)
                gr_dest2.DrawImage(My.Resources.GreenButtonDown, 0, 0)
        End Select

        ButtonImage = ButtonUpImage

        '* Perform some cleanup
        gr_dest.Dispose()
        gr_dest2.Dispose()

        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)

        Me.Invalidate()
    End Sub
End Class
