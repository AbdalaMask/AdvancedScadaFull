
Option Infer On
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D


Public Class LedSingleControl
    Inherits Control
#Region "Declare variables"
    Private _Value As Boolean = True

    Private _OnColor As Color = Color.Lime

    Private _OffColor As Color = Color.DarkGray
#End Region

#Region "Contructors"
    Public Sub New()

    End Sub
#End Region
    <Category("HMI Properties")>
    Public Property Value() As Boolean
        Get
            Return _Value
        End Get

        Set(ByVal value As Boolean)
            _Value = value

            Me.Refresh()
        End Set
    End Property
    <Category("HMI Properties")>
    Public Property OnColor() As Color
        Get
            Return _OnColor
        End Get

        Set(ByVal value As Color)
            _OnColor = value
            Me.Refresh()
        End Set
    End Property
    <Category("HMI Properties")>
    Public Property OffColor() As Color
        Get
            Return _OffColor
        End Get

        Set(ByVal value As Color)
            _OffColor = value
            Me.Refresh()
        End Set
    End Property

#Region "Methods & Events"

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim g As Graphics = e.Graphics
        Dim pointF As New PointF(CSng(Me.Width) / 2.0F, CSng(Me.Height) / 2.0F)
        Dim num1 As Single = Math.Min(pointF.X, pointF.Y)
        Dim num2 As Single = CSng(CDbl(num1) * 65.0 / 100.0)
        Dim num3 As Single = CSng(CDbl(num1) * 55.0 / 100.0)
        Dim num4 As Single = CSng(CDbl(num1) * 45.0 / 100.0)
        Dim _Brush As Brush = CType(New LinearGradientBrush(New Point(CInt(Math.Truncate(CDbl(pointF.X) - CDbl(num2))), CInt(Math.Truncate(CDbl(pointF.Y) - CDbl(num2)))), New Point(CInt(Math.Truncate(CDbl(pointF.X) + CDbl(num2))), CInt(Math.Truncate(CDbl(pointF.Y) + CDbl(num2)))), Color.WhiteSmoke, SystemColors.ControlDarkDark), Brush)
        g.FillEllipse(_Brush, pointF.X - num2, pointF.Y - num2, 2.0F * num2, 2.0F * num2)
        _Brush.Dispose()
        If Me._Value Then
            Dim path As New GraphicsPath()
            path.AddEllipse(pointF.X - num1, pointF.Y - num1, num1 * 2.0F, num1 * 2.0F)
            Dim pathGradientBrush As New PathGradientBrush(path)
            pathGradientBrush.CenterColor = Color.FromArgb(150, CInt(Math.Truncate(Me.OnColor.R)), CInt(Math.Truncate(Me.OnColor.G)), CInt(Math.Truncate(Me.OnColor.B)))
            Dim colorArray() As Color = {Color.FromArgb(1, CInt(Math.Truncate(Me.OnColor.R)), CInt(Math.Truncate(Me.OnColor.G)), CInt(Math.Truncate(Me.OnColor.B)))}
            pathGradientBrush.SurroundColors = colorArray
            g.FillEllipse(CType(pathGradientBrush, Brush), pointF.X - num1, pointF.Y - num1, num1 * 2.0F, num1 * 2.0F)
            path.Dispose()
            pathGradientBrush.Dispose()
        End If
        Dim brush2 As Brush = CType(New LinearGradientBrush(New Point(CInt(Math.Truncate(CDbl(pointF.X) - CDbl(num3))), CInt(Math.Truncate(CDbl(pointF.Y) - CDbl(num3)))), New Point(CInt(Math.Truncate(CDbl(pointF.X) + CDbl(num3))), CInt(Math.Truncate(CDbl(pointF.Y) + CDbl(num2)))), SystemColors.ControlDarkDark, Color.WhiteSmoke), Brush)
        g.FillEllipse(brush2, pointF.X - num3, pointF.Y - num3, 2.0F * num3, 2.0F * num3)
        _Brush.Dispose()
        Dim gp As New GraphicsPath()
        gp.AddEllipse(pointF.X - num4, pointF.Y - num4, 2.0F * num4, 2.0F * num4)
        If Me._Value Then
            Dim pathGradientBrush As New PathGradientBrush(gp)
            pathGradientBrush.CenterColor = Color.WhiteSmoke
            Dim colorArray() As Color = {Me.OnColor}
            pathGradientBrush.SurroundColors = colorArray
            pathGradientBrush.CenterPoint = New PointF(pointF.X - num4 / 2.0F, pointF.Y - num4 / 2.0F)
            g.FillEllipse(CType(pathGradientBrush, Brush), pointF.X - num4, pointF.Y - num4, 2.0F * num4, 2.0F * num4)
            pathGradientBrush.Dispose()
        Else
            Dim pathGradientBrush As New PathGradientBrush(gp)
            pathGradientBrush.CenterColor = Color.WhiteSmoke
            Dim colorArray() As Color = {Me.OffColor}
            pathGradientBrush.SurroundColors = colorArray
            pathGradientBrush.CenterPoint = New PointF(pointF.X - num4 / 2.0F, pointF.Y - num4 / 2.0F)
            g.FillEllipse(CType(pathGradientBrush, Brush), pointF.X - num4, pointF.Y - num4, 2.0F * num4, 2.0F * num4)
            pathGradientBrush.Dispose()
        End If
        gp.Dispose()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Dim num As Single = CSng(Math.Min(Me.Width, Me.Height))
        If CDbl(num) < 20.0 Then
            num = 20.0F
        End If
        Me.Width = CInt(Math.Truncate(num))
        Me.Height = CInt(Math.Truncate(num))
        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, Me.Width, Me.Height)
        Me.Region = New Region(path)
    End Sub

#End Region

End Class
