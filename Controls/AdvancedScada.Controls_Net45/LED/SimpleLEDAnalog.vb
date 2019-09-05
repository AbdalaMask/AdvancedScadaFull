Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class SimpleLEDAnalog
    Inherits Control
    Private color_0 As Color()

    Private color_1 As Color

    Private color_2 As Color

    Private color_3 As Color

    Private color_4 As Color

    Private float_0 As Single

    Private float_1 As Single

    Private float_2 As Single

    Private float_3 As Single

    Private float_4 As Single

    Private float_5 As Single

    Private float_6 As Single

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private color_5 As Color

    Private led_Col_0 As SimpleLEDAnalog.LED_Col

    Private float_7 As Single

    Private bool_2 As Boolean

    Private float_8 As Single

    Private float_9 As Single

    Private bool_3 As Boolean

    <Browsable(False)>
    Public Property BorderStyle As System.Windows.Forms.BorderStyle

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable LED border.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Border As Boolean
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
    <DefaultValue(GetType(Color), "MediumSeaGreen")>
    <Description("LED border color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BorderColor As Color
        Get
            Return Me.color_5
        End Get
        Set(ByVal value As Color)
            If (Me.color_5 <> value) Then
                Me.color_5 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("LED color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Color As SimpleLEDAnalog.LED_Col
        Get
            Return Me.led_Col_0
        End Get
        Set(ByVal value As SimpleLEDAnalog.LED_Col)
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
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Show the current analog value on the LED itself (also controlled by the mouse DoubleClick event).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ShowValue As Boolean
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
    <DefaultValue(False)>
    <Description("Enable user interaction at Runtime (when focused use MouseWheel to increase/decrease the LED brightness).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_UserInteraction As Boolean
        Get
            Return Me.bool_3
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_3 <> value) Then
                Me.bool_3 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(255!)>
    <Description("Maximum LED analog value (needs to be at least 5 higher than the Minimum).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Maximum As Single
        Get
            Return Me.float_8
        End Get
        Set(ByVal value As Single)
            If (Me.float_8 <> value) Then
                Me.float_8 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0!)>
    <Description("Minimum LED analog value (needs to be at least 5 lower than the Maximum).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Minimum As Single
        Get
            Return Me.float_9
        End Get
        Set(ByVal value As Single)
            If (Me.float_9 <> value) Then
                Me.float_9 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0!)>
    <Description("LED analog value.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Single
        Get
            Return Me.float_7
        End Get
        Set(ByVal value As Single)
            If (Me.float_7 <> value) Then
                Me.float_7 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.SimpleLEDAnalog_Click)
        AddHandler MyBase.DoubleClick, New EventHandler(AddressOf Me.SimpleLEDAnalog_DoubleClick)
        AddHandler MyBase.MouseWheel, New MouseEventHandler(AddressOf Me.SimpleLEDAnalog_MouseWheel)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SimpleLEDAnalog_Resize)
        Me.color_0 = New Color() {Color.Red, Color.Green, Color.Lime, Color.Blue, Color.Cyan, Color.Orange, Color.Yellow, Color.Violet, Color.White}
        Me.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.bool_0 = True
        Me.color_5 = Color.MediumSeaGreen
        Me.float_8 = 255!
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent
        Me.MinimumSize = New System.Drawing.Size(30, 30)
        Me.MaximumSize = New System.Drawing.Size(360, 360)
        MyBase.Width = MyBase.Height
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        If (Math.Min(Me.float_9, Me.float_8) = Me.float_8) Then
            Me.float_9 = Me.float_8 - 5!
            MessageBox.Show("The minimum value should be less than the currently set maximum value!")
        ElseIf (Math.Max(Me.float_8, Me.float_9) = Me.float_9) Then
            Me.float_8 = Me.float_9 + 5!
            MessageBox.Show("The maximum value should be greater than the currently set minimum value!")
        ElseIf (Me.float_8 - Me.float_9 >= 5!) Then
            Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(1, 1, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 2!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 2!)))))
            Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(2, 2, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 4!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 4!)))))
            Dim rectangle2 As System.Drawing.Rectangle = New System.Drawing.Rectangle(3, 3, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 6!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 6!)))))
            Dim rectangle3 As System.Drawing.Rectangle = New System.Drawing.Rectangle(5, 5, CInt(Math.Round(CDbl((CSng(MyBase.Width) - 9!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - 9!)))))
            painte.Graphics.SmoothingMode = SmoothingMode.HighQuality
            If (Not Me.bool_0) Then
                MyBase.Region = New System.Drawing.Region()
            Else
                Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                    graphicsPath.AddEllipse(MyBase.ClientRectangle)
                    MyBase.Region = New System.Drawing.Region(graphicsPath)
                End Using
            End If
            Using graphicsPath1 As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                graphicsPath1.AddEllipse(rectangle3)
                Using pathGradientBrush As System.Drawing.Drawing2D.PathGradientBrush = New System.Drawing.Drawing2D.PathGradientBrush(graphicsPath1)
                    If (Me.bool_1) Then
                        Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_5, 1!)
                            painte.Graphics.DrawEllipse(pen, rectangle)
                        End Using
                    End If
                    Dim num As Integer = CInt(Math.Round(CDbl(Me.float_9)))
                    Dim num1 As Integer = CInt(Math.Round(CDbl(Math.Max(Me.float_9, Math.Min(Me.float_8, Me.float_7)))))
                    Dim num2 As Integer = num
                    Do
                        Me.float_0 = CSng(num2) - Me.float_9
                        Me.float_1 = Me.float_8 - Me.float_9
                        Me.float_2 = CSng(CInt(Math.Round(CDbl((Me.float_0 * 255! / Me.float_1)))))
                        Me.float_3 = CSng(CInt(Math.Round(CDbl((Me.float_0 * 150! / Me.float_1)))))
                        Me.float_4 = CSng(CInt(Math.Round(CDbl((Me.float_0 * 510! / Me.float_1)))))
                        Me.float_5 = CSng(CInt(Math.Round(CDbl((Me.float_0 * 310! / Me.float_1)))))
                        Me.float_6 = CSng(CInt(Math.Round(CDbl((Me.float_0 * 120! / Me.float_1)))))
                        If (Me.float_2 >= 128!) Then
                            Me.color_3 = Color.FromArgb(CInt(Math.Round(CDbl((Me.float_4 - 255!)))), Me.color_0(CInt(Me.led_Col_0)))
                            Me.color_4 = Color.FromArgb(CInt(Math.Round(CDbl((Me.float_6 + 75!)))), Me.color_1)
                            pathGradientBrush.CenterColor = Me.color_3
                            pathGradientBrush.SurroundColors = New Color() {Me.color_4}
                            painte.Graphics.FillEllipse(New SolidBrush(Me.color_2), rectangle3)
                        Else
                            Me.color_1 = Color.FromArgb(255, ControlPaint.Dark(Me.color_0(CInt(Me.led_Col_0))))
                            Me.color_2 = Color.FromArgb(CInt(Math.Round(CDbl((100! + Me.float_5)))), Me.color_1)
                            pathGradientBrush.CenterColor = Me.color_1
                            pathGradientBrush.SurroundColors = New Color() {Me.color_2}
                            painte.Graphics.FillEllipse(New SolidBrush(Color.Black), rectangle1)
                        End If
                        num2 = num2 + 1
                    Loop While num2 <= num1
                    painte.Graphics.DrawEllipse(New System.Drawing.Pen(Color.FromArgb(CInt(Math.Round(CDbl((Me.float_3 + 45!)))), Me.color_0(CInt(Me.led_Col_0))), 2.5!), rectangle2)
                    painte.Graphics.FillEllipse(pathGradientBrush, rectangle3)
                    If (Not String.IsNullOrEmpty(Me.Text) Or Me.bool_2) Then
                        Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
                        {
                            .Alignment = StringAlignment.Center,
                            .LineAlignment = StringAlignment.Center
                        }
                        If (Not Me.bool_2) Then
                            painte.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
                        Else
                            painte.Graphics.DrawString(Me.float_7.ToString(), Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
                        End If
                    End If
                End Using
            End Using
        Else
            Me.float_8 = Me.float_9 + 5!
            MessageBox.Show("The operating range is narrow. The difference between Maximum and Minimum values will be increased!")
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Private Sub SimpleLEDAnalog_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
    End Sub

    Private Sub SimpleLEDAnalog_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Me.LED_ShowValue = Not Me.LED_ShowValue
        MyBase.Invalidate()
    End Sub

    Private Sub SimpleLEDAnalog_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (If(Not Me.Focused, False, Me.bool_3)) Then
            If (Math.Sign(e.Delta) <= 0) Then
                Me.Value = Me.Value - 2.5!
            Else
                Me.Value = Me.Value + 2.5!
            End If
        End If
    End Sub

    Private Sub SimpleLEDAnalog_Resize(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Width = MyBase.Height
    End Sub

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
