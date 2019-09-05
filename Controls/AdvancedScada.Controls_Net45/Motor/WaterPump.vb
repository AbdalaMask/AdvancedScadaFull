Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class WaterPump
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private float_0 As Single

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private stringFormat_1 As StringFormat

    Private stringFormat_2 As StringFormat

    Private stringFormat_3 As StringFormat

    Private bool_0 As Boolean

    Private bitmap_2 As Bitmap

    Private solidBrush_0 As SolidBrush

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Shadows Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (MyBase.ForeColor <> value) Then
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

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
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
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.WaterPump_Resize)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.stringFormat_1 = New StringFormat()
        Me.stringFormat_2 = New StringFormat()
        Me.stringFormat_3 = New StringFormat()
        Me.method_0()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                Me.solidBrush_0.Dispose()
                Me.bitmap_2.Dispose()
                Me.bitmap_0.Dispose()
                Me.bitmap_1.Dispose()
                Me.stringFormat_0.Dispose()
                Me.stringFormat_1.Dispose()
                Me.stringFormat_2.Dispose()
                Me.stringFormat_3.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        Dim width As Single = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.RSCorPumpOff.Width)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.RSCorPumpOff.Height)))
        If (width >= height) Then
            Me.float_0 = height
        Else
            Me.float_0 = width
        End If
        If (Me.float_0 > 0!) Then
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.RSCorPumpOff.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.RSCorPumpOff.Height) * Me.float_0))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.RSCorPumpOff, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CSng(My.Resources.RSCorPumpOn.Width) * Me.float_0), Convert.ToInt32(CSng(My.Resources.RSCorPumpOn.Height) * Me.float_0))
            graphic = Graphics.FromImage(Me.bitmap_1)
            graphic.DrawImage(My.Resources.RSCorPumpOn, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            graphic.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Y = 0
            Me.rectangle_0.Width = Me.bitmap_0.Width
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.99))
            Me.stringFormat_1.Alignment = StringAlignment.Center
            Me.stringFormat_1.LineAlignment = StringAlignment.Far
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            If (Me.bitmap_2 IsNot Nothing) Then
                Me.bitmap_2.Dispose()
            End If
            Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_0 IsNot Nothing And Me.solidBrush_0 IsNot Nothing) Then
            If (Me.bitmap_2 Is Nothing) Then
                Me.bitmap_2 = New Bitmap(MyBase.ClientSize.Width, MyBase.ClientSize.Height)
            End If
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_2)
            If (Not Me.bool_0) Then
                graphic.DrawImage(Me.bitmap_0, 0, 0)
            Else
                graphic.DrawImage(Me.bitmap_1, 0, 0)
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_1)
            End If
            painte.Graphics.DrawImageUnscaled(Me.bitmap_2, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.bitmap_2 IsNot Nothing) Then
            Me.bitmap_2.Dispose()
            Me.bitmap_2 = Nothing
        End If
        Me.method_0()
        MyBase.OnSizeChanged(e)
    End Sub

    Private Sub WaterPump_Resize(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
End Class
