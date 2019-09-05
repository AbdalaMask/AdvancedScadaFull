Public Class ListViewDesigner
    Inherits System.Windows.Forms.Design.ControlDesigner

    Public Overrides ReadOnly Property Verbs As System.ComponentModel.Design.DesignerVerbCollection
        Get
            Dim verbs_ As New System.ComponentModel.Design.DesignerVerbCollection()
            '* "Editor" will be added to the Smart Tags popup menu
            Dim dv1 As New System.ComponentModel.Design.DesignerVerb("Editor", New EventHandler(AddressOf Me.ShowDesignerWindow))
            verbs_.Add(dv1)
            Return verbs_
        End Get
    End Property

    Private Sub ShowDesignerWindow(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.Component IsNot Nothing) Then
            Dim mcdf As New ListViewDesignerForm
            mcdf.ControlToEdit = Component
            mcdf.ShowDialog()
        End If
    End Sub

    Public Overrides Sub DoDefaultAction() 'Implements IDesigner.DoDefaultAction
        'Throw New NotImplementedException()
        If (Me.Component IsNot Nothing) Then
            Dim mcdf As New ListViewDesignerForm
            mcdf.ControlToEdit = Component
            mcdf.ShowDialog()
        End If
    End Sub
End Class
