Imports System.Drawing

Public Class StackLight
    Inherits System.Windows.Forms.UserControl

    Private StaticImage, StaticImage2 As Bitmap
    Private LightImages(5) As Bitmap
    Private Light1Image, Light2Image, Light3Image As Bitmap
    Private TextRect As New Rectangle
    Private ImageRatio As Single
    Private m As New System.Drawing.Drawing2D.Matrix

#Region "Properties"
    Private m_ValueGreen As Single
    Public Property ValueGreen() As Boolean
        Get
            Return m_ValueGreen
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueGreen Then
                m_ValueGreen = value

                If m_ValueGreen Then
                    Light1Image = LightImages(1)
                Else
                    Light1Image = LightImages(0)
                End If
                Me.Invalidate()

                'Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
                'Me.Invalidate()
            End If
        End Set
    End Property

    Private m_EnableGreen As Boolean = True
    Public Property EnableGreen() As Boolean
        Get
            Return m_EnableGreen
        End Get
        Set(ByVal value As Boolean)
            m_EnableGreen = value
            SetLightCount()
        End Set
    End Property

    Private m_ValueAmber As Single
    Public Property ValueAmber() As Boolean
        Get
            Return m_ValueAmber
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueAmber Then
                m_ValueAmber = value

                If m_ValueAmber Then
                    Light2Image = LightImages(3)
                Else
                    Light2Image = LightImages(2)
                End If
                Me.Invalidate()

                'Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
            End If
        End Set
    End Property

    Private m_EnableAmber As Boolean = True
    Public Property EnableAmber() As Boolean
        Get
            Return m_EnableAmber
        End Get
        Set(ByVal value As Boolean)
            m_EnableAmber = value
            SetLightCount()
        End Set
    End Property

    Private m_ValueRed As Single
    Public Property ValueRed() As Boolean
        Get
            Return m_ValueRed
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueRed Then
                m_ValueRed = value

                If m_ValueRed Then
                    Light3Image = LightImages(5)
                Else
                    Light3Image = LightImages(4)
                End If
                Me.Invalidate()

                'Me.Invalidate(New Rectangle(Me.Width * 0.14, Me.Height * 0.14, Me.Width * 0.72, Me.Height * 0.63))
            End If
        End Set
    End Property

    Private m_EnableRed As Boolean = True
    Public Property EnableRed() As Boolean
        Get
            Return m_EnableRed
        End Get
        Set(ByVal value As Boolean)
            m_EnableRed = value
            SetLightCount()
        End Set
    End Property

    Private LightCount As Integer = 3
    Private Sub SetLightCount()
        LightCount = 0
        If m_EnableAmber Then LightCount += 1
        If m_EnableRed Then LightCount += 1
        If m_EnableGreen Then LightCount += 1

        LegendPlateRatio = (My.Resources.StackLightCap.Height + (My.Resources.StackLightGreenOn.Height * LightCount)) / My.Resources.StackLightCap.Width

        RefreshImage()
        Me.Invalidate()
    End Sub

    '*****************************************
    '* Property - Color and type of button
    '*****************************************
    Public Enum ButtonColors
        Red
        Green
    End Enum
    'Private m_ButtonColor As ButtonColors = ButtonColors.Green
    'Public Property ButtonColor() As ButtonColors
    '    Get
    '        Return (m_ButtonColor)
    '    End Get
    '    Set(ByVal value As ButtonColors)
    '        m_ButtonColor = value
    '        SetButtonUpImage()
    '        RefreshImage()
    '        Me.Invalidate()
    '    End Set
    'End Property

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
        If StaticImage Is Nothing Or StaticImage2 Is Nothing Or _backBuffer Is Nothing Then Exit Sub

        Dim g As Graphics = Graphics.FromImage(_backBuffer)

        Dim Position As Integer

        Try
            If m_EnableRed Then
                g.DrawImage(Light3Image, CInt(StaticImage2.Width / 2 - Light1Image.Width / 2), CInt(StaticImage2.Height / 1.25))
                Position += Light3Image.Height
            End If

            If m_EnableAmber Then
                g.DrawImage(Light2Image, CInt(StaticImage2.Width / 2 - Light2Image.Width / 2), CInt((StaticImage2.Height + Position) / 1.25))
                Position += Light3Image.Height
            End If

            If m_EnableGreen Then
                g.DrawImage(Light1Image, CInt(StaticImage2.Width / 2 - Light3Image.Width / 2), CInt((StaticImage2.Height + Position) / 1.25))
                Position += Light3Image.Height
            End If

            'Draw the base
            g.DrawImage(StaticImage, CInt(StaticImage2.Width / 2 - StaticImage.Width / 2), CInt((StaticImage2.Height + Position) / 1.25))

            '* Draw the cap
            g.DrawImage(StaticImage2, 0, 0)
        Catch ex As Exception
            Dim x = 0
        End Try


        'Copy the back buffer to the screen
        e.Graphics.DrawImage(_backBuffer, 0, 0)
        g.Dispose()
    End Sub




    '****************************
    '* Event - Mouse Down
    '****************************
    Private Sub MomentaryButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown


    End Sub


    Private Sub SetButtonUpImage()
    End Sub
    '****************************
    '* Event - Mouse Up
    '****************************
    Private Sub MomentaryButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp


    End Sub




    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private NotificationIDGreen, NotificationIDAmber, NotificationIDRed As Integer


    Private Sub GreenPolledDataReturned(ByVal Values() As String)
        Try
            ValueGreen = Values(0)
        Catch
            If Values(0).Length < 10 Then
                LegendText = "INVALID VALUE for Green!"
            Else
                LegendText = Values(0)
            End If
        End Try
    End Sub

    Private Sub AmberPolledDataReturned(ByVal Values() As String)
        Try
            ValueAmber = Values(0)
        Catch
            If Values(0).Length < 10 Then
                LegendText = "INVALID VALUE for Amber!"
            Else
                LegendText = Values(0)
            End If
        End Try
    End Sub

    Private Sub RedPolledDataReturned(ByVal Values() As String)
        Try
            ValueRed = Values(0)
        Catch
            If Values(0).Length < 10 Then
                LegendText = "INVALID VALUE for Red!"
            Else
                LegendText = Values(0)
            End If
        End Try
    End Sub




