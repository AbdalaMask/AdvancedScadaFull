Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class ImageDisplayByValue
    Inherits Label
    Public Sub New()
        MyBase.New()
        Me.AutoSize = False
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.WhiteSmoke
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Dim num As Integer = 0
        Dim count As Integer = MyBase.Parent.Site.Container.Components.Count
        While MyBase.ImageList Is Nothing And num < count
            If (Operators.CompareString(MyBase.Parent.Site.Container.Components(num).[GetType]().ToString(), "System.Windows.Forms.ImageList", False) = 0) Then
                MyBase.ImageList = DirectCast(MyBase.Parent.Site.Container.Components(num), System.Windows.Forms.ImageList)
            End If
            num = num + 1
        End While
        If (MyBase.ImageList Is Nothing) Then
            MyBase.Parent.Site.Container.Add(New System.Windows.Forms.ImageList())
            MyBase.ImageList = DirectCast(MyBase.Parent.Site.Container.Components(MyBase.Parent.Site.Container.Components.Count - 1), System.Windows.Forms.ImageList)
        End If
        MyBase.ImageIndex = 0
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        Try
            If (If(MyBase.ImageList Is Nothing OrElse MyBase.ImageList.Images Is Nothing OrElse MyBase.ImageList.Images.Count <= 0, False, MyBase.ImageList.Images(0) IsNot Nothing)) Then
                painte.Graphics.DrawImage(MyBase.ImageList.Images(0), 0, 0, MyBase.Width, MyBase.Height)
            End If
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            ProjectData.ClearProjectError()
        End Try
    End Sub
End Class
