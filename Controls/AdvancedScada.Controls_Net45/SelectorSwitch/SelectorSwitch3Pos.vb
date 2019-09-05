Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Public Class SelectorSwitch3Pos
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private bitmap_4 As Bitmap

    Private rectangle_0 As Rectangle

    Private float_0 As Single

    Private int_0 As Integer

    Private int_1 As Integer

    Private int_2 As Integer

    Private int_3 As Integer

    Private legendPlates_0 As SelectorSwitch3Pos.LegendPlates

    Private stringFormat_0 As StringFormat

    Private solidBrush_0 As SolidBrush

    Private decimal_0 As Decimal

    Private int_4 As Integer

    Private int_5 As Integer

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    Public Property LegendPlate As SelectorSwitch3Pos.LegendPlates
        Get
            Return Me.legendPlates_0
        End Get
        Set(ByVal value As SelectorSwitch3Pos.LegendPlates)
            If (Me.legendPlates_0 <> value) Then
                Me.legendPlates_0 = value
                If (value <> SelectorSwitch3Pos.LegendPlates.Large) Then
                    MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * My.Resources.LegendPlateShort.Height)) / CDbl(My.Resources.LegendPlateShort.Width)))
                Else
                    MyBase.Height = CInt(Math.Round(CDbl((MyBase.Width * My.Resources.LegendPlate.Height)) / CDbl(My.Resources.LegendPlate.Width)))
                End If
                Me.OnSizeChanged(EventArgs.Empty)
                Me.method_1()
            End If
        End Set
    End Property

    ''   <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Dim num As Integer = value
            If (num = Me.int_1) Then
                Me.bitmap_1 = Me.bitmap_2
            ElseIf (num = Me.int_2) Then
                Me.bitmap_1 = Me.bitmap_3
            ElseIf (num <> Me.int_3) Then
                Me.bitmap_1 = Me.bitmap_3
            Else
                Me.bitmap_1 = Me.bitmap_4
            End If
            Me.int_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property ValueOfCenterPosition As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            If (Me.int_2 <> value) Then
                Me.int_2 = value
            End If
        End Set
    End Property

    Public Property ValueOfLeftPosition As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (Me.int_1 <> value) Then
                Me.int_1 = value
            End If
        End Set
    End Property

    Public Property ValueOfRightPosition As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            If (Me.int_3 <> value) Then
                Me.int_3 = value
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.MouseDown, New MouseEventHandler(AddressOf Me.SelectorSwitch3Pos_MouseDown)
        Me.rectangle_0 = New Rectangle()
        Me.int_0 = 1
        Me.int_1 = -1
        Me.int_2 = 0
        Me.int_3 = 1
        Me.legendPlates_0 = SelectorSwitch3Pos.LegendPlates.Large
        Me.stringFormat_0 = New StringFormat()
        Me.decimal_0 = New Decimal(CDbl(My.Resources.LegendPlate.Height) / CDbl(My.Resources.LegendPlate.Width))
        Me.BackgroundImageLayout = ImageLayout.Zoom
        Me.solidBrush_0 = New SolidBrush(Color.Black)
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Private Sub method_0()
        If (Me.legendPlates_0 <> SelectorSwitch3Pos.LegendPlates.Large) Then
            Me.decimal_0 = New Decimal(CSng(My.Resources.LegendPlateShort.Height) / CSng(My.Resources.LegendPlateShort.Width))
        Else
            Me.decimal_0 = New Decimal(CSng(My.Resources.LegendPlate.Height) / CSng(My.Resources.LegendPlate.Width))
        End If
    End Sub

    Private Sub method_1()
        Try
            Me.method_0()
            If (Not (Decimal.Compare(Me.decimal_0, Decimal.Zero) <= 0 Or Me.float_0 <= 0!)) Then
                If (Me.bitmap_0 IsNot Nothing) Then
                    Me.bitmap_0.Dispose()
                End If
                If (Me.legendPlates_0 <> SelectorSwitch3Pos.LegendPlates.Large) Then
                    Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.LegendPlateSmallWithNut.Width) * Me.float_0 * 2!), Convert.ToInt32(CSng(My.Resources.LegendPlateSmallWithNut.Height) * Me.float_0 * 2!))
                    Me.BackgroundImage = My.Resources.LegendPlateSmallWithNut
                Else
                    Me.bitmap_0 = New Bitmap(Convert.ToInt32(CSng(My.Resources.LegendPlateLargeWithNut.Width) * Me.float_0 * 2!), Convert.ToInt32(CSng(My.Resources.LegendPlateLargeWithNut.Height) * Me.float_0 * 2!))
                    Me.BackgroundImage = My.Resources.LegendPlateLargeWithNut
                End If
                Using graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
                    If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                        Me.rectangle_0.X = 0
                        Me.rectangle_0.Width = MyBase.Width - 1
                        Me.rectangle_0.Y = 0
                        If (Me.legendPlates_0 <> SelectorSwitch3Pos.LegendPlates.Large) Then
                            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.2))
                        Else
                            Me.rectangle_0.Height = CInt(Math.Round(CDbl(MyBase.Height) * 0.4))
                        End If
                        Me.stringFormat_0.Alignment = StringAlignment.Center
                        Me.stringFormat_0.LineAlignment = StringAlignment.Center
                        Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
                        graphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                        graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
                    End If
                End Using
                Me.bitmap_2 = New Bitmap(Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Width) * Me.float_0 * 2!), Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Height) * Me.float_0 * 2!))
                Me.bitmap_4 = New Bitmap(Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Width) * Me.float_0 * 2!), Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Height) * Me.float_0 * 2!))
                Me.bitmap_3 = New Bitmap(Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Width) * Me.float_0 * 2!), Convert.ToInt32(CSng(My.Resources.SelectorSwitchCenterNoNut.Height) * Me.float_0 * 2!))
                Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
                    Using graphic2 As Graphics = Graphics.FromImage(Me.bitmap_4)
                        Using graphic3 As Graphics = Graphics.FromImage(Me.bitmap_3)
                            Dim matrix As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                            Dim width As Double = CDbl(Me.bitmap_2.Width) / 2
                            Dim height As Double = CDbl(Me.bitmap_2.Height) / 2
                            matrix.RotateAt(-30!, New Point(Convert.ToInt32(width), Convert.ToInt32(height)), MatrixOrder.Prepend)
                            graphic1.Transform = matrix
                            graphic1.DrawImage(My.Resources.SelectorSwitchCenterNoNut, 0, 0, Me.bitmap_2.Width, Me.bitmap_2.Height)
                            matrix.Reset()
                            matrix.RotateAt(30!, New Point(Convert.ToInt32(width), Convert.ToInt32(height)), MatrixOrder.Prepend)
                            graphic2.Transform = matrix
                            graphic2.DrawImage(My.Resources.SelectorSwitchCenterNoNut, 0, 0, Me.bitmap_4.Width, Me.bitmap_4.Height)
                            graphic3.DrawImage(My.Resources.SelectorSwitchCenterNoNut, 0, 0, Me.bitmap_3.Width, Me.bitmap_3.Height)
                            Me.Value = Me.int_0
                        End Using
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
        Me.method_1()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.bitmap_0 IsNot Nothing And Me.bitmap_1 IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            graphics.DrawImage(Me.bitmap_0, 0, 0)
            If (Me.legendPlates_0 <> SelectorSwitch3Pos.LegendPlates.Large) Then
                graphics.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(MyBase.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(Me.bitmap_0.Height) * 0.45 - CDbl(Me.bitmap_1.Height) / 2))
            Else
                graphics.DrawImage(Me.bitmap_1, Convert.ToInt32(CDbl(MyBase.Width) / 2 - CDbl(Me.bitmap_1.Width) / 2), Convert.ToInt32(CDbl(MyBase.Height) * 0.68 - CDbl(Me.bitmap_1.Height) / 2))
            End If
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
        If (Me.int_5 < MyBase.Height Or Me.int_4 < MyBase.Width) Then
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
        Me.int_4 = MyBase.Width
        Me.int_5 = MyBase.Height
        Me.float_0 = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.LegendPlate.Width)))
        Me.method_1()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.method_1()
    End Sub

    Private Sub SelectorSwitch3Pos_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (CDbl(e.X) < CDbl(MyBase.Width) * 0.5) Then
            If (Me.int_0 = Me.int_2) Then
                Me.Value = Me.int_1
                RaiseEvent SwitchLeft(Me, EventArgs.Empty)
            ElseIf (Me.int_0 = Me.int_3) Then
                Me.Value = Me.int_2
                RaiseEvent SwitchCenter(Me, EventArgs.Empty)
            End If
        ElseIf (Me.int_0 = Me.int_2) Then
            Me.Value = Me.int_3
            RaiseEvent SwitchRight(Me, EventArgs.Empty)
        ElseIf (Me.int_0 = Me.int_1) Then
            Me.Value = Me.int_2
            RaiseEvent SwitchCenter(Me, EventArgs.Empty)
        End If
    End Sub

    Public Event SwitchCenter As EventHandler


    Public Event SwitchLeft As EventHandler


    Public Event SwitchRight As EventHandler


    Public Enum LegendPlates
        Large
        Small
    End Enum
End Class
