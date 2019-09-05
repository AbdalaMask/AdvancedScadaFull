Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Motor3Color
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private rotateFlipType_0 As RotateFlipType

    Private outputType_0 As OutputType

    Private bool_2 As Boolean

    Private bitmap_4 As Bitmap

    Private decimal_0 As Decimal

    Private decimal_1 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    Public Property OutputType As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    Public Property Rotation As RotateFlipType
        Get
            Return Me.rotateFlipType_0
        End Get
        Set(ByVal value As RotateFlipType)
            If (Me.rotateFlipType_0 <> value) Then
                Me.rotateFlipType_0 = value
                Me.bool_2 = True
                Me.method_0()
                Me.method_1()
            End If
        End Set
    End Property

    <Category("Appearance")>
    Public Property SelectColor2 As Boolean
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

    <Category("Appearance")>
    Public Property SelectColor3 As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_1) Then
                Me.bool_1 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Private Property tmrError As Timer

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipNone
        Me.outputType_0 = OutputType.MomentarySet
        Me.tmrError = New Timer()
        Me.decimal_1 = New Decimal(CDbl(My.Resources.MotorGray.Height) / CDbl(My.Resources.MotorGray.Width))
        Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
    End Sub

    Private Sub method_0()
        If (Not (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone)) Then
            Me.decimal_1 = New Decimal(CDbl(My.Resources.MotorGray.Height) / CDbl(My.Resources.MotorGray.Width))
        Else
            Me.decimal_1 = New Decimal(CDbl(My.Resources.MotorGray.Width) / CDbl(My.Resources.MotorGray.Height))
        End If
        If (Me.int_1 < MyBase.Height Or Me.int_0 < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_1)) Then
                MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_1))
            Else
                MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_1))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_1)) Then
            MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_1))
        Else
            MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_1))
        End If
        Me.int_0 = MyBase.Width
        Me.int_1 = MyBase.Height
        Me.method_1()
    End Sub

    Private Sub method_1()
        Dim num As Decimal
        Dim num1 As Decimal
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            If (Not (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone)) Then
                Me.decimal_1 = New Decimal(CDbl(My.Resources.MotorGray.Height) / CDbl(My.Resources.MotorGray.Width))
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.MotorGray.Width))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.MotorGray.Height))
            Else
                Me.decimal_1 = New Decimal(CDbl(My.Resources.MotorGray.Width) / CDbl(My.Resources.MotorGray.Height))
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.MotorGray.Height))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.MotorGray.Width))
            End If
            Me.decimal_0 = Math.Min(num, num1)
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Height), Me.decimal_0)))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.MotorGray, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Height), Me.decimal_0)))
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGray.Height), Me.decimal_0)))
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGreen.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorGreen.Height), Me.decimal_0)))
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorRed.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MotorRed.Height), Me.decimal_0)))
            graphic = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            Dim graphic2 As Graphics = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(My.Resources.MotorGray, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            graphic1.DrawImage(My.Resources.MotorGreen, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            graphic2.DrawImage(My.Resources.MotorRed, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
            Me.bitmap_1.RotateFlip(Me.rotateFlipType_0)
            Me.bitmap_2.RotateFlip(Me.rotateFlipType_0)
            Me.bitmap_0 = Me.bitmap_1
            If (Me.bool_0) Then
                Me.bitmap_0 = Me.bitmap_2
            ElseIf (Me.bool_1) Then
                Me.bitmap_0 = Me.bitmap_3
            End If
            graphic.Dispose()
            graphic1.Dispose()
            graphic2.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.95))
            Me.rectangle_0.Y = 0
            Me.rectangle_0.Height = MyBase.Height
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If (MyBase.Enabled) Then
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
        If (Me.bitmap_4 IsNot Nothing And Me.bitmap_1 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_4)
            Me.bitmap_0 = Me.bitmap_1
            If (Me.bool_1) Then
                Me.bitmap_0 = Me.bitmap_3
            ElseIf (Me.bool_0) Then
                Me.bitmap_0 = Me.bitmap_2
            End If
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(Me.Text)) Then
                If (Me.solidBrush_0.Color <> Me.ForeColor) Then
                    Me.solidBrush_0.Color = Me.ForeColor
                End If
                graphic.DrawString(Me.Text, Me.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
            painte.Graphics.DrawImage(Me.bitmap_4, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        If (Me.bool_2) Then
            MyBase.OnPaintBackground(pevent)
            Me.bool_2 = False
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub
End Class
