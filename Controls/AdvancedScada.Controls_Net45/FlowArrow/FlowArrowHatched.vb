Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class FlowArrowHatched
    Inherits Control
    Private hstyle_0 As FlowArrowHatched.HStyle

    Private color_0 As Color

    Private bool_0 As Boolean

    Private int_0 As Integer

    Private directionOption_0 As FlowArrowHatched.DirectionOption

    Private bool_1 As Boolean

    <Browsable(True)>
    <DefaultValue(500)>
    <Description("The active arrow blinking interval in milliseconds.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowBlinkInterval As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value < 0) Then
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
    <DefaultValue(GetType(Color), "DarkGreen")>
    <Description("The arrow color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowColor As Color
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
    <DefaultValue(2)>
    <Description("The direction of the arrow.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowDirection As FlowArrowHatched.DirectionOption
        Get
            Return Me.directionOption_0
        End Get
        Set(ByVal value As FlowArrowHatched.DirectionOption)
            Me.directionOption_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(34)>
    <Description("The arrow hatch style.")>
    Public Property ArrowHatchStyle As FlowArrowHatched.HStyle
        Get
            Return Me.hstyle_0
        End Get
        Set(ByVal value As FlowArrowHatched.HStyle)
            Me.hstyle_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Private Property BlinkTimer As System.Windows.Forms.Timer


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
                    Me.BlinkTimer.Enabled = False
                    Me.bool_1 = False
                Else
                    Me.BlinkTimer.Enabled = True
                End If
                MyBase.Invalidate()

                RaiseEvent ValueChanged(Me, EventArgs.Empty)

            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.FlowArrowHatched_Click)
        Me.hstyle_0 = FlowArrowHatched.HStyle.SmallConfetti
        Me.color_0 = Color.PaleGreen
        Me.int_0 = 500
        Me.directionOption_0 = FlowArrowHatched.DirectionOption.Left
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.BackColor = Color.Transparent
        MyBase.Size = New System.Drawing.Size(121, 121)
        Me.MinimumSize = New System.Drawing.Size(28, 28)
        Me.BlinkTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = Me.int_0,
            .Enabled = False
        }
        Me.bool_1 = False
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub FlowArrowHatched_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        Me.bool_1 = Not Me.bool_1
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        painte.Graphics.SmoothingMode = SmoothingMode.HighQuality
        Dim pointF As System.Drawing.PointF() = Nothing
        If (Me.directionOption_0 = FlowArrowHatched.DirectionOption.Left) Then
            pointF = New System.Drawing.PointF() {New System.Drawing.PointF(0!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, 0!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 2.1! / 7!), New System.Drawing.PointF(CSng((MyBase.Width - 1)), CSng(MyBase.Height) * 2.1! / 7!), New System.Drawing.PointF(CSng((MyBase.Width - 1)), CSng(MyBase.Height) * 4.9! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 4.9! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng((MyBase.Height - 1)))}
        End If
        If (Me.directionOption_0 = FlowArrowHatched.DirectionOption.Right) Then
            pointF = New System.Drawing.PointF() {New System.Drawing.PointF(0!, CSng(MyBase.Height) * 2.1! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 2.1! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, 0!), New System.Drawing.PointF(CSng((MyBase.Width - 1)), CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng((MyBase.Height - 1))), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 4.9! / 7!), New System.Drawing.PointF(0!, CSng(MyBase.Height) * 4.9! / 7!)}
        End If
        If (Me.directionOption_0 = FlowArrowHatched.DirectionOption.Up) Then
            pointF = New System.Drawing.PointF() {New System.Drawing.PointF(0!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, 0!), New System.Drawing.PointF(CSng((MyBase.Width - 1)), CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 4.9! / 7!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 4.9! / 7!, CSng((MyBase.Height - 1))), New System.Drawing.PointF(CSng(MyBase.Width) * 2.1! / 7!, CSng((MyBase.Height - 1))), New System.Drawing.PointF(CSng(MyBase.Width) * 2.1! / 7!, CSng(MyBase.Height) * 3.5! / 7!)}
        End If
        If (Me.directionOption_0 = FlowArrowHatched.DirectionOption.Down) Then
            pointF = New System.Drawing.PointF() {New System.Drawing.PointF(0!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 2.1! / 7!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 2.1! / 7!, 0!), New System.Drawing.PointF(CSng(MyBase.Width) * 4.9! / 7!, 0!), New System.Drawing.PointF(CSng(MyBase.Width) * 4.9! / 7!, CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng((MyBase.Width - 1)), CSng(MyBase.Height) * 3.5! / 7!), New System.Drawing.PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng((MyBase.Height - 1)))}
        End If
        Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath.AddPolygon(pointF)
            MyBase.Region = New System.Drawing.Region(graphicsPath)
            If (Me.bool_1) Then
                If (Me.hstyle_0 <> FlowArrowHatched.HStyle.SolidColor) Then
                    Using hatchBrush As System.Drawing.Drawing2D.HatchBrush = New System.Drawing.Drawing2D.HatchBrush(DirectCast(Me.hstyle_0, HatchStyle), ControlPaint.Dark(Me.color_0))
                        Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.color_0)
                            painte.Graphics.DrawPolygon(New Pen(solidBrush, 1!), pointF)
                            painte.Graphics.FillPolygon(hatchBrush, pointF)
                        End Using
                    End Using
                Else
                    Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(ControlPaint.Dark(Me.color_0))
                        painte.Graphics.FillPolygon(solidBrush1, pointF)
                    End Using
                End If
            ElseIf (Me.hstyle_0 <> FlowArrowHatched.HStyle.SolidColor) Then
                Using hatchBrush1 As System.Drawing.Drawing2D.HatchBrush = New System.Drawing.Drawing2D.HatchBrush(DirectCast(Me.hstyle_0, HatchStyle), ControlPaint.Light(Me.color_0))
                    Using solidBrush2 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(ControlPaint.Dark(Me.color_0))
                        painte.Graphics.DrawPolygon(New Pen(solidBrush2, 1!), pointF)
                        painte.Graphics.FillPolygon(hatchBrush1, pointF)
                    End Using
                End Using
            Else
                Using solidBrush3 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.color_0)
                    painte.Graphics.FillPolygon(solidBrush3, pointF)
                End Using
            End If
        End Using
        If (Not String.IsNullOrEmpty(Me.Text)) Then
            Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
            {
                .Alignment = StringAlignment.Center,
                .LineAlignment = StringAlignment.Center
            }
            painte.Graphics.DrawString(Me.Text, Me.Font, New System.Drawing.SolidBrush(Me.ForeColor), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), CInt(Math.Round(CDbl(MyBase.Height) / 2))), stringFormat)
        End If
    End Sub

    Public Event ValueChanged As EventHandler


    Public Enum DirectionOption
        Right
        Up
        Left
        Down
    End Enum

    Public Enum HStyle
        Horizontal
        Vertical
        ForwardDiagonal
        BackwardDiagonal
        Cross
        DiagonalCross
        Percent05
        Percent10
        Percent20
        Percent25
        Percent30
        Percent40
        Percent50
        Percent60
        Percent70
        Percent75
        Percent80
        Percent90
        LightDownwardDiagonal
        LightUpwardDiagonal
        DarkDownwardDiagonal
        DarkUpwardDiagonal
        WideDownwardDiagonal
        WideUpwardDiagonal
        LightVertical
        LightHorizontal
        NarrowVertical
        NarrowHorizontal
        DarkVertical
        DarkHorizontal
        DashedDownwardDiagonal
        DashedUpwardDiagonal
        DashedHorizontal
        DashedVertical
        SmallConfetti
        LargeConfetti
        Zigzag
        Wave
        DiagonalBrick
        HorizontalBrick
        Weave
        Plaid
        Divot
        DottedGrid
        DottedDiamond
        Shingle
        Trellis
        Sphere
        SmallGrid
        SmallCheckerboard
        LargeCheckerboard
        OutlinedDiamond
        SolidDiamond
        SolidColor
    End Enum
End Class
