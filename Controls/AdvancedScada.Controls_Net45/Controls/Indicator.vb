Imports System.Drawing
'****************************************************************************
'* Indicator
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* 10-MAY-11
'*
'* 1-AUG-14 Added Invalidate on OutlineWidth property setter
'****************************************************************************
Public Class Indicator
    Inherits System.Windows.Forms.Control

    Public Event ValueSelectColor2Changed As EventHandler
    Public Event ValueSelectColor3Changed As EventHandler

    Private TextRect As New Rectangle

    Private BlinkTimer As System.Windows.Forms.Timer

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        ''* reduce the flicker
        Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor,
                    True)


        'BackColor = Drawing.Color.Transparent
    End Sub

    '****************************************************************
    '* UserControl overrides dispose to clean up the component list.
    '****************************************************************
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If BrushColor1 IsNot Nothing Then BrushColor1.Dispose()
                If BrushColor2 IsNot Nothing Then BrushColor2.Dispose()
                If BrushColor3 IsNot Nothing Then BrushColor3.Dispose()
                If BorderPen IsNot Nothing Then BorderPen.Dispose()
                If TextBrush IsNot Nothing Then TextBrush.Dispose()
                If BackgroundBrush IsNot Nothing Then BackgroundBrush.Dispose()
                If sf IsNot Nothing Then sf.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

