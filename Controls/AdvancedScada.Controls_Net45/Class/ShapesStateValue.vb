Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing

<Serializable>
<TypeConverter(GetType(ShapesStateValueTypeConverter))>
Public Class ShapesStateValue
    Implements ICloneable
    Private m_Value As Integer

    Private m_StateBackColor As Color

    Private m_StateBackColorBlink As Color

    Private m_StateBorderColor As Color

    Private m_StateBorderColorBlink As Color

    Private m_StateBlink As Boolean

    Public Property StateBackColor As Color
        Get
            Return Me.m_StateBackColor
        End Get
        Set(ByVal value As Color)
            Me.m_StateBackColor = value
        End Set
    End Property

    Public Property StateBackColorBlink As Color
        Get
            Return Me.m_StateBackColorBlink
        End Get
        Set(ByVal value As Color)
            Me.m_StateBackColorBlink = value
        End Set
    End Property

    Public Property StateBlink As Boolean
        Get
            Return Me.m_StateBlink
        End Get
        Set(ByVal value As Boolean)
            Me.m_StateBlink = value
        End Set
    End Property

    Public Property StateBorderColor As Color
        Get
            Return Me.m_StateBorderColor
        End Get
        Set(ByVal value As Color)
            Me.m_StateBorderColor = value
        End Set
    End Property

    Public Property StateBorderColorBlink As Color
        Get
            Return Me.m_StateBorderColorBlink
        End Get
        Set(ByVal value As Color)
            Me.m_StateBorderColorBlink = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Integer)
            Me.m_Value = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal value As Integer, ByVal backColor As String, ByVal backColorBlink As String, ByVal borderColor As String, ByVal borderColorBlink As String, ByVal blink As Boolean)
        MyClass.New(value, backColor, backColorBlink, borderColor, borderColorBlink)
        Me.m_StateBlink = blink
    End Sub

    Public Sub New(ByVal value As Integer, ByVal backColor As String, ByVal backColorBlink As String, ByVal borderColor As String, ByVal borderColorBlink As String)
        MyClass.New(value, backColor, backColorBlink, borderColor)
        If (Not (String.IsNullOrEmpty(borderColorBlink) Or Operators.CompareString(borderColorBlink, "0", False) = 0)) Then
            Me.m_StateBorderColorBlink = Color.FromArgb(Conversions.ToInteger(borderColorBlink))
        Else
            Me.m_StateBorderColorBlink = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal value As Integer, ByVal backColor As String, ByVal backColorBlink As String, ByVal borderColor As String)
        MyClass.New(value, backColor, backColorBlink)
        If (Not (String.IsNullOrEmpty(borderColor) Or Operators.CompareString(borderColor, "0", False) = 0)) Then
            Me.m_StateBorderColor = Color.FromArgb(Conversions.ToInteger(borderColor))
        Else
            Me.m_StateBorderColor = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal value As Integer, ByVal backColor As String, ByVal backColorBlink As String)
        MyClass.New(value, backColor)
        If (Not (String.IsNullOrEmpty(backColorBlink) Or Operators.CompareString(backColorBlink, "0", False) = 0)) Then
            Me.m_StateBackColorBlink = Color.FromArgb(Conversions.ToInteger(backColorBlink))
        Else
            Me.m_StateBackColorBlink = Color.Empty
        End If
    End Sub

    Public Sub New(ByVal value As Integer, ByVal backColor As String)
        MyClass.New(backColor)
        Me.m_Value = value
    End Sub

    Public Sub New(ByVal backColor As String)
        MyBase.New()
        If (Not (String.IsNullOrEmpty(backColor) Or Operators.CompareString(backColor, "0", False) = 0)) Then
            Me.m_StateBackColor = Color.FromArgb(Conversions.ToInteger(backColor))
        Else
            Me.m_StateBackColor = Color.Empty
        End If
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim shapesStateValue As ShapesStateValue = New ShapesStateValue() With
        {
            .Value = Me.m_Value,
            .StateBackColor = Me.m_StateBackColor,
            .StateBackColorBlink = Me.m_StateBackColorBlink,
            .StateBorderColor = Me.m_StateBorderColor,
            .StateBorderColorBlink = Me.m_StateBorderColorBlink,
            .StateBlink = Me.m_StateBlink
        }
        Return shapesStateValue
    End Function
End Class
