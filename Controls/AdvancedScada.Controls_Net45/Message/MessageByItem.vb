Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing

<TypeConverter(GetType(MessageConverterByItem))>
Public Class MessageByItem
    Implements ICloneable
    Private color_0 As Color

    Private color_1 As Color

    Private bool_0 As Boolean

    Friend bool_1 As Boolean

    Private string_0 As String

    Private int_0 As Integer

    Private string_1 As String

    Public Property BackColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            Me.color_0 = value
        End Set
    End Property

    Public Property ForeColor As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            Me.color_1 = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            Me.string_0 = value
        End Set
    End Property

    Public Property MessageNumber As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
        End Set
    End Property

    Public Property PLCAddress As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Property Value As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                Me.OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.string_0 = String.Empty
    End Sub

    Public Sub New(ByVal message As String, ByVal backColor As String, ByVal foreColor As String, ByVal plcAddress As String)
        MyClass.New(message, backColor, foreColor)
        Me.string_1 = plcAddress
    End Sub

    Public Sub New(ByVal message As String, ByVal backColor As String, ByVal foreColor As String)
        MyClass.New(message, backColor)
        If (Not (String.IsNullOrEmpty(foreColor) Or Operators.CompareString(foreColor, "0", False) = 0)) Then
            Me.color_1 = Color.FromArgb(Conversions.ToInteger(foreColor))
        Else
            Me.color_1 = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal message As String, ByVal backColor As String)
        MyClass.New(message)
        If (Not (String.IsNullOrEmpty(backColor) Or Operators.CompareString(backColor, "0", False) = 0)) Then
            Me.color_0 = Color.FromArgb(Conversions.ToInteger(backColor))
        Else
            Me.color_0 = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New()
        Me.string_0 = String.Empty
        Me.string_0 = message
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim messageByItem As MessageByItem = New MessageByItem() With
        {
            .string_0 = Me.string_0,
            .Value = Me.bool_0,
            .BackColor = Me.color_0,
            .ForeColor = Me.color_1,
            .PLCAddress = Me.string_1
        }
        Return messageByItem
    End Function

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Overrides Function ToString() As String
        Return Me.string_0
    End Function

    Public Event ValueChanged As EventHandler

End Class
