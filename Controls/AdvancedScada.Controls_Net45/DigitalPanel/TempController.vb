Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<DefaultEvent("")>
Public Class TempController
    Inherits Control
    Private StaticImage As Bitmap

    Private LED As Bitmap()

    Private LEDGreen As Bitmap()

    Private DecimalImage As Bitmap

    Private Button1Image As Bitmap

    Private Button1UpImage As Bitmap

    Private Button1PressedImage As Bitmap

    Private Button2Image As Bitmap

    Private Button2UpImage As Bitmap

    Private Button2PressedImage As Bitmap

    Private Button3Image As Bitmap

    Private Button3UpImage As Bitmap

    Private Button3PressedImage As Bitmap

    Private Button4Image As Bitmap

    Private Button4UpImage As Bitmap

    Private Button4PressedImage As Bitmap

    Private ImageRatio As Single

    Private TextRectangle As Rectangle

    Private sf As StringFormat

    Private TextBrush As SolidBrush

    Private m_ValuePV As Single

    Private m_Value_ValueScaleFactor As Decimal

    Private m_ValueSP As Single

    Private m_Value_ValueScaleFactorSP As Decimal

    Private m_Value2Text As String

    Private _Button1Text As String

    Private _Button2Text As String

    Private m_NumberOfDigits As Integer

    Private _DecimalPosition As Integer

    Private SegWidth As Integer

    Private bitmap_16 As Bitmap

    Private LastWidth As Integer

    Private LastHeight As Integer

    Private StaticImageRatio As Single

    Public Property Button1Text As String
        Get
            Return Me._Button1Text
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(Me._Button1Text, value, False) <> 0) Then
                Me._Button1Text = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property Button2Text As String
        Get
            Return Me._Button2Text
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(Me._Button2Text, value, False) <> 0) Then
                Me._Button2Text = value
                MyBase.Invalidate()
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

    Public Property DecimalPosition As Integer
        Get
            Return Me._DecimalPosition
        End Get
        Set(ByVal value As Integer)
            Me._DecimalPosition = Math.Max(Math.Min(Me.m_NumberOfDigits - 1, value), 0)
            Me.RefreshImage()
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Font As System.Drawing.Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As System.Drawing.Font)
            MyBase.Font = value
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            If (Me.TextBrush IsNot Nothing) Then
                Me.TextBrush.Color = value
            Else
                Me.TextBrush = New SolidBrush(value)
            End If
            MyBase.ForeColor = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property Value_ValueScaleFactor As Decimal
        Get
            Return Me.m_Value_ValueScaleFactor
        End Get
        Set(ByVal value As Decimal)
            Me.m_Value_ValueScaleFactor = value
            MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.08)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.19)), CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.65)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.4))))
        End Set
    End Property

    Public Property Value_ValueScaleFactorSP As Decimal
        Get
            Return Me.m_Value_ValueScaleFactorSP
        End Get
        Set(ByVal value As Decimal)
            Me.m_Value_ValueScaleFactorSP = value
            MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.08)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.19)), CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.65)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.4))))
        End Set
    End Property

    Public Property Value2Text As String
        Get
            Return Me.m_Value2Text
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(Me.m_Value2Text, value, False) <> 0) Then
                Me.m_Value2Text = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property ValuePV As Single
        Get
            Return Me.m_ValuePV
        End Get
        Set(ByVal value As Single)
            If (value <> Me.m_ValuePV) Then
                Me.m_ValuePV = value
                MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.08)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.1)), CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.85)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.7))))
            End If
        End Set
    End Property

    Public Property ValueSP As Single
        Get
            Return Me.m_ValueSP
        End Get
        Set(ByVal value As Single)
            If (value <> Me.m_ValueSP) Then
                Me.m_ValueSP = value
                MyBase.Invalidate(New Rectangle(CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.08)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.1)), CInt(Math.Round(CDbl(Me.StaticImage.Width) * 0.85)), CInt(Math.Round(CDbl(Me.StaticImage.Height) * 0.8))))
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        ReDim Me.LED(11)
        ReDim Me.LEDGreen(11)
        Me.TextRectangle = New Rectangle()
        Me.sf = New StringFormat()
        Me.m_Value_ValueScaleFactor = Decimal.One
        Me.m_Value_ValueScaleFactorSP = Decimal.One
        Me.m_Value2Text = "SP"
        Me.m_NumberOfDigits = 5
        Me._DecimalPosition = 0
        Me.SegWidth = 98
        Me.StaticImageRatio = CSng((CDbl(My.Resources.TempController.Height) / CDbl(My.Resources.TempController.Width)))
        If ((MyBase.ForeColor = Color.FromKnownColor(KnownColor.ControlText)) Or (Me.ForeColor = Color.FromArgb(0, 0, 0, 0))) Then
            Me.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub RefreshImage()
        Dim width As Single = CSng((CDbl(MyBase.Width) / CDbl(My.Resources.TempController.Width)))
        Dim height As Single = CSng((CDbl(MyBase.Height) / CDbl(My.Resources.TempController.Height)))
        If (width >= height) Then
            Me.ImageRatio = height
        Else
            Me.ImageRatio = width
        End If
        If (Me.ImageRatio > 0!) Then
            If (Me.StaticImage IsNot Nothing) Then
                Me.StaticImage.Dispose()
            End If
            Me.StaticImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.TempController.Width) * Me.ImageRatio), Convert.ToInt32(CSng(My.Resources.TempController.Height) * Me.ImageRatio))
            Dim graphic As Graphics = Graphics.FromImage(Me.StaticImage)
            graphic.DrawImage(My.Resources.TempController, 0, 0, Me.StaticImage.Width, Me.StaticImage.Height)
            Me.Button1UpImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1.Height) * Me.ImageRatio)))))
            Me.Button2UpImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton2.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1.Height) * Me.ImageRatio)))))
            Me.Button3UpImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton3.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1.Height) * Me.ImageRatio)))))
            Me.Button4UpImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton4.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1.Height) * Me.ImageRatio)))))
            Me.Button1PressedImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1Pressed.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton1Pressed.Height) * Me.ImageRatio)))))
            Me.Button2PressedImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton2Pressed.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton2Pressed.Height) * Me.ImageRatio)))))
            Me.Button3PressedImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton3Pressed.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton3Pressed.Height) * Me.ImageRatio)))))
            Me.Button4PressedImage = New Bitmap(Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton4Pressed.Width) * Me.ImageRatio)))), Convert.ToInt32(Math.Ceiling(CDbl((CSng(My.Resources.TempControllerButton4Pressed.Height) * Me.ImageRatio)))))
            graphic = Graphics.FromImage(Me.Button1UpImage)
            graphic.DrawImage(My.Resources.TempControllerButton1, 0!, 0!, CSng(My.Resources.TempControllerButton1.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton1.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button2UpImage)
            graphic.DrawImage(My.Resources.TempControllerButton2, 0!, 0!, CSng(My.Resources.TempControllerButton2.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton2.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button3UpImage)
            graphic.DrawImage(My.Resources.TempControllerButton3, 0!, 0!, CSng(My.Resources.TempControllerButton3.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton3.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button4UpImage)
            graphic.DrawImage(My.Resources.TempControllerButton4, 0!, 0!, CSng(My.Resources.TempControllerButton4.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton4.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button1PressedImage)
            graphic.DrawImage(My.Resources.TempControllerButton1Pressed, 0!, 0!, CSng(My.Resources.TempControllerButton1Pressed.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton1Pressed.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button2PressedImage)
            graphic.DrawImage(My.Resources.TempControllerButton2Pressed, 0!, 0!, CSng(My.Resources.TempControllerButton2Pressed.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton2Pressed.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button3PressedImage)
            graphic.DrawImage(My.Resources.TempControllerButton3Pressed, 0!, 0!, CSng(My.Resources.TempControllerButton3Pressed.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton3Pressed.Height) * Me.ImageRatio)
            graphic = Graphics.FromImage(Me.Button4PressedImage)
            graphic.DrawImage(My.Resources.TempControllerButton4Pressed, 0!, 0!, CSng(My.Resources.TempControllerButton4Pressed.Width) * Me.ImageRatio, CSng(My.Resources.TempControllerButton4Pressed.Height) * Me.ImageRatio)
            Me.Button1Image = Me.Button1UpImage
            Me.Button2Image = Me.Button2UpImage
            Me.Button3Image = Me.Button3UpImage
            Me.Button4Image = Me.Button4UpImage
            Me.TextRectangle.X = 0
            Me.TextRectangle.Y = CInt(Math.Round(CDbl((30.0! * Me.ImageRatio))))
            Me.TextRectangle.Width = Me.StaticImage.Width - 1
            Me.TextRectangle.Height = CInt(Math.Round(CDbl((60.0! * Me.ImageRatio))))
            Me.sf.Alignment = StringAlignment.Center
            Me.sf.LineAlignment = StringAlignment.Center
            If (Me.TextBrush Is Nothing) Then
                Me.TextBrush = New SolidBrush(MyBase.ForeColor)
            End If
            Dim num As Integer = Convert.ToInt32(CSng(My.Resources.LED7Segment8Red.Width) * Me.ImageRatio)
            Dim num1 As Integer = Convert.ToInt32(CSng(My.Resources.LED7Segment8Red.Height) * Me.ImageRatio)
            Dim num2 As Integer = 0
            Do
                If (Me.LED(num2) IsNot Nothing) Then
                    Me.LED(num2).Dispose()
                End If
                Me.LED(num2) = New Bitmap(num, num1)
                graphic = Graphics.FromImage(Me.LED(num2))
                graphic.ScaleTransform(CSng((CDbl(Me.ImageRatio) * 0.95)), CSng((CDbl(Me.ImageRatio) * 0.95)))
                If (Me.LEDGreen(num2) IsNot Nothing) Then
                    Me.LEDGreen(num2).Dispose()
                End If
                Me.LEDGreen(num2) = New Bitmap(num, num1)
                Dim graphic1 As Graphics = Graphics.FromImage(Me.LEDGreen(num2))
                graphic1.ScaleTransform(CSng((CDbl(Me.ImageRatio) * 2.5)), CSng((CDbl(Me.ImageRatio) * 2.5)))
                SevenSegmentGDI.DrawDigit(graphic1, num2, Color.Green, True, 0.04, 0.048)
                Select Case num2
                    Case 0
                        graphic.DrawImage(My.Resources.LED7Segment0Red, 0, 0)
                        Exit Select
                    Case 1
                        graphic.DrawImage(My.Resources.LED7Segment1Red, 0, 0)
                        Exit Select
                    Case 2
                        graphic.DrawImage(My.Resources.LED7Segment2Red, 0, 0)
                        Exit Select
                    Case 3
                        graphic.DrawImage(My.Resources.LED7Segment3Red, 0, 0)
                        Exit Select
                    Case 4
                        graphic.DrawImage(My.Resources.LED7Segment4Red, 0, 0)
                        Exit Select
                    Case 5
                        graphic.DrawImage(My.Resources.LED7Segment5Red, 0, 0)
                        Exit Select
                    Case 6
                        graphic.DrawImage(My.Resources.LED7Segment6Red, 0, 0)
                        Exit Select
                    Case 7
                        graphic.DrawImage(My.Resources.LED7Segment7Red, 0, 0)
                        Exit Select
                    Case 8
                        graphic.DrawImage(My.Resources.LED7Segment8Red, 0, 0)
                        Exit Select
                    Case 9
                        graphic.DrawImage(My.Resources.LED7Segment9Red, 0, 0)
                        Exit Select
                    Case 10
                        graphic.DrawImage(My.Resources.LED7SegmentOffRed, 0, 0)
                        Exit Select
                    Case 11
                        graphic.DrawImage(My.Resources.LED7Segment_Red, 0, 0)
                        Exit Select
                End Select
                graphic.Dispose()
                graphic1.Dispose()
                num2 = num2 + 1
            Loop While num2 <= 11
            Me.DecimalImage = New Bitmap(Convert.ToInt32(CSng(My.Resources.LED7SegmentDecimalRed.Width) * Me.ImageRatio), Convert.ToInt32(CSng(My.Resources.LED7SegmentDecimalRed.Height) * Me.ImageRatio))
            graphic = Graphics.FromImage(Me.DecimalImage)
            graphic.DrawImage(My.Resources.LED7SegmentDecimalRed, 0, 0)
            If (Me.bitmap_16 IsNot Nothing) Then
                Me.bitmap_16.Dispose()
            End If
            Me.bitmap_16 = New Bitmap(MyBase.Width, MyBase.Height)
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        Dim num As Integer
        Dim flag As Boolean = False
        If (Not (Me.StaticImage Is Nothing Or Me.bitmap_16 Is Nothing) And Me.TextBrush IsNot Nothing) Then
            Dim graphic As Graphics = Graphics.FromImage(Me.bitmap_16)
            graphic.DrawImage(Me.StaticImage, 0, 0)
            graphic.DrawImage(Me.Button1Image, Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.09), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7))
            graphic.DrawImage(Me.Button2Image, Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.31), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7))
            graphic.DrawImage(Me.Button3Image, Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.53), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7))
            graphic.DrawImage(Me.Button4Image, Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.75), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7))
            Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle(Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.09), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7), Me.Button1Image.Width, Me.Button1Image.Height)
            Me.sf.Alignment = StringAlignment.Center
            Me.sf.LineAlignment = StringAlignment.Center
            graphic.DrawString(Me._Button1Text, Me.Font, Brushes.Brown, rectangle, Me.sf)
            rectangle = New System.Drawing.Rectangle(Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.31), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.7), Me.Button2Image.Width, Me.Button2Image.Height)
            graphic.DrawString(Me._Button2Text, Me.Font, Brushes.Brown, rectangle, Me.sf)
            If (Not String.IsNullOrEmpty(Me.m_Value2Text)) Then
                rectangle = New System.Drawing.Rectangle(0, Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.45), Convert.ToInt32(CDbl(Me.StaticImage.Width) * 0.31), Convert.ToInt32(CDbl(Me.StaticImage.Height) * 0.5))
                Me.sf.LineAlignment = StringAlignment.Near
                Me.sf.Alignment = StringAlignment.Far
                graphic.DrawString(Me.m_Value2Text, Me.Font, Brushes.Brown, rectangle, Me.sf)
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                If (Me.TextBrush.Color <> MyBase.ForeColor) Then
                    Me.TextBrush.Color = MyBase.ForeColor
                End If
                Me.sf.Alignment = StringAlignment.Center
                Me.sf.LineAlignment = StringAlignment.Center
                graphic.DrawString(MyBase.Text, MyBase.Font, Me.TextBrush, Me.TextRectangle, Me.sf)
            End If
            Dim num1 As Integer = Convert.ToInt32(Me.m_ValuePV * Convert.ToSingle(Me.m_Value_ValueScaleFactor))
            If (Not (CDbl(num1) <= Math.Pow(10, CDbl(Me.m_NumberOfDigits)) - 1 And CDbl(num1) >= (Math.Pow(10, CDbl((Me.m_NumberOfDigits - 1))) - 1) * -1)) Then
                Dim int0 As Integer = Me.m_NumberOfDigits
                For i As Integer = 1 To int0 Step 1
                    graphic.DrawImage(Me.LED(11), CSng((90 + Me.SegWidth * (i - 1))) * Me.ImageRatio, 105.0! * Me.ImageRatio)
                Next

            Else
                Dim int01 As Integer = Me.m_NumberOfDigits
                For j As Integer = 1 To int01 Step 1
                    If (num1 >= 0) Then
                        num = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl((Me.m_NumberOfDigits - j)))))
                        If (num > 0 Or j = Me.m_NumberOfDigits Or j > Me.m_NumberOfDigits - Me._DecimalPosition) Then
                            flag = True
                        End If
                        If (Not flag) Then
                            graphic.DrawImage(Me.LED(10), CSng((90 + Me.SegWidth * (j - 1))) * Me.ImageRatio, 105.0! * Me.ImageRatio)
                        Else
                            graphic.DrawImage(Me.LED(num), CSng((90 + Me.SegWidth * (j - 1))) * Me.ImageRatio, 105.0! * Me.ImageRatio)
                        End If
                        num1 = CInt(Math.Round(CDbl(num1) - CDbl(num) * Math.Pow(10, CDbl((Me.m_NumberOfDigits - j)))))
                    Else
                        graphic.DrawImage(Me.LED(11), CSng((90 + Me.SegWidth * (j - 1))) * Me.ImageRatio, 105.0! * Me.ImageRatio)
                        num1 = Math.Abs(num1)
                    End If
                Next

            End If
            If (Me._DecimalPosition > 0) Then
                graphic.DrawImage(Me.DecimalImage, CSng(((Me.m_NumberOfDigits - Me._DecimalPosition) * Me.SegWidth + 50)) * Me.ImageRatio, 150.0! * Me.ImageRatio)
            End If
            Dim num2 As Integer = 80
            Dim num3 As Integer = 250
            Dim num4 As Integer = 275
            Dim num5 As Integer = 4
            num = 0
            num1 = CInt(Math.Round(CDbl((Me.m_ValueSP * Convert.ToSingle(Me.m_Value_ValueScaleFactorSP)))))
            flag = False
            If (Not (CDbl(num1) <= Math.Pow(10, 4.0!) - 1 And CDbl(num1) >= (Math.Pow(10, 3.0!) - 1) * -1)) Then
                Dim num6 As Integer = num5
                For k As Integer = 1 To num6 Step 1
                    graphic.DrawImage(Me.LEDGreen(11), CSng((num3 + num2 * (k - 1))) * Me.ImageRatio, CSng(num4) * Me.ImageRatio)
                Next

            Else
                Dim num7 As Integer = num5
                For l As Integer = 1 To num7 Step 1
                    If (num1 >= 0) Then
                        num = Convert.ToInt32(Math.Floor(CDbl(num1) / Math.Pow(10, CDbl((num5 - l)))))
                        If (num > 0 Or l = num5 Or l > num5 - Me._DecimalPosition) Then
                            flag = True
                        End If
                        If (Not flag) Then
                            graphic.DrawImage(Me.LEDGreen(10), CSng((num3 + num2 * (l - 1))) * Me.ImageRatio, CSng(num4) * Me.ImageRatio)
                        Else
                            graphic.DrawImage(Me.LEDGreen(num), CSng((num3 + num2 * (l - 1))) * Me.ImageRatio, CSng(num4) * Me.ImageRatio)
                        End If
                        num1 = CInt(Math.Round(CDbl(num1) - CDbl(num) * Math.Pow(10, CDbl((num5 - l)))))
                    Else
                        graphic.DrawImage(Me.LEDGreen(11), CSng((num3 + num2 * (l - 1))) * Me.ImageRatio, CSng(num4) * Me.ImageRatio)
                        num1 = Math.Abs(num1)
                    End If
                Next

            End If
            painte.Graphics.DrawImage(Me.bitmap_16, 0, 0)
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        MyBase.Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If (CDbl(e.Location.Y) > CDbl(Me.StaticImage.Height) * 0.7) Then
            If (CDbl(e.Location.X) > CDbl(Me.StaticImage.Width) * 0.1 And CDbl(e.Location.X) < CDbl(Me.StaticImage.Width) * 0.24) Then
                Me.Button1Image = Me.Button1PressedImage
                MyBase.Invalidate()
                RaiseEvent Button1MouseDown(Me, e)
            End If
            If (CDbl(e.Location.X) > CDbl(Me.StaticImage.Width) * 0.31 And CDbl(e.Location.X) < CDbl(Me.StaticImage.Width) * 0.46) Then
                Me.Button2Image = Me.Button2PressedImage
                MyBase.Invalidate()
                RaiseEvent Button2MouseDown(Me, e)
            End If
            If (CDbl(e.Location.X) > CDbl(Me.StaticImage.Width) * 0.54 And CDbl(e.Location.X) < CDbl(Me.StaticImage.Width) * 0.69) Then
                Me.Button3Image = Me.Button3PressedImage
                MyBase.Invalidate()
                RaiseEvent Button3MouseDown(Me, e)
            End If
            If (CDbl(e.Location.X) > CDbl(Me.StaticImage.Width) * 0.76 And CDbl(e.Location.X) < CDbl(Me.StaticImage.Width) * 0.91) Then
                Me.Button4Image = Me.Button4PressedImage
                MyBase.Invalidate()
                RaiseEvent Button4MouseDown(Me, e)
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        If (Me.Button1Image Is Me.Button1PressedImage) Then
            RaiseEvent Button1MouseUp(Me, e)
        End If
        If (Me.Button2Image Is Me.Button2PressedImage) Then
            RaiseEvent Button2MouseUp(Me, e)
        End If
        If (Me.Button3Image Is Me.Button3PressedImage) Then
            RaiseEvent Button3MouseUp(Me, e)
        End If
        If (Me.Button4Image Is Me.Button4PressedImage) Then
            RaiseEvent Button4MouseUp(Me, e)
        End If
        Me.Button1Image = Me.Button1UpImage
        Me.Button2Image = Me.Button2UpImage
        Me.Button3Image = Me.Button3UpImage
        Me.Button4Image = Me.Button4UpImage
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.StaticImageRatio = CSng((CDbl(My.Resources.TempController.Height) / CDbl(My.Resources.TempController.Width)))
        If (Me.LastHeight < MyBase.Height Or Me.LastWidth < MyBase.Width) Then
            If (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.StaticImageRatio)) Then
                MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.StaticImageRatio))))
            Else
                MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.StaticImageRatio))))
            End If
        ElseIf (CDbl(MyBase.Height) / CDbl(MyBase.Width) <= CDbl(Me.StaticImageRatio)) Then
            MyBase.Width = CInt(Math.Round(CDbl((CSng(MyBase.Height) / Me.StaticImageRatio))))
        Else
            MyBase.Height = CInt(Math.Round(CDbl((CSng(MyBase.Width) * Me.StaticImageRatio))))
        End If
        Me.LastWidth = MyBase.Width
        Me.LastHeight = MyBase.Height
        Me.RefreshImage()
    End Sub


    Public Event Button1MouseDown As EventHandler


    Public Event Button1MouseUp As EventHandler


    Public Event Button2MouseDown As EventHandler


    Public Event Button2MouseUp As EventHandler


    Public Event Button3MouseDown As EventHandler

    Public Event Button3MouseUp As EventHandler


    Public Event Button4MouseDown As EventHandler


    Public Event Button4MouseUp As EventHandler

End Class
