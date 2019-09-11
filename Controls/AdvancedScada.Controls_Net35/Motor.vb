Imports System.Drawing
Imports System.Windows.Forms

Public Class Motor
    Inherits System.Windows.Forms.UserControl

    Private StaticImage As Bitmap
    Private ShaftImage, ShaftPosImages(5) As Bitmap
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
        If StaticImage Is Nothing Or ShaftImage Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)

        g.DrawImage(StaticImage, 0, 0)

        g.DrawImage(ShaftImage, CInt(StaticImage.Width * 0.832), CInt(StaticImage.Height * 0.47 - ShaftImage.Height / 2))

        If m_Value Then g.FillRectangle(Brushes.SeaGreen, CInt(StaticImage.Width * 0.304), CInt(StaticImage.Height * 0.79), CInt(StaticImage.Width * 0.115), CInt(StaticImage.Height * 0.11))

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
        ShaftImage = ShaftPosImages(CurrentShaftPos)
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
    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private NotificationID As Integer


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
        Dim WidthRatio As Single = CSng(Me.Width) / CSng(My.Resources.MotorNoShaft.Width * 1.2)
        Dim HeightRatio As Single = CSng(Me.Height) / CSng(My.Resources.MotorNoShaft.Height)

        If WidthRatio < HeightRatio Then
            ImageRatio = WidthRatio
        Else
            ImageRatio = HeightRatio
        End If


        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        StaticImage = New Bitmap(CInt(My.Resources.MotorNoShaft.Width * ImageRatio * 1.2), CInt(My.Resources.MotorNoShaft.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(StaticImage)
        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.MotorNoShaft, 0, 0)

        TextRect.X = 1
        TextRect.Width = 150
        TextRect.Y = 1
        TextRect.Height = StaticImage.Height
        TextRect.Height = My.Resources.MotorNoShaft.Height / 1.25 - 30
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center


        'gr_dest.DrawRectangle(Pens.Black, TextRect)
        Dim b As New SolidBrush(Color.FromArgb(250, 60, 70, 60))
        gr_dest.DrawString(m_LegendText, New Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Point), b, TextRect, sf)


        'ShaftPosImages(0) = New Bitmap(CInt(My.Resources.ShaftPos1.Width * ImageRatio), CInt(My.Resources.ShaftPos1.Height * ImageRatio))
        ShaftPosImages(1) = New Bitmap(CInt(My.Resources.ShaftPos2.Width * ImageRatio), CInt(My.Resources.ShaftPos2.Height * ImageRatio))
        ShaftPosImages(2) = New Bitmap(CInt(My.Resources.ShaftPos3.Width * ImageRatio), CInt(My.Resources.ShaftPos3.Height * ImageRatio))
        ShaftPosImages(3) = New Bitmap(CInt(My.Resources.ShaftPos3.Width * ImageRatio), CInt(My.Resources.ShaftPos3.Height * ImageRatio))
        ShaftPosImages(4) = New Bitmap(CInt(My.Resources.ShaftPos3.Width * ImageRatio), CInt(My.Resources.ShaftPos3.Height * ImageRatio))

        gr_dest = Graphics.FromImage(ShaftPosImages(4))
        Dim gr_dest2 As Graphics = Graphics.FromImage(ShaftPosImages(1))
        Dim gr_dest3 As Graphics = Graphics.FromImage(ShaftPosImages(2))
        Dim gr_dest4 As Graphics = Graphics.FromImage(ShaftPosImages(3))
        Dim gr_dest5 As Graphics = Graphics.FromImage(ShaftPosImages(4))

        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m
        gr_dest2.Transform = m
        gr_dest3.Transform = m
        gr_dest4.Transform = m
        gr_dest5.Transform = m

        gr_dest.DrawImage(My.Resources.ShaftPos3, 0, 0)
        gr_dest2.DrawImage(My.Resources.ShaftPos1_5, 0, 0)
        gr_dest3.DrawImage(My.Resources.ShaftPos2, 0, 0)
        gr_dest4.DrawImage(My.Resources.ShaftPos2_5, 0, 0)
        gr_dest5.DrawImage(My.Resources.ShaftPos3, 0, 0)

        ShaftImage = ShaftPosImages(3)

        '* Perform some cleanup
        gr_dest.Dispose()
        gr_dest2.Dispose()
        gr_dest3.Dispose()
        gr_dest4.Dispose()
        gr_dest5.Dispose()


        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)

        Me.Invalidate()
    End Sub
End Class
