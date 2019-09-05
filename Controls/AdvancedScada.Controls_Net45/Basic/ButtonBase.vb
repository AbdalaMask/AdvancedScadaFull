Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public MustInherit Class ButtonBase
    Inherits Control
    Protected BackImage As Image

    Protected StaticImage As Image

    Protected OnImage As Image

    Protected OffImage As Image

    Protected ImageRatio As Double

    Protected TextRectangle As Rectangle

    Protected stringFormat_0 As StringFormat

    Protected TextBrush As SolidBrush

    Protected BaseImage As Image

    Protected BackBuffer As Bitmap

    Protected m_Value As Boolean

    Protected float_0 As Single

    Protected float_1 As Single

    Protected LastWidth As Integer

    Protected LastHeight As Integer

    Public Overridable Property Value As Boolean
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.m_Value) Then
                Me.m_Value = value
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Protected Sub New()
        MyBase.New()
        Me.stringFormat_0 = New StringFormat()
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.BackColor = Color.Transparent
    End Sub

    Protected Overridable Sub AdjustSize()
        If (Me.BaseImage IsNot Nothing) Then
            Me.ImageRatio = CDbl(Me.BaseImage.Height) / CDbl(Me.BaseImage.Width)
            If (Me.LastHeight < MyBase.Height Or Me.LastWidth < MyBase.Width) Then
                If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Me.ImageRatio) Then
                    MyBase.Height = Convert.ToInt32(CDbl(MyBase.Width) * Me.ImageRatio)
                Else
                    MyBase.Width = Convert.ToInt32(CDbl(MyBase.Height) / Me.ImageRatio)
                End If
            ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= Me.ImageRatio) Then
                MyBase.Width = Convert.ToInt32(CDbl(MyBase.Height) / Me.ImageRatio)
            Else
                MyBase.Height = Convert.ToInt32(CDbl(MyBase.Width) * Me.ImageRatio)
            End If
        End If
    End Sub

    Protected MustOverride Sub CreateStaticImage()

    Private Sub method_0()
        If (MyBase.Height <> Me.LastHeight Or MyBase.Width <> Me.LastWidth) Then
            Me.AdjustSize()
            Me.LastWidth = MyBase.Width
            Me.LastHeight = MyBase.Height
            If (MyBase.Width > 0 And MyBase.Height > 0) Then
                Me.CreateStaticImage()
            End If
        End If
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.CreateStaticImage()
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.TextBrush IsNot Nothing) Then
            Me.TextBrush.Color = MyBase.ForeColor
        Else
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.CreateStaticImage()
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If (Me.BackBuffer IsNot Nothing) Then
            Me.BackBuffer.Dispose()
            Me.BackBuffer = Nothing
        End If
        Me.method_0()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            Me.CreateStaticImage()
        End If
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
