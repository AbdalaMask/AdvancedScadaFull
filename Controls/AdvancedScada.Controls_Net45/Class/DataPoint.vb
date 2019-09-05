Public Class DataPoint

    Private m_Timestamp As Date
    Public Property Timestamp As Date
        Get
            Return m_Timestamp
        End Get
        Set(value As Date)
            m_Timestamp = value
        End Set
    End Property

    Private m_Points As New System.Collections.ObjectModel.Collection(Of Single)
    Public ReadOnly Property Points As System.Collections.ObjectModel.Collection(Of Single)
        Get
            Return m_Points
        End Get
    End Property

End Class
