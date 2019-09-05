Imports System

Public Class PanChartEventArgs
    Inherits EventArgs
    Private int_0 As Integer

    Private int_1 As Integer

    Private dateTime_0 As DateTime

    Public Property CurrentPoint As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            Me.int_1 = value
        End Set
    End Property

    Public ReadOnly Property CurrentPointDate As DateTime
        Get
            Return Me.dateTime_0
        End Get
    End Property

    Public Property NumberOfPointsOnChart As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
        End Set
    End Property

    Public Sub New(ByVal numberOfPointsOnChart As Integer, ByVal currentPoint As Integer, ByVal currentPointDate As DateTime)
        MyBase.New()
        Me.int_0 = numberOfPointsOnChart
        Me.int_1 = currentPoint
        Me.dateTime_0 = currentPointDate
    End Sub
End Class
