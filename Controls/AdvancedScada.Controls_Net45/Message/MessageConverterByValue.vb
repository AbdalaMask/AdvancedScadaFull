Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Class MessageConverterByValue
    Inherits TypeConverter
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (sourceType.Equals(GetType(String)) Or sourceType.Equals(GetType(MessageByValue))), MyBase.CanConvertFrom(context, sourceType), True)
        Return flag
    End Function

    Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (destinationType.Equals(GetType(String)) Or destinationType.Equals(GetType(MessageByValue))), MyBase.CanConvertTo(context, destinationType), True)
        Return flag
    End Function

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object
        Dim messageByValue As Object
        If (Not TypeOf value Is String) Then
            messageByValue = MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value))
        Else
            Dim str As String = Conversions.ToString(value)
            Dim strArrays As String() = str.Split(New Char() {culture.TextInfo.ListSeparator(0)})
            Try
                If (CInt(strArrays.Length) > 4) Then
                    messageByValue = New MessageByValue(Conversions.ToInteger(strArrays(0)), strArrays(1), strArrays(2), strArrays(3), Conversions.ToBoolean(strArrays(4)))
                ElseIf (CInt(strArrays.Length) = 4) Then
                    messageByValue = New MessageByValue(Conversions.ToInteger(strArrays(0)), strArrays(1), strArrays(2), strArrays(3))
                ElseIf (CInt(strArrays.Length) <> 3) Then
                    messageByValue = If(CInt(strArrays.Length) <> 2, New MessageByValue(99, strArrays(0)), New MessageByValue(Conversions.ToInteger(strArrays(0)), strArrays(1)))
                Else
                    messageByValue = New MessageByValue(Conversions.ToInteger(strArrays(0)), strArrays(1), strArrays(2))
                End If
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                Throw New InvalidCastException(Conversions.ToString(value))
            End Try
        End If
        Return messageByValue
    End Function

    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        Dim obj As Object
        If (Not destinationType.Equals(GetType(String))) Then
            obj = MyBase.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType)
        Else
            Dim messageByValue As MessageByValue = DirectCast(value, MessageByValue)
            Dim str() As String = {Conversions.ToString(messageByValue.Value), Conversions.ToString(culture.TextInfo.ListSeparator(0)), messageByValue.Message, Conversions.ToString(culture.TextInfo.ListSeparator(0)), Nothing, Nothing, Nothing, Nothing, Nothing}
            Dim argb As Integer = messageByValue.BackColor.ToArgb()
            str(4) = argb.ToString()
            str(5) = Conversions.ToString(culture.TextInfo.ListSeparator(0))
            argb = messageByValue.ForeColor.ToArgb()
            str(6) = argb.ToString()
            str(7) = Conversions.ToString(culture.TextInfo.ListSeparator(0))
            str(8) = messageByValue.Blink.ToString()
            obj = String.Concat(str)
        End If
        Return obj
    End Function

    Public Overrides Function GetProperties(ByVal context As ITypeDescriptorContext, ByVal value As Object, ByVal attribute As System.Attribute()) As PropertyDescriptorCollection
        Return TypeDescriptor.GetProperties(RuntimeHelpers.GetObjectValue(value))
    End Function

    Public Overrides Function GetPropertiesSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class
