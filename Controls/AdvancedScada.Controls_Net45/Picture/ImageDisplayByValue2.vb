Imports System
Imports System.Windows.Forms

Public Class ImageDisplayByValue2
    Inherits Control
    Private m_Images As ImageList.ImageCollection

    Private m_Value As Integer

    Public ReadOnly Property Images As ImageList.ImageCollection
        Get
            Return Me.m_Images

        End Get
    End Property

    Public Property Value As Integer
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Integer)
            If (Me.m_Value <> value) Then
                Me.m_Value = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        If (If(Me.m_Images Is Nothing, False, Me.m_Images.Count > 0)) Then
            Dim num As Integer = Math.Min(Me.m_Value, Me.m_Images.Count)
            num = Math.Max(0, num)
            painte.Graphics.DrawImage(Me.m_Images(num), 0, 0)
        End If
    End Sub
End Class
