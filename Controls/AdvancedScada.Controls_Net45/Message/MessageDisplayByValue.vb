Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class MessageDisplayByValue
    Inherits Label
    Private color_0 As Color

    Private color_1 As Color

    Private collection_0 As Collection(Of MessageByValue)

    Private bool_0 As Boolean

    Private int_0 As Integer

    Private color_2 As Color

    Private string_0 As String

    Private bool_1 As Boolean

    '' Private speechSynthesizer_0 As SpeechSynthesizer

    Private bool_2 As Boolean

    Private string_1 As String

    Private timer_0 As System.Windows.Forms.Timer

    Public Shadows Property BackColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
            Me.color_0 = value
        End Set
    End Property

    Public Shadows Property ForeColor As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
            Me.color_1 = value
        End Set
    End Property

    Public Property IniFileName As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Messages As Collection(Of MessageByValue)
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

    Public Property SpeakMessage As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            Me.bool_2 = value
            If (value) Then
                '' Me.speechSynthesizer_0 = New SpeechSynthesizer()
            End If
        End Set
    End Property

    Public Property TextPrefix As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            Me.string_0 = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (value <> Me.int_0) Then
                Dim valueChangedEventArg As ValueChangedEventArgs = New ValueChangedEventArgs(CLng(value), CLng(Me.int_0))
                Me.int_0 = value
                Me.OnValueChanged(valueChangedEventArg)
                Me.method_0()
            End If
            If (Not Me.bool_0) Then
                Me.method_0()
                Me.bool_0 = True
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.color_0 = Color.Transparent
        Me.color_1 = Color.Transparent
        Me.collection_0 = New Collection(Of MessageByValue)()
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            MyBase.ForeColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub method_0()
        Dim num As Integer = 0
        Dim backColor As Color
        Dim flag As Boolean
        Dim b As Boolean
        Dim b1 As Boolean
        Dim str As String = String.Empty
        If (Me.bool_1) Then
            str = String.Concat("(", Conversions.ToString(Me.Value), ") ")
        End If
        While True
            If (num >= Me.collection_0.Count) Then
                flag = False
            Else
                flag = If(Me.collection_0(num) Is Nothing, True, Me.collection_0(num).Value <> Me.Value)
            End If
            If (Not flag) Then
                Exit While
            End If
            num = num + 1
        End While
        str = If(num >= Me.collection_0.Count, String.Concat("Undefined Message for ", Conversions.ToString(Me.Value)), String.Concat(str, Me.collection_0(num).Message))
        If (num >= Me.collection_0.Count) Then
            If (Me.timer_0 IsNot Nothing) Then
                Me.timer_0.[Stop]()
            End If
        ElseIf (Me.collection_0(num).Blink) Then
            If (Me.timer_0 Is Nothing) Then
                Me.timer_0 = New System.Windows.Forms.Timer() With
                {
                    .Interval = 650
                }
                AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
            End If
            Me.color_2 = Me.collection_0(num).BackColor
            Me.timer_0.Start()
        ElseIf (Me.timer_0 IsNot Nothing) Then
            Me.timer_0.[Stop]()
        End If
        If (num >= Me.collection_0.Count) Then
            b = False
        Else
            backColor = Me.collection_0(num).BackColor
            Dim a As Boolean = backColor.A <> 0
            backColor = Me.collection_0(num).BackColor
            Dim r As Boolean = a Or backColor.R <> 0
            backColor = Me.collection_0(num).BackColor
            Dim g As Boolean = r Or backColor.G <> 0
            backColor = Me.collection_0(num).BackColor
            b = g Or backColor.B <> 0
        End If
        If (Not b) Then
            MyBase.BackColor = Me.color_0
        Else
            MyBase.BackColor = Me.collection_0(num).BackColor
        End If
        If (num >= Me.collection_0.Count) Then
            b1 = False
        Else
            backColor = Me.collection_0(num).ForeColor
            Dim a1 As Boolean = backColor.A <> 0
            backColor = Me.collection_0(num).ForeColor
            Dim r1 As Boolean = a1 Or backColor.R <> 0
            backColor = Me.collection_0(num).ForeColor
            Dim g1 As Boolean = r1 Or backColor.G <> 0
            backColor = Me.collection_0(num).ForeColor
            b1 = g1 Or backColor.B <> 0
        End If
        If (Not b1) Then
            MyBase.ForeColor = Me.color_1
        Else
            MyBase.ForeColor = Me.collection_0(num).ForeColor
        End If
        If (Not String.IsNullOrEmpty(Me.string_0)) Then
            str = String.Concat(Me.string_0, str)
        End If
        Me.Text = str

    End Sub

    Protected Overridable Sub OnFileReadError(ByVal e As EventArgs)
        RaiseEvent FileReadError(Me, e)
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        Dim num As Integer
        Dim color As System.Drawing.Color
        Dim messageByValue As MessageByValue
        Dim color1 As System.Drawing.Color
        Dim color2 As System.Drawing.Color
        MyBase.OnHandleCreated(e)
        If (Not MyBase.DesignMode AndAlso Not String.IsNullOrEmpty(Me.string_1)) Then
            Try
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(Me.string_1)
                    Dim chrArray() As Char = {","C, ";"C}
                    Dim colorConverter As System.Drawing.ColorConverter = New System.Drawing.ColorConverter()
                    While Not streamReader.EndOfStream
                        Dim strArrays As String() = streamReader.ReadLine().Split(chrArray)
                        If (CInt(strArrays.Length) > 1 AndAlso Integer.TryParse(strArrays(0), num)) Then
                            Me.Messages.Add(New MessageByValue(num, strArrays(1)))
                            If (CInt(strArrays.Length) > 2) Then
                                Try
                                    Dim item As MessageByValue = Me.Messages(Me.Messages.Count - 1)
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
                                    Dim item1 As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                                    messageByValue = item1
                                    item1.Message = String.Concat(messageByValue.Message, "(Invalid BackColor-", strArrays(2), ")")
                                    ProjectData.ClearProjectError()
                                End Try
                            End If
                            If (CInt(strArrays.Length) > 3) Then
                                Try
                                    Dim messageByValue1 As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                                    Dim obj1 As Object = colorConverter.ConvertFromString(strArrays(3))
                                    If (obj1 IsNot Nothing) Then
                                        color1 = DirectCast(obj1, System.Drawing.Color)
                                    Else
                                        color = New System.Drawing.Color()
                                        color1 = color
                                    End If
                                    messageByValue1.ForeColor = color1
                                Catch exception1 As System.Exception
                                    ProjectData.SetProjectError(exception1)
                                    Dim item2 As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                                    messageByValue = item2
                                    item2.Message = String.Concat(messageByValue.Message, "(Invalid ForeColor-", strArrays(3), ")")
                                    ProjectData.ClearProjectError()
                                End Try
                            End If
                            If (CInt(strArrays.Length) > 4) Then
                                Try
                                    Me.Messages(Me.Messages.Count - 1).Blink = UtilitiesControls.BooleanFromString(strArrays(4))
                                Catch exception2 As System.Exception
                                    ProjectData.SetProjectError(exception2)
                                    Dim messageByValue2 As MessageByValue = Me.Messages(Me.Messages.Count - 1)
                                    messageByValue = messageByValue2
                                    messageByValue2.Message = String.Concat(messageByValue.Message, "(Invalid Blink (boolean) parameter-", strArrays(4), ")")
                                    ProjectData.ClearProjectError()
                                End Try
                            End If
                        End If
                        If (Me.Value <> Me.Messages(Me.Messages.Count - 1).Value) Then
                            Continue While
                        End If
                        Me.method_0()
                    End While
                End Using
            Catch exception4 As System.Exception
                ProjectData.SetProjectError(exception4)
                Dim exception3 As System.Exception = exception4
                Me.OnFileReadError(EventArgs.Empty)
                Me.Text = exception3.Message
                ProjectData.ClearProjectError()
            End Try
        End If
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As ValueChangedEventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If (MyBase.BackColor <> Me.color_2) Then
            MyBase.BackColor = Me.color_2
        Else
            MyBase.BackColor = Me.color_0
        End If
        Me.timer_0.Start()
    End Sub

    Public Event FileReadError As EventHandler


    Public Event ValueChanged As EventHandler(Of ValueChangedEventArgs)

End Class
