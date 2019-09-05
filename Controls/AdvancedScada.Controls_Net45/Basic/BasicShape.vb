
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.Timers
Imports System.Windows.Forms
Public Class BasicShape
    Inherits Control
    Private icontainer_0 As IContainer

    Private graphicsPath_0 As GraphicsPath

    Private _Connector As ConnecterType

    Private graphicsPath_1 As GraphicsPath

    Private bool_0 As Boolean

    Private int_0 As Integer

    Private color_0 As Color

    Private bool_1 As Boolean

    Private int_1 As Integer

    Private dashStyle_0 As DashStyle

    Private timer_0 As System.Windows.Forms.Timer

    Private timer_1 As System.Windows.Forms.Timer

    Private timer_2 As System.Timers.Timer

    Private color_1 As Color

    Private color_2 As Color

    Private color_3 As Color

    Private color_4 As Color

    Private collection_0 As Collection(Of ShapesStateValue)

    Private string_0 As String

    Private bool_2 As Boolean

    Private _Blink As Boolean

    Private _BlinkRate As Integer

    Private _Direction As LineDirection

    Private _AnimateBorder As Boolean

    Private bool_5 As Boolean

    Private _BackColor As Color

    Private bool_6 As Boolean

    Private color_6 As Color

    Private color_7 As Color

    Private color_8 As Color

    Private _BorderWidth As Integer

    Private color_9 As Color

    Private _BorderColor As Color

    Private _BorderStyle As DashStyle

    Private shapeType_0 As ShapeType

    Private int_4 As Integer

    Private methodInvoker_0 As MethodInvoker

    <Category("Shape")>
    <Description("Causes the control border to animate")>
    Public Property AnimateBorder As Boolean
        Get
            Return Me._AnimateBorder
        End Get
        Set(ByVal value As Boolean)
            Me._AnimateBorder = value
            If (Not Me._AnimateBorder) Then
                Me._BorderWidth = Me.int_0
                Me._BorderColor = Me.color_0
                Me.BorderStyle = Me.dashStyle_0
            Else
                Me.dashStyle_0 = Me._BorderStyle
                Me.int_0 = Me._BorderWidth
                Me.color_0 = Me._BorderColor
                If (Me._BorderWidth = 0) Then
                    Me._BorderWidth = 3
                End If
                Dim r As Integer = Me._BorderColor.R
                Dim g As Integer = Me._BorderColor.G
                Dim b As Integer = Me._BorderColor.B
                Me._BorderColor = Color.FromArgb(255, r, g, b)
            End If
            Me.timer_1.Enabled = Me._AnimateBorder
        End Set
    End Property

    <Category("Shape")>
    <Description("Back Color")>
    Public Shadows Property BackColor As Color
        Get
            Return Me._BackColor
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
            Me._BackColor = value
        End Set
    End Property

    <Category("Shape")>
    <Description("Causes the control to blink, disabled during design mode")>
    Public Property Blink As Boolean
        Get
            Return Me._Blink
        End Get
        Set(ByVal value As Boolean)
            Me._Blink = value
            If (Me.timer_2 IsNot Nothing) Then
                Me.timer_2.Enabled = Me._Blink
            End If
            If (Not Me._Blink) Then
                MyBase.Visible = Me.bool_2
            End If
        End Set
    End Property

    <Description("Blink rate in milliseconds , 1 second = 1000 milliseconds")>
    Public Property BlinkRate As Integer
        Get
            Return Me._BlinkRate
        End Get
        Set(ByVal value As Integer)
            If (Me._BlinkRate <> value) Then
                Me._BlinkRate = value
                Me.method_1()
            End If
        End Set
    End Property

    <Category("Shape")>
    <Description("Border Color")>
    Public Property BorderColor As Color
        Get
            Return Me._BorderColor
        End Get
        Set(ByVal value As Color)
            If (Me._BorderColor <> value) Then
                Me._BorderColor = value
                Me.color_9 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Shape")>
    <Description("Border Style")>
    Public Property BorderStyle As DashStyle
        Get
            Return Me._BorderStyle
        End Get
        Set(ByVal value As DashStyle)
            Me._BorderStyle = value
            MyBase.Invalidate()
        End Set
    End Property

    <Category("Shape")>
    <Description("Border Width")>
    Public Property BorderWidth As Integer
        Get
            Return Me._BorderWidth
        End Get
        Set(ByVal value As Integer)
            Me._BorderWidth = value
            If (Me._BorderWidth < 0) Then
                Me._BorderWidth = 0
            End If
            Me.OnResize(Nothing)
        End Set
    End Property

    <Category("Shape")>
    <Description("Connect at")>
    Public Property Connector As ConnecterType
        Get
            Return Me._Connector
        End Get
        Set(ByVal value As ConnecterType)
            Me._Connector = value
        End Set
    End Property

    <Category("Shape")>
    <Description("For Directional Line")>
    Public Property Direction As LineDirection
        Get
            Return Me._Direction
        End Get
        Set(ByVal value As LineDirection)
            Me._Direction = value
            Me.OnResize(Nothing)
        End Set
    End Property

    <Category("Shape")>
    <Description("Color at center")>
    Public Property GradientInsideColor As Color
        Get
            Return Me.color_6
        End Get
        Set(ByVal value As Color)
            Me.color_6 = value
            MyBase.Invalidate()
        End Set
    End Property

    <Category("Shape")>
    <Description("Color at the edges of the Shape")>
    Public Property GradientOutsideColor As Color
        Get
            Return Me.color_8
        End Get
        Set(ByVal value As Color)
            If (Me.color_8 <> value) Then
                Me.color_8 = value
                Me.color_7 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public ReadOnly Property Outline As GraphicsPath
        Get
            Return Me.graphicsPath_1
        End Get
    End Property

    <Browsable(True)>
    <Category("Shape")>
    <Description("Select Shape")>
    <Editor(GetType(ShapeTypeEditor), GetType(UITypeEditor))>
    Public Property Shape As ShapeType
        Get
            Return Me.shapeType_0
        End Get
        Set(ByVal value As ShapeType)
            Me.shapeType_0 = value
            If (If(Me.shapeType_0 = ShapeType.LineVertical OrElse Me.shapeType_0 = ShapeType.LineHorizontal OrElse Me.shapeType_0 = ShapeType.LineUp, True, Me.shapeType_0 = ShapeType.LineDown)) Then
                Me.ForeColor = Color.FromArgb(0, 255, 255, 255)
            End If
            If (Me.shapeType_0 = ShapeType.LineVertical) Then
                MyBase.Width = 20
            End If
            If (Me.shapeType_0 = ShapeType.LineHorizontal) Then
                MyBase.Height = 20
            End If
            Me.OnResize(Nothing)
        End Set
    End Property

    <Description("State collections")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property States As Collection(Of ShapesStateValue)
        Get
            Return Me.collection_0
        End Get
    End Property

    <Browsable(False)>
    <Category("Shape")>
    <Description("Text to display")>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    <Category("Shape")>
    <Description("Using Gradient to fill Shape")>
    Public Property UseGradient As Boolean
        Get
            Return Me.bool_6
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_6 <> value) Then
                Me.bool_6 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Description("In designer, set this value to test, otherwise this value is automatic")>
    Public Property Value As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(value, Me.string_0, False) <> 0 AndAlso value IsNot Nothing) Then
                Me.string_0 = value
                Me.method_1()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Shape")>
    <Description("Causes the control to vibrate")>
    Public Property Vibrate As Boolean
        Get
            Return Me.bool_5
        End Get
        Set(ByVal value As Boolean)
            Me.bool_5 = value
            Me.timer_0.Enabled = Me.bool_5
            If (Not Me.bool_5 AndAlso Me.bool_0) Then
                MyBase.Top = MyBase.Top + 5
                Me.bool_0 = False
            End If
        End Set
    End Property

    Public Shadows Property Visible As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_2) Then
                Me.bool_2 = value
                If (Not Me._Blink) Then
                    MyBase.Visible = value
                End If
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.graphicsPath_0 = New GraphicsPath()
        Me._Connector = ConnecterType.Center
        Me.graphicsPath_1 = New GraphicsPath()
        Me.int_0 = 3
        Me.color_0 = Color.Red
        Me.bool_1 = False
        Me.int_1 = 0
        Me.dashStyle_0 = DashStyle.Solid
        Me.bool_2 = True
        Me._BlinkRate = 500
        Me._Direction = LineDirection.None
        Me._AnimateBorder = False
        Me.bool_6 = False
        Me.color_6 = Color.FromArgb(100, 255, 0, 0)
        Me.color_8 = Color.FromArgb(100, 0, 255, 255)
        Me._BorderWidth = 3
        Me._BorderColor = Color.FromArgb(255, 255, 0, 0)
        Me._BorderStyle = DashStyle.Solid
        Me.shapeType_0 = ShapeType.Rectangle
        Me.methodInvoker_0 = New MethodInvoker(AddressOf Me.method_5)
        Me.collection_0 = New Collection(Of ShapesStateValue)()
        Me.method_0()
        Me.DoubleBuffered = True
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer, True)
        Me.BackColor = Color.LightGray
        Me.BorderColor = Color.Black
        Me.BorderWidth = 1
        MyBase.Width = 100
        MyBase.Height = 100
        Me.Text = String.Empty
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing) Then
            If (Me.icontainer_0 IsNot Nothing) Then
                Me.icontainer_0.Dispose()
            End If
            If (Me.graphicsPath_0 IsNot Nothing) Then
                Me.graphicsPath_0.Dispose()
            End If
            If (Me.graphicsPath_1 IsNot Nothing) Then
                Me.graphicsPath_1.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub method_0()
        Me.icontainer_0 = New System.ComponentModel.Container()
        Me.timer_0 = New System.Windows.Forms.Timer(Me.icontainer_0)
        Me.timer_1 = New System.Windows.Forms.Timer(Me.icontainer_0)
        MyBase.SuspendLayout()
        Me.timer_0.Interval = 200
        AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
        AddHandler Me.timer_1.Tick, New EventHandler(AddressOf Me.timer_1_Tick)
        MyBase.ResumeLayout(False)
    End Sub

    Private Sub method_1()
        If (Me.collection_0.Count > 0) Then
            Dim flag As Boolean = False
            Dim string0 As String = Me.string_0
            If (Operators.CompareString(string0, "True", False) = 0) Then
                string0 = Conversions.ToString(1)
            ElseIf (Operators.CompareString(string0, "False", False) = 0) Then
                string0 = Conversions.ToString(0)
            End If
            Dim count As Integer = Me.collection_0.Count - 1
            Dim num As Integer = 0
            Do
                If (Operators.CompareString(string0, Me.collection_0(num).Value.ToString(), False) = 0) Then
                    Me.int_4 = num
                    flag = True
                End If
                num = num + 1
            Loop While num <= count
            If (Not flag) Then
                If (Me.timer_2 IsNot Nothing) Then
                    Me.timer_2.[Stop]()
                End If
                Me.method_3()
            Else
                Me.method_2()
            End If
        Else
            Me.method_3()
        End If
    End Sub

    Private Sub method_2()
        If (Not Me.collection_0(Me.int_4).StateBlink) Then
            If (Me.collection_0(Me.int_4).StateBackColor.A <> 0 Or Me.collection_0(Me.int_4).StateBackColor.R <> 0 Or Me.collection_0(Me.int_4).StateBackColor.G <> 0 Or Me.collection_0(Me.int_4).StateBackColor.B <> 0) Then
                If (Not Me.bool_6) Then
                    MyBase.BackColor = Me.collection_0(Me.int_4).StateBackColor
                Else
                    Me.color_7 = Me.collection_0(Me.int_4).StateBackColor
                End If
            End If
            If (Me.collection_0(Me.int_4).StateBorderColor = Color.Empty) Then
                Me.color_9 = Me.BorderColor
            Else
                Me.color_9 = Me.collection_0(Me.int_4).StateBorderColor
            End If
        End If
        If (Me.collection_0(Me.int_4).StateBackColor <> Color.Empty) Then
            Me.color_3 = Me.collection_0(Me.int_4).StateBackColor
        ElseIf (Not Me.bool_6) Then
            Me.color_3 = Me._BackColor
        Else
            Me.color_3 = Me.color_8
        End If
        If (Me.collection_0(Me.int_4).StateBackColorBlink <> Color.Empty) Then
            Me.color_4 = Me.collection_0(Me.int_4).StateBackColorBlink
        ElseIf (Not Me.bool_6) Then
            Me.color_4 = Me._BackColor
        Else
            Me.color_4 = Me.color_8
        End If
        If (Me.collection_0(Me.int_4).StateBorderColor = Color.Empty) Then
            Me.color_1 = Me.BorderColor
        Else
            Me.color_1 = Me.collection_0(Me.int_4).StateBorderColor
        End If
        If (Me.collection_0(Me.int_4).StateBorderColorBlink = Color.Empty) Then
            Me.color_2 = Me.BorderColor
        Else
            Me.color_2 = Me.collection_0(Me.int_4).StateBorderColorBlink
        End If
        If (Not MyBase.DesignMode) Then
            If (Me.collection_0(Me.int_4).StateBlink) Then
                If (Me.timer_2 Is Nothing) Then
                    Me.timer_2 = New System.Timers.Timer() With
                    {
                        .Interval = CDbl(Me._BlinkRate)
                    }
                    AddHandler Me.timer_2.Elapsed, New ElapsedEventHandler(AddressOf Me.timer_2_Elapsed)
                End If
                Me.timer_2.Start()
            ElseIf (Me.timer_2 IsNot Nothing) Then
                Me.timer_2.[Stop]()
            End If
        End If
    End Sub

    Private Sub method_3()
        If (Not Me.bool_6) Then
            MyBase.BackColor = Me._BackColor
        Else
            Me.color_7 = Me.color_8
        End If
        Me.color_9 = Me.BorderColor
    End Sub

    Private Sub method_4(ByVal sender As Object, ByVal e As EventArgs)
        If ((Me.color_3 <> Color.Empty) Or (Me.color_4 <> Color.Empty)) Then
            If (MyBase.BackColor <> Me.color_3) Then
                MyBase.BackColor = Me.color_3
            Else
                MyBase.BackColor = Me.color_4
            End If
        End If
        If ((Me.color_1 <> Color.Empty) Or (Me.color_2 <> Color.Empty)) Then
            If (Me.color_9 <> Me.color_1) Then
                Me.color_9 = Me.color_1
            Else
                Me.color_9 = Me.color_2
            End If
        End If
    End Sub

    Private Sub method_5()
        MyBase.Visible = Not MyBase.Visible
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If (Me.timer_2 Is Nothing) Then
            Me.timer_2 = New System.Timers.Timer() With
            {
                .Interval = CDbl(Me._BlinkRate)
            }
            AddHandler Me.timer_2.Elapsed, New ElapsedEventHandler(AddressOf Me.timer_2_Elapsed)
            Me.timer_2.Enabled = Me._Blink
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        If (Not MyBase.DesignMode) Then
            Me.Value = Conversions.ToString(Conversions.ToInteger(Me.string_0) - 1)
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim int1 As Single()
        If (painte IsNot Nothing) Then
            If (Me.bool_6) Then
                Try
                    Using pathGradientBrush As System.Drawing.Drawing2D.PathGradientBrush = New System.Drawing.Drawing2D.PathGradientBrush(Me.graphicsPath_1)
                        pathGradientBrush.CenterColor = Me.color_6
                        pathGradientBrush.SurroundColors = New Color() {Me.color_7}
                        painte.Graphics.FillPath(pathGradientBrush, Me.graphicsPath_1)
                    End Using
                Catch exception As System.Exception
                    ProjectData.SetProjectError(exception)
                    ProjectData.ClearProjectError()
                End Try
            End If
            If (Me._BorderWidth > 0) Then
                Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_9, CSng((Me._BorderWidth * 2)))
                    If (If(Me.Shape = ShapeType.LineDown OrElse Me.Shape = ShapeType.LineUp OrElse Me.Shape = ShapeType.LineHorizontal, True, Me.Shape = ShapeType.LineVertical)) Then
                        pen.StartCap = LineCap.Round
                        pen.EndCap = LineCap.Round
                        If (Me._Direction <> LineDirection.None) Then
                            Dim shape As ShapeType = Me.Shape
                            If (CInt(shape) - CInt(ShapeType.LineDown) > CInt(ShapeType.Diamond)) Then
                                If (shape = ShapeType.LineVertical) Then
                                    If (If(Me._Direction = LineDirection.LeftUp, False, Me._Direction <> LineDirection.RightUp)) Then
                                        pen.EndCap = LineCap.ArrowAnchor
                                    Else
                                        pen.StartCap = LineCap.ArrowAnchor
                                    End If
                                End If
                            ElseIf (If(Me._Direction = LineDirection.LeftUp, False, Me._Direction <> LineDirection.LeftDown)) Then
                                pen.EndCap = LineCap.ArrowAnchor
                            Else
                                pen.StartCap = LineCap.ArrowAnchor
                            End If
                        End If
                    End If
                    pen.DashStyle = Me._BorderStyle
                    If (pen.DashStyle = DashStyle.Custom) Then
                        pen.DashPattern = New Single() {1.0!, 1.0!, 1.0!, 1.0!}
                    End If
                    If (Me.AnimateBorder) Then
                        pen.DashStyle = DashStyle.Custom
                        Me.int_1 = Math.Max(Interlocked.Increment(Me.int_1), Me.int_1 - 1) Mod 10
                        Dim pen1 As System.Drawing.Pen = pen
                        If (Me.bool_1) Then
                            int1 = New Single() {1.0!, 0!, 1.0!, 1.0!, 1.0!}
                            int1(1) = 0.01! + 0.05! * CSng(Me.int_1)
                        Else
                            int1 = New Single() {1.0!, 1.0!, 1.0!, 1.0!}
                        End If
                        pen1.DashPattern = int1
                    End If
                    painte.Graphics.SmoothingMode = SmoothingMode.HighQuality
                    painte.Graphics.DrawPath(pen, Me.graphicsPath_1)
                End Using
            End If
            MyBase.OnPaint(painte)
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        If (MyBase.Width < 2 * Me._BorderWidth) Then
            MyBase.Width = 2 * Me._BorderWidth
        ElseIf (MyBase.Height >= 2 * Me._BorderWidth) Then
            Me.graphicsPath_1 = New System.Drawing.Drawing2D.GraphicsPath()
            BasicShape.smethod_0(Me.graphicsPath_1, Me.shapeType_0, MyBase.Width, MyBase.Height, Me.BorderWidth)
            If (Me.graphicsPath_1 IsNot Nothing) Then
                If (If(Me.Shape = ShapeType.LineDown OrElse Me.Shape = ShapeType.LineUp OrElse Me.Shape = ShapeType.LineHorizontal OrElse Me.Shape = ShapeType.LineVertical, False, Me.Shape <> ShapeType.Rectangle)) Then
                    MyBase.Region = New System.Drawing.Region(Me.graphicsPath_1)
                Else
                    Dim num As Integer = If(Me.Shape = ShapeType.Rectangle, 1, Me._BorderWidth)
                    Dim graphicsPath As System.Drawing.Drawing2D.GraphicsPath = DirectCast(Me.graphicsPath_1.Clone(), System.Drawing.Drawing2D.GraphicsPath)
                    Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_9, CSng(num))
                        pen.StartCap = LineCap.Round
                        pen.EndCap = LineCap.Round
                        If (Me._Direction <> LineDirection.None) Then
                            Dim shape As ShapeType = Me.Shape
                            If (CInt(shape) - CInt(ShapeType.LineDown) > CInt(ShapeType.Diamond)) Then
                                If (shape = ShapeType.LineVertical) Then
                                    If (If(Me._Direction = LineDirection.LeftUp, False, Me._Direction <> LineDirection.RightUp)) Then
                                        pen.EndCap = LineCap.ArrowAnchor
                                    Else
                                        pen.StartCap = LineCap.ArrowAnchor
                                    End If
                                End If
                            ElseIf (If(Me._Direction = LineDirection.LeftUp, False, Me._Direction <> LineDirection.LeftDown)) Then
                                pen.EndCap = LineCap.ArrowAnchor
                            Else
                                pen.StartCap = LineCap.ArrowAnchor
                            End If
                        End If
                        Try
                            graphicsPath.Widen(pen)
                        Catch exception As System.Exception
                            ProjectData.SetProjectError(exception)
                            ProjectData.ClearProjectError()
                        End Try
                    End Using
                    If (Me.Shape = ShapeType.Rectangle) Then
                        Me.graphicsPath_1.AddPath(graphicsPath, True)
                        Me.graphicsPath_1.CloseAllFigures()
                        MyBase.Region = New System.Drawing.Region(Me.graphicsPath_1)
                    Else
                        MyBase.Region = New System.Drawing.Region(graphicsPath)
                        Using region As System.Drawing.Region = New System.Drawing.Region(Me.graphicsPath_1)
                            MyBase.Region.Union(region)
                        End Using
                    End If
                End If
            End If
            MyBase.Invalidate()
            MyBase.OnResize(e)
        Else
            MyBase.Height = 2 * Me._BorderWidth
        End If
    End Sub

    Protected Overridable Sub OnValueChanged()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub

    Friend Shared Sub smethod_0(ByRef graphicsPath_2 As GraphicsPath, ByVal shapeType_1 As ShapeType, ByVal int_5 As Integer, ByVal int_6 As Integer, ByVal int_7 As Integer)
        Select Case shapeType_1
            Case ShapeType.Rectangle
                graphicsPath_2.AddRectangle(New RectangleF(0!, 0!, CSng(int_5), CSng(int_6)))
                Exit Select
            Case ShapeType.RoundedRectangle
                graphicsPath_2.AddArc(0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 180.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0!, 0!, CSng(int_5) - CSng(int_5) / 8.0!, 0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 270.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5), CSng(int_5) / 8.0!, CSng(int_5), CSng(int_6) - CSng(int_5) / 8.0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6), CSng(int_5) / 8.0!, CSng(int_6))
                graphicsPath_2.AddArc(0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 90.0!, 90.0!)
                graphicsPath_2.AddLine(0!, CSng(int_6) - CSng(int_5) / 8.0!, 0!, CSng(int_5) / 8.0!)
                Exit Select
            Case ShapeType.Diamond
                graphicsPath_2.AddPolygon(New PointF() {New PointF(0!, CSng(int_6) / 2.0!), New PointF(CSng(int_5) / 2.0!, 0!), New PointF(CSng(int_5), CSng(int_6) / 2.0!), New PointF(CSng(int_5) / 2.0!, CSng(int_6))})
                Exit Select
            Case ShapeType.Ellipse
                graphicsPath_2.AddEllipse(0!, 0!, CSng(int_5), CSng(int_6))
                Exit Select
            Case ShapeType.TriangleUp
                graphicsPath_2.AddPolygon(New PointF() {New PointF(0!, CSng(int_6)), New PointF(CSng(int_5), CSng(int_6)), New PointF(CSng(int_5) / 2.0!, 0!)})
                Exit Select
            Case ShapeType.TriangleDown
                graphicsPath_2.AddPolygon(New PointF() {New PointF(0!, 0!), New PointF(CSng(int_5), 0!), New PointF(CSng((int_5 / 2)), CSng(int_6))})
                Exit Select
            Case ShapeType.TriangleLeft
                graphicsPath_2.AddPolygon(New PointF() {New PointF(CSng(int_5), 0!), New PointF(0!, CSng(int_6) / 2.0!), New PointF(CSng(int_5), CSng(int_6))})
                Exit Select
            Case ShapeType.TriangleRight
                graphicsPath_2.AddPolygon(New PointF() {New PointF(0!, 0!), New PointF(CSng(int_5), CSng(int_6) / 2.0!), New PointF(0!, CSng(int_6))})
                Exit Select
            Case ShapeType.BalloonNE
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6), CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6))
                graphicsPath_2.AddArc(0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 90.0!, 90.0!)
                graphicsPath_2.AddLine(0!, CSng(int_6) - CSng(int_5) / 8.0!, 0!, CSng(int_6) * 0.25! + CSng(int_5) / 8.0!)
                graphicsPath_2.AddArc(0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 180.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0!, CSng(int_6) * 0.25!, 5.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddLine(5.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.25!, 3.0! * CSng(int_5) / 4.0!, 0!)
                graphicsPath_2.AddLine(3.0! * CSng(int_5) / 4.0!, 0!, 3.0! * CSng(int_5) / 4.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddLine(3.0! * CSng(int_5) / 4.0!, CSng(int_6) * 0.25!, CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 270.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5), CSng(int_5) / 8.0! + CSng(int_6) * 0.25!, CSng(int_5), CSng(int_6) - CSng(int_5) / 8.0!)
                Exit Select
            Case ShapeType.BalloonNW
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6), CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6))
                graphicsPath_2.AddArc(0!, CSng(int_6) - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 90.0!, 90.0!)
                graphicsPath_2.AddLine(0!, CSng(int_6) - CSng(int_5) / 8.0!, 0!, CSng(int_6) * 0.25! + CSng(int_5) / 8.0!)
                graphicsPath_2.AddArc(0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 180.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddLine(CSng(int_5) / 4.0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, 0!)
                graphicsPath_2.AddLine(CSng(int_5) / 4.0!, 0!, 3.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddLine(3.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.25!, CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6) * 0.25!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.25!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 270.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5), CSng(int_5) / 8.0! + CSng(int_6) * 0.25!, CSng(int_5), CSng(int_6) - CSng(int_5) / 8.0!)
                Exit Select
            Case ShapeType.BalloonSW
                graphicsPath_2.AddArc(0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 180.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0!, 0!, CSng(int_5) - CSng(int_5) / 8.0!, 0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 270.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5), CSng(int_5) / 8.0!, CSng(int_5), CSng(int_6) * 0.75! - CSng(int_5) / 8.0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.75! - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6) * 0.75!, CSng(int_5) / 8.0! + CSng(int_5) / 4.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0! + CSng(int_5) / 4.0!, CSng(int_6) * 0.75!, CSng(int_5) / 8.0! + CSng(int_5) / 8.0!, CSng(int_6))
                graphicsPath_2.AddLine(CSng(int_5) / 8.0! + CSng(int_5) / 8.0!, CSng(int_6), CSng(int_5) / 8.0! + CSng(int_5) / 8.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0! + CSng(int_5) / 8.0!, CSng(int_6) * 0.75!, CSng(int_5) / 8.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddArc(0!, CSng(int_6) * 0.75! - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 90.0!, 90.0!)
                graphicsPath_2.AddLine(0!, CSng(int_6) * 0.75! - CSng(int_5) / 8.0!, 0!, CSng(int_5) / 8.0!)
                Exit Select
            Case ShapeType.BalloonSE
                graphicsPath_2.AddArc(0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 180.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) / 8.0!, 0!, CSng(int_5) - CSng(int_5) / 8.0!, 0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, 0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 270.0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5), CSng(int_5) / 8.0!, CSng(int_5), CSng(int_6) * 0.75! - CSng(int_5) / 8.0!)
                graphicsPath_2.AddArc(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.75! - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 0!, 90.0!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 8.0!, CSng(int_6) * 0.75!, CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6) * 0.75!, CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6))
                graphicsPath_2.AddLine(CSng(int_5) - CSng(int_5) / 4.0!, CSng(int_6), CSng(int_5) - 3.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddLine(CSng(int_5) - 3.0! * CSng(int_5) / 8.0!, CSng(int_6) * 0.75!, CSng(int_5) / 8.0!, CSng(int_6) * 0.75!)
                graphicsPath_2.AddArc(0!, CSng(int_6) * 0.75! - CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, CSng(int_5) / 4.0!, 90.0!, 90.0!)
                graphicsPath_2.AddLine(0!, CSng(int_6) * 0.75! - CSng(int_5) / 8.0!, 0!, CSng(int_5) / 8.0!)
                Exit Select
            Case ShapeType.CustomPolygon
                graphicsPath_2.AddPolygon(New PointF() {New PointF(0!, 0!), New PointF(CSng(int_5) / 2.0!, CSng(int_6) / 4.0!), New PointF(CSng(int_5), 0!), New PointF(CSng(int_5) * 3.0! / 4.0!, CSng(int_6) / 2.0!), New PointF(CSng(int_5), CSng(int_6)), New PointF(CSng(int_5) / 2.0!, CSng(int_6) * 3.0! / 4.0!), New PointF(0!, CSng(int_6)), New PointF(CSng(int_5) / 4.0!, CSng(int_6) / 2.0!)})
                Exit Select
            Case ShapeType.CustomPie
                graphicsPath_2.AddPie(0, 0, int_5, int_6, 180.0!, 270.0!)
                Exit Select
            Case ShapeType.LineDown
                graphicsPath_2.AddLine(New PointF(CSng(int_7), CSng(int_7)), New PointF(CSng((int_5 - int_7)), CSng((int_6 - int_7))))
                Exit Select
            Case ShapeType.LineUp
                graphicsPath_2.AddLine(New PointF(CSng(int_7), CSng((int_6 - int_7))), New PointF(CSng((int_5 - int_7)), CSng(int_7)))
                Exit Select
            Case ShapeType.LineHorizontal
                graphicsPath_2.AddLine(New PointF(CSng(int_7), CSng(int_6) / 2.0!), New PointF(CSng((int_5 - int_7)), CSng(int_6) / 2.0!))
                Exit Select
            Case ShapeType.LineVertical
                graphicsPath_2.AddLine(New PointF(CSng(int_5) / 2.0!, CSng(int_7)), New PointF(CSng(int_5) / 2.0!, CSng((int_6 - int_7))))
                Exit Select
        End Select
    End Sub

    Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.bool_5) Then
            Me.bool_0 = Not Me.bool_0
            MyBase.Top = If(Me.bool_0, MyBase.Top - 5, MyBase.Top + 5)
        End If
    End Sub

    Private Sub timer_1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Invalidate()
        Me.bool_1 = Not Me.bool_1
    End Sub

    Private Sub timer_2_Elapsed(ByVal sender As Object, ByVal e As EventArgs)
        If (Not MyBase.DesignMode) Then
            Try
                If (Me.bool_2) Then
                    MyBase.Invoke(Me.methodInvoker_0)
                ElseIf (MyBase.Visible) Then
                    MyBase.Invoke(Me.methodInvoker_0)
                End If
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                ProjectData.ClearProjectError()
            End Try
        End If
    End Sub

    Public Event ValueChanged As EventHandler

End Class