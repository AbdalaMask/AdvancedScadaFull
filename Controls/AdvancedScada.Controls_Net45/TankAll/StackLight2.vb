Imports System.Drawing

Public Class StackLight2
    Inherits System.Windows.Forms.Control

    Public Event LightGreenValueChanged As EventHandler
    Public Event LightAmberValueChanged As EventHandler
    Public Event LightRedValueChanged As EventHandler

    '* Images for the Base and Cap which never change
    Private StaticImage, StaticImage2 As Bitmap

    '* Images for each light represented as On and Off
    Private LightImages(7) As Bitmap
    '* Pointers to current active images
    Private ActiveGreenImage, ActiveAmberImage, ActiveBlueImage, ActiveRedImage As Bitmap

    '* Used for scaling the source images to the current control size
    Private ImageRatio As Single

    Private TextRectangle As Rectangle
    Private sf As StringFormat
    Private TextBrush As SolidBrush

#Region "Constructor"
    Public Sub New()
        MyBase.New()

        '* reduce the flicker
        Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)


        ForeColor = System.Drawing.Color.White
        TextBrush = New SolidBrush(MyBase.ForeColor)

        sf = New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        RefreshImage()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                TextBrush.Dispose()
                sf.Dispose()
                If StaticImage2 IsNot Nothing Then StaticImage2.Dispose()
                If StaticImage IsNot Nothing Then StaticImage.Dispose()
                For index = 0 To LightImages.Length - 1
                    If LightImages(index) IsNot Nothing Then LightImages(index).Dispose()
                Next
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region

#Region "Properties"
    Private m_ValueGreen As Boolean
    Public Property LightGreenValue() As Boolean
        Get
            Return m_ValueGreen
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueGreen Then
                m_ValueGreen = value

                If m_ValueGreen Then
                    ActiveGreenImage = LightImages(1)
                Else
                    ActiveGreenImage = LightImages(0)
                End If
                Me.Invalidate()

                OnLightGreenValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Private m_EnableGreen As Boolean = True
    Public Property LightGreenEnable() As Boolean
        Get
            Return m_EnableGreen
        End Get
        Set(ByVal value As Boolean)
            If value <> m_EnableGreen Then
                m_EnableGreen = value
                SetLightCount()
            End If
        End Set
    End Property

    Private m_ValueAmber As Boolean
    Public Property LightAmberValue() As Boolean
        Get
            Return m_ValueAmber
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueAmber Then
                m_ValueAmber = value

                If m_ValueAmber Then
                    ActiveAmberImage = LightImages(3)
                Else
                    ActiveAmberImage = LightImages(2)
                End If
                Me.Invalidate()

                OnLightAmberValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Private m_EnableAmber As Boolean = True
    Public Property LightAmberEnable() As Boolean
        Get
            Return m_EnableAmber
        End Get
        Set(ByVal value As Boolean)
            If value <> m_EnableAmber Then
                m_EnableAmber = value
                SetLightCount()
            End If
        End Set
    End Property

    Private m_ValueBlue As Boolean
    Public Property LightBlueValue() As Boolean
        Get
            Return m_ValueBlue
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueBlue Then
                m_ValueBlue = value

                If m_ValueBlue Then
                    ActiveBlueImage = LightImages(5)
                Else
                    ActiveBlueImage = LightImages(4)
                End If
                Me.Invalidate()

                OnLightBlueValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Private m_EnableBlue As Boolean = True
    Public Property LightBlueEnable() As Boolean
        Get
            Return m_EnableBlue
        End Get
        Set(ByVal value As Boolean)
            If value <> m_EnableBlue Then
                m_EnableBlue = value
                SetLightCount()
            End If
        End Set
    End Property

    Private m_ValueRed As Boolean
    Public Property LightRedValue() As Boolean
        Get
            Return m_ValueRed
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueRed Then
                m_ValueRed = value

                If m_ValueRed Then
                    ActiveRedImage = LightImages(7)
                Else
                    ActiveRedImage = LightImages(6)
                End If
                Me.Invalidate()

                OnLightRedValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Private m_EnableRed As Boolean = True
    Public Property LightRedEnable() As Boolean
        Get
            Return m_EnableRed
        End Get
        Set(ByVal value As Boolean)
            If value <> m_EnableRed Then
                m_EnableRed = value
                SetLightCount()
            End If
        End Set
    End Property



    <System.ComponentModel.Browsable(True)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Me.Invalidate()
        End Set
    End Property

    '* These next properties are overriden so that we can refresh the image when changed
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)>
    Public Overrides Property ForeColor() As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If TextBrush Is Nothing Then
                TextBrush = New SolidBrush(MyBase.ForeColor)
            Else
                TextBrush.Color = value
            End If

            MyBase.ForeColor = value
            Me.Invalidate()
        End Set
    End Property
