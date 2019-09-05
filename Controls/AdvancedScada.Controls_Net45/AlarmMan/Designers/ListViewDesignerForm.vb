Imports System.Drawing
Imports System.Windows.Forms

Public Class ListViewDesignerForm
#Region "Properties"
    Public Property ControlToEdit As AlarmMan
    Public NListViewColumns() As String
    Private SetListViewColumns As String
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Try


            Dim ListBoxSelectedItemsCount As String = ListBoxSelected.Items.Count
            For i As Integer = 0 To ListBoxSelected.Items.Count - 1
                SetListViewColumns = String.Format("{0}.", ListBoxSelected.Items(i))
            Next
            My.Settings.ChkAlarmOff = ChkAlarmOff.Checked
            My.Settings.ChkAlarmOn = ChkAlarmOn.Checked
            My.Settings.ChkAlarmVariation = ChkAlarmVariation.Checked
            My.Settings.ChkAlarmAck = ChkAlarmAck.Checked


            My.Settings.Save()

            Close()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Close()
    End Sub
    Dim Lst As New ListBox

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim Selected_txt As String = ListBoxSelected.SelectedItem

            If Selected_txt <> vbNullString Then ' التحقق من عدم خلو اختيار قيمة فارغة 

                If Not Me.ListBoxNonSelected.Items.Contains(Selected_txt) Then ' يتم التحقق هل النص موجود سلفا باللست بوكس أما لا حتى لا يضاف مرة أخرى 

                    Me.ListBoxNonSelected.Items.Add(Selected_txt)
                    ListBoxSelected.Items.RemoveAt(ListBoxSelected.SelectedIndex)
                End If

            End If

        Catch ex As Exception
            Exit Sub
        End Try


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try

            Dim MyIndex As Integer
            Dim MyItem As String
            MyIndex = ListBoxSelected.SelectedIndex
            MyItem = ListBoxSelected.Text

            If MyIndex = 0 Then
                Exit Sub
            End If

            ListBoxSelected.Items.RemoveAt(ListBoxSelected.SelectedIndex)
            ListBoxSelected.Items.Insert(MyIndex - 1, MyItem)
            ListBoxSelected.SelectedIndex = MyIndex - 1
        Catch ex As Exception
            Exit Sub
        End Try



    End Sub

    Private Sub ListBoxSelected_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxSelected.SelectedIndexChanged
        'If (ListBoxNonSelected.Items.Contains(ListBoxSelected.SelectedItem)) Then
        '    '' The item Is already exist in the listbox collection 
        '    Exit Sub
        'Else
        '    '' The item Is Not exist in the listbox collection 
        '    ListBoxNonSelected.Items.Add(ListBoxSelected.SelectedItem)
        'End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim MyIndex As Integer
            Dim MyItem As String
            MyIndex = ListBoxSelected.SelectedIndex
            MyItem = ListBoxSelected.Text

            If MyIndex = ListBoxSelected.Items.Count - 1 Then
                Exit Sub
            End If

            ListBoxSelected.Items.RemoveAt(ListBoxSelected.SelectedIndex)
            ListBoxSelected.Items.Insert(MyIndex + 1, MyItem)
            ListBoxSelected.SelectedIndex = MyIndex + 1
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Dim nListBoxSelected As New List(Of String)
    Private Sub ListViewDesignerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            For i As Integer = 0 To ListBoxSelected.Items.Count - 1
                nListBoxSelected.Add(ListBoxSelected.Items(i))
            Next
            ControlToEdit.NListViewColumnsColor.Clear()
            ListBoxSelected.Items.Clear()
            ' Open the file to read from.


            NListViewColumns = My.Settings.ListViewColumns.Split(New [Char]() {"."c})

            For Each item As String In NListViewColumns
                ListBoxSelected.Items.Add(item)

            Next



            Button6.BackColor = My.Settings.Button6BackColor
            Button7.BackColor = My.Settings.Button7BackColor
            Button8.BackColor = My.Settings.Button8BackColor
            Button9.BackColor = My.Settings.Button9BackColor
            Button5.BackColor = My.Settings.BackGroundBackColor
            ChkAlarmOff.Checked = My.Settings.ChkAlarmOff
            ChkAlarmOn.Checked = My.Settings.ChkAlarmOn
            ChkAlarmVariation.Checked = My.Settings.ChkAlarmVariation
            ChkAlarmAck.Checked = My.Settings.ChkAlarmAck


        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click
        Try


            ColorDialog1.ShowDialog()

            If sender.Name = "Button6" Then
                Button6.BackColor = ColorDialog1.Color
                ControlToEdit.NListViewColumnsColor.Add(Button6.BackColor)
                My.Settings.Button6BackColor = Button6.BackColor


            End If

            If sender.Name = "Button7" Then
                Button7.BackColor = ColorDialog1.Color
                ControlToEdit.NListViewColumnsColor.Add(Button7.BackColor)
                My.Settings.Button7BackColor = Button7.BackColor

            End If
            If sender.Name = "Button8" Then
                Button8.BackColor = ColorDialog1.Color
                ControlToEdit.NListViewColumnsColor.Add(Button8.BackColor)
                My.Settings.Button8BackColor = Button8.BackColor

            End If
            If sender.Name = "Button9" Then
                Button9.BackColor = ColorDialog1.Color
                ControlToEdit.NListViewColumnsColor.Add(Button9.BackColor)
                My.Settings.Button9BackColor = Button9.BackColor

            End If
            If sender.Name = "Button5" Then
                Button5.BackColor = ColorDialog1.Color
                ControlToEdit.NListViewColumnsColor.Add(Button5.BackColor)
                My.Settings.BackGroundBackColor = Button5.BackColor

            End If
        Catch ex As Exception
            Exit Sub
        End Try
        My.Settings.Save()
    End Sub

