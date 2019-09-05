Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace FlowArrow
    Public Class FlowArrowMatrix1
        Inherits Control
        ' Events
        Public Event ValueChanged As EventHandler

        ' Methods
        Public Sub New()
            AddHandler MyBase.Click, New EventHandler(AddressOf Me.FlowArrowMatrix1_Click)
            AddHandler MyBase.Resize, New EventHandler(AddressOf Me.FlowArrowMatrix1_Resize)
            Me.ulong_0 = New UInt64(5 - 1) {}
            Me.color_0 = Color.Red
            Me.arrowDirectionOption_0 = ArrowDirectionOption.Down
            Me.int_0 = 500
            MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or (ControlStyles.SupportsTransparentBackColor Or (ControlStyles.ResizeRedraw Or (ControlStyles.UserPaint Or ControlStyles.ContainerControl))))), True)
            MyBase.DoubleBuffered = True
            Me.DoubleBuffered = True
            MyBase.BackColor = Color.Transparent
            MyBase.Size = New Size(&H57, &H79)
            Me.ulong_0(0) = Convert.ToUInt64("00100010100010100010001000100010001", 2)
            Me.ulong_0(1) = Convert.ToUInt64("10001000100010001000101000101000100", 2)
            Me.ulong_0(2) = Convert.ToUInt64("10001010100010000000100010101000100", 2)
            Me.ulong_0(3) = Convert.ToUInt64("00100010101000100000001000101010001", 2)
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

        Private Sub FlowArrowMatrix1_Click(ByVal sender As Object, ByVal e As EventArgs)
            MyBase.Focus()
        End Sub

        Private Sub FlowArrowMatrix1_Resize(ByVal sender As Object, ByVal e As EventArgs)
            If ((Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Right) OrElse (Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Left)) Then
                If (MyBase.Width < &H19) Then
                    MyBase.Width = &H19
                End If
                MyBase.Height = CInt(Math.Round(CDbl((0.7154471! * MyBase.Width))))
                If (MyBase.Height < &H12) Then
                    MyBase.Height = &H12
                End If
            Else
                If (MyBase.Height < &H19) Then
                    MyBase.Height = &H19
                End If
                MyBase.Width = CInt(Math.Round(CDbl((0.7154471! * MyBase.Height))))
                If (MyBase.Width < &H12) Then
                    MyBase.Width = &H12
                End If
            End If
        End Sub

        Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
            Me.bool_1 = Not Me.bool_1
            MyBase.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal paintEventArgs_0 As PaintEventArgs)
            Dim num As Integer
            Dim num2 As Integer
            Dim num3 As Integer
            Dim num4 As Integer
            MyBase.OnPaint(paintEventArgs_0)
            paintEventArgs_0.Graphics.SmoothingMode = SmoothingMode.HighQuality
            If ((Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Right) OrElse (Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Left)) Then
                num = 4
                num2 = 6
                num3 = 7
                num4 = CInt(Math.Round(CDbl((num3 + 0.25))))
            Else
                num = 6
                num2 = 4
                num3 = 5
                num4 = CInt(Math.Round(CDbl((num3 + 0.25))))
            End If
            Dim num5 As Integer = num
            Dim i As Integer = 0
            Do While (i <= num5)
                Dim num7 As Short = CShort(num2)
                Dim j As Short = 0
                Do While (j <= num7)
                    Dim num9 As Long = CLng(Math.Round(Math.Pow(2, CDbl(((i * num3) + j)))))
                    Dim rect As New Rectangle(Convert.ToInt32(CDbl((j * (CDbl(MyBase.Width) / CDbl(num3))))), Convert.ToInt32(CDbl((i * (CDbl(MyBase.Width) / CDbl(num4))))), Convert.ToInt32(CDbl(((CDbl(MyBase.Width) / CDbl(num3)) - 1))), Convert.ToInt32(CDbl(((CDbl(MyBase.Width) / CDbl(num3)) - 1))))
                    Using path As GraphicsPath = New GraphicsPath
                        path.AddEllipse(rect)
                        Dim brush As New PathGradientBrush(path)
                        If ((CLng(Me.ulong_0(CInt(Me.arrowDirectionOption_0))) And num9) > 0) Then
                            rect.Inflate(-1, -1)
                            Using brush2 As SolidBrush = New SolidBrush(Color.Black)
                                paintEventArgs_0.Graphics.FillEllipse(brush2, rect)
                            End Using
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
                    End Using
                    j = CShort((j + 1))
                Loop
                i += 1
            Loop
            If Not String.IsNullOrEmpty(Me.Text) Then
                Dim format As New StringFormat With { _
                    .Alignment = StringAlignment.Center, _
                    .LineAlignment = StringAlignment.Center _
                }
                paintEventArgs_0.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(Math.Round(Width / 2), Math.Round(Height / 2)), format)
            End If
        End Sub


        ' Properties
        Private Property BlinkTimer As Timer
            Get
                Return _BlinkTimer
            End Get
            Set
                _BlinkTimer = Value
            End Set
        End Property

        <DefaultValue(GetType(Color), "Red"), Description("The arrow LEDs color."), Browsable(True), RefreshProperties(RefreshProperties.All)>
        Public Property ArrowLEDColor As Color
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

        <Description("Select arrow direction (Right, Left, Up, Down)."), DefaultValue(3), Browsable(True), RefreshProperties(RefreshProperties.All)>
        Public Property ArrowDirection As ArrowDirectionOption
            Get
                Return Me.arrowDirectionOption_0
            End Get
            Set(ByVal value As ArrowDirectionOption)
                If (Me.arrowDirectionOption_0 <> value) Then
                    Me.float_0 = MyBase.Width
                    Me.float_1 = MyBase.Height
                    If ((((Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Right) OrElse (Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Left)) AndAlso ((value <> ArrowDirectionOption.Right) AndAlso (value <> ArrowDirectionOption.Left))) OrElse (((Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Up) OrElse (Me.arrowDirectionOption_0 = FlowArrow.FlowArrowMatrix1.ArrowDirectionOption.Down)) AndAlso ((value <> ArrowDirectionOption.Up) AndAlso (value <> ArrowDirectionOption.Down)))) Then
                        Me.arrowDirectionOption_0 = value
                        MyBase.Width = CInt(Math.Round(CDbl(Me.float_1)))
                        MyBase.Height = CInt(Math.Round(CDbl(Me.float_0)))
                        Me.FlowArrowMatrix1_Resize(Me, Nothing)
                    Else
                        Me.arrowDirectionOption_0 = value
                    End If
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Indicates whether there is a flow."), DefaultValue(False)>
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
                    MyBase.Invalidate()

                    RaiseEvent ValueChanged(Me, EventArgs.Empty)

                End If
            End Set
        End Property

        <Browsable(True), Description("The arrow LED blinking interval in milliseconds."), DefaultValue(500), RefreshProperties(RefreshProperties.All)>
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

        <Browsable(True), Description("Constant value used to maintain the ratio between width and height."), RefreshProperties(RefreshProperties.All)>
        Public ReadOnly Property SizeScaleCoefficient As Single
            Get
                Return 0.7154471!
            End Get
        End Property

        Public Overrides Property [Text] As String
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


        ' Fields
        Private ulong_0 As UInt64()
        <CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never), AccessedThroughProperty("BlinkTimer")>
        Private timer_0 As Timer
        Private color_0 As Color
        Private float_0 As Single
        Private float_1 As Single
        Private arrowDirectionOption_0 As ArrowDirectionOption
        Private bool_0 As Boolean
        Private int_0 As Integer
        Private bool_1 As Boolean
        Private _BlinkTimer As Timer

        ' Nested Types
        Public Enum ArrowDirectionOption
            ' Fields
            Right = 0
            Left = 1
            Up = 2
            Down = 3
        End Enum
    End Class
End Namespace