#End Region

#Region "Events"
    '*************************************************************************
    '* 
    '**************************************************************************
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If StaticImage Is Nothing Or StaticImage2 Is Nothing Then Exit Sub

        Dim g As Graphics = e.Graphics

        Dim Position As Integer

        Try
            If m_EnableRed Then
                g.DrawImage(ActiveRedImage, 0, Convert.ToInt32(StaticImage2.Height + Position))
                Position += ActiveRedImage.Height
            End If

            If m_EnableBlue Then
                g.DrawImage(ActiveBlueImage, 0, Convert.ToInt32(StaticImage2.Height + Position))
                Position += ActiveBlueImage.Height
            End If


            If m_EnableAmber Then
                g.DrawImage(ActiveAmberImage, 0, Convert.ToInt32((StaticImage2.Height + Position)))
                Position += ActiveAmberImage.Height
            End If

            If m_EnableGreen Then
                g.DrawImage(ActiveGreenImage, 0, Convert.ToInt32((StaticImage2.Height + Position)))
                Position += ActiveGreenImage.Height
            End If

            'Draw the base
            g.DrawImage(StaticImage, 0, Convert.ToInt32((StaticImage2.Height + Position)))

            '* Draw the cap
            g.DrawImage(StaticImage2, 0, 0)

            If MyBase.Text IsNot Nothing AndAlso (String.Compare(MyBase.Text, String.Empty) <> 0) Then
                If TextBrush.Color <> MyBase.ForeColor Then
                    TextBrush.Color = MyBase.ForeColor
                End If

                g.DrawString(MyBase.Text, MyBase.Font, TextBrush, TextRectangle, sf)
                '* Debugging - draw a rectangle to see exactly where the text is to be
                'g.DrawRectangle(Pens.Red, TextRectangle)
            End If
        Catch
        End Try
    End Sub


    Protected Overridable Sub OnLightGreenValueChanged(ByVal e As EventArgs)
        RaiseEvent LightGreenValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightAmberValueChanged(ByVal e As EventArgs)
        RaiseEvent LightAmberValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightBlueValueChanged(ByVal e As EventArgs)
        RaiseEvent LightAmberValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightRedValueChanged(ByVal e As EventArgs)
        RaiseEvent LightRedValueChanged(Me, e)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        RefreshImage()
    End Sub
#End Region

