Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class BeveledButtonDisplay
    Inherits ButtonBase
    Private string_0 As String

    Private color_0 As Color

    Private color_1 As Color

    Private int_0 As Integer

    Private bool_0 As Boolean

    Private outputType_0 As OutputType

    Public Property BackcolorOn As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            Me.color_1 = value
            Me.CreateStaticImage()
        End Set
    End Property

    Public Property BeepOnClick As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            Me.bool_0 = value
        End Set
    End Property

    Public Property BorderColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            Me.color_0 = value
            Me.CreateStaticImage()
            Me.OnBorderColorChanged(EventArgs.Empty)
        End Set
    End Property

    Public Property BorderWidth As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (Me.int_0 <> value) Then
                Me.int_0 = Math.Min(Math.Max(2, value), 10)
                Me.CreateStaticImage()
                Me.OnBorderWidthChanged(EventArgs.Empty)
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

    Public Property Text2 As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(Me.string_0, value, False) <> 0) Then
                Me.string_0 = value
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.color_1 = Color.Black
        Me.bool_0 = True
        Me.outputType_0 = OutputType.MomentarySet
        Me.color_0 = Color.DimGray
        MyBase.BackColor = Color.LightGray
        Me.int_0 = 7
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub CreateStaticImage()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.OnImage = New Bitmap(MyBase.Width, MyBase.Height)
            Me.OffImage = New Bitmap(MyBase.Width, MyBase.Height)
            Dim graphic As Graphics = Graphics.FromImage(Me.OnImage)
            Dim graphic1 As Graphics = Graphics.FromImage(Me.OffImage)
            BeveledButtonDisplay.Draw3DBorder(Me, graphic, Me.color_0, Me.int_0)
            BeveledButtonDisplay.Draw3DBorder(Me, graphic1, Me.color_0, Me.int_0)
            Dim width As Integer = MyBase.Width - Me.int_0 * 4
            If (width <= 0) Then
                width = 1
            End If
            Dim height As Integer = MyBase.Height - Me.int_0 * 4
            If (height <= 0) Then
                height = 1
            End If
            Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.int_0 * 2, Me.int_0 * 2, width, height)
            Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                graphicsPath.AddRectangle(rectangle)
                Dim pathGradientBrush As System.Drawing.Drawing2D.PathGradientBrush = New System.Drawing.Drawing2D.PathGradientBrush(graphicsPath)
                If (Me.color_1 <> Color.Black) Then
                    pathGradientBrush.CenterColor = BeveledButtonDisplay.GetRelativeColor(Me.color_1, 20)
                Else
                    pathGradientBrush.CenterColor = BeveledButtonDisplay.GetRelativeColor(Me.BackColor, 20)
                End If
                Dim color1(0) As Color
                If (Me.color_1 <> Color.Black) Then
                    color1(0) = Me.color_1
                Else
                    color1(0) = BeveledButtonDisplay.GetRelativeColor(Me.BackColor, 0.9)
                End If
                pathGradientBrush.SurroundColors = color1
                graphic.FillRectangle(pathGradientBrush, rectangle)
            End Using
            If (Me.color_1 <> Color.Black) Then
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(BeveledButtonDisplay.GetRelativeColor(Me.color_1, 0.75))
                    Using pen As System.Drawing.Pen = New System.Drawing.Pen(solidBrush, 2!)
                        graphic.DrawRectangle(pen, rectangle)
                    End Using
                End Using
            Else
                Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(BeveledButtonDisplay.GetRelativeColor(Me.color_0, 0.5))
                    Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(solidBrush1, 2!)
                        graphic.DrawRectangle(pen1, rectangle)
                    End Using
                End Using
            End If
            Using solidBrush2 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.BackColor)
                graphic1.FillRectangle(solidBrush2, rectangle)
            End Using
            Using pen2 As System.Drawing.Pen = New System.Drawing.Pen(New System.Drawing.SolidBrush(BeveledButtonDisplay.GetRelativeColor(Me.color_0, 0.5)), 2!)
                graphic1.DrawRectangle(pen2, rectangle)
            End Using
            Me.TextRectangle = New System.Drawing.Rectangle(Me.int_0 * 2 + 1, Me.int_0 * 2 + 1, MyBase.Width - (Me.int_0 * 4 + 2), MyBase.Height - (Me.int_0 * 4 + 2))
            Dim r As Byte = Me.ForeColor.R
            Dim g As Byte = Me.ForeColor.G
            Dim foreColor As Color = Me.ForeColor
            Me.TextBrush = New System.Drawing.SolidBrush(Color.FromArgb(216, CInt(r), CInt(g), CInt(foreColor.B)))
            MyBase.Invalidate()
        End If
    End Sub

    Public Shared Sub Draw3DBorder(ByVal targetControl As Control, ByVal graphics As System.Drawing.Graphics, ByVal color As System.Drawing.Color, ByVal borderWidth As Integer)
        Dim point() As System.Drawing.Point = {New System.Drawing.Point(0, 0), New System.Drawing.Point(0, targetControl.Height), New System.Drawing.Point(borderWidth, targetControl.Height - borderWidth), New System.Drawing.Point(borderWidth, borderWidth), New System.Drawing.Point(targetControl.Width - borderWidth, borderWidth), New System.Drawing.Point(targetControl.Width, 0)}
        Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(BeveledButtonDisplay.GetRelativeColor(color, 1.8))
            Using pen As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Color.FromArgb(128, 32, 32, 32), 1!)
                graphics.FillPolygon(solidBrush, point)
                graphics.DrawLine(pen, point(0), point(3))
            End Using
        End Using
        point(0).X = targetControl.Width
        point(0).Y = targetControl.Height
        point(3).X = targetControl.Width - borderWidth
        point(3).Y = targetControl.Height - borderWidth
        graphics.FillPolygon(New System.Drawing.SolidBrush(BeveledButtonDisplay.GetRelativeColor(color, 0.5)), point)
        graphics.DrawLine(New System.Drawing.Pen(System.Drawing.Color.FromArgb(128, 120, 120, 120), 1!), point(0), point(3))
        Dim linearGradientBrush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New System.Drawing.Point(0, 0), New System.Drawing.Point(targetControl.Width, targetControl.Height), BeveledButtonDisplay.GetRelativeColor(color, 1.2), BeveledButtonDisplay.GetRelativeColor(color, 2.1))
        Dim pen1 As System.Drawing.Pen = New System.Drawing.Pen(linearGradientBrush, CSng(borderWidth))
        graphics.DrawRectangle(pen1, CInt(Math.Floor(CDbl(borderWidth) * 1.5)), CInt(Math.Floor(CDbl(borderWidth) * 1.5)), targetControl.Width - borderWidth * 3, targetControl.Height - borderWidth * 3)
    End Sub

    Public Shared Function GetRelativeColor(ByVal color As System.Drawing.Color, ByVal intensity As Double) As System.Drawing.Color
        intensity = Math.Max(intensity, 0)
        Dim color1 As System.Drawing.Color = System.Drawing.Color.FromArgb(CInt(Math.Round(Math.Min(CDbl((color.R + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.G + 1)) * intensity, 255))), CInt(Math.Round(Math.Min(CDbl((color.B + 1)) * intensity, 255))))
        Return color1
    End Function

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overridable Sub OnBorderColorChanged(ByVal e As EventArgs)
        RaiseEvent BorderColorChanged(Me, e)
    End Sub

    Protected Overridable Sub OnBorderWidthChanged(ByVal e As EventArgs)
        RaiseEvent BorderWidthChanged(Me, e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Console.Beep()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.OnImage IsNot Nothing) Then
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            Me.stringFormat_0.Alignment = StringAlignment.Center
            If (Not Me.m_Value) Then
                painte.Graphics.DrawImage(Me.OffImage, 0, 0)
                painte.Graphics.DrawString(Me.Text, Me.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
            Else
                painte.Graphics.DrawImage(Me.OnImage, 0, 0)
                painte.Graphics.DrawString(Me.string_0, Me.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_0)
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        If (Operators.CompareString(Me.string_0, String.Empty, False) = 0) Then
            Me.string_0 = Me.Text
        End If
        MyBase.OnTextChanged(e)
    End Sub

    Public Event BorderColorChanged As EventHandler


    Public Event BorderWidthChanged As EventHandler

End Class
