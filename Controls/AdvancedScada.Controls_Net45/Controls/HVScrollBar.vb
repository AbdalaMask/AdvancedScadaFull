Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Public Class HVScrollBar
    Inherits Control
    '* Original code for Horizontal CustomScrollBar from:
    '* https://msdn.microsoft.com/en-us/library/system.windows.forms.scrollbarrenderer(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1
    '*
    '* Modified on 17-MAR-16 By Godra to allow either Horizontal or Vertical orientation, re-sizing of the buttons/thumb and to include Value/Text/Other properties.
    '* The code was organized into Regions.
    '* PLC related events MouseDown/MouseUp were also added to corresponding sections.
    '* 

    Private clickedBarRectangle As Rectangle
    Private thumbRectangle As Rectangle
    Private leftArrowRectangle As Rectangle
    Private rightArrowRectangle As Rectangle
    Private leftArrowClicked As Boolean
    Private rightArrowClicked As Boolean
    Private leftBarClicked As Boolean
    Private rightBarClicked As Boolean
    Private thumbClicked As Boolean
    Private showValue As Boolean
    Private thumbState As ScrollBarState = ScrollBarState.Normal
    Private leftButtonState As ScrollBarArrowButtonState
    Private rightButtonState As ScrollBarArrowButtonState

    ' This control does allow widths/heights to be altered.
    Private m_thumbWidth As Integer = 15
    Private m_arrowWidth As Integer = 15

    ' Set the right/bottom limit of the thumb's right/bottom border.
    Private thumbRightLimitRight As Integer = 0

    ' Set the right/bottom limit of the thumb's left/top border.
    Private thumbRightLimitLeft As Integer = 0

    ' Set the left/top limit of thumb's left/top border.
    Private thumbLeftLimit As Integer = 0

    ' Set the distance from the left/top edge of the thumb to the cursor tip.
    Private thumbPosition As Integer = 0

    ' Set the distance from the left/top edge of the scroll bar track to the cursor tip.
    Private trackPosition As Integer = 0

    ' This timer draws the moving thumb while the scroll arrows or track are pressed.
    Private WithEvents progressTimer As New Timer()

    Public Event ValueChanged As EventHandler