#End Region
    Dim colRemovedTabs As New List(Of TabPage)
    Sub HidePage(ByVal NamePage As String)
        ' Hide multiple pages
        For Each tabpg As TabPage In TabControl1.TabPages
            If tabpg.Name = NamePage Then
                colRemovedTabs.Add(tabpg)
                TabControl1.Controls.Remove(tabpg)
            End If
        Next
    End Sub
    Sub ShowAllPages()
        '  Reinsert all removed pages
        For Each savedTab As TabPage In colRemovedTabs
            TabControl1.Controls.Add(savedTab)
        Next

        ' Clear the collection as it has now done its work
        colRemovedTabs.Clear()

    End Sub

    Dim SelectTab As String
    Private Sub BtnCheckAll_Click(sender As Object, e As EventArgs) Handles BtnCheckAll.Click

        Try


            Select Case SelectTab
                Case "tbAlarmLevel"

                    CheckBox_Check(tbAlarmLevel)
                Case "tbAlarmZone"
                    CheckBox_Check(tbAlarmZone)
                Case "tbDisplayType"
                    CheckBox_Check(tbDisplayType)
                Case "tbAlarmStatus"
                    CheckBox_Check(tbAlarmStatus)
            End Select
        Catch ex As Exception
            Exit Sub
        End Try


    End Sub
    Public Sub CheckBox_Check(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            CheckBox_Check(ctrl)
            If TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = True
            End If
        Next
    End Sub
    Public Sub CheckBox_UCheck(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            CheckBox_UCheck(ctrl)
            If TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False
            End If
        Next
    End Sub
    Private Sub CheckBox17_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox17.CheckedChanged

    End Sub

    Private Sub BtnUCheckAll_Click(sender As Object, e As EventArgs) Handles BtnUCheckAll.Click
        Try
            Select Case SelectTab
                Case "tbAlarmLevel"
                    CheckBox_UCheck(tbAlarmLevel)
                Case "tbAlarmZone"
                    CheckBox_UCheck(tbAlarmZone)
                Case "tbDisplayType"
                    CheckBox_UCheck(tbDisplayType)
                Case "tbAlarmStatus"
                    CheckBox_UCheck(tbAlarmStatus)
            End Select
        Catch ex As Exception
            Exit Sub
        End Try



    End Sub

    Private Sub ListBoxSelected_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxSelected.DrawItem
        Try



            '' الخروج من الإجراء إذا لم يوجد عناصر
            If e.Index < 0 Then Exit Sub

            '' اللون الإفتراضي لخلفية العنصر المحدد
            e.DrawBackground()

            '' خصائص تنسيق النص
            Dim _sf As New StringFormat

            '' تحويل الإتجاه من اليمين إلى اليسار
            If sender.RightToLeft Then _sf.FormatFlags = StringFormatFlags.DirectionRightToLeft


            '' للعنصر المحدد
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then

                '' لون خلفية العنصر المحدد
                e.Graphics.FillRectangle(Brushes.Brown, e.Bounds)

                '' لون نص العنصر المحدد
                e.Graphics.DrawString(sender.GetItemText(sender.Items(e.Index)), e.Font, New SolidBrush(e.ForeColor), e.Bounds, _sf)


            Else '' للعناصر غير المحددة

                If e.Index Mod 2 Then ' الترتيب الزوجي -------------------------

                    '' لون خلفية العنصر  غير المحدد حسب الترتيب الزوجي
                    e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(10, 0, 0, 0)), e.Bounds)

                    '' لون نص العنصر بالترتيب الزوجي
                    e.Graphics.DrawString(sender.GetItemText(sender.Items(e.Index)), e.Font, Brushes.Red, e.Bounds, _sf)

                Else ' الترتيب الفردي -------------------------

                    '' لون خلفية العنصر  غير المحدد حسب الترتيب الفردي
                    e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(20, 0, 0, 0)), e.Bounds)

                    '' لون نص العنصر بالترتيب الفردي
                    e.Graphics.DrawString(sender.GetItemText(sender.Items(e.Index)), e.Font, Brushes.Blue, e.Bounds, _sf)
                End If

            End If

            '' رسم مستطيل التركيز
            e.DrawFocusRectangle()

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim Selected_txt As String = ListBoxNonSelected.SelectedItem

            If Selected_txt <> vbNullString Then ' التحقق من عدم خلو اختيار قيمة فارغة 

                If Not Me.ListBoxSelected.Items.Contains(Selected_txt) Then ' يتم التحقق هل النص موجود سلفا باللست بوكس أما لا حتى لا يضاف مرة أخرى 

                    Me.ListBoxSelected.Items.Add(Selected_txt)
                    ListBoxNonSelected.Items.RemoveAt(ListBoxNonSelected.SelectedIndex)
                End If

            End If
        Catch ex As Exception
            Exit Sub
        End Try


    End Sub


    Private Sub tbAlarmLevel_Enter(sender As Object, e As EventArgs) Handles tbAlarmLevel.Enter, tbAlarmZone.Enter, tbDisplayType.Enter, tbAlarmStatus.Enter
        Try
            If sender.name = "tbAlarmLevel" Then SelectTab = tbAlarmLevel.Name
            If sender.name = "tbAlarmZone" Then SelectTab = tbAlarmZone.Name
            If sender.name = "tbDisplayType" Then SelectTab = tbDisplayType.Name
            If sender.name = "tbAlarmStatus" Then SelectTab = tbAlarmStatus.Name

        Catch ex As Exception
            Exit Sub
        End Try

    End Sub
End Class