Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class FlowArrowStriped
    Inherits Control
    Private color_0 As Color

    Private color_1 As Color

    Private bool_0 As Boolean

    Private int_0 As Integer

    Private float_0 As Single

    Private float_1 As Single

    Private float_2 As Single

    Private dir_0 As FlowArrowStriped.Dir

    Private bool_1 As Boolean

    Private pointF_0 As PointF()

    Private point_0 As Point

    Private point_1 As Point

    Private bool_2 As Boolean

    <Browsable(True)>
    <DefaultValue(GetType(Color), "Red")>
    <Description("The arrow color1.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowColor1 As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            If (Me.color_0 <> value) Then
                Me.color_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(GetType(Color), "GreenYellow")>
    <Description("The arrow color2.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowColor2 As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            If (Me.color_1 <> value) Then
                Me.color_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(500)>
    <Description("The arrow color shift interval in milliseconds.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowColorShiftInterval As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value < 0) Then
                value = 500
            End If
            If (Me.int_0 <> value) Then
                Me.int_0 = value
                Me.ShiftTimer.Interval = Me.int_0
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("The direction of the arrow.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowDirection As FlowArrowStriped.Dir
        Get
            Return Me.dir_0
        End Get
        Set(ByVal value As FlowArrowStriped.Dir)
            Dim flag As Boolean
            If (Me.dir_0 <> value) Then
                Me.float_0 = CSng(MyBase.Width)
                Me.float_1 = CSng(MyBase.Height)
                If (Not (value = FlowArrowStriped.Dir.Right Or value = FlowArrowStriped.Dir.Down)) Then
                    Me.float_2 = 180!
                Else
                    Me.float_2 = 0!
                End If
                If ((Me.dir_0 = FlowArrowStriped.Dir.Right OrElse Me.dir_0 = FlowArrowStriped.Dir.Left) AndAlso value <> FlowArrowStriped.Dir.Right) Then
                    If (value = FlowArrowStriped.Dir.Left) Then
                        GoTo Label3
                    End If
                    flag = True
                    GoTo Label0
                End If
Label3:
                If (Me.dir_0 <> FlowArrowStriped.Dir.Up) Then
                    If (Me.dir_0 = FlowArrowStriped.Dir.Down) Then
                        GoTo Label4
                    End If
                    flag = False
                    GoTo Label0
                End If
Label4:
                flag = If(value = FlowArrowStriped.Dir.Up, False, value <> FlowArrowStriped.Dir.Down)
Label0:
                If (Not flag) Then
                    Me.dir_0 = value
                Else
                    Me.dir_0 = value
                    MyBase.Width = CInt(Math.Round(CDbl(Me.float_1)))
                    MyBase.Height = CInt(Math.Round(CDbl(Me.float_0)))
                    Me.FlowArrowStriped_Resize(Me, Nothing)
                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Private Property ShiftTimer As System.Windows.Forms.Timer


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
    <Description("Indicates whether there is a flow.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                If (Not Me.bool_0) Then
                    Me.ShiftTimer.Enabled = False
                    Me.bool_2 = False
                Else
                    Me.ShiftTimer.Enabled = True
                End If
                MyBase.Invalidate()

                RaiseEvent ValueChanged(Me, EventArgs.Empty)

            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.FlowArrowStriped_Click)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.FlowArrowStriped_Resize)
        Me.color_0 = Color.Red
        Me.color_1 = Color.GreenYellow
        Me.int_0 = 500
        Me.dir_0 = FlowArrowStriped.Dir.Right
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.BackColor = Color.Transparent
        MyBase.Size = New System.Drawing.Size(185, 90)
        Me.ShiftTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = Me.int_0,
            .Enabled = False
        }
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub FlowArrowStriped_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
    End Sub

    Private Sub FlowArrowStriped_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (If(Me.dir_0 = FlowArrowStriped.Dir.Right, False, Me.dir_0 <> FlowArrowStriped.Dir.Left)) Then
            If (MyBase.Height < 35) Then
                MyBase.Height = 35
            End If
            MyBase.Width = CInt(Math.Round(CDbl((0.4864865! * CSng(MyBase.Height)))))
            If (MyBase.Width < 15) Then
                MyBase.Width = 15
            End If
        Else
            If (MyBase.Width < 35) Then
                MyBase.Width = 35
            End If
            MyBase.Height = CInt(Math.Round(CDbl((0.4864865! * CSng(MyBase.Width)))))
            If (MyBase.Height < 15) Then
                MyBase.Height = 15
            End If
        End If
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        Me.bool_2 = Not Me.bool_2
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        painte.Graphics.SmoothingMode = SmoothingMode.HighQuality
        Dim graphics As System.Drawing.Graphics = painte.Graphics
        Dim clientRectangle As Rectangle = MyBase.ClientRectangle
        Dim width As Single = CSng(clientRectangle.Width) / 2!
        clientRectangle = MyBase.ClientRectangle
        graphics.TranslateTransform(width, CSng(clientRectangle.Height) / 2!)
        painte.Graphics.RotateTransform(-Me.float_2)
        Dim graphic As System.Drawing.Graphics = painte.Graphics
        clientRectangle = MyBase.ClientRectangle
        Dim [single] As Single = -CSng(clientRectangle.Width) / 2!
        clientRectangle = MyBase.ClientRectangle
        graphic.TranslateTransform([single], -CSng(clientRectangle.Height) / 2!)
        Dim num As Integer = 0
        Do
            If (If(Me.dir_0 = FlowArrowStriped.Dir.Right, False, Me.dir_0 <> FlowArrowStriped.Dir.Left)) Then
                Me.pointF_0 = New PointF() {New PointF(CSng((MyBase.Width - 1)), CSng((num * MyBase.Height)) / 6! + 1!), New PointF(CSng((MyBase.Width - 1)), CSng((num + 1)) * (CSng(MyBase.Height) / 6!)), New PointF(CSng(MyBase.Width) / 2!, CSng((num + 2)) * (CSng(MyBase.Height) / 6!)), New PointF(0!, CSng((num + 1)) * (CSng(MyBase.Height) / 6!)), New PointF(0!, CSng((num * MyBase.Height)) / 6! + 1!), New PointF(CSng(MyBase.Width) / 2!, CSng((num + 1)) * (CSng(MyBase.Height) / 6!))}
                Me.point_0 = New Point(0, CInt(Math.Round(CDbl(MyBase.Height) / 2)))
                Me.point_1 = New Point(MyBase.Width, CInt(Math.Round(CDbl(MyBase.Height) / 2)))
            Else
                Me.pointF_0 = New PointF() {New PointF(CSng((num * MyBase.Width)) / 6! + 1!, 0!), New PointF(CSng((num + 1)) * (CSng(MyBase.Width) / 6!), 0!), New PointF(CSng((num + 2)) * (CSng(MyBase.Width) / 6!), CSng(MyBase.Height) / 2!), New PointF(CSng((num + 1)) * (CSng(MyBase.Width) / 6!), CSng((MyBase.Height - 1))), New PointF(CSng((num * MyBase.Width)) / 6! + 1!, CSng((MyBase.Height - 1))), New PointF(CSng((num + 1)) * (CSng(MyBase.Width) / 6!), CSng(MyBase.Height) / 2!)}
                Me.point_0 = New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), 0)
                Me.point_1 = New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), MyBase.Height)
            End If
            Dim blend As System.Drawing.Drawing2D.Blend = New System.Drawing.Drawing2D.Blend(11) With
            {
                .Positions = New Single() {0!, 0.1!, 0.2!, 0.3!, 0.4!, 0.5!, 0.6!, 0.7!, 0.8!, 0.9!, 1!},
                .Factors = New Single() {0!, 0!, 0.1!, 0.2!, 0.3!, 0.5!, 0.3!, 0.2!, 0.1!, 0!, 0!}
            }
            Dim color1 As Color = New Color()
            If (Not Me.bool_1) Then
                If (Not Me.bool_2) Then
                    color1 = If(Not Me.bool_0, ControlPaint.Dark(Me.color_0), Me.color_0)
                Else
                    color1 = Me.color_1
                End If
                Me.bool_1 = True
            Else
                If (Not Me.bool_2) Then
                    color1 = If(Not Me.bool_0, ControlPaint.Dark(Me.color_1), Me.color_1)
                Else
                    color1 = Me.color_0
                End If
                Me.bool_1 = False
            End If
            Using linearGradientBrush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(Me.point_0, Me.point_1, color1, ControlPaint.LightLight(color1))
                linearGradientBrush.Blend = blend
                painte.Graphics.DrawPolygon(New Pen(New SolidBrush(Color.Black), 1!), Me.pointF_0)
                painte.Graphics.FillPolygon(linearGradientBrush, Me.pointF_0)
            End Using
            num = num + 1
        Loop While num <= 4
        Me.bool_1 = False
        painte.Graphics.ResetTransform()
        If (Not String.IsNullOrEmpty(Me.Text)) Then
            Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
            {
                .Alignment = StringAlignment.Center,
                .LineAlignment = StringAlignment.Center
            }
            painte.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
        End If
    End Sub

    Public Event ValueChanged As EventHandler


    Public Enum Dir
        Right
        Left
        Up
        Down
    End Enum
End Class