#Region "Properties"

    Enum Dir
        Horizontal = 0
        Vertical = 1
    End Enum

    Private currentWidth, currentHeight As Single
    Private m_Orientation As Dir = Dir.Horizontal
    <Browsable(True), Category("Properties"), Description("The ScrollBar orientation."), DefaultValue(Dir.Horizontal)>
    Public Property ScrollBar_Orientation As Dir
        Get
            Return Me.m_Orientation
        End Get
        Set(ByVal value As Dir)
            If value = Dir.Horizontal Then
                Me.leftButtonState = ScrollBarArrowButtonState.LeftNormal
                Me.rightButtonState = ScrollBarArrowButtonState.RightNormal
            Else
                Me.leftButtonState = ScrollBarArrowButtonState.UpNormal
                Me.rightButtonState = ScrollBarArrowButtonState.DownNormal
            End If

            If Me.m_Orientation <> value Then
                Me.currentWidth = Me.Width
                Me.currentHeight = Me.Height
                Me.m_Orientation = value
                Me.Width = Me.currentHeight
                Me.Height = Me.currentWidth
                Me.HVScrollBar_Resize(Me, Nothing)
            End If
            Me.Invalidate()
        End Set
    End Property

    Private m_Minimum As Integer
    <Browsable(True), Category("Properties"), Description("The ScrollBar minimum value. Changing this value will reset the thumb position to Minimum."), DefaultValue(0)>
    Public Property Minimum As Integer
        Get
            Return Me.m_Minimum
        End Get
        Set(ByVal value As Integer)
            If value >= Me.m_Maximum Then Exit Property
            If Me.m_Minimum <> value Then
                Me.m_Minimum = value
                Me.Value = Me.m_Minimum
            End If
            Me.Invalidate()
        End Set
    End Property

    Private m_Maximum As Integer = 100
    <Browsable(True), Category("Properties"), Description("The ScrollBar maximum value. Changing this value will reset the thumb position to Minimum."), DefaultValue(100)>
    Public Property Maximum As Integer
        Get
            Return Me.m_Maximum
        End Get
        Set(ByVal value As Integer)
            If value <= Me.m_Minimum Then Exit Property
            If Me.m_Maximum <> value Then
                Me.m_Maximum = value
                Me.Value = Me.m_Minimum
            End If
            Me.Invalidate()
        End Set
    End Property

    Private m_Value As Integer
    <Browsable(True), Category("Properties"), Description("The ScrollBar current value."), DefaultValue(0)>
    Public Property Value As Integer
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Integer)
            If value < Me.m_Minimum OrElse value > Me.m_Maximum Then Exit Property
            Me.m_Value = value
            Dim OutputRange As Integer = Me.m_Maximum - Me.m_Minimum
            If Me.m_Orientation = Dir.Horizontal Then
                Dim InputRange As Integer = Me.Width - 2 * m_arrowWidth - m_thumbWidth
                thumbRectangle.X = ((m_Value - Me.m_Minimum) * InputRange / OutputRange) + m_arrowWidth
            Else
                Dim InputRange As Integer = Me.Height - 2 * m_arrowWidth - m_thumbWidth
                thumbRectangle.Y = ((m_Value - Me.m_Minimum) * InputRange / OutputRange) + m_arrowWidth
            End If
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("Show current value on the ScrollBar."), DefaultValue(False)>
    Public Property ScrollBar_ShowValue As Boolean
        Get
            Return showValue
        End Get
        Set(ByVal value As Boolean)
            If showValue <> value Then
                showValue = value
            End If
            SetUpScrollBar()
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("The ScrollBar arrow button width (valid values 10 up to 1/4 of the control's Width or Height dependent on the current orientation)."), DefaultValue(15)>
    Public Property ScrollBar_ArrowWidth As Integer
        Get
            Return m_arrowWidth
        End Get
        Set(ByVal value As Integer)
            If Not IsNumeric(value) OrElse value < 10 Then Exit Property
            If Me.m_Orientation = Dir.Horizontal Then
                If value > Me.Width / 4 Then Exit Property
            Else
                If value > Me.Height / 4 Then Exit Property
            End If
            If m_arrowWidth <> value Then
                m_arrowWidth = value
            End If
            SetUpScrollBar()
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("The ScrollBar thumb width (valid values 10 up to 1/4 of the control's Width or Height dependent on the current orientation)."), DefaultValue(15)>
    Public Property ScrollBar_ThumbWidth As Integer
        Get
            Return m_thumbWidth
        End Get
        Set(ByVal value As Integer)
            If Not IsNumeric(value) OrElse value < 10 Then Exit Property
            If Me.m_Orientation = Dir.Horizontal Then
                If value > Me.Width / 4 Then Exit Property
            Else
                If value > Me.Height / 4 Then Exit Property
            End If
            If m_thumbWidth <> value Then
                m_thumbWidth = value
            End If
            SetUpScrollBar()
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Properties"), Description("Text to display on the control.")>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If String.Compare(MyBase.Text, value) <> 0 Then
                MyBase.Text = value
            End If
            Me.Invalidate()
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        With Me
            .Width = 200
            .Height = 20
            .DoubleBuffered = True
            .ForeColor = Color.Black
        End With
        If Me.m_Orientation = Dir.Horizontal Then
            Me.leftButtonState = ScrollBarArrowButtonState.LeftNormal
            Me.rightButtonState = ScrollBarArrowButtonState.RightNormal
        Else
            Me.leftButtonState = ScrollBarArrowButtonState.UpNormal
            Me.rightButtonState = ScrollBarArrowButtonState.DownNormal
        End If
        progressTimer.Interval = 20
        SetUpScrollBar()
        Me.Focus()
    End Sub

