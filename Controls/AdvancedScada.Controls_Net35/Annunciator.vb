Imports System.Drawing

Public Class Annunciator
    Inherits System.Windows.Forms.UserControl

    Public StaticImage As Bitmap
    Private Image1, Image2 As Bitmap
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

                If value Then
                    StaticImage = Image2
                Else
                    StaticImage = Image1
                End If

                Me.Invalidate()
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
    Private m_Text As String = "Text"
    Public Property TextDisplay() As String
        Get
            Return m_Text
        End Get
        Set(ByVal value As String)
            m_Text = value
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
        End Get
    End Property
#End Region


#Region "Events"
    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Image1.Dispose()
        Image2.Dispose()
        _backBuffer.Dispose()
        m.Dispose()

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
        If StaticImage Is Nothing Or Image2 Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)

        g.DrawImage(StaticImage, 0, 0)


        'Copy the back buffer to the screen
        e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0)

        '_backBuffer.Dispose()
        g.Dispose()
    End Sub




    '****************************
    '* Event - Mouse Down
    '****************************
    Private Sub m_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Exit Sub

        Me.Invalidate()

    End Sub


    '****************************
    '* Event - Mouse Up
    '****************************
    Private Sub MomentaryButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Exit Sub

        Me.Invalidate()


    End Sub




#End Region


    'Private NamePlateRatio As Single = My.Resources.NamePlate.Height / My.Resources.NamePlate.Width
    Private SizeRatio As Single = My.Resources.Annunciator.Height / My.Resources.Annunciator.Width
    'Private LastWidth, LastHeight As Integer
    Private Sub m_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'If LastHeight < Me.Height Or LastWidth < Me.Width Then
        '    If Me.Height / Me.Width > SizeRatio Then
        '        Me.Width = Me.Height / SizeRatio
        '    Else
        '        Me.Height = Me.Width * SizeRatio
        '    End If
        'Else
        '    If Me.Height / Me.Width > SizeRatio Then
        '        Me.Height = Me.Width * SizeRatio
        '    Else
        '        Me.Width = Me.Height / SizeRatio
        '    End If
        'End If

        'LastWidth = Me.Width
        'LastHeight = Me.Height

        RefreshImage()
    End Sub

    Private Sub RefreshImage()
        Dim WidthRatio As Single = CSng(Me.Width) / CSng(My.Resources.Annunciator.Width)
        Dim HeightRatio As Single = CSng(Me.Height) / CSng(My.Resources.Annunciator.Height)

        If WidthRatio < HeightRatio Then
            ImageRatio = WidthRatio
        Else
            ImageRatio = HeightRatio
        End If


        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If Image1 IsNot Nothing Then Image1.Dispose()
        Image1 = New Bitmap(CInt(My.Resources.Annunciator.Width * ImageRatio), CInt(My.Resources.Annunciator.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(Image1)
        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.Annunciator, 0, 0)

        TextRect.X = 1
        TextRect.Width = My.Resources.Annunciator.Width / 1.25 - 4
        TextRect.Y = 1
        TextRect.Height = My.Resources.Annunciator.Height / 1.25 - 2
        'TextRect.Height = My.Resources.Annunciator.Height / 1.25 - 30
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'gr_dest.DrawRectangle(Pens.Aqua, TextRect)


        Dim b As New SolidBrush(Color.FromArgb(250, 60, 70, 60))
        gr_dest.DrawString(m_Text, New Font("Arial", 30, FontStyle.Regular, GraphicsUnit.Point), b, TextRect, sf)


        Image2 = New Bitmap(CInt(My.Resources.AnnunciatorRed.Width * ImageRatio), CInt(My.Resources.AnnunciatorRed.Height * ImageRatio))

        gr_dest = Graphics.FromImage(Image2)

        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        gr_dest.DrawImage(My.Resources.AnnunciatorRed, 0, 0)
        gr_dest.DrawString(m_Text, New Font("Arial", 30, FontStyle.Bold, GraphicsUnit.Point), b, TextRect, sf)


        '* Perform some cleanup
        gr_dest.Dispose()

        StaticImage = Image1

        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)

        Me.Invalidate()
    End Sub
End Class
