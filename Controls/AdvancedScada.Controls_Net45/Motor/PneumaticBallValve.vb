Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class PneumaticBallValve
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private rectangle_0 As Rectangle

    Private decimal_0 As Decimal

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bitmap_3 As Bitmap

    Private bool_0 As Boolean

    Private rotateFlipType_0 As RotateFlipType

    Private outputType_0 As OutputType

    Private bool_1 As Boolean

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
            Me.rotateFlipType_0 = value
            Me.bool_1 = True
            Me.method_0()
        End Set
    End Property

    Private Property tmrError As Timer

    Public Property Value As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Not value) Then
                Me.bitmap_0 = Me.bitmap_1
            Else
                Me.bitmap_0 = Me.bitmap_2
            End If
            Me.bool_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipNone
        Me.outputType_0 = OutputType.MomentarySet
        Me.tmrError = New Timer()
        Me.decimal_1 = New Decimal(CDbl(My.Resources.PneumaticBallValveOff.Height) / CDbl(My.Resources.PneumaticBallValveOff.Width))
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub method_0()
        Dim num As Decimal = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.PneumaticBallValveOff.Width))
        Dim num1 As Decimal = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.PneumaticBallValveOff.Height))
        If (Decimal.Compare(num, num1) >= 0) Then
            Me.decimal_0 = num1
        Else
            Me.decimal_0 = num
        End If
        If (Decimal.Compare(Me.decimal_0, Decimal.Zero) > 0) Then
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Height), Me.decimal_0)))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.PneumaticBallValveOff, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Height), Me.decimal_0)))
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOff.Height), Me.decimal_0)))
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOn.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.PneumaticBallValveOn.Height), Me.decimal_0)))
            graphic = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.PneumaticBallValveOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            graphic1.DrawImage(My.Resources.PneumaticBallValveOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            Me.bitmap_1.RotateFlip(Me.rotateFlipType_0)
            Me.bitmap_2.RotateFlip(Me.rotateFlipType_0)
            If (Not Me.bool_0) Then
                Me.bitmap_0 = Me.bitmap_1
            Else
                Me.bitmap_0 = Me.bitmap_2
            End If
            graphic.Dispose()
            graphic1.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.95))
            Me.rectangle_0.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.55))
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.45))
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            If (Me.bitmap_3 IsNot Nothing) Then
                Me.bitmap_3.Dispose()
            End If
            Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If (MyBase.Enabled) Then
            Me.bitmap_0 = Me.bitmap_2
            If (Me.outputType_0 = OutputType.Toggle) Then
                Me.Value = Not Me.Value
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        If (MyBase.Enabled) Then
            If (Me.OutputType <> OutputType.Toggle) Then
                Me.bitmap_0 = Me.bitmap_1
            End If
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
        If (Me.bool_1) Then
            MyBase.OnPaintBackground(pevent)
            Me.bool_1 = False
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
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
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub
End Class
