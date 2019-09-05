Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Thermometer2
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private rectangle_0 As Rectangle

    Private rectangle_1 As Rectangle

    Private double_0 As Double

    Private double_1 As Double

    Private unitsSelect_0 As Thermometer2.UnitsSelect

    Private color_0 As Color

    Private columnColorOption_0 As Thermometer2.ColumnColorOption

    Public Property ColumnColor As Thermometer2.ColumnColorOption
        Get
            Return Me.columnColorOption_0
        End Get
        Set(ByVal value As Thermometer2.ColumnColorOption)
            If (Me.columnColorOption_0 <> value) Then
                Me.columnColorOption_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property FillColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            If (Me.color_0 <> value) Then
                Me.color_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (Me.double_0 <> value) Then
                Me.double_0 = value
                Me.method_0()
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
            Me.method_0()
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_1) Then
                Me.double_1 = value
                Me.method_1()
            End If
        End Set
    End Property

    Public Property ValueUnits As Thermometer2.UnitsSelect
        Get
            Return Me.unitsSelect_0
        End Get
        Set(ByVal value As Thermometer2.UnitsSelect)
            If (Me.unitsSelect_0 <> value) Then
                Me.unitsSelect_0 = value
                Me.method_1()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.double_0 = -40
        Me.color_0 = Color.Red
        Me.columnColorOption_0 = Thermometer2.ColumnColorOption.Red
        Me.rectangle_0 = New Rectangle(0, 0, 10, 10)
        Me.ForeColor = Color.Black
        MyBase.Height = 240
        MyBase.Width = 75
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            If (Me.bitmap_2 IsNot Nothing) Then
                Me.bitmap_2.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Shared Function GetRelativeColor(ByVal color As System.Drawing.Color, ByVal intensity As Double) As System.Drawing.Color
        intensity = Math.Max(intensity, 0)
        Dim color1 As System.Drawing.Color = System.Drawing.Color.FromArgb(CInt(Math.Round(Math.Min(CDbl((color.R + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.G + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.B + 1)) * intensity, 255))))
        Return color1
    End Function

    Private Sub method_0()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
                graphic.DrawImage(My.Resources.Thermometer2Static, 0, 0, Me.bitmap_0.Width, Me.bitmap_0.Height)
                Dim stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat() With
                {
                    .LineAlignment = StringAlignment.Center,
                    .Alignment = StringAlignment.Far
                }
                Dim num As Double = 20
                Dim rectangle(8) As System.Drawing.Rectangle
                Dim num1 As Integer = 0
                Do
                    rectangle(num1) = New System.Drawing.Rectangle(0, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * (0.775 - CDbl(num1) * 0.08))), CInt(Math.Round(CDbl(Me.bitmap_0.Width) * 0.24)), CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.05)))
                    graphic.DrawString(Convert.ToString(Me.double_0 + num * CDbl(num1)), New System.Drawing.Font(Me.Font.FontFamily, CSng((CDbl(MyBase.Height) * 0.025)), FontStyle.Regular, GraphicsUnit.Point), New SolidBrush(Me.ForeColor), rectangle(num1), stringFormat)
                    num1 = num1 + 1
                Loop While num1 <= 8
                num = 10
                stringFormat.Alignment = StringAlignment.Near
                Dim double0 As Double = (Me.double_0 - 32) * 5 / 9
                Dim num2 As Integer = CInt(Math.Round(CDbl(MyBase.Width) * 0.77))
                Dim num3 As Integer = 0
                Do
                    rectangle(0) = New System.Drawing.Rectangle(num2, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * (0.762 - CDbl(num3) * 0.071))), MyBase.Width - num2, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.05)))
                    graphic.DrawString(Convert.ToString(CInt(Math.Round(double0 + num * CDbl(num3)))), New System.Drawing.Font(Me.Font.FontFamily, CSng((CDbl(MyBase.Height) * 0.025)), FontStyle.Regular, GraphicsUnit.Point), New SolidBrush(Me.ForeColor), rectangle(0), stringFormat)
                    num3 = num3 + 1
                Loop While num3 <= 9
                stringFormat.LineAlignment = StringAlignment.Near
                stringFormat.Alignment = StringAlignment.Center
                Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, CInt(Math.Round(CDbl(Me.bitmap_0.Height) * 0.01)), Me.bitmap_0.Width, Me.bitmap_0.Height)
                graphic.DrawString(MyBase.Text, Me.Font, New SolidBrush(Me.ForeColor), rectangle1, stringFormat)
            End Using
            Me.bitmap_2 = New Bitmap(CInt(Math.Round(CDbl(MyBase.Width) * 0.366)), CInt(Math.Round(CDbl(MyBase.Height) * 0.125)))
            Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
                Select Case Me.columnColorOption_0
                    Case Thermometer2.ColumnColorOption.Red
                        graphic1.DrawImage(My.Resources.Thermometer2Bulb, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                        Exit Select
                    Case Thermometer2.ColumnColorOption.Green
                        graphic1.DrawImage(My.Resources.Thermometer2BulbGrn, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                        Exit Select
                    Case Thermometer2.ColumnColorOption.Blue
                        graphic1.DrawImage(My.Resources.Thermometer2BulbBlue, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                        Exit Select
                End Select
            End Using
            Me.method_1()
            MyBase.Invalidate()
        End If
    End Sub

    Private Sub method_1()
        Dim double0 As Double
        Dim num As Double = Math.Max(Me.double_1, Me.double_0 - 5)
        If (Me.unitsSelect_0 <> Thermometer2.UnitsSelect.F) Then
            num = Math.Min(num, Me.double_0 + 92)
            double0 = (num * 1.8 + 32 - Me.double_0) / 160
        Else
            num = Math.Min(num, Me.double_0 + 165)
            double0 = (num - Me.double_0) / 160
        End If
        Dim height As Double = CDbl(MyBase.Height) * 0.85
        Dim height1 As Double = CDbl(MyBase.Height) * 0.645 * double0 + CDbl(MyBase.Height) * 0.05
        Me.rectangle_0.Height = CInt(Math.Round(height1))
        Me.rectangle_0.Width = CInt(Math.Round(CDbl(MyBase.Width) * 0.08))
        Me.rectangle_0.X = CInt(Math.Round(CDbl(MyBase.Width) * 0.47))
        Me.rectangle_0.Y = CInt(Math.Round(height - height1))
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.method_0()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        If (Me.bitmap_1 IsNot Nothing Or Me.bitmap_0 IsNot Nothing Or Me.bitmap_2 IsNot Nothing) Then
            Using graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
                graphic.DrawImage(Me.bitmap_0, 0, 0)
                Select Case Me.columnColorOption_0
                    Case Thermometer2.ColumnColorOption.Red
                        graphic.DrawImage(My.Resources.Thermometer2Column, Me.rectangle_0)
                        Exit Select
                    Case Thermometer2.ColumnColorOption.Green
                        graphic.DrawImage(My.Resources.Thermometer2ColumnGrn, Me.rectangle_0)
                        Exit Select
                    Case Thermometer2.ColumnColorOption.Blue
                        graphic.DrawImage(My.Resources.Thermometer2ColumnBlue, Me.rectangle_0)
                        Exit Select
                End Select
                graphic.DrawImage(Me.bitmap_2, CInt(Math.Round(CDbl(MyBase.Width) * 0.335)), CInt(Math.Round(CDbl(MyBase.Height) * 0.822)))
            End Using
            painte.Graphics.DrawImageUnscaled(Me.bitmap_1, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler


    Public Enum ColumnColorOption
        Red
        Green
        Blue
    End Enum

    Public Enum UnitsSelect
        F
        C
    End Enum
End Class
