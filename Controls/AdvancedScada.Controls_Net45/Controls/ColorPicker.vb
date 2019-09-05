Imports System.Drawing
Imports System.Windows.Forms

Public Class ColorPicker
    Inherits Button

    Public Event ColorChanged As EventHandler

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Cursor = Cursors.Hand
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderColor = Color.Silver
        Me.TextAlign = ContentAlignment.MiddleCenter
        Me.BackColor = Color.White
        Me.ForeColor = Color.Black
        Me.Size = New System.Drawing.Size(200, 30)
        Me.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        Me.AutoEllipsis = True

    End Sub

#End Region

#Region "Basic Properties"

    Private m_BackColor As System.Drawing.Color
    Public Shadows Property BackColor As System.Drawing.Color
        Get
            Return m_BackColor
        End Get
        Set(value As System.Drawing.Color)
            If MyBase.BackColor <> value Then
                MyBase.BackColor = value
                m_BackColor = value

                If value <> Color.Empty Then
                    Me.Text = value.ToString 'value.Name
                Else
                    Me.Text = String.Empty
                End If

                If (CInt(value.R) + CInt(value.G) + CInt(value.B)) > ((255I * 3I) \ 2I) Then
                    Me.ForeColor = Color.Black
                Else
                    Me.ForeColor = Color.White
                End If

                OnValueChanged(EventArgs.Empty)
            End If
        End Set
    End Property

#End Region

#Region "Events"

    Private Sub btn_Click(sender As Object, e As EventArgs) Handles Me.Click

        Dim cDialog As New ColorDialog()
        cDialog.Color = Me.BackColor

        If (cDialog.ShowDialog() = DialogResult.OK) Then
            If Me.BackColor <> cDialog.Color Then
                Me.BackColor = cDialog.Color
            End If
        End If

    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ColorChanged(Me, e)
    End Sub

#End Region

End Class