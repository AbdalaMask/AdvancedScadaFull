Imports System.Drawing
Imports System.Windows.Forms

Public Class Odometer
    Inherits Control

    Private BackBuffer As Bitmap
    Private digits(4) As RollingDigit

#Region "Constructor"
    Public Sub New()
        For i = 0 To digits.Length - 1
            digits(i) = New RollingDigit
        Next
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
                m_Value = value

                '*****************************************************
                '* Break the value into the individual digits
                '*****************************************************
                Dim WorkingValue As Double = m_Value
                '* Extract each digit's value
                For i = 0 To digits.Count - 1
                    Dim DigitValue As Double = WorkingValue / (10 ^ (digits.Count - i - 1))
                    digits(i).Value = DigitValue

                    WorkingValue -= (Math.Truncate(DigitValue) * (10 ^ (digits.Count - i - 1)))
                Next

                '****************************************************************
                '* Last digit always rolls, all other digits roll with next nine
                '****************************************************************
                For index = digits.Count - 2 To 0 Step -1
                    If digits(index + 1).Value <= 9 Then
                        digits(index).Value = Math.Truncate(digits(index).Value)
                    Else
                        '* Is the digits after in a roll? If so, roll the same
                        Dim v As Double = (digits(index + 1).Value - Math.Truncate(digits(index + 1).Value))
                        digits(index).Value = Math.Truncate(digits(index).Value) + v
                    End If
                Next

                Me.Invalidate()
            End If
        End Set
    End Property

    '* Use the ont in the base control for the font of the rolling digits
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            For i = 0 To digits.Length - 1
                digits(i).Font = value
            Next
        End Set
    End Property

    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
            For i = 0 To digits.Length - 1
                digits(i).BackColor = value
            Next
        End Set
    End Property

    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
            For i = 0 To digits.Length - 1
                digits(i).ForeColor = value
            Next
        End Set
    End Property
#End Region


#Region "Events"
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If BackBuffer Is Nothing Then
            BackBuffer = New Bitmap(Me.Width, Me.Height)
        End If

        Using g As Graphics = Graphics.FromImage(BackBuffer)
            For index = 0 To digits.Count - 1
                digits(index).Draw(g, index * digits(index).Width, 0)
            Next

            e.Graphics.DrawImageUnscaled(BackBuffer, 0, 0)
        End Using
    End Sub

    '* Block background painting to reduce flicker
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        'MyBase.OnPaintBackground(pevent)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        Dim DigitWidth As Integer = CInt(Me.Width / digits.Count)
        For index = 0 To digits.Count - 1
            digits(index).Height = Me.Height
            digits(index).Width = DigitWidth
        Next

        '* Create this for doubke buffer to reduce flicker
        BackBuffer = New Bitmap(Me.Width, Me.Height)
    End Sub
#End Region
End Class
