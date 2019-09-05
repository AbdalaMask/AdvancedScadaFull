Imports System.Runtime.InteropServices
Imports System.Text
''' <summary>
''' Create a New INI file to store or load data
''' </summary>
Public Class iniClass
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    <DllImport("kernel32")>
    Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function

    ' INI 값 읽기
    Public Function GetIniValue(ByVal Section As String, ByVal Key As String, ByVal iniPath As String) As String
        Dim temp As New StringBuilder(255)
        Dim i As Integer = GetPrivateProfileString(Section, Key, String.Empty, temp, 255, iniPath)
        Return temp.ToString()
    End Function

    ' INI 값 설정
    Public Sub SetIniValue(ByVal Section As String, ByVal Key As String, ByVal Value As String, ByVal iniPath As String)
        WritePrivateProfileString(Section, Key, Value, iniPath)
    End Sub
End Class
