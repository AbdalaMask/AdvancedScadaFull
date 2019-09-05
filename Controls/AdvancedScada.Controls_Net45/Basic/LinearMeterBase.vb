Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class LinearMeterBase
    Inherits AnalogMeterBase
    Protected Friend BarRectangle As Rectangle

    Protected Friend m_FillType As LinearMeterBase.FillTypeOption

    Protected Friend m_FillColor As Color

    Protected Friend m_FillColorInRange As Color

    Protected Friend m_FillAreaBackcolor As Color

    Protected Friend m_BorderColor As Color

    Protected Friend m_BorderWidth As Integer

    Protected Friend m_NumericFormat As String

    Protected Friend m_CenterTargetValue As Boolean

    Protected Friend m_TargetValue As Double

    Protected Friend m_ScaleTargetValue As Boolean

    Protected Friend m_TolerancePlus As Double

    Protected Friend m_ToleranceMinus As Double

    Protected Friend m_MajorDivisions As Integer

    Protected Friend m_MinorDivisions As Integer

    Protected Friend m_ShowValidRangeMarker As Boolean

    Protected Friend m_ShowValue As Boolean

    Public Property BorderColor As Color
        Get
            Return Me.m_BorderColor
        End Get
        Set(ByVal value As Color)
            If (Me.m_BorderColor <> value) Then
                Me.m_BorderColor = value
                Me.CreateStaticImage()
                Me.OnBorderColorChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Property BorderWidth As Integer
        Get
            Return Me.m_BorderWidth
        End Get
        Set(ByVal value As Integer)
            If (Me.m_BorderWidth <> value) Then
                Me.m_BorderWidth = Math.Max(0, value)
                Me.m_BorderWidth = Math.Min(Me.m_BorderWidth, 20)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property CenterTargetValue As Boolean
        Get
            Return Me.m_CenterTargetValue
        End Get
        Set(ByVal value As Boolean)
            If (Me.m_CenterTargetValue <> value) Then
                Me.m_CenterTargetValue = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property FillAreaBackcolor As Color
        Get
            Return Me.m_FillAreaBackcolor
        End Get
        Set(ByVal value As Color)
            If (Me.m_FillAreaBackcolor <> value) Then
                Me.m_FillAreaBackcolor = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property FillColor As Color
        Get
            Return Me.m_FillColor
        End Get
        Set(ByVal value As Color)
            If (Me.m_FillColor <> value) Then
                Me.m_FillColor = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property FillColorInRange As Color
        Get
            Return Me.m_FillColorInRange
        End Get
        Set(ByVal value As Color)
            If (Me.m_FillColorInRange <> value) Then
                Me.m_FillColorInRange = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property FillType As LinearMeterBase.FillTypeOption
        Get
            Return Me.m_FillType
        End Get
        Set(ByVal value As LinearMeterBase.FillTypeOption)
            If (Me.m_FillType <> value) Then
                Me.m_FillType = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property MajorDivisions As Integer
        Get
            Return Me.m_MajorDivisions
        End Get
        Set(ByVal value As Integer)
            If (Me.m_MajorDivisions <> value) Then
                Me.m_MajorDivisions = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property MinorDivisions As Integer
        Get
            Return Me.m_MinorDivisions
        End Get
        Set(ByVal value As Integer)
            If (Me.m_MinorDivisions <> value) Then
                Me.m_MinorDivisions = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property NumericFormat As String
        Get
            Return Me.m_NumericFormat
        End Get
        Set(ByVal value As String)
            Me.m_NumericFormat = value
        End Set
    End Property

    Public Property ScaleTargetValue As Boolean
        Get
            Return Me.m_ScaleTargetValue
        End Get
        Set(ByVal value As Boolean)
            Me.m_ScaleTargetValue = value
        End Set
    End Property

    Public Property ShowValidRangeMarker As Boolean
        Get
            Return Me.m_ShowValidRangeMarker
        End Get
        Set(ByVal value As Boolean)
            If (Me.m_ShowValidRangeMarker <> value) Then
                Me.m_ShowValidRangeMarker = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property ShowValue As Boolean
        Get
            Return Me.m_ShowValue
        End Get
        Set(ByVal value As Boolean)
            If (Me.m_ShowValue <> value) Then
                Me.m_ShowValue = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property TargetValue As Double
        Get
            Return Me.m_TargetValue
        End Get
        Set(ByVal value As Double)
            If (Me.m_TargetValue <> value) Then
                Me.m_TargetValue = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property ToleranceMinus As Double
        Get
            Return Me.m_ToleranceMinus
        End Get
        Set(ByVal value As Double)
            If (Me.m_ToleranceMinus <> value) Then
                Me.m_ToleranceMinus = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property TolerancePlus As Double
        Get
            Return Me.m_TolerancePlus
        End Get
        Set(ByVal value As Double)
            If (Me.m_TolerancePlus <> value) Then
                Me.m_TolerancePlus = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.m_FillType = LinearMeterBase.FillTypeOption.Fill
        Me.m_FillColor = Color.FromArgb(128, 16, 16)
        Me.m_FillColorInRange = Color.FromArgb(16, 128, 16)
        Me.m_FillAreaBackcolor = Color.LightGray
        Me.m_BorderColor = Color.DimGray
        Me.m_BorderWidth = 8
        Me.m_TargetValue = 50
        Me.m_ScaleTargetValue = True
        Me.m_TolerancePlus = 10
        Me.m_ToleranceMinus = 10
        Me.m_MajorDivisions = 2
        Me.m_MinorDivisions = 5
        Me.m_ShowValidRangeMarker = True
        Me.m_ShowValue = True
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Protected Overrides Sub CreateStaticImage()
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnBackgroundImageChanged(ByVal e As EventArgs)
        MyBase.OnBackgroundImageChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnBackgroundImageLayoutChanged(ByVal e As EventArgs)
        MyBase.OnBackgroundImageLayoutChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overridable Sub OnBorderColorChanged(ByVal e As EventArgs)
        RaiseEvent BorderColorChanged(Me, e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        painte.Graphics.DrawImage(Me.StaticImage, 0, 0)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        MyBase.Invalidate(Me.BarRectangle)
    End Sub

    Public Event BorderColorChanged As EventHandler


    Public Enum FillTypeOption
        Fill
        WideBand
        NarrowBand
    End Enum
End Class
