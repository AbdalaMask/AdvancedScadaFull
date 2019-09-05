Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class MessageListByItem
    Inherits ListBox
    Private collection_0 As Collection(Of MessageByItem)

    Private string_0 As String

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private bool_2 As Boolean

    Private int_0 As Integer

    Public Property MaxMessagesInList As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = Math.Max(1, value)
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Messages As Collection(Of MessageByItem)
        Get
            Return Me.collection_0
        End Get
    End Property

    Public Property RemoveDuplicateMessages As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            Me.bool_0 = value
        End Set
    End Property

    Public Property ShowMessageNumber As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            Me.bool_1 = value
        End Set
    End Property

    Public Property ShowTimestamp As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            Me.bool_2 = value
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

    Public Sub New()
        MyBase.New()
        Me.collection_0 = New Collection(Of MessageByItem)()
        Me.string_0 = "MM/dd/yyyy HH:mm:ss"
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            MyBase.ForeColor = Color.WhiteSmoke
        End If
        Me.int_0 = 50
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.bool_2 = True
    End Sub

    Private Sub method_0()
        Dim str As String
        Dim num As Integer = 0
        Dim count As Integer = Me.collection_0.Count - 1
        Dim value As Integer = 0
        Do
            If (If(Not Me.collection_0(value).Value, False, Not Me.collection_0(value).bool_1)) Then
                If (Not Me.bool_2) Then
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
                If (Me.bool_1) Then
                    str = String.Concat(str, "(", Conversions.ToString(Me.collection_0(value).MessageNumber), ") ")
                End If
                Dim messageByItem As MessageByItem = DirectCast(Me.collection_0(value).Clone(), MessageByItem)
                messageByItem.Message = String.Concat(str, Me.collection_0(value).Message)
                If (Me.bool_0) Then
                    While num < MyBase.Items.Count
                        If (Not TypeOf MyBase.Items(num) Is MessageByItem) Then
                            num = num + 1
                        ElseIf (Operators.CompareString(DirectCast(MyBase.Items(num), MessageByItem).PLCAddress, messageByItem.PLCAddress, False) <> 0) Then
                            num = num + 1
                        Else
                            MyBase.Items.RemoveAt(num)
                        End If
                    End While
                End If
                MyBase.Items.Add(messageByItem)
                If (If(MyBase.Items.Count <= 0, False, MyBase.Items.Count > Me.int_0)) Then
                    MyBase.Items.RemoveAt(0)
                End If
            End If
            Me.collection_0(value).bool_1 = Me.collection_0(value).Value
            value = value + 1
        Loop While value <= count
        If (MyBase.Items.Count > 0) Then
            MyBase.SetSelected(MyBase.Items.Count - 1, True)
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal drawIteme As DrawItemEventArgs)
        Dim bounds As Rectangle
        If (If(MyBase.Items Is Nothing OrElse MyBase.Items.Count <= drawIteme.Index, True, drawIteme.Index < 0)) Then
            MyBase.OnDrawItem(drawIteme)
        ElseIf (CObj(MyBase.Items(drawIteme.Index).[GetType]()) <> CObj(GetType(MessageByItem))) Then
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                Dim graphics As System.Drawing.Graphics = drawIteme.Graphics
                Dim str As String = MyBase.Items(drawIteme.Index).ToString()
                Dim font As System.Drawing.Font = Me.Font
                bounds = drawIteme.Bounds
                graphics.DrawString(str, font, solidBrush, bounds.Location, StringFormat.GenericDefault)
            End Using
        Else
            Dim item As MessageByItem = DirectCast(MyBase.Items(drawIteme.Index), MessageByItem)
            Dim backColor As Color = item.BackColor
            If (backColor.R <> 0 Or backColor.G <> 0 Or backColor.B <> 0 Or backColor.A <> 0) Then
                Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(backColor)
                    drawIteme.Graphics.FillRectangle(solidBrush1, drawIteme.Bounds)
                End Using
            End If
            Dim foreColor As Color = item.ForeColor
            If (foreColor.R = 0 And foreColor.G = 0 And foreColor.B = 0 And foreColor.A = 0) Then
                foreColor = Me.ForeColor
            End If
            Using solidBrush2 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(foreColor)
                Dim graphic As System.Drawing.Graphics = drawIteme.Graphics
                Dim message As String = item.Message
                Dim font1 As System.Drawing.Font = drawIteme.Font
                bounds = drawIteme.Bounds
                graphic.DrawString(message, font1, solidBrush2, bounds.Location, StringFormat.GenericDefault)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        Dim count As Integer = Me.collection_0.Count - 1
        For i As Integer = 0 To count Step 1
            Dim messageListByItem As MessageListByItem = Me
            AddHandler Me.collection_0(i).ValueChanged, New EventHandler(AddressOf messageListByItem.ValueChangedHandler)
        Next

    End Sub

    Protected Overrides Sub OnMeasureItem(ByVal measureIteme As MeasureItemEventArgs)
        MyBase.OnMeasureItem(measureIteme)
        Dim size As System.Drawing.Size = TextRenderer.MeasureText("Ag", Me.Font)
        measureIteme.ItemHeight = size.Height
    End Sub

    Protected Overridable Sub ValueChangedHandler(ByVal sender As Object, ByVal e As EventArgs)
        Me.method_0()
    End Sub
End Class
