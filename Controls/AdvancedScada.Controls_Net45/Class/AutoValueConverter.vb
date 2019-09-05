Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Class AutoValueConverter
    Inherits DoubleDateNanValueConverter
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object
        Dim obj As Object
        Dim str As String = TryCast(value, String)
        obj = If(str Is Nothing OrElse String.Compare(str, "Auto", StringComparison.OrdinalIgnoreCase) <> 0, MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value)), Double.NaN)
        Return obj
    End Function

    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        Dim obj As Object
        Dim num As Double = Conversions.ToDouble(value)
        If (Not (destinationType = GetType(String)) OrElse Not Double.IsNaN(num)) Then
            obj = MyBase.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType)
        Else
            obj = "Auto"
        End If
        Return obj
    End Function

    Public Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As TypeConverter.StandardValuesCollection
        Dim arrayLists As ArrayList = New ArrayList()
        arrayLists.Add(Double.NaN)
        Return New TypeConverter.StandardValuesCollection(arrayLists)
    End Function

    Public Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
        Return False
    End Function

    Public Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class
