'****************************************************************************
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* support@advancedhmi.com
'* 08-JAN-17
'*
'* Copyright 2017 Archie Jacobs
'*
'* Distributed under the GNU General Public License (www.gnu.org)
'*
'* This program is free software; you can redistribute it and/or
'* as published by the Free Software Foundation; either version 2
'* of the License, or (at your option) any later version.
'*
'* This program is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.

'* You should have received a copy of the GNU General Public License
'* along with this program; if not, write to the Free Software
'* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
'*
'* 08-JAN-17 Created
'* 16-NOV-19 Modified from BarMeter to AnalogMovingIndicator - James Landwerlen
'****************************************************************************
Imports System.Drawing

<System.ComponentModel.ToolboxItem(True)>
Public Class AnalogMovingIndicator

    Inherits System.Windows.Forms.Control
    Implements System.ComponentModel.ISupportInitialize

    Public Event ValueChanged As EventHandler
    Public Event WarningValueExceeded As EventHandler
    Public Event AlarmValueExceeded As EventHandler

    Private ArrowPoints(2) As System.Drawing.Point
    Private BarRectangles(6) As System.Drawing.Rectangle
    Private ArrowBrush As System.Drawing.SolidBrush
    Private ArrowPen As System.Drawing.Pen

#Region "Properties"

    '******************************************************************************************
    '* Use the base control's text property and make it visible as a property on the designer
    '******************************************************************************************
    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Protected m_Maximum As Double = 100
    ''' <summary>
    ''' The overall high value for the display
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The overall high value for the display")>
    <System.ComponentModel.Category("PLC Data")>
    Public Property Maximum() As Double
        Get
            Return m_Maximum
        End Get
        Set(ByVal value As Double)
            If m_Maximum <> value Then
                m_Maximum = value
                RefreshImage()
            End If
        End Set
    End Property

    Protected m_Minimum As Double = 0
    ''' <summary>
    ''' The overall low value for the display
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The overall low value for the display")>
    <System.ComponentModel.Category("PLC Data")>
    Public Property Minimum() As Double
        Get
            Return m_Minimum
        End Get
        Set(ByVal value As Double)
            If m_Minimum <> value Then
                m_Minimum = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Value As Double
    ''' <summary>
    ''' The current value of the process
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The current value of the process")>
    <System.ComponentModel.Category("PLC Data")>
    Property Value As Double
        Get
            Return m_Value
        End Get
        Set(ByVal value As Double)

            If value <> m_Value Then
                m_Value = value

                If m_HiAlarm <> String.Empty AndAlso (m_Value >= m_HiAlarm) Or m_LoAlarm <> String.Empty AndAlso (m_Value <= m_LoAlarm) Then
                    OnWarningValueExceeded(System.EventArgs.Empty)
                End If

                If m_HiHiAlarm <> String.Empty AndAlso (m_Value >= m_HiHiAlarm) Or m_LoLoAlarm <> String.Empty AndAlso (m_Value <= m_LoLoAlarm) Then
                    OnAlarmValueExceeded(System.EventArgs.Empty)
                End If

                SetArrowPosition()
                Invalidate()
                OnValueChanged(System.EventArgs.Empty)

            End If
        End Set
    End Property

    Private m_HiHiAlarm As String
    ''' <summary>
    ''' The value above which is a high-high alarm
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The value above which is a high-high alarm")>
    <System.ComponentModel.Category("PLC Data")>
    Property HiHiAlarm As String
        Get
            Return m_HiHiAlarm
        End Get
        Set(ByVal value As String)

            If value <> m_HiHiAlarm Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_HiHiAlarm = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_HiAlarm As String
    ''' <summary>
    ''' The value above which is a high alarm
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The value above which is a high alarm")>
    <System.ComponentModel.Category("PLC Data")>
    Property HiAlarm As String
        Get
            Return m_HiAlarm
        End Get
        Set(ByVal value As String)

            If value <> m_HiAlarm Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_HiAlarm = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_LoAlarm As String
    ''' <summary>
    ''' The value below which is a low alarm
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The value below which is a low alarm")>
    <System.ComponentModel.Category("PLC Data")>
    Property LoAlarm As String
        Get
            Return m_LoAlarm
        End Get
        Set(ByVal value As String)

            If value <> m_LoAlarm Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_LoAlarm = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_LoLoAlarm As String
    ''' <summary>
    ''' The value below which is a low-low alarm
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The value below which is a low-low alarm")>
    <System.ComponentModel.Category("PLC Data")>
    Property LoLoAlarm As String
        Get
            Return m_LoLoAlarm
        End Get
        Set(ByVal value As String)

            If value <> m_LoLoAlarm Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_LoLoAlarm = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_DesiredHigh As String
    ''' <summary>
    ''' The upper value of the desired operating range
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The upper value of the desired operating range")>
    <System.ComponentModel.Category("PLC Data")>
    Property DesiredHigh As String
        Get
            Return m_DesiredHigh
        End Get
        Set(ByVal value As String)

            If value <> m_DesiredHigh Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_DesiredHigh = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_DesiredLow As String
    ''' <summary>
    ''' The lower value of the desired operating range
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The lower value of the desired operating range")>
    <System.ComponentModel.Category("PLC Data")>
    Property DesiredLow As String
        Get
            Return m_DesiredLow
        End Get
        Set(ByVal value As String)

            If value <> m_DesiredLow Then
                If Not IsNumeric(value) Then
                    value = String.Empty
                End If
                m_DesiredLow = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_RangeFillColor As Drawing.Color = Color.FromArgb(221, 221, 221)
    ''' <summary>
    ''' The background color of the range strip
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The background color of the range strip")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property RangeFillColor() As Drawing.Color
        Get
            Return m_RangeFillColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_RangeFillColor <> value Then
                m_RangeFillColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_DesiredRangeColor As Drawing.Color = Color.FromArgb(184, 218, 255)
    ''' <summary>
    ''' The color of the desired range
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The color of the desired range")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property DesiredRangeColor() As Drawing.Color
        Get
            Return m_DesiredRangeColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_DesiredRangeColor <> value Then
                m_DesiredRangeColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_InactiveAlarmColor As Drawing.Color = Color.FromArgb(128, 128, 128)
    ''' <summary>
    ''' The color of an inactive alarm range
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The color of an inactive alarm range")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property InactiveAlarmColor() As Drawing.Color
        Get
            Return m_InactiveAlarmColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_InactiveAlarmColor <> value Then
                m_InactiveAlarmColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Level1AlarmColor As Drawing.Color = Color.FromArgb(255, 0, 0)
    ''' <summary>
    ''' The color of an active level 1 alarm (Hi-Hi or Lo-Lo)
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The color of an active level 1 alarm (Hi-Hi or Lo-Lo)")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property Level1AlarmColor() As Drawing.Color
        Get
            Return m_Level1AlarmColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_Level1AlarmColor <> value Then
                m_Level1AlarmColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_Level2AlarmColor As Drawing.Color = Color.FromArgb(255, 255, 0)
    ''' <summary>
    ''' The color of an active level 2 alarm (Hi or Lo)
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The color of an active level 2 alarm (Hi or Lo)")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property Level2AlarmColor() As Drawing.Color
        Get
            Return m_Level2AlarmColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_Level2AlarmColor <> value Then
                m_Level2AlarmColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_BorderColor As Drawing.Color = Color.FromArgb(64, 64, 64)
    ''' <summary>
    ''' The border color of the range strip
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The border color of the range strip")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property BorderColor() As Drawing.Color
        Get
            Return m_BorderColor
        End Get
        Set(ByVal value As Drawing.Color)
            If m_BorderColor <> value Then
                m_BorderColor = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_BorderSize As Integer = 1
    ''' <summary>
    ''' The border size of the range strip
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The border size of the range strip")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property BorderSize() As Integer
        Get
            Return m_BorderSize
        End Get
        Set(ByVal value As Integer)
            If m_BorderSize <> value Then
                m_BorderSize = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_ReverseIndicator As Boolean = False
    ''' <summary>
    ''' Put the indicator triangle on the other side of the track
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("Put the indicator triangle on the other side of the track")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property ReverseIndicator() As Boolean
        Get
            Return m_ReverseIndicator
        End Get
        Set(ByVal value As Boolean)
            If m_ReverseIndicator <> value Then
                m_ReverseIndicator = value
                RefreshImage()
            End If
        End Set
    End Property

    Private m_ArrowWidth As Integer = 15
    ''' <summary>
    ''' The arrow size
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The arrow size")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property ArrowWidth As Integer
        Get
            Return m_ArrowWidth
        End Get
        Set(value As Integer)
            If m_ArrowWidth <> value Then
                m_ArrowWidth = value
                RefreshImage()
            End If
        End Set
    End Property

    ''' <summary>
    ''' The arrow color
    ''' </summary>
    ''' <remarks></remarks>
    <System.ComponentModel.Description("The arrow color")>
    <System.ComponentModel.Category("PLC Appearance")>
    Public Property ArrowColor As System.Drawing.Color
        Get
            Return ArrowBrush.Color
        End Get
        Set(value As System.Drawing.Color)
            If ArrowBrush.Color <> value Then
                ArrowBrush.Color = value
                ArrowPen.Color = value
                RefreshImage()
            End If
        End Set
    End Property

