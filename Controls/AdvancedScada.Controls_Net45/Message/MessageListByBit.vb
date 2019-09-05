Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class MessageListByBit
    Inherits ListBox
    Private collection_0 As Collection(Of MessageByBit)

    Private long_0 As Long

    Private long_1 As Long

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
    Public ReadOnly Property Messages As Collection(Of MessageByBit)
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

    Public Property Value As Long
        Get
            Return Me.long_0
        End Get
        Set(ByVal value As Long)
            If (value <> Me.long_0) Then
                Me.long_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.collection_0 = New Collection(Of MessageByBit)()
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
        Dim num1 As Integer = 0
        Dim num2 As Integer = 0
        Do
            If (If((Me.long_0 And Convert.ToInt64(Math.Pow(2, CDbl(num2)))) > 0L, (Me.long_1 And Convert.ToInt64(Math.Pow(2, CDbl(num2)))) <= 0L, False)) Then
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
                    str = String.Concat(str, "(", Conversions.ToString(Me.Value), ") ")
                End If
                While True
                    If (If(num >= Me.collection_0.Count, True, Me.collection_0(num).BitNumber = num2)) Then
                        Exit While
                    End If
                    num = num + 1
                End While
                If (num >= Me.collection_0.Count) Then
                    MyBase.Items.Add(String.Concat(str, "Undefined Message for Bit ", Conversions.ToString(num2)))
                Else
                    Dim messageByBit As MessageByBit = DirectCast(Me.collection_0(num).Clone(), MessageByBit)
                    messageByBit.Message = String.Concat(str, Me.collection_0(num).Message)
                    If (Me.bool_0) Then
                        While num1 < MyBase.Items.Count
                            If (Not TypeOf MyBase.Items(num1) Is MessageByBit) Then
                                num1 = num1 + 1
                            ElseIf (DirectCast(MyBase.Items(num1), MessageByBit).BitNumber <> num2) Then
                                num1 = num1 + 1
                            Else
                                MyBase.Items.RemoveAt(num1)
                            End If
                        End While
                    End If
                    MyBase.Items.Add(messageByBit)
                End If
                If (If(MyBase.Items.Count <= 0, False, MyBase.Items.Count > Me.int_0)) Then
                    MyBase.Items.RemoveAt(0)
                End If
            End If
            num2 = num2 + 1
        Loop While num2 <= 62
        If (MyBase.Items.Count > 0) Then
            MyBase.SetSelected(MyBase.Items.Count - 1, True)
        End If
        Me.long_1 = Me.long_0
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal drawIteme As DrawItemEventArgs)
        Dim bounds As Rectangle
        If (If(MyBase.Items Is Nothing OrElse MyBase.Items.Count <= drawIteme.Index, True, drawIteme.Index < 0)) Then
            MyBase.OnDrawItem(drawIteme)
        ElseIf (CObj(MyBase.Items(drawIteme.Index).[GetType]()) <> CObj(GetType(MessageByBit))) Then
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                Dim graphics As System.Drawing.Graphics = drawIteme.Graphics
                Dim str As String = MyBase.Items(drawIteme.Index).ToString()
                Dim font As System.Drawing.Font = Me.Font
                bounds = drawIteme.Bounds
                graphics.DrawString(str, font, solidBrush, bounds.Location, StringFormat.GenericDefault)
            End Using
        Else
            Dim item As MessageByBit = DirectCast(MyBase.Items(drawIteme.Index), MessageByBit)
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

    Protected Overrides Sub OnMeasureItem(ByVal measureIteme As MeasureItemEventArgs)
        MyBase.OnMeasureItem(measureIteme)
        Dim size As System.Drawing.Size = TextRenderer.MeasureText("Ag", Me.Font)
        measureIteme.ItemHeight = size.Height
    End Sub
End Class
