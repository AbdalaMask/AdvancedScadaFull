Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Interface IKeyboard

    Event ButtonClick As ButtonClickEventHandler

    Property Font As Font

    Property ForeColor As Color

    Property Location As Point

    Property StartPosition As FormStartPosition

    Property Text As String

    Property TopMost As Boolean

    Property Value As String

    Property Visible As Boolean
    Property PassWordChar As Boolean
    Property CurrentValue As String
    Delegate Sub ButtonClickEventHandler(ByVal sender As Object, ByVal e As KeypadEventArgs)
End Interface
