Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class Fan
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private float_0 As Single

    Private float_1 As Single

    Private stringFormat_0 As StringFormat

    Private rectangle_0 As Rectangle

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private outputType_0 As OutputType

    Private decimal_0 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Private matrix_0 As Matrix

    Private graphics_0 As Graphics

    Private point_0 As Point

    Private Property AnimateTimer As System.Windows.Forms.Timer


    Public Property Direction As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            Me.bool_1 = value
        End Set
    End Property

    Public Property OutputType As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    Public Property TextPosition As StringAlignment
        Get
            Return Me.stringFormat_0.LineAlignment
        End Get
        Set(ByVal value As StringAlignment)
            Me.stringFormat_0.LineAlignment = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Value As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_0) Then
                Me.bool_0 = value
                Me.AnimateTimer.Enabled = value And Not MyBase.DesignMode
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.outputType_0 = OutputType.MomentarySet
        Me.decimal_0 = New Decimal(Convert.ToInt32(CDbl(My.Resources.FanFrame.Height) / CDbl(My.Resources.FanFrame.Width)))
        Me.matrix_0 = New Matrix()
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center
        }
        Me.AnimateTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = 250
        }
    End Sub

    Private Sub method_0()
        If (Me.int_1 < MyBase.Height Or Me.int_0 < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_0)) Then
                MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_0))
            Else
                MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_0))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_0)) Then
            MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_0))
        Else
            MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_0))
        End If
        Me.int_0 = MyBase.Width
        Me.int_1 = MyBase.Height
        Me.method_1()
    End Sub

    Private Sub method_1()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.float_0 = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.FanFrame.Width)))
            Me.float_1 = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.FanFrame.Height)))
            Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CSng(My.Resources.FanCenterCap.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.FanCenterCap.Height) * Me.float_1))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            graphic.DrawImage(My.Resources.FanCenterCap, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(CSng(My.Resources.FanBlades.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.FanBlades.Height) * Me.float_1))
            graphic = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.FanBlades, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.FanFrame.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.FanFrame.Height) * Me.float_1))
            graphic = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.FanFrame, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
            Me.point_0 = New Point(Convert.ToInt32(CDbl(Me.bitmap_2.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_2.Height) / 2))
            Me.rectangle_0 = New Rectangle(0, Convert.ToInt32(10! * Me.float_1), MyBase.Width, MyBase.Height - Convert.ToInt32(20! * Me.float_1))
            Me.graphics_0 = Graphics.FromImage(Me.bitmap_2)
        End If
    End Sub

    Private Sub method_2(ByVal sender As Object, ByVal e As EventArgs)
        Me.AnimateTimer.Enabled = False
        If (Me.bitmap_2 IsNot Nothing And Me.graphics_0 IsNot Nothing) Then
            If (Not Me.bool_1) Then
                Me.matrix_0.RotateAt(20!, Me.point_0)
            Else
                Me.matrix_0.RotateAt(-20!, Me.point_0)
            End If
            Me.graphics_0.Transform = Me.matrix_0
            Me.graphics_0.Clear(Color.Transparent)
            Me.graphics_0.DrawImage(My.Resources.FanBlades, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            MyBase.Invalidate()
        End If
        Me.AnimateTimer.Enabled = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_1 IsNot Nothing And Me.bitmap_2 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            graphic.DrawImage(Me.bitmap_2, Convert.ToInt32(40! * Me.float_0), Convert.ToInt32(40! * Me.float_1))
            graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(151! * Me.float_0), Convert.ToInt32(151! * Me.float_1))
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                graphic.DrawString(Me.Text, Me.Font, solidBrush, Me.rectangle_0, Me.stringFormat_0)
            End Using
            painte.Graphics.DrawImage(Me.bitmap_3, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
