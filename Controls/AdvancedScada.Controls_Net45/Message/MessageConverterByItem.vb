Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Class MessageConverterByItem
    Inherits TypeConverter
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (sourceType.Equals(GetType(String)) Or sourceType.Equals(GetType(MessageByItem))), MyBase.CanConvertFrom(context, sourceType), True)
        Return flag
    End Function

    Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
        Dim flag As Boolean
        flag = If(Not (destinationType.Equals(GetType(String)) Or destinationType.Equals(GetType(MessageByItem))), MyBase.CanConvertTo(context, destinationType), True)
        Return flag
    End Function

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object
        Dim messageByItem As Object
        If (Not TypeOf value Is String) Then
            messageByItem = MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value))
        Else
            Dim str As String = Conversions.ToString(value)
            Dim strArrays As String() = str.Split(New Char() {culture.TextInfo.ListSeparator(0)})
            Try
                If (CInt(strArrays.Length) = 4) Then
                    messageByItem = New MessageByItem(strArrays(0), strArrays(1), strArrays(2), strArrays(3))
                ElseIf (CInt(strArrays.Length) <> 3) Then
                    messageByItem = If(CInt(strArrays.Length) <> 2, New MessageByItem(strArrays(0)), New MessageByItem(strArrays(0), strArrays(1)))
                Else
                    messageByItem = New MessageByItem(strArrays(0), strArrays(1), strArrays(2))
                End If
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                Throw New InvalidCastException(value.ToString())
            End Try
        End If
        Return messageByItem
    End Function

    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        Dim obj As Object
        Dim messageByItem As MessageByItem = DirectCast(value, MessageByItem)
        If (Not (destinationType.Equals(GetType(String)) And messageByItem IsNot Nothing)) Then
            obj = MyBase.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType)
        Else
            Dim message() As String = {messageByItem.Message, Conversions.ToString(culture.TextInfo.ListSeparator(0)), Nothing, Nothing, Nothing, Nothing, Nothing}
            Dim argb As Integer = messageByItem.BackColor.ToArgb()
            message(2) = argb.ToString()
            message(3) = Conversions.ToString(culture.TextInfo.ListSeparator(0))
            argb = messageByItem.ForeColor.ToArgb()
            message(4) = argb.ToString()
            message(5) = Conversions.ToString(culture.TextInfo.ListSeparator(0))
            message(6) = messageByItem.PLCAddress
            obj = String.Concat(message)
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
