Option Strict On
Option Explicit On
'********************************************************************************************************
'* 06-FEB-16  v1.1
'* 18-JUN-16 Added more features such as TargetValue and ShowCompassDirections
'*
'* This is a rotational position indicator control which can be used as weather vane as well.
'*
'* It features full circle positional arrow (360 degrees) whose home/zero position can be selected as any angle value 0-360 in 1° increments.
'* The East position is taken as zero angle and the home/zero position angle is measured counterclockwise from East.
'*
'* The "Value" property is defined as double-precision floating point input and reflects the angle of the arrow.
'* The angle is in degrees and can be positive or negative, measured clockwise from zero position when positive and counter-clockwise when negative.
'* Any received value over 360 or below -360 degrees can be shown as value within -360 to 360 degrees range, calculated as:
'*
'*                             multiplier * 360 + remainder
'*
'* Multiplier is positive for positive angles and negative for negative angles.
'*
'* Example 1: received value is 450 degrees which corresponds to 90 degrees (1 * 360 + 90 = 450)
'* Example 2: received value is -725 degrees which corresponds to -5 degrees (-2 * 360 + (-5) = -725)
'*
'* The corresponding -360 to 360 degrees range angle value will always show on the control itself.
'*
'********************************************************************************************************
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RotationalPositionIndicator
    Inherits Control

    Public Event ValueChanged As EventHandler

