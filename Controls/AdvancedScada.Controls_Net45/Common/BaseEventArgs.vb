Imports System

Namespace Common
    <CLSCompliant(True)>
    Public Class BaseEventArgs
        Inherits EventArgs
        Protected m_ErrorMessage As String

        Protected m_ErrorId As Integer

        Public Property ErrorId As Integer
            Get
                Return Me.m_ErrorId
            End Get
            Set(ByVal value As Integer)
                Me.m_ErrorId = value
            End Set
        End Property

        Public Property ErrorMessage As String
            Get
                Return Me.m_ErrorMessage
            End Get
            Set(ByVal value As String)
                Me.m_ErrorMessage = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal errorId As Integer, ByVal errorMessage As String)
            MyBase.New()
            Me.m_ErrorId = errorId
            Me.m_ErrorMessage = errorMessage
        End Sub
    End Class
End Namespace