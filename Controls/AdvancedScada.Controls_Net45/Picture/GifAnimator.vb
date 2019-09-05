Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class GifAnimator
    Inherits Control
    Private eventHandler_0 As EventHandler

    Private m_Image As System.Drawing.Image

    Private m_Value As Boolean

    Public Property Image As System.Drawing.Image
        Get
            Return Me.m_Image
        End Get
        Set(ByVal value As System.Drawing.Image)
            Me.m_Image = value
            Me.Value = Me.m_Value
        End Set
    End Property

    Public Property Value As Boolean
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Boolean)
            Me.m_Value = value
            If (If(Me.m_Image Is Nothing, False, ImageAnimator.CanAnimate(Me.m_Image))) Then
                If (Not Me.m_Value) Then
                    ImageAnimator.StopAnimate(Me.m_Image, Me.eventHandler_0)
                Else
                    ImageAnimator.Animate(Me.m_Image, Me.eventHandler_0)
                End If
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.eventHandler_0 = New EventHandler(AddressOf Me.method_0)
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        If (Me.m_Image IsNot Nothing) Then
            ImageAnimator.UpdateFrames()
            e.Graphics.DrawImage(Me.m_Image, New Point(0, 0))
        End If
    End Sub
End Class
