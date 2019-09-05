Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

Public Class VerticalSlider
    Inherits Control
    Private int_0 As Integer

    Private double_0 As Double

    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private double_1 As Double

    Private double_2 As Double

    Protected m_Value As Double

    Private bool_0 As Boolean

    Private int_1 As Integer

    Public Property MajorTickMarks As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (Me.int_1 <> value) Then
                Me.int_1 = Math.Min(Math.Max(value, 2), 25)
                Me.method_1()
            End If
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            Me.double_2 = value
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            Me.double_1 = value
        End Set
    End Property

    Public Property ShowTickMarks As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                Me.method_1()
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
        Me.double_2 = 100
        Me.int_1 = 4
        MyBase.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If (Me.bitmap_0 IsNot Nothing) Then
            Me.bitmap_0.Dispose()
        End If
    End Sub

    Private Sub method_0(ByVal int_2 As Integer)
        Dim height As Double = (CDbl((MyBase.Height - int_2)) - CDbl(Me.int_0) / 2) / CDbl((MyBase.Height - Me.int_0)) * (Me.double_2 - Me.double_1) + Me.double_1
        height = Math.Min(Me.double_2, height)
        height = Math.Max(Me.double_1, height)
        If (height <> Me.m_Value) Then
            Me.m_Value = height
            Me.OnValueChangedWithSlider(EventArgs.Empty)
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub method_1()
        If (MyBase.Height > 0 And MyBase.Width > 0) Then
            Me.int_0 = CInt(Math.Round(CDbl(MyBase.Height) / 20))
            Me.double_0 = CDbl(MyBase.Width)
            If (Me.bool_0) Then
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
            Dim linearGradientBrush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 5)), 0), Color.FromArgb(255, 128, 128, 128), Color.FromArgb(255, 220, 220, 220))
            Dim colorBlend As System.Drawing.Drawing2D.ColorBlend = New System.Drawing.Drawing2D.ColorBlend() With
            {
                .Colors = New Color() {Color.FromArgb(255, 220, 220, 220), Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 220, 220, 220)},
                .Positions = New Single() {Nothing, 0.5!, 1!}
            }
            linearGradientBrush.InterpolationColors = colorBlend
            Dim num As Integer = CInt(Math.Round(CDbl(MyBase.Width) / 2))
            If (Me.bool_0) Then
                num = CInt(Math.Round(CDbl(MyBase.Width) / 3))
            End If
            graphic.FillRectangle(linearGradientBrush, CInt(Math.Round(CDbl(num) - Me.double_0 / 10)), CInt(Math.Round(CDbl(Me.int_0) / 2)), CInt(Math.Round(Me.double_0 / 5)), MyBase.Height - Me.int_0)
            If (Me.bool_0) Then
                Dim height As Double = CDbl((MyBase.Height - Me.int_0)) / CDbl((Me.int_1 - 1))
                Dim int1 As Integer = Me.int_1 - 1
                For i As Integer = 0 To int1 Step 1
                    graphic.DrawLine(New Pen(Me.ForeColor), CSng((CDbl(MyBase.Width) * 0.7)), CSng((CDbl(i) * height + CDbl(Me.int_0) / 2)), CSng(MyBase.Width), CSng((CDbl(i) * height + CDbl(Me.int_0) / 2)))
                Next

            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.method_1()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.method_1()
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
        Dim mValue As Double = 0
        MyBase.OnPaint(painte)
        If (Me.bitmap_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(Me.bitmap_1, 0, 0)
            If (Me.int_0 > 0) Then
                If (Me.double_2 - Me.double_1 > 0 And MyBase.Height - Me.int_0 > 0) Then
                    mValue = (Me.m_Value - Me.double_1) / (Me.double_2 - Me.double_1) * CDbl((MyBase.Height - Me.int_0))
                End If
                ButtonRenderer.DrawButton(graphic, New Rectangle(0, CInt(Math.Round(CDbl((MyBase.Height - Me.int_0)) - mValue)), CInt(Math.Round(Me.double_0)), Me.int_0), PushButtonState.Normal)
            End If
            painte.Graphics.DrawImage(Me.bitmap_0, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_1()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnValueChangedWithSlider(ByVal e As EventArgs)
        RaiseEvent ValueChangedWithSlider(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler


    Public Event ValueChangedWithSlider As EventHandler

End Class