#Region "Basic Properties"

    '*****************************************
    '* Property - Select Color 2
    '*****************************************
    Private m_SelectColor2 As Boolean
    <System.ComponentModel.Category("Appearance")> _
    Public Property SelectColor2() As Boolean
        Get
            Return m_SelectColor2
        End Get
        Set(ByVal value As Boolean)
            If value <> m_SelectColor2 Then
                m_SelectColor2 = value

                'RefreshBackground = True
                Me.Invalidate()

                OnValueSelectColor2Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Select Color 3
    '*****************************************
    Private m_SelectColor3 As Boolean
    <System.ComponentModel.Category("Appearance")> _
    Public Property SelectColor3() As Boolean
        Get
            Return m_SelectColor3
        End Get
        Set(ByVal value As Boolean)
            If value <> m_SelectColor3 Then
                m_SelectColor3 = value

                Me.Invalidate()

                OnValueSelectColor3Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Color 1, no selection
    '*****************************************
    Private m_Color1 As Color = Color.DarkGray
    Private BrushColor1 As SolidBrush
    <System.ComponentModel.Category("Appearance")> _
    Public Property Color1() As Color
        Get
            Return (m_Color1)
        End Get
        Set(ByVal value As Color)
            If m_Color1 <> value Then
                m_Color1 = value
                'If BrushColor1 IsNot Nothing Then BrushColor1.Dispose()

                If BrushColor1 Is Nothing Then
                    BrushColor1 = New SolidBrush(m_Color1)
                Else
                    BrushColor1.Color = value
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Color 2
    '*****************************************
    Private m_Color2 As Color = Color.Green
    Private BrushColor2 As SolidBrush
    <System.ComponentModel.Category("Appearance")> _
    Public Property Color2() As Color
        Get
            Return (m_Color2)
        End Get
        Set(ByVal value As Color)
            If m_Color2 <> value Then
                m_Color2 = value
                If BrushColor2 Is Nothing Then
                    BrushColor2 = New SolidBrush(m_Color2)
                Else
                    BrushColor2.Color = value
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Color 3
    '*****************************************
    Private m_Color3 As Color = Color.Red
    Private BrushColor3 As SolidBrush
    <System.ComponentModel.Category("Appearance")> _
    Public Property Color3() As Color
        Get
            Return (m_Color3)
        End Get
        Set(ByVal value As Color)
            If m_Color3 <> value Then
                m_Color3 = value
                If BrushColor3 Is Nothing Then
                    BrushColor3 = New SolidBrush(m_Color3)
                Else
                    BrushColor3.Color = value
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

    Private _OutlineColor As Color = Color.Transparent
    <System.ComponentModel.Category("Appearance")> _
    Public Property OutlineColor() As Color
        Get
            Return _OutlineColor
        End Get
        Set(ByVal value As Color)
            _OutlineColor = value

            If BorderPen Is Nothing Then
                BorderPen = New Pen(_OutlineColor, m_OutlineWidth)
            Else
                BorderPen.Color = value
            End If

            Me.Invalidate()
        End Set
    End Property

    Private m_OutlineWidth As Integer = 1
    <System.ComponentModel.Category("Appearance")>
    Public Property OutlineWidth() As Integer
        Get
            Return m_OutlineWidth
        End Get
        Set(ByVal value As Integer)
            If value <> m_OutlineWidth Then
                m_OutlineWidth = Math.Max(value, 1)
                BorderPen = New Pen(_OutlineColor, m_OutlineWidth)
                Me.Invalidate()
            End If
        End Set
    End Property

    Private m_Blink As Boolean
    Public Property Blink As Boolean
        Get
            Return m_Blink
        End Get
        Set(value As Boolean)
            m_Blink = value
            If m_Blink Then
                If BlinkTimer Is Nothing Then
                    BlinkTimer = New System.Windows.Forms.Timer()
                    BlinkTimer.Interval = 800
                    AddHandler BlinkTimer.Tick, AddressOf BlinkTimer_Tick
                End If
            End If
            If BlinkTimer IsNot Nothing Then
                BlinkTimer.Enabled = value
            End If

            If Not value Then
                SelectColor2 = False
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Shape
    '*****************************************
    Public Enum ShapeType
        Round
        Rectangle
    End Enum

    Private m_Shape As ShapeType = ShapeType.Round
    <System.ComponentModel.Category("Appearance")>
    Public Property Shape() As ShapeType
        Get
            Return m_Shape
        End Get
        Set(ByVal value As ShapeType)
            If value <> m_Shape Then
                m_Shape = value
                'RefreshBackground = True

                If m_Shape = ShapeType.Round Then
                    Using CircularPath As New System.Drawing.Drawing2D.GraphicsPath
                        CircularPath.AddEllipse(0, 0, Width, Height)
                        Me.Region = New System.Drawing.Region(CircularPath)
                    End Using
                Else
                    Me.Region = Nothing
                End If
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Events"

    Protected Overridable Sub OnValueSelectColor2Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelectColor2Changed(Me, e)
    End Sub

    Protected Overridable Sub OnValueSelectColor3Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelectColor3Changed(Me, e)
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As System.EventArgs)
        MyBase.OnForeColorChanged(e)

        If TextBrush Is Nothing Then
            TextBrush = New SolidBrush(MyBase.ForeColor)
        Else
            TextBrush.Color = MyBase.ForeColor
        End If

        If BorderPen Is Nothing Then
            BorderPen = New Pen(_OutlineColor, m_OutlineWidth)
        Else
            BorderPen.Color = _OutlineColor
        End If

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnBackColorChanged(e As System.EventArgs)
        MyBase.OnBackColorChanged(e)

        If BackgroundBrush Is Nothing Then
            BackgroundBrush = New SolidBrush(BackColor)
        Else
            BackgroundBrush.Color = BackColor
        End If

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnTextChanged(e As System.EventArgs)
        MyBase.OnTextChanged(e)

        Me.Invalidate()
    End Sub

    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private sf As New StringFormat
    Private TextBrush As SolidBrush
    Private BackgroundBrush As SolidBrush
    Private BorderPen As Pen
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If TextBrush Is Nothing Or BorderPen Is Nothing Or BrushColor1 Is Nothing Or
        BrushColor2 Is Nothing Or BrushColor3 Is Nothing Or BackgroundBrush Is Nothing Then Exit Sub

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        'e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.High

        MyBase.OnPaint(e)

        'Dim _buffer As New Bitmap(Me.Width, Me.Height)


        Dim g As Graphics = e.Graphics ' Graphics.FromImage(_buffer)

        'g.InterpolationMode = Drawing2D.InterpolationMode.High

        'g.FillRectangle(BackgroundBrush, 0, 0, Me.Width, Me.Height)


        If m_Shape = ShapeType.Round Then
            If m_SelectColor2 Then
                If BrushColor2.Color <> Color.Transparent Then g.FillEllipse(BrushColor2, 0, 0, Me.Width - 1, Me.Height - 1)
            ElseIf m_SelectColor3 Then
                g.FillEllipse(BrushColor3, 0, 0, Me.Width - 1, Me.Height - 1)
            Else
                If BrushColor1.Color <> Color.Transparent Then g.FillEllipse(BrushColor1, 0, 0, Me.Width - 1, Me.Height - 1)
            End If
            g.DrawEllipse(BorderPen, CInt(Math.Floor(m_OutlineWidth / 2)), CInt(Math.Floor(m_OutlineWidth / 2)), Me.Width - m_OutlineWidth, Me.Height - m_OutlineWidth)
        Else
            If m_SelectColor2 Then
                g.FillRectangle(BrushColor2, 0, 0, Me.Width, Me.Height)
            ElseIf m_SelectColor3 Then
                g.FillRectangle(BrushColor3, 0, 0, Me.Width, Me.Height)
            Else
                g.FillRectangle(BrushColor1, 0, 0, Me.Width, Me.Height)
            End If
            g.DrawRectangle(BorderPen, 0, 0, Me.Width - 1, Me.Height - 1)
        End If

        If Not String.IsNullOrEmpty(MyBase.Text) Then
            If TextBrush.Color <> MyBase.ForeColor Then
                TextBrush.Color = MyBase.ForeColor
            End If

            g.DrawString(MyBase.Text, MyBase.Font, TextBrush, Me.Width / 2, Me.Height / 2, sf)
        End If

        'e.Graphics.DrawImage(_buffer, 0, 0)
    End Sub

    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        '* If there is a black background, then change the default foreground color from black
        If DesignMode Then
            If Me.Parent.BackColor = Color.Black And MyBase.ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.ControlText) Then
                ForeColor = Color.WhiteSmoke
            End If
        End If

        If TextBrush Is Nothing Then
            TextBrush = New SolidBrush(MyBase.ForeColor)
        End If

        If BorderPen Is Nothing Then
            BorderPen = New Pen(_OutlineColor)
        End If

        If BrushColor1 Is Nothing Then
            BrushColor1 = New SolidBrush(Me.Color1)
        End If

        If BrushColor2 Is Nothing Then
            BrushColor2 = New SolidBrush(Me.Color2)
        End If

        If BrushColor3 Is Nothing Then
            BrushColor3 = New SolidBrush(Me.Color3)
        End If

        If BackgroundBrush Is Nothing Then
            BackgroundBrush = New SolidBrush(Me.BackColor)
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        If m_Shape = ShapeType.Round Then
            Using CircularPath As New System.Drawing.Drawing2D.GraphicsPath
                CircularPath.AddEllipse(0, 0, Width, Height)
                Me.Region = New System.Drawing.Region(CircularPath)
            End Using
        End If
    End Sub

    Private Sub BlinkTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SelectColor2 = Not SelectColor2
    End Sub

#End Region

End Class
