Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Controls
    Public Class MultistateIndicator
        Inherits Control
        ' Events
        Public Event ValueChanged As EventHandler

        ' Methods
        Public Sub New()
            Me.solidBrush_2 = New SolidBrush(MyBase.ForeColor)
            Me.solidBrush_0 = New SolidBrush(MyBase.BackColor)
            Me.m_BorderColor = New Pen(Brushes.White, 1.0!)
            MyBase.SetStyle((ControlStyles.OptimizedDoubleBuffer Or (ControlStyles.AllPaintingInWmPaint Or (ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint))), True)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            Me.stringFormat_0.Dispose()
            Me.m_StatesFont.Dispose()
            Me.solidBrush_2.Dispose()
            Me.m_StatesForecolor.Dispose()

            If (Me.solidBrush_0 IsNot Nothing) Then
                Me.solidBrush_0.Dispose()
            End If
            If (Me.m_BorderColor IsNot Nothing) Then
                Me.m_BorderColor.Dispose()
            End If
            If (Me.linearGradientBrush_0 IsNot Nothing) Then
                Me.linearGradientBrush_0.Dispose()
            End If
        End Sub

        Private Sub method_0()
            Dim red As Integer = Convert.ToInt32(Math.Min(CDbl((Me.m_SelectedStateBackColor.R * 1.4)), CDbl(255)))
            Dim green As Integer = Convert.ToInt32(Math.Min(CDbl((Me.m_SelectedStateBackColor.G * 1.4)), CDbl(255)))
            Dim color As Color = Color.FromArgb(Me.m_SelectedStateBackColor.A, red, green, Convert.ToInt32(Math.Min(CDbl((Me.m_SelectedStateBackColor.B * 1.4)), CDbl(255))))
            If (Me.linearGradientBrush_0 IsNot Nothing) Then
                Me.linearGradientBrush_0.Dispose()
            End If
            Dim introduced7 As Integer = Convert.ToInt32(CDbl((Me.m_SelectedStateBackColor.R * 0.6)))
            Dim introduced8 As Integer = Convert.ToInt32(CDbl((Me.m_SelectedStateBackColor.G * 0.6)))
            Dim color2 As Color = Color.FromArgb(Me.m_SelectedStateBackColor.A, introduced7, introduced8, Convert.ToInt32(CDbl((Me.m_SelectedStateBackColor.B * 0.6))))
            Dim point As New Point(0, 0)
            Dim point2 As New Point(0, MyBase.Height)
            Me.linearGradientBrush_0 = New LinearGradientBrush(point, point2, color, color2)
            MyBase.Invalidate()
        End Sub

        Protected Overrides Sub OnBackColorChanged(ByVal eventArgs_0 As EventArgs)
            MyBase.OnBackColorChanged(eventArgs_0)
            Me.solidBrush_0.Color = Me.BackColor
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            Me.stringFormat_0.Alignment = StringAlignment.Center
            Me.stringFormat_0.LineAlignment = StringAlignment.Center
            If (Me.solidBrush_2 Is Nothing) Then
                Me.solidBrush_2 = New SolidBrush(MyBase.ForeColor)
            End If
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal eventArgs_0 As EventArgs)
            MyBase.OnForeColorChanged(eventArgs_0)
            If (Me.solidBrush_2 Is Nothing) Then
                Me.solidBrush_2 = New SolidBrush(Color.Black)
            Else
                Me.solidBrush_2.Color = MyBase.ForeColor
            End If
            MyBase.Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(ByVal paintEventArgs_0 As PaintEventArgs)
            If ((((Not paintEventArgs_0 Is Nothing) AndAlso (Not paintEventArgs_0.Graphics Is Nothing)) AndAlso (Not Me.solidBrush_2 Is Nothing)) AndAlso Not ((MyBase.Height <= 0) Or (MyBase.Width <= 0))) Then
                Dim graphics As Graphics = paintEventArgs_0.Graphics
                Dim height As Integer = (Me.Font.Height + 4)
                If String.IsNullOrEmpty(Me.Text) Then
                    height = 0
                End If
                graphics.FillRectangle(Brushes.LightGray, 0, 0, MyBase.Width, MyBase.Height)
                graphics.FillRectangle(Me.solidBrush_0, 0, 0, MyBase.Width, height)
                Dim width As Integer = MyBase.Width
                If (Me.States.Count > 1) Then
                    width = Convert.ToInt32(CDbl((CDbl(MyBase.Width) / CDbl(Me.States.Count))))
                End If
                If (height > 0) Then
                    graphics.DrawLine(Pens.White, 0, height, MyBase.Width, height)
                End If
                Dim num3 As Integer = (Me.States.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num3)
                    Dim x As Single = Convert.ToSingle(CDbl(((width * i) + (CDbl(width) / 2))))
                    Dim y As Single = Convert.ToSingle(CDbl((height + (CDbl(((MyBase.Height - Me.Font.Height) - 4)) / 2))))
                    If (Me.m_States(i).Value = Me.m_Value) Then
                        If (Me.linearGradientBrush_0 Is Nothing) Then
                            Me.method_0()
                        End If
                        If (Me.linearGradientBrush_0 Is Nothing) Then
                            graphics.FillRectangle(Brushes.Maroon, (width * i), (height + 1), width, (MyBase.Height - height))
                        Else
                            graphics.FillRectangle(Me.linearGradientBrush_0, (width * i), (height + 1), width, (MyBase.Height - height))
                        End If
                    End If
                    graphics.DrawLine(Me.m_BorderColor, (width * i), height, (width * i), MyBase.Height)
                    If (height > 0) Then
                        graphics.DrawString(Me.m_States(i).Message, Me.StatesFont, Me.m_StatesForecolor, x, y, Me.stringFormat_0)
                    End If
                    i += 1
                Loop
                If Not String.IsNullOrEmpty(MyBase.Text) Then
                    graphics.DrawString(MyBase.Text, MyBase.Font, Me.solidBrush_2, CSng(Convert.ToInt32(CDbl((CDbl(MyBase.Width) / 2)))), CSng(Convert.ToInt32(CDbl(((CDbl(MyBase.Font.Height) / 2) + 2)))), Me.stringFormat_0)
                End If
                graphics.DrawRectangle(Me.m_BorderColor, 0, 0, (MyBase.Width - 1), (MyBase.Height - 1))
            End If
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
            MyBase.OnTextChanged(e)
            MyBase.Invalidate()
        End Sub

        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            RaiseEvent ValueChanged(Me, e)
        End Sub


        ' Properties
        Public Property Value As Integer
            Get
                Return Me.m_Value
            End Get
            Set(ByVal value As Integer)
                If (value <> Me.m_Value) Then
                    Me.m_Value = value
                    MyBase.Invalidate()
                    Me.OnValueChanged(EventArgs.Empty)
                End If
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property States As Collection(Of MessageByValue)
            Get
                Return Me.m_States
            End Get
        End Property

        Public Property SelectedStateBackColor As Color
            Get
                Return Me.m_SelectedStateBackColor
            End Get
            Set(ByVal value As Color)
                If (Me.m_SelectedStateBackColor <> value) Then
                    Me.m_SelectedStateBackColor = value
                    If (MyBase.Height > 0) Then
                        Me.method_0()
                    End If
                End If
            End Set
        End Property

        Public Property StatesFont As Font
            Get
                Return Me.m_StatesFont
            End Get
            Set(ByVal value As Font)
                Me.m_StatesFont = value
                MyBase.Invalidate()
            End Set
        End Property

        Public Property StatesForecolor As Color
            Get
                Return Me.m_StatesForecolor.Color
            End Get
            Set(ByVal value As Color)
                Me.m_StatesForecolor.Color = value
                MyBase.Invalidate()
            End Set
        End Property

        Public Property BorderColor As Color
            Get
                Return Me.m_BorderColor.Color
            End Get
            Set(ByVal value As Color)
                If (Me.m_BorderColor.Color <> value) Then
                    If (Me.m_BorderColor IsNot Nothing) Then
                        Me.m_BorderColor.Color = value
                    Else
                        Me.m_BorderColor = New Pen(value, 1.0!)
                    End If
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        Public Property BorderWidth As Integer
            Get
                Return Convert.ToInt32(Me.m_BorderColor.Width)
            End Get
            Set(ByVal value As Integer)
                If (Me.m_BorderColor.Width <> value) Then
                    If (Me.m_BorderColor IsNot Nothing) Then
                        Me.m_BorderColor.Width = value
                    Else
                        If (Me.m_BorderColor IsNot Nothing) Then
                            Me.m_BorderColor.Dispose()
                        End If
                        Me.m_BorderColor = New Pen(Brushes.White, CSng(value))
                    End If
                    MyBase.Invalidate()
                End If
            End Set
        End Property

        Public Property NumericFormat As String
            Get
                Return Me.m_NumericFormat
            End Get
            Set(ByVal value As String)
                Me.m_NumericFormat = value
            End Set
        End Property

        Public Property ValueScaleFactor As Decimal
            Get
                Return Me.m_ValueScaleFactor
            End Get
            Set(ByVal value As Decimal)
                Me.m_ValueScaleFactor = value
            End Set
        End Property
        Private solidBrush_0 As SolidBrush
        Private m_Value As Integer
        Private m_States As Collection(Of MessageByValue) = New Collection(Of MessageByValue)
        Private linearGradientBrush_0 As LinearGradientBrush
        Private m_SelectedStateBackColor As Color = Color.Maroon
        Private m_StatesFont As Font = New Font("Arial", 16.0!, FontStyle.Regular, GraphicsUnit.Point)
        Private m_StatesForecolor As SolidBrush = New SolidBrush(Color.Black)
        Private m_BorderColor As Pen
        Private m_NumericFormat As String
        Private m_ValueScaleFactor As Decimal = Decimal.One
        Private stringFormat_0 As StringFormat = New StringFormat
        Private solidBrush_2 As SolidBrush
    End Class
End Namespace