#Region "Private Methods"
    Private LightCount As Integer = 4
    Private Sub SetLightCount()
        LightCount = 0
        If m_EnableAmber Then LightCount += 1
        If m_EnableRed Then LightCount += 1
        If m_EnableGreen Then LightCount += 1
        If m_EnableBlue Then LightCount += 1


        RefreshImage()
        Me.Invalidate()
    End Sub

    Private Sub RefreshImage()
        '********************************************************
        '* Calculate the scaling for the images
        '********************************************************
        Dim TotalHeight As Single
        Try
            Dim WidthRatio As Single = CSng(Me.Width) / CSng(My.Resources.StackLightBase.Width)
            TotalHeight = My.Resources.StackLightBase.Height + My.Resources.StackLightCap.Height
            TotalHeight += My.Resources.StackLightElementGray.Height * LightCount
            Dim HeightRatio As Single = CSng(Height) / CSng(TotalHeight)

            If WidthRatio < HeightRatio Then
                ImageRatio = WidthRatio
            Else
                ImageRatio = HeightRatio
            End If
        Catch
        End Try

        '* Prevent exceptions
        If ImageRatio <= 0 Then Exit Sub

        '****************************************************************
        ' Scale the image so it will draw faster in Paint event
        '****************************************************************
        ' Make a bitmap for the result.
        ' The Base image will be stored in StaticImage
        If StaticImage IsNot Nothing Then StaticImage.Dispose()
        StaticImage = New Bitmap(Convert.ToInt32(My.Resources.StackLightBase.Width * ImageRatio), Convert.ToInt32(My.Resources.StackLightBase.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Using gr_dest As Graphics = Graphics.FromImage(StaticImage)
            ' Copy the source image into the destination bitmap.
            gr_dest.DrawImage(My.Resources.StackLightBase, 0, 0, StaticImage.Width, Convert.ToInt32(StaticImage.Width / My.Resources.StackLightBase.Width * My.Resources.StackLightBase.Height))


            TextRectangle.X = 1
            TextRectangle.Width = StaticImage.Width - 2
            TextRectangle.Y = Convert.ToInt32(TotalHeight * 0.82 * ImageRatio)
            TextRectangle.Height = Convert.ToInt32(TotalHeight * 0.16 * ImageRatio)
        End Using


        '* Draw the cap to scaled size
        If StaticImage2 IsNot Nothing Then StaticImage2.Dispose()
        StaticImage2 = New Bitmap(Convert.ToInt32(My.Resources.StackLightCap.Width * ImageRatio), Convert.ToInt32(My.Resources.StackLightCap.Height * ImageRatio))

        ' Make a Graphics object for the result Bitmap.
        Using gr_dest As Graphics = Graphics.FromImage(StaticImage2)
            ' Copy the source image into the destination bitmap.
            gr_dest.DrawImage(My.Resources.StackLightCap, 0, 0, StaticImage.Width,
                              Convert.ToInt32(StaticImage.Width / My.Resources.StackLightBase.Width * My.Resources.StackLightCap.Height))
        End Using

        Dim targetRectangle As Rectangle

        '* Create the bitmaps to hold all On and Off images
        For index = 0 To LightImages.Length - 1
            LightImages(index) = New Bitmap(StaticImage.Width, Convert.ToInt32(My.Resources.StackLightElementGray.Height * ImageRatio))
        Next

        '* Create the rectangle for scaling
        targetRectangle = New Rectangle(0, 0, LightImages(0).Width, LightImages(0).Height)

        '* attributes for changing color
        Using ColorChangeAttribute As System.Drawing.Imaging.ImageAttributes = New System.Drawing.Imaging.ImageAttributes()

            '***************************
            '* Green Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row reduces red
                '* Second row increases green
                '* Third row reduces blue
                Dim OffMatrix As Single()() = {New Single() {0.25, 0, 0, 0, 0},
                                               New Single() {0, 0.5, 0, 0, 0},
                                               New Single() {0, 0, 0.25, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using

                '******************************
                '* ON COLOR
                '* First row reduces red
                '* Second row increases green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {0.25, 0, 0, 0, 0},
                                              New Single() {0, 2.5, 0, 0, 0},
                                              New Single() {0, 0, 0.25, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using
            Catch
            End Try

            '***************************
            '* Amber Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row increases red
                '* Second row increases green
                '* Third row reduces blue
                Dim OffMatrix As Single()() = {New Single() {0.5, 0, 0, 0, 0},
                                               New Single() {0, 0.5, 0, 0, 0},
                                               New Single() {0, 0, 0.25, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using

                '******************************
                '* ON COLOR
                '* First row increases red
                '* Second row increases green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {2.5, 0, 0, 0, 0},
                                              New Single() {0, 2.5, 0, 0, 0},
                                              New Single() {0, 0, 0.25, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using
            Catch
            End Try

            '***************************
            '* Blue Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row reduces red
                '* Second row reduces green
                '* Third row increases blue
                Dim OffMatrix As Single()() = {New Single() {0.25, 0, 0, 0, 0},
                                               New Single() {0, 0.25, 0, 0, 0},
                                               New Single() {0, 0, 0.5, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using

                '******************************
                '* ON COLOR
                '* First row reduces red
                '* Second row reduces green
                '* Third row increases blue
                Dim OnMatrix As Single()() = {New Single() {0.25, 0, 0, 0, 0},
                                              New Single() {0, 0.25, 0, 0, 0},
                                              New Single() {0, 0, 2.5, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using
            Catch
            End Try

            '***************************
            '* Red Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row reduces red
                '* Second row increases green
                '* Third row reduces blue
                Dim OffMatrix As Single()() = {New Single() {0.5, 0, 0, 0, 0},
                                               New Single() {0, 0.25, 0, 0, 0},
                                               New Single() {0, 0, 0.25, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using

                '******************************
                '* ON COLOR
                '* First row increases red
                '* Second row reduces green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {2.5, 0, 0, 0, 0},
                                              New Single() {0, 0.25, 0, 0, 0},
                                              New Single() {0, 0, 0.25, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                    gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                      My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                      GraphicsUnit.Pixel, ColorChangeAttribute)
                End Using
            Catch
            End Try


        End Using

        SetActiveImages()
    End Sub

    '********************************************************
    '* Point the image variables to the correct light states
    '********************************************************
    Private Sub SetActiveImages()
        '* Set active image
        If m_ValueGreen Then
            ActiveGreenImage = LightImages(1)
        Else
            ActiveGreenImage = LightImages(0)
        End If

        If m_ValueAmber Then
            ActiveAmberImage = LightImages(3)
        Else
            ActiveAmberImage = LightImages(2)
        End If

        If m_ValueBlue Then
            ActiveBlueImage = LightImages(5)
        Else
            ActiveBlueImage = LightImages(4)
        End If

        If m_ValueRed Then
            ActiveRedImage = LightImages(7)
        Else
            ActiveRedImage = LightImages(6)
        End If
    End Sub
#End Region
End Class
