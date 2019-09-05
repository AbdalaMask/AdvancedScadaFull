Imports System.Drawing

Public Class StackLight5Lights_V4
    Inherits System.Windows.Forms.Control

    Public Event LightGreenValueChanged As EventHandler
    Public Event LightAmberValueChanged As EventHandler
    Public Event LightRedValueChanged As EventHandler

    '* Images for the Base and Cap which never change
    Private StaticImage, StaticImage2 As Bitmap

    '* Images for each light represented as On and Off
    Private LightImages(9) As Bitmap
    '* Pointers to current active images
    Private ActivePos_1Image, ActivePos_2Image, ActivePos_3Image, ActivePos_4Image, ActivePos_5Image As Bitmap

    '* Used for scaling the source images to the current control size
    Private ImageRatio As Single

    Private TextRectangle As Rectangle
    Private sf As StringFormat
    Private TextBrush As SolidBrush

    Private position_1 As Colors = Colors.GREEN
    Private position_2 As Colors = Colors.AMBER
    Private position_3 As Colors = Colors.RED
    Private position_4 As Colors = Colors.BLUE
    Private position_5 As Colors = Colors.WHITE




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
    Private m_ValuePos_1 As Boolean
    Public Property LightPos_1Value() As Boolean
        Get
            Return m_ValuePos_1
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValuePos_1 Then
                m_ValuePos_1 = value

                If m_ValuePos_1 Then
                    ActivePos_1Image = LightImages(1)
                Else
                    ActivePos_1Image = LightImages(0)
                End If
                Me.Invalidate()

                OnLightPos_1ValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Dim m_EnablePos_1 As Boolean = True
    Public Property StackPosition1 As Colors
        Get
            Return position_1

        End Get
        Set(ByVal value As Colors)
            If value <> position_1 Then
                position_1 = value

                If value = position_1 Then
                    m_EnablePos_1 = True
                    If value = Colors.NONE Then
                        m_EnablePos_1 = False
                    End If
                End If
            End If
            SetLightCount()
        End Set
    End Property

    Private m_ValuePos_2 As Boolean
    Public Property LightPos_2Value() As Boolean
        Get
            Return m_ValuePos_2
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValuePos_2 Then
                m_ValuePos_2 = value

                If m_ValuePos_2 Then
                    ActivePos_2Image = LightImages(3)
                Else
                    ActivePos_2Image = LightImages(2)
                End If
                Me.Invalidate()

                OnLightPos_2ValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Dim m_EnablePos_2 As Boolean = True
    Public Property StackPosition2 As Colors
        Get
            Return position_2

        End Get
        Set(ByVal value As Colors)
            If value <> position_2 Then
                position_2 = value

                If value = position_2 Then
                    m_EnablePos_2 = True
                    If value = Colors.NONE Then
                        m_EnablePos_2 = False
                    End If
                End If
            End If
            SetLightCount()
        End Set
    End Property

    Private m_ValuePos_3 As Boolean
    Public Property LightPos_3Value() As Boolean
        Get
            Return m_ValuePos_3
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValuePos_3 Then
                m_ValuePos_3 = value

                If m_ValuePos_3 Then
                    ActivePos_3Image = LightImages(5)
                Else
                    ActivePos_3Image = LightImages(4)
                End If
                Me.Invalidate()

                OnLightPos_3ValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Dim m_EnablePos_3 As Boolean = True
    Public Property StackPosition3 As Colors
        Get
            Return position_3

        End Get
        Set(ByVal value As Colors)
            If value <> position_3 Then
                position_3 = value

                If value = position_3 Then
                    m_EnablePos_3 = True
                    If value = Colors.NONE Then
                        m_EnablePos_3 = False
                    End If
                End If
            End If
            SetLightCount()
        End Set
    End Property

    Private m_ValuePos_4 As Boolean
    Public Property LightPos_4Value() As Boolean
        Get
            Return m_ValuePos_4
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValuePos_4 Then
                m_ValuePos_4 = value

                If m_ValuePos_4 Then
                    ActivePos_4Image = LightImages(7)
                Else
                    ActivePos_4Image = LightImages(6)
                End If
                Me.Invalidate()

                OnLightPos_4ValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property

    Dim m_EnablePos_4 As Boolean = True
    Public Property StackPosition4 As Colors
        Get
            Return position_4

        End Get
        Set(ByVal value As Colors)
            If value <> position_4 Then
                position_4 = value

                If value = position_4 Then
                    m_EnablePos_4 = True
                    If value = Colors.NONE Then
                        m_EnablePos_4 = False
                    End If
                End If
            End If
            SetLightCount()
        End Set
    End Property


    Private m_ValuePos_5 As Boolean
    Public Property LightPos_5Value() As Boolean

        Get
            Return m_ValuePos_5
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValuePos_5 Then
                m_ValuePos_5 = value

                If m_ValuePos_5 Then
                    ActivePos_5Image = LightImages(9)
                Else
                    ActivePos_5Image = LightImages(8)
                End If
                Me.Invalidate()

                OnLightPos_5ValueChanged(System.EventArgs.Empty)
            End If
        End Set
    End Property



    Dim m_EnablePos_5 As Boolean = True
    Public Property StackPosition5 As Colors
        Get
            Return position_5

        End Get
        Set(ByVal value As Colors)
            If value <> position_5 Then
                position_5 = value

                If value = position_5 Then
                    m_EnablePos_5 = True
                    If value = Colors.NONE Then
                        m_EnablePos_5 = False
                    End If
                End If
            End If
            SetLightCount()
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

            If m_EnablePos_5 Then
                g.DrawImage(ActivePos_5Image, 0, Convert.ToInt32((StaticImage2.Height + Position)))
                Position += ActivePos_5Image.Height
            End If

            If m_EnablePos_4 Then
                g.DrawImage(ActivePos_4Image, 0, Convert.ToInt32(StaticImage2.Height + Position))
                Position += ActivePos_4Image.Height
            End If

            If m_EnablePos_3 Then
                g.DrawImage(ActivePos_3Image, 0, Convert.ToInt32(StaticImage2.Height + Position))
                Position += ActivePos_3Image.Height
            End If

            If m_EnablePos_2 Then
                g.DrawImage(ActivePos_2Image, 0, Convert.ToInt32((StaticImage2.Height + Position)))
                Position += ActivePos_2Image.Height
            End If

            If m_EnablePos_1 Then
                g.DrawImage(ActivePos_1Image, 0, Convert.ToInt32((StaticImage2.Height + Position)))
                Position += ActivePos_1Image.Height
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

    Protected Overridable Sub OnLightPos_1ValueChanged(ByVal e As EventArgs)
        RaiseEvent LightGreenValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightPos_2ValueChanged(ByVal e As EventArgs)
        RaiseEvent LightAmberValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightPos_3ValueChanged(ByVal e As EventArgs)
        RaiseEvent LightAmberValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightPos_4ValueChanged(ByVal e As EventArgs)
        RaiseEvent LightRedValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnLightPos_5ValueChanged(ByVal e As EventArgs)
        RaiseEvent LightRedValueChanged(Me, e)
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        RefreshImage()
    End Sub