#End Region

#Region "Private Methods"

    ' Calculate the sizes of the scroll bar elements.
    Private Sub SetUpScrollBar()

        clickedBarRectangle = Me.ClientRectangle
        ' Set the default starting thumb position.
        thumbPosition = m_thumbWidth / 2

        If Me.m_Orientation = Dir.Horizontal Then
            thumbRectangle = New Rectangle(Me.ClientRectangle.X + Me.m_arrowWidth, Me.ClientRectangle.Y, m_thumbWidth, Me.ClientRectangle.Height)
            leftArrowRectangle = New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Y, m_arrowWidth, Me.ClientRectangle.Height)
            rightArrowRectangle = New Rectangle(Me.ClientRectangle.Right - m_arrowWidth, Me.ClientRectangle.Y, m_arrowWidth, Me.ClientRectangle.Height)

            ' Set the right limit of the thumb's right border.
            thumbRightLimitRight = Me.ClientRectangle.Right - m_arrowWidth

            ' Set the right limit of the thumb's left border.
            thumbRightLimitLeft = thumbRightLimitRight - m_thumbWidth

            ' Set the left limit of the thumb's left border.
            thumbLeftLimit = Me.ClientRectangle.X + m_arrowWidth
        Else
            thumbRectangle = New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Bottom - m_arrowWidth - Me.m_thumbWidth, Me.ClientRectangle.Width, m_thumbWidth)
            leftArrowRectangle = New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Y, Me.ClientRectangle.Width, m_arrowWidth)
            rightArrowRectangle = New Rectangle(Me.ClientRectangle.X, Me.ClientRectangle.Bottom - m_arrowWidth, Me.ClientRectangle.Width, m_arrowWidth)

            ' Set the bottom limit of the thumb's bottom border.
            thumbRightLimitRight = Me.ClientRectangle.Bottom - m_arrowWidth

            ' Set the bottom limit of the thumb's top border.
            thumbRightLimitLeft = thumbRightLimitRight - m_thumbWidth

            ' Set the top limit of the thumb's top border.
            thumbLeftLimit = Me.ClientRectangle.Y + m_arrowWidth
        End If
        Me.Value = Me.m_Value
        Me.Invalidate()
    End Sub

    ' Handle the timer tick by updating the thumb position.
    Private Sub progressTimer_Tick(ByVal sender As Object, ByVal myEventArgs As EventArgs) Handles progressTimer.Tick

        If Not ScrollBarRenderer.IsSupported Then
            Return
        End If

        If Me.m_Orientation = Dir.Horizontal Then
            ' If an arrow is clicked, move the thumb in small increments.
            If rightArrowClicked And thumbRectangle.X < thumbRightLimitLeft Then
                thumbRectangle.X += 1

            ElseIf leftArrowClicked And thumbRectangle.X > thumbLeftLimit Then
                thumbRectangle.X -= 1

                ' If the track bar to right of the thumb is clicked, move the 
                ' thumb to the right in larger increments until the right edge 
                ' of the thumb hits the cursor.
            ElseIf rightBarClicked And thumbRectangle.X < thumbRightLimitLeft And thumbRectangle.X + thumbRectangle.Width < trackPosition Then
                thumbRectangle.X += 3

                ' If the track bar to left of the thumb is clicked, move the 
                ' thumb to the left in larger increments until the left edge 
                ' of the thumb hits the cursor.
            ElseIf leftBarClicked And thumbRectangle.X > thumbLeftLimit And thumbRectangle.X > trackPosition Then
                thumbRectangle.X -= 3
            End If
        Else
            ' If an arrow is clicked, move the thumb in small increments.
            If rightArrowClicked And thumbRectangle.Y < thumbRightLimitLeft Then
                thumbRectangle.Y += 1

            ElseIf leftArrowClicked And thumbRectangle.Y > thumbLeftLimit Then
                thumbRectangle.Y -= 1

                ' If the track bar to right of the thumb is clicked, move the 
                ' thumb to the right in larger increments until the right edge 
                ' of the thumb hits the cursor.
            ElseIf rightBarClicked And thumbRectangle.Y < thumbRightLimitLeft And thumbRectangle.Y + thumbRectangle.Height < trackPosition Then
                thumbRectangle.Y += 3

                ' If the track bar to left of the thumb is clicked, move the 
                ' thumb to the left in larger increments until the left edge 
                ' of the thumb hits the cursor.
            ElseIf leftBarClicked And thumbRectangle.Y > thumbLeftLimit And thumbRectangle.Y > trackPosition Then
                thumbRectangle.Y -= 3
            End If
        End If

        Me.Invalidate()
    End Sub

    Private Sub HVScrollBar_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
        Me.Invalidate()
    End Sub

    Private Sub HVScrollBar_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.m_Orientation = Dir.Horizontal Then
            If m_arrowWidth > m_thumbWidth Then
                If Me.Width < 4 * m_arrowWidth Then Me.Width = 4 * m_arrowWidth
            Else
                If Me.Width < 4 * m_thumbWidth Then Me.Width = 4 * m_thumbWidth
            End If
            If Me.Height < 15 Then Me.Height = 15
        Else
            If m_arrowWidth > m_thumbWidth Then
                If Me.Height < 4 * m_arrowWidth Then Me.Height = 4 * m_arrowWidth
            Else
                If Me.Height < 4 * m_thumbWidth Then Me.Height = 4 * m_thumbWidth
            End If
            If Me.Width < 15 Then Me.Width = 15
        End If
        SetUpScrollBar()
        RaiseEvent ValueChanged(Me, e)
    End Sub

