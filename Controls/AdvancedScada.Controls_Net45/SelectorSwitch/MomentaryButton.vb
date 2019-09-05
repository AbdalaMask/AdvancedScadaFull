Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

<DefaultEvent("Click")>
Public Class MomentaryButton
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bool_0 As Boolean

    Private rectangle_0 As Rectangle

    Private decimal_0 As Decimal

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private buttonColorOption_0 As MomentaryButton.ButtonColorOption

    Private legendPlateOption_0 As MomentaryButton.LegendPlateOption

    Private outputTypes_0 As MomentaryButton.OutputTypes

    Private decimal_1 As Decimal

    Private int_0 As Integer

    Private int_1 As Integer

    Public Property ButtonColor As MomentaryButton.ButtonColorOption
        Get
            Return Me.buttonColorOption_0
        End Get
        Set(ByVal value As MomentaryButton.ButtonColorOption)
            Me.buttonColorOption_0 = value
            Me.method_0()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            Me.method_0()
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
            If (Me.solidBrush_0 IsNot Nothing) Then
                Me.solidBrush_0.Color = value
            Else
                Me.solidBrush_0 = New SolidBrush(value)
            End If
            Me.method_0()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property LegendPlate As MomentaryButton.LegendPlateOption
        Get
            Return Me.legendPlateOption_0
        End Get
        Set(ByVal value As MomentaryButton.LegendPlateOption)
            Me.legendPlateOption_0 = value
            Me.method_0()
            Me.MomentaryButton_Resize(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property OutputType As MomentaryButton.OutputTypes
        Get
            Return Me.outputTypes_0
        End Get
        Set(ByVal value As MomentaryButton.OutputTypes)
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
            Me.method_0()
            MyBase.Invalidate()
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.MomentaryButton_Resize)
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.buttonColorOption_0 = MomentaryButton.ButtonColorOption.Green
        Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large
        Me.outputTypes_0 = MomentaryButton.OutputTypes.MomentarySet
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        Me.method_0()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.solidBrush_0 IsNot Nothing) Then
                    Me.solidBrush_0.Dispose()
                End If
                If (Me.stringFormat_0 IsNot Nothing) Then
                    Me.stringFormat_0.Dispose()
                End If
                If (Me.bitmap_0 IsNot Nothing) Then
                    Me.bitmap_0.Dispose()
                End If
                If (Me.bitmap_1 IsNot Nothing) Then
                    Me.bitmap_1.Dispose()
                End If
                If (Me.bitmap_2 IsNot Nothing) Then
                    Me.bitmap_2.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        Dim num As Decimal
        Dim num1 As Decimal
        Dim singleArray As Single()()
        If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
            num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.LegendPlateLargeWithNut.Width))
            num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.LegendPlateLargeWithNut.Height))
        ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
            num = New Decimal(CDbl(MyBase.Width) / CDbl(My.Resources.Nut.Width))
            num1 = New Decimal(CDbl(MyBase.Height) / CDbl(My.Resources.Nut.Height))
        Else
            num = New Decimal(CDbl(MyBase.Width) / CDbl(My.Resources.LegendPlateSmallWithNut.Width))
            num1 = New Decimal(CDbl(MyBase.Height) / CDbl(My.Resources.LegendPlateSmallWithNut.Height))
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
            If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateLargeWithNut.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateLargeWithNut.Height), Me.decimal_0)))
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlateLargeWithNut.Height) / CDbl(My.Resources.LegendPlateLargeWithNut.Width))
            ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.Nut.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.Nut.Height), Me.decimal_0)))
                Me.decimal_1 = Decimal.One
            Else
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateSmallWithNut.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.LegendPlateSmallWithNut.Height), Me.decimal_0)))
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlateSmallWithNut.Height) / CDbl(My.Resources.LegendPlateSmallWithNut.Width))
            End If
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
                graphic.DrawImage(My.Resources.LegendPlateLargeWithNut, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateLargeWithNut.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateLargeWithNut.Height), Me.decimal_0)))
            ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
                graphic.DrawImage(My.Resources.Nut, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
            Else
                graphic.DrawImage(My.Resources.LegendPlateSmallWithNut, 0!, 0!, Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateSmallWithNut.Width), Me.decimal_0)), Convert.ToSingle(Decimal.Multiply(New Decimal(My.Resources.LegendPlateSmallWithNut.Height), Me.decimal_0)))
            End If
            Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.None) Then
                Me.decimal_0 = New Decimal(Convert.ToDouble(Me.decimal_0) / 1.35)
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MAButtonGray.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MAButtonGray.Height), Me.decimal_0)))
            Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, 0, Me.bitmap_1.Width, Me.bitmap_1.Height)
            Select Case Me.buttonColorOption_0
                Case MomentaryButton.ButtonColorOption.Red
                    singleArray = New Single()() {New Single() {2.5!, Nothing, Nothing, Nothing, Nothing}, New Single() {Nothing, 0.5!, Nothing, Nothing, Nothing}, New Single() {Nothing, Nothing, 0.5!, Nothing, Nothing}, New Single() {Nothing, Nothing, Nothing, 1!, Nothing}, New Single() {Nothing, Nothing, Nothing, Nothing, 1!}}
                    Exit Select
                Case MomentaryButton.ButtonColorOption.Green
