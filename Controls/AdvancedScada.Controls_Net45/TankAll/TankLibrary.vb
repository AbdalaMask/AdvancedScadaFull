Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Public Class TankLibrary
    Inherits Control

#Region "Filde"
    Private TankImage As Bitmap
    Private TankCategory As Bitmap
    Private TextRectangle As Rectangle
    Private TextBrush As SolidBrush
    Private sfCenter As StringFormat
    Private sfCenterTop As StringFormat
    Private m_TankContentColor As HatchBrush
    'private bool m_LockAspectRatio;
    'private int BarHeight;
    'private int FullBarHeight;
    Private _backBuffer As Bitmap
    Private ImageRatio As Single
    Private _TankColor As HMITankColorFour
    Private _TankType As HMITankTypeFour
#End Region
#Region "Enum"


    Public Enum HMITankColorFour
        Blue
        Green
        Gray
        Red
        Yellow
    End Enum
    Public Enum HMITankTypeFour
        Type1
        Type2
        Type3
        Type4
        Type5
        Type6
        Type7
        Type8
        Type9
        Type10

    End Enum
#End Region
#Region "Property"
    Public Property TankType() As HMITankTypeFour
        Get
            Return _TankType
        End Get
        Set(value As HMITankTypeFour)
            _TankType = value
            Select Case Me.TankType
                Case HMITankTypeFour.Type1
                    Select Case Me.TankColor
                        Case HMITankColorFour.Blue
                            Me.TankCategory = My.Resources.DrumBlue
                            Exit Select
                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.DrumGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Drum
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.DrumRed
                            Exit Select
                        Case HMITankColorFour.Yellow
                            Me.TankCategory = My.Resources.DrumBlack
                            Exit Select
                    End Select
                    Exit Select
                Case HMITankTypeFour.Type2
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Ketlle1Green
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Ketlle1
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Ketlle1Red
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type3
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.KetlleLongLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.KetlleLongLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.KetlleLongLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type4
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.KettleShortLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.KettleShortLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.KettleShortLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type5
                    Select Case Me.TankColor
                        Case HMITankColorFour.Blue
                            '' Me.TankCategory = My.Resources.SiloBlue
                            Exit Select
                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.SiloGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Silo
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.SiloRed
                            Exit Select
                        Case HMITankColorFour.Yellow
                            '' Me.TankCategory = My.Resources.SiloYellow
                            Exit Select
                    End Select
                    Exit Select
                Case HMITankTypeFour.Type6
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.SiloWithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.SiloWithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.SiloWithLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type7
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Tank1Green
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Tank1
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Tank1Red
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type8
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Tank1WithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Tank1WithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Tank1WithLegsRed
                            Exit Select

                    End Select
                    Exit Select

                Case HMITankTypeFour.Type9
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.VesselGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Vessel
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.VesselRed
                            Exit Select

                    End Select
                    Exit Select

                Case HMITankTypeFour.Type10
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.VesselWithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.VesselWithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.VesselRedWithLegs
                            Exit Select

                    End Select
                    Exit Select




            End Select
            Me.RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    Public Property TankColor() As HMITankColorFour
        Get
            Return _TankColor
        End Get
        Set(value As HMITankColorFour)
            _TankColor = value
            Select Case Me.TankType
                Case HMITankTypeFour.Type1
                    Select Case Me.TankColor
                        Case HMITankColorFour.Blue
                            Me.TankCategory = My.Resources.DrumBlue
                            Exit Select
                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.DrumGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Drum
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.DrumRed
                            Exit Select
                        Case HMITankColorFour.Yellow
                            Me.TankCategory = My.Resources.DrumBlack
                            Exit Select
                    End Select
                    Exit Select
                Case HMITankTypeFour.Type2
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Ketlle1Green
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Ketlle1
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Ketlle1Red
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type3
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.KetlleLongLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.KetlleLongLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.KetlleLongLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type4
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.KettleShortLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.KettleShortLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.KettleShortLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type5
                    Select Case Me.TankColor
                        Case HMITankColorFour.Blue
                            '' Me.TankCategory = My.Resources.SiloBlue
                            Exit Select
                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.SiloGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Silo
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.SiloRed
                            Exit Select
                        Case HMITankColorFour.Yellow
                            ''  Me.TankCategory = My.Resources.SiloYellow
                            Exit Select
                    End Select
                    Exit Select
                Case HMITankTypeFour.Type6
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.SiloWithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.SiloWithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.SiloWithLegsRed
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type7
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Tank1Green
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Tank1
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Tank1Red
                            Exit Select

                    End Select
                    Exit Select
                Case HMITankTypeFour.Type8
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.Tank1WithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Tank1WithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.Tank1WithLegsRed
                            Exit Select

                    End Select
                    Exit Select

                Case HMITankTypeFour.Type9
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.VesselGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.Vessel
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.VesselRed
                            Exit Select

                    End Select
                    Exit Select

                Case HMITankTypeFour.Type10
                    Select Case Me.TankColor

                        Case HMITankColorFour.Green
                            Me.TankCategory = My.Resources.VesselWithLegsGreen
                            Exit Select
                        Case HMITankColorFour.Gray
                            Me.TankCategory = My.Resources.VesselWithLegs
                            Exit Select
                        Case HMITankColorFour.Red
                            Me.TankCategory = My.Resources.VesselRedWithLegs
                            Exit Select

                    End Select
                    Exit Select




            End Select
            Me.RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
    <Browsable(True)> _
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(value As String)
            MyBase.Text = value
            Me.RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
    Public Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            Me.RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
    Public Shadows Property ForeColor() As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            If Not (value <> MyBase.ForeColor) Then
                Return
            End If
            MyBase.ForeColor = value
            If Me.TextBrush Is Nothing Then
                Me.TextBrush = New SolidBrush(MyBase.ForeColor)
            Else
                Me.TextBrush.Color = value
            End If
            Me.Invalidate()
        End Set
    End Property


    Public Property TankContentColor() As Color
        Get
            Return Me.m_TankContentColor.BackgroundColor
        End Get
        Set(value As Color)
            If Me.m_TankContentColor IsNot Nothing Then
                Me.m_TankContentColor.Dispose()
            End If
            Me.m_TankContentColor = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(240, Math.Max(CInt(value.R) - 40, 0), Math.Max(CInt(value.G) - 40, 0), Math.Max(CInt(value.B) - 40, 0)), value)
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim createParams__1 As CreateParams = MyBase.CreateParams
            createParams__1.ExStyle = createParams__1.ExStyle Or 32
            Return createParams__1
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        '* reduce the flicker
        SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        AddHandler Me.Resize, AddressOf Me.Tank_Resize
        Me.TextRectangle = New Rectangle()
        Me.sfCenter = New StringFormat()
        Me.sfCenterTop = New StringFormat()
        Me.m_TankContentColor = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Aqua)
        TankCategory = My.Resources.Tank1Red
        Me.RefreshImage()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If Not disposing Then
                Return
            End If
            Me.TankImage.Dispose()
            Me._backBuffer.Dispose()
            Me.m_TankContentColor.Dispose()
            Me.sfCenterTop.Dispose()
            Me.sfCenter.Dispose()
            Me.TextBrush.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If Me.TankImage Is Nothing Or Me._backBuffer Is Nothing Or Me.TextBrush Is Nothing Then
            Return
        End If
        Dim graphics__1 As Graphics = Graphics.FromImage(DirectCast(Me._backBuffer, Image))
        graphics__1.DrawImage(DirectCast(Me.TankImage, Image), 0, 0)
        If Me.m_TankContentColor IsNot Nothing Then
            graphics__1.FillRectangle(DirectCast(Me.m_TankContentColor, Brush), Convert.ToInt32(361.0 * (CDbl(Me.Width) / CDbl(Me.TankCategory.Width))), Convert.ToInt32(158.0 * (CDbl(Me.Height) / CDbl(Me.TankCategory.Height))), Convert.ToInt32(68.0 * (CDbl(Me.Width) / CDbl(Me.TankCategory.Width))), 0)
        End If
        If MyBase.Text IsNot Nothing AndAlso String.Compare(MyBase.Text, String.Empty) <> 0 Then
            If Me.TextBrush.Color <> MyBase.ForeColor Then
                Me.TextBrush.Color = MyBase.ForeColor
            End If
            'graphics__1.DrawString(MyBase.Text, MyBase.Font, DirectCast(Me.TextBrush, Brush), DirectCast(Me.TextRectangle, RectangleF), Me.sfCenterTop)
        End If
        e.Graphics.DrawImage(DirectCast(Me._backBuffer, Image), 0, 0)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        If Me._backBuffer IsNot Nothing Then
            Me._backBuffer.Dispose()
            Me._backBuffer = DirectCast(Nothing, Bitmap)
        End If
        MyBase.OnSizeChanged(e)
        If Me.Parent Is Nothing Then
            Return
        End If
        Me.Parent.Invalidate()
    End Sub

    Private Sub RefreshImage()
        Dim num1 As Single = CSng(Me.Width) / CSng(Me.TankCategory.Width)
        Dim num2 As Single = CSng(Me.Height) / CSng(Me.TankCategory.Height)
        Me.ImageRatio = CSng(Me.Width) / CSng(Me.Height)
        Me.ImageRatio = 1.0F
        Dim num3 As Integer
        Dim num4 As Integer
        If CDbl(num1) < CDbl(num2) Then
            num3 = Me.Height
            num4 = If(Not (Me.Height > 0 And Me.TankCategory.Height > 0), 1, CInt(Math.Round(CDbl(Me.TankCategory.Width) / CDbl(Me.TankCategory.Height) * CDbl(Me.Height))))
            Me.ImageRatio = num1
        Else
            num4 = Me.Width
            num3 = CInt(Math.Round(CDbl(Me.TankCategory.Height) / CDbl(Me.TankCategory.Width) * CDbl(Me.Width)))
            Me.ImageRatio = num2
        End If
        If CDbl(Me.ImageRatio) <= 0.0 Then
            Return
        End If
        If Me.TankImage IsNot Nothing Then
            Me.TankImage.Dispose()
        End If
        Me.TankImage = New Bitmap(Convert.ToInt32(Me.Width), Convert.ToInt32(Me.Height))
        Dim graphics__1 As Graphics = Graphics.FromImage(DirectCast(Me.TankImage, Image))
        graphics__1.DrawImage(DirectCast(Me.TankCategory, Image), 0, 0, Me.TankImage.Width, Me.TankImage.Height)
        Me.TextRectangle.X = 1
        Me.TextRectangle.Y = CInt(Math.Round(70.0 * CDbl(Me.ImageRatio) - 1.0))
        Me.TextRectangle.Width = Me.TankImage.Width - 2
        Me.TextRectangle.Height = CInt(Math.Round(CDbl(Me.TankImage.Height) * 0.1))
        Me.sfCenterTop.Alignment = StringAlignment.Center
        Me.sfCenterTop.LineAlignment = StringAlignment.Near
        Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        If Me._backBuffer IsNot Nothing Then
            Me._backBuffer.Dispose()
        End If
        Me._backBuffer = New Bitmap(Me.Width, Me.Height)
        graphics__1.Dispose()
        Me.Invalidate()
    End Sub

    Private Sub Tank_Resize(sender As Object, e As EventArgs)
        Me.RefreshImage()
    End Sub
#End Region
End Class
