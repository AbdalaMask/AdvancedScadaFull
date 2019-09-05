Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class LinearMeter
    Inherits AnalogMeterBase
    Private stringFormat_1 As StringFormat

    Private stringFormat_2 As StringFormat

    Private float_0 As Single

    Private float_1 As Single

    Private flatStyle_0 As System.Windows.Forms.FlatStyle

    Private hatchBrush_0 As HatchBrush

    Private pen_0 As Pen

    Private fillDirectionOption_0 As LinearMeter.FillDirectionOption

    Private int_2 As Integer

    Public Property BarContentColor As Color
        Get
            Return Me.hatchBrush_0.BackgroundColor
        End Get
        Set(ByVal value As Color)
            Me.hatchBrush_0 = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(240, Math.Max(value.R - 40, 0), Math.Max(value.G - 40, 0), Math.Max(value.B - 40, 0)), value)
            MyBase.Invalidate()
        End Set
    End Property

    Public Property BorderColor As Color
        Get
            Return Me.pen_0.Color
        End Get
        Set(ByVal value As Color)
            If (Me.pen_0 IsNot Nothing) Then
                Me.pen_0.Color = value
            Else
                Me.pen_0 = New Pen(value, 1!)
            End If
            MyBase.Invalidate()
        End Set
    End Property

    Public Property FillDirection As LinearMeter.FillDirectionOption
        Get
            Return Me.fillDirectionOption_0
        End Get
        Set(ByVal value As LinearMeter.FillDirectionOption)
            If (value <> Me.fillDirectionOption_0) Then
                Me.fillDirectionOption_0 = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property FlatStyle As System.Windows.Forms.FlatStyle
        Get
            Return Me.flatStyle_0
        End Get
        Set(ByVal value As System.Windows.Forms.FlatStyle)
            Me.flatStyle_0 = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            Me.CreateStaticImage()
            MyBase.Invalidate()
        End Set
    End Property

    Public Property MajorTicks As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_2) Then
                If (value >= 0) Then
                    Me.int_2 = value
                Else
                    Me.int_2 = 0
                End If
                Me.CreateStaticImage()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.stringFormat_1 = New StringFormat()
        Me.stringFormat_2 = New StringFormat()
        Me.hatchBrush_0 = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Red, Color.DarkOrange)
        Me.pen_0 = New Pen(Color.Wheat, 1!)
        Me.fillDirectionOption_0 = LinearMeter.FillDirectionOption.Up
        Me.ForeColor = Color.WhiteSmoke
        Me.stringFormat_1 = New StringFormat()
        Me.stringFormat_2 = New StringFormat()
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub CreateStaticImage()
        Me.TextRectangle.X = 1
        Me.TextRectangle.Y = 1
        Me.TextRectangle.Width = MyBase.Width - 2
        If (Me.int_2 > 0) Then
            Me.TextRectangle.Width = Convert.ToInt32(CDbl((MyBase.Width * 2)) / 3)
            Me.TextRectangle.X = Convert.ToInt32(CDbl(MyBase.Width) / 3)
        End If
        Me.TextRectangle.Height = MyBase.FontHeight + 4
        If (Me.stringFormat_2 Is Nothing) Then
            Me.stringFormat_2 = New StringFormat()
        End If
        Me.stringFormat_2.Alignment = StringAlignment.Center
        Me.stringFormat_2.LineAlignment = StringAlignment.Near
        Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        MyBase.Invalidate()
    End Sub

    <DebuggerNonUserCode>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                Me.hatchBrush_0.Dispose()
                Me.stringFormat_2.Dispose()
                Me.stringFormat_1.Dispose()
                Me.TextBrush.Dispose()
                Me.pen_0.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_2()
        Dim mMaximum As Single = CSng((1 / (Me.m_Maximum - Me.m_Minimum) * (Me.m_Value - Me.m_Minimum)))
        Me.float_0 = CSng((MyBase.Height - 2)) * mMaximum
        Me.float_1 = CSng((MyBase.Width - 2)) * mMaximum
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.TextBrush IsNot Nothing And Me.pen_0 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(MyBase.BackColor)
                graphics.FillRectangle(solidBrush, 0, 0, MyBase.Width, MyBase.Height)
            End Using
            Dim width As Integer = MyBase.Width
            If (Me.hatchBrush_0 IsNot Nothing) Then
                Select Case Me.fillDirectionOption_0
                    Case LinearMeter.FillDirectionOption.Down
                        graphics.FillRectangle(Me.hatchBrush_0, 0, Convert.ToInt32(Me.float_0) - 1, width, 2)
                        If (Me.float_0 + CSng(MyBase.FontHeight) >= CSng(MyBase.Height)) Then
                            Exit Select
                        End If
                        Me.TextRectangle.Y = CInt(Math.Round(CDbl((Me.float_0 - CSng(MyBase.FontHeight) - 2!))))
                        graphics.DrawString(Conversions.ToString(Me.m_Value), MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_2)
                        Exit Select
                    Case LinearMeter.FillDirectionOption.Left
                        graphics.FillRectangle(Me.hatchBrush_0, MyBase.Width - Convert.ToInt32(Me.float_1), 0, Convert.ToInt32(Me.float_1), MyBase.Height)
                        Exit Select
                    Case LinearMeter.FillDirectionOption.Right
                        graphics.FillRectangle(Me.hatchBrush_0, 0, 0, Convert.ToInt32(Me.float_1), MyBase.Height)
                        Exit Select
                    Case Else
                        graphics.FillRectangle(Me.hatchBrush_0, Convert.ToInt32(CDbl(MyBase.Width) / 3), MyBase.Height - Convert.ToInt32(Me.float_0) - 1, width, 2)
                        If (CSng(MyBase.Height) - Me.float_0 + CSng(MyBase.FontHeight) >= CSng(MyBase.Height)) Then
                            Exit Select
                        End If
                        Me.TextRectangle.Y = CInt(Math.Round(CDbl((CSng(MyBase.Height) - Me.float_0))))
                        graphics.DrawString(Conversions.ToString(Me.m_Value), MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.stringFormat_2)
                        Exit Select
                End Select
            End If
            If (Me.int_2 <> 0) Then
                graphics.DrawRectangle(Me.pen_0, Convert.ToInt32(CDbl(MyBase.Width) / 3), 0, width - 1, MyBase.Height - 1)
                If (Me.int_2 <= 1) Then
                    graphics.FillRectangle(Brushes.Wheat, 0, Convert.ToInt32(CDbl(MyBase.Height) / 2), Convert.ToInt32(CDbl(MyBase.Width) / 3) - 2, 2)
                Else
                    Dim num As Integer = CInt(Math.Round(CDbl((MyBase.Height - 2)) / CDbl((Me.int_2 - 1))))
                    Dim int2 As Integer = Me.int_2 - 1
                    For i As Integer = 0 To int2 Step 1
                        graphics.FillRectangle(Brushes.Wheat, 0, i * num, Convert.ToInt32(CDbl(MyBase.Width) / 3) - 2, 2)
                    Next

                End If
            Else
                graphics.DrawRectangle(Me.pen_0, 0, 0, MyBase.Width - 1, MyBase.Height - 1)
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.method_2()
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        If (MyBase.Parent IsNot Nothing) Then
            MyBase.Parent.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        Me.method_2()
        MyBase.Invalidate()
    End Sub

    Public Enum FillDirectionOption
        Up
        Down
        Left
        Right
    End Enum
End Class
