Imports System

Public Class KeypadEventArgs
    Inherits EventArgs
    Private m_Key As String

    Public ReadOnly Property Key As String
        Get
            Return Me.m_Key
        End Get
    End Property

    Public Sub New(ByVal Key As String)
        MyBase.New()
        Me.m_Key = Key
    End Sub
End Class
