Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class BasicSelectorSwitch
    Inherits ButtonBase
    Private outputType_0 As OutputType

    Public Property OutputType As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.MouseDown, New MouseEventHandler(AddressOf Me.BasicSelectorSwitch_MouseDown)
        AddHandler MyBase.MouseUp, New MouseEventHandler(AddressOf Me.BasicSelectorSwitch_MouseUp)
        AddHandler MyBase.TextChanged, New EventHandler(AddressOf Me.BasicSelectorSwitch_TextChanged)
        Me.outputType_0 = OutputType.MomentarySet
    End Sub

    Protected Overrides Sub AdjustSize()
    End Sub

    Private Sub BasicSelectorSwitch_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (MyBase.Enabled) Then
            If (Me.outputType_0 = OutputType.Toggle) Then
                Me.m_Value = Not Me.m_Value
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub BasicSelectorSwitch_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (MyBase.Enabled) Then
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub BasicSelectorSwitch_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub CreateStaticImage()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Dim num As Integer = Math.Max(MyBase.Width, MyBase.Height)
            Using bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(num, num)
                Using matrix As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                    Dim graphic As Graphics = Graphics.FromImage(bitmap)
                    matrix.RotateAt(-25!, New PointF(CSng(MyBase.Width) / 2!, CSng(MyBase.Height) / 2!))
                    graphic.Transform = matrix
                    graphic.FillRectangle(Brushes.DarkGray, Convert.ToInt32(CDbl(MyBase.Width) / 2 - CDbl(MyBase.Width) * 0.1), Convert.ToInt32(CDbl(MyBase.Height) * 0.1), Convert.ToInt32(CDbl(MyBase.Width) * 0.2), Convert.ToInt32(CDbl(MyBase.Height) * 0.8))
                    graphic.DrawRectangle(Pens.Red, 0, 0, num - 1, num - 1)
                    Me.OffImage = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height)
                    Me.OnImage = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height)
                    Dim graphic1 As Graphics = Graphics.FromImage(Me.OffImage)
                    Dim graphic2 As Graphics = Graphics.FromImage(Me.OnImage)
                    graphic1.FillEllipse(Brushes.Gray, 0, 0, MyBase.Width - 1, MyBase.Height - 1)
                    graphic1.DrawEllipse(Pens.White, 0, 0, MyBase.Width - 1, MyBase.Height - 1)
                    graphic1.DrawImage(bitmap, 0, 0, MyBase.Width, MyBase.Height)
                    graphic1.Dispose()
                    graphic2.Dispose()
                End Using
            End Using
            If (Me.BackBuffer IsNot Nothing) Then
                Me.BackBuffer.Dispose()
            End If
            Me.BackBuffer = New System.Drawing.Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.BackBuffer IsNot Nothing And Me.OnImage IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.BackBuffer)
            If (Not Me.m_Value) Then
                graphic.DrawImage(Me.OffImage, 0, 0)
            Else
                graphic.DrawImage(Me.OnImage, 0, 0)
            End If
            painte.Graphics.DrawImage(Me.BackBuffer, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub
End Class
