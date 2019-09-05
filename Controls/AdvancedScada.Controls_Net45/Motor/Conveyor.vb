Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Conveyor
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private rectangle_0 As Rectangle

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private m_Value As Boolean

    Private m_Rotation As RotateFlipType

    Private m_OutputType As OutputType

    Private bool_1 As Boolean

    Private bitmap_3 As Bitmap

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
            Return Me.m_OutputType
        End Get
        Set(ByVal value As OutputType)
            Me.m_OutputType = value
        End Set
    End Property

    Public Property Rotation As RotateFlipType
        Get
            Return Me.m_Rotation
        End Get
        Set(ByVal value As RotateFlipType)
            If (Me.m_Rotation <> value) Then
                Me.m_Rotation = value
                Me.bool_1 = True
                Me.method_0()
                Me.method_1()
            End If
        End Set
    End Property

    Private Property tmrError As System.Windows.Forms.Timer

    Public Property Value As Boolean
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.m_Value) Then
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
        Me.rectangle_0 = New Rectangle()
        Me.m_Rotation = RotateFlipType.RotateNoneFlipNone
        Me.m_OutputType = OutputType.MomentarySet
        Me.tmrError = New System.Windows.Forms.Timer()
        Me.decimal_1 = New Decimal(CDbl(My.Resources.ConveyorOff.Height) / CDbl(My.Resources.ConveyorOff.Width))
        Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Far
        }
    End Sub

    Private Sub method_0()
        If (Not (Me.m_Rotation = RotateFlipType.Rotate90FlipNone Or Me.m_Rotation = RotateFlipType.Rotate270FlipNone Or Me.m_Rotation = RotateFlipType.Rotate90FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipX Or Me.m_Rotation = RotateFlipType.Rotate90FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipNone Or Me.m_Rotation = RotateFlipType.Rotate90FlipNone)) Then
            Me.decimal_1 = New Decimal(CDbl(My.Resources.ConveyorOff.Height) / CDbl(My.Resources.ConveyorOff.Width))
        Else
            Me.decimal_1 = New Decimal(CDbl(My.Resources.ConveyorOff.Width) / CDbl(My.Resources.ConveyorOff.Height))
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
        Me.bool_1 = True
        Me.method_1()
    End Sub

    Private Sub method_1()
        Dim num As Decimal
        Dim num1 As Decimal
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            If (Not (Me.m_Rotation = RotateFlipType.Rotate90FlipNone Or Me.m_Rotation = RotateFlipType.Rotate270FlipNone Or Me.m_Rotation = RotateFlipType.Rotate90FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipX Or Me.m_Rotation = RotateFlipType.Rotate90FlipX Or Me.m_Rotation = RotateFlipType.Rotate270FlipNone Or Me.m_Rotation = RotateFlipType.Rotate90FlipNone)) Then
                Me.decimal_1 = New Decimal(CDbl(My.Resources.ConveyorOff.Height) / CDbl(My.Resources.ConveyorOff.Width))
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.ConveyorOff.Width))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.ConveyorOff.Height))
            Else
                Me.decimal_1 = New Decimal(CDbl(My.Resources.ConveyorOff.Width) / CDbl(My.Resources.ConveyorOff.Height))
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.ConveyorOff.Height))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.ConveyorOff.Width))
            End If
            Me.decimal_0 = Math.Min(num, num1)
            Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Height), Me.decimal_0)))
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            graphic.DrawImage(My.Resources.ConveyorOff, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Height), Me.decimal_0)))
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOff.Height), Me.decimal_0)))
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOn.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.ConveyorOn.Height), Me.decimal_0)))
            graphic = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.ConveyorOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            graphic1.DrawImage(My.Resources.ConveyorOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
            Me.bitmap_2.RotateFlip(Me.m_Rotation)
            Me.bitmap_1.RotateFlip(Me.m_Rotation)
            If (Not Me.m_Value) Then
                Me.bitmap_0 = Me.bitmap_1
            Else
                Me.bitmap_0 = Me.bitmap_2
            End If
            graphic.Dispose()
            graphic1.Dispose()
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.95))
            Me.rectangle_0.Y = CInt(Math.Round(CDbl(MyBase.Height) * 0.47))
            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.5))
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
            If (Me.m_OutputType = OutputType.Toggle) Then
                Me.Value = Not Me.Value
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        If (MyBase.Enabled) Then
            If (Me.m_OutputType <> OutputType.Toggle) Then
                Me.bitmap_0 = Me.bitmap_1
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_3 IsNot Nothing And Me.bitmap_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(Me.Text)) Then
                If (Me.solidBrush_0.Color <> Me.ForeColor) Then
                    Me.solidBrush_0.Color = Me.ForeColor
                End If
                graphic.DrawString(Me.Text, Me.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
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
        Me.method_0()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler(Of EventArgs)

End Class
