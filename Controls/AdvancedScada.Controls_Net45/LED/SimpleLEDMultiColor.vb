Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class SimpleLEDMultiColor
    Inherits Control
    Private color_0 As Color()

    Private color_1 As Color

    Private color_2 As Color

    Friend bool_0 As Boolean

    Private bool_1 As Boolean

    Private bool_2 As Boolean

    Private color_3 As Color

    Private led_Bri_0 As SimpleLEDMultiColor.LED_Bri

    Private led_Col_0 As SimpleLEDMultiColor.LED_Col

    Private led_Col_1 As SimpleLEDMultiColor.LED_Col

    Private int_0 As Integer

    Private bool_3 As Boolean

    Private bool_4 As Boolean

    Private int_1 As Integer

    Private bool_5 As Boolean

    Private Property BlinkTimer As System.Windows.Forms.Timer


    <Browsable(False)>
    Public Property BorderStyle As System.Windows.Forms.BorderStyle

    <Browsable(False)>
    <DefaultValue(False)>
    <Description("Enable LED blink.")>
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
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(500)>
    <Description("LED blinking interval in milliseconds.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BlinkInterval As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (value <= 0) Then
                value = 500
            End If
            If (Me.int_1 <> value) Then
                Me.int_1 = value
                Me.BlinkTimer.Interval = Me.int_1
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
    Public Property LED_Brightness As SimpleLEDMultiColor.LED_Bri
        Get
            Return Me.led_Bri_0
        End Get
        Set(ByVal value As SimpleLEDMultiColor.LED_Bri)
            If (Me.led_Bri_0 <> value) Then
                Me.led_Bri_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <Description("LED color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public ReadOnly Property LED_Color As SimpleLEDMultiColor.LED_Col
        Get
            Return Me.led_Col_0
        End Get
    End Property

    <Browsable(True)>
    <Description("Set LED default OFF color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ColorDefaultOFF As SimpleLEDMultiColor.LED_Col
        Get
            Return Me.led_Col_1
        End Get
        Set(ByVal value As SimpleLEDMultiColor.LED_Col)
            If (Me.led_Col_1 <> value) Then
                Me.led_Col_1 = value
                If (Me.int_0 = 0) Then
                    Me.led_Col_0 = Me.led_Col_1
                End If
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
    <Description("Show the current integer value on the LED itself (also controlled by the mouse DoubleClick event).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ShowValue As Boolean
        Get
            Return Me.bool_3
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_3 <> value) Then
                Me.bool_3 = value
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable user interaction at Runtime (when focused use MouseWheel to increase/decrease integer value thus change the LED color and state).")>
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

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("LED single input integer value (valid range 0 to 18).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (If(value < 0, True, value > 18)) Then
                value = 0
            End If
            If (Me.int_0 <> value) Then
                Me.int_0 = value
                If (Me.int_0 = 0) Then
                    Me.led_Col_0 = Me.led_Col_1
                ElseIf (Me.int_0 = 1 Or Me.int_0 = 2) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Red
                ElseIf (Me.int_0 = 3 Or Me.int_0 = 4) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Green
                ElseIf (Me.int_0 = 5 Or Me.int_0 = 6) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Lime
                ElseIf (Me.int_0 = 7 Or Me.int_0 = 8) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Blue
                ElseIf (Me.int_0 = 9 Or Me.int_0 = 10) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Cyan
                ElseIf (Me.int_0 = 11 Or Me.int_0 = 12) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Orange
                ElseIf (Me.int_0 = 13 Or Me.int_0 = 14) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Yellow
                ElseIf (Not (Me.int_0 = 15 Or Me.int_0 = 16)) Then
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.White
                Else
                    Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Violet
                End If
                Me.method_0(Me.int_0)
                MyBase.Invalidate()
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.SimpleLEDMultiColor_Click)
        AddHandler MyBase.DoubleClick, New EventHandler(AddressOf Me.SimpleLEDMultiColor_DoubleClick)
        AddHandler MyBase.MouseWheel, New MouseEventHandler(AddressOf Me.SimpleLEDMultiColor_MouseWheel)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SimpleLEDMultiColor_Resize)
        Me.color_0 = New Color() {Color.Red, Color.Green, Color.Lime, Color.Blue, Color.Cyan, Color.Orange, Color.Yellow, Color.Violet, Color.White}
        Me.bool_0 = False
        Me.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.bool_1 = True
        Me.color_3 = Color.MediumSeaGreen
        Me.led_Bri_0 = SimpleLEDMultiColor.LED_Bri.Normal
        Me.led_Col_1 = SimpleLEDMultiColor.LED_Col.Red
        Me.int_1 = 500
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.MinimumSize = New System.Drawing.Size(30, 30)
        Me.MaximumSize = New System.Drawing.Size(360, 360)
        MyBase.Width = MyBase.Height
        Me.BlinkTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = Me.int_1,
            .Enabled = False
        }
        Me.led_Col_0 = SimpleLEDMultiColor.LED_Col.Red
        Me.bool_0 = False
        Me.LED_Blink = False
    End Sub

    Private Sub method_0(ByVal int_2 As Integer)
        If (int_2 = 0) Then
            Me.bool_0 = False
            Me.LED_Blink = False
        End If
        If (int_2 = 1 Or int_2 = 3 Or int_2 = 5 Or int_2 = 7 Or int_2 = 9 Or int_2 = 11 Or int_2 = 13 Or int_2 = 15 Or int_2 = 17) Then
            Me.LED_Blink = False
            Me.bool_0 = True
        End If
        If (int_2 = 2 Or int_2 = 4 Or int_2 = 6 Or int_2 = 8 Or int_2 = 10 Or int_2 = 12 Or int_2 = 14 Or int_2 = 16 Or int_2 = 18) Then
            Me.bool_0 = False
            Me.LED_Blink = True
        End If
    End Sub

    Private Sub method_1(ByVal sender As Object, ByVal e As EventArgs)
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
            Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath.AddEllipse(MyBase.ClientRectangle)
            MyBase.Region = New System.Drawing.Region(graphicsPath)
        End If
        If (Me.bool_2) Then
            painte.Graphics.DrawEllipse(New Pen(Me.color_3, 1!), rectangle)
        End If
        Using graphicsPath1 As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath1.AddEllipse(rectangle3)
            Using pathGradientBrush As System.Drawing.Drawing2D.PathGradientBrush = New System.Drawing.Drawing2D.PathGradientBrush(graphicsPath1)
                If (Not Me.bool_0) Then
                    painte.Graphics.FillEllipse(New SolidBrush(Color.Black), rectangle1)
                    Me.color_1 = Color.FromArgb(255, ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0))))
                    Me.color_2 = Color.FromArgb(100, Me.color_1)
                    painte.Graphics.DrawEllipse(New Pen(Color.FromArgb(45, Me.color_0(CInt(Me.led_Col_0))), 2.5!), rectangle2)
                Else
                    painte.Graphics.FillEllipse(New SolidBrush(ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0)))), rectangle1)
                    Me.color_1 = Color.FromArgb(255, Me.color_0(CInt(Me.led_Col_0)))
                    Me.color_2 = Color.FromArgb(0, Me.color_1)
                    painte.Graphics.DrawEllipse(New Pen(Color.FromArgb(125, Me.color_0(CInt(Me.led_Col_0))), 2.5!), rectangle2)
                    If (Me.led_Bri_0 = SimpleLEDMultiColor.LED_Bri.Brighter) Then
                        pathGradientBrush.CenterColor = ControlPaint.Light(Me.color_0(CInt(Me.led_Col_0)))
                        pathGradientBrush.SurroundColors = New Color() {Color.FromArgb(25, ControlPaint.Light(Me.color_0(CInt(Me.led_Col_0))))}
                        painte.Graphics.FillEllipse(pathGradientBrush, rectangle3)
                    End If
                End If
                pathGradientBrush.CenterColor = Me.color_1
                pathGradientBrush.SurroundColors = New Color() {Me.color_2}
                painte.Graphics.FillEllipse(pathGradientBrush, rectangle3)
                If (Not String.IsNullOrEmpty(Me.Text) Or Me.bool_3) Then
                    Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
                    {
                        .Alignment = StringAlignment.Center,
                        .LineAlignment = StringAlignment.Center
                    }
                    If (Not Me.bool_3) Then
                        painte.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
                    Else
                        painte.Graphics.DrawString(Me.int_0.ToString(), Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
                    End If
                End If
            End Using
        End Using
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Private Sub SimpleLEDMultiColor_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
    End Sub

    Private Sub SimpleLEDMultiColor_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.LED_ShowValue = Not Me.LED_ShowValue
        MyBase.Invalidate()
    End Sub

    Private Sub SimpleLEDMultiColor_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (If(Not Me.Focused, False, Me.bool_5)) Then
            If (Math.Sign(e.Delta) <= 0) Then
                Me.Value = Me.Value - 1
                If (Me.Value < 0) Then
                    Me.Value = 0
                End If
            Else
                Me.Value = Me.Value + 1
                If (Me.Value > 18) Then
                    Me.Value = 18
                End If
            End If
        End If
    End Sub

    Private Sub SimpleLEDMultiColor_Resize(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Width = MyBase.Height
    End Sub

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