Label0:
                    Dim singleArray1()() As Single = {New Single() {1!, Nothing, Nothing, Nothing, Nothing}, New Single() {Nothing, 2!, Nothing, Nothing, Nothing}, New Single() {Nothing, Nothing, 1!, Nothing, Nothing}, New Single() {Nothing, Nothing, Nothing, 1!, Nothing}, New Single() {Nothing, Nothing, Nothing, Nothing, 1!}}
                    singleArray = singleArray1
                    Exit Select
                Case MomentaryButton.ButtonColorOption.Blue
                    singleArray = New Single()() {New Single() {0.75!, Nothing, Nothing, Nothing, Nothing}, New Single() {Nothing, 0.75!, Nothing, Nothing, Nothing}, New Single() {Nothing, Nothing, 2.75!, Nothing, Nothing}, New Single() {Nothing, Nothing, Nothing, 1!, Nothing}, New Single() {Nothing, Nothing, Nothing, Nothing, 1!}}
                    Exit Select
                Case MomentaryButton.ButtonColorOption.Black
                    singleArray = New Single()() {New Single() {1!, Nothing, Nothing, Nothing, Nothing}, New Single() {Nothing, 1!, Nothing, Nothing, Nothing}, New Single() {Nothing, Nothing, 1!, Nothing, Nothing}, New Single() {Nothing, Nothing, Nothing, 1!, Nothing}, New Single() {Nothing, Nothing, Nothing, Nothing, 1!}}
                    Exit Select
                Case Else
                    GoTo Label0
            End Select
            Using imageAttribute As ImageAttributes = New ImageAttributes()
                Try
                    imageAttribute.SetColorMatrix(New ColorMatrix(singleArray))
                    Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_1)
                        graphic1.DrawImage(My.Resources.MAButtonGray, rectangle, 0, 0, My.Resources.MAButtonGray.Width, My.Resources.MAButtonGray.Height, GraphicsUnit.Pixel, imageAttribute)
                    End Using
                Catch exception As System.Exception
                    ProjectData.SetProjectError(exception)
                    ProjectData.ClearProjectError()
                End Try
            End Using
            If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.68 - CDbl(Me.bitmap_1.Height) / 2))
            ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl((Me.bitmap_0.Width - Me.bitmap_1.Width)) / 2), Convert.ToInt32(CDbl((Me.bitmap_0.Height - Me.bitmap_1.Height)) / 2))
            Else
                graphic.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.6 - CDbl(Me.bitmap_1.Height) / 2))
            End If
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MAButtonGrayPressedRing.Width), Me.decimal_0)), Convert.ToInt32(Decimal.Multiply(New Decimal(My.Resources.MAButtonGrayPressedRing.Height), Me.decimal_0)))
            Using graphic2 As Graphics = Graphics.FromImage(Me.bitmap_2)
                graphic2.DrawImage(My.Resources.MAButtonGrayPressedRing, New System.Drawing.Rectangle(0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height), 0, 0, My.Resources.MAButtonGrayPressedRing.Width, My.Resources.MAButtonGrayPressedRing.Height, GraphicsUnit.Pixel)
            End Using
            graphic = Graphics.FromImage(Me.bitmap_1)
            Dim graphic3 As Graphics = Graphics.FromImage(Me.bitmap_2)
            Me.rectangle_0.X = 0
            Me.rectangle_0.Width = MyBase.Width
            Me.rectangle_0.Y = 0
            If (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Large) Then
                Me.rectangle_0.Height = Convert.ToInt32(CDbl(MyBase.Height) * 0.18)
            Else
                Me.rectangle_0.Height = Convert.ToInt32(CDbl(MyBase.Height) * 0.4)
            End If
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            graphic.Dispose()
            graphic3.Dispose()
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub MomentaryButton_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.Height <> Me.int_1 Or MyBase.Width <> Me.int_0) Then
            If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlateLargeWithNut.Height) / CDbl(My.Resources.LegendPlateLargeWithNut.Width))
            ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
                Me.decimal_1 = Decimal.One
            Else
                Me.decimal_1 = New Decimal(CDbl(My.Resources.LegendPlateSmallWithNut.Height) / CDbl(My.Resources.LegendPlateSmallWithNut.Width))
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
            Me.method_0()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Me.bool_0 = True
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        Me.bool_0 = False
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImage(Me.bitmap_0, 0, 0)
            If (Me.bool_0 And Me.bitmap_2 IsNot Nothing) Then
                If (Me.legendPlateOption_0 = MomentaryButton.LegendPlateOption.Large) Then
                    graphics.DrawImage(Me.bitmap_2, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_2.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.68 - CDbl(Me.bitmap_2.Height) / 2))
                ElseIf (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.Small) Then
                    graphics.DrawImage(Me.bitmap_2, Convert.ToInt32(CDbl((Me.bitmap_0.Width - Me.bitmap_2.Width)) / 2), Convert.ToInt32(CDbl((Me.bitmap_0.Height - Me.bitmap_2.Height)) / 2))
                Else
                    graphics.DrawImage(Me.bitmap_2, Convert.ToInt32(CDbl(Me.bitmap_0.Width) / 2 - CDbl(Me.bitmap_2.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.6 - CDbl(Me.bitmap_2.Height) / 2))
                End If
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                If (Me.legendPlateOption_0 <> MomentaryButton.LegendPlateOption.None) Then
                    graphics.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
                End If
            End If
        End If
    End Sub

    Public Enum ButtonColorOption
        Red
        Green
        Blue
        Black
    End Enum

    Public Enum LegendPlateOption
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
