Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class SimpleLedAnalogArray
    Inherits Control
    Private bool_0 As Boolean

    Private color_0 As Color

    Private led_Col_0 As SimpleLEDAnalog.LED_Col

    Private int_0 As Integer

    Private int_1 As Integer

    Private led_Dir_0 As SimpleLedAnalogArray.LED_Dir

    Private float_0 As Single

    Private bool_1 As Boolean

    Private float_1 As Single

    Private float_2 As Single

    Private int_2 As Integer

    <Browsable(True)>
    <DefaultValue(0)>
    <Description("Set LEDs array orientation (Horizontal or Vertical).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ArrayOrientation As SimpleLedAnalogArray.LED_Dir
        Get
            Return Me.led_Dir_0
        End Get
        Set(ByVal value As SimpleLedAnalogArray.LED_Dir)
            If (Me.led_Dir_0 <> value) Then
                Me.led_Dir_0 = value
                Me.method_2()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Enable tiny border on each LED.")>
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
                        DirectCast(enumerator.Current, SimpleLEDAnalog).LED_Border = Me.bool_0
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
                        DirectCast(enumerator.Current, SimpleLEDAnalog).LED_BorderColor = Me.color_0
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
    Public Property LED_Color As SimpleLEDAnalog.LED_Col
        Get
            Return Me.led_Col_0
        End Get
        Set(ByVal value As SimpleLEDAnalog.LED_Col)
            Dim enumerator As IEnumerator = Nothing
            If (Me.led_Col_0 <> value) Then
                Me.led_Col_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLEDAnalog).LED_Color = Me.led_Col_0
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
                        Me.SimpleLedAnalogArray_Resize(Me, Nothing)
                    Next

                Else
                    Dim int1 As Integer = Me.int_1 - Me.int_0
                    For j As Integer = 1 To int1 Step 1
                        Me.method_0()
                        Me.SimpleLedAnalogArray_Resize(Me, Nothing)
                    Next

                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(False)>
    <Description("Show the current analog value on the LED itself (also controlled by the mouse DoubleClick event).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_ShowValue As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            Dim enumerator As IEnumerator = Nothing
            If (Me.bool_1 <> value) Then
                Me.bool_1 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLEDAnalog).LED_ShowValue = Me.bool_1
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
    <Description("Single LED height (the same as LED width).")>
    <RefreshProperties(RefreshProperties.All)>
    Public ReadOnly Property LED_SingleLEDHeight As Integer
        Get
            Return Me.int_2
        End Get
    End Property

    <Browsable(True)>
    <DefaultValue(30)>
    <Description("Single LED width (valid values 30 to 360).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property LED_SingleLEDWidth As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            If (value < 30) Then
                value = 30
            End If
            If (value > 360) Then
                value = 360
            End If
            If (Me.int_2 <> value) Then
                Me.int_2 = value
                Me.method_2()
                MyBase.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <DefaultValue(255!)>
    <Description("Maximum LED analog value (needs to be at least 5 higher than the Minimum).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Maximum As Single
        Get
            Return Me.float_1
        End Get
        Set(ByVal value As Single)
            Dim enumerator As IEnumerator = Nothing
            If (Me.float_1 <> value) Then
                Me.float_1 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLEDAnalog).Maximum = Me.float_1
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
    <DefaultValue(0!)>
    <Description("Minimum LED analog value (needs to be at least 5 lower than the Maximum).")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Minimum As Single
        Get
            Return Me.float_2
        End Get
        Set(ByVal value As Single)
            Dim enumerator As IEnumerator = Nothing
            If (Me.float_2 <> value) Then
                Me.float_2 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLEDAnalog).Minimum = Me.float_2
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
    <DefaultValue(0!)>
    <Description("Set LED analog value.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property Value As Single
        Get
            Return Me.float_0
        End Get
        Set(ByVal value As Single)
            Dim enumerator As IEnumerator = Nothing
            If (Me.float_0 <> value) Then
                Me.float_0 = value
                Try
                    enumerator = MyBase.Controls.GetEnumerator()
                    While enumerator.MoveNext()
                        DirectCast(enumerator.Current, SimpleLEDAnalog).Value = Me.float_0
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

    Public Sub New()
        MyBase.New()
        AddHandler MyBase.Resize, New EventHandler(AddressOf Me.SimpleLedAnalogArray_Resize)
        Me.color_0 = Color.MediumSeaGreen
        Me.led_Col_0 = SimpleLEDAnalog.LED_Col.Red
        Me.int_1 = 2
        Me.led_Dir_0 = SimpleLedAnalogArray.LED_Dir.Horizontal
        Me.float_1 = 255!
        Me.int_2 = 30
        MyBase.SetStyle(ControlStyles.ContainerControl Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.DoubleBuffered = True
        Me.DoubleBuffered = True
        MyBase.Size = New System.Drawing.Size(60, 30)
        Me.BackColor = Color.Transparent
        Dim int1 As Integer = Me.int_1
        For i As Integer = 1 To int1 Step 1
            Me.method_0()
        Next

    End Sub

    Private Function method_0() As SimpleLEDAnalog
        Dim simpleLEDAnalog As SimpleLEDAnalog = New SimpleLEDAnalog()
        MyBase.Controls.Add(simpleLEDAnalog)
        simpleLEDAnalog.Size = New System.Drawing.Size(Me.int_2, Me.int_2)
        If (Me.led_Dir_0 <> SimpleLedAnalogArray.LED_Dir.Horizontal) Then
            simpleLEDAnalog.Top = (MyBase.Controls.Count - 1) * Me.int_2
            simpleLEDAnalog.Left = 0
        Else
            simpleLEDAnalog.Top = 0
            simpleLEDAnalog.Left = (MyBase.Controls.Count - 1) * Me.int_2
        End If
        simpleLEDAnalog.Value = Me.float_0
        simpleLEDAnalog.Text = String.Empty
        simpleLEDAnalog.LED_Color = Me.led_Col_0
        simpleLEDAnalog.LED_Border = Me.bool_0
        simpleLEDAnalog.LED_BorderColor = Me.color_0
        simpleLEDAnalog.LED_ShowValue = Me.bool_1
        simpleLEDAnalog.Minimum = Me.float_2
        simpleLEDAnalog.Maximum = Me.float_1
        Return simpleLEDAnalog
    End Function

    Private Sub method_1()
        If (MyBase.Controls.Count > 0) Then
            MyBase.Controls.Remove(DirectCast(MyBase.Controls(MyBase.Controls.Count - 1), SimpleLEDAnalog))
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
            Me.SimpleLedAnalogArray_Resize(Me, Nothing)
        Next

    End Sub

    Private Sub SimpleLedAnalogArray_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.led_Dir_0 <> SimpleLedAnalogArray.LED_Dir.Horizontal) Then
            MyBase.Width = Me.int_2
            MyBase.Height = Me.int_1 * Me.int_2
        Else
            MyBase.Width = Me.int_1 * Me.int_2
            MyBase.Height = Me.int_2
        End If
    End Sub

    Public Enum LED_Dir
        Horizontal
        Vertical
    End Enum
End Class
