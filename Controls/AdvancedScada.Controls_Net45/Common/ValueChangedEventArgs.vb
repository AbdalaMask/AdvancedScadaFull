Imports System
Imports System.Linq

Public Class ValueChangedEventArgs
    Inherits EventArgs

    Private m_NewValue As Long

    Private m_OldValue As Long

    Private m_BitNumber As Integer

    Public Property BitNumber() As Integer
        Get
            Return Me.m_BitNumber
        End Get
        Set(ByVal value As Integer)
            Me.m_BitNumber = value
        End Set
    End Property

    Public Property NewValue() As Long
        Get
            Return Me.m_NewValue
        End Get
        Set(ByVal value As Long)
            Me.m_NewValue = value
        End Set
    End Property

    Public Property OldValue() As Long
        Get
            Return Me.m_OldValue
        End Get
        Set(ByVal value As Long)
            Me.m_OldValue = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal newValue As Long)
        Me.m_NewValue = newValue
    End Sub

    Public Sub New(ByVal newValue As Long, ByVal oldValue As Long)
        Me.New(newValue)
        Me.m_OldValue = oldValue
    End Sub

    Public Sub New(ByVal newValue As Long, ByVal oldValue As Long, ByVal bitNumber As Integer)
        Me.New(newValue, oldValue)
        Me.m_BitNumber = bitNumber
    End Sub
End Class

