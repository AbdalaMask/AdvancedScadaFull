Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class SimpleLED
    Inherits Control
    Private color_0 As Color()

    Private color_1 As Color

    Private color_2 As Color

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private bool_2 As Boolean

    Private color_3 As Color

    Private led_Bri_0 As SimpleLED.LED_Bri

    Private led_Col_0 As SimpleLED.LED_Col

    Private bool_3 As Boolean

    Private bool_4 As Boolean

    Private int_0 As Integer

    Private bool_5 As Boolean

    Private Property BlinkTimer As System.Windows.Forms.Timer


    <Browsable(False)>
    Public Property BorderStyle As System.Windows.Forms.BorderStyle

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable LED blink (also controlled by the mouse DoubleClick event when user interaction is enabled).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Blink As Boolean
        Get
            Return Me.bool_4
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_4 <> value) Then
                Me.bool_4 = value
                If (Not Me.bool_4) Then
                    Me.BlinkTimer.Enabled = False
                Else
                    Me.BlinkTimer.Enabled = True
                End If
                MyBase.Invalidate()
                RaiseEvent BlinkChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(500)>
    <Description("LED blinking interval in milliseconds.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BlinkInterval As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value <= 0) Then
                value = 500
            End If
            If (Me.int_0 <> value) Then
                Me.int_0 = value
                Me.BlinkTimer.Interval = Me.int_0
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable LED border.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Border As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_2 <> value) Then
                Me.bool_2 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(GetType(Color), "MediumSeaGreen")>
    <Description("LED border color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BorderColor As Color
        Get
            Return Me.color_3
        End Get
        Set(ByVal value As Color)
            If (Me.color_3 <> value) Then
                Me.color_3 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("LED brightness.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Brightness As SimpleLED.LED_Bri
        Get
            Return Me.led_Bri_0
        End Get
        Set(ByVal value As SimpleLED.LED_Bri)
            If (Me.led_Bri_0 <> value) Then
                Me.led_Bri_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("LED color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Color As SimpleLED.LED_Col
        Get
            Return Me.led_Col_0
        End Get
        Set(ByVal value As SimpleLED.LED_Col)
            If (Me.led_Col_0 <> value) Then
                Me.led_Col_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(True)>
    <Description("Limit painting region so corners do not hide other close controls.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_LimitPaintingRegion As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_1 <> value) Then
                Me.bool_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable user interaction at Runtime (events: Click for ON/OFF, DoubleClick for blink).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_UserInteraction As Boolean
        Get
            Return Me.bool_5
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_5 <> value) Then
                Me.bool_5 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If (String.Compare(MyBase.Text, value, StringComparison.CurrentCulture) <> 0) Then
                MyBase.Text = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Turn LED ON (also controlled by the mouse Click event when user interaction is enabled).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Boolean
        Get
            Return Me.bool_3
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_3 <> value) Then
                Me.bool_3 = value
                MyBase.Invalidate()
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.SimpleLED_Click)
        AddHandler MyBase.DoubleClick, New EventHandler(AddressOf Me.SimpleLED_DoubleClick)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SimpleLED_Resize)
        Me.color_0 = New Color() {Color.Red, Color.Green, Color.Lime, Color.Blue, Color.Cyan, Color.Orange, Color.Yellow, Color.Violet, Color.White}
        Me.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.bool_1 = True
        Me.color_3 = Color.MediumSeaGreen
        Me.led_Bri_0 = SimpleLED.LED_Bri.Normal
        Me.led_Col_0 = SimpleLED.LED_Col.Red
        Me.int_0 = 500
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.MinimumSize = New System.Drawing.Size(30, 30)
        Me.MaximumSize = New System.Drawing.Size(360, 360)
        MyBase.Width = MyBase.Height
        Me.BlinkTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = Me.int_0,
            .Enabled = False
        }
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        Me.bool_0 = Not Me.bool_0
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(1, 1, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 2!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 2!)))))
        Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(2, 2, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 4!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 4!)))))
        Dim rectangle2 As System.Drawing.Rectangle = New System.Drawing.Rectangle(3, 3, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 6!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 6!)))))
        Dim rectangle3 As System.Drawing.Rectangle = New System.Drawing.Rectangle(5, 5, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 9!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 9!)))))
        painte.Graphics.SmoothingMode = SmoothingMode.HighQuality
        If (Not Me.bool_1) Then
            MyBase.Region = New System.Drawing.Region()
        Else
            Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                graphicsPath.AddEllipse(MyBase.ClientRectangle)
                MyBase.Region = New System.Drawing.Region(graphicsPath)
            End Using
        End If
        If (Me.bool_2) Then
            Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_3, 1!)
                painte.Graphics.DrawEllipse(pen, rectangle)
            End Using
        End If
        If (Not Me.BlinkTimer.Enabled) Then
            If (Not Me.bool_3) Then
                Me.bool_0 = False
            Else
                Me.bool_0 = True
            End If
        End If
        Using graphicsPath1 As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath1.AddEllipse(rectangle3)
            Using pathGradientBrush As System.Drawing.Drawing2D.PathGradientBrush = New System.Drawing.Drawing2D.PathGradientBrush(graphicsPath1)
                If (Not Me.bool_0) Then
                    painte.Graphics.FillEllipse(New SolidBrush(Color.Black), rectangle1)
                    Me.color_1 = Color.FromArgb(255, ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0))))
                    Me.color_2 = Color.FromArgb(100, Me.color_1)
                    painte.Graphics.DrawEllipse(New System.Drawing.Pen(Color.FromArgb(45, Me.color_0(CInt(Me.led_Col_0))), 2.5!), rectangle2)
                Else
                    If (If(Me.led_Col_0 = SimpleLED.LED_Col.Green OrElse Me.led_Col_0 = SimpleLED.LED_Col.Orange, False, Me.led_Col_0 <> SimpleLED.LED_Col.Violet)) Then
                        painte.Graphics.FillEllipse(New SolidBrush(ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0)), 20!)), rectangle1)
                    Else
                        painte.Graphics.FillEllipse(New SolidBrush(ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0)))), rectangle1)
                    End If
                    Me.color_1 = Color.FromArgb(255, Me.color_0(CInt(Me.led_Col_0)))
                    Me.color_2 = Color.FromArgb(0, Me.color_1)
                    painte.Graphics.DrawEllipse(New System.Drawing.Pen(Color.FromArgb(125, Me.color_0(CInt(Me.led_Col_0))), 2.5!), rectangle2)
                    If (Me.led_Bri_0 = SimpleLED.LED_Bri.Brighter) Then
                        pathGradientBrush.CenterColor = ControlPaint.Light(Me.color_0(CInt(Me.led_Col_0)))
                        pathGradientBrush.SurroundColors = New Color() {Color.FromArgb(25, ControlPaint.Light(Me.color_0(CInt(Me.led_Col_0))))}
                        painte.Graphics.FillEllipse(pathGradientBrush, rectangle3)
                    End If
                End If
                pathGradientBrush.CenterColor = Me.color_1
                pathGradientBrush.SurroundColors = New Color() {Me.color_2}
                painte.Graphics.FillEllipse(pathGradientBrush, rectangle3)
                If (Not String.IsNullOrEmpty(Me.Text)) Then
                    Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
                    {
                        .Alignment = StringAlignment.Center,
                        .LineAlignment = StringAlignment.Center
                    }
                    painte.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
                End If
            End Using
        End Using
    End Sub

    Private Sub SimpleLED_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
        If (Not Me.bool_4 AndAlso Me.bool_5) Then
            Me.bool_3 = Not Me.bool_3
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub SimpleLED_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        If (Not Me.bool_4 AndAlso Me.bool_5) Then
            Me.BlinkTimer.Enabled = Not Me.BlinkTimer.Enabled
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub SimpleLED_Resize(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Width = MyBase.Height
    End Sub

    Public Event BlinkChanged As EventHandler


    Public Event ValueChanged As EventHandler


    Public Enum LED_Bri
        Normal
        Brighter
    End Enum

    Public Enum LED_Col
        Red
        Green
        Lime
        Blue
        Cyan
        Orange
        Yellow
        Violet
        White
    End Enum
End Class
