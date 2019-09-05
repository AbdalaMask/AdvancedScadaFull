Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class Gauge
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap()

    Private double_0 As Double

    Private double_1 As Double

    Private int_0 As Integer

    Private int_1 As Integer

    Private double_2 As Double

    Private rectangle_0 As Rectangle

    Private rectangle_1 As Rectangle()

    Private stringFormat_0 As StringFormat

    Private stringFormat_1 As StringFormat

    Private stringFormat_2 As StringFormat

    Private stringFormat_3 As StringFormat

    Private double_3 As Double

    Private double_4 As Double

    Private decimal_0 As Decimal

    Private int_2 As Integer

    Private int_3 As Integer

    Private bool_0 As Boolean

    Private double_5 As Double

    Private double_6 As Double

    Private matrix_0 As Matrix

    Private bitmap_4 As Bitmap

    Private solidBrush_0 As SolidBrush

    Private int_4 As Integer

    Private int_5 As Integer

    Private bool_1 As Boolean

    Private int_6 As Integer

    Private int_7 As Integer

    Public Property AllowDragging As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            Me.bool_0 = value
        End Set
    End Property

    Public Property Maximum As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            Me.int_2 = Convert.ToInt32(Math.Ceiling(CDbl((value - Me.int_3)) / 10) * 10 + CDbl(Me.int_3))
            Me.method_0()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Minimum As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            Me.int_3 = value
            Me.Maximum = Me.int_2
            Me.method_0()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_4
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_4) Then
                Me.double_4 = Math.Max(Math.Min(value, Convert.ToDouble(Decimal.Divide(New Decimal(Me.int_2), Me.decimal_0))), CDbl(Me.int_3))
                Me.double_3 = (Me.double_4 * Convert.ToDouble(Me.decimal_0) - CDbl(Me.int_3)) * (4.71 / CDbl((Me.int_2 - Me.int_3))) * -1
                MyBase.Invalidate(New Rectangle(Convert.ToInt32(CDbl(MyBase.Width) * 0.14), Convert.ToInt32(CDbl(MyBase.Height) * 0.14), Convert.ToInt32(CDbl(MyBase.Width) * 0.72), Convert.ToInt32(CDbl(MyBase.Height) * 0.63)))
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.decimal_0
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_0 = value
            Me.double_4 = Math.Max(Math.Min(Me.double_4, Convert.ToDouble(Decimal.Divide(New Decimal(Me.int_2), Me.decimal_0))), CDbl(Me.int_3))
            Me.double_3 = (Me.double_4 * Convert.ToDouble(Me.decimal_0) - CDbl(Me.int_3)) * (4.71 / CDbl((Me.int_2 - Me.int_3))) * -1
            MyBase.Invalidate(New Rectangle(Convert.ToInt32(CDbl(MyBase.Width) * 0.14), Convert.ToInt32(CDbl(MyBase.Height) * 0.14), Convert.ToInt32(CDbl(MyBase.Width) * 0.72), Convert.ToInt32(CDbl(MyBase.Height) * 0.63)))
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        ReDim Me.bitmap_3(11)
        Me.rectangle_0 = New Rectangle()
        ReDim Me.rectangle_1(10)
        Me.stringFormat_0 = New StringFormat()
        Me.stringFormat_1 = New StringFormat()
        Me.stringFormat_2 = New StringFormat()
        Me.stringFormat_3 = New StringFormat()
        Me.decimal_0 = Decimal.One
        Me.int_2 = 100
        Me.int_3 = 0
        Me.matrix_0 = New Matrix()

        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.BackColor = Color.Transparent
        Me.method_0()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.solidBrush_0 IsNot Nothing) Then
                    Me.solidBrush_0.Dispose()
                End If
                If (Me.bitmap_4 IsNot Nothing) Then
                    Me.bitmap_4.Dispose()
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
                If (Me.stringFormat_0 IsNot Nothing) Then
                    Me.stringFormat_0.Dispose()
                End If
                If (Me.stringFormat_1 IsNot Nothing) Then
                    Me.stringFormat_1.Dispose()
                End If
                If (Me.stringFormat_2 IsNot Nothing) Then
                    Me.stringFormat_2.Dispose()
                End If
                If (Me.stringFormat_3 IsNot Nothing) Then
                    Me.stringFormat_3.Dispose()
                End If
                If (Me.matrix_0 IsNot Nothing) Then
                    Me.matrix_0.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
            graphicsPath.AddEllipse(-1, -1, MyBase.Width + 1, MyBase.Height + 1)
            MyBase.Region = New System.Drawing.Region(graphicsPath)
        End Using
        Dim width As Double = CDbl(MyBase.Width) / CDbl(My.Resources.GaugeSilverNoNeedle.Width)
        Dim height As Double = CDbl(MyBase.Height) / CDbl(My.Resources.GaugeSilverNoNeedle.Height)
        If (width >= height) Then
            Me.double_5 = CDbl(MyBase.Width)
            Me.double_6 = CDbl(My.Resources.GaugeSilverNoNeedle.Height) / CDbl(My.Resources.GaugeSilverNoNeedle.Width) * CDbl(MyBase.Width)
            Me.double_2 = height
        Else
            Me.double_6 = CDbl(MyBase.Height)
            If (Not (MyBase.Height > 0 And My.Resources.GaugeSilverNoNeedle.Height > 0)) Then
                Me.double_5 = 1
            Else
                Me.double_5 = CDbl(My.Resources.GaugeSilverNoNeedle.Width) / CDbl(My.Resources.GaugeSilverNoNeedle.Height) * CDbl(MyBase.Height)
            End If
            Me.double_2 = width
        End If
        If (Me.double_2 > 0) Then
            Me.rectangle_0.X = Convert.ToInt32(CDbl(MyBase.Width) * 0.3)
            Me.rectangle_0.Y = Convert.ToInt32(CDbl(MyBase.Height) * 0.27)
            Me.rectangle_0.Width = MyBase.Width - Me.rectangle_0.X * 2
            Me.rectangle_0.Height = Convert.ToInt32(CDbl(MyBase.Height) * 0.18)
            Me.stringFormat_1.Alignment = StringAlignment.Center
            Me.stringFormat_1.LineAlignment = StringAlignment.Center
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New System.Drawing.SolidBrush(MyBase.ForeColor)
            End If
            Me.stringFormat_3.Alignment = StringAlignment.Near
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_2.Alignment = StringAlignment.Far
            Dim num As Integer = Convert.ToInt32(140 * Me.double_2)
            Dim num1 As Integer = Convert.ToInt32(80 * Me.double_2)
            Me.rectangle_1(0) = New Rectangle(Convert.ToInt32(215 * Me.double_2), Convert.ToInt32(478 * Me.double_2), num, num1)
            Me.rectangle_1(1) = New Rectangle(Convert.ToInt32(160 * Me.double_2), Convert.ToInt32(405 * Me.double_2), num, num1)
            Me.rectangle_1(2) = New Rectangle(Convert.ToInt32(160 * Me.double_2), Convert.ToInt32(316 * Me.double_2), num, num1)
            Me.rectangle_1(3) = New Rectangle(Convert.ToInt32(195 * Me.double_2), Convert.ToInt32(241 * Me.double_2), num, num1)
            Me.rectangle_1(4) = New Rectangle(Convert.ToInt32(255 * Me.double_2), Convert.ToInt32(189 * Me.double_2), num, num1)
            Me.rectangle_1(5) = New Rectangle(Convert.ToInt32(300 * Me.double_2), Convert.ToInt32(155 * Me.double_2), num, num1)
            Me.rectangle_1(6) = New Rectangle(Convert.ToInt32(340 * Me.double_2), Convert.ToInt32(189 * Me.double_2), num, num1)
            Me.rectangle_1(7) = New Rectangle(Convert.ToInt32(402 * Me.double_2), Convert.ToInt32(241 * Me.double_2), num, num1)
            Me.rectangle_1(8) = New Rectangle(Convert.ToInt32(435 * Me.double_2), Convert.ToInt32(316 * Me.double_2), num, num1)
            Me.rectangle_1(9) = New Rectangle(Convert.ToInt32(422 * Me.double_2), Convert.ToInt32(405 * Me.double_2), num, num1)
            Me.rectangle_1(10) = New Rectangle(Convert.ToInt32(380 * Me.double_2), Convert.ToInt32(473 * Me.double_2), num, num1)
            If (Me.bitmap_0 IsNot Nothing) Then
                Me.bitmap_0.Dispose()
            End If
            Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            Me.matrix_0.Reset()
            graphic.DrawImage(My.Resources.GaugeSilverNoNeedle, 0, 0, Convert.ToInt32(CDbl(My.Resources.GaugeSilverNoNeedle.Width) * Me.double_2), Convert.ToInt32(CDbl(My.Resources.GaugeSilverNoNeedle.Height) * Me.double_2))
            Dim num2 As Integer = Convert.ToInt32(CDbl((Me.int_2 - Me.int_3)) / 10)
            Using font As System.Drawing.Font = New System.Drawing.Font("Arial", CSng((40 * Me.double_2)), FontStyle.Regular, GraphicsUnit.Point)
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Color.FromArgb(250, 35, 35, 35))
                    graphic.DrawString(Me.int_3.ToString(), font, solidBrush, Me.rectangle_1(0), Me.stringFormat_3)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2), font, solidBrush, Me.rectangle_1(1), Me.stringFormat_3)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 2), font, solidBrush, Me.rectangle_1(2), Me.stringFormat_3)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 3), font, solidBrush, Me.rectangle_1(3), Me.stringFormat_3)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 4), font, solidBrush, Me.rectangle_1(4), Me.stringFormat_3)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 5), font, solidBrush, Me.rectangle_1(5), Me.stringFormat_0)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 6), font, solidBrush, Me.rectangle_1(6), Me.stringFormat_2)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 7), font, solidBrush, Me.rectangle_1(7), Me.stringFormat_2)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 8), font, solidBrush, Me.rectangle_1(8), Me.stringFormat_2)
                    graphic.DrawString(Convert.ToString(Me.int_3 + num2 * 9), font, solidBrush, Me.rectangle_1(9), Me.stringFormat_2)
                    graphic.DrawString(Convert.ToString(Me.int_2), font, solidBrush, Me.rectangle_1(10), Me.stringFormat_2)
                    graphic.FillRectangle(Brushes.Black, Convert.ToInt32(CDbl(MyBase.Width) * 0.432), Convert.ToInt32(CDbl(MyBase.Height) * 0.57), Convert.ToInt32(103 * Me.double_2), Convert.ToInt32(60 * Me.double_2))
                End Using
            End Using
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New Bitmap(Convert.ToInt32(CDbl(My.Resources.GaugeNeedle.Width) * Me.double_2), Convert.ToInt32(CDbl(My.Resources.GaugeNeedle.Height) * Me.double_2))
            graphic = Graphics.FromImage(Me.bitmap_1)
            graphic.DrawImage(My.Resources.GaugeNeedle, 0, 0, Convert.ToInt32(CDbl(My.Resources.GaugeNeedle.Width) * Me.double_2), Convert.ToInt32(CDbl(My.Resources.GaugeNeedle.Height) * Me.double_2))
            If (Me.bitmap_2 IsNot Nothing) Then
                Me.bitmap_2.Dispose()
            End If
            Me.bitmap_2 = New Bitmap(Convert.ToInt32(CDbl(My.Resources.GaugeNeedleShadow.Width) * Me.double_2), Convert.ToInt32(CDbl(My.Resources.GaugeNeedleShadow.Height) * Me.double_2))
            graphic = Graphics.FromImage(Me.bitmap_2)
            graphic.DrawImage(My.Resources.GaugeNeedleShadow, 0, 0, Convert.ToInt32(CDbl(My.Resources.GaugeNeedleShadow.Width) * Me.double_2), Convert.ToInt32(CDbl(My.Resources.GaugeNeedleShadow.Height) * Me.double_2))
            Dim num3 As Integer = Convert.ToInt32(32 * Me.double_2)
            Dim num4 As Integer = Convert.ToInt32(58 * Me.double_2)
            Dim num5 As Integer = 0
            Do
                If (Me.bitmap_3(num5) IsNot Nothing) Then
                    Me.bitmap_3(num5).Dispose()
                End If
                Me.bitmap_3(num5) = New Bitmap(num3, num4)
                graphic = Graphics.FromImage(Me.bitmap_3(num5))
                SevenSegmentGDI.DrawDigit(graphic, num5, Color.Green, True, 0.0125, 0.0185)
                num5 = num5 + 1
            Loop While num5 <= 11
            graphic.Dispose()
            Me.double_0 = CDbl(My.Resources.GaugeSilverNoNeedle.Width) / 2 * Me.double_2
            Me.double_1 = (CDbl(CSng(My.Resources.GaugeNeedle.Height)) * 0.5 - 2) * Me.double_2
            Me.double_0 = CDbl(My.Resources.GaugeSilverNoNeedle.Width) * 0.338 * Me.double_2
            Me.double_1 = CDbl(Me.bitmap_1.Height) / 2
            Me.int_0 = Convert.ToInt32(CDbl(My.Resources.GaugeSilverNoNeedle.Width) * 0.16 * Me.double_2)
            Me.int_1 = Convert.ToInt32(CDbl(My.Resources.GaugeSilverNoNeedle.Height) * 0.485 * Me.double_2)
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_4.Dispose()
            End If
            Me.bitmap_4 = New Bitmap(MyBase.Width, MyBase.Height)
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.solidBrush_0 IsNot Nothing) Then
            Me.solidBrush_0.Color = MyBase.ForeColor
        Else
            Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Me.bool_1 = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If (Me.bool_0) Then
            If (Me.bool_1 And Me.int_6 <> 0) Then
                Dim location As Point = MyBase.Location
                Dim x As Integer = location.X - Me.int_6 + e.X
                location = MyBase.Location
                MyBase.Location = New Point(x, location.Y - Me.int_7 + e.Y)
            End If
            Me.int_6 = e.X
            Me.int_7 = e.Y
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        Me.bool_1 = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_0 IsNot Nothing And Me.solidBrush_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            If (Me.BackColor <> Color.Transparent) Then
                graphics.FillRectangle(New System.Drawing.SolidBrush(Me.BackColor), 0, 0, MyBase.Width, MyBase.Height)
            ElseIf (MyBase.Parent IsNot Nothing) Then
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(MyBase.Parent.BackColor)
                    graphics.FillRectangle(solidBrush, 0, 0, MyBase.Width, MyBase.Height)
                End Using
            End If
            graphics.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphics.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_1)
            End If
            Dim num As Integer = Convert.ToInt32(35 * Me.double_2)
            Dim double4 As Double = Me.double_4 * Convert.ToDouble(Me.decimal_0)
            If (double4 > 999) Then
                double4 = double4 - Math.Floor(double4 / 1000) * 1000
            End If
            If (Not (double4 > 999 Or double4 < -99)) Then
                Dim num1 As Integer = 1
                Do
                    If (Not (num1 = 1 And double4 < 0)) Then
                        Dim num2 As Integer = Convert.ToInt32(Math.Floor(Math.Abs(double4) / Math.Pow(10, CDbl((3 - num1)))))
                        graphics.DrawImage(Me.bitmap_3(num2), Convert.ToInt32(322 * Me.double_2 + CDbl((num * (num1 - 1)))), Convert.ToInt32(CDbl(MyBase.Height) * 0.57))
                        double4 = If(double4 < 0, double4 + CDbl(num2) * Math.Pow(10, CDbl((3 - num1))), double4 - CDbl(num2) * Math.Pow(10, CDbl((3 - num1))))
                    Else
                        graphics.DrawImage(Me.bitmap_3(11), Convert.ToInt32(322 * Me.double_2 + CDbl((num * (num1 - 1)))), Convert.ToInt32(CDbl(MyBase.Height) * 0.57))
                    End If
                    num1 = num1 + 1
                Loop While num1 <= 3
            Else
                Dim num3 As Integer = 1
                Do
                    graphics.DrawImage(Me.bitmap_3(11), Convert.ToInt32(322 * Me.double_2 + CDbl((num * (num3 - 1)))), Convert.ToInt32(CDbl(MyBase.Height) * 0.57))
                    num3 = num3 + 1
                Loop While num3 <= 3
            End If
            Dim double3 As Double = Me.double_3 + 0.78
            Me.matrix_0.Reset()
            Me.matrix_0.Translate(CSng((CDbl(Me.int_0) + 8 * Me.double_2)), CSng((CDbl(Me.int_1) + 8 * Me.double_2)))
            Me.matrix_0.RotateAt(CSng((-double3 * 180 / 3.14159265358979)), New Point(Convert.ToInt32(Me.double_0), Convert.ToInt32(Me.double_1)))
            graphics.Transform = Me.matrix_0
            graphics.DrawImage(Me.bitmap_2, 0, 0)
            Me.matrix_0.Reset()
            Me.matrix_0.Translate(CSng(Me.int_0), CSng(Me.int_1))
            Me.matrix_0.RotateAt(CSng((-double3 * 180 / 3.14159265358979)), New Point(Convert.ToInt32(Me.double_0), Convert.ToInt32(Me.double_1)))
            graphics.Transform = Me.matrix_0
            graphics.DrawImage(Me.bitmap_1, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Dim size As System.Drawing.Size
        MyBase.OnResize(e)
        If (Not (Me.int_5 < MyBase.Height Or Me.int_4 < MyBase.Width)) Then
            size = If(MyBase.Height < MyBase.Width, New System.Drawing.Size(MyBase.Height, MyBase.Height), New System.Drawing.Size(MyBase.Width, MyBase.Width))
        Else
            size = If(MyBase.Height < MyBase.Width, New System.Drawing.Size(MyBase.Width, MyBase.Width), New System.Drawing.Size(MyBase.Height, MyBase.Height))
        End If
        MyBase.Size = size
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.bitmap_4 IsNot Nothing) Then
            Me.bitmap_4.Dispose()
            Me.bitmap_4 = Nothing
        End If
        Me.method_0()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
