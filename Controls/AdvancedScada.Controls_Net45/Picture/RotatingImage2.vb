'* 23-AUG-16  v1.0
'* 
'* This is a simple rotating image control.
'*
'* Its purpose is to demonstrate the VB Net code for the following:
'*  - Hardcoding an image and using it as a built-in image
'*  - Providing a choice of selecting specific anchor point for rotation
'*  - Rotating an image around the anchor point (arbitrary angle of rotation, -360 to 360 degree range)
'*  - Horizontal/Vertical flipping
'*  - Free re-sizing or preserving the image aspect ratio
'*

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class RotatingImage2
    Inherits Control

    '* Declare internal timer to be used for endless rotation of the image
    Private WithEvents tmr As Timer

    '* Declare built-in/hardcoded image (AHMI with transparent background)
    Private PictureStream As New IO.MemoryStream(New Byte() _
    {&H89, &H50, &H4E, &H47, &HD, &HA, &H1A, &HA, &H0, &H0, &H0, &HD, &H49, &H48, &H44, &H52, _
     &H0, &H0, &H0, &H30, &H0, &H0, &H0, &H30, &H8, &H6, &H0, &H0, &H0, &H57, &H2, &HF9, _
     &H87, &H0, &H0, &H0, &H9, &H70, &H48, &H59, &H73, &H0, &H0, &HE, &HC3, &H0, &H0, &HE, _
     &HC3, &H1, &HC7, &H6F, &HA8, &H64, &H0, &H0, &H0, &HE8, &H49, &H44, &H41, &H54, &H68, &H81, _
     &HED, &H96, &H4B, &HE, &HC3, &H20, &HC, &H44, &HD9, &HE5, &HFE, &H57, &HE8, &HE9, &HBA, &HEE, _
     &H5, &H68, &HB6, &H4D, &HF8, &HD8, &H1E, &H3B, &H76, &HA5, &H79, &H12, &H2B, &HC0, &HCC, &H4B, _
     &H10, &HD0, &H1A, &H21, &H84, &H10, &HE2, &H48, &HFF, &HB4, &HAE, &H69, &H3F, &H93, &H5F, &HEF, _
     &H2E, &H6E, &H68, &H28, &H34, &H38, &H14, &H5E, &H23, &H60, &H9, &HB6, &H93, &HBD, &HA1, &HD, _
     &H2F, &H15, &H8, &HB, &H8C, &H86, &HF7, &H12, &H50, &H5, &HD5, &H86, &HDF, &H8D, &HFB, &H9B, _
     &HF0, &H16, &H1, &HF7, &H6D, &H82, &H84, &HD7, &H8, &H84, &H7C, &H69, &H34, &HBC, &H54, &HA0, _
     &H6C, &HF8, &H32, &H2, &HC8, &H89, &H62, &H11, &H28, &H13, &H3E, &H5D, &HC0, &HE3, &H36, &H4D, _
     &H13, &HB0, &H5E, &H52, &H96, &H8B, &HEC, &HD1, &HA3, &H32, &H42, &H60, &H26, &H61, &H12, &H89, _
     &HE, &HBF, &HDA, &H7E, &H69, &H6F, &H1B, &H2F, &H81, &H99, &H4, &HF4, &HAA, &H44, &H41, &H6B, _
     &H8A, &H44, &HA2, &HC2, &HCF, &H6A, &HBB, &H12, &H19, &H7E, &H56, &H9F, &H54, &HE3, &HFC, &H27, _
     &HFD, &HDA, &H2C, &HE3, &HA4, &H75, &HDC, &H6B, &H7A, &H8, &H8C, &HFA, &H76, &H12, &H92, &H39, _
     &H25, &H4, &H46, &HF5, &HA4, &HE3, &HCB, &H8, &H68, &H1B, &H2C, &HE0, &HB5, &H90, &H64, &H7E, _
     &H69, &H81, &H55, &H7D, &HEB, &HDC, &HC7, &H5, &H46, &HFD, &HC8, &HD6, &H4C, &H11, &H58, &H11, _
     &H22, &H10, &HB2, &H10, &H5, &H28, &HB0, &H58, &HE8, &H38, &HF2, &H4, &H8, &H21, &H84, &H58, _
     &HF8, &H2, &H24, &H6, &H20, &H39, &HD7, &H11, &H90, &H8D, &H0, &H0, &H0, &H0, &H49, &H45, _
     &H4E, &H44, &HAE, &H42, &H60, &H82})

#Region "Constructor"

    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.ContainerControl Or ControlStyles.SupportsTransparentBackColor, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.Size = New Size(48, 48)
        Me.tmr = New Timer
        Me.tmr.Enabled = False
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If PictureStream IsNot Nothing Then
                    PictureStream.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

