Imports System.Drawing
Imports System.Windows.Forms

<System.ComponentModel.Designer(GetType(ListViewDesigner))>
Public Class AlarmMan
    Inherits ListView
    Dim intCount As Integer = 0
    Dim nTime As String = "2017/12/18 16:44:02"
    Dim nTagName As String = "TagName"
    Dim nTagValue As String = "15135"
    Dim nColor() As Color = New Color(3) {Color.Red, Color.Green, Color.Blue, Color.Yellow}
#Region "Constructor/Destructor"
    Public Sub New()

    End Sub

    Public NListViewColumns() As String

    Public NListViewColumnsColor As New List(Of Color)

#End Region
    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        If Me.DesignMode AndAlso Me.Parent IsNot Nothing AndAlso Me.Parent.Site IsNot Nothing Then
            '********************************************************
            '* Search for AdvancedHMIDrivers.IComComponent component in parent form
            '* If one exists, set the client of this component to it
            '********************************************************
            Dim i = 0
            Dim j As Integer = Me.Parent.Site.Container.Components.Count

            Me.DoubleBuffered = True
            With Me
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .Columns.Clear()
                .Items.Clear()
                .Columns.Add("Tag Name", 130, HorizontalAlignment.Left)
                .Columns.Add("Tag Value", 130, HorizontalAlignment.Left)
                .Columns.Add("Time", 130, HorizontalAlignment.Left)
                .HeaderStyle = ColumnHeaderStyle.Nonclickable
            End With

            For index = 0 To 19
                Dim row0 As String() = {nTagName, nTagValue, nTime}
                Dim item As New ListViewItem(row0)
                item.ForeColor = nColor(intCount)
                Me.Items.Insert(0, item)
                intCount = intCount + 1
                If intCount = 3 Then
                    intCount = 0
                End If

            Next


        Else
            If Not DesignMode And IsHandleCreated Then
                RListViewColumns()
                Me.Sorting = SortOrder.Ascending
            End If
        End If
    End Sub
    Public Sub RListViewColumns()

        NListViewColumns = My.Settings.ListViewColumns.Split(New [Char]() {"."c})
        With Me
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .Columns.Clear()
            .Items.Clear()
            .HeaderStyle = ColumnHeaderStyle.Nonclickable
            .BackColor = My.Settings.BackGroundBackColor
        End With

        For i As Integer = 0 To NListViewColumns.Count - 1
            Me.Columns.Add(NListViewColumns(i).ToString, 130, HorizontalAlignment.Left)
        Next

    End Sub
    Public Sub AlarmMan_DataChanged(senderPlcAddress As String, e As DriverBase.Comm.PlcComEventArgs)
        Dim LastValue As String = String.Empty
        If e.PlcAddress = senderPlcAddress Then
            Dim row0 As String() = {e.PlcAddress, e.Values(0), Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}

            If e.Values(0) <> LastValue Then
                LastValue = e.Values(0)
                '* Do something here for the value changed
                If LastValue = "True" Then
                    Dim flag As Boolean = False
                    For Each listViewItem As ListViewItem In Me.Items
                        If listViewItem.Text = row0(0) And listViewItem.SubItems(2).Text.Substring(14, 2) = row0(2).Substring(14, 2) Then
                            listViewItem.ForeColor = Color.Red
                            If listViewItem.SubItems(1).Text <> row0(1) Then
                                listViewItem.SubItems(1).Text = row0(1)
                            End If
                            If listViewItem.SubItems(2).Text <> row0(2) Then
                                listViewItem.SubItems(2).Text = row0(2)
                            End If

                            flag = True
                            Exit For
                        End If
                    Next listViewItem
                    If Not flag Then
                        Dim item As New ListViewItem(row0)
                        item.ForeColor = Color.Red
                        Me.Items.Insert(0, item)
                    End If
                ElseIf LastValue = "False" Then
                    Dim row1 As String() = {e.PlcAddress, e.Values(0), Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}

                    Dim flag As Boolean = False
                    For Each listViewItem As ListViewItem In Me.Items
                        If listViewItem.Text = row0(0) And listViewItem.SubItems(2).Text.Substring(14, 2) = row0(2).Substring(14, 2) Then
                            listViewItem.ForeColor = Color.Green
                            If listViewItem.SubItems(1).Text <> row1(1) Then
                                listViewItem.SubItems(1).Text = row1(1)
                            End If
                            If listViewItem.SubItems(2).Text <> row1(2) Then
                                listViewItem.SubItems(2).Text = row1(2)
                            End If
                            flag = True
                            Exit For
                        End If
                    Next listViewItem
                    If Not flag Then
                        Dim item As New ListViewItem(row1)
                        item.ForeColor = Color.Green
                        Me.Items.Insert(0, item)
                    End If
                End If
            End If
        End If
    End Sub

End Class
