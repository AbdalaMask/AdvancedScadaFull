Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class BarMeter
    Inherits Control
    Private point_0 As Point()

    Private rectangle_0 As Rectangle()

    Private solidBrush_0 As SolidBrush

    Private pen_0 As Pen

    Protected m_Maximum As Double

    Protected m_Minimum As Double

    Private double_0 As Double

    Private double_1 As Double

    Private double_2 As Double

    Private double_3 As Double

    Private int_0 As Integer

    Public Property ArrowColor As Color
        Get
            Return Me.solidBrush_0.Color
        End Get
        Set(ByVal value As Color)
            Me.solidBrush_0.Color = value
            Me.pen_0.Color = value
        End Set
    End Property

    Public Property ArrowWidth As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (Me.int_0 <> value) Then
                Me.int_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property BarHighAlarm_SetPoint As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_2) Then
                Me.double_2 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property BarHighWarningSetPoint As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_1) Then
                Me.double_1 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property BarLowSetPoint As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_0) Then
                Me.double_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.m_Maximum
        End Get
        Set(ByVal value As Double)
            If (Me.m_Maximum <> value) Then
                Me.m_Maximum = value
                MyBase.Invalidate()
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
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_3
        End Get
        Set(ByVal value As Double)
            If (value <> Me.double_3) Then
                If (If(value < Me.double_1, False, Me.double_3 < Me.double_1)) Then
                    Me.OnWarningValueExceeded(EventArgs.Empty)
                End If
                If (If(value < Me.double_2, False, Me.double_3 < Me.double_2)) Then
                    Me.OnAlarmValueExceeded(EventArgs.Empty)
                End If
                Me.double_3 = value
                Me.method_1()
                MyBase.Invalidate()
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        ReDim Me.point_0(2)
        ReDim Me.rectangle_0(4)
        Me.m_Maximum = 100
        Me.double_0 = 25
        Me.double_1 = 50
        Me.double_2 = 75
        Me.int_0 = 15
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Dim num As Integer = 0
        Do
            Me.rectangle_0(num) = New Rectangle()
            num = num + 1
        Loop While num <= 3
        Me.solidBrush_0 = New SolidBrush(Color.White)
        Me.pen_0 = New Pen(Color.White)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If (disposing) Then
            If (Me.solidBrush_0 IsNot Nothing) Then
                Me.solidBrush_0.Dispose()
            End If
            If (Me.pen_0 IsNot Nothing) Then
                Me.pen_0.Dispose()
            End If
        End If
    End Sub

    Private Sub method_0()
        Dim int0 As Double = CDbl(Me.int_0)
        Dim width As Double = CDbl((MyBase.Width - Me.int_0))
        Dim double3 As Double = (Me.double_3 - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum) * (CDbl(MyBase.Height) - int0)
        Me.point_0(0) = New Point(CInt(Math.Round(width)), CInt(Math.Round(double3)))
        Me.point_0(1) = New Point(CInt(Math.Round(width + CDbl(Me.ArrowWidth))), CInt(Math.Round(double3 + int0 / 2)))
        Me.point_0(2) = New Point(CInt(Math.Round(width + CDbl(Me.ArrowWidth))), CInt(Math.Round(double3 + -int0 / 2)))
        Me.rectangle_0(0).Width = Math.Max(MyBase.Width - Me.int_0, 2)
        Me.rectangle_0(0).Height = CInt(Math.Round(CDbl(MyBase.Height) - int0))
        Me.rectangle_0(0).Y = CInt(Math.Round(int0 / 2))
        Me.rectangle_0(1).Width = Me.rectangle_0(0).Width
        Me.rectangle_0(1).Y = Me.rectangle_0(0).Y
        Me.rectangle_0(1).Height = CInt(Math.Round(CDbl(Me.rectangle_0(0).Height) - (Me.double_0 - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum) * (CDbl(MyBase.Height) - int0)))
        Me.rectangle_0(2).Width = Me.rectangle_0(0).Width
        Me.rectangle_0(2).Y = Me.rectangle_0(0).Y
        Me.rectangle_0(2).Height = CInt(Math.Round(CDbl(Me.rectangle_0(0).Height) - (Me.double_1 - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum) * (CDbl(MyBase.Height) - int0)))
        Me.rectangle_0(3).Width = Me.rectangle_0(0).Width
        Me.rectangle_0(3).Y = Me.rectangle_0(0).Y
        Me.rectangle_0(3).Height = CInt(Math.Round(CDbl(Me.rectangle_0(0).Height) - (Me.double_2 - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum) * (CDbl(MyBase.Height) - int0)))
        Me.method_1()
        MyBase.Invalidate()
    End Sub

    Private Sub method_1()
        Dim num As Double = Math.Min(Me.m_Maximum, Me.double_3)
        num = Math.Max(num, Me.m_Minimum)
        Dim arrowWidth As Double = CDbl(Me.ArrowWidth)
        Dim mMinimum As Double = (num - Me.m_Minimum) / (Me.m_Maximum - Me.m_Minimum) * (CDbl(MyBase.Height) - arrowWidth)
        Dim height As Double = CDbl(MyBase.Height) - CDbl(Me.ArrowWidth) / 2 - mMinimum
        Me.point_0(0).Y = CInt(Math.Round(height))
        Me.point_0(1).Y = CInt(Math.Round(height + arrowWidth / 2))
        Me.point_0(2).Y = CInt(Math.Round(height + -arrowWidth / 2))
    End Sub

    Protected Overridable Sub OnAlarmValueExceeded(ByVal e As EventArgs)
        RaiseEvent AlarmValueExceeded(Me, e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        painte.Graphics.FillPolygon(Me.solidBrush_0, Me.point_0)
        painte.Graphics.FillRectangle(Brushes.Blue, Me.rectangle_0(0))
        painte.Graphics.FillRectangle(Brushes.Green, Me.rectangle_0(1))
        painte.Graphics.FillRectangle(Brushes.Yellow, Me.rectangle_0(2))
        painte.Graphics.FillRectangle(Brushes.Red, Me.rectangle_0(3))
        painte.Graphics.DrawLine(Me.pen_0, 0, Me.point_0(0).Y, Me.point_0(0).X, Me.point_0(0).Y)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.method_0()
        MyBase.Invalidate()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnWarningValueExceeded(ByVal e As EventArgs)
        RaiseEvent WarningValueExceeded(Me, e)
    End Sub

    Public Event AlarmValueExceeded As EventHandler


    Public Event ValueChanged As EventHandler


    Public Event WarningValueExceeded As EventHandler

End Class
