Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Class DoubleDateNanValueConverter
    Inherits DoubleConverter
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object
        Dim obj As Object
        Dim dateTime As System.DateTime
        Dim flag As Boolean
        Dim objectValue As Object = Nothing
        Dim flag1 As Boolean = False
        Dim str As String = TryCast(value, String)
        If (str Is Nothing OrElse String.Compare(str, "NotSet", StringComparison.OrdinalIgnoreCase) <> 0) Then
            Try
                objectValue = RuntimeHelpers.GetObjectValue(MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value)))
            Catch argumentException As System.ArgumentException
                ProjectData.SetProjectError(argumentException)
                objectValue = Nothing
                ProjectData.ClearProjectError()
            Catch notSupportedException As System.NotSupportedException
                ProjectData.SetProjectError(notSupportedException)
                objectValue = Nothing
                ProjectData.ClearProjectError()
            End Try
            If (str Is Nothing) Then
                flag = False
            Else
                flag = If(flag1, True, objectValue Is Nothing)
            End If
            obj = If(Not flag OrElse Not System.DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, dateTime), MyBase.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value)), dateTime.ToOADate())
        Else
            obj = Double.NaN
        End If
        Return obj
    End Function

    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        Dim obj As Object
        If (Not (destinationType = GetType(String)) OrElse Not Double.IsNaN(Conversions.ToDouble(value))) Then
            obj = MyBase.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType)
        Else
            obj = "NotSet"
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
