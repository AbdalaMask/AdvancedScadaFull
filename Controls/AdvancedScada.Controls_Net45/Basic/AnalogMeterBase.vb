Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public MustInherit Class AnalogMeterBase
    Inherits Control
    Protected StaticImage As Bitmap

    Protected NeedleImage As Bitmap

    Protected ImageRatio As Double

    Protected TextRectangle As Rectangle

    Protected stringFormat_0 As StringFormat

    Protected TextBrush As SolidBrush

    Protected m_BaseImage As Bitmap

    Protected m_Value As Double

    Protected m_Maximum As Double

    Protected m_Minimum As Double

    Protected m_ValueScaleFactor As Double

    Private int_0 As Integer

    Private int_1 As Integer

    Protected Property BaseImage As Bitmap
        Get
            Return Me.m_BaseImage
        End Get
        Set(ByVal value As Bitmap)
            Me.m_BaseImage = value
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.m_Maximum
        End Get
        Set(ByVal value As Double)
            If (Me.m_Maximum <> value) Then
                Me.m_Maximum = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.m_Minimum
        End Get
        Set(ByVal value As Double)
            If (Me.m_Minimum <> value) Then
                Me.m_Minimum = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Overridable Property Value As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If (value <> Me.m_Value) Then
                Me.m_Value = value
                Me.OnValueChanged(EventArgs.Empty)
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Category("Numeric Display")>
    Public Property ValueScaleFactor As Double
        Get
            Return Me.m_ValueScaleFactor
        End Get
        Set(ByVal value As Double)
            If (value <> Me.m_ValueScaleFactor) Then
                Me.m_ValueScaleFactor = value
                If (Me.StaticImage IsNot Nothing) Then
                    MyBase.Invalidate()
                End If
            End If
        End Set
    End Property

    Protected Sub New()
        MyBase.New()
        Me.m_Maximum = 100
        Me.m_ValueScaleFactor = 1
        Me.ForeColor = Color.LightGray
        Me.stringFormat_0 = New StringFormat() With
        {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.method_1()
    End Sub

    Protected MustOverride Sub CreateStaticImage()

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
                If (Me.NeedleImage IsNot Nothing) Then
                    Me.NeedleImage.Dispose()
                End If
                If (Me.TextBrush IsNot Nothing) Then
                    Me.TextBrush.Dispose()
                End If
                If (Me.stringFormat_0 IsNot Nothing) Then
                    Me.stringFormat_0.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub method_0()
        If (MyBase.Height <> Me.int_1 Or MyBase.Width <> Me.int_0) Then
            Me.int_0 = MyBase.Width
            Me.int_1 = MyBase.Height
            Me.CreateStaticImage()
        End If
    End Sub

    Private Sub method_1()
        If (Me.BaseImage IsNot Nothing) Then
            Dim height As Double = CDbl(Me.m_BaseImage.Height) / CDbl(Me.m_BaseImage.Width)
            If (Me.int_1 < MyBase.Height Or Me.int_0 < MyBase.Width) Then
                If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= height) Then
                    MyBase.Height = Convert.ToInt32(CDbl(MyBase.Width) * height)
                Else
                    MyBase.Width = Convert.ToInt32(CDbl(MyBase.Height) / height)
                End If
            ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= height) Then
                MyBase.Width = Convert.ToInt32(CDbl(MyBase.Height) / height)
            Else
                MyBase.Height = Convert.ToInt32(CDbl(MyBase.Width) * height)
            End If
        End If
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.TextBrush IsNot Nothing) Then
            Me.TextBrush.Color = MyBase.ForeColor
        Else
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        Me.method_0()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
