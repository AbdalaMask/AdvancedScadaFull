Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Class RotationTypeConverter
    Inherits TypeConverter
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (sourceType.Equals(GetType(String)) Or sourceType.Equals(GetType(RotationScale))), MyBase.CanConvertFrom(context, sourceType), True)
        Return flag
    End Function

    Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (destinationType.Equals(GetType(String)) Or destinationType.Equals(GetType(RotationScale))), MyBase.CanConvertTo(context, destinationType), True)
        Return flag
    End Function

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object
        Dim rotationScale As Object
        If (Not TypeOf value Is String) Then
            rotationScale = MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value))
        Else
            Dim str As String = Conversions.ToString(value)
            Dim strArrays As String() = str.Split(New Char() {","C})
            Try
                rotationScale = New RotationScale(Conversions.ToSingle(strArrays(0)), Conversions.ToSingle(strArrays(1)), Conversions.ToSingle(strArrays(2)), Conversions.ToSingle(strArrays(3)), Conversions.ToInteger(strArrays(4)), Conversions.ToInteger(strArrays(5)))
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                Throw New InvalidCastException(Conversions.ToString(value))
            End Try
        End If
        Return rotationScale
    End Function

    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        Dim obj As Object
        If (Not destinationType.Equals(GetType(String))) Then
            obj = MyBase.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType)
        Else
            Dim rotationScale As RotationScale = DirectCast(value, RotationScale)
            obj = String.Concat(New String() {Conversions.ToString(rotationScale.RotationCcwAngle), ",", Conversions.ToString(rotationScale.RotationCwAngle), ",", Conversions.ToString(rotationScale.RotationMaxValueCcw), ",", Conversions.ToString(rotationScale.RotationMaxValueCw), ",", Conversions.ToString(rotationScale.XPosition), ",", Conversions.ToString(rotationScale.YPosition)})
        End If
        Return obj
    End Function

    Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object, ByVal Attribute As System.Attribute()) As PropertyDescriptorCollection
        Return TypeDescriptor.GetProperties(RuntimeHelpers.GetObjectValue(value))
    End Function

    Public Overrides Function GetPropertiesSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class
