Imports System.Management
Public Class Form1
    Public A As String = ""
    Dim s As String = "BFEBFBFF000006FD"
    Dim f As String = ""
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error Resume Next
        Dim ProcessorID As New ManagementObjectSearcher("SELECT * FROM Win32_processor")
        Dim CPUID As ManagementObject
        For Each CPUID In ProcessorID.Get()
            A = Trim((CPUID("processorId").ToString()))
            If A <> "" Then
                f = (A)
            End If
        Next
        TextBox1.Text = f
    End Sub
End Class
