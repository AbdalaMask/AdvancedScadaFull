Imports System.Drawing
Imports System.Windows.Forms

Public Class RollingDigit
    '* This class has some disposable objects, so we must implement IDisposable to clean up
    Implements IDisposable

    Private DigitDrawings(9) As System.Drawing.Bitmap
    Private disposedValue As Boolean ' To detect redundant calls

#Region "Constructor"
    Public Sub New()

    End Sub

    ' IDisposable - clean up DigitDrawings because they are disposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                For i = 0 To DigitDrawings.Length - 1
                    If DigitDrawings(i) IsNot Nothing Then
                        DigitDrawings(i).Dispose()
                    End If
                Next

                If m_Font IsNot Nothing Then
                    m_Font.Dispose()
                End If
            End If
        End If
        disposedValue = True
    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

#Region "Properties"
    Private m_Value As Double
    Public Property Value As Double
        Get
            Return m_Value
        End Get
        Set(value As Double)
            If value <> m_Value Then
                '* Constrain the value withing a single digit
                m_Value = Math.Max(value, 0)
                m_Value = Math.Min(m_Value, 9.999999999)
            End If
        End Set
    End Property

    Private m_Font As Font = New Font("Arial", 8)
    Public Property Font As Font
        Get
            Return m_Font
        End Get
        Set(value As Font)
            m_Font = value
            CreateDigitImages()
        End Set
    End Property

    Private m_BackColor As Color = Color.White
    Public Property BackColor As Color
        Get
            Return m_BackColor
        End Get
        Set(value As Color)
            If value <> m_BackColor Then
                m_BackColor = value
                CreateDigitImages()
            End If
        End Set
    End Property

    Private m_ForeColor As Color = Color.Black
    Public Property ForeColor As Color
        Get
            Return m_ForeColor
        End Get
        Set(value As Color)
            If value <> m_ForeColor Then
                m_ForeColor = value
                CreateDigitImages()
            End If
        End Set
    End Property

    Private m_Width As Integer
    Public Property Width As Integer
        Get
            Return m_Width
        End Get
        Set(value As Integer)
            If value <> m_Width Then
                m_Width = value
                CreateDigitImages()
            End If
        End Set
    End Property

    Private m_Height As Integer
    Public Property Height As Integer
        Get
            Return m_Height
        End Get
        Set(value As Integer)
            If value <> m_Height Then
                m_Height = value
                CreateDigitImages()
            End If
        End Set
    End Property
#End Region

#Region "Private Methods"
    Private Sub CreateDigitImages()
        '* Make sure it has a valid size
        If m_Width > 0 And m_Height > 0 Then
            Dim f As New Font(m_Font.FontFamily, 1, FontStyle.Regular, GraphicsUnit.Pixel)

            '* Search for the font size that fits the best
            Dim TextSize As New SizeF
            While TextSize.Width < m_Width And TextSize.Height < m_Height
                TextSize = TextRenderer.MeasureText("0", f)
                '* Increment the text size
                f = New Font(m_Font.FontFamily, f.Size + 1, FontStyle.Regular, GraphicsUnit.Pixel)
            End While

            '* The loop will exceed the size, so reduce it by one step
            f = New Font(m_Font.FontFamily, f.Size - 1, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center

            For Index = 0 To 9
                DigitDrawings(Index) = New Bitmap(m_Width, m_Height)
                Dim g As Graphics = Graphics.FromImage(DigitDrawings(Index))

                '* Improve the quality of the text
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

                '* Draw the Text in the center of the DigitDrawing
                g.DrawString(CStr(Index), f, New SolidBrush(m_ForeColor), DigitDrawings(Index).Width / 2, DigitDrawings(Index).Height / 2, sf)

                '* Draw a separting vertical line
                g.DrawLine(Pens.DarkGray, 0, 0, 0, m_Height)
            Next
        End If
    End Sub
#End Region

    Public Sub Draw(ByVal g As Graphics, ByVal offsetX As Integer, ByVal offsetY As Integer)
        If m_Width > 0 And m_Height > 0 Then
            '* Fill with the backcolor
            'g.Clear(m_BackColor)
            g.FillRectangle(New SolidBrush(m_BackColor), offsetX, offsetY, Me.Width, Me.Height)

            '* Find the lowest digit in display
            Dim LowDigit As Integer = CInt(Math.Floor(m_Value))


            Dim PercentOfRoll As Double = m_Value - LowDigit
            Dim RollOffsetPosition As Integer = m_Height * PercentOfRoll

            If (DigitDrawings(LowDigit)) Is Nothing Then
                CreateDigitImages()
            End If

            '* Give the effect of rolling over the arc
            Dim ImageHeightCompress As Double = Me.Height
            If PercentOfRoll > 0 Then
                ImageHeightCompress = (1 - PercentOfRoll * 0.5) * Me.Height
            End If

            '* Draw the digit that is rolling off the top
            g.DrawImage(DigitDrawings(LowDigit), offsetX, CInt(0 - RollOffsetPosition + (Me.Height - ImageHeightCompress)), Me.Width, CInt(ImageHeightCompress))

            '* Draw the next digit coming up
            If RollOffsetPosition > 0 Then
                Dim NextDigit As Integer = LowDigit + 1
                If NextDigit >= 10 Then NextDigit = 0
                If PercentOfRoll > 0 Then
                    ImageHeightCompress = Math.Min(Me.Height, (PercentOfRoll * 1.75) * Me.Height)
                End If
                g.DrawImage(DigitDrawings(NextDigit), offsetX, 0 + m_Height - RollOffsetPosition, Me.Width, CInt(ImageHeightCompress))
            End If

            '* Overlay a shadow to give depth
            Dim gb As New Drawing.Drawing2D.LinearGradientBrush(New Rectangle(0, 0, m_Height, m_Width), Color.Black, Color.Black, 0, False)
            Dim cb As New System.Drawing.Drawing2D.ColorBlend
            cb.Positions = New Single() {0, 0.15, 0.4, 0.75, 1.0}
            cb.Colors = New Color() {Color.FromArgb(144, 0, 0, 0), Color.FromArgb(64, 0, 0, 0), Color.FromArgb(0, 0, 0, 0),
                Color.FromArgb(64, 0, 0, 0), Color.FromArgb(240, 0, 0, 0)}
            gb.RotateTransform(90)
            gb.InterpolationColors = cb

            g.FillRectangle(gb, offsetX, offsetY, Me.Width, Me.Height)
        End If
    End Sub


    Public Shared Function GetRelativeColor(ByVal color As System.Drawing.Color, ByVal intensity As Double, ByVal alpha As Integer) As Drawing.Color
        intensity = Math.Max(intensity, 0)

        Dim c As Drawing.Color
        c = Drawing.Color.FromArgb(alpha,
                                   Math.Min((color.R + 1) * intensity, 255),
                                   Math.Min((color.G + 1) * intensity, 255),
                                   Math.Min((color.B + 1) * intensity, 255))

        Return c
    End Function
End Class
