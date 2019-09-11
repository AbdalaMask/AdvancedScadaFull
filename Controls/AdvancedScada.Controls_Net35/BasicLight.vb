Imports System.Drawing

Public Class BasicLight
    Inherits System.Windows.Forms.UserControl

    Private StaticImage As Bitmap
    Private TextRect As New Rectangle
    Private ImageRatio As Single

#Region "Properties"
    Private m_Value As Boolean
    Public Property Value() As Boolean
        Get
            Return m_Value
        End Get
        Set(ByVal value As Boolean)
            If value <> m_Value Then
                m_Value = value

                Me.Invalidate()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Color of Light
    '*****************************************
    Private m_LightOnColor As Color = Color.Green
    Private BrushOnColor As New SolidBrush(Color.Green)
    Public Property LightOnColor() As Color
        Get
            Return (m_LightOnColor)
        End Get
        Set(ByVal value As Color)
            m_LightOnColor = value
            BrushOnColor.Dispose()
            BrushOnColor = New SolidBrush(m_LightOnColor)
            'RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Color of Light
    '*****************************************
    Private m_LightOffColor As Color = Color.Black
    Private BrushOffColor As New SolidBrush(Color.Black)
    Public Property LightOffColor() As Color
        Get
            Return (m_LightOffColor)
        End Get
        Set(ByVal value As Color)
            m_LightOffColor = value
            BrushOffColor.Dispose()
            BrushOffColor = New SolidBrush(m_LightOffColor)
            'RefreshImage()
            Me.Invalidate()
        End Set
    End Property

    '*****************************************
    '* Property - Text on Legend Plate
    '*****************************************
    Private m_LegendText As String = "Text"
    Public Property LegendText() As String
        Get
            Return m_LegendText
        End Get
        Set(ByVal value As String)
            m_LegendText = value
            RefreshImage()
            Me.Invalidate()
        End Set
    End Property


    '*****************************************
    '* Property - What to do to bit in PLC
    '*****************************************
    Public Enum OutputTypes
        MomentarySet
        MomentaryReset
        SetTrue
        SetFalse
        Toggle
    End Enum
    Private m_OutputType As OutputTypes = OutputTypes.MomentarySet
    Public Property OutputType() As OutputTypes
        Get
            Return m_OutputType
        End Get
        Set(ByVal value As OutputTypes)
            m_OutputType = value
        End Set
    End Property

    '* This is necessary to make the background draw correctly
    '*  http://www.bobpowell.net/transcontrols.htm
    '*part of the transparent background code
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or 32
            Return cp
            Return MyBase.CreateParams
        End Get
    End Property
#End Region


#Region "Events"
    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '* This is part of the transparent background code and it stops flicker
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        'MyBase.OnPaintBackground(e)
    End Sub

    '*************************************************************************
    '* Manually double buffer in order to allow a true transparent background
    '**************************************************************************
    Private _backBuffer As Bitmap
    Private GrayPen As New Pen(Color.Gray, 2)
    Private LegendFont As Font
    Private sf As New StringFormat
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'If _backBuffer Is Nothing Then Exit Sub


        'Dim g As Graphics = Graphics.FromImage(_backBuffer)
        Dim g As Graphics = e.Graphics


        If m_Value Then
            g.FillEllipse(BrushOnColor, 1, 1, Me.Width - 2, Me.Height - 2)
        Else
            g.FillEllipse(BrushOffColor, 1, 1, Me.Width - 2, Me.Height - 2)
        End If

        g.DrawEllipse(GrayPen, 0, 0, Me.Width - 1, Me.Height - 1)

        g.DrawString(m_LegendText, LegendFont, Brushes.Bisque, Me.Width / 2, Me.Height / 2, sf)

        'Copy the back buffer to the screen
        'e.Graphics.DrawImage(_backBuffer, 0, 0)
    End Sub





    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center



    End Sub



    Private Sub Meter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub











#End Region


    Private Sub MomentaryButton_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        RefreshImage()
    End Sub

    Private Sub RefreshImage()
        '* Create a new resized backbuffer for double buffering
        If _backBuffer IsNot Nothing Then _backBuffer.Dispose()
        _backBuffer = New Bitmap(Me.Width, Me.Height)

        If LegendFont IsNot Nothing Then LegendFont.Dispose()
        LegendFont = New Font("Arial", Me.Height / 8, FontStyle.Regular, GraphicsUnit.Point)

        Me.Invalidate()
    End Sub

End Class
