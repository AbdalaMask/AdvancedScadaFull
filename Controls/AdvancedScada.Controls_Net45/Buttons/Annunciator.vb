Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Annunciator
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private rectangle_0 As Rectangle

    Private solidBrush_0 As SolidBrush

    Private stringFormat_0 As StringFormat

    Private m_Value As Boolean

    Private bitmap_3 As Bitmap

    Public Property OutputTypes As OutputType

    Public Property Value As Boolean
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Boolean)
            If (Me.m_Value <> value) Then
                If (Not value) Then
                    Me.bitmap_0 = Me.bitmap_1
                Else
                    Me.bitmap_0 = Me.bitmap_2
                End If
                Me.m_Value = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.OutputTypes = OutputType.MomentarySet
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        Me.rectangle_0 = New Rectangle()
        Me.solidBrush_0 = New SolidBrush(Color.Black)
    End Sub

    Private Sub method_0()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.AnnunciatorOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            graphic1.DrawImage(My.Resources.AnnunciatorOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            If (Not Me.m_Value) Then
                Me.bitmap_0 = Me.bitmap_1
            Else
                Me.bitmap_0 = Me.bitmap_2
            End If
            graphic.Dispose()
            graphic1.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Y = 0
            Me.rectangle_0.Height = MyBase.Height
            Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If (MyBase.Enabled) Then
            Me.bitmap_0 = Me.bitmap_2
            If (Me.OutputTypes = OutputType.Toggle) Then
                Me.Value = Not Me.Value
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        If (MyBase.Enabled) Then
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_3 IsNot Nothing And Me.bitmap_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
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
