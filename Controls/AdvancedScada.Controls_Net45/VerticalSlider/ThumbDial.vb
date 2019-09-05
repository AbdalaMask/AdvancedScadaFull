Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class ThumbDial
    Inherits Control
    Private int_0 As Integer

    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap()

    Private bitmap_2 As Bitmap

    Private point_0 As Point

    Private point_1 As Point

    Private double_0 As Double

    Private double_1 As Double

    Protected m_Value As Double

    Private double_2 As Double

    Private double_3 As Double

    Private int_1 As Integer

    Public Property Increment As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            Me.double_2 = value
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            Me.double_1 = value
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            Me.double_0 = value
        End Set
    End Property

    Public Property Sensitivity As Double
        Get
            Return Me.double_3
        End Get
        Set(ByVal value As Double)
            Me.double_3 = Math.Max(value, 0.1)
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
        ReDim Me.bitmap_1(2)
        Me.double_1 = 100
        Me.double_2 = 1
        Me.double_3 = 1
        MyBase.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Private Sub method_0()
        If (MyBase.Height > 0 And MyBase.Width > 0) Then
            Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
            Math.Min(CDbl(MyBase.Width) / CDbl(My.Resources.ThumbDialFrame1.Width), CDbl(MyBase.Height) / CDbl(My.Resources.ThumbDialFrame1.Height))
            Dim num As Integer = CInt(Math.Round(CDbl(MyBase.Width) * 0.58))
            Dim num1 As Integer = CInt(Math.Round(CDbl(MyBase.Height) * 0.45))
            Me.point_0 = New Point(CInt(Math.Round(CDbl(MyBase.Width) * 0.21)), CInt(Math.Round(CDbl(MyBase.Height) * 0.2)))
            Me.bitmap_1(0) = New Bitmap(num, num1)
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1(0))
            graphic.DrawImage(My.Resources.ThumbDialFrame1, 0, 0, num, num1)
            Me.bitmap_1(1) = New Bitmap(num, num1)
            graphic = Graphics.FromImage(Me.bitmap_1(1))
            graphic.DrawImage(My.Resources.ThumbDialFrame2, 0, 0, num, num1)
            Me.bitmap_1(2) = New Bitmap(num, num1)
            graphic = Graphics.FromImage(Me.bitmap_1(2))
            graphic.DrawImage(My.Resources.ThumbDialFrame3, 0, 0, num, num1)
            Me.point_1 = New Point(0, 0)
            Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
            graphic = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.ThumbDialBase, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
        End If
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)


    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.Clear(Me.BackColor)
            graphic.DrawImage(Me.bitmap_2, Me.point_1)
            If (If(Me.int_0 > 2, False, Me.bitmap_1(Me.int_0) IsNot Nothing)) Then
                graphic.DrawImageUnscaled(Me.bitmap_1(Me.int_0), Me.point_0)
            End If
            painte.Graphics.DrawImage(Me.bitmap_0, 0, 0)
        End Using
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
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
