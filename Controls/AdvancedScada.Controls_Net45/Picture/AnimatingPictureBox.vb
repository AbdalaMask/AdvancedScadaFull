Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class AnimatingPictureBox
    Inherits PictureBox
    Private m_ImageRotationScale As RotationScale

    Private m_ImageRotationValue As Single

    Private m_ImageTranslateXScale As TranslationScale

    Private m_ImageTranslateXValue As Integer

    Private m_ImageTranslateYScale As TranslationScale

    Private m_ImageTranslateYValue As Integer

    Private m_ImageSizeXScale As TranslationScale

    Private m_ImageSizeXValue As Double

    Private m_ImageSizeYScale As TranslationScale

    Private m_ImageSizeYValue As Double

    Private m_Location As Point

    Private m_LocationOffsetX As Integer

    Private m_LocationOffsetY As Integer

    Private bitmap_0 As Bitmap

    Private matrix_0 As Matrix

    Private rectangle_0 As Rectangle

    Public Property ImageRotationScale As RotationScale
        Get
            Return Me.m_ImageRotationScale
        End Get
        Set(ByVal value As RotationScale)
            Me.m_ImageRotationScale = value
        End Set
    End Property

    Public Property ImageRotationValue As Single
        Get
            Return Me.m_ImageRotationValue
        End Get
        Set(ByVal value As Single)
            If (Me.m_ImageRotationValue <> value) Then
                Me.m_ImageRotationValue = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property ImageSizeXScale As TranslationScale
        Get
            Return Me.m_ImageSizeXScale
        End Get
        Set(ByVal value As TranslationScale)
            Me.m_ImageSizeXScale = value
        End Set
    End Property

    Public Property ImageSizeXValue As Double
        Get
            Return Me.m_ImageSizeXValue
        End Get
        Set(ByVal value As Double)
            If (Me.m_ImageSizeXValue <> value) Then
                Me.m_ImageSizeXValue = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property ImageSizeYScale As TranslationScale
        Get
            Return Me.m_ImageSizeYScale
        End Get
        Set(ByVal value As TranslationScale)
            Me.m_ImageSizeYScale = value
        End Set
    End Property

    Public Property ImageSizeYValue As Double
        Get
            Return Me.m_ImageSizeYValue
        End Get
        Set(ByVal value As Double)
            If (Me.m_ImageSizeYValue <> value) Then
                Me.m_ImageSizeYValue = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property ImageTranslateXScale As TranslationScale
        Get
            Return Me.m_ImageTranslateXScale
        End Get
        Set(ByVal value As TranslationScale)
            Me.m_ImageTranslateXScale = value
        End Set
    End Property

    Public Property ImageTranslateXValue As Integer
        Get
            Return Me.m_ImageTranslateXValue
        End Get
        Set(ByVal value As Integer)
            If (Me.m_ImageTranslateXValue <> value) Then
                Me.m_ImageTranslateXValue = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property ImageTranslateYScale As TranslationScale
        Get
            Return Me.m_ImageTranslateYScale
        End Get
        Set(ByVal value As TranslationScale)
            Me.m_ImageTranslateYScale = value
        End Set
    End Property

    Public Property ImageTranslateYValue As Integer
        Get
            Return Me.m_ImageTranslateYValue
        End Get
        Set(ByVal value As Integer)
            If (Me.m_ImageTranslateYValue <> value) Then
                Me.m_ImageTranslateYValue = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Shadows Property Location As Point
        Get
            Return Me.m_Location
        End Get
        Set(ByVal value As Point)
            Me.m_Location = value
            MyBase.SetBounds(Me.m_Location.X + Me.m_LocationOffsetX, Me.m_Location.Y, MyBase.Width, MyBase.Height, BoundsSpecified.Location)
        End Set
    End Property

    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property LocationOffsetX As Integer
        Get
            Return Me.m_LocationOffsetX
        End Get
        Set(ByVal value As Integer)
            If (Me.m_LocationOffsetX <> value) Then
                Me.m_LocationOffsetX = value
                MyBase.SetBounds(Me.m_Location.X + Me.m_LocationOffsetX, Me.m_Location.Y, MyBase.Width, MyBase.Height, BoundsSpecified.Location)
            End If
        End Set
    End Property

    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property LocationOffsetY As Integer
        Get
            Return Me.m_LocationOffsetY
        End Get
        Set(ByVal value As Integer)
            If (Me.m_LocationOffsetY <> value) Then
                Me.m_LocationOffsetY = value
                MyBase.SetBounds(Me.m_Location.X + Me.m_LocationOffsetX, Me.m_Location.Y + Me.m_LocationOffsetY, MyBase.Width, MyBase.Height, BoundsSpecified.Location)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.m_ImageRotationScale = New RotationScale()
        Me.m_ImageTranslateXScale = New TranslationScale()
        Me.m_ImageTranslateYScale = New TranslationScale()
        Me.m_ImageSizeXValue = 1
        Me.m_ImageSizeYValue = 1
        Me.matrix_0 = New Matrix()
        Me.BackColor = Color.Transparent
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.m_ImageSizeXScale = New TranslationScale() With
        {
            .InputMaxValue = 1,
            .OutputMaxValue = 1,
            .ErrorValue = 1
        }
        Me.m_ImageSizeYScale = New TranslationScale() With
        {
            .InputMaxValue = 1,
            .OutputMaxValue = 1,
            .ErrorValue = 1
        }
    End Sub

    Private Sub method_0()
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        If (MyBase.Image IsNot Nothing) Then
            If (If(Me.bitmap_0 Is Nothing OrElse Me.bitmap_0.Width <> MyBase.Image.Width, True, Me.bitmap_0.Height <> MyBase.Image.Height)) Then
                Me.bitmap_0 = New Bitmap(MyBase.Width, MyBase.Height)
                Me.bitmap_0.SetResolution(MyBase.Image.HorizontalResolution, MyBase.Image.VerticalResolution)
            End If
            If (Me.m_ImageTranslateXScale IsNot Nothing) Then
                num = CInt(Math.Round(Me.m_ImageTranslateXScale.GetValue(CDbl(Me.m_ImageTranslateXValue))))
            End If
            If (Me.m_ImageTranslateYScale IsNot Nothing) Then
                num1 = CInt(Math.Round(Me.m_ImageTranslateXScale.GetValue(CDbl(Me.m_ImageTranslateYValue))))
            End If
            Me.rectangle_0 = New Rectangle(0, 0, MyBase.Width, MyBase.Height)
            Using matrix0 As Graphics = Graphics.FromImage(Me.bitmap_0)
                Me.matrix_0.Reset()
                matrix0.Clear(Color.Transparent)
                Dim value As Double = Me.m_ImageSizeXScale.GetValue(Me.m_ImageSizeXValue)
                Dim value1 As Double = Me.m_ImageSizeYScale.GetValue(Me.m_ImageSizeYValue)
                If (value = 0) Then
                    value = 1
                End If
                If (value1 = 0) Then
                    value1 = 1
                End If
                Me.matrix_0.RotateAt(Me.ImageRotationScale.GetAngle(Me.m_ImageRotationValue), New Point(CInt(Math.Round(CDbl(MyBase.Width) / 2 + CDbl(Me.ImageRotationScale.XPosition) * value + CDbl(num) * value)), CInt(Math.Round(CDbl(MyBase.Height) / 2 + CDbl(Me.ImageRotationScale.YPosition) * value1 + CDbl(num1) * value1))))
                Me.matrix_0.Translate(CSng(((CDbl(MyBase.Width) - CDbl(MyBase.Image.Width) * value) / 2)), CSng(((CDbl(MyBase.Height) - CDbl(MyBase.Image.Height) * value1) / 2)))
                Me.matrix_0.Scale(CSng(value), CSng(value1))
                matrix0.Transform = Me.matrix_0
                matrix0.DrawImage(MyBase.Image, New Point(num, num1))
            End Using
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim rectangle As System.Drawing.Rectangle
        Dim flag As Boolean
        Dim flag1 As Boolean
        Try
            If (MyBase.Image Is Nothing) Then
                flag = False
            ElseIf (Me.bitmap_0 Is Nothing) Then
                flag = True
            Else
                Dim rectangle0 As System.Drawing.Rectangle = Me.rectangle_0
                rectangle = New System.Drawing.Rectangle()
                flag = rectangle0 = rectangle
            End If
            If (flag) Then
                Me.method_0()
            End If
            If (Me.bitmap_0 Is Nothing) Then
                flag1 = False
            Else
                Dim rectangle01 As System.Drawing.Rectangle = Me.rectangle_0
                rectangle = New System.Drawing.Rectangle()
                flag1 = rectangle01 <> rectangle
            End If
            If (flag1) Then
                painte.Graphics.DrawImage(Me.bitmap_0, Me.rectangle_0)
            End If
        Catch ex As System.Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        Me.method_0()
        MyBase.OnSizeChanged(e)
    End Sub
End Class
