Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class ListButtons
    Inherits Panel
    Implements ISupportInitialize
    Private int_0 As Integer

    Private int_1 As Integer

    Private int_2 As Integer

    Private collection_0 As Collection(Of String)

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Items As Collection(Of String)
        Get
            Return Me.collection_0
        End Get
    End Property

    Public Property MaximumColumns As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            Me.int_1 = Math.Max(1, value)
        End Set
    End Property

    Public Property MaximumRows As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            Me.int_2 = Math.Max(1, value)
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.int_1 = 10
        Me.int_2 = 10
        Me.collection_0 = New Collection(Of String)()
    End Sub

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
    End Sub

    Protected Overridable Sub EndInit() Implements ISupportInitialize.EndInit
        Me.RefreshDisplay()
    End Sub

    Private Sub method_0(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnButtonClicked(New ListButtonsEventArgs(DirectCast(sender, Button).Text))
    End Sub

    Private Sub method_1(ByVal sender As Object, ByVal e As EventArgs)


    End Sub

    Private Sub method_2(ByVal sender As Object, ByVal e As EventArgs)


    End Sub

    Protected Overridable Sub OnButtonClicked(ByVal e As ListButtonsEventArgs)
        RaiseEvent ItemSelected(Me, e)
    End Sub

    Public Sub RefreshDisplay()
        Dim num As Integer = 0
        MyBase.Controls.Clear()
        If (Me.collection_0.Count > 0) Then
            Dim [single] As Single = CSng(Math.Min(Math.Sqrt(CDbl(Me.collection_0.Count)), CDbl(Me.int_2)))
            Dim num1 As Integer = Convert.ToInt32(Math.Ceiling(Math.Min(Math.Sqrt(CDbl(Me.collection_0.Count)), CDbl(Me.int_2))))
            Dim num2 As Integer = CInt(Math.Ceiling(Math.Min(Math.Sqrt(CDbl(Me.collection_0.Count)), CDbl(Me.int_1))))
            Dim num3 As Integer = Convert.ToInt32(Math.Max(1, CDbl(MyBase.Width) * 0.8 / CDbl(num2)))
            Dim num4 As Integer = Convert.ToInt32(CDbl(MyBase.Height) / CDbl(num1))
            Dim int0 As Integer = Me.int_0 * num1 * num2
            While int0 < Me.collection_0.Count And num < num1
                Dim num5 As Integer = 0
                While int0 < Me.collection_0.Count And num5 < num2
                    Dim button As System.Windows.Forms.Button = New System.Windows.Forms.Button() With
                    {
                        .Width = num3,
                        .Height = num4,
                        .Location = New Point(CInt(Math.Round(CDbl((num5 * num3)) + CDbl(MyBase.Width) * 0.1)), num * num4),
                        .Font = Me.Font,
                        .Text = Me.collection_0(int0)
                    }
                    AddHandler button.Click, New EventHandler(AddressOf Me.method_0)
                    MyBase.Controls.Add(button)
                    int0 = int0 + 1
                    num5 = num5 + 1
                End While
                num = num + 1
            End While
            Dim button1 As System.Windows.Forms.Button = New System.Windows.Forms.Button() With
            {
                .Location = New Point(CInt(Math.Round(CDbl(MyBase.Width) * 0.9)), 0),
                .Width = Convert.ToInt32(CDbl(MyBase.Width) * 0.1),
                .Height = MyBase.Height,
                .Font = Me.Font
            }
            If (int0 >= Me.collection_0.Count) Then
                button1.Enabled = False
            Else
                button1.Text = ">"
                AddHandler button1.Click, New EventHandler(AddressOf Me.method_1)
            End If
            MyBase.Controls.Add(button1)
            Dim button2 As System.Windows.Forms.Button = New System.Windows.Forms.Button() With
            {
                .Location = New Point(0, 0),
                .Width = Convert.ToInt32(CDbl(MyBase.Width) * 0.1),
                .Height = MyBase.Height,
                .Font = Me.Font
            }
            If (Me.int_0 <= 0) Then
                button2.Enabled = False
            Else
                button2.Text = "<"
                AddHandler button2.Click, New EventHandler(AddressOf Me.method_2)
            End If
            MyBase.Controls.Add(button2)
        End If
    End Sub

    Public Event ItemSelected As EventHandler(Of ListButtonsEventArgs)

End Class
