Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Pipe
    Inherits Control
    Private bitmap_0 As Bitmap

    Private rectangle_0 As Rectangle

    Private solidBrush_0 As SolidBrush

    Private stringFormat_0 As StringFormat

    Private fittingType_0 As Pipe.FittingType

    Private rotateFlipType_0 As RotateFlipType

    Private bool_0 As Boolean

    Private bitmap_1 As Bitmap

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            If (MyBase.BackColor <> value) Then
                MyBase.BackColor = value
                Me.bool_0 = True
                Me.method_0()
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            If (Me.fittingType_0 <> Pipe.FittingType.Straight) Then
                exStyle.ExStyle = exStyle.ExStyle Or 32
            End If
            Return exStyle
        End Get
    End Property

    Public Property Fitting As Pipe.FittingType
        Get
            Return Me.fittingType_0
        End Get
        Set(ByVal value As Pipe.FittingType)
            If (Me.fittingType_0 <> value) Then
                Me.fittingType_0 = value
                Me.bool_0 = True
                Me.method_0()
            End If
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (Me.solidBrush_0 IsNot Nothing) Then
                Me.solidBrush_0.Color = value
            Else
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            MyBase.ForeColor = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Rotation As RotateFlipType
        Get
            Return Me.rotateFlipType_0
        End Get
        Set(ByVal value As RotateFlipType)
            If (value <> Me.rotateFlipType_0) Then
                Me.rotateFlipType_0 = value
                Me.bool_0 = True
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
            MyBase.Invalidate()
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.rotateFlipType_0 = RotateFlipType.RotateNoneFlipNone
        MyBase.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    <DebuggerNonUserCode>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Me.bitmap_1.Dispose()
        Me.bitmap_0.Dispose()
        Me.stringFormat_0.Dispose()
        Try
            Me.solidBrush_0.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        Dim horizontalPipe As Image
        Dim flag As Boolean = False
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Select Case Me.fittingType_0
                Case Pipe.FittingType.Straight
                    horizontalPipe = My.Resources.HorizontalPipe
                    Exit Select
                Case Pipe.FittingType.Elbow
                    horizontalPipe = My.Resources.Elbow1
                    Exit Select
                Case Pipe.FittingType.Tee
                    horizontalPipe = My.Resources.Tee1
                    Exit Select
                Case Pipe.FittingType.Cross
                    horizontalPipe = My.Resources.Cross
                    Exit Select
                Case Else
                    horizontalPipe = My.Resources.HorizontalPipe
                    Exit Select
            End Select
            If (Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipX Or Me.rotateFlipType_0 = RotateFlipType.Rotate270FlipNone Or Me.rotateFlipType_0 = RotateFlipType.Rotate90FlipNone) Then
                flag = True
            End If
            If (Me.bitmap_1 IsNot Nothing) Then
                Me.bitmap_1.Dispose()
            End If
            Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
            If (flag) Then
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(MyBase.Height), Convert.ToInt32(MyBase.Width))
            Else
                Me.bitmap_0 = New Bitmap(Convert.ToInt32(MyBase.Width), Convert.ToInt32(MyBase.Height))
            End If
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_0)
            If (Me.BackColor <> Color.Transparent) Then
                graphic.Clear(Me.BackColor)
            End If
            If (flag) Then
                graphic.DrawImage(horizontalPipe, 0, 0, MyBase.Height, MyBase.Width)
            Else
                graphic.DrawImage(horizontalPipe, 0, 0, MyBase.Width, MyBase.Height)
            End If
            Me.bitmap_0.RotateFlip(Me.rotateFlipType_0)
            Me.rectangle_0.X = 1
            Me.rectangle_0.Width = MyBase.Width - 2
            Me.rectangle_0.Y = 1
            Me.rectangle_0.Height = MyBase.Height - 2
            Me.stringFormat_0 = New StringFormat() With
            {
                .Alignment = StringAlignment.Center,
                .LineAlignment = StringAlignment.Center
            }
            If (Me.solidBrush_0 Is Nothing) Then
                Me.solidBrush_0 = New SolidBrush(MyBase.ForeColor)
            End If
            graphic.Dispose()
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If (MyBase.DesignMode) Then
            If (If(MyBase.Text.Length <= 3, False, Operators.CompareString(MyBase.Text.Substring(0, 4).ToUpper(), "PIPE", False) = 0)) Then
                MyBase.Text = String.Empty
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Not (Me.bitmap_0 Is Nothing Or Me.bitmap_1 Is Nothing) And Me.solidBrush_0 IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
            graphic.DrawImage(Me.bitmap_0, 0, 0)
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.solidBrush_0.Color <> MyBase.ForeColor) Then
                    Me.solidBrush_0.Color = MyBase.ForeColor
                End If
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_0, Me.rectangle_0, Me.stringFormat_0)
            End If
            painte.Graphics.DrawImageUnscaled(Me.bitmap_1, 0, 0)
            graphic.Dispose()
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        If (Me.bool_0) Then
            MyBase.OnPaintBackground(pevent)
            Me.bool_0 = False
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.method_0()
    End Sub

    Public Enum FittingType
        Straight
        Elbow
        Tee
        Cross
    End Enum
End Class
