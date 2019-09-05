Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing

<Serializable>
<TypeConverter(GetType(MessageConverterByBit))>
Public Class MessageByBit
    Implements ICloneable
    Private Shared HighestNumber As Integer

    Private m_BackColor As Color

    Private m_ForeColor As Color

    Private m_BitNumber As Integer

    Private m_Message As String

    Private m_Blink As Boolean

    Public Property BackColor As Color
        Get
            Return Me.m_BackColor
        End Get
        Set(ByVal value As Color)
            Me.m_BackColor = value
        End Set
    End Property

    Public Property BitNumber As Integer
        Get
            If (Me.m_BitNumber > HighestNumber) Then
                HighestNumber = Me.m_BitNumber + 1
            End If
            Return Me.m_BitNumber
        End Get
        Set(ByVal value As Integer)
            Me.m_BitNumber = value
            If (Me.m_BitNumber > HighestNumber) Then
                HighestNumber = Me.m_BitNumber + 1
            End If
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

    Public Sub New()
        MyBase.New()
        Me.m_Message = String.Empty
        Me.m_BitNumber = HighestNumber
        HighestNumber = HighestNumber + 1
    End Sub

    Public Sub New(ByVal bitNumber As Integer, ByVal message As String, ByVal backColor As String, ByVal foreColor As String, ByVal blink As Boolean)
        MyClass.New(bitNumber, message, backColor, foreColor)
        Me.m_Blink = blink
    End Sub

    Public Sub New(ByVal bitNumber As Integer, ByVal message As String, ByVal backColor As String, ByVal foreColor As String)
        MyClass.New(bitNumber, message, backColor)
        If (Not (String.IsNullOrEmpty(foreColor) Or Operators.CompareString(foreColor, "0", False) = 0)) Then
            Me.m_ForeColor = Color.FromArgb(Conversions.ToInteger(foreColor))
        Else
            Me.m_ForeColor = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal bitNumber As Integer, ByVal message As String, ByVal backColor As String)
        MyClass.New(bitNumber, message)
        If (Not (String.IsNullOrEmpty(backColor) Or Operators.CompareString(backColor, "0", False) = 0)) Then
            Me.m_BackColor = Color.FromArgb(Conversions.ToInteger(backColor))
        Else
            Me.m_BackColor = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal bitNumber As Integer, ByVal message As String)
        MyClass.New(message)
        Me.m_BitNumber = bitNumber
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New()
        Me.m_Message = String.Empty
        Me.m_Message = message
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim messageByBit As MessageByBit = New MessageByBit() With
        {
            .m_Message = Me.m_Message,
            .BitNumber = Me.m_BitNumber,
            .BackColor = Me.m_BackColor,
            .ForeColor = Me.m_ForeColor,
            .Blink = Me.m_Blink
        }
        Return messageByBit
    End Function

    Public Overrides Function ToString() As String
        If (Me.m_BitNumber > HighestNumber) Then
            HighestNumber = Me.m_BitNumber + 1
        End If
        Dim str As String = String.Concat(Conversions.ToString(Me.m_BitNumber), ",", Me.m_Message)
        Return str
    End Function
End Class
