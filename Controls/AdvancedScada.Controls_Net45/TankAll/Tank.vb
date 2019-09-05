Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class Tank
    Inherits Control
    Private bitmap_0 As Bitmap

    Private rectangle_0 As Rectangle

    Private solidBrush_0 As SolidBrush

    Private stringFormat_0 As StringFormat

    Private stringFormat_1 As StringFormat

    Private rectangle_1 As Rectangle

    Private float_0 As Single

    Private decimal_0 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Private hatchBrush_0 As HatchBrush

    Private int_2 As Integer

    Private int_3 As Integer

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private float_1 As Single

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Shadows Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (value <> MyBase.ForeColor) Then
                MyBase.ForeColor = value
                If (Me.solidBrush_0 IsNot Nothing) Then
                    Me.solidBrush_0.Color = value
                Else
                    Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property MaxValue As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = CInt(Math.Round(Math.Ceiling(CDbl((value - Me.int_1)) / 10) * 10 + CDbl(Me.int_1)))
            Me.method_0()
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property MinValue As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            Me.int_1 = value
            Me.MaxValue = Me.int_0
            Me.method_0()
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property TankContentColor As Color
        Get
            Return Me.hatchBrush_0.BackgroundColor
        End Get
        Set(ByVal value As Color)
            If (Me.hatchBrush_0 IsNot Nothing) Then
                Me.hatchBrush_0.Dispose()
            End If
            Me.hatchBrush_0 = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(240, Math.Max(value.R - 40, 0), Math.Max(value.G - 40, 0), Math.Max(value.B - 40, 0)), value)
            MyBase.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Value As Single
        Get
            Return Me.float_0
        End Get
        Set(ByVal value As Single)
            If (value <> Me.float_0) Then
                Me.float_0 = Math.Max(Math.Min(value, CSng(Me.int_0)), CSng(Me.int_1))
                Me.method_0()
                MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.49)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.1)), CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.2)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.6))))
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.decimal_0
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_0 = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.Tank_Resize)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.stringFormat_1 = New StringFormat()
        Me.decimal_0 = Decimal.One
        Me.int_0 = 100
        Me.hatchBrush_0 = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Aqua)
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.BackColor = Color.Transparent
        Me.method_1()
    End Sub

    <DebuggerNonUserCode>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                Me.bitmap_0.Dispose()
                Me.bitmap_2.Dispose()
                Me.hatchBrush_0.Dispose()
                Me.stringFormat_1.Dispose()
                Me.stringFormat_0.Dispose()
                Me.solidBrush_0.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        Me.int_3 = CInt(Math.Round(CDbl(MyBase.Height) * 0.52))
        Me.int_2 = Convert.ToInt32((Me.float_0 * Convert.ToSingle(Me.decimal_0) - CSng(Me.int_1)) / CSng((Me.int_0 - Me.int_1)) * CSng(Me.int_3))
    End Sub

    Private Sub method_1()
        Dim width As Single = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.TankWithWindow.Width)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.TankWithWindow.Height)))
        Me.float_1 = CSng((CDbl(MyBase.Width) / CDbl(MyBase.Height)))
        Me.float_1 = 1!
        If (width >= height) Then
            Me.float_1 = height
        Else
            Me.float_1 = width
        End If
        If (Me.float_1 > 0!) Then
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(MyBase.Width), Convert.ToInt32(MyBase.Height))
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
                graphic.DrawImage(My.Resources.TankWithWindow, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
                Me.rectangle_0.X = 1
                Me.rectangle_0.Y = Convert.ToInt32(70! * Me.float_1 - 1!)
                Me.rectangle_0.Width = Me.bitmap_0.Width - 2
                Me.rectangle_0.Height = Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.1)
                Me.stringFormat_1.Alignment = StringAlignment.Center
                Me.stringFormat_1.LineAlignment = StringAlignment.Near
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
                If (Me.bitmap_2 IsNot Nothing) Then
                    Me.bitmap_2.Dispose()
                End If
                Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
            End Using
            Me.rectangle_1 = New Rectangle(Convert.ToInt32(361 * (CDbl(MyBase.Width) / CDbl(My.Resources.TankWithWindow.Width))), Convert.ToInt32(158 * (CDbl(MyBase.Height) / CDbl(My.Resources.TankWithWindow.Height)) + CDbl(Me.int_3)), Convert.ToInt32(68 * (CDbl(MyBase.Width) / CDbl(My.Resources.TankWithWindow.Width))), Me.int_3)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_2 Is Nothing) And Me.solidBrush_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            If (Me.bitmap_1 IsNot Nothing) Then
                graphics.DrawImageUnscaled(Me.bitmap_1, 0, 0)
            End If
            graphics.DrawImage(Me.bitmap_0, 0, 0)
            If (Me.hatchBrush_0 IsNot Nothing) Then
                graphics.FillRectangle(Me.hatchBrush_0, Me.rectangle_1.X, Me.rectangle_1.Y - Me.int_2, Me.rectangle_1.Width, Me.int_2)
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphics.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_1)
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        MyBase.OnPaintBackground(pevent)
        If (Me.BackColor = Color.Transparent AndAlso MyBase.Parent IsNot Nothing) Then
            Dim childIndex As Integer = MyBase.Parent.Controls.GetChildIndex(Me)
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height)
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
                Dim count As Integer = MyBase.Parent.Controls.Count - 1
                Dim num As Integer = childIndex + 1
                For i As Integer = count To num Step -1
                    Dim item As Control = MyBase.Parent.Controls(i)
                    If (If(Not item.Bounds.IntersectsWith(MyBase.Bounds), False, item.Visible)) Then
                        Using bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(item.Width, item.Height, pevent.Graphics)
                            item.DrawToBitmap(bitmap, item.ClientRectangle)
                            graphic.DrawImageUnscaled(bitmap, item.Left - MyBase.Left, item.Top - MyBase.Top)
                        End Using
                    End If
                Next

            End Using
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.bitmap_2 IsNot Nothing) Then
            Me.bitmap_2.Dispose()
            Me.bitmap_2 = Nothing
        End If
        MyBase.OnSizeChanged(e)
        If (MyBase.Parent IsNot Nothing) Then
            MyBase.Parent.Invalidate()
        End If
    End Sub

    Private Sub Tank_Resize(ByVal sender As Object, ByVal e As EventArgs)
        Me.method_0()
        Me.method_1()
    End Sub
End Class