#End Region



#Region "Private Methods"

    Private Sub RefreshImage()

        Dim ArrowHeight As Double = m_ArrowWidth
        Dim xPoint As Double
        If m_ReverseIndicator Then
            xPoint = m_ArrowWidth
        Else
            xPoint = Width - m_ArrowWidth
        End If
        Dim yPoint As Double = ((m_Value - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)

        If m_ReverseIndicator Then
            ArrowPoints(0) = New System.Drawing.Point(xPoint, yPoint)
            ArrowPoints(1) = New System.Drawing.Point(xPoint - ArrowWidth, yPoint - ArrowHeight / 2)
            ArrowPoints(2) = New System.Drawing.Point(xPoint - ArrowWidth, yPoint - -ArrowHeight / 2)
        Else
            ArrowPoints(0) = New System.Drawing.Point(xPoint, yPoint)
            ArrowPoints(1) = New System.Drawing.Point(xPoint + ArrowWidth, yPoint + ArrowHeight / 2)
            ArrowPoints(2) = New System.Drawing.Point(xPoint + ArrowWidth, yPoint + -ArrowHeight / 2)
        End If

        '* Draw full rectangle
        BarRectangles(0).Width = Math.Max(Width - (m_ArrowWidth * 2), 2) 'changed
        BarRectangles(0).Height = Height - ArrowHeight
        BarRectangles(0).X = m_ArrowWidth
        BarRectangles(0).Y = ArrowHeight / 2

        '* Rectangle for Desired Range
        Dim dRange As Double
        If Not IsNumeric(m_DesiredLow) Then m_DesiredLow = 0
        If IsNumeric(m_DesiredHigh) Then
            dRange = m_DesiredHigh - m_DesiredLow
        End If
        If IsNumeric(dRange) AndAlso dRange <= m_Maximum AndAlso IsNumeric(m_DesiredLow) Then
            BarRectangles(1).Width = BarRectangles(0).Width
            BarRectangles(1).Height = (dRange / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
            BarRectangles(1).X = m_ArrowWidth
            Dim Rate = (BarRectangles(0).Y - (Height - (ArrowHeight / 2))) / (m_Maximum - m_Minimum)
            BarRectangles(1).Y = ((m_DesiredHigh - m_Minimum) * Rate) + (Height - (ArrowHeight / 2))
        Else
            BarRectangles(1) = Nothing
        End If

        '* Rectangle for Lo Lo Alarm
        If IsNumeric(m_LoLoAlarm) Then
            BarRectangles(2).Width = BarRectangles(0).Width
            BarRectangles(2).Height = ((m_LoLoAlarm - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
            BarRectangles(2).X = m_ArrowWidth
            BarRectangles(2).Y = (BarRectangles(0).Y + BarRectangles(0).Height) - BarRectangles(2).Height
        Else
            BarRectangles(2) = Nothing
        End If

        '* Rectangle for Lo Alarm
        If IsNumeric(m_LoAlarm) Then
            BarRectangles(3).Width = BarRectangles(0).Width
            BarRectangles(3).Height = ((m_LoAlarm - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
            BarRectangles(3).X = m_ArrowWidth
            BarRectangles(3).Y = (BarRectangles(0).Y + BarRectangles(0).Height) - BarRectangles(3).Height
        Else
            BarRectangles(3) = Nothing
        End If

        '* Rectangle for Hi Alarm
        If IsNumeric(m_HiAlarm) AndAlso m_HiAlarm >= m_Minimum Then
            BarRectangles(4).Width = BarRectangles(0).Width
            BarRectangles(4).X = m_ArrowWidth
            BarRectangles(4).Y = BarRectangles(0).Y
            BarRectangles(4).Height = BarRectangles(0).Height - ((m_HiAlarm - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
        Else
            BarRectangles(4) = Nothing
        End If

        '* Rectangle for Hi Hi Alarm
        If IsNumeric(m_HiHiAlarm) AndAlso m_HiHiAlarm >= m_Minimum Then
            BarRectangles(5).Width = BarRectangles(0).Width
            BarRectangles(5).X = m_ArrowWidth
            BarRectangles(5).Y = BarRectangles(0).Y
            BarRectangles(5).Height = BarRectangles(0).Height - ((m_HiHiAlarm - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
        Else
            BarRectangles(5) = Nothing
        End If

        SetArrowPosition()
        Invalidate()

    End Sub

    Private Sub SetArrowPosition()

        Dim ConstrainedValue As Double = Math.Min(m_Maximum, m_Value)
        ConstrainedValue = Math.Max(ConstrainedValue, m_Minimum)
        Dim ArrowHeight As Double = ArrowWidth
        Dim YPixelOffset As Double = ((ConstrainedValue - m_Minimum) / (m_Maximum - m_Minimum)) * (Height - ArrowHeight)
        Dim yPoint As Double = Height - ArrowWidth / 2 - YPixelOffset

        If m_Value >= m_Minimum AndAlso m_Value <= m_Maximum Then
            ArrowPoints(0).Y = yPoint
            ArrowPoints(1).Y = yPoint + ArrowHeight / 2
            ArrowPoints(2).Y = yPoint + -ArrowHeight / 2
        Else
            ArrowPoints(0).Y = Nothing
            ArrowPoints(1).Y = Nothing
            ArrowPoints(2).Y = Nothing
        End If

    End Sub

#End Region

#Region "Events"



    Protected Overridable Sub OnValueChanged(e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Protected Overridable Sub OnWarningValueExceeded(e As EventArgs)
        RaiseEvent WarningValueExceeded(Me, e)
    End Sub

    Protected Overridable Sub OnAlarmValueExceeded(e As EventArgs)
        RaiseEvent AlarmValueExceeded(Me, e)
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        Dim borderPen As New Pen(m_BorderColor, m_BorderSize)

        e.Graphics.FillPolygon(ArrowBrush, ArrowPoints)

        '* Draw full rectangle 0
        '* Rectangle for Desired Range 1
        '* Rectangle for Lo Lo Alarm 2
        '* Rectangle for Lo Alarm 3
        '* Rectangle for Hi Alarm 4
        '* Rectangle for Hi Hi Alarm 5

        Using br1 = New SolidBrush(m_RangeFillColor)
            e.Graphics.FillRectangle(br1, BarRectangles(0))
        End Using


        If m_LoAlarm <> String.Empty Then 'Disregard if not using Lo Alarm
            If m_Value <= m_LoAlarm Then
                Using br4 = New SolidBrush(m_Level2AlarmColor)
                    e.Graphics.FillRectangle(br4, BarRectangles(3))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(3))
                End Using
            Else
                Using br4 = New SolidBrush(m_InactiveAlarmColor)
                    e.Graphics.FillRectangle(br4, BarRectangles(3))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(3))
                End Using
            End If
        End If


        If m_LoLoAlarm <> String.Empty Then 'Disregard if not using Lo Lo Alarm
            If m_Value <= m_LoLoAlarm Then
                Using br3 = New SolidBrush(m_Level1AlarmColor)
                    e.Graphics.FillRectangle(br3, BarRectangles(2))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(2))
                End Using
            Else
                Using br3 = New SolidBrush(m_InactiveAlarmColor)
                    e.Graphics.FillRectangle(br3, BarRectangles(2))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(2))
                End Using
            End If
        End If


        If m_HiAlarm <> String.Empty Then 'Disregard if not using Hi Alarm
            If m_Value >= m_HiAlarm Then
                Using br5 = New SolidBrush(m_Level2AlarmColor)
                    e.Graphics.FillRectangle(br5, BarRectangles(4))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(4))
                End Using
            Else
                Using br5 = New SolidBrush(m_InactiveAlarmColor)
                    e.Graphics.FillRectangle(br5, BarRectangles(4))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(4))
                End Using
            End If
        End If

        If m_HiHiAlarm <> String.Empty Then 'Disregard if not using Hi Hi Alarm
            If m_Value >= m_HiHiAlarm Then
                Using br6 = New SolidBrush(m_Level1AlarmColor)
                    e.Graphics.FillRectangle(br6, BarRectangles(5))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(5))
                End Using
            Else
                Using br6 = New SolidBrush(m_InactiveAlarmColor)
                    e.Graphics.FillRectangle(br6, BarRectangles(5))
                    e.Graphics.DrawRectangle(borderPen, BarRectangles(5))
                End Using
            End If
        End If

        Using br2 = New SolidBrush(m_DesiredRangeColor)
            e.Graphics.FillRectangle(br2, BarRectangles(1))
            e.Graphics.DrawRectangle(borderPen, BarRectangles(1))
        End Using

        If m_Value >= m_Minimum AndAlso m_Value <= m_Maximum Then
            e.Graphics.DrawLine(ArrowPen, m_ArrowWidth, ArrowPoints(0).Y, Width - m_ArrowWidth, ArrowPoints(0).Y)
        End If
        e.Graphics.DrawRectangle(borderPen, BarRectangles(0))

    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        RefreshImage()
        Invalidate()
    End Sub

#End Region

#Region "Constructor/Destructor"

    Public Sub New()
        MyBase.New()


        '* Eliminate Flicker
        Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        For index = 0 To 5
            BarRectangles(index) = New System.Drawing.Rectangle
        Next
        ArrowBrush = New System.Drawing.SolidBrush(Drawing.Color.Black)
        ArrowPen = New System.Drawing.Pen(Drawing.Color.Black)

    End Sub

    '****************************************************************
    '* UserControl overrides dispose to clean up the component list.
    '****************************************************************
    Protected Overrides Sub dispose(disposing As Boolean)
        Try
            If disposing Then
                If ArrowBrush IsNot Nothing Then ArrowBrush.Dispose()
                If ArrowPen IsNot Nothing Then ArrowPen.Dispose()

            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region



#Region "IniFileHandling"

    Private m_IniFileName As String = String.Empty
    <System.ComponentModel.Category("PLC Data")>
    Public Property IniFileName As String
        Get
            Return m_IniFileName
        End Get
        Set(value As String)
            m_IniFileName = value
        End Set
    End Property

    Private m_IniFileSection As String
    <System.ComponentModel.Category("PLC Data")>
    Public Property IniFileSection As String
        Get
            Return m_IniFileSection
        End Get
        Set(value As String)
            m_IniFileSection = value
        End Set
    End Property

    Private Initializing As Boolean
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit
        Initializing = True
    End Sub

    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        If Not Me.DesignMode Then
            If Not String.IsNullOrEmpty(m_IniFileName) Then
                Try
                    Utilities.SetPropertiesByIniFile(Me, m_IniFileName, m_IniFileSection)
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show("INI File - " & ex.Message)
                End Try
            End If
        End If
        Initializing = False
    End Sub

#End Region

End Class
