Imports System.Drawing
Imports System.Windows.Forms

Public Class Pipe
    Inherits System.Windows.Forms.UserControl

    Private StaticImage As Bitmap
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
                    RotateTimer.Enabled = True
                Else
                    RotateTimer.Enabled = False
                End If
                Me.Invalidate()

                Me.Invalidate(New Rectangle(Me.Width * 0.8, Me.Height * 0.3, Me.Width * 0.2, Me.Height * 0.2))
            End If
        End Set
    End Property

    Public Enum FittingType
        Horizontal
        Vertical
        Elbow1
        Elbow2
        Elbow3
        Elbow4
        Tee1
        Tee2
        Tee3
        Tee4
    End Enum

    Private m_Fitting As FittingType
    Public Property Fitting() As FittingType
        Get
            Return m_Fitting
        End Get
        Set(ByVal value As FittingType)
            m_Fitting = value
            RefreshImage()
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
        m.Dispose()
        _backBuffer.Dispose()
        StaticImage.Dispose()

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


        'If m_Value Then g.FillRectangle(Brushes.SeaGreen, CInt(StaticImage.Width * 0.304), CInt(StaticImage.Height * 0.79), CInt(StaticImage.Width * 0.115), CInt(StaticImage.Height * 0.11))

        'Copy the back buffer to the screen
        e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0)

        '_backBuffer.Dispose()
        g.Dispose()
    End Sub



    Private WithEvents RotateTimer As New Timer

    '****************************
    '* Event - Mouse Down
    '****************************
    Private Sub m_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    End Sub


    '****************************
    '* Event - Mouse Up
    '****************************
    Private Sub MomentaryButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
    End Sub

    Dim CurrentShaftPos As Integer
    Private Sub Animate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RotateTimer.Tick
        CurrentShaftPos += 1
        If CurrentShaftPos > 3 Then CurrentShaftPos = 0
        Me.Invalidate()
    End Sub

    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()




        RotateTimer.Interval = 50
    End Sub


    Private WithEvents SetupDelay As New Timer
    Private Sub m_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub




    Private Sub PolledDataReturned(ByVal Values() As String)
        Value = Values(0)
    End Sub





#End Region


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
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)


        Dim ImageTemp As Image
        ImageRatio = 0
        Select Case m_Fitting
            Case FittingType.Horizontal
                ImageTemp = My.Resources.HorizontalPipe
                ImageRatio = CSng(Me.Height) / CSng(ImageTemp.Height)
            Case FittingType.Vertical
                ImageTemp = My.Resources.VerticalPipe
                ImageRatio = CSng(Me.Width) / CSng(ImageTemp.Width)
            Case FittingType.Elbow1
                ImageTemp = My.Resources.Elbow1
            Case FittingType.Elbow2
                ImageTemp = My.Resources.Elbow2
            Case FittingType.Elbow3
                ImageTemp = My.Resources.Elbow3
            Case FittingType.Elbow4
                ImageTemp = My.Resources.Elbow4
            Case FittingType.Tee1
                ImageTemp = My.Resources.Tee1
            Case FittingType.Tee2
                ImageTemp = My.Resources.Tee2
            Case FittingType.Tee3
                ImageTemp = My.Resources.Tee3
            Case FittingType.Tee4
                ImageTemp = My.Resources.Tee4
            Case Else
                ImageTemp = My.Resources.HorizontalPipe
        End Select


        If ImageRatio = 0 Then
            Dim WidthRatio As Single = CSng(Me.Width) / CSng(ImageTemp.Width)
            Dim HeightRatio As Single = CSng(Me.Height) / CSng(ImageTemp.Height)

            If WidthRatio < HeightRatio Then
                ImageRatio = WidthRatio
            Else
                ImageRatio = HeightRatio
            End If
        End If



        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If StaticImage IsNot Nothing Then StaticImage.Dispose()

        m.Reset()

        Select Case m_Fitting
            Case FittingType.Horizontal
                m.Scale((Me.Width / ImageTemp.Width) * 2, ImageRatio * 1.25)
                StaticImage = New Bitmap(CInt(Me.Width), CInt(Me.Height))
                'm.Scale(2, ImageRatio * 1.25)
            Case FittingType.Vertical
                m.Scale(ImageRatio * 1.25, (Me.Height / ImageTemp.Height) * 2)
                StaticImage = New Bitmap(CInt(Me.Width), CInt(Me.Height))
            Case Else
                m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
                StaticImage = New Bitmap(CInt(Me.Width), CInt(Me.Height))
        End Select

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(StaticImage)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(ImageTemp, 0, 0)

        TextRect.X = 1
        TextRect.Width = 150
        TextRect.Y = 1
        TextRect.Height = StaticImage.Height
        TextRect.Height = ImageTemp.Height / 1.25 - 30
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center


        'gr_dest.DrawRectangle(Pens.Black, TextRect)
        Dim b As New SolidBrush(Color.FromArgb(250, 60, 70, 60))
        'gr_dest.DrawString(m_LegendText, New Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Point), b, TextRect, sf)

        gr_dest.Dispose()


        Me.Invalidate()
    End Sub
End Class
