Imports System

Public Class ListButtonsEventArgs
    Inherits EventArgs
    Private string_0 As String

    Public ReadOnly Property ButtonText As String
        Get
            Return Me.string_0
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal buttonText As String)
        MyClass.New()
        Me.string_0 = buttonText
    End Sub
End Class