#End Region


    'Private NamePlateRatio As Single = My.Resources.NamePlate.Height / My.Resources.NamePlate.Width
    Private LegendPlateRatio As Single = My.Resources.StackLightCap.Height + (My.Resources.StackLightGreenOn.Height * LightCount) / My.Resources.StackLightCap.Width
    Private LastWidth, LastHeight As Integer
    Private Sub MomentaryButton_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'If LastHeight < Me.Height Or LastWidth < Me.Width Then
        '    If Me.Height / Me.Width > LegendPlateRatio Then
        '        Me.Width = Me.Height / LegendPlateRatio
        '    Else
        '        Me.Height = Me.Width * LegendPlateRatio
        '    End If
        'Else
        '    If Me.Height / Me.Width > LegendPlateRatio Then
        '        Me.Height = Me.Width * LegendPlateRatio
        '    Else
        '        Me.Width = Me.Height / LegendPlateRatio
        '    End If
        'End If

        'LastWidth = Me.Width
        'LastHeight = Me.Height

        RefreshImage()
    End Sub

    Private Sub RefreshImage()
        Try
            Dim WidthRatio As Single = CSng(Me.Width) / CSng(My.Resources.StackLightBase.Width)
            Dim HeightRatio As Single = CSng(Me.Height) / CSng(My.Resources.StackLightBase.Height * 5)

            If WidthRatio < HeightRatio Then
                ImageRatio = WidthRatio
            Else
                ImageRatio = HeightRatio
            End If
        Catch ex As Exception
            Dim x = 0
        End Try

        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        Dim gr_dest As Graphics
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        StaticImage = New Bitmap(CInt(My.Resources.StackLightBase.Width * ImageRatio), CInt(My.Resources.StackLightBase.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        gr_dest = Graphics.FromImage(StaticImage)
        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.StackLightBase, 0, 0)


        TextRect.X = 5
        TextRect.Width = (StaticImage.Width * 0.62) / ImageRatio
        TextRect.Y = 70
        TextRect.Height = StaticImage.Height * 0.8 / ImageRatio * 0.65
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'gr_dest.DrawRectangle(Pens.Wheat, TextRect)
        Dim b As New SolidBrush(Color.FromArgb(250, 150, 160, 180))
        gr_dest.DrawString(m_LegendText, New Font("Arial", 42, FontStyle.Regular, GraphicsUnit.Point), b, TextRect, sf)

        gr_dest.Dispose()


        '* Draw the cap to scaled size
        If StaticImage2 IsNot Nothing Then StaticImage2.Dispose()
        StaticImage2 = New Bitmap(CInt(My.Resources.StackLightCap.Width * ImageRatio), CInt(My.Resources.StackLightCap.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        gr_dest = Graphics.FromImage(StaticImage2)
        m.Reset()
        m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
        gr_dest.Transform = m

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(My.Resources.StackLightCap, 0, 0)
        gr_dest.Dispose()


        '***************************
        '* Green Light
        '***************************
        Dim gr_dest2 As Graphics
        Try
            LightImages(0) = New Bitmap(CInt(My.Resources.StackLightGreenOff.Width * ImageRatio), CInt(My.Resources.StackLightGreenOff.Height * ImageRatio))
            LightImages(1) = New Bitmap(CInt(My.Resources.StackLightGreenOn.Width * ImageRatio), CInt(My.Resources.StackLightGreenOn.Height * ImageRatio))


            gr_dest = Graphics.FromImage(LightImages(0))
            gr_dest2 = Graphics.FromImage(LightImages(1))

            m.Reset()
            m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
            gr_dest.Transform = m
            gr_dest2.Transform = m

            gr_dest.DrawImage(My.Resources.StackLightGreenOff, 0, 0)
            gr_dest2.DrawImage(My.Resources.StackLightGreenOn, 0, 0)
            gr_dest.Dispose()
            gr_dest2.Dispose()
        Catch ex As Exception
            Dim x = 0
        End Try

        '***************************
        '* Amber Light
        '***************************
        Try
            LightImages(2) = New Bitmap(CInt(My.Resources.StackLightAmberOff.Width * ImageRatio), CInt(My.Resources.StackLightAmberOff.Height * ImageRatio))
            LightImages(3) = New Bitmap(CInt(My.Resources.StackLightAmberOn.Width * ImageRatio), CInt(My.Resources.StackLightAmberOn.Height * ImageRatio))

            gr_dest = Graphics.FromImage(LightImages(2))
            gr_dest2 = Graphics.FromImage(LightImages(3))

            m.Reset()
            m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
            gr_dest.Transform = m
            gr_dest2.Transform = m

            gr_dest.DrawImage(My.Resources.StackLightAmberOff, 0, 0)
            gr_dest2.DrawImage(My.Resources.StackLightAmberOn, 0, 0)
            gr_dest.Dispose()
            gr_dest2.Dispose()

        Catch ex As Exception
            Dim x = 0
        End Try
        '***************************
        '* Red Light
        '***************************
        Try
            LightImages(4) = New Bitmap(CInt(My.Resources.StackLightRedOff.Width * ImageRatio), CInt(My.Resources.StackLightRedOff.Height * ImageRatio))
            LightImages(5) = New Bitmap(CInt(My.Resources.StackLightRedON.Width * ImageRatio), CInt(My.Resources.StackLightRedON.Height * ImageRatio))

            gr_dest = Graphics.FromImage(LightImages(4))
            gr_dest2 = Graphics.FromImage(LightImages(5))

            m.Reset()
            m.Scale(ImageRatio * 1.25, ImageRatio * 1.25)
            gr_dest.Transform = m
            gr_dest2.Transform = m

            gr_dest.DrawImage(My.Resources.StackLightRedOff, 0, 0)
            gr_dest2.DrawImage(My.Resources.StackLightRedON, 0, 0)
            gr_dest.Dispose()
            gr_dest2.Dispose()
        Catch ex As Exception
            Dim x = 0
        End Try


        '* Set default t all Off
        Light1Image = LightImages(0)
        Light2Image = LightImages(2)
        Light3Image = LightImages(4)


        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)
    End Sub
End Class