#Region "Properties"

    Private ratio As Single = 1.0F
    Private img As Image = Nothing
    <Browsable(True), Category("Properties"), Description("Image to show and rotate. If none is selected then the built-in image will be used."), DefaultValue("")> _
    Public Property RI_Image As Image
        Get
            Return img
        End Get
        Set(value As Image)
            If Me.img IsNot value Then
                Me.img = value
                If Me.img IsNot Nothing Then
                    'Custom loaded image
                    ratio = img.Width / img.Height
                    Me.Size = New Size(img.Width, img.Height)
                Else
                    'Built-in image
                    ratio = 1.0F
                    Me.Size = New Size(48, 48)
                End If
            End If
            Me.Invalidate()
        End Set
    End Property

    Private m_fixedAspect As Boolean
    <Browsable(True), Category("Properties"), Description("Preserve image aspect ratio."), DefaultValue(False)> _
    Public Property RI_FixedAspectRatio As Boolean
        Get
            Return Me.m_fixedAspect
        End Get
        Set(value As Boolean)
            If Me.m_fixedAspect <> value Then Me.m_fixedAspect = value
            Me.RotatingImage_Resize(Me, System.EventArgs.Empty)
            Me.Invalidate()
        End Set
    End Property

    Enum Direction
        Clockwise = 0
        Counterclockwise = 1
    End Enum

    Private m_direction As Direction = Direction.Clockwise
    <Browsable(True), Category("Properties"), Description("Direction of perpetual rotation (clockwise or counterclockwise)."), DefaultValue(Direction.Clockwise)> _
    Public Property RI_PerpetualRotationDirection As Direction
        Get
            Return Me.m_direction
        End Get
        Set(value As Direction)
            If Me.m_direction <> value Then Me.m_direction = value
            Me.Invalidate()
        End Set
    End Property

    Private m_Angle As Single
    <Browsable(True), Category("Properties"), Description("Angle of rotation (valid values -360 to 360)."), DefaultValue(0.0F)> _
    Public Property RI_RotationAngle As Single
        Get
            Return Me.m_Angle
        End Get
        Set(ByVal value As Single)
            If value < -360 OrElse value > 360 Then
                MessageBox.Show("Invalid value!")
                Exit Property
            End If
            If Me.m_Angle <> value Then Me.m_Angle = value
            Me.Invalidate()
        End Set
    End Property

    Enum AnchorPoint
        TopLeft = 0
        TopCenter = 1
        TopRight = 2
        MiddleLeft = 3
        MiddleCenter = 4
        MiddleRight = 5
        BottomLeft = 6
        BottomCenter = 7
        BottomRight = 8
    End Enum

    Private m_Anchor As AnchorPoint = AnchorPoint.MiddleCenter
    <Browsable(True), Category("Properties"), Description("Center point of image rotation."), DefaultValue(AnchorPoint.MiddleCenter)> _
    Public Property RI_CenterOfRotation As AnchorPoint
        Get
            Return Me.m_Anchor
        End Get
        Set(value As AnchorPoint)
            If Me.m_Anchor <> value Then Me.m_Anchor = value
            Me.Invalidate()
        End Set
    End Property

    Private m_flipV As Boolean
    <Browsable(True), Category("Properties"), Description("Flip control vertically."), DefaultValue(False)> _
    Public Property RI_FlipV As Boolean
        Get
            Return Me.m_flipV
        End Get
        Set(ByVal value As Boolean)
            If Me.m_flipV <> value Then Me.m_flipV = value
            Me.Invalidate()
        End Set
    End Property

    Private m_flipH As Boolean
    <Browsable(True), Category("Properties"), Description("Flip control horizontally."), DefaultValue(False)> _
    Public Property RI_FlipH As Boolean
        Get
            Return Me.m_flipH
        End Get
        Set(ByVal value As Boolean)
            If Me.m_flipH <> value Then Me.m_flipH = value
            Me.Invalidate()
        End Set
    End Property

    Private m_Value As Boolean
    <Browsable(True), Category("Properties"), Description("Enable endless rotation."), DefaultValue(False)> _
    Public Property Value As Boolean
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Boolean)
            If Me.m_Value <> value Then
                Me.m_Value = value
                If Me.m_Value Then
                    Me.tmr.Enabled = True
                Else
                    Me.tmr.Enabled = False
                End If
            End If
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("Timer interval for endless rotation (valid values 5-10000)."), DefaultValue(100)> _
    Public Property RI_PerpetualTimerInterval As Integer
        Get
            Return Me.tmr.Interval
        End Get
        Set(ByVal value As Integer)
            If value < 5 OrElse value > 10000 Then
                MessageBox.Show("Invalid value!")
                Exit Property
            End If
            If Me.tmr.Interval <> value Then Me.tmr.Interval = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("Text to display on the control.")> _
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If String.Compare(MyBase.Text, value) <> 0 Then MyBase.Text = value
            Me.Invalidate()
        End Set
    End Property

