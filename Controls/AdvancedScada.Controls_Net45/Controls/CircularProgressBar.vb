Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class CircularProgressBar
    Inherits Control

    Public Event ValueChanged As EventHandler

    Private CenterPoint As New Point
    Private ScaleValue As Decimal

#Region "Constructor"
    Public Sub New()
        MyBase.New

        '* reduce the flicker
        SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

    End Sub
#End Region

#Region "Properties"

    '*****************************************
    '* Property - Progress Bar Value
    '*****************************************
    Public Property PLCAddressValue As String
    Private m_value As Integer = 0
    ''' <summary>
    ''' Progrss bar current value
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Progrss bar current value")>
    Public Property Value As Integer
        Get
            Return m_value
        End Get
        Set(ByVal value As Integer)
            If value >= Minimum AndAlso value <= Maximum Then
                m_value = value
                Dim Rate As Decimal = 100 / (Maximum - Minimum)
                ScaleValue = (value - Minimum) * Rate
                Invalidate()
                OnValueChanged()
            End If
        End Set
    End Property

    Private m_Minimum As Integer = 0
    ''' <summary>
    ''' Minimum value for the progress bar, this can be negative
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Minimum value for the progress bar, this can be negative")>
    Public Property Minimum As Integer
        Get
            Return m_Minimum
        End Get
        Set(ByVal value As Integer)
            m_Minimum = value
            Invalidate()
        End Set
    End Property

    Private m_Maximum As Integer = 100
    ''' <summary>
    ''' Maximum value for the progress bar
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Maximum value for the progress bar")>
    Public Property Maximum As Integer
        Get
            Return m_Maximum
        End Get
        Set(ByVal value As Integer)
            m_Maximum = value
            Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Progress Bar Value Suffix
    '*****************************************
    Private m_Suffix As String = "%"
    ''' <summary>
    ''' Suffix for progress bar current value
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Suffix for progress bar current value")>
    Public Property Suffix As String
        Get
            Return m_Suffix
        End Get
        Set(ByVal value As String)
            m_Suffix = value
            Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Progress Bar Pen Size
    '*****************************************
    Private m_PenSize As Integer = 4
    ''' <summary>
    ''' Progress bar pen size
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Progress bar pen size")>
    Public Property PenSize As Integer
        Get
            Return m_PenSize
        End Get
        Set(ByVal value As Integer)
            m_PenSize = value
            Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Progress Bar Value Visibility
    '*****************************************
    Private m_ShowValue As Boolean = True
    ''' <summary>
    ''' Progress bar visibilty, high equals invisible
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Progress bar visibilty, high equals invisible")>
    Public Property ShowValue As Boolean
        Get
            Return m_ShowValue
        End Get
        Set(ByVal value As Boolean)
            m_ShowValue = value
            Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Pen Back Color
    '*****************************************
    Private m_PenBackColor As Color = Color.White
    ''' <summary>
    ''' Progress bar backcolor
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Progress bar backcolor")>
    Public Property PenBackColor As Color
        Get
            Return m_PenBackColor
        End Get
        Set(ByVal value As Color)
            m_PenBackColor = value
            Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Pen Fore Color
    '*****************************************
    Private m_PenForeColor As Color = Color.Lime
    ''' <summary>
    ''' Progress bar forecolor
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Progress bar forecolor")>
    Public Property PenForeColor As Color
        Get
            Return m_PenForeColor
        End Get
        Set(ByVal value As Color)
            m_PenForeColor = value
            Invalidate()
        End Set
    End Property

#End Region

#Region "Events"

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        Dim rect As New Rectangle(CenterPoint.X, CenterPoint.Y, 1, 1)
        rect.Inflate((Math.Min(Me.Width, Me.Height) / 2) * 0.85, (Math.Min(Me.Width, Me.Height) / 2) * 0.85)

        Dim progressAngle = CSng(360 / 100 * ScaleValue)
        Dim remainderAngle = 360 - progressAngle

        Using progressPen As New Pen(PenForeColor, PenSize), remainderPen As New Pen(PenBackColor, PenSize)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.DrawArc(progressPen, rect, -90, progressAngle)
            g.DrawArc(remainderPen, rect, progressAngle - 90, remainderAngle)
        End Using

        If ShowValue Then
            Dim txt As String = m_value.ToString + Suffix
            Dim szF As SizeF = g.MeasureString(txt, Me.Font)
            Using B As New SolidBrush(Me.ForeColor)
                g.DrawString(txt, Me.Font, B, New Point(CenterPoint.X - szF.Width / 2, CenterPoint.Y - szF.Height / 2))
            End Using
        End If

    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        If Width > 0 And Height > 0 Then
            '* Limit the drawing region to allow other controls to go into the corner regions without flicker
            Dim CircularGagePath As New System.Drawing.Drawing2D.GraphicsPath
            CircularGagePath.AddEllipse(0, 0, Me.Width, Me.Height)

            Me.Region = New Drawing.Region(CircularGagePath)
        Else
            Me.Region = Nothing
        End If

        '* Calculate this so it is not recalculated every paint event
        CenterPoint = New Point(Me.Width / 2, Me.Height / 2)
    End Sub

    Protected Overridable Sub OnValueChanged()
        RaiseEvent ValueChanged(Me, System.EventArgs.Empty)
    End Sub

#End Region

End Class