#Region "Properties"

    Private m_arrowColor As Color = Color.Black
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("The arrow color."), DefaultValue(GetType(Color), "Black"), System.ComponentModel.Category("Appearance")>
    Public Property RPI_ArrowColor As Color
        Get
            Return Me.m_arrowColor
        End Get
        Set(ByVal value As Color)
            If Me.m_arrowColor <> value Then
                Me.m_arrowColor = value
                If ArrowGradientBrush IsNot Nothing Then
                    SetArrowColors()
                End If

                Me.Invalidate()
            End If
        End Set
    End Property

    Private m_ArrowWidth As Integer = 9
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("The arrow width."), DefaultValue(9), System.ComponentModel.Category("Appearance")>
    Public Property RPI_ArrowWidth As Integer
        Get
            Return m_ArrowWidth
        End Get
        Set(value As Integer)
            If m_ArrowWidth <> value Then
                m_ArrowWidth = Math.Max(5, value)
                RefreshImage()
            End If
        End Set
    End Property

    Private m_circleColor As Color = Color.Gainsboro
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("The background circle color."), DefaultValue(GetType(Color), "Gainsboro"), System.ComponentModel.Category("Appearance")>
    Public Property RPI_CircleColor As Color
        Get
            Return Me.m_circleColor
        End Get
        Set(ByVal value As Color)
            If Me.m_circleColor <> value Then
                Me.m_circleColor = value
                RefreshImage()
                Me.Invalidate()
            End If
        End Set
    End Property

    Private m_zeroLineColor As Color = Color.White
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("The arrow zero/home line color."), DefaultValue(GetType(Color), "White"),
       System.ComponentModel.Category("Appearance")>
    Public Property RPI_ZeroLineColor As Color
        Get
            Return Me.m_zeroLineColor
        End Get
        Set(ByVal value As Color)
            If Me.m_zeroLineColor <> value Then
                Me.m_zeroLineColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_zeroPosition As Integer = 90
    <Browsable(True), RefreshProperties(RefreshProperties.All), Editor(GetType(NumericUpDownValueEditor), GetType(UITypeEditor)),
 Description("Indicates the arrow zero/home position (East is set as zero angle and the zero line position angle is measured counterclockwise in increments of 1°)."),
 DefaultValue(90)>
    Public Property RPI_ZeroLinePosition As Integer
        Get
            NumericUpDownValueEditor.nudControl.DecimalPlaces = 0
            NumericUpDownValueEditor.nudControl.Increment = 1
            NumericUpDownValueEditor.nudControl.Minimum = 0
            NumericUpDownValueEditor.nudControl.Maximum = 360
            NumericUpDownValueEditor.valueType = "Integer"
            Return Me.m_zeroPosition
        End Get
        Set(ByVal value As Integer)
            If Me.m_zeroPosition <> value Then
                Me.m_zeroPosition = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_string As String = "0" & "°"
    Private m_Value As Double = 0.0F
    <Browsable(True), RefreshProperties(RefreshProperties.All),
    Description("Indicates the actual received arrow angle value in degrees. It could be any double-precision floating point value."), DefaultValue(0.0F)>
    Public Property Value() As Double
        Get
            Return Me.m_Value
        End Get
        Set(ByVal value As Double)
            If Me.m_Value <> value Then
                Me.m_Value = value
                OnValueChanged(System.EventArgs.Empty)
            End If

            Me.m_string = String.Format(CStr(CDec(Me.m_Value) Mod CDec(360.0F)), "0") & "°"

            Me.Invalidate()
        End Set
    End Property

    Private m_TargetValue As Double
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("First target range value."), DefaultValue(0.0F)>
    Public Property TargetValue As Double
        Get
            Return m_TargetValue
        End Get
        Set(value As Double)
            If m_TargetValue <> value Then
                m_TargetValue = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_TargetValueTolerance As Double = 5.0F
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("First target range tolerance."), DefaultValue(5.0F)>
    Public Property TargetValueTolerance As Double
        Get
            Return m_TargetValueTolerance
        End Get
        Set(value As Double)
            If m_TargetValueTolerance <> value Then
                m_TargetValueTolerance = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_TargetValueColor As Color = Color.Gold
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("First target range color."), DefaultValue(GetType(Color), "Yellow") _
    , System.ComponentModel.Category("Appearance")>
    Public Property TargetValueColor As Color
        Get
            Return m_TargetValueColor
        End Get
        Set(value As Color)
            If m_TargetValueColor <> value Then
                m_TargetValueColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Target2Value As Double
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Second target range value."), DefaultValue(0.0F)>
    Public Property Target2Value As Double
        Get
            Return m_Target2Value
        End Get
        Set(value As Double)
            If m_Target2Value <> value Then
                m_Target2Value = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Target2ValueTolerance As Double = 10.0F
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Second target range tolerance."), DefaultValue(10.0F)>
    Public Property Target2ValueTolerance As Double
        Get
            Return m_Target2ValueTolerance
        End Get
        Set(value As Double)
            If m_Target2ValueTolerance <> value Then
                m_Target2ValueTolerance = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Target2ValueColor As Color = Color.FromArgb(192, 0, 0)
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Second target range color.") _
    , System.ComponentModel.Category("Appearance")>
    Public Property Target2ValueColor As Color
        Get
            Return m_Target2ValueColor
        End Get
        Set(value As Color)
            If m_Target2ValueColor <> value Then
                m_Target2ValueColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_ShowTargetBands As Boolean = True
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Show target bands."), DefaultValue(True) _
    , System.ComponentModel.Category("Appearance")>
    Public Property ShowTargetBands As Boolean
        Get
            Return m_ShowTargetBands
        End Get
        Set(value As Boolean)
            If m_ShowTargetBands <> value Then
                m_ShowTargetBands = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_ShowZeroTick As Boolean = True
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Display a tick to represent the zero position."), DefaultValue(True) _
    , System.ComponentModel.Category("Appearance")>
    Public Property ShowZeroTick As Boolean
        Get
            Return m_ShowZeroTick
        End Get
        Set(value As Boolean)
            If m_ShowZeroTick <> value Then
                m_ShowZeroTick = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_ShowCompassDirections As Boolean
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Display compass directions (N, E, S and W)."), DefaultValue(False) _
    , System.ComponentModel.Category("Appearance")>
    Public Property ShowCompassDirections As Boolean
        Get
            Return m_ShowCompassDirections
        End Get
        Set(value As Boolean)
            If m_ShowCompassDirections <> value Then
                m_ShowCompassDirections = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_MajorTickColor As Color = Color.Black
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Major tick color."), DefaultValue(GetType(Color), "Black"),
      System.ComponentModel.Category("Appearance")>
    Public Property RPI_MajorTickColor As Color
        Get
            Return m_MajorTickColor
        End Get
        Set(value As Color)
            If value <> m_MajorTickColor Then
                m_MajorTickColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_MinorTickColor As Color = Color.FromArgb(20, 20, 20)
    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Minor tick color."),
      System.ComponentModel.Category("Appearance")>
    Public Property RPI_MinorTickColor As Color
        Get
            Return m_MinorTickColor
        End Get
        Set(value As Color)
            If value <> m_MinorTickColor Then
                m_MinorTickColor = value
                RefreshImage()
            End If
        End Set
    End Property

    <Browsable(True), RefreshProperties(RefreshProperties.All), Description("Text to show on the control.")>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If String.Compare(MyBase.Text, value) <> 0 Then
                MyBase.Text = value
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New()

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.ContainerControl Or ControlStyles.SupportsTransparentBackColor, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.BackColor = Color.Transparent
        MyBase.ForeColor = Color.Black
        Me.Size = New Size(120, 120)
        Me.MinimumSize = New Size(60, 60)
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If ForeColorBrush IsNot Nothing Then ForeColorBrush.Dispose()
                If ArrowGradientBrush IsNot Nothing Then ArrowGradientBrush.Dispose()
                If sf IsNot Nothing Then sf.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region

