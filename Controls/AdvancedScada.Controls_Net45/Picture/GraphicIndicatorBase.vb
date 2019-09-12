Option Strict On
Imports System.Drawing
'****************************************************************************
'* 12-DEC-08 Added line in OnPaint to exit is LegendText is nothing
'* 05-OCT-09 Exit OnPaint if GrayPen is Nothing
'****************************************************************************
Public Class GraphicIndicatorBase
    Inherits System.Windows.Forms.Control

    Public Event ValueSelect1Changed As EventHandler
    Public Event ValueSelect2Changed As EventHandler

    Private StaticImage As Bitmap
    Private Image1, Image2, Image3 As Bitmap
    Private TextRect As New Rectangle
    Private ImageRatio As Single

#Region "Constructor"
    Public Sub New()
        MyBase.New

        ''* reduce the flicker
        Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        ForeColor = Color.WhiteSmoke
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If sf2 IsNot Nothing Then sf2.Dispose()
        If sf IsNot Nothing Then sf.Dispose()
        If m_Font2 IsNot Nothing Then m_Font2.Dispose()
        If Image1 IsNot Nothing Then Image1.Dispose()
        If Image2 IsNot Nothing Then Image2.Dispose()
        If Image3 IsNot Nothing Then Image3.Dispose()
        TextBrush.Dispose()
    End Sub

#End Region

#Region "Properties"
    Private m_ValueSelect1 As Boolean
    Public Property ValueSelect1() As Boolean
        Get
            Return m_ValueSelect1
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueSelect1 Then
                m_ValueSelect1 = value

                'BackGroundNeedsRefreshed = True
                Me.Invalidate()

                OnValueSelect1Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    Private m_ValueSelect2 As Boolean
    Public Property ValueSelect2() As Boolean
        Get
            Return m_ValueSelect2
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueSelect2 Then
                m_ValueSelect2 = value

                'BackGroundNeedsRefreshed = True
                Me.Invalidate()

                OnValueSelect2Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when All is off
    '*****************************************
    'Private m_LightOnColor As Color = Color.Green
    Private m_GraphicAllOff As Bitmap
    Public Property GraphicAllOff() As Bitmap
        Get
            Return (m_GraphicAllOff)
        End Get
        Set(ByVal value As Bitmap)
            If m_GraphicAllOff IsNot value Then
                m_GraphicAllOff = value
                RefreshImages()
                'Invalidate()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when
    '*****************************************
    'Private m_LightOnColor As Color = Color.Green
    Private m_GraphicSelect1 As Bitmap
    Public Property GraphicSelect1() As Bitmap
        Get
            Return (m_GraphicSelect1)
        End Get
        Set(ByVal value As Bitmap)
            m_GraphicSelect1 = value
            RefreshImages()
            'Me.Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when
    '*****************************************
    'Private m_LightOnColor As Color = Color.Green
    Private m_GraphicSelect2 As Bitmap
    Public Property GraphicSelect2() As Bitmap
        Get
            Return (m_GraphicSelect2)
        End Get
        Set(ByVal value As Bitmap)
            m_GraphicSelect2 = value
            RefreshImages()
            'Me.Invalidate()
        End Set
    End Property

    Private FlashTimer As System.Windows.Forms.Timer
    Private m_Flash1 As Boolean
    Public Property Flash1 As Boolean
        Get
            Return m_Flash1
        End Get
        Set(value As Boolean)
            If m_Flash1 <> value Then
                m_Flash1 = value

                If m_Flash1 Then
                    If FlashTimer Is Nothing Then
                        FlashTimer = New System.Windows.Forms.Timer
                        FlashTimer.Interval = 800
                        AddHandler FlashTimer.Tick, AddressOf FlashTimerTick
                        FlashTimer.Start()
                    End If
                Else
                    If FlashTimer IsNot Nothing Then
                        FlashTimer.Stop()
                    End If
                End If
            End If
        End Set
    End Property

    Private Sub FlashTimerTick(sender As Object, e As EventArgs)
        ValueSelect1 = Not ValueSelect1
    End Sub

    Private m_SizeMode As System.Windows.Forms.PictureBoxSizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Public Property SizeMode As System.Windows.Forms.PictureBoxSizeMode
        Get
            Return m_SizeMode
        End Get
        Set(value As System.Windows.Forms.PictureBoxSizeMode)
            If m_SizeMode <> value Then
                m_SizeMode = value
                RefreshImages()
            End If
        End Set
    End Property

    Public Enum RotationAngleEnum
        NoRotation
        Rotate90
        Rotate180
        Rotate270
    End Enum
    Private m_RotationAngle As RotationAngleEnum = RotationAngleEnum.NoRotation
    Public Property RotationAngle As RotationAngleEnum
        Get
            Return m_RotationAngle
        End Get
        Set(value As RotationAngleEnum)
            If m_RotationAngle <> value Then
                m_RotationAngle = value
                RefreshImages()
            End If
        End Set
    End Property


    '    <System.ComponentModel.Browsable(True)> _
    '<System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)> _
    'Public Overrides Property Text() As String
    '        Get
    '            Return MyBase.Text
    '        End Get
    '        Set(ByVal value As String)
    '            MyBase.Text = value
    '        End Set
    '    End Property

    '    'Private m_font As New Font("Arial", 22, FontStyle.Regular, GraphicsUnit.Point)
    '    '* These next properties are overriden so that we can refresh the image when changed
    '    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)> _
    '    Public Overrides Property Font() As Font
    '        Get
    '            Return MyBase.Font
    '        End Get
    '        Set(ByVal value As Font)
    '            MyBase.Font = value
    '        End Set
    '    End Property


    '*****************************************
    '* Property - Second Text
    '*****************************************
    Private m_Text2 As String = ""
    Public Property Text2() As String
        Get
            Return m_Text2
        End Get
        Set(ByVal value As String)
            If m_Text2 <> value Then
                m_Text2 = value
                If Not String.IsNullOrEmpty(m_Format) And (Not DesignMode) Then
                    Try
                        m_Text2 = (CSng(value) * m_ValueScaleFactor).ToString(m_Format)
                    Catch ex As Exception
                        m_Text2 = "Check NumericFormat and variable type"
                    End Try
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Second Text
    '*****************************************
    Private m_Font2 As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    Public Property Font2() As Font
        Get
            Return m_Font2
        End Get
        Set(ByVal value As Font)
            m_Font2 = value
            'RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Private m_Format As String
    Public Property NumericFormat() As String
        Get
            Return m_Format
        End Get
        Set(ByVal value As String)
            m_Format = value
        End Set
    End Property

    Private m_ValueScaleFactor As Decimal = 1
    Public Property ValueScaleFactor() As Decimal
        Get
            Return m_ValueScaleFactor
        End Get
        Set(ByVal value As Decimal)
            m_ValueScaleFactor = value
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

    ''* This is necessary to make the background draw correctly
    ''*  http://www.bobpowell.net/transcontrols.htm
    ''*part of the transparent background code
    'Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    '    Get
    '        Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
    '        cp.ExStyle = cp.ExStyle Or 32
    '        Return cp
    '        Return MyBase.CreateParams
    '    End Get
    'End Property
