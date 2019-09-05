Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace FlowArrow
    Public Class FlowArrowMatrix2
        Inherits Control
        ' Events
        Public Event ValueChanged As EventHandler

        ' Methods
        Public Sub New()
            AddHandler MyBase.Click, New EventHandler(AddressOf Me.FlowArrowMatrix2_Click)
            AddHandler MyBase.Resize, New EventHandler(AddressOf Me.FlowArrowMatrix2_Resize)
            Me.color_0 = Color.DodgerBlue
            Me.int_0 = 500
            Me.m_Angle = Angle.SE
            MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or (ControlStyles.SupportsTransparentBackColor Or (ControlStyles.ResizeRedraw Or (ControlStyles.UserPaint Or ControlStyles.ContainerControl))))), True)
            MyBase.DoubleBuffered = True
            Me.DoubleBuffered = True
            MyBase.BackColor = Color.Transparent
            MyBase.Size = New Size(&H79, &H79)
            Me.ulong_0 = Convert.ToUInt64("0001000001100001111111111111011111100110000001000", 2)
            Me.BlinkTimer = New Timer
            Me.BlinkTimer.Interval = Me.int_0
            Me.BlinkTimer.Enabled = False
            Me.bool_1 = False
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing Then
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub FlowArrowMatrix2_Click(ByVal sender As Object, ByVal e As EventArgs)
            MyBase.Focus()
        End Sub

        Private Sub FlowArrowMatrix2_Resize(ByVal sender As Object, ByVal e As EventArgs)
            If (MyBase.Height < &H1A) Then
                MyBase.Height = &H1A
            End If
            If (MyBase.Width < &H1A) Then
                MyBase.Width = &H1A
            End If
            MyBase.Width = MyBase.Height
        End Sub

        Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
            Me.bool_1 = Not Me.bool_1
            MyBase.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal paintEventArgs_0 As PaintEventArgs)
            MyBase.OnPaint(paintEventArgs_0)
            paintEventArgs_0.Graphics.SmoothingMode = SmoothingMode.HighQuality
            paintEventArgs_0.Graphics.TranslateTransform(CSng((CDbl(MyBase.ClientRectangle.Width) / 2)), CSng((CDbl(MyBase.ClientRectangle.Height) / 2)))
            paintEventArgs_0.Graphics.RotateTransform(-CSng(Me.m_Angle))
            paintEventArgs_0.Graphics.TranslateTransform(CSng((CDbl((0 - MyBase.ClientRectangle.Width)) / 2)), CSng((CDbl((0 - MyBase.ClientRectangle.Height)) / 2)))
            Dim num As Integer = 0
            Do
                Dim num2 As Short = 0
                Do
                    Dim num3 As Long = CLng(Math.Round(Math.Pow(2, CDbl(((num * 7) + num2)))))
                    Dim rect As New Rectangle(Convert.ToInt32(CDbl((num2 * (CDbl(MyBase.Width) / 7)))), Convert.ToInt32(CDbl((num * (CDbl(MyBase.Width) / 7)))), Convert.ToInt32(CDbl(((CDbl(MyBase.Width) / 7) - 1))), Convert.ToInt32(CDbl(((CDbl(MyBase.Width) / 7) - 1))))
                    Dim path As New GraphicsPath
                    path.AddEllipse(rect)
                    Dim brush As New PathGradientBrush(path)
                    If ((CLng(Me.ulong_0) And num3) > 0) Then
                        rect.Inflate(-1, -1)
                        paintEventArgs_0.Graphics.FillEllipse(New SolidBrush(Color.Black), rect)
                        If Me.bool_1 Then
                            paintEventArgs_0.Graphics.DrawEllipse(New Pen(Color.FromArgb(&H2D, Me.color_0), 2.0!), rect)
                            brush.CenterColor = ControlPaint.DarkDark(Me.color_0)
                            brush.SurroundColors = New Color() {ControlPaint.Dark(Me.color_0)}
                        Else
                            paintEventArgs_0.Graphics.DrawEllipse(New Pen(Color.FromArgb(&HB9, Me.color_0), 2.0!), rect)
                            brush.CenterColor = ControlPaint.Light(Me.color_0)
                            brush.SurroundColors = New Color() {Color.FromArgb(&H19, ControlPaint.Light(Me.color_0))}
                        End If
                        paintEventArgs_0.Graphics.FillEllipse(brush, rect)
                    End If
                    num2 = CShort((num2 + 1))
                Loop While (num2 <= 6)
                num += 1
            Loop While (num <= 6)
            paintEventArgs_0.Graphics.ResetTransform
            If Not String.IsNullOrEmpty(Me.Text) Then
                Dim format As New StringFormat With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
                }
                Dim point As New Point(CInt(Math.Round(CDbl(Me.Width) / 2)), CInt(Math.Round(CDbl(Me.Height) / 2)))
                paintEventArgs_0.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), point, format)
            End If
        End Sub


        ' Properties
        Private Property BlinkTimer As Timer


        <RefreshProperties(RefreshProperties.All), Browsable(True), Description("The arrow LEDs color."), DefaultValue(GetType(Color), "DodgerBlue")> _
        Public Property ArrowLEDColor As Color
            Get
                Return Me.color_0
            End Get
            Set(ByVal value As Color)
                If (Me.color_0 <> value) Then
                    Me.color_0 = value
                    MyBase.Invalidate
                End If
            End Set
        End Property

        <RefreshProperties(RefreshProperties.All), Browsable(True), Description("Indicates whether there is a flow."), DefaultValue(False)> _
        Public Property Value As Boolean
            Get
                Return Me.bool_0
            End Get
            Set(ByVal value As Boolean)
                If (Me.bool_0 <> value) Then
                    Me.bool_0 = value
                    If Me.bool_0 Then
                        Me.BlinkTimer.Enabled = True
                    Else
                        Me.BlinkTimer.Enabled = False
                        Me.bool_1 = False
                    End If
                    MyBase.Invalidate

                    RaiseEvent ValueChanged(Me, EventArgs.Empty)
                End If
            End Set
        End Property

        <RefreshProperties(RefreshProperties.All), DefaultValue(500), Browsable(True), Description("LED blinking interval in milliseconds.")> _
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
                    MyBase.Invalidate
                End If
            End Set
        End Property

        <Description("The direction of the arrow."), Browsable(True), DefaultValue(&H13B), RefreshProperties(RefreshProperties.All)> _
        Public Property ArrowDirection As Angle
            Get
                Return Me.m_Angle
            End Get
            Set(ByVal value As Angle)
                Me.m_Angle = value
                MyBase.Invalidate
            End Set
        End Property

        Public Overrides Property [Text] As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                If (String.Compare(MyBase.Text, value, StringComparison.CurrentCulture) <> 0) Then
                    MyBase.Text = value
                    MyBase.Invalidate
                End If
            End Set
        End Property


        ' Fields
        Private ulong_0 As UInt64
        <AccessedThroughProperty("BlinkTimer"), CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)> _
        Private timer_0 As Timer
        Private color_0 As Color
        Private bool_0 As Boolean
        Private int_0 As Integer
        Public m_Angle As Angle
        Private bool_1 As Boolean

        ' Nested Types
        Public Enum Angle
            ' Fields
            E = 0
            NE = &H2D
            N = 90
            NW = &H87
            W = 180
            SW = &HE1
            S = 270
            SE = &H13B
        End Enum
    End Class
End Namespace

