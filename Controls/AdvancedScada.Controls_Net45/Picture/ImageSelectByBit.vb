Imports System.Drawing
Imports System.Windows.Forms

Public Class ImageSelectByBit
    Inherits PictureBox

#Region "Properties"
    Private m_ValueSelect1 As Boolean
    Public Property ValueSelect1() As Boolean
        Get
            Return m_ValueSelect1
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueSelect1 Then
                m_ValueSelect1 = value

                RefreshImage()
            End If
        End Set
    End Property

    Private m_ValueSelect2 As Boolean
    Public Property ValueSelect2() As Boolean
        Get
            Return m_ValueSelect2
        End Get
        Set(ByVal value As Boolean)
            If value <> m_ValueSelect2 Then
                m_ValueSelect2 = value

                RefreshImage()
            End If
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when All is off
    '*****************************************
    'Private m_LightOnColor As Color = Color.Green
    Private m_GraphicAllOff As Bitmap
    Public Property GraphicAllOff() As Bitmap
        Get
            Return (m_GraphicAllOff)
        End Get
        Set(ByVal value As Bitmap)
            m_GraphicAllOff = value
            RefreshImage()
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when
    '*****************************************
    Private m_GraphicSelect1 As Bitmap
    Public Property GraphicSelect1() As Bitmap
        Get
            Return (m_GraphicSelect1)
        End Get
        Set(ByVal value As Bitmap)
            m_GraphicSelect1 = value
            RefreshImage()
        End Set
    End Property

    '*****************************************
    '* Property - Image to Show when
    '*****************************************
    Private m_GraphicSelect2 As Bitmap
    Public Property GraphicSelect2() As Bitmap
        Get
            Return (m_GraphicSelect2)
        End Get
        Set(ByVal value As Bitmap)
            m_GraphicSelect2 = value
            RefreshImage()
        End Set
    End Property
#End Region

#Region "Private Methods"
    Private Sub RefreshImage()
        If m_ValueSelect1 Then
            If m_GraphicSelect1 IsNot Nothing Then
                Image = m_GraphicSelect1
            End If
        ElseIf m_ValueSelect2 Then
            If m_GraphicSelect2 IsNot Nothing Then
                Image = m_GraphicSelect2
            End If
        Else
            If m_GraphicAllOff IsNot Nothing Then
                Image = m_GraphicAllOff
            End If
        End If

        Invalidate()
    End Sub
#End Region
End Class
