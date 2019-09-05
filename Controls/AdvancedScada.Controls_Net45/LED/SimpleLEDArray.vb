Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class SimpleLEDArray
    Inherits Control
    Private bool_0 As Boolean

    Private color_0 As Color

    Private led_Col_0 As SimpleLEDArray.LED_Col

    Private led_Bri_0 As SimpleLEDArray.LED_Bri

    Private int_0 As Integer

    Private int_1 As Integer

    Private led_Dir_0 As SimpleLEDArray.LED_Dir

    Private int_2 As Integer

    Private int_3 As Integer

    Private int_4 As Integer

    Private bool_1 As Boolean

    Private Property BlinkTimer As System.Windows.Forms.Timer


    <Browsable(True)>
    <DefaultValue(0)>
    <Description("Set LEDs array orientation (Horizontal or Vertical).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ArrayOrientation As SimpleLEDArray.LED_Dir
        Get
            Return Me.led_Dir_0
        End Get
        Set(ByVal value As SimpleLEDArray.LED_Dir)
            If (Me.led_Dir_0 <> value) Then
                Me.led_Dir_0 = value
                Me.method_2()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(500)>
    <Description("LEDs blinking interval in milliseconds.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BlinkInterval As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            If (value <= 0) Then
                value = 500
            End If
            If (Me.int_3 <> value) Then
                Me.int_3 = value
                Me.BlinkTimer.Interval = Me.int_3
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable border on each LED.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Border As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            Dim enumerator As IEnumerator = Nothing
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLED).LED_Border = Me.bool_0
                    End While
                Finally
                    If (TypeOf enumerator Is IDisposable) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(GetType(Color), "MediumSeaGreen")>
    <Description("LED border color.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_BorderColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            Dim enumerator As IEnumerator = Nothing
            If (Me.color_0 <> value) Then
                Me.color_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLED).LED_BorderColor = Me.color_0
                    End While
                Finally
                    If (TypeOf enumerator Is IDisposable) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("LEDs brightness.")>
    Public Property LED_Brightness As SimpleLEDArray.LED_Bri
        Get
            Return Me.led_Bri_0
        End Get
        Set(ByVal value As SimpleLEDArray.LED_Bri)
            Dim enumerator As IEnumerator = Nothing
            If (Me.led_Bri_0 <> value) Then
                Me.led_Bri_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLED).LED_Brightness = DirectCast(Me.led_Bri_0, SimpleLED.LED_Bri)
                    End While
                Finally
                    If (TypeOf enumerator Is IDisposable) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("Color of all LEDs.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_Color As SimpleLEDArray.LED_Col
        Get
            Return Me.led_Col_0
        End Get
        Set(ByVal value As SimpleLEDArray.LED_Col)
            Dim enumerator As IEnumerator = Nothing
            If (Me.led_Col_0 <> value) Then
                Me.led_Col_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLED).LED_Color = DirectCast(Me.led_Col_0, SimpleLED.LED_Col)
                    End While
                Finally
                    If (TypeOf enumerator Is IDisposable) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(2)>
    <Description("Number of LEDs in array (valid values 2 to 30).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_NumberOfLEDs As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (value < 2) Then
                value = 2
            End If
            If (value > 30) Then
                value = 30
            End If
            If (Me.int_1 <> value) Then
                Me.int_0 = Me.int_1
                Me.int_1 = value
                If (Me.int_1 <= Me.int_0) Then
                    Dim int0 As Integer = Me.int_0 - Me.int_1
                    For i As Integer = 1 To int0 Step 1
                        Me.method_1()
                        Me.SimpleLEDArray_Resize(Me, Nothing)
                    Next

                Else
                    Dim int1 As Integer = Me.int_1 - Me.int_0
                    For j As Integer = 1 To int1 Step 1
                        Me.method_0()
                        Me.SimpleLEDArray_Resize(Me, Nothing)
                    Next

                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <Description("Single LED height (the same as LED width).")>
    <RefreshProperties(RefreshProperties.All)>
    Public ReadOnly Property LED_SingleLEDHeight As Integer
        Get
            Return Me.int_4
        End Get
    End Property

    <Browsable(True)>
    <DefaultValue(30)>
    <Description("Single LED width (valid values 30 to 360).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_SingleLEDWidth As Integer
        Get
            Return Me.int_4
        End Get
        Set(ByVal value As Integer)
            If (value < 30) Then
                value = 30
            End If
            If (value > 360) Then
                value = 360
            End If
            If (Me.int_4 <> value) Then
                Me.int_4 = value
                Me.method_2()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("Set LEDs to OFF/ON/BLINK state (valid values 0, 1 and 2 respectively).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            Dim enumerator As IEnumerator = Nothing
            If (value < 0) Then
                value = 0
            End If
            If (value > 2) Then
                value = 2
            End If
            If (Me.int_2 <> value) Then
                Me.int_2 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        Dim current As SimpleLED = DirectCast(enumerator.Current, SimpleLED)
                        current.LED_BlinkInterval = Me.int_3
                        If (If(Me.int_2 = 0, False, Me.int_2 <> 1)) Then
                            Me.bool_1 = False
                            Me.BlinkTimer.Enabled = True
                        Else
                            current.LED_Blink = False
                            current.Value = Me.int_2 <> 0
                            Me.BlinkTimer.Enabled = False
                        End If
                    End While
                Finally
                    If (TypeOf enumerator Is IDisposable) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                MyBase.Invalidate()
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SimpleLEDArray_Resize)
        Me.color_0 = Color.MediumSeaGreen
        Me.led_Col_0 = SimpleLEDArray.LED_Col.Red
        Me.led_Bri_0 = SimpleLEDArray.LED_Bri.Normal
        Me.int_1 = 2
        Me.led_Dir_0 = SimpleLEDArray.LED_Dir.Horizontal
        Me.int_3 = 500
        Me.int_4 = 30
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.Size = New System.Drawing.Size(60, 30)
        Me.BackColor = Color.Transparent
        Me.BlinkTimer = New System.Windows.Forms.Timer() With
        {
            .Interval = Me.int_3,
            .Enabled = False
        }
        Dim int1 As Integer = Me.int_1
        For i As Integer = 1 To int1 Step 1
            Me.method_0()
        Next

    End Sub

    Private Function method_0() As SimpleLED
        Dim simpleLED As SimpleLED = New SimpleLED()
        MyBase.Controls.Add(simpleLED)
        simpleLED.Size = New System.Drawing.Size(Me.int_4, Me.int_4)
        If (Me.led_Dir_0 <> SimpleLEDArray.LED_Dir.Horizontal) Then
            simpleLED.Top = (MyBase.Controls.Count - 1) * Me.int_4
            simpleLED.Left = 0
        Else
            simpleLED.Top = 0
            simpleLED.Left = (MyBase.Controls.Count - 1) * Me.int_4
        End If
        If (Me.int_2 <> 2) Then
            simpleLED.Value = Me.int_2 <> 0
        End If
        simpleLED.Text = String.Empty
        simpleLED.LED_Color = DirectCast(Me.led_Col_0, SimpleLED.LED_Col)
        simpleLED.LED_Brightness = DirectCast(Me.led_Bri_0, SimpleLED.LED_Bri)
        simpleLED.LED_Border = Me.bool_0
        simpleLED.LED_BorderColor = Me.color_0
        simpleLED.Value = Me.int_2 <> 0
        simpleLED.LED_Blink = False
        Return simpleLED
    End Function

    Private Sub method_1()
        If (MyBase.Controls.Count > 0) Then
            MyBase.Controls.Remove(DirectCast(MyBase.Controls(MyBase.Controls.Count - 1), SimpleLED))
        End If
    End Sub

    Private Sub method_2()
        Dim int1 As Integer = Me.int_1
        Dim num As Integer = 1
        Do
            Me.method_1()
            num = num + 1
        Loop While num <= int1
        Dim int11 As Integer = Me.int_1
        For i As Integer = 1 To int11 Step 1
            Me.method_0()
            Me.SimpleLEDArray_Resize(Me, Nothing)
        Next

    End Sub

    Private Sub method_3(ByVal sender As Object, ByVal e As EventArgs)
        Dim enumerator As IEnumerator = Nothing
        Try
            enumerator = MyBase.Controls.GetEnumerator()
            While enumerator.MoveNext()
                Dim current As SimpleLED = DirectCast(enumerator.Current, SimpleLED)
                current.LED_Blink = False
                If (Me.bool_1) Then
                    current.Value = False
                Else
                    current.Value = True
                End If
            End While
        Finally
            If (TypeOf enumerator Is IDisposable) Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Me.bool_1 = Not Me.bool_1
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub

    Private Sub SimpleLEDArray_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.led_Dir_0 <> SimpleLEDArray.LED_Dir.Horizontal) Then
            MyBase.Width = Me.int_4
            MyBase.Height = Me.int_1 * Me.int_4
        Else
            MyBase.Width = Me.int_1 * Me.int_4
            MyBase.Height = Me.int_4
        End If
    End Sub

    Public Event ValueChanged As EventHandler


    Public Enum LED_Bri
        Normal
        Brighter
    End Enum

    Public Enum LED_Col
        Red
        Green
        Lime
        Blue
        Cyan
        Orange
        Yellow
        Violet
        White
    End Enum

    Public Enum LED_Dir
        Horizontal
        Vertical
    End Enum
End Class
