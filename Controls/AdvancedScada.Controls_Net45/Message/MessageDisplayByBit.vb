Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class MessageDisplayByBit
    Inherits Label
    Private color_0 As Color

    Private color_1 As Color

    Private color_2 As Color

    Private color_3 As Color

    Private string_0 As String

    Private collection_0 As Collection(Of MessageByBit)

    Private long_0 As Long

    Private long_1 As Long

    Private timer_0 As System.Windows.Forms.Timer

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private string_1 As String

    Private string_2 As String

    Private int_0 As Integer

    Private int_1 As Integer

    Private timer_1 As System.Windows.Forms.Timer

    Private color_4 As Color

    Public Shadows Property BackColor As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
            Me.color_1 = value
        End Set
    End Property

    Public Property DefaultMessage As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    Public Property DisplayTime As Integer
        Get
            If (Me.timer_0 Is Nothing) Then
                Me.timer_0 = New System.Windows.Forms.Timer() With
                {
                    .Interval = 3000
                }
            End If
            Return Me.timer_0.Interval
        End Get
        Set(ByVal value As Integer)
            If (Me.timer_0 Is Nothing) Then
                Me.timer_0 = New System.Windows.Forms.Timer() With
                {
                    .Interval = 3000
                }
            End If
            Me.timer_0.Interval = Math.Max(value, 100)
        End Set
    End Property

    Public Shadows Property ForeColor As Color
        Get
            Return Me.color_2
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
            Me.color_2 = value
        End Set
    End Property

    Public Property HighlightColor As Color
        Get
            Return Me.color_3
        End Get
        Set(ByVal value As Color)
            Me.color_3 = value
        End Set
    End Property

    Public Property HighlightKeyCharacter As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            Me.string_0 = value
        End Set
    End Property

    Public Property IniFileName As String
        Get
            Return Me.string_2
        End Get
        Set(ByVal value As String)
            Me.string_2 = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Messages As Collection(Of MessageByBit)
        Get
            Return Me.collection_0
        End Get
    End Property

    Public Property ShowMessageNumber As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            Me.bool_1 = value
            Me.method_0()
        End Set
    End Property

    Public Property ShowUndefinedMessages As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            Me.bool_0 = value
        End Set
    End Property

    Public Property Value As Long
        Get
            Return Me.long_0
        End Get
        Set(ByVal value As Long)
            Dim num As Integer = 0
            Dim num1 As Integer = 0
            If (Me.long_0 <> value) Then
                Me.OnValueChanged(New ValueChangedEventArgs(value, Me.long_0))


                If (Me.long_0 = 0L And Me.long_1 = 0L Or (Me.long_1 And Me.long_1) = 0L Or MyBase.DesignMode) Then
                    Me.long_0 = value
                    Me.method_0()
                End If
                Me.long_0 = value
            End If
        End Set
    End Property

    Public Property ValueBitmask As Long
        Get
            Return Me.long_1
        End Get
        Set(ByVal value As Long)
            If (Me.long_1 <> value) Then
                Me.long_1 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.color_1 = Color.Transparent
        Me.color_2 = Color.Transparent
        Me.color_3 = Color.Red
        Me.string_0 = "!"
        Me.collection_0 = New Collection(Of MessageByBit)()
        Me.bool_0 = True
        Me.string_1 = "No Messages"
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            MyBase.ForeColor = Color.WhiteSmoke
        End If
    End Sub

    Public Function GetMessageText(ByVal bitNumber As Integer) As String
        Dim num As Integer = 0
        Dim str As String
        While True
            If (If(num >= Me.collection_0.Count, True, Me.collection_0(num).BitNumber = bitNumber)) Then
                Exit While
            End If
            num = num + 1
        End While
        str = If(num < Me.collection_0.Count, Me.collection_0(num).Message, String.Concat("Undefined Message for bit ", Conversions.ToString(bitNumber)))
        Return str
    End Function

    Private Sub method_0()


    End Sub

    Protected Overridable Sub OnBitChanged(ByVal e As EventArgs)

    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If (Me.timer_0.Interval < 1000) Then
            Me.timer_0.Interval = 3000
        End If
        AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
        If (Not MyBase.DesignMode) Then
            Me.timer_0.Enabled = True
        End If
    End Sub

    Protected Overridable Sub OnFileReadError(ByVal e As EventArgs)

    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        Dim num As Integer
        Dim color As System.Drawing.Color
        Dim messageByBit As MessageByBit
        Dim color1 As System.Drawing.Color
        Dim color2 As System.Drawing.Color
        MyBase.OnHandleCreated(e)
        If (Not MyBase.DesignMode AndAlso Not String.IsNullOrEmpty(Me.string_2)) Then
            Try
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(Me.string_2)
                    Dim chrArray() As Char = {","C, ";"C}
                    Dim colorConverter As System.Drawing.ColorConverter = New System.Drawing.ColorConverter()
                    While Not streamReader.EndOfStream
                        Dim strArrays As String() = streamReader.ReadLine().Split(chrArray)
                        If (CInt(strArrays.Length) <= 1 OrElse Not Integer.TryParse(strArrays(0), num)) Then
                            Continue While
                        End If
                        Me.Messages.Add(New MessageByBit(num, strArrays(1)))
                        If (CInt(strArrays.Length) > 2) Then
                            Try
                                Dim item As MessageByBit = Me.Messages(Me.Messages.Count - 1)
                                Dim obj As Object = colorConverter.ConvertFromString(strArrays(2))
                                If (obj IsNot Nothing) Then
                                    color2 = DirectCast(obj, System.Drawing.Color)
                                Else
                                    color = New System.Drawing.Color()
                                    color2 = color
                                End If
                                item.BackColor = color2
                            Catch exception As System.Exception
                                ProjectData.SetProjectError(exception)
                                Dim item1 As MessageByBit = Me.Messages(Me.Messages.Count - 1)
                                messageByBit = item1
                                item1.Message = String.Concat(messageByBit.Message, "(Invalid BackColor-", strArrays(2), ")")
                                ProjectData.ClearProjectError()
                            End Try
                        End If
                        If (CInt(strArrays.Length) <= 3) Then
                            Continue While
                        End If
                        Try
                            Dim messageByBit1 As MessageByBit = Me.Messages(Me.Messages.Count - 1)
                            Dim obj1 As Object = colorConverter.ConvertFromString(strArrays(3))
                            If (obj1 IsNot Nothing) Then
                                color1 = DirectCast(obj1, System.Drawing.Color)
                            Else
                                color = New System.Drawing.Color()
                                color1 = color
                            End If
                            messageByBit1.ForeColor = color1
                        Catch exception1 As System.Exception
                            ProjectData.SetProjectError(exception1)
                            Dim item2 As MessageByBit = Me.Messages(Me.Messages.Count - 1)
                            messageByBit = item2
                            item2.Message = String.Concat(messageByBit.Message, "(Invalid ForeColor-", strArrays(3), ")")
                            ProjectData.ClearProjectError()
                        End Try
                    End While
                End Using
            Catch exception3 As System.Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As System.Exception = exception3
                Me.OnFileReadError(EventArgs.Empty)
                Me.Text = exception2.Message
                ProjectData.ClearProjectError()
            End Try
        End If
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)

    End Sub

    Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Me.timer_0.Enabled = False
        Me.method_0()
        Me.timer_0.Enabled = True
    End Sub

    Private Sub timer_1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.BackColor <> Me.color_4) Then
            MyBase.BackColor = Me.color_4
        Else
            MyBase.BackColor = Me.color_1
        End If
        Me.timer_1.Start()
    End Sub

    Public Event BitChanged As EventHandler(Of ValueChangedEventArgs)


    Public Event FileReadError As EventHandler


    Public Event ValueChanged As EventHandler(Of ValueChangedEventArgs)

End Class
