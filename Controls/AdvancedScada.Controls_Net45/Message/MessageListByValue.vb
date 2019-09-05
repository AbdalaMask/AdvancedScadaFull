Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Class MessageListByValue
    Inherits ListBox
    Private bool_0 As Boolean

    Private toolTip_0 As ToolTip

    Private int_0 As Integer

    Private collection_0 As Collection(Of MessageByValue)

    Private bool_1 As Boolean

    Private int_1 As Integer

    Private int_2 As Integer

    Private string_0 As String

    Private bool_2 As Boolean

    Private bool_3 As Boolean

    Private bool_4 As Boolean

    Private bool_5 As Boolean

    Private int_3 As Integer

    Private string_1 As String

    <Description("This property determines whether strings from the Items property are used as the list title. If set to False then these strings will be shown at the top of the list.")>
    Public Property ClearItemsListOnStartup As Boolean
        Get
            Return Me.bool_5
        End Get
        Set(ByVal value As Boolean)
            Me.bool_5 = value
        End Set
    End Property

    Private Property context As System.Windows.Forms.ContextMenuStrip


    Public Property IniFileName As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    Public Property MaxMessagesInList As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            Me.int_3 = Math.Max(1, value)
        End Set
    End Property

    Public Property MessageNumberToIgnore As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            Me.int_2 = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Messages As Collection(Of MessageByValue)
        Get
            Return Me.collection_0
        End Get
    End Property

    Public Property RemoveDuplicateMessages As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            Me.bool_2 = value
        End Set
    End Property

    Public Property ShowMessageNumber As Boolean
        Get
            Return Me.bool_3
        End Get
        Set(ByVal value As Boolean)
            Me.bool_3 = value
        End Set
    End Property

    Public Property ShowTimestamp As Boolean
        Get
            Return Me.bool_4
        End Get
        Set(ByVal value As Boolean)
            Me.bool_4 = value
        End Set
    End Property

    Public Property TimestampFormat As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            Me.string_0 = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (Not Me.bool_1) Then
                Me.bool_1 = True
            ElseIf (value <> Me.int_1) Then
                Dim valueChangedEventArg As ValueChangedEventArgs = New ValueChangedEventArgs(CLng(value), CLng(Me.int_1))
                Me.int_1 = value
                Me.OnValueChanged(valueChangedEventArg)
                If (Me.int_1 <> Me.int_2) Then
                    Me.method_1()
                End If
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.HandleCreated, New EventHandler(AddressOf Me.MessageListByValue_HandleCreated)
        AddHandler MyBase.MouseUp, New MouseEventHandler(AddressOf Me.MessageListByValue_MouseUp)
        AddHandler MyBase.MouseHover, New EventHandler(AddressOf Me.MessageListByValue_MouseHover)
        Me.context = New System.Windows.Forms.ContextMenuStrip()
        Me.toolTip_0 = New ToolTip()
        Me.collection_0 = New Collection(Of MessageByValue)()
        Me.int_1 = -1
        Me.string_0 = "MM/dd/yyyy HH:mm:ss"
        Me.bool_5 = True
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            MyBase.ForeColor = Color.WhiteSmoke
        End If
        Me.int_3 = 50
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
    End Sub

    Private Sub MessageListByValue_HandleCreated(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.bool_5) Then
            MyBase.Items.Clear()
        End If
    End Sub

    Private Sub MessageListByValue_MouseHover(ByVal sender As Object, ByVal e As EventArgs)
        Me.toolTip_0.SetToolTip(Me, "Right click to save or clear the list.")
    End Sub

    Private Sub MessageListByValue_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (e.Button = System.Windows.Forms.MouseButtons.Right) Then
            Me.ContextMenuStrip = Me.context
            If (Not Me.bool_0) Then
                Me.context.Items.Clear()
                Me.context.Items.Add("Clear List")
                Me.context.Items.Add("Save List As")
                Me.bool_0 = True
            End If
            Me.context.Show(Me, e.Location)
            Me.context.Items(0).[Select]()
        End If
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
        Dim enumerator As IEnumerator = Nothing
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        Dim enumerator1 As IEnumerator = Nothing
        If (Operators.CompareString(e.ClickedItem.ToString(), "Clear List", False) <> 0) Then
            Dim saveFileDialog As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog() With
            {
                .Filter = "Text Files | *.txt | Log Files | *.log",
                .DefaultExt = "txt",
                .AddExtension = True,
                .CreatePrompt = True
            }
            If (saveFileDialog.ShowDialog() = DialogResult.OK) Then
                Using streamWriter As System.IO.StreamWriter = New System.IO.StreamWriter(saveFileDialog.OpenFile())
                    Try
                        enumerator1 = MyBase.Items.GetEnumerator()
                        While enumerator1.MoveNext()
                            Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator1.Current)
                            If (Operators.CompareString(objectValue.[GetType]().ToString(), "MessageByValue", False) <> 0) Then
                                streamWriter.WriteLine(objectValue.ToString())
                            Else
                                streamWriter.WriteLine(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, Nothing, "Message", New Object(-1) {}, Nothing, Nothing, Nothing)))
                            End If
                        End While
                    Finally
                        If (TypeOf enumerator1 Is IDisposable) Then
                            TryCast(enumerator1, IDisposable).Dispose()
                        End If
                    End Try
                End Using
            End If
        Else
            Dim numArray(MyBase.Items.Count - 1 + 1 - 1) As Integer
            Try
                enumerator = MyBase.Items.GetEnumerator()
                While enumerator.MoveNext()
                    Dim obj As Object = RuntimeHelpers.GetObjectValue(enumerator.Current)
                    If (If(Operators.CompareString(obj.[GetType]().ToString(), "MessageByValue", False) = 0, False, Not obj.ToString().StartsWith("Undefined Message"))) Then
                        Continue While
                    End If
                    numArray(num) = MyBase.Items.IndexOf(RuntimeHelpers.GetObjectValue(obj))
                    num = num + 1
                End While
            Finally
                If (TypeOf enumerator Is IDisposable) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            Dim length As Integer = CInt(numArray.Length) - 1
            num = 0
            Do
                If (If(num <= 0, True, numArray(num) <> 0)) Then
                    MyBase.Items.RemoveAt(numArray(num) - num1)
                    num1 = num1 + 1
                End If
                num = num + 1
            Loop While num <= length
            Me.int_1 = -1
        End If
    End Sub

    Private Sub method_1()
        Dim str As String
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        If (Not Me.bool_4) Then
            str = String.Empty
        Else
            Try
                Dim now As DateTime = DateTime.Now
                str = String.Concat(now.ToString(Me.string_0), " ")
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                str = "Invalid Time Stamp Format"
                ProjectData.ClearProjectError()
            End Try
        End If
        If (Me.bool_3) Then
            str = String.Concat(str, "(", Conversions.ToString(Me.Value), ") ")
        End If
        While True
            If (If(num >= Me.collection_0.Count, True, Me.collection_0(num).Value = Me.Value)) Then
                Exit While
            End If
            num = num + 1
        End While
        If (num >= Me.collection_0.Count) Then
            MyBase.Items.Add(String.Concat(str, "Undefined Message for ", Conversions.ToString(Me.Value)))
        Else
            Dim messageByValue As MessageByValue = DirectCast(Me.collection_0(num).Clone(), MessageByValue)
            messageByValue.Message = String.Concat(str, Me.collection_0(num).Message)
            If (Me.bool_2) Then
                While num1 < MyBase.Items.Count
                    If (Not TypeOf MyBase.Items(num1) Is MessageByValue) Then
                        num1 = num1 + 1
                    ElseIf (DirectCast(MyBase.Items(num1), MessageByValue).Value <> Me.Value) Then
                        num1 = num1 + 1
                    Else
                        MyBase.Items.RemoveAt(num1)
                    End If
                End While
            End If
            MyBase.Items.Add(messageByValue)
        End If
        If (Me.bool_5) Then
            If (If(MyBase.Items.Count <= 0, False, MyBase.Items.Count > Me.int_3)) Then
                MyBase.Items.RemoveAt(0)
            End If
        ElseIf (If(MyBase.Items.Count <= 0, False, MyBase.Items.Count > Me.int_3 + Me.int_0)) Then
            MyBase.Items.RemoveAt(Me.int_0)
        End If
        MyBase.SetSelected(MyBase.Items.Count - 1, True)
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        If (Not MyBase.DesignMode) Then
            Me.int_0 = MyBase.Items.Count
        End If
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal drawIteme As DrawItemEventArgs)
        If (If(MyBase.Items Is Nothing OrElse MyBase.Items.Count <= drawIteme.Index, True, drawIteme.Index < 0)) Then
            MyBase.OnDrawItem(drawIteme)
        ElseIf (CObj(MyBase.Items(drawIteme.Index).[GetType]()) <> CObj(GetType(MessageByValue))) Then
            drawIteme.Graphics.DrawString(Conversions.ToString(MyBase.Items(drawIteme.Index)), drawIteme.Font, SystemBrushes.ControlText, drawIteme.Bounds, StringFormat.GenericDefault)
        Else
            Dim item As MessageByValue = DirectCast(MyBase.Items(drawIteme.Index), MessageByValue)
            Dim backColor As Color = item.BackColor
            If (backColor.R <> 0 Or backColor.G <> 0 Or backColor.B <> 0 Or backColor.A <> 0) Then
                Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(backColor)
                    drawIteme.Graphics.FillRectangle(solidBrush, drawIteme.Bounds)
                End Using
            End If
            Dim foreColor As Color = item.ForeColor
            If (foreColor.R = 0 And foreColor.G = 0 And foreColor.B = 0 And foreColor.A = 0) Then
                foreColor = Me.ForeColor
            End If
            Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(foreColor)
                Dim graphics As System.Drawing.Graphics = drawIteme.Graphics
                Dim message As String = item.Message
                Dim font As System.Drawing.Font = drawIteme.Font
                Dim bounds As Rectangle = drawIteme.Bounds
                graphics.DrawString(message, font, solidBrush1, bounds.Location, StringFormat.GenericDefault)
            End Using
        End If
    End Sub

    Protected Overridable Sub OnFileReadError(ByVal e As EventArgs)
        RaiseEvent FileReadError(Me, e)
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        Dim num As Integer
        Dim color As System.Drawing.Color
        MyBase.OnHandleCreated(e)
        If (Not MyBase.DesignMode AndAlso Not String.IsNullOrEmpty(Me.string_1)) Then
            Try
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(Me.string_1)
                    Dim chrArray() As Char = {","C, ";"C}
                    Dim colorConverter As System.Drawing.ColorConverter = New System.Drawing.ColorConverter()
                    While Not streamReader.EndOfStream
                        Dim strArrays As String() = streamReader.ReadLine().Split(chrArray)
                        If (CInt(strArrays.Length) <= 1 OrElse Not Integer.TryParse(strArrays(0), num)) Then
                            Continue While
                        End If
                        Me.Messages.Add(New MessageByValue(num, strArrays(1)))
                        If (CInt(strArrays.Length) <= 2) Then
                            Continue While
                        End If
                        Try
                            Dim item As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                            Dim obj As Object = colorConverter.ConvertFromString(strArrays(2))
                            If (obj IsNot Nothing) Then
                                color = DirectCast(obj, System.Drawing.Color)
                            Else
                                color = New System.Drawing.Color()
                            End If
                            item.BackColor = color
                        Catch exception As System.Exception
                            ProjectData.SetProjectError(exception)
                            Dim messageByValue As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                            Dim messageByValue1 As MessageByValue = messageByValue
                            messageByValue.Message = String.Concat(messageByValue1.Message, "(Invalid Color-", strArrays(2), ")")
                            ProjectData.ClearProjectError()
                        End Try
                    End While
                End Using
            Catch exception2 As System.Exception
                ProjectData.SetProjectError(exception2)
                Dim exception1 As System.Exception = exception2
                Me.OnFileReadError(EventArgs.Empty)
                Me.Text = exception1.Message
                ProjectData.ClearProjectError()
            End Try
        End If
    End Sub

    Protected Overrides Sub OnMeasureItem(ByVal measureIteme As MeasureItemEventArgs)
        MyBase.OnMeasureItem(measureIteme)
        Dim size As System.Drawing.Size = TextRenderer.MeasureText("Ag", Me.Font)
        measureIteme.ItemHeight = size.Height
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As ValueChangedEventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Event FileReadError As EventHandler


    Public Event ValueChanged As EventHandler(Of ValueChangedEventArgs)

End Class
