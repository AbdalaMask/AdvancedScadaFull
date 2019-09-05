Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class GroupPanel
    Inherits Panel
    Private m_BackColor As Color

    Private m_BackColor2 As Color

    Private m_BackColor3 As Color

    Private m_SelectBackColor2 As Boolean

    Private m_SelectBackColor3 As Boolean

    Public Shadows Property BackColor As Color
        Get
            Return Me.m_BackColor
        End Get
        Set(ByVal value As Color)
            Me.m_BackColor = value
            Me.method_0()
        End Set
    End Property

    Public Property BackColor2 As Color
        Get
            Return Me.m_BackColor2
        End Get
        Set(ByVal value As Color)
            Me.m_BackColor2 = value
            Me.method_0()
        End Set
    End Property

    Public Property BackColor3 As Color
        Get
            Return Me.m_BackColor3
        End Get
        Set(ByVal value As Color)
            Me.m_BackColor3 = value
            Me.method_0()
        End Set
    End Property

    <Category("Appearance")>
    Public Property SelectBackColor2 As Boolean
        Get
            Return Me.m_SelectBackColor2
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.m_SelectBackColor2) Then
                Me.m_SelectBackColor2 = value
                Me.method_0()
                MyBase.Invalidate()
                Me.OnSelectBackColor2Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Appearance")>
    Public Property SelectBackColor3 As Boolean
        Get
            Return Me.m_SelectBackColor3
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.m_SelectBackColor3) Then
                Me.m_SelectBackColor3 = value
                Me.method_0()
                MyBase.Invalidate()
                Me.OnSelectBackColor2Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.m_BackColor = Color.Transparent
        Me.m_BackColor2 = Color.Green
        Me.m_BackColor3 = Color.Red
    End Sub

    Private Sub method_0()
        If (Me.m_SelectBackColor3) Then
            MyBase.BackColor = Me.m_BackColor3
        ElseIf (Not Me.m_SelectBackColor2) Then
            MyBase.BackColor = Me.m_BackColor
        Else
            MyBase.BackColor = Me.m_BackColor2
        End If
    End Sub

    Protected Overridable Sub OnSelectBackColor2Changed(ByVal e As EventArgs)
        RaiseEvent SelectBackColor2Changed(Me, e)
    End Sub

    Public Event SelectBackColor2Changed As EventHandler

End Class
