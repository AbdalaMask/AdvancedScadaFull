Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class VerticalSlider3
    Inherits Control
    Private int_0 As Integer

    Private double_0 As Double

    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private _Minimum As Double

    Private _Maximum As Double

    Protected m_Value As Double

    Private _Increment As Double

    Private _ShowTickMarks As Boolean

    Private _MajorTickMarks As Integer

    Private _MinorTickMarks As Integer

    Private _BorderColor As Color

    Public Property BorderColor As Color
        Get
            Return Me._BorderColor
        End Get
        Set(ByVal value As Color)
            If (value <> Me._BorderColor) Then
                Me._BorderColor = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Increment As Double
        Get
            Return Me._Increment
        End Get
        Set(ByVal value As Double)
            Me._Increment = value
        End Set
    End Property

    Public Property MajorTickMarks As Integer
        Get
            Return Me._MajorTickMarks
        End Get
        Set(ByVal value As Integer)
            If (Me._MajorTickMarks <> value) Then
                Me._MajorTickMarks = Math.Min(Math.Max(value, 2), 25)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me._Maximum
        End Get
        Set(ByVal value As Double)
            Me._Maximum = value
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me._Minimum
        End Get
        Set(ByVal value As Double)
            Me._Minimum = value
        End Set
    End Property

    Public Property MinorTickMarks As Integer
        Get
            Return Me._MinorTickMarks
        End Get
        Set(ByVal value As Integer)
            If (Me._MinorTickMarks <> value) Then
                Me._MinorTickMarks = Math.Min(Math.Max(value, 0), 10)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property ShowTickMarks As Boolean
        Get
            Return Me._ShowTickMarks
        End Get
        Set(ByVal value As Boolean)
            If (Me._ShowTickMarks <> value) Then
                Me._ShowTickMarks = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If (Me.m_Value <> value) Then
                Me.m_Value = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._Maximum = 100
        Me._Increment = 0.1
        Me._MajorTickMarks = 5
        Me._MinorTickMarks = 4
        Me._BorderColor = Color.Transparent
        MyBase.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        Me.bitmap_0.Dispose()
        If (Me.bitmap_1 IsNot Nothing) Then
            Me.bitmap_1.Dispose()
        End If
    End Sub

    Private Sub method_0(ByVal int_3 As Integer)
        Dim height As Double = (CDbl((MyBase.Height - int_3)) - CDbl(Me.int_0) / 2) / CDbl((MyBase.Height - Me.int_0)) * (Me._Maximum - Me._Minimum) + Me._Minimum
        height = Math.Min(Me._Maximum, height)
        height = Math.Max(Me._Minimum, height)
        If (Me._Increment > 0) Then
            height = CDbl(Convert.ToInt32(height / Me._Increment)) * Me._Increment
        End If
        If (height <> Me.m_Value) Then
            Me.m_Value = height
            Me.OnValueChangedByMouse(EventArgs.Empty)
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub CreateStaticImage()
        If (MyBase.Height > 0 And MyBase.Width > 0) Then
            Me.double_0 = CDbl(MyBase.Width)
            If (Me._ShowTickMarks) Then
                Me.double_0 = CDbl(MyBase.Width) * 0.6667
            End If
            Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            If ((Me.BackColor <> Color.Transparent) Or MyBase.Parent Is Nothing) Then
                graphic.Clear(Me.BackColor)
            ElseIf (MyBase.Parent IsNot Nothing) Then
                graphic.Clear(MyBase.Parent.BackColor)
            End If
            Dim height As Double = CDbl(MyBase.Height) / CDbl(My.Resources.AnalogSliderSlot.Height)
            Dim width As Double = CDbl(My.Resources.AnalogSliderSlot.Width) * height
            graphic.DrawImage(My.Resources.AnalogSliderSlot, CInt(Math.Round(CDbl(MyBase.Width) / 2 - width / 2)), 0, CInt(Math.Round(width)), MyBase.Height)
            Me.double_0 = CDbl(CInt(Math.Round(CDbl(My.Resources.VerticalSlider.Width) * height)))
            Me.int_0 = CInt(Math.Round(CDbl(My.Resources.VerticalSlider.Height) * height))
            If (Me._ShowTickMarks) Then
                Dim num As Integer = CInt(Math.Round((CDbl(MyBase.Width) - width * 3) / 2))
                If (num <= 0) Then
                    num = 1
                End If
                Dim num1 As Integer = CInt(Math.Round(Math.Max(CDbl(num) / 2, 1)))
                Dim height1 As Double = CDbl((MyBase.Height - Me.int_0)) / CDbl((Me._MajorTickMarks - 1))
                Dim int1 As Integer = Me._MajorTickMarks - 1
                For i As Integer = 0 To int1 Step 1
                    Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.ForeColor)
                        graphic.DrawLine(pen, 0!, CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2)), CSng(num), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2)))
                        graphic.DrawLine(pen, CSng((MyBase.Width - num)), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2)), CSng(MyBase.Width), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2)))
                    End Using
                    If (Me._MinorTickMarks > 0 And i < Me._MajorTickMarks - 1) Then
                        Dim int2 As Double = height1 / CDbl((Me._MinorTickMarks + 1))
                        Dim int21 As Integer = Me._MinorTickMarks
                        For j As Integer = 0 To int21 Step 1
                            Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(Me.ForeColor)
                                graphic.DrawLine(pen1, 0!, CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2 + CDbl(j) * int2)), CSng(num1), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2 + CDbl(j) * int2)))
                                graphic.DrawLine(pen1, CSng((MyBase.Width - num1)), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2 + CDbl(j) * int2)), CSng(MyBase.Width), CSng((CDbl(i) * height1 + CDbl(Me.int_0) / 2 + CDbl(j) * int2)))
                            End Using
                        Next

                    End If
                Next

            End If
            If (Me._BorderColor <> Color.Transparent) Then
                Using pen2 As System.Drawing.Pen = New System.Drawing.Pen(Me._BorderColor)
                    graphic.DrawRectangle(pen2, 0, 0, MyBase.Width - 1, MyBase.Height - 1)
                End Using
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Me.method_0(e.Y)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim location As Point = e.Location
        If (e.Button = System.Windows.Forms.MouseButtons.Left And location.Y > -10 And e.Location.X > -10) Then
            Me.method_0(e.Y)
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim double1 As Double = 0
        MyBase.OnPaint(painte)
        If (Me.bitmap_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(Me.bitmap_1, 0, 0)
            If (Me.int_0 > 0) Then
                Dim num As Double = Math.Max(Math.Min(Me.m_Value, Me._Maximum), Me._Minimum)
                If (Me._Maximum - Me._Minimum > 0 And MyBase.Height - Me.int_0 > 0) Then
                    double1 = (num - Me._Minimum) / (Me._Maximum - Me._Minimum) * CDbl((MyBase.Height - Me.int_0))
                End If
                graphic.DrawImage(My.Resources.VerticalSlider, New Rectangle(CInt(Math.Round((CDbl(MyBase.Width) - Me.double_0) / 2)), CInt(Math.Round(CDbl((MyBase.Height - Me.int_0)) - double1)), CInt(Math.Round(Me.double_0)), Me.int_0))
            End If
            painte.Graphics.DrawImage(Me.bitmap_0, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)

        RaiseEvent ValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnValueChangedByMouse(ByVal e As EventArgs)

        RaiseEvent ValueChangedByMouse(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler


    Public Event ValueChangedByMouse As EventHandler

End Class