#End Region

#Region "Private Methods"
    Private LightCount As Integer = 5
    Private Sub SetLightCount()
        LightCount = 0
        If m_EnablePos_1 Then LightCount += 1
        If m_EnablePos_2 Then LightCount += 1
        If m_EnablePos_3 Then LightCount += 1
        If m_EnablePos_4 Then LightCount += 1
        If m_EnablePos_5 Then LightCount += 1

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
            '* Green light
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

                If position_1 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
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

                If position_1 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.GREEN Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
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

                If position_1 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                '******************************
                '* ON COLOR
                '* First row increases red
                '* Second row increases green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {2.5, 0, 0, 0, 0},
                                              New Single() {0, 1.5, 0, 0, 0},
                                              New Single() {0, 0, 0.1, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                If position_1 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.AMBER Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
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

                If position_1 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

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

                If position_1 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.BLUE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
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

                If position_1 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

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

                If position_1 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.RED Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
            Catch
            End Try

            '***************************
            '* White Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row reduces red
                '* Second row increases green
                '* Third row reduces blue
                Dim OffMatrix As Single()() = {New Single() {1.5, 0, 0, 0, 0},
                                               New Single() {0, 1.5, 0, 0, 0},
                                               New Single() {0, 0, 1.5, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                If position_1 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                '******************************
                '* ON COLOR
                '* First row increases red
                '* Second row reduces green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {2.5, 0, 0, 0, 0},
                                              New Single() {0, 2.5, 0, 0, 0},
                                              New Single() {0, 0, 2.5, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                If position_1 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.WHITE Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
            Catch
            End Try


            '***************************
            '* Yellow Light
            '***************************
            Try
                '****************************
                '* OFF COLOR
                '* First row reduces red
                '* Second row increases green
                '* Third row reduces blue
                Dim OffMatrix As Single()() = {New Single() {1.1, 0, 0, 0, 0},
                                               New Single() {0, 1.1, 0, 0, 0},
                                               New Single() {0, 0, 0, 0, 0},
                                               New Single() {0, 0, 0, 1, 0},
                                               New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OffMatrix))

                If position_1 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(0))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(2))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(4))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(6))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(8))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                '******************************
                '* ON COLOR
                '* First row increases red
                '* Second row reduces green
                '* Third row reduces blue
                Dim OnMatrix As Single()() = {New Single() {2.5, 0, 0, 0, 0},
                                              New Single() {0, 2.5, 0, 0, 0},
                                              New Single() {0, 0, 0, 0, 0},
                                              New Single() {0, 0, 0, 1, 0},
                                              New Single() {0, 0, 0, 0, 1}}
                ColorChangeAttribute.SetColorMatrix(New System.Drawing.Imaging.ColorMatrix(OnMatrix))

                If position_1 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(1))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_2 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(3))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_3 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(5))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_4 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(7))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If

                If position_5 = Colors.YELLOW Then

                    Using gr_dest As Graphics = Graphics.FromImage(LightImages(9))
                        gr_dest.DrawImage(My.Resources.StackLightElementGray, targetRectangle, 0, 0,
                                          My.Resources.StackLightElementGray.Width, My.Resources.StackLightElementGray.Height,
                                          GraphicsUnit.Pixel, ColorChangeAttribute)
                    End Using
                End If
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
        If m_ValuePos_1 Then
            ActivePos_1Image = LightImages(1)
        Else
            ActivePos_1Image = LightImages(0)
        End If

        If m_ValuePos_2 Then
            ActivePos_2Image = LightImages(3)
        Else
            ActivePos_2Image = LightImages(2)
        End If

        If m_ValuePos_3 Then
            ActivePos_3Image = LightImages(5)
        Else
            ActivePos_3Image = LightImages(4)
        End If

        If m_ValuePos_4 Then
            ActivePos_4Image = LightImages(7)
        Else
            ActivePos_4Image = LightImages(6)
        End If

        If m_ValuePos_5 Then
            ActivePos_5Image = LightImages(9)
        Else
            ActivePos_5Image = LightImages(8)
        End If
    End Sub
#End Region



End Class


Public Enum Colors

    NONE
    RED
    YELLOW
    AMBER
    GREEN
    BLUE
    WHITE


End Enum
