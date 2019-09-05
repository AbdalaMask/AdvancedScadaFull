Imports System.Drawing
Imports System.Windows.Forms

Public Class BasicLine
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'mhLineHMI
        '
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Padding = New System.Windows.Forms.Padding(0)
        Me.MinimumSize = New System.Drawing.Size(1, 1)
        Me.Name = "BasicLine"
        Me.Size = New System.Drawing.Size(100, 100)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Enum tOrientation
        Horizontal
        Vertical
        Free
    End Enum

#Region "Properties"
    Private mStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    Public Property DashStyle() As Drawing2D.DashStyle
        Get
            Return mStyle
        End Get
        Set(ByVal value As Drawing2D.DashStyle)
            mStyle = value
            Me.Invalidate()
        End Set
    End Property

    Private mlngPenWidth As Integer = 2
    Public Property PenWidth() As Integer
        Get
            Return mlngPenWidth
        End Get
        Set(ByVal Value As Integer)
            mlngPenWidth = Value
            If mlngPenWidth < 1 Then mlngPenWidth = 1
            Me.Invalidate()
        End Set
    End Property

    Private m_Orientation As tOrientation = tOrientation.Free
    Public Property Orientation() As tOrientation
        Get
            Return m_Orientation
        End Get
        Set(ByVal Value As tOrientation)
            m_Orientation = Value
            Me.Invalidate()
        End Set
    End Property

    Private m_Flip As Boolean = False
    Public Property OrientationFlip() As Boolean
        Get
            Return m_Flip
        End Get
        Set(ByVal Value As Boolean)
            m_Flip = Value
            Me.Invalidate()
            Me.Invalidate()
        End Set
    End Property

    Private m_Color1 As Color = Color.WhiteSmoke
    Public Property Color1 As Color
        Get
            Return m_Color1
        End Get
        Set(ByVal Value As Color)
            m_Color1 = Value
            Me.Invalidate()
        End Set
    End Property

    Private m_Color2 As Color = Color.Green
    Public Property Color2 As Color
        Get
            Return m_Color2
        End Get
        Set(ByVal Value As Color)
            m_Color2 = Value
            Me.Invalidate()
        End Set
    End Property

    Private m_Color3 As Color = Color.Red
    Public Property Color3 As Color
        Get
            Return m_Color3
        End Get
        Set(ByVal Value As Color)
            m_Color3 = Value
            Me.Invalidate()
        End Set
    End Property

    Private m_SelectColor2 As Boolean = False
    Public Property SelectColor2 As Boolean
        Get
            Return m_SelectColor2
        End Get
        Set(ByVal Value As Boolean)
            m_SelectColor2 = Value
            Me.Invalidate()
        End Set
    End Property

    Private m_SelectColor3 As Boolean = False
    Public Property SelectColor3 As Boolean
        Get
            Return m_SelectColor3
        End Get
        Set(ByVal Value As Boolean)
            m_SelectColor3 = Value
            Me.Invalidate()
        End Set
    End Property

#End Region

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        '// Point Variables
        Dim pL2 As Point
        Dim pL4 As Point

        '// Create a pen, you can change the colors if they're wrong.
        Dim iColor As Color = Me.Color1
        If Me.SelectColor2 Then
            iColor = Me.Color2
        ElseIf SelectColor3 Then
            iColor = Me.Color3
        End If

        Me.BackColor = Parent.BackColor

        Dim LP As New Pen(iColor, mlngPenWidth)
        LP.DashStyle = mStyle

        '// Determine orientation then set the height of the control
        '// Set the starting points for first and second lines:
        If m_Orientation = tOrientation.Horizontal Then
            Me.Height = CInt(mlngPenWidth \ 2 + 0.5)
            pL2 = New Point(0, 0)
            pL4 = New Point(Me.Width, 0)

        ElseIf m_Orientation = tOrientation.Vertical Then
            Me.Width = CInt(mlngPenWidth \ 2 + 0.5)
            pL2 = New Point(0, 0)
            pL4 = New Point(0, Me.Height)

        Else '// Free
            If m_Flip Then
                pL2 = New Point(Me.Width, 0)
                pL4 = New Point(0, Me.Height)
            Else
                pL2 = New Point(0, 0)
                pL4 = New Point(Me.Width, Me.Height)
            End If
        End If

        '// Draw the line
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawLine(LP, pL2, pL4)

    End Sub

    Private Sub Control_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Invalidate()
        If Me.Size.Height <= 10 Then
            m_Orientation = tOrientation.Horizontal
        ElseIf Me.Size.Width <= 10 Then
            m_Orientation = tOrientation.Vertical
        Else
            m_Orientation = tOrientation.Free
        End If
    End Sub


End Class