#Region "Events"

    Private OuterCircleBounds, InnerCircleBounds, MiddleCircleBounds As Rectangle
    Private CurvedArrowBottomBounds As Rectangle
    Private ArrowPoints(6) As PointF
    Private ForeColorBrush As SolidBrush
    Private sf As StringFormat
    Private ArrowGradientBrush As LinearGradientBrush
    Private BackImage As Image

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        '* Prevent an exception if RefreshImage was not called
        If ForeColorBrush Is Nothing Then Exit Sub

        e.Graphics.SmoothingMode = SmoothingMode.HighQuality

        '* Static Back Image
        e.Graphics.DrawImage(BackImage, 0, 0)


        If Not String.IsNullOrEmpty(Me.Text) Then
            e.Graphics.DrawString(Me.Text, Me.Font, ForeColorBrush, New Point(CInt(Me.Width / 2.0F), CInt(Me.Height * 0.55)), sf)
        End If

        '*************************************
        '* Draw the Arrow
        '*************************************
        e.Graphics.TranslateTransform(CSng(Me.ClientRectangle.Width) / 2.0F, CSng(Me.ClientRectangle.Height) / 2.0F)
        e.Graphics.RotateTransform(CSng(Me.m_Value - CSng(Me.m_zeroPosition)))
        e.Graphics.TranslateTransform(-CSng(Me.ClientRectangle.Width) / 2.0F, -CSng(Me.ClientRectangle.Height) / 2.0F)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.FillEllipse(ArrowGradientBrush, CurvedArrowBottomBounds)
        e.Graphics.FillPolygon(ArrowGradientBrush, ArrowPoints)

        '***************************************
        e.Graphics.ResetTransform()

        e.Graphics.DrawString(Me.m_string, Me.Font, ForeColorBrush, New Point(CInt(Me.Width / 2.0F), CInt(Me.Height * 2.0F / 3.0F)), sf)
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
        MyBase.OnForeColorChanged(e)

        ForeColorBrush = New SolidBrush(Me.ForeColor)
    End Sub

    Private Sub RotationalPositionIndicator_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = Me.Height
        RefreshImage()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

#End Region

