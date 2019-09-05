Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class FlowArrowSolid
    Inherits Control
    Private float_0 As Single

    Private color_0 As Color

    Private bool_0 As Boolean

    Private int_0 As Integer

    Private float_1 As Single

    Private angle_0 As FlowArrowSolid.Angle

    Private amode_0 As FlowArrowSolid.AMode

    Private pointF_0 As PointF()

    Private bool_1 As Boolean

    <Browsable(True)>
    <DefaultValue(GetType(Color), "BlueViolet")>
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
    <DefaultValue(45)>
    <Description("The direction of the arrow.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowDirection As FlowArrowSolid.Angle
        Get
            Return Me.angle_0
        End Get
        Set(ByVal value As FlowArrowSolid.Angle)
            If (value <> Me.angle_0) Then
                Me.angle_0 = value
                Me.method_0()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(1)>
    <Description("The active arrow mode (Blink or Fill).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowMode As FlowArrowSolid.AMode
        Get
            Return Me.amode_0
        End Get
        Set(ByVal value As FlowArrowSolid.AMode)
            If (Me.amode_0 <> value) Then
                Me.amode_0 = value
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(5)>
    <Description("Coefficient to adjust the arrow fill rate (valid values 1 to 10).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowTimerCoefficient As Integer
        Get
            Return CInt(Math.Round(CDbl(Me.float_1)))
        End Get
        Set(ByVal value As Integer)
            If (value > 10) Then
                value = 10
            End If
            If (value < 1) Then
                value = 1
            End If
            If (Me.float_1 <> CSng(value)) Then
                Me.float_1 = CSng(value)
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(100)>
    <Description("Interval in milliseconds to define arrow Blink/Fill rate.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property ArrowTimerInterval As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)

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
        AddHandler MyBase.Click, New EventHandler(AddressOf Me.FlowArrowSolid_Click)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.FlowArrowSolid_Resize)
        Me.float_0 = 1!
        Me.color_0 = Color.BlueViolet
        Me.int_0 = 100
        Me.float_1 = 5!
        Me.angle_0 = FlowArrowSolid.Angle.NE
        Me.amode_0 = FlowArrowSolid.AMode.Fill
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
        Me.method_0()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub FlowArrowSolid_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Focus()
    End Sub

    Private Sub FlowArrowSolid_Resize(ByVal sender As Object, ByVal e As EventArgs)
        Me.method_0()
    End Sub

    Private Sub method_0()
        Me.pointF_0 = New PointF() {New PointF(0!, CSng(MyBase.Height) * 2.1! / 7!), New PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 2.1! / 7!), New PointF(CSng(MyBase.Width) * 3.5! / 7!, 0!), New PointF(CSng(MyBase.Width), CSng(MyBase.Height) * 3.5! / 7!), New PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height)), New PointF(CSng(MyBase.Width) * 3.5! / 7!, CSng(MyBase.Height) * 4.9! / 7!), New PointF(0!, CSng(MyBase.Height) * 4.9! / 7!)}
    End Sub

    Private Sub method_1(ByVal sender As Object, ByVal e As EventArgs)
        Me.bool_1 = Not Me.bool_1
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
        painte.Graphics.RotateTransform(-CSng(Me.angle_0))
        Dim graphic As System.Drawing.Graphics = painte.Graphics
        clientRectangle = MyBase.ClientRectangle
        Dim [single] As Single = -CSng(clientRectangle.Width) / 2!
        clientRectangle = MyBase.ClientRectangle
        graphic.TranslateTransform([single], -CSng(clientRectangle.Height) / 2!)
        Dim rectangleF As System.Drawing.RectangleF = New System.Drawing.RectangleF(New PointF(0!, 0!), New SizeF(Me.float_0, CSng(MyBase.Height)))
        Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
        graphicsPath.AddPolygon(Me.pointF_0)
        Dim blend As System.Drawing.Drawing2D.Blend = New System.Drawing.Drawing2D.Blend(11) With
        {
            .Positions = New Single() {0!, 0.1!, 0.2!, 0.3!, 0.4!, 0.5!, 0.6!, 0.7!, 0.8!, 0.9!, 1!},
            .Factors = New Single() {0!, 0!, 0.1!, 0.2!, 0.3!, 0.5!, 0.3!, 0.2!, 0.1!, 0!, 0!}
        }
        If (Me.amode_0 = FlowArrowSolid.AMode.Fill) Then
            Using linearGradientBrush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), 0), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), MyBase.Height), Me.color_0, ControlPaint.LightLight(Me.color_0))
                linearGradientBrush.Blend = blend
                painte.Graphics.DrawPolygon(New Pen(New SolidBrush(ControlPaint.Dark(Me.color_0)), 1!), Me.pointF_0)
                If (Not Me.BlinkTimer.Enabled) Then
                    painte.Graphics.FillPolygon(linearGradientBrush, Me.pointF_0)
                ElseIf (Me.float_0 <= CSng(MyBase.Width) + 10!) Then
                    Dim region As System.Drawing.Region = New System.Drawing.Region(graphicsPath)
                    region.Intersect(rectangleF)
                    painte.Graphics.FillRegion(linearGradientBrush, region)
                    Me.float_0 += Me.float_1
                Else
                    Me.float_0 = 1!
                    Return
                End If
            End Using
        ElseIf (Not Me.bool_1) Then
            Using linearGradientBrush1 As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), 0), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), MyBase.Height), Me.color_0, ControlPaint.LightLight(Me.color_0))
                linearGradientBrush1.Blend = blend
                painte.Graphics.DrawPolygon(New Pen(New SolidBrush(ControlPaint.Dark(Me.color_0)), 1!), Me.pointF_0)
                painte.Graphics.FillPolygon(linearGradientBrush1, Me.pointF_0)
            End Using
        Else
            Using linearGradientBrush2 As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), 0), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2)), MyBase.Height), ControlPaint.DarkDark(Me.color_0), ControlPaint.Dark(Me.color_0))
                linearGradientBrush2.Blend = blend
                painte.Graphics.DrawPolygon(New Pen(New SolidBrush(ControlPaint.Dark(Me.color_0)), 1!), Me.pointF_0)
                painte.Graphics.FillPolygon(linearGradientBrush2, Me.pointF_0)
            End Using
        End If
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


    Public Enum AMode
        Blink
        Fill
    End Enum

    Public Enum Angle
        E = 0
        NE = 45
        N = 90
        NW = 135
        W = 180
        SW = 225
        S = 270
        SE = 315
    End Enum
End Class