#End Region

#Region "Protected Methods"

    ' Draw the scroll bar in its normal state.
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' Visual styles are not enabled.
        If Not ScrollBarRenderer.IsSupported Then
            'Me.Parent.Text = "CustomScrollBar Disabled"
            Return
        End If

        'Me.Parent.Text = "CustomScrollBar Enabled"

        ' Draw the scroll bar track.
        If Me.m_Orientation = Dir.Horizontal Then
            ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, Me.ClientRectangle, ScrollBarState.Normal)
        Else
            ScrollBarRenderer.DrawLowerVerticalTrack(e.Graphics, Me.ClientRectangle, ScrollBarState.Normal)
        End If

        ' Draw the thumb and thumb grip in the current state.
        If Me.m_Orientation = Dir.Horizontal Then
            ScrollBarRenderer.DrawHorizontalThumb(e.Graphics, thumbRectangle, thumbState)
            ScrollBarRenderer.DrawHorizontalThumbGrip(e.Graphics, thumbRectangle, thumbState)
        Else
            ScrollBarRenderer.DrawVerticalThumb(e.Graphics, thumbRectangle, thumbState)
            ScrollBarRenderer.DrawVerticalThumbGrip(e.Graphics, thumbRectangle, thumbState)
        End If

        ' Draw the scroll arrows in the current state.
        ScrollBarRenderer.DrawArrowButton(e.Graphics, leftArrowRectangle, leftButtonState)
        ScrollBarRenderer.DrawArrowButton(e.Graphics, rightArrowRectangle, rightButtonState)

        ' Draw a highlighted rectangle in the left side of the scroll 
        ' bar track if the user has clicked between the left arrow 
        ' and thumb.
        If leftBarClicked Then
            If Me.m_Orientation = Dir.Horizontal Then
                clickedBarRectangle.X = thumbLeftLimit
                clickedBarRectangle.Width = thumbRectangle.X - thumbLeftLimit
                ScrollBarRenderer.DrawLeftHorizontalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed)
            Else
                clickedBarRectangle.Y = thumbLeftLimit
                clickedBarRectangle.Height = thumbRectangle.Y - thumbLeftLimit
                ScrollBarRenderer.DrawUpperVerticalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed)
            End If

            ' Draw a highlighted rectangle in the right side of the scroll 
            ' bar track if the user has clicked between the right arrow 
            ' and thumb.
        ElseIf rightBarClicked Then
            If Me.m_Orientation = Dir.Horizontal Then
                clickedBarRectangle.X = thumbRectangle.X + thumbRectangle.Width
                clickedBarRectangle.Width = thumbRightLimitRight - clickedBarRectangle.X
                ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed)
            Else
                clickedBarRectangle.Y = thumbRectangle.Y + thumbRectangle.Height
                clickedBarRectangle.Height = thumbRightLimitRight - clickedBarRectangle.Y
                ScrollBarRenderer.DrawLowerVerticalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed)
            End If
        End If

        If showValue Then
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(Me.m_Value.ToString, Me.Font, New SolidBrush(Me.ForeColor), New Point(Me.Width / 2, Me.Height / 2), sf)
            Exit Sub
        End If

        If Not String.IsNullOrEmpty(Me.Text) Then
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New Point(Me.Width / 2, Me.Height / 2), sf)
        End If

    End Sub

    ' Handle a mouse click in the scroll bar.
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)



        If Not ScrollBarRenderer.IsSupported Then
            Return
        End If

        ' When the thumb is clicked, update the distance from the left
        ' edge of the thumb to the cursor tip.
        If thumbRectangle.Contains(e.Location) Then
            thumbClicked = True
            If Me.m_Orientation = Dir.Horizontal Then
                thumbPosition = e.Location.X - thumbRectangle.X
            Else
                thumbPosition = e.Location.Y - thumbRectangle.Y
            End If
            thumbState = ScrollBarState.Pressed

            ' When the left arrow is clicked, start the timer to scroll 
            ' while the arrow is held down.
        ElseIf leftArrowRectangle.Contains(e.Location) Then
            leftArrowClicked = True
            If Me.m_Orientation = Dir.Horizontal Then
                leftButtonState = ScrollBarArrowButtonState.LeftPressed
            Else
                leftButtonState = ScrollBarArrowButtonState.UpPressed
            End If
            progressTimer.Start()

            ' When the right arrow is clicked, start the timer to scroll 
            ' while arrow is held down.
        ElseIf rightArrowRectangle.Contains(e.Location) Then
            rightArrowClicked = True
            If Me.m_Orientation = Dir.Horizontal Then
                rightButtonState = ScrollBarArrowButtonState.RightPressed
            Else
                rightButtonState = ScrollBarArrowButtonState.DownPressed
            End If
            progressTimer.Start()

        Else
            ' When the scroll bar is clicked, start the timer to move the
            ' thumb while the mouse is held down.
            If Me.m_Orientation = Dir.Horizontal Then
                trackPosition = e.Location.X
                If e.Location.X < Me.thumbRectangle.X Then
                    leftBarClicked = True
                Else
                    rightBarClicked = True
                End If
            Else
                trackPosition = e.Location.Y
                If e.Location.Y < Me.thumbRectangle.Y Then
                    leftBarClicked = True
                Else
                    rightBarClicked = True
                End If
            End If
            progressTimer.Start()
        End If
        OnValueChanged(System.EventArgs.Empty)
        Me.Invalidate()
    End Sub

    ' Draw the track.
    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        If Not ScrollBarRenderer.IsSupported Then
            Return
        End If

        ' Update the thumb position, if the new location is within 
        ' the bounds.
        If thumbClicked Then
            thumbClicked = False
            thumbState = ScrollBarState.Normal
            If Me.m_Orientation = Dir.Horizontal Then
                If e.Location.X > thumbLeftLimit + thumbPosition And e.Location.X < thumbRightLimitLeft + thumbPosition Then
                    thumbRectangle.X = e.Location.X - thumbPosition
                End If
            Else
                If e.Location.Y > thumbLeftLimit + thumbPosition And e.Location.Y < thumbRightLimitLeft + thumbPosition Then
                    thumbRectangle.Y = e.Location.Y - thumbPosition
                End If
            End If

            ' If one of the four thumb movement areas was clicked, 
            ' stop the timer.
        ElseIf leftArrowClicked Then
            leftArrowClicked = False
            If Me.m_Orientation = Dir.Horizontal Then
                leftButtonState = ScrollBarArrowButtonState.LeftNormal
            Else
                leftButtonState = ScrollBarArrowButtonState.UpNormal
            End If
            progressTimer.Stop()

        ElseIf rightArrowClicked Then
            rightArrowClicked = False
            If Me.m_Orientation = Dir.Horizontal Then
                rightButtonState = ScrollBarArrowButtonState.RightNormal
            Else
                rightButtonState = ScrollBarArrowButtonState.DownNormal
            End If
            progressTimer.Stop()

        ElseIf leftBarClicked Then
            leftBarClicked = False
            progressTimer.Stop()

        ElseIf rightBarClicked Then
            rightBarClicked = False
            progressTimer.Stop()
        End If

        OnValueChanged(System.EventArgs.Empty)
        Me.Invalidate()



    End Sub

    ' Track mouse movements if the user clicks on the scroll bar thumb.
    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If Not ScrollBarRenderer.IsSupported Then
            Return
        End If

        ' Update the thumb position, if the new location is 
        ' within the bounds.
        If thumbClicked Then

            If Me.m_Orientation = Dir.Horizontal Then
                ' The thumb is all the way to the left.
                If e.Location.X <= thumbLeftLimit + thumbPosition Then
                    thumbRectangle.X = thumbLeftLimit

                    ' The thumb is all the way to the right.
                ElseIf e.Location.X >= thumbRightLimitLeft + thumbPosition Then
                    thumbRectangle.X = thumbRightLimitLeft

                    ' The thumb is between the ends of the track.
                Else
                    thumbRectangle.X = e.Location.X - thumbPosition
                End If
            Else
                ' The thumb is all the way to the left.
                If e.Location.Y <= thumbLeftLimit + thumbPosition Then
                    thumbRectangle.Y = thumbLeftLimit

                    ' The thumb is all the way to the right.
                ElseIf e.Location.Y >= thumbRightLimitLeft + thumbPosition Then
                    thumbRectangle.Y = thumbRightLimitLeft

                    ' The thumb is between the ends of the track.
                Else
                    thumbRectangle.Y = e.Location.Y - thumbPosition
                End If
            End If

        End If
        OnValueChanged(System.EventArgs.Empty)
        Me.Invalidate()
    End Sub

    ' Recalculate the sizes of the scroll bar elements.
    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        SetUpScrollBar()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As System.EventArgs)
        'OutputRange = OutputEnd - OutputStart
        Dim OutputRange As Integer = Me.m_Maximum - Me.m_Minimum
        If Me.m_Orientation = Dir.Horizontal Then
            'InputRange = InputEnd - InputStart
            Dim InputRange As Integer = (Me.Width - m_arrowWidth - m_thumbWidth) - m_arrowWidth
            'Output = (Input - InputStart) * OutputRange / InputRange + OutputStart
            Me.Value = CInt((thumbRectangle.X - m_arrowWidth) * OutputRange / InputRange) + Me.m_Minimum
        Else
            'InputRange = InputEnd - InputStart
            Dim InputRange As Integer = (Me.Height - m_arrowWidth - m_thumbWidth) - m_arrowWidth
            'Output = (Input - InputStart) * OutputRange / InputRange + OutputStart
            Me.Value = CInt((thumbRectangle.Y - m_arrowWidth) * OutputRange / InputRange) + Me.m_Minimum
        End If
        RaiseEvent ValueChanged(Me, e)
    End Sub

#End Region

End Class