#Region "Private Methods"

    Private RingThickness As Integer
    Private Sub RefreshImage()

        OuterCircleBounds = New Rectangle(New Point(0, 0), New Size(CInt(Me.Width - 1), CInt(Me.Height - 1)))

        '* Limit painting region so corners do not hide other close controls
        Dim CircularGagePath As New System.Drawing.Drawing2D.GraphicsPath
        CircularGagePath.AddEllipse(OuterCircleBounds)
        Me.Region = New System.Drawing.Region(CircularGagePath)

        RingThickness = CInt(Me.Width / 17)
        MiddleCircleBounds = New Rectangle(New Point(RingThickness, RingThickness), New Size(CInt(Me.Width - RingThickness * 2), CInt(Me.Height - RingThickness * 2)))

        '* If there are target values, then create a gap for their color bands
        Dim GapBetweenRingAndGageFace As Integer = RingThickness
        If m_ShowTargetBands Or m_ShowZeroTick Then
            GapBetweenRingAndGageFace = CInt(Me.Width / 10)
        End If

        InnerCircleBounds = New Rectangle(New Point(GapBetweenRingAndGageFace, GapBetweenRingAndGageFace), New Size(CInt(Me.Width - GapBetweenRingAndGageFace * 2), CInt(Me.Height - GapBetweenRingAndGageFace * 2)))

        Dim Target2ValueBrush As New SolidBrush(m_Target2ValueColor)
        Dim TargetValueBrush As New SolidBrush(m_TargetValueColor)
        Dim CircleColorBrush As New SolidBrush(Me.m_circleColor)

        If ForeColorBrush Is Nothing Then
            ForeColorBrush = New SolidBrush(Me.ForeColor)
        End If

        SetArrowColors()
        GetOuterRingColors()

        BackImage = New Bitmap(Me.Width, Me.Height)
        Dim TickLength As Integer = 15
        Dim ThinPen As New Pen(m_MinorTickColor, 1)
        Dim ThickPen As New Pen(m_MajorTickColor, 2)
        Dim PenToUse As Pen = ThinPen
        Dim ZeroLinePen As New Pen(m_zeroLineColor, 3)
        Using g As Graphics = Graphics.FromImage(BackImage)
            g.SmoothingMode = SmoothingMode.AntiAlias

            '* Outside circle
            g.FillEllipse(GetOuterRingColors, OuterCircleBounds)

            If m_Target2ValueTolerance > 0 And m_ShowTargetBands Then
                g.FillPie(Target2ValueBrush, MiddleCircleBounds, CSng(m_Target2Value - m_Target2ValueTolerance - (Me.m_zeroPosition)),
                                                                                  CSng(m_Target2ValueTolerance * 2))
            End If

            If m_TargetValueTolerance > 0 And m_ShowTargetBands Then
                g.FillPie(TargetValueBrush, MiddleCircleBounds, CSng(m_TargetValue - m_TargetValueTolerance - (Me.m_zeroPosition)),
                                                                                CSng(m_TargetValueTolerance * 2))
            End If

            '* Main circle
            g.FillEllipse(CircleColorBrush, InnerCircleBounds)

            For i = 0 To 71
                If (i Mod 3) = 0 Then
                    TickLength = CInt(Me.Width / 10.0F)
                    PenToUse = ThickPen
                Else
                    TickLength = CInt(Me.Width / 15.0F)
                    PenToUse = ThinPen
                End If
                g.DrawLine(PenToUse, CInt(((InnerCircleBounds.Width - TickLength) / 2) * Math.Cos(i * 5 * Math.PI / 180) + InnerCircleBounds.Width / 2) + CInt((OuterCircleBounds.Width - InnerCircleBounds.Width) / 2),
                           CInt(((InnerCircleBounds.Width - TickLength) / 2) * Math.Sin(i * 5 * Math.PI / 180) + InnerCircleBounds.Height / 2) + CInt((OuterCircleBounds.Height - InnerCircleBounds.Height) / 2),
                           CInt(((InnerCircleBounds.Width) / 2) * Math.Cos(i * 5 * Math.PI / 180) + InnerCircleBounds.Width / 2) + CInt((OuterCircleBounds.Width - InnerCircleBounds.Width) / 2),
                           CInt(((InnerCircleBounds.Width) / 2) * Math.Sin(i * 5 * Math.PI / 180) + InnerCircleBounds.Height / 2) + CInt((OuterCircleBounds.Height - InnerCircleBounds.Height) / 2))
            Next

            If m_ShowCompassDirections Then
                Dim f As New Font("Bell MT", CSng(20 * Me.Height / 300), FontStyle.Bold)
                Dim sf2 As New StringFormat

                sf2.Alignment = StringAlignment.Center
                sf2.LineAlignment = StringAlignment.Far
                g.DrawString("N", f, ForeColorBrush, CInt(Me.Width / 2.0F), OuterCircleBounds.Height - MiddleCircleBounds.Height + GapBetweenRingAndGageFace + 10, sf2)

                sf2.LineAlignment = StringAlignment.Near
                g.DrawString("S", f, ForeColorBrush, CInt(Me.Width / 2.0F), Me.Height - (OuterCircleBounds.Height - MiddleCircleBounds.Height + GapBetweenRingAndGageFace + 10), sf2)

                sf2.Alignment = StringAlignment.Far
                sf2.LineAlignment = StringAlignment.Center
                g.DrawString("W", f, ForeColorBrush, OuterCircleBounds.Width - InnerCircleBounds.Width + 15, CInt(Me.Width / 2.0F), sf2)

                sf2.Alignment = StringAlignment.Near
                g.DrawString("E", f, ForeColorBrush, Me.Width - (OuterCircleBounds.Width - InnerCircleBounds.Width + 10), CInt(Me.Width / 2.0F), sf2)
            End If


            If m_ShowZeroTick Then
                g.DrawLine(ZeroLinePen, CInt(Me.Width / 2) + CInt((InnerCircleBounds.Width / 2) * Math.Cos(m_zeroPosition * Math.PI / 180)),
                           CInt(Me.Height / 2) - CInt((InnerCircleBounds.Width / 2) * Math.Sin(m_zeroPosition * Math.PI / 180)),
                           CInt(Me.Width / 2) + CInt((MiddleCircleBounds.Width / 2) * Math.Cos(m_zeroPosition * Math.PI / 180)),
                           CInt(Me.Height / 2) - CInt((MiddleCircleBounds.Width / 2) * Math.Sin(m_zeroPosition * Math.PI / 180)))
            End If
        End Using

        '********************************************
        '* Arrow
        '********************************************
        CurvedArrowBottomBounds = New Rectangle(New Point(CInt(Me.Width / 2.0F - m_ArrowWidth / 2.0F), CInt(Me.Height / 2 - m_ArrowWidth / 2.0F)),
                                         New Size((m_ArrowWidth), (m_ArrowWidth)))

        '* Draw Arrow pointing to right
        '* Top left
        ArrowPoints(0) = New PointF(Me.Width / 2.0F, Me.Height / 2.0F - m_ArrowWidth / 2.0F) ' Me.Height * 3.1F / 7.0F)
        ArrowPoints(1) = New PointF(Me.Width * 5.25F / 7.0F, Me.Height / 2.0F - m_ArrowWidth / 2.0F)
        ArrowPoints(2) = New PointF(Me.Width * 5.25F / 7.0F, Me.Height / 2.0F - m_ArrowWidth) ' Me.Height * 6.0F / 16.0F)
        '* Arrow Tip
        ArrowPoints(3) = New PointF(Me.Width - (Me.Width - InnerCircleBounds.Width) / 2.0F, Me.Height / 2.0F) '* 3.0F / 7.0F)
        ArrowPoints(4) = New PointF(Me.Width * 5.25F / 7.0F, Me.Height / 2.0F + m_ArrowWidth) ' Me.Height * 10.0F / 16.0F)
        ArrowPoints(5) = New PointF(Me.Width * 5.25F / 7.0F, Me.Height / 2.0F + m_ArrowWidth / 2.0F)
        '* Bottom Left
        ArrowPoints(6) = New PointF(Me.Width / 2.0F, Me.Height / 2.0F + m_ArrowWidth / 2.0F)

        If sf Is Nothing Then
            sf = New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
        End If

        Target2ValueBrush.Dispose()
        TargetValueBrush.Dispose()
        CircleColorBrush.Dispose()


        Me.Invalidate()
    End Sub

    Private Sub SetArrowColors()
        If ArrowGradientBrush IsNot Nothing Then
            ArrowGradientBrush.Dispose()
        End If

        ArrowGradientBrush = New LinearGradientBrush(New Point(0, CInt(Me.Height / 2 - m_ArrowWidth)),
                                                     New Point(0, CInt(Me.Height / 2 + m_ArrowWidth)),
                                                Me.m_arrowColor, ControlPaint.LightLight(Me.m_arrowColor))

        '* Create a 3 point gradient to give the arrow depth
        Dim stops() As Single = {0.0F, 0.3F, 0.5F, 0.7F, 1.0F}
        Dim colrs() As Color = {m_arrowColor, m_arrowColor, ControlPaint.Light(m_arrowColor, 0.5), m_arrowColor, m_arrowColor}
        Dim cb As New ColorBlend
        cb.Positions = stops
        cb.Colors = colrs
        ArrowGradientBrush.InterpolationColors = cb
    End Sub

    '************************************************************
    '* Create the brush to give the outer ring its 3d appearance
    '************************************************************
    Private Function GetOuterRingColors() As PathGradientBrush
        Dim pth As New GraphicsPath
        pth.AddEllipse(OuterCircleBounds)

        Dim OuterRingGradientBrush As New PathGradientBrush(pth)

        Dim stops() As Single = {0.0F, 0.06, 0.12, 1.0F}
        Dim colrs() As Color = {Color.Black, Color.White, Color.Black, Color.Black}
        Dim cb As New ColorBlend
        cb.Positions = stops
        cb.Colors = colrs
        OuterRingGradientBrush.InterpolationColors = cb

        Return OuterRingGradientBrush
    End Function

#End Region

End Class

Public Class NumericUpDownValueEditor
    Inherits UITypeEditor

    'Class references:
    'http://stackoverflow.com/questions/14291291/how-to-add-numericupdown-control-to-custom-property-grid-in-c
    'https://msdn.microsoft.com/en-CA/library/ms171840.aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1
    '30-JUL-2015 Converted to VB Net By Godra and modified.

    Public Shared nudControl As New NumericUpDown()
    Public Shared valueType As String

    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim editorService As System.Windows.Forms.Design.IWindowsFormsEditorService = Nothing
        If provider IsNot Nothing Then
            editorService = TryCast(provider.GetService(GetType(System.Windows.Forms.Design.IWindowsFormsEditorService)), System.Windows.Forms.Design.IWindowsFormsEditorService)
        End If

        If editorService IsNot Nothing Then
            nudControl.Value = CDec(value)
            editorService.DropDownControl(nudControl)
            If valueType = "Single" Then value = CSng(nudControl.Value)
            If valueType = "Integer" Then value = CInt(nudControl.Value)
        End If

        Return value
    End Function
End Class
