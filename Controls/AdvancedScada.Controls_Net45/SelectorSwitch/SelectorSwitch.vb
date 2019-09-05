Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Public Class SelectorSwitch
    Inherits ButtonBase
    Private legendPlates_0 As SelectorSwitch.LegendPlates

    Private outputType_0 As OutputType

    Private decimal_0 As Decimal

    Public Property LegendPlate As SelectorSwitch.LegendPlates
        Get
            Return Me.legendPlates_0
        End Get
        Set(ByVal value As SelectorSwitch.LegendPlates)
            If (Me.legendPlates_0 <> value) Then
                Me.legendPlates_0 = value
                If (value <> SelectorSwitch.LegendPlates.Large) Then
                    MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * My.Resources.LegendPlateShort.Height)) / CDbl(My.Resources.LegendPlateShort.Width)))
                Else
                    MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * My.Resources.LegendPlate.Height)) / CDbl(My.Resources.LegendPlate.Width)))
                End If
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property OutputType As OutputType
        Get
            Return Me.outputType_0
        End Get
        Set(ByVal value As OutputType)
            Me.outputType_0 = value
        End Set
    End Property

    '' <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.MouseUp, New MouseEventHandler(AddressOf Me.SelectorSwitch_MouseUp)
        Me.legendPlates_0 = SelectorSwitch.LegendPlates.Large
        Me.outputType_0 = OutputType.MomentarySet
        Me.decimal_0 = New Decimal(CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width))
        Me.BackgroundImage = My.Resources.LegendPlate
        Me.BackgroundImageLayout = ImageLayout.Zoom
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Dim num As Decimal
        Dim num1 As Decimal
        Try
            If (Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large) Then
                Me.decimal_0 = New Decimal(CDbl(My.Resources.LegendPlateShort.Height) / CDbl(My.Resources.LegendPlateShort.Width))
            Else
                Me.decimal_0 = New Decimal(CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width))
            End If
            If (Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large) Then
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.LegendPlateShort.Width))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.LegendPlateShort.Height))
            Else
                num = New Decimal(CSng(MyBase.Width) / CSng(My.Resources.LegendPlate.Width))
                num1 = New Decimal(CSng(MyBase.Height) / CSng(My.Resources.LegendPlate.Height))
            End If
            If (Decimal.Compare(num, num1) >= 0) Then
                Me.ImageRatio = Convert.ToDouble(num1)
            Else
                Me.ImageRatio = Convert.ToDouble(num)
            End If
            If (Me.ImageRatio > 0) Then
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
                If (Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large) Then
                    Me.StaticImage = New Bitmap(Convert.ToInt32(CDbl(My.Resources.LegendPlateShort.Width) * Me.ImageRatio), Convert.ToInt32(CDbl(My.Resources.LegendPlateShort.Height) * Me.ImageRatio))
                Else
                    Me.StaticImage = New Bitmap(Convert.ToInt32(CDbl(My.Resources.LegendPlate.Width) * Me.ImageRatio), Convert.ToInt32(CDbl(My.Resources.LegendPlate.Height) * Me.ImageRatio))
                End If
                Using graphic As Graphics = Graphics.FromImage(Me.StaticImage)
                    If (Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large) Then
                        Me.BackgroundImage = My.Resources.LegendPlateSmallWithNut
                    Else
                        Me.BackgroundImage = My.Resources.LegendPlateLargeWithNut
                    End If
                    If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                        Me.TextRectangle.X = 0
                        Me.TextRectangle.Width = MyBase.Width
                        Me.TextRectangle.Y = 0
                        If (Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large) Then
                            Me.TextRectangle.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.18))
                        Else
                            Me.TextRectangle.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.4))
                        End If
                        Me.stringFormat_0.Alignment = StringAlignment.Center
                        Me.stringFormat_0.LineAlignment = StringAlignment.Center
                        Me.TextBrush = New SolidBrush(MyBase.ForeColor)
                        If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                            Me.TextBrush.Color = MyBase.ForeColor
                        End If
                        If (Me.BackColor <> Color.Transparent) Then
                            graphic.TextRenderingHint = TextRenderingHint.AntiAlias
                        Else
                            graphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                        End If
                        graphic.DrawString(Me.Text, MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
                    End If
                End Using
                Me.OffImage = New Bitmap(Convert.ToInt32(CDbl(My.Resources.SelectorSwitchCenterNoNut.Width) * Me.ImageRatio * 2), Convert.ToInt32(CDbl(My.Resources.SelectorSwitchCenterNoNut.Height) * Me.ImageRatio * 2))
                Me.OnImage = New Bitmap(Convert.ToInt32(CDbl(My.Resources.SelectorSwitchCenterNoNut.Width) * Me.ImageRatio * 2), Convert.ToInt32(CDbl(My.Resources.SelectorSwitchCenterNoNut.Height) * Me.ImageRatio * 2))
                Dim width As Double = CDbl(Me.OffImage.Width) / 2
                Dim height As Double = CDbl(Me.OffImage.Height) / 2
                Using graphic1 As Graphics = Graphics.FromImage(Me.OffImage)
                    Using matrix As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                        matrix.RotateAt(-30!, New Point(Convert.ToInt32(width), Convert.ToInt32(height)), MatrixOrder.Prepend)
                        graphic1.Transform = matrix
                        graphic1.DrawImage(My.Resources.SelectorSwitchCenterNoNut, 0, 0, Me.OffImage.Width, Me.OffImage.Height)
                    End Using
                End Using
                Using graphic2 As Graphics = Graphics.FromImage(Me.OnImage)
                    Using matrix1 As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                        matrix1.RotateAt(30!, New Point(Convert.ToInt32(width), Convert.ToInt32(height)), MatrixOrder.Prepend)
                        graphic2.Transform = matrix1
                        graphic2.DrawImage(My.Resources.SelectorSwitchCenterNoNut, 0, 0, Me.OnImage.Width, Me.OnImage.Height)
                    End Using
                End Using
                MyBase.Invalidate()
            End If
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If (MyBase.Enabled) Then
            If (Me.outputType_0 = OutputType.Toggle) Then
                Me.Value = Not Me.m_Value
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim num As Integer
        If (Me.StaticImage IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImage(Me.StaticImage, 0, 0)
            num = If(Me.legendPlates_0 <> SelectorSwitch.LegendPlates.Large, Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.59 - CDbl(Me.OnImage.Height) / 2), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.68 - CDbl(Me.OnImage.Height) / 2))
            If (Not Me.m_Value) Then
                graphics.DrawImage(Me.OffImage, Convert.ToInt32(CDbl(Me.StaticImage.Width) / 2 - CDbl(Me.OffImage.Width) / 2), num)
            Else
                graphics.DrawImage(Me.OnImage, Convert.ToInt32(CDbl(Me.StaticImage.Width) / 2 - CDbl(Me.OnImage.Width) / 2), num)
            End If
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        If (Me.LastHeight < MyBase.Height Or Me.LastWidth < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_0)) Then
                MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_0))
            Else
                MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_0))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Convert.ToDouble(Me.decimal_0)) Then
            MyBase.Width = Convert.ToInt32(Decimal.Divide(New Decimal(MyBase.Height), Me.decimal_0))
        Else
            MyBase.Height = Convert.ToInt32(Decimal.Multiply(New Decimal(MyBase.Width), Me.decimal_0))
        End If
        Me.LastWidth = MyBase.Width
        Me.LastHeight = MyBase.Height
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.CreateStaticImage()
    End Sub

    Private Sub SelectorSwitch_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (MyBase.Enabled) Then
            MyBase.Invalidate()
        End If
    End Sub

    Public Enum LegendPlates
        Large
        Small
    End Enum
End Class
