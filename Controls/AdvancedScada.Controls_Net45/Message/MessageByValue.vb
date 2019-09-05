Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing

<Serializable>
<TypeConverter(GetType(MessageConverterByValue))>
Public Class MessageByValue
    Implements ICloneable
    Private Shared HighestNumber As Integer

    Private m_Value As Integer

    Private m_Message As String

    Private m_BackColor As Color

    Private m_ForeColor As Color

    Private m_Blink As Boolean

    <DefaultValue(GetType(Color), "Empty")>
    Public Property BackColor As Color
        Get
            Return Me.m_BackColor
        End Get
        Set(ByVal value As Color)
            Me.m_BackColor = value
        End Set
    End Property

    Public Property Blink As Boolean
        Get
            Return Me.m_Blink
        End Get
        Set(ByVal value As Boolean)
            Me.m_Blink = value
        End Set
    End Property

    Public Property ForeColor As Color
        Get
            Return Me.m_ForeColor
        End Get
        Set(ByVal value As Color)
            Me.m_ForeColor = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return Me.m_Message
        End Get
        Set(ByVal value As String)
            Me.m_Message = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            If (Me.m_Value > MessageByValue.HighestNumber) Then
                MessageByValue.HighestNumber = Me.m_Value + 1
            End If
            Return Me.m_Value
        End Get
        Set(ByVal value As Integer)
            Me.m_Value = value
            If (Me.m_Value > MessageByValue.HighestNumber) Then
                MessageByValue.HighestNumber = Me.m_Value + 1
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.m_Message = String.Empty
        Me.m_BackColor = Color.Empty
        Me.m_Value = MessageByValue.HighestNumber
        MessageByValue.HighestNumber = MessageByValue.HighestNumber + 1
    End Sub

    Public Sub New(ByVal value As Integer, ByVal message As String, ByVal backColor As String, ByVal foreColor As String, ByVal blink As Boolean)
        MyClass.New(value, message, backColor, foreColor)
        Me.m_Blink = blink
    End Sub

    Public Sub New(ByVal value As Integer, ByVal message As String, ByVal backColor As String, ByVal foreColor As String)
        MyClass.New(value, message, backColor)
        If (Not (String.IsNullOrEmpty(foreColor) Or Operators.CompareString(foreColor, "0", False) = 0)) Then
            Me.m_ForeColor = Color.FromArgb(Conversions.ToInteger(foreColor))
        Else
            Me.m_ForeColor = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal value As Integer, ByVal message As String, ByVal backColor As String)
        MyClass.New(value, message)
        If (Not (String.IsNullOrEmpty(backColor) Or Operators.CompareString(backColor, "0", False) = 0)) Then
            Me.m_BackColor = Color.FromArgb(Conversions.ToInteger(backColor))
        Else
            Me.m_BackColor = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal value As Integer, ByVal message As String)
        MyClass.New(message)
        Me.m_Value = value
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New()
        Me.m_Message = String.Empty
        Me.m_BackColor = Color.Empty
        Me.m_Message = message
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim messageByValue As MessageByValue = New MessageByValue() With
        {
            .BackColor = Me.m_BackColor,
            .ForeColor = Me.m_ForeColor,
            .m_Message = Me.m_Message,
            .Value = Me.m_Value,
            .Blink = Me.m_Blink
        }
        Return messageByValue
    End Function
End Class
