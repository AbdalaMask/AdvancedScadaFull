Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class GraphicIndicator
    Inherits Control
    Private bitmap_0 As Bitmap

    Private bitmap_1 As Bitmap

    Private bitmap_2 As Bitmap

    Private bitmap_3 As Bitmap

    Private rectangle_0 As Rectangle

    Private float_0 As Single

    Private stringFormat_0 As StringFormat

    Private bool_0 As Boolean

    Private bool_1 As Boolean

    Private bitmap_4 As Bitmap

    Private bitmap_5 As Bitmap

    Private bitmap_6 As Bitmap

    Private timer_0 As System.Windows.Forms.Timer

    Private bool_2 As Boolean

    Private pictureBoxSizeMode_0 As PictureBoxSizeMode

    Private rotationAngleEnum_0 As GraphicIndicator.RotationAngleEnum

    Private contentAlignment_0 As ContentAlignment

    Private string_0 As String

    Private font_0 As System.Drawing.Font

    Private string_1 As String

    Private decimal_0 As Decimal

    Private outputTypes_0 As GraphicIndicator.OutputTypes

    Private stringFormat_1 As StringFormat

    Private rectangle_1 As Rectangle

    Protected TextBrush As SolidBrush

    Public Property Flash1 As Boolean
        Get
            Return Me.bool_2
        End Get
        Set(ByVal value As Boolean)
            If (Me.bool_2 <> value) Then
                Me.bool_2 = value
                If (Me.bool_2) Then
                    If (Me.timer_0 Is Nothing) Then
                        Me.timer_0 = New System.Windows.Forms.Timer() With
                        {
                            .Interval = 800
                        }
                        AddHandler Me.timer_0.Tick, New EventHandler(AddressOf Me.timer_0_Tick)
                        Me.timer_0.Start()
                    End If
                ElseIf (Me.timer_0 IsNot Nothing) Then
                    Me.timer_0.[Stop]()
                End If
            End If
        End Set
    End Property

    Public Property Font2 As System.Drawing.Font
        Get
            Return Me.font_0
        End Get
        Set(ByVal value As System.Drawing.Font)
            Me.font_0 = value
            MyBase.Invalidate()
        End Set
    End Property

    Public Property GraphicAllOff As Bitmap
        Get
            Return Me.bitmap_4
        End Get
        Set(ByVal value As Bitmap)
            'If (bitmap_4 <> value) Then
            '    Me.bitmap_4 = value
            '    Me.method_0()
            'End If
        End Set
    End Property

    Public Property GraphicSelect1 As Bitmap
        Get
            Return Me.bitmap_5
        End Get
        Set(ByVal value As Bitmap)
            Me.bitmap_5 = value
            Me.method_0()
        End Set
    End Property

    Public Property GraphicSelect2 As Bitmap
        Get
            Return Me.bitmap_6
        End Get
        Set(ByVal value As Bitmap)
            Me.bitmap_6 = value
            Me.method_0()
        End Set
    End Property

    Public Property NumericFormat As String
        Get
            Return Me.string_1
        End Get
        Set(ByVal value As String)
            Me.string_1 = value
        End Set
    End Property

    Public Property OutputType As GraphicIndicator.OutputTypes
        Get
            Return Me.outputTypes_0
        End Get
        Set(ByVal value As GraphicIndicator.OutputTypes)
            Me.outputTypes_0 = value
        End Set
    End Property

    Public Property RotationAngle As GraphicIndicator.RotationAngleEnum
        Get
            Return Me.rotationAngleEnum_0
        End Get
        Set(ByVal value As GraphicIndicator.RotationAngleEnum)
            If (Me.rotationAngleEnum_0 <> value) Then
                Me.rotationAngleEnum_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    Public Property SizeMode As PictureBoxSizeMode
        Get
            Return Me.pictureBoxSizeMode_0
        End Get
        Set(ByVal value As PictureBoxSizeMode)
            If (Me.pictureBoxSizeMode_0 <> value) Then
                Me.pictureBoxSizeMode_0 = value
                Me.method_0()
            End If
        End Set
    End Property

    ''  <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property Text2 As String
        Get
            Return Me.string_0
        End Get
        Set(ByVal value As String)
            If (Operators.CompareString(Me.string_0, value, False) <> 0) Then
                Me.string_0 = value
                If (Not String.IsNullOrEmpty(Me.string_1) And Not MyBase.DesignMode) Then
                    Try
                        Dim [single] As Single = Conversions.ToSingle(value) * Convert.ToSingle(Me.decimal_0)
                        Me.string_0 = [single].ToString(Me.string_1)
                    Catch exception As System.Exception
                        ProjectData.SetProjectError(exception)
                        Me.string_0 = "Check NumericFormat and variable type"
                        ProjectData.ClearProjectError()
                    End Try
                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property TextAlignment As ContentAlignment
        Get
            Return Me.contentAlignment_0
        End Get
        Set(ByVal value As ContentAlignment)
            If (Me.contentAlignment_0 <> value) Then
                Me.contentAlignment_0 = value
                If (Me.contentAlignment_0 = ContentAlignment.BottomCenter Or Me.contentAlignment_0 = ContentAlignment.MiddleCenter Or Me.contentAlignment_0 = ContentAlignment.TopCenter) Then
                    Me.stringFormat_0.Alignment = StringAlignment.Center
                ElseIf (Me.contentAlignment_0 = ContentAlignment.BottomLeft Or Me.contentAlignment_0 = ContentAlignment.MiddleLeft Or Me.contentAlignment_0 = ContentAlignment.TopLeft) Then
                    Me.stringFormat_0.Alignment = StringAlignment.Near
                ElseIf (Me.contentAlignment_0 = ContentAlignment.BottomRight Or Me.contentAlignment_0 = ContentAlignment.MiddleRight Or Me.contentAlignment_0 = ContentAlignment.TopRight) Then
                    Me.stringFormat_0.Alignment = StringAlignment.Far
                End If
                If (Me.contentAlignment_0 = ContentAlignment.TopLeft Or Me.contentAlignment_0 = ContentAlignment.TopCenter Or Me.contentAlignment_0 = ContentAlignment.TopRight) Then
                    Me.stringFormat_0.LineAlignment = StringAlignment.Near
                ElseIf (Me.contentAlignment_0 = ContentAlignment.MiddleLeft Or Me.contentAlignment_0 = ContentAlignment.MiddleCenter Or Me.contentAlignment_0 = ContentAlignment.MiddleRight) Then
                    Me.stringFormat_0.LineAlignment = StringAlignment.Center
                ElseIf (Me.contentAlignment_0 = ContentAlignment.BottomLeft Or Me.contentAlignment_0 = ContentAlignment.BottomCenter Or Me.contentAlignment_0 = ContentAlignment.BottomRight) Then
                    Me.stringFormat_0.LineAlignment = StringAlignment.Far
                End If
                MyBase.Invalidate()
            End If
        End Set
    End Property

    Public Property ValueScaleFactor As Decimal
        Get
            Return Me.decimal_0
        End Get
        Set(ByVal value As Decimal)
            Me.decimal_0 = value
        End Set
    End Property

    Public Property ValueSelect1 As Boolean
        Get
            Return Me.bool_0
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_0) Then
                Me.bool_0 = value
                MyBase.Invalidate()
                Me.OnValueSelect1Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Property ValueSelect2 As Boolean
        Get
            Return Me.bool_1
        End Get
        Set(ByVal value As Boolean)
            If (value <> Me.bool_1) Then
                Me.bool_1 = value
                MyBase.Invalidate()
                Me.OnValueSelect2Changed(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me.rectangle_0 = New Rectangle()
        Me.stringFormat_0 = New StringFormat()
        Me.pictureBoxSizeMode_0 = PictureBoxSizeMode.StretchImage
        Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.NoRotation
        Me.contentAlignment_0 = ContentAlignment.TopCenter
        Me.string_0 = String.Empty
        Me.font_0 = New System.Drawing.Font("Arial", 10!, FontStyle.Regular, GraphicsUnit.Point)
        Me.decimal_0 = Decimal.One
        Me.outputTypes_0 = GraphicIndicator.OutputTypes.MomentarySet
        Me.stringFormat_1 = New StringFormat()
        MyBase.SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.ForeColor = Color.WhiteSmoke
        Me.stringFormat_0.Alignment = StringAlignment.Center
        Me.stringFormat_0.LineAlignment = StringAlignment.Center
        Me.stringFormat_1.Alignment = StringAlignment.Center
        Me.stringFormat_1.LineAlignment = StringAlignment.Near
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If (Me.stringFormat_1 IsNot Nothing) Then
            Me.stringFormat_1.Dispose()
        End If
        If (Me.stringFormat_0 IsNot Nothing) Then
            Me.stringFormat_0.Dispose()
        End If
        If (Me.font_0 IsNot Nothing) Then
            Me.font_0.Dispose()
        End If
        If (Me.bitmap_1 IsNot Nothing) Then
            Me.bitmap_1.Dispose()
        End If
        If (Me.bitmap_2 IsNot Nothing) Then
            Me.bitmap_2.Dispose()
        End If
        If (Me.bitmap_3 IsNot Nothing) Then
            Me.bitmap_3.Dispose()
        End If
        Me.TextBrush.Dispose()
    End Sub

    Private Sub method_0()
        If (MyBase.Width > 0 And MyBase.Height > 0) Then
            If (Me.bitmap_4 IsNot Nothing) Then
                Me.bitmap_1 = New Bitmap(MyBase.Width, MyBase.Height)
                Using graphic As Graphics = Graphics.FromImage(Me.bitmap_1)
                    Using matrix As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                        If (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate90) Then
                            matrix.Translate(CSng(Me.bitmap_4.Height), 0!)
                            matrix.Rotate(90!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_4.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_4.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate180) Then
                            matrix.Translate(CSng((0 - Me.bitmap_4.Width)), CSng((0 - Me.bitmap_4.Height)))
                            matrix.Rotate(180!, MatrixOrder.Append)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_4.Width)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_4.Height)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate270) Then
                            matrix.Translate(0!, CSng(Me.bitmap_4.Width))
                            matrix.Rotate(270!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_4.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_4.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                            matrix.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_4.Width)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_4.Height)), MatrixOrder.Append)
                        End If
                        graphic.Transform = matrix
                        graphic.DrawImage(Me.bitmap_4, 0, 0)
                    End Using
                End Using
            End If
            If (Me.bitmap_5 IsNot Nothing) Then
                Me.bitmap_2 = New Bitmap(MyBase.Width, MyBase.Height)
                Using graphic1 As Graphics = Graphics.FromImage(Me.bitmap_2)
                    Using matrix1 As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                        If (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate90) Then
                            matrix1.Translate(CSng(Me.bitmap_5.Height), 0!)
                            matrix1.Rotate(90!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix1.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_5.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_5.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate180) Then
                            matrix1.Translate(CSng((0 - Me.bitmap_5.Width)), CSng((0 - Me.bitmap_5.Height)))
                            matrix1.Rotate(180!, MatrixOrder.Append)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix1.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_5.Width)), CSng(Convert.ToInt32(CDbl(MyBase.Height) / CDbl(Me.bitmap_5.Height))), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate270) Then
                            matrix1.Translate(0!, CSng(Me.bitmap_5.Width))
                            matrix1.Rotate(270!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix1.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_5.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_5.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                            matrix1.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_5.Width)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_5.Height)), MatrixOrder.Append)
                        End If
                        graphic1.Transform = matrix1
                        graphic1.DrawImage(Me.bitmap_5, 0, 0)
                    End Using
                End Using
            End If
            If (Me.bitmap_6 IsNot Nothing) Then
                Me.bitmap_3 = New Bitmap(MyBase.Width, MyBase.Height)
                Using graphic2 As Graphics = Graphics.FromImage(Me.bitmap_3)
                    Using matrix2 As System.Drawing.Drawing2D.Matrix = New System.Drawing.Drawing2D.Matrix()
                        If (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate90) Then
                            matrix2.Translate(CSng(Me.bitmap_6.Height), 0!)
                            matrix2.Rotate(90!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix2.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_6.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_6.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate180) Then
                            matrix2.Translate(CSng((0 - Me.bitmap_6.Width)), CSng((0 - Me.bitmap_6.Height)))
                            matrix2.Rotate(180!, MatrixOrder.Append)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix2.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_6.Width)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_6.Height)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.rotationAngleEnum_0 = GraphicIndicator.RotationAngleEnum.Rotate270) Then
                            matrix2.Translate(0!, CSng(Me.bitmap_6.Width))
                            matrix2.Rotate(270!, MatrixOrder.Prepend)
                            If (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                                matrix2.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_6.Height)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_6.Width)), MatrixOrder.Append)
                            End If
                        ElseIf (Me.SizeMode = PictureBoxSizeMode.StretchImage) Then
                            matrix2.Scale(Convert.ToSingle(CDbl(MyBase.Width) / CDbl(Me.bitmap_6.Width)), Convert.ToSingle(CDbl(MyBase.Height) / CDbl(Me.bitmap_6.Height)), MatrixOrder.Append)
                        End If
                        graphic2.Transform = matrix2
                        graphic2.DrawImage(Me.bitmap_6, 0, 0)
                    End Using
                End Using
            End If
            MyBase.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If (Me.TextBrush Is Nothing) Then
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        If (Me.TextBrush IsNot Nothing) Then
            Me.TextBrush.Color = MyBase.ForeColor
        Else
            Me.TextBrush = New SolidBrush(MyBase.ForeColor)
        End If
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal painte As PaintEventArgs)
        If (Me.TextBrush IsNot Nothing) Then
            Dim graphics As System.Drawing.Graphics = painte.Graphics
            If (Me.bool_0) Then
                If (Me.bitmap_2 IsNot Nothing) Then
                    graphics.DrawImage(Me.bitmap_2, 0, 0)
                End If
            ElseIf (Me.bool_1) Then
                If (Me.bitmap_3 IsNot Nothing) Then
                    graphics.DrawImage(Me.bitmap_3, 0, 0)
                End If
            ElseIf (Me.bitmap_1 IsNot Nothing) Then
                graphics.DrawImage(Me.bitmap_1, 0, 0)
            End If
            If (Not String.IsNullOrEmpty(MyBase.Text)) Then
                graphics.DrawString(MyBase.Text, MyBase.Font, Me.TextBrush, MyBase.ClientRectangle, Me.stringFormat_0)
            End If
            If (Not String.IsNullOrEmpty(Me.string_0)) Then
                graphics.DrawString(Me.string_0, Me.font_0, Me.TextBrush, Me.rectangle_1, Me.stringFormat_1)
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        MyBase.OnPaintBackground(pevent)
        If (Me.BackColor = Color.Transparent AndAlso MyBase.Parent IsNot Nothing) Then
            Dim childIndex As Integer = MyBase.Parent.Controls.GetChildIndex(Me)
            Dim count As Integer = MyBase.Parent.Controls.Count - 1
            Dim num As Integer = childIndex + 1
            For i As Integer = count To num Step -1
                Dim item As Control = MyBase.Parent.Controls(i)
                If (If(Not item.Bounds.IntersectsWith(MyBase.Bounds), False, item.Visible)) Then
                    Using bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(item.Width, item.Height, pevent.Graphics)
                        item.DrawToBitmap(bitmap, item.ClientRectangle)
                        pevent.Graphics.DrawImageUnscaled(bitmap, item.Left - MyBase.Left, item.Top - MyBase.Top)
                    End Using
                End If
            Next

        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.rectangle_1 = New Rectangle(0, Convert.ToInt32(CDbl(MyBase.Height) / 1.9), MyBase.Width, Convert.ToInt32(CDbl(MyBase.Height) / 2.1))
        Me.method_0()
    End Sub

    Protected Overridable Sub OnValueSelect1Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelect1Changed(Me, e)
    End Sub

    Protected Overridable Sub OnValueSelect2Changed(ByVal e As EventArgs)
        RaiseEvent ValueSelect2Changed(Me, e)
    End Sub

    Private Sub timer_0_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Me.ValueSelect1 = Not Me.ValueSelect1
    End Sub

    Public Event ValueSelect1Changed As EventHandler


    Public Event ValueSelect2Changed As EventHandler


    Public Enum OutputTypes
        MomentarySet
        MomentaryReset
        SetTrue
        SetFalse
        Toggle
    End Enum

    Public Enum RotationAngleEnum
        NoRotation
        Rotate90
        Rotate180
        Rotate270
    End Enum
End Class
