Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D

Friend Class PanelGraphics
    Public Shared Function GetRoundPath(ByVal r As Rectangle, ByVal depth As Integer) As GraphicsPath
        Dim graphPath As New GraphicsPath()

        graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90)
        graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90)
        graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90)
        graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90)
        graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth \ 2)

        Return graphPath
    End Function
End Class
Friend Class A1PanelGlobals
    Public Const A1Category As String = "A1"
End Class

