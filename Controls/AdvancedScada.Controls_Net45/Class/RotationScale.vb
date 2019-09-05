Imports System
Imports System.ComponentModel
Imports System.Drawing.Design

<Editor(GetType(RotationValueEditor), GetType(UITypeEditor))>
<TypeConverter(GetType(RotationTypeConverter))>
Public Class RotationScale
    Private float_0 As Single

    Private float_1 As Single

    Private float_2 As Single

    Private float_3 As Single

    Private int_0 As Integer

    Private int_1 As Integer

    Public Property RotationCcwAngle As Single
        Get
            Return Me.float_0
        End Get
        Set(ByVal value As Single)
            Me.float_0 = value
        End Set
    End Property

    Public Property RotationCwAngle As Single
        Get
            Return Me.float_1
        End Get
        Set(ByVal value As Single)
            Me.float_1 = value
        End Set
    End Property

    Public Property RotationMaxValueCcw As Single
        Get
            Return Me.float_2
        End Get
        Set(ByVal value As Single)
            Me.float_2 = value
        End Set
    End Property

    Public Property RotationMaxValueCw As Single
        Get
            Return Me.float_3
        End Get
        Set(ByVal value As Single)
            Me.float_3 = value
        End Set
    End Property

    Public Property XPosition As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
        End Set
    End Property

    Public Property YPosition As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            Me.int_1 = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.float_1 = 90!
        Me.float_2 = 0!
        Me.float_3 = 90!
    End Sub

    Public Sub New(ByVal rotationCCWAngle As Single, ByVal rotationCWAngle As Single, ByVal rotationMaxValueCCW As Single, ByVal rotationMaxValueCW As Single, ByVal xPosition As Integer, ByVal yPosition As Integer)
        MyClass.New()
        Me.float_0 = rotationCCWAngle
        Me.float_1 = rotationCWAngle
        Me.float_3 = rotationMaxValueCW
        Me.float_2 = rotationMaxValueCCW
        Me.int_0 = xPosition
        Me.int_1 = yPosition
    End Sub

    Public Function GetAngle(ByVal value As Single) As Single
        Dim float2 As Single = Me.float_2 - Me.float_3
        Dim float0 As Single = Me.float_0 - Me.float_1
        Return float0 / float2 * value + Me.float_0
    End Function
End Class