#End Region


#Region "Events"
    Protected Overridable Sub OnValueSelect1Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelect1Changed(Me, e)
    End Sub

    Protected Overridable Sub OnValueSelect2Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelect2Changed(Me, e)
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As System.EventArgs)
        MyBase.OnForeColorChanged(e)

        If TextBrush Is Nothing Then
            '* V3.99f - would always revert to black
            TextBrush = New SolidBrush(MyBase.ForeColor)
        Else
            TextBrush.Color = MyBase.ForeColor
        End If

        Me.Invalidate()
    End Sub


    Protected Overrides Sub OnPaintBackground(pevent As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintBackground(pevent)

        '******************************************************
        '* Go through all controls behind this and draw them
        '* to the background to simulate transparency
        '******************************************************
        If BackColor = Color.Transparent Then
            If Parent IsNot Nothing Then
                Dim index As Integer = Parent.Controls.GetChildIndex(Me)

                For i As Integer = Parent.Controls.Count - 1 To index + 1 Step -1
                    Dim c As System.Windows.Forms.Control = Parent.Controls(i)
                    If c.Bounds.IntersectsWith(Bounds) AndAlso c.Visible Then
                        Using bmp As New Bitmap(c.Width, c.Height, pevent.Graphics)
                            c.DrawToBitmap(bmp, c.ClientRectangle)
                            pevent.Graphics.DrawImageUnscaled(bmp, c.Left - Left, c.Top - Top)
                        End Using
                    End If
                Next
            End If
        End If
    End Sub

    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private sf As New StringFormat
    Private sf2 As New StringFormat
    Private Text2Rect As Rectangle
    Protected TextBrush As SolidBrush
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If TextBrush Is Nothing Then Exit Sub

        Dim g As Graphics = e.Graphics
        ''g.RotateTransform(20.0)
        'If m_GraphicAllOff IsNot Nothing Then
        '    m_GraphicAllOff.RotateFlip(RotateFlipType.Rotate90FlipNone)
        'End If


        If m_ValueSelect1 Then
            If Image2 IsNot Nothing Then
                g.DrawImage(Image2, 0, 0)
            End If
        ElseIf m_ValueSelect2 Then
            If Image3 IsNot Nothing Then
                g.DrawImage(Image3, 0, 0)
            End If
        Else
            If Image1 IsNot Nothing Then
                g.DrawImage(Image1, 0, 0)
            End If
        End If



        If Not String.IsNullOrEmpty(MyBase.Text) Then
            g.DrawString(MyBase.Text, MyBase.Font, TextBrush, Convert.ToSingle(Me.Width / 2.0!), Me.Height / 3.0!, sf)
        End If

        If Not String.IsNullOrEmpty(m_Text2) Then
            ' g.DrawString(m_Text2, m_Font2, Brushes.Bisque, Me.Width / 2, (Me.Height / 3) * 2, sf)
            g.DrawString(m_Text2, m_Font2, TextBrush, Text2Rect, sf2)
        End If

        'Copy the back buffer to the screen
        'e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub


    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center
        sf2.Alignment = StringAlignment.Center
        sf2.LineAlignment = StringAlignment.Near

        If TextBrush Is Nothing Then TextBrush = New SolidBrush(MyBase.ForeColor)

    End Sub


    Protected Overrides Sub OnResize(e As System.EventArgs)
        MyBase.OnResize(e)

        Text2Rect = New Rectangle(0, Convert.ToInt32(Me.Height / 1.9), Me.Width, Convert.ToInt32(Height / 2.1))

        RefreshImages()
    End Sub

#End Region

    Private Sub RefreshImages()
        If Width > 0 And Height > 0 Then
            If m_GraphicAllOff IsNot Nothing Then
                Image1 = New Bitmap(Me.Width, Me.Height)

                Using gr_dest As Graphics = Graphics.FromImage(Image1), m As New System.Drawing.Drawing2D.Matrix
                    If m_RotationAngle = RotationAngleEnum.Rotate90 Then
                        'm.Translate(0, -m_GraphicAllOff.Height)
                        m.Translate(m_GraphicAllOff.Height, 0)
                        m.Rotate(90, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicAllOff.Height), Convert.ToSingle(Height / m_GraphicAllOff.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate180 Then
                        m.Translate(-m_GraphicAllOff.Width, -m_GraphicAllOff.Height)
                        m.Rotate(180, Drawing2D.MatrixOrder.Append)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicAllOff.Width), Convert.ToSingle(Height / m_GraphicAllOff.Height), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate270 Then
                        m.Translate(0, m_GraphicAllOff.Width)
                        m.Rotate(270, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicAllOff.Height), Convert.ToSingle(Height / m_GraphicAllOff.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    Else
                        '* No Rotaion
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicAllOff.Width), Convert.ToSingle(Height / m_GraphicAllOff.Height), Drawing2D.MatrixOrder.Append)
                        End If

                    End If

                    gr_dest.Transform = m
                    gr_dest.DrawImage(m_GraphicAllOff, 0, 0)
                End Using
            End If

            If m_GraphicSelect1 IsNot Nothing Then
                Image2 = New Bitmap(Me.Width, Me.Height)

                Using gr_dest As Graphics = Graphics.FromImage(Image2), m As New System.Drawing.Drawing2D.Matrix
                    If m_RotationAngle = RotationAngleEnum.Rotate90 Then
                        m.Translate(m_GraphicSelect1.Height, 0)
                        m.Rotate(90, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect1.Height), Convert.ToSingle(Height / m_GraphicSelect1.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate180 Then
                        m.Translate(-m_GraphicSelect1.Width, -m_GraphicSelect1.Height)
                        m.Rotate(180, Drawing2D.MatrixOrder.Append)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect1.Width), Convert.ToInt32((Height / m_GraphicSelect1.Height)), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate270 Then
                        m.Translate(0, m_GraphicSelect1.Width)
                        m.Rotate(270, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect1.Height), Convert.ToSingle(Height / m_GraphicSelect1.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    Else
                        '* No Rotaion
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect1.Width), Convert.ToSingle(Height / m_GraphicSelect1.Height), Drawing2D.MatrixOrder.Append)
                        End If

                    End If

                    gr_dest.Transform = m
                    gr_dest.DrawImage(m_GraphicSelect1, 0, 0)
                End Using
            End If

            If m_GraphicSelect2 IsNot Nothing Then
                Image3 = New Bitmap(Me.Width, Me.Height)

                Using gr_dest As Graphics = Graphics.FromImage(Image3), m As New System.Drawing.Drawing2D.Matrix
                    If m_RotationAngle = RotationAngleEnum.Rotate90 Then
                        m.Translate(m_GraphicSelect2.Height, 0)
                        m.Rotate(90, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect2.Height), Convert.ToSingle(Height / m_GraphicSelect2.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate180 Then
                        m.Translate(-m_GraphicSelect2.Width, -m_GraphicSelect2.Height)
                        m.Rotate(180, Drawing2D.MatrixOrder.Append)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect2.Width), Convert.ToSingle(Height / m_GraphicSelect2.Height), Drawing2D.MatrixOrder.Append)
                        End If
                    ElseIf m_RotationAngle = RotationAngleEnum.Rotate270 Then
                        m.Translate(0, m_GraphicSelect2.Width)
                        m.Rotate(270, Drawing2D.MatrixOrder.Prepend)
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect2.Height), Convert.ToSingle(Height / m_GraphicSelect2.Width), Drawing2D.MatrixOrder.Append)
                        End If
                    Else
                        '* No Rotaion
                        If SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                            m.Scale(Convert.ToSingle(Width / m_GraphicSelect2.Width), Convert.ToSingle(Height / m_GraphicSelect2.Height), Drawing2D.MatrixOrder.Append)
                        End If

                    End If

                    gr_dest.Transform = m
                    gr_dest.DrawImage(m_GraphicSelect2, 0, 0)
                End Using
            End If

            'BackGroundNeedsRefreshed = True

            Me.Invalidate()
        End If
    End Sub
End Class
