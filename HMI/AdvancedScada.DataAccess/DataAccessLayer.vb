Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms

Public Class DataAccessLayer
    Public constr As String = "Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Database\batchs.mdf;Integrated Security=True;Connect Timeout=30"
    Public SqlConnection As SqlConnection
    ' مشيد اتصال 
    Public Sub New()
        SqlConnection = New SqlConnection(constr)

    End Sub
    'اجراء فتح الاتصال 
    Public Sub open()
        If SqlConnection.State <> ConnectionState.Open Then
            SqlConnection.Open()

        End If
    End Sub
    'اجراء غلق الاتصال 

    Public Sub close()
        If SqlConnection.State = ConnectionState.Open Then
            SqlConnection.Close()

        End If


    End Sub

    'قراة البيانات 

    Public Function SelectData(ByVal stored_procedure As String, ByVal param() As SqlParameter) As DataTable
        Dim sqlcmd As New SqlCommand()
        sqlcmd.CommandType = CommandType.StoredProcedure
        sqlcmd.CommandText = stored_procedure
        sqlcmd.Connection = SqlConnection

        If param IsNot Nothing Then
            For i As Integer = 0 To param.Length - 1
                sqlcmd.Parameters.Add(param(i))

            Next i
        End If
        Dim da As New SqlDataAdapter(sqlcmd)
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt
    End Function

    'اجراء اضافة البيانات 
    Public Sub ExecuteCommand(ByVal stored_procedure As String, ByVal param() As SqlParameter)
        Try
            Dim sqlcmd As New SqlCommand()
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.CommandText = stored_procedure
            sqlcmd.Connection = SqlConnection

            sqlcmd.Parameters.AddRange(param)


            sqlcmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try



    End Sub
End Class
