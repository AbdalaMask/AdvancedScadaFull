Public NotInheritable Class Utilities
    Public Sub New()
    End Sub

    Public Shared Sub SetPropertiesByIniFile(ByVal targetObject As Object, ByVal iniFileName As String, ByVal iniFileSection As String)
        If targetObject Is Nothing Then
            Throw New System.ArgumentNullException("targetObject", "SetPropertiesByIniFile null parameter of targetObject")
        End If

        If Not String.IsNullOrEmpty(iniFileName) Then
            Dim p As New IniParser(iniFileName)
            Dim settings() As String = p.ListSettings(iniFileSection)
            '* Loop thtough all the settings in this section
            For index = 0 To settings.Length - 1
                Dim pi As System.Reflection.PropertyInfo
                pi = targetObject.GetType().GetProperty(settings(index), Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                '* Check if a matching property name exists in the targetObject
                If pi IsNot Nothing Then
                    Dim value As Object = Nothing
                    If pi.PropertyType.IsEnum Then
                        Try
                            '* V3.99y - Added Enum capability
                            '* Enum type have to be converted from string to the Enum option
                            value = [Enum].Parse(pi.PropertyType, p.GetSetting(iniFileSection, settings(index)), True)
                        Catch ex As Exception
                            System.Windows.Forms.MessageBox.Show("Ini File Error - " & settings(index) & " is an an Enum. Values of " & p.GetSetting(iniFileSection, settings(index)) & " is not a valid option.")
                        End Try
                    Else
                        value = Convert.ChangeType(p.GetSetting(iniFileSection, settings(index)), targetObject.GetType().GetProperty(pi.Name).PropertyType, Globalization.CultureInfo.InvariantCulture)
                    End If
                    pi.SetValue(targetObject, value, Nothing)
                Else
                    System.Windows.Forms.MessageBox.Show("Ini File Error - " & settings(index) & " is not a valid property.")
                End If
            Next
        End If
    End Sub
    Public Shared Function DynamicConverter(ByVal value As String, ByVal t As Type) As Object
        If t = GetType(Boolean) Then
            Dim boolValue As Boolean
            If (Boolean.TryParse(value, boolValue)) Then
                Return boolValue
            Else
                Dim intValue As Integer
                If (Integer.TryParse(value, intValue)) Then
                    Return System.Convert.ChangeType(intValue, t)
                Else
                    Throw New Exception("Invalid Conversion of " & value)
                End If
            End If
        Else
            Return Convert.ChangeType(value, t)
        End If
    End Function
End Class
