Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<DefaultEvent("Click")>
Public Class MushroomButton
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private rectangle_0 As Rectangle

    Private decimal_0 As Decimal

    Private legendPlates_0 As MushroomButton.LegendPlates

    Private outputTypes_0 As MushroomButton.OutputTypes

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private bitmap_4 As Bitmap

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
            MyBase.ForeColor = value
            Me.solidBrush_0.Color = value
            Me.method_1()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property LegendPlate As MushroomButton.LegendPlates
        Get
            Return Me.legendPlates_0
        End Get
        Set(ByVal value As MushroomButton.LegendPlates)
            Me.legendPlates_0 = value
            Me.method_0()
        End Set
    End Property

    Public Property OutputType As MushroomButton.OutputTypes
        Get
            Return Me.outputTypes_0
        End Get
        Set(ByVal value As MushroomButton.OutputTypes)
            Me.outputTypes_0 = value
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

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.MouseDown, New MouseEventHandler(AddressOf Me.MushroomButton_MouseDown)
        AddHandler MyBase.MouseUp, New MouseEventHandler(AddressOf Me.MushroomButton_MouseUp)
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.MushroomButton_Resize)
        Me.rectangle_0 = New Rectangle()
        Me.legendPlates_0 = MushroomButton.LegendPlates.Large
        Me.outputTypes_0 = MushroomButton.OutputTypes.MomentarySet
        Me.stringFormat_0 = New StringFormat()
        Me.solidBrush_0 = New SolidBrush(Color.Black)
    End Sub

    Private Sub method_0()
        If (Me.legendPlates_0 = MushroomButton.LegendPlates.Large) Then
            Me.decimal_1 = Convert.ToDecimal(CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width))
        ElseIf (Me.legendPlates_0 <> MushroomButton.LegendPlates.Small) Then
            Me.decimal_1 = Decimal.One
        Else
            Me.decimal_1 = Convert.ToDecimal(CDbl(My.Resources.LegendPlateShort.Height) / CDbl(My.Resources.LegendPlateShort.Width))
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
        If (Me.legendPlates_0 = MushroomButton.LegendPlates.Large) Then
            num = New Decimal(CDbl(MyBase.Width) / CDbl(My.Resources.LegendPlate.Width))
            num1 = New Decimal(CDbl(MyBase.Height) / CDbl(My.Resources.LegendPlate.Height))
        ElseIf (Me.legendPlates_0 <> MushroomButton.LegendPlates.Small) Then
            num = New Decimal(CDbl(MyBase.Width) / CDbl(My.Resources.EstopButton.Width))
            num1 = New Decimal(CDbl(MyBase.Height) / CDbl(My.Resources.EstopButton.Height))
        Else
            num = New Decimal(CDbl(MyBase.Width) / CDbl(My.Resources.LegendPlateShort.Width))
            num1 = New Decimal(CDbl(MyBase.Height) / CDbl(My.Resources.LegendPlateShort.Height))
        End If
        If (Decimal.Compare(num, num1) >= 0) Then
            Me.decimal_0 = num1
        Else
            Me.decimal_0 = num
        End If
        If (Decimal.Compare(Me.decimal_0, Decimal.Zero) > 0) Then
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            If (Me.legendPlates_0 = MushroomButton.LegendPlates.Large) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Height), Me.decimal_0)))
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width))
            ElseIf (Me.legendPlates_0 <> MushroomButton.LegendPlates.Small) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Height), Me.decimal_0)))
                Me.decimal_1 = Decimal.One
            Else
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Height), Me.decimal_0)))
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlateShort.Height) / CDbl(My.Resources.LegendPlateShort.Width))
            End If
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Height), Me.decimal_0)))
            Me.bitmap_3 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButtonDown.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.EstopButtonDown.Height), Me.decimal_0)))
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
                Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_3)
                    Using graphic2 As Graphics = Graphics.FromImage(Me.bitmap_2)
                        If (Me.legendPlates_0 = MushroomButton.LegendPlates.Large) Then
                            graphic.DrawImage(My.Resources.LegendPlate, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlate.Height), Me.decimal_0)))
                        ElseIf (Me.legendPlates_0 = MushroomButton.LegendPlates.Small) Then
                            graphic.DrawImage(My.Resources.LegendPlateShort, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateShort.Height), Me.decimal_0)))
                        End If
                        Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
                        graphic2.DrawImage(My.Resources.EstopButton, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.EstopButton.Height), Me.decimal_0)))
                        graphic1.DrawImage(My.Resources.EstopButtonDown, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.EstopButtonDown.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.EstopButtonDown.Height), Me.decimal_0)))
                    End Using
                End Using
            End Using
            Me.bitmap_1 = Me.bitmap_2
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Y = 0
            If (Me.legendPlates_0 <> MushroomButton.LegendPlates.Large) Then
                Me.rectangle_0.Height = Convert.ToInt32(CDbl(MyBase.Height) * 0.18)
            Else
                Me.rectangle_0.Height = Convert.ToInt32(CDbl(MyBase.Height) * 0.4)
            End If
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub MushroomButton_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        Me.bitmap_1 = Me.bitmap_3
        MyBase.Invalidate()
    End Sub

    Private Sub MushroomButton_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Me.bitmap_1 = Me.bitmap_2
        MyBase.Invalidate()
    End Sub

    Private Sub MushroomButton_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.Height <> Me.int_1 Or MyBase.Width <> Me.int_0) Then
            Me.method_0()
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_0 IsNot Nothing And Me.bitmap_4 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_4)
            If (Me.legendPlates_0 <> MushroomButton.LegendPlates.None) Then
                graphic.DrawImage(Me.bitmap_0, 0, 0)
            End If
            If (Me.legendPlates_0 = MushroomButton.LegendPlates.Large) Then
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.68 - CDbl(Me.bitmap_1.Height) / 2))
            ElseIf (Me.legendPlates_0 <> MushroomButton.LegendPlates.Small) Then
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl((Me.bitmap_0.Height - Me.bitmap_1.Height)) / 2))
            Else
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.59 - CDbl(Me.bitmap_1.Height) / 2))
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
            painte.Graphics.DrawImage(Me.bitmap_4, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Public Enum LegendPlates
        None
        Small
        Large
    End Enum

    Public Enum OutputTypes
        MomentarySet
        MomentaryReset
        SetTrue
        SetFalse
        Toggle
    End Enum
End Class
