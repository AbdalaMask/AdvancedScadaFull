Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Globalization
Imports System.Windows.Forms

Public Class MeterCompact
    Inherits Control
    Protected StaticImage As Bitmap

    Private rectangle_0 As Rectangle

    Private pointF_0 As PointF()

    Private linearGradientBrush_0 As LinearGradientBrush

    Private solidBrush_0 As SolidBrush

    Private float_0 As Single

    Private matrix_0 As Matrix

    Private pointF_1 As PointF

    Private double_0 As Double

    Private double_1 As Double

    Private double_2 As Double

    Private string_0 As String

    Private int_0 As Integer

    Private int_1 As Integer

    Protected Friend m_BorderColor As Color

    Private color_0 As Color

    Private bool_0 As Boolean

    Private int_2 As Integer

    Private int_3 As Integer

    Private color_1 As Color

    Private int_4 As Integer

    Private color_2 As Color

    Private color_3 As Color

    Private double_3 As Double

    Private double_4 As Double

    Private color_4 As Color

    Private double_5 As Double

    Private double_6 As Double

    Private double_7 As Double

    Private double_8 As Double

    Private color_5 As Color

    Public Property Band1Color As Color
        Get
            Return Me.color_3
        End Get
        Set(ByVal value As Color)
            If (Me.color_3 <> value) Then
                Me.color_3 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band1EndValue As Double
        Get
            Return Me.double_4
        End Get
        Set(ByVal value As Double)
            If (Me.double_4 <> value) Then
                Me.double_4 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band1StartValue As Double
        Get
            Return Me.double_3
        End Get
        Set(ByVal value As Double)
            If (Me.double_3 <> value) Then
                Me.double_3 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band2Color As Color
        Get
            Return Me.color_4
        End Get
        Set(ByVal value As Color)
            If (Me.color_4 <> value) Then
                Me.color_4 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band2EndValue As Double
        Get
            Return Me.double_6
        End Get
        Set(ByVal value As Double)
            If (Me.double_6 <> value) Then
                Me.double_6 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band2StartValue As Double
        Get
            Return Me.double_5
        End Get
        Set(ByVal value As Double)
            If (Me.double_5 <> value) Then
                Me.double_5 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band3Color As Color
        Get
            Return Me.color_5
        End Get
        Set(ByVal value As Color)
            If (Me.color_5 <> value) Then
                Me.color_5 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band3EndValue As Double
        Get
            Return Me.double_8
        End Get
        Set(ByVal value As Double)
            If (Me.double_8 <> value) Then
                Me.double_8 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Band3StartValue As Double
        Get
            Return Me.double_7
        End Get
        Set(ByVal value As Double)
            If (Me.double_7 <> value) Then
                Me.double_7 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BorderColor As Color
        Get
            Return Me.m_BorderColor
        End Get
        Set(ByVal value As Color)
            If (Me.m_BorderColor <> value) Then
                Me.m_BorderColor = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property BorderWidth As Integer
        Get
            Return Me.int_1
        End Get
        Set(ByVal value As Integer)
            If (Me.int_1 <> value) Then
                Me.int_1 = Math.Min(25, Math.Max(0, value))
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim exStyle As System.Windows.Forms.CreateParams = MyBase.CreateParams
            exStyle.ExStyle = exStyle.ExStyle Or 32
            Return exStyle
        End Get
    End Property

    Public Property MajorTickColor As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            If (Me.color_1 <> value) Then
                Me.color_1 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property MajorTickDivisions As Integer
        Get
            Return Me.int_2
        End Get
        Set(ByVal value As Integer)
            If (Me.int_2 <> value) Then
                Me.int_2 = Math.Max(value, 1)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property MajorTickLength As Integer
        Get
            Return Me.int_3
        End Get
        Set(ByVal value As Integer)
            If (Me.int_3 <> value) Then
                Me.int_3 = Math.Max(2, value)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Maximum As Double
        Get
            Return Me.double_2
        End Get
        Set(ByVal value As Double)
            If (Me.double_2 <> value) Then
                Me.double_2 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property Minimum As Double
        Get
            Return Me.double_1
        End Get
        Set(ByVal value As Double)
            If (Me.double_1 <> value) Then
                Me.double_1 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property MinorTickColor As Color
        Get
            Return Me.color_2
        End Get
        Set(ByVal value As Color)
            If (Me.color_2 <> value) Then
                Me.color_2 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property MinorTickDivisions As Integer
        Get
            Return Me.int_4
        End Get
        Set(ByVal value As Integer)
            If (Me.int_4 <> value) Then
                Me.int_4 = Math.Max(value, 1)
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <DefaultValue(GetType(Color), "Red")>
    <Description("The arrow color.")>
    Public Property NeedleColor As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            If (Me.color_0 <> value) Then
                Me.color_0 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <DefaultValue(9)>
    <Description("The arrow width.")>
    <RefreshProperties(RefreshProperties.All)>
    Public Property NeedleWidth As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            If (Me.int_0 <> value) Then
                Me.int_0 = Math.Max(3, value)
                Me.int_0 = CInt(Math.Round(Math.Floor(CDbl(Me.int_0) / 2) * 2)) + 1
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property NumericFormat As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(value, Me.string_0, False) <> 0) Then
                Me.string_0 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    Public Property ShowLabels As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_0 <> value) Then
                Me.bool_0 = value
                Me.CreateStaticImage()
            End If
        End Set
    End Property

    '' <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property Value As Double
        Get
            Return Me.double_0
        End Get
        Set(ByVal value As Double)
            If (Me.double_0 <> value) Then
                Me.double_0 = value
                Me.OnValueChanged(EventArgs.Empty)
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        ReDim Me.pointF_0(6)
        Me.matrix_0 = New Matrix()
        Me.double_2 = 100
        Me.int_0 = 9
        Me.m_BorderColor = Color.DimGray
        Me.color_0 = Color.Maroon
        Me.bool_0 = True
        Me.int_2 = 10
        Me.int_3 = 15
        Me.color_1 = Color.DimGray
        Me.int_4 = 5
        Me.color_2 = Color.DimGray
        Me.color_3 = Color.Red
        Me.double_3 = 80
        Me.double_4 = 100
        Me.color_4 = Color.Yellow
        Me.double_5 = 70
        Me.double_6 = 80
        Me.double_7 = 60
        Me.double_8 = 70
        Me.color_5 = Color.Green

        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.BackColor = Color.Transparent
        Me.ForeColor = Color.LightGray
        Me.Font = New System.Drawing.Font("Arial", 12!, FontStyle.Bold)

    End Sub

    Protected Function ConvertValueToAngle(ByVal value As Double, ByVal angleRange As Single) As Single
        Dim [single] As Single = 0!
        If (Me.double_1 <> Me.double_2) Then
            [single] = CSng(((value - Me.double_1) / (Me.double_2 - Me.double_1) * CDbl(angleRange)))
        End If
        [single] = Math.Min([single], angleRange)
        [single] = Math.Max([single], 0!)
        Return [single]
    End Function

    Protected Overridable Sub CreateNeedle(ByVal needleLength As Single)
        Me.rectangle_0 = New Rectangle(New Point(CInt(Math.Round(CDbl((CSng(MyBase.Width) / 2! - CSng(Me.int_0) * 0.5! - 2!)))), CInt(Math.Round(CDbl((CSng(MyBase.Height) - Me.float_0 - CSng(Me.int_0) * 0.5! - 2!))))), New System.Drawing.Size(CInt(Math.Round(CDbl(Me.int_0) * 1)) + 4, CInt(Math.Round(CDbl(Me.int_0) * 1)) + 4))
        Me.pointF_0(0) = New PointF(CSng(MyBase.Width) / 2!, CSng(MyBase.Height) - Me.float_0 - CSng(Me.int_0) * 0.5!)
        Me.pointF_0(1) = New PointF(CSng(MyBase.Width) / 2! + CSng(Me.int_0), CSng(MyBase.Height) - Me.float_0 - CSng(Me.int_0) * 0.5!)
        Me.pointF_0(2) = New PointF(Me.pointF_0(1).X + 1!, CSng(MyBase.Height) - Me.float_0 - CSng(Me.int_0) / 2!)
        Me.pointF_0(3) = New PointF(CSng(MyBase.Width) / 2! + needleLength, CSng(MyBase.Height) - Me.float_0)
        Me.pointF_0(4) = New PointF(Me.pointF_0(2).X, CSng(MyBase.Height) - Me.float_0 + CSng(Me.int_0) / 2!)
        Me.pointF_0(5) = New PointF(Me.pointF_0(1).X, CSng(MyBase.Height) - Me.float_0 + CSng(Me.int_0) * 0.5!)
        Me.pointF_0(6) = New PointF(CSng(MyBase.Width) / 2!, CSng(MyBase.Height) - Me.float_0 + CSng(Me.int_0) * 0.5!)
        Dim width As Single = CSng(MyBase.Width) / 2!
        Dim clientRectangle As Rectangle = MyBase.ClientRectangle
        Me.pointF_1 = New PointF(width, CSng((CSng(clientRectangle.Height) - Me.float_0)))
        Me.method_0(CInt(Math.Round(CDbl((CSng(MyBase.Height) - Me.float_0)) - CDbl(Me.int_0) / 2)))
    End Sub

    Protected Overridable Sub CreateStaticImage()
        Dim sizeF As System.Drawing.SizeF
        Dim num As Integer = 0
        Dim height As Single = 0!
        Dim int2 As Double = 0
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            If (Me.StaticImage IsNot Nothing) Then
                Me.StaticImage.Dispose()
            End If
            Me.StaticImage = New Bitmap(MyBase.Width, MyBase.Height)
            Using graphic As Graphics = Graphics.FromImage(Me.StaticImage)
                Using stringFormat As System.Drawing.StringFormat = New System.Drawing.StringFormat()
                    graphic.SmoothingMode = SmoothingMode.AntiAlias
                    Me.float_0 = CSng(Math.Ceiling(CDbl((CSng(Me.int_0) / 2! + CSng((Me.int_1 * 2)) + 2!))))
                    If (Me.bool_0) Then
                        Dim str As String = Conversions.ToString(Me.Minimum)
                        Dim str1 As String = Conversions.ToString(Me.Maximum)
                        If (Not String.IsNullOrEmpty(Me.NumericFormat)) Then
                            Try
                                Dim minimum As Double = Me.Minimum
                                str = minimum.ToString(Me.string_0, CultureInfo.CurrentCulture)
                                minimum = Me.Maximum
                                str1 = minimum.ToString(Me.string_0, CultureInfo.CurrentCulture)
                            Catch exception As System.Exception
                                ProjectData.SetProjectError(exception)
                                str = "Inv. NumFormat"
                                str1 = "Inv. NumFormat"
                                ProjectData.ClearProjectError()
                            End Try
                        End If
                        sizeF = graphic.MeasureString(str, Me.Font)
                        Dim width As Single = sizeF.Width
                        sizeF = graphic.MeasureString(str1, Me.Font)
                        num = CInt(Math.Ceiling(CDbl(Math.Max(width, sizeF.Width))))
                        Dim [single] As Single = CSng(num)
                        sizeF = graphic.MeasureString(str, Me.Font)
                        num = CInt(Math.Ceiling(CDbl(Math.Max([single], sizeF.Height))))
                        Dim float0 As Single = Me.float_0
                        sizeF = graphic.MeasureString(str, Me.Font)
                        Me.float_0 = Math.Max(float0, sizeF.Height / 2! - 1! + CSng((Me.int_1 * 2)))
                        sizeF = graphic.MeasureString(str, Me.Font)
                        height = sizeF.Height
                    End If
                    Dim num1 As Integer = CInt(Math.Round(CDbl((CSng((MyBase.Height * 2)) - Me.float_0 * 2! - CSng((Me.int_1 * 4))))))
                    If (Not String.IsNullOrEmpty(Me.Text)) Then
                        sizeF = graphic.MeasureString(Me.Text, Me.Font)
                        Dim num2 As Integer = CInt(Math.Ceiling(CDbl(sizeF.Height)))
                        num1 = num1 - Convert.ToInt32(Math.Ceiling(New Decimal(num2))) * 2
                    End If
                    Dim num3 As Integer = Math.Min(MyBase.Width - Me.int_1 * 4, num1)
                    Dim single1 As Single = CSng(num3) / 2! - CSng(num)
                    If (Not (Me.int_1 = 0 And String.IsNullOrEmpty(Me.Text))) Then
                        MyBase.Region = Nothing
                    Else
                        Using graphicsPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
                            graphicsPath.AddArc(CInt(Math.Ceiling(CDbl(MyBase.Width) / 2 - CDbl(num3) / 2)), CInt(Math.Ceiling(CDbl(MyBase.Height) - CDbl(num3) / 2 - CDbl(Me.float_0))), num3, num3, 0!, -180!)
                            Dim lastPoint As System.Drawing.PointF = graphicsPath.GetLastPoint()
                            Dim pointF As System.Drawing.PointF = New System.Drawing.PointF(lastPoint.X, CSng(MyBase.Height))
                            graphicsPath.AddLine(lastPoint, pointF)
                            graphicsPath.AddLine(pointF, New System.Drawing.PointF(pointF.X + CSng(num3), CSng(MyBase.Height)))
                            lastPoint = graphicsPath.GetLastPoint()
                            graphicsPath.CloseFigure()
                            MyBase.Region = New System.Drawing.Region(graphicsPath)
                        End Using
                    End If
                    Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_1, 2!)
                        Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(Me.color_2, 1!)
                            Dim int3 As Single = CSng(Me.int_3) * 0.7!
                            If (Me.int_2 > 0 And Me.int_4 > 0) Then
                                int2 = 3.14159265358979 / CDbl(Me.int_2) / CDbl(Me.int_4)
                            End If
                            Me.DrawColorBands(graphic, single1 - CSng(Me.int_3) + int3 / 2!, int3, 180!, 180!, CInt(Math.Round(CDbl(Me.float_0))))
                            Dim int21 As Integer = Me.int_2
                            Dim num4 As Integer = 0
                            Do
                                Dim majorTickLength As Single = CSng((CDbl((single1 - CSng(Me.MajorTickLength))) * Math.Cos(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                Dim majorTickLength1 As Single = CSng((CDbl((single1 - CSng(Me.MajorTickLength))) * Math.Sin(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                Dim int31 As Single = CSng((CDbl(single1) * Math.Cos(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                Dim int32 As Single = CSng((CDbl(single1) * Math.Sin(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                graphic.DrawLine(pen, CSng(CInt(Math.Round(CDbl(majorTickLength) + CDbl(MyBase.Width) / 2))), CSng(MyBase.Height) - majorTickLength1 - Me.float_0, CSng(CInt(Math.Round(CDbl(int31) + CDbl(MyBase.Width) / 2))), CSng(MyBase.Height) - int32 - Me.float_0)
                                If (num4 < Me.int_2) Then
                                    Dim int4 As Integer = Me.int_4 - 1
                                    For i As Integer = 1 To int4 Step 1
                                        majorTickLength = CSng((CDbl((single1 - CSng(Me.int_3))) * Math.Cos(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2) + CDbl(i) * int2)))
                                        majorTickLength1 = CSng((CDbl((single1 - CSng(Me.int_3))) * Math.Sin(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2) + CDbl(i) * int2)))
                                        int31 = CSng((CDbl((single1 - CSng(Me.int_3) + int3)) * Math.Cos(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2) + CDbl(i) * int2)))
                                        int32 = CSng((CDbl((single1 - CSng(Me.int_3) + int3)) * Math.Sin(CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2) + CDbl(i) * int2)))
                                        graphic.DrawLine(pen1, CSng(CInt(Math.Round(CDbl(majorTickLength) + CDbl(MyBase.Width) / 2))), CSng(MyBase.Height) - majorTickLength1 - Me.float_0, CSng(CInt(Math.Round(CDbl(int31) + CDbl(MyBase.Width) / 2))), CSng(MyBase.Height) - int32 - Me.float_0)
                                    Next

                                End If
                                If (Me.bool_0) Then
                                    graphic.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                                    Dim maximum As Double = (Me.Maximum - Me.Minimum) / CDbl(Me.int_2) * CDbl(num4) + Me.Minimum
                                    Dim str2 As String = Conversions.ToString(CInt(Math.Round(maximum)))
                                    If (Not String.IsNullOrEmpty(Me.string_0)) Then
                                        str2 = maximum.ToString(Me.string_0, CultureInfo.CurrentCulture)
                                    End If
                                    sizeF = graphic.MeasureString(str2, Me.Font)
                                    Dim num5 As Integer = CInt(Math.Round(CDbl((sizeF.Width + 1!))))
                                    stringFormat.LineAlignment = StringAlignment.Center
                                    stringFormat.Alignment = StringAlignment.Center
                                    majorTickLength = CSng(((CDbl(single1) + CDbl(num3) / 2) / 2 * Math.Cos(3.14159265358979 - CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                    majorTickLength1 = CSng(((CDbl(single1) + CDbl(num3) / 2) / 2 * Math.Sin(3.14159265358979 - CDbl(num4) * 3.14159265358979 / CDbl(Me.int_2))))
                                    Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(CInt(Math.Round(CDbl((majorTickLength + CSng(MyBase.Width) / 2!)) - CDbl(num5) / 2)) - 3, CInt(Math.Round(CDbl((CSng(MyBase.Height) - majorTickLength1 - height / 2! - 2! - Me.float_0)))) - 2, num5 + 6, CInt(Math.Round(CDbl(height))) + 6)
                                    Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                                        graphic.DrawString(str2, Me.Font, solidBrush, rectangle, stringFormat)
                                    End Using
                                End If
                                num4 = num4 + 1
                            Loop While num4 <= int21
                            Dim majorTickLength2 As Single = single1 - CSng(Me.MajorTickLength) + int3 / 2!
                            Me.CreateNeedle(majorTickLength2)
                        End Using
                    End Using
                    If (Not String.IsNullOrEmpty(Me.Text)) Then
                        Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Me.int_1 * 2, Me.int_1 * 2 + 1, MyBase.Width - Me.int_1 * 4, CInt(Math.Round(CDbl((CSng(MyBase.Height) - single1 - CSng((Me.int_1 * 4)) - Me.float_0 - height - 8!)))))
                        Using stringFormat1 As System.Drawing.StringFormat = New System.Drawing.StringFormat()
                            Using solidBrush1 As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(Me.ForeColor)
                                stringFormat.LineAlignment = StringAlignment.Far
                                If (Me.int_1 > 0) Then
                                    stringFormat1.LineAlignment = StringAlignment.Center
                                End If
                                stringFormat1.Alignment = StringAlignment.Center
                                graphic.DrawString(Me.Text, Me.Font, solidBrush1, rectangle1, stringFormat1)
                            End Using
                        End Using
                        If (Me.int_1 > 0) Then
                            Using pen2 As System.Drawing.Pen = New System.Drawing.Pen(Me.m_BorderColor, 2!)
                                graphic.DrawLine(pen2, 0, rectangle1.Height + 3 + Me.int_1 * 2, MyBase.Width, rectangle1.Height + 2 + Me.int_1 * 2)
                            End Using
                        End If
                    End If
                    If (Me.int_1 > 0) Then
                        BeveledButtonDisplay.Draw3DBorder(Me, graphic, Me.m_BorderColor, Me.int_1)
                    End If
                End Using
            End Using
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Me.StaticImage IsNot Nothing) Then
                    Me.StaticImage.Dispose()
                End If
                If (Me.solidBrush_0 IsNot Nothing) Then
                    Me.solidBrush_0.Dispose()
                End If
                If (Me.linearGradientBrush_0 IsNot Nothing) Then
                    Me.linearGradientBrush_0.Dispose()
                End If
                If (Me.matrix_0 IsNot Nothing) Then
                    Me.matrix_0.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Protected Sub DrawColorBands(ByVal graphics As System.Drawing.Graphics, ByVal tickArcRadius As Single, ByVal minorTickLength As Single, ByVal zeroAngle As Single, ByVal spanAngle As Single, ByVal int_5 As Integer)
        Dim num As Integer = CInt(Math.Round(CDbl(MyBase.Width) / 2 - CDbl(tickArcRadius)))
        Dim num1 As Integer = CInt(Math.Round(CDbl((CSng(MyBase.Height) - tickArcRadius - CSng(int_5)))))
        Dim [single] As Single = tickArcRadius * 2!
        If (Me.double_3 <> Me.double_4) Then
            Dim angle As Single = Me.ConvertValueToAngle(Me.double_3, spanAngle)
            Dim angle1 As Single = Me.ConvertValueToAngle(Me.double_4, spanAngle)
            Try
                Using pen As System.Drawing.Pen = New System.Drawing.Pen(Me.color_3, CSng(CInt(Math.Round(CDbl(minorTickLength)))))
                    graphics.DrawArc(pen, CSng(num), CSng(num1), [single], [single], angle - zeroAngle, angle1 - angle)
                End Using
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                ProjectData.ClearProjectError()
            End Try
        End If
        If (Me.double_5 <> Me.double_6) Then
            Dim single1 As Single = Me.ConvertValueToAngle(Me.double_5, spanAngle)
            Dim angle2 As Single = Me.ConvertValueToAngle(Me.double_6, spanAngle)
            Try
                Using pen1 As System.Drawing.Pen = New System.Drawing.Pen(Me.color_4, CSng(CInt(Math.Round(CDbl(minorTickLength)))))
                    graphics.DrawArc(pen1, CSng(num), CSng(num1), [single], [single], single1 - zeroAngle, angle2 - single1)
                End Using
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError()
            End Try
        End If
        If (Me.double_7 <> Me.double_8) Then
            Dim single2 As Single = Me.ConvertValueToAngle(Me.double_7, spanAngle)
            Dim angle3 As Single = Me.ConvertValueToAngle(Me.double_8, spanAngle)
            Try
                Using pen2 As System.Drawing.Pen = New System.Drawing.Pen(Me.color_5, CSng(CInt(Math.Round(CDbl(minorTickLength)))))
                    graphics.DrawArc(pen2, CSng(num), CSng(num1), [single], [single], single2 - zeroAngle, angle3 - single2)
                End Using
            Catch exception2 As System.Exception
                ProjectData.SetProjectError(exception2)
                ProjectData.ClearProjectError()
            End Try
        End If
    End Sub

    Protected Sub DrawNeedle(ByVal graphics As System.Drawing.Graphics, ByVal angle As Single, ByVal int_5 As Integer, ByVal int_6 As Integer)
        Me.matrix_0.Reset()
        Me.matrix_0.Translate(CSng(int_5), CSng(int_6))
        Me.matrix_0.RotateAt(angle, Me.pointF_1)
        graphics.Transform = Me.matrix_0
        graphics.SmoothingMode = SmoothingMode.AntiAlias
        graphics.FillPolygon(Me.linearGradientBrush_0, Me.pointF_0)
        graphics.FillEllipse(Me.solidBrush_0, Me.rectangle_0)
    End Sub

    Private Sub method_0(ByVal int_5 As Integer)
        If (Me.linearGradientBrush_0 IsNot Nothing) Then
            Me.linearGradientBrush_0.Dispose()
        End If
        Me.linearGradientBrush_0 = New LinearGradientBrush(New Point(0, int_5 - 1), New Point(0, Convert.ToInt32(Decimal.Add(New Decimal(int_5), Math.Ceiling(New Decimal(Me.int_0)))) + 2), Me.color_0, ControlPaint.LightLight(Me.color_0))
        Dim singleArray() As Single = {0!, 0.35!, 0.5!, 0.65!, 1!}
        Dim color0() As Color = {Me.color_0, Me.color_0, ControlPaint.Light(Me.color_0, 1!), Me.color_0, Me.color_0}
        Dim colorBlend As System.Drawing.Drawing2D.ColorBlend = New System.Drawing.Drawing2D.ColorBlend() With
        {
            .Positions = singleArray,
            .Colors = color0
        }
        Me.linearGradientBrush_0.InterpolationColors = colorBlend
        Me.solidBrush_0 = New SolidBrush(ControlPaint.Dark(Me.color_0, 0.25!))
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        MyBase.OnPaint(painte)
        If (Me.StaticImage IsNot Nothing) Then
            painte.Graphics.DrawImage(Me.StaticImage, 0, 0)
        End If
        Try
            Me.PaintActiveContent(painte.Graphics)
        Catch exception1 As System.Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As System.Exception = exception1
            painte.Graphics.DrawString(exception.Message, Me.Font, New SolidBrush(Me.ForeColor), 0!, 0!)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.CreateStaticImage()
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Protected Overridable Sub PaintActiveContent(ByVal graphics As System.Drawing.Graphics)
        Dim angle As Single = Me.ConvertValueToAngle(Me.double_0, 180!)
        Me.DrawNeedle(graphics, -180! + angle, 0, 0)
    End Sub

    Public Event ValueChanged As EventHandler

End Class
