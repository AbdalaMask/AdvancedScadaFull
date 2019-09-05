Imports System
Imports System.ComponentModel

<TypeConverter(GetType(TranslationScaleTypeConverter))>
Public Class TranslationScale
    Private double_0 As Double

    Private double_1 As Double

    Private double_2 As Double

    Private double_3 As Double

    Private double_4 As Double

    Public Property ErrorValue As Double
        Get
            Return Me.double_4
        End Get
        Set(ByVal value As Double)
            Me.double_4 = value
        End Set
    End Property

    Public Property InputMaxValue As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            Me.double_1 = value
        End Set
    End Property

    Public Property InputMinValue As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            Me.double_0 = value
        End Set
    End Property

    Public Property OutputMaxValue As Double
        Get
            Return Me.double_3
        End Get
        Set(ByVal value As Double)
            Me.double_3 = value
        End Set
    End Property

    Public Property OutputMinValue As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            Me.double_2 = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.double_1 = 100
        Me.double_3 = 100
    End Sub

    Public Sub New(ByVal inputMinValue As Double, ByVal inputMaxValue As Double, ByVal outputMinValue As Double, ByVal outputMaxValue As Double)
        MyClass.New()
        Me.double_0 = inputMinValue
        Me.double_1 = inputMaxValue
        Me.double_2 = outputMinValue
        Me.double_3 = outputMaxValue
    End Sub

    Public Function GetValue(ByVal value As Double) As Double
        Dim num As Double
        Dim double1 As Double = Me.double_1 - Me.double_0
        Dim double3 As Double = Me.double_3 - Me.double_2
        num = If(double1 = 0, Me.double_4, (value - Me.double_0) * (double3 / double1) + Me.double_2)
        Return num
    End Function
End Class
