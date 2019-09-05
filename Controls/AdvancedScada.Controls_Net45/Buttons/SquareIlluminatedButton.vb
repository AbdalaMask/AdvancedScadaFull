Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class SquareIlluminatedButton
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private rectangle_0 As Rectangle

    Private bool_0 As Boolean

    Private lightColorOption_0 As SquareIlluminatedButton.LightColorOption

    Private bool_1 As Boolean

    Private outputType_0 As OutputType

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bitmap_4 As Bitmap

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    Protected Overridable Property ErrorTimer As Timer

    Public Property LightColor As SquareIlluminatedButton.LightColorOption
        Get
            Return Me.lightColorOption_0
        End Get
        Set(ByVal value As SquareIlluminatedButton.LightColorOption)
            Me.lightColorOption_0 = value
            Me.method_0()
        End Set
    End Property

    Public Property OutputTypes As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    Public Property Value As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (Not value) Then
                Me.bitmap_0 = Me.bitmap_1
            Else
                Me.bitmap_0 = Me.bitmap_2
            End If
            Me.bool_1 = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.MouseDown, New MouseEventHandler(AddressOf Me.SquareIlluminatedButton_MouseDown)
        AddHandler MyBase.MouseUp, New MouseEventHandler(AddressOf Me.SquareIlluminatedButton_MouseUp)
        AddHandler MyBase.TextChanged, New EventHandler(AddressOf Me.SquareIlluminatedButton_TextChanged)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SquareIlluminatedButton_Resize)
        Me.lightColorOption_0 = SquareIlluminatedButton.LightColorOption.Green
        Me.outputType_0 = OutputType.MomentarySet
        Me.stringFormat_0 = New StringFormat()
        Me.ErrorTimer = New Timer()
        Me.stringFormat_0.Alignment = StringAlignment.Center
        Me.stringFormat_0.LineAlignment = StringAlignment.Center
        Me.solidBrush_0 = New SolidBrush(Color.Black)
        Me.rectangle_0 = New Rectangle()
    End Sub

    Private Sub method_0()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
            Select Case Me.lightColorOption_0
                Case SquareIlluminatedButton.LightColorOption.Red
                    graphic.DrawImage(My.Resources.SquareButtonRedOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
                    graphic1.DrawImage(My.Resources.SquareButtonRedOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                    Exit Select
                Case SquareIlluminatedButton.LightColorOption.Green
                    graphic.DrawImage(My.Resources.SquareButtonGreenOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
                    graphic1.DrawImage(My.Resources.SquareButtonGreenOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                    Exit Select
                Case SquareIlluminatedButton.LightColorOption.Blue
                    graphic.DrawImage(My.Resources.SquareButtonBlueOff, 0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
                    graphic1.DrawImage(My.Resources.SquareButtonBlueOn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                    Exit Select
            End Select
            Dim num As Decimal = New Decimal(CDbl(My.Resources.SquareButtonDown.Width) / CDbl(My.Resources.SquareButtonBlueOff.Width))
            Dim num1 As Decimal = New Decimal(CDbl(My.Resources.SquareButtonDown.Height) / CDbl(My.Resources.SquareButtonBlueOff.Height))
            Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
            graphic = Graphics.FromImage(Me.bitmap_3)
            graphic.DrawImage(My.Resources.SquareButtonDown, Convert.ToSingle(Decimal.Multiply(New Decimal(10L), num)), Convert.ToSingle(Decimal.Multiply(New Decimal(10L), num1)), Convert.ToSingle(Decimal.Multiply(New Decimal(Me.bitmap_3.Width), num)), Convert.ToSingle(Decimal.Multiply(New Decimal(Me.bitmap_3.Height), num1)))
            If (Not Me.bool_1) Then
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
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_4 IsNot Nothing And Me.bitmap_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_4)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
            If (Me.bool_0) Then
                graphic.DrawImage(Me.bitmap_3, 0, 0)
            End If
            painte.Graphics.DrawImage(Me.bitmap_4, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Private Sub SquareIlluminatedButton_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (MyBase.Enabled) Then
            Me.bool_0 = True
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub SquareIlluminatedButton_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Me.bool_0 = False
        MyBase.Invalidate()
    End Sub

    Private Sub SquareIlluminatedButton_Resize(ByVal sender As Object, ByVal e As EventArgs)
        Me.method_0()
    End Sub

    Private Sub SquareIlluminatedButton_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Invalidate()
    End Sub

    Public Enum LightColorOption
        Red
        Green
        Blue
    End Enum
End Class