#End Region

#Region "Protected Metods"

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        '* Create a new bitmap to hold the image and allow for its manipulation before it is displayed
        Dim backImage As Bitmap

        If img IsNot Nothing Then
            backImage = New Bitmap(img)
        Else
            backImage = New Bitmap(PictureStream)
        End If

        '* Rotate the image by using the arbitrary angle set by user.
        '* Move the origin to the selected center of rotation:
        If Me.m_Anchor = AnchorPoint.BottomCenter Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width) / 2.0F, CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.BottomLeft Then
            e.Graphics.TranslateTransform(0.0F, CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.BottomRight Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width), CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.MiddleCenter Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width) / 2.0F, CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.MiddleLeft Then
            e.Graphics.TranslateTransform(0.0F, CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.MiddleRight Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width), CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.TopCenter Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width) / 2.0F, 0.0F)
        ElseIf Me.m_Anchor = AnchorPoint.TopRight Then
            e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width), 0.0F)
        End If

        '* Rotate the image:
        e.Graphics.RotateTransform(-Me.m_Angle)

        '* Move the origin back to to the upper-left corner of the control:
        If Me.m_Anchor = AnchorPoint.BottomCenter Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width) / 2.0F, -CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.BottomLeft Then
            e.Graphics.TranslateTransform(0.0F, -CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.BottomRight Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width), -CSng(Me.ClientRectangle.Height))
        ElseIf Me.m_Anchor = AnchorPoint.MiddleCenter Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width) / 2.0F, -CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.MiddleLeft Then
            e.Graphics.TranslateTransform(0.0F, -CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.MiddleRight Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width), -CSng(Me.ClientRectangle.Height) / 2.0F)
        ElseIf Me.m_Anchor = AnchorPoint.TopCenter Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width) / 2.0F, 0.0F)
        ElseIf Me.m_Anchor = AnchorPoint.TopRight Then
            e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width), 0.0F)
        End If

        '* Pass the current image to the FlipHV sub to flip it (if H or V flip is enabled) and display it
        FlipHV(e.Graphics, backImage, Me.m_flipV, Me.m_flipH)

        backImage.Dispose()

        '* Reset the transformation
        e.Graphics.ResetTransform()

        '* Draw text string on the control
        If Not String.IsNullOrEmpty(Me.Text) Then
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(Me.Width / 2, Me.Height / 2), sf)
        End If

    End Sub

#End Region

#Region "Private Methods"

    Private Sub RotatingImage_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Me.Value = Not Me.Value
    End Sub

    Private Sub RotatingImage_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.m_fixedAspect Then
            Me.Width = ratio * Me.Height
        End If
    End Sub

    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        If Me.m_direction = Direction.Clockwise Then
            Me.RI_RotationAngle -= 1
        Else
            Me.RI_RotationAngle += 1
        End If
        If Me.m_Angle > 359 OrElse Me.m_Angle < -359 Then Me.m_Angle = 0
    End Sub

    '* Reference: https://msdn.microsoft.com/en-us/library/3b575a03(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1
    Private Sub FlipHV(ByVal g As Graphics, ByVal img As Bitmap, ByVal flipV As Boolean, ByVal flipH As Boolean)
        '* Original image points:   Upper-Left (0, 0)
        '*                          Upper-Right (Me.Width, 0)
        '*                          Lower-Left (0, Me.Height)
        '*
        '* Use points() array to store destination points for the above mentioned points of the original image

        '* No flipping - Destination Points are the same as original
        Dim points() As Point = {New Point(0, 0), New Point(Me.Width, 0), New Point(0, Me.Height)}

        '* Flip image horizontally - Destination Points: (Me.Width, 0) (0, 0); (Me.Width, Me.Height)
        If flipH Then points = {New Point(Me.Width, 0), New Point(0, 0), New Point(Me.Width, Me.Height)}

        '* Flip image vertically
        If flipV Then
            If flipH Then '* Account for horizontal flip
                '* Destination Points: (Me.Width, Me.Height); (0, Me.Height); (Me.Width, 0)
                points = {New Point(Me.Width, Me.Height), New Point(0, Me.Height), New Point(Me.Width, 0)}
            Else
                '* Destination Points: (0, Me.Height); (Me.Width, Me.Height); (0, 0)
                points = {New Point(0, Me.Height), New Point(Me.Width, Me.Height), New Point(0, 0)}
            End If
        End If

        '* Draw image using the resulting points() array
        g.DrawImage(img, points)
    End Sub

#End Region

End Class
