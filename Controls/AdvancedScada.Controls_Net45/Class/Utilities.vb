Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Public NotInheritable Class UtilitiesControls
    Private Sub New()
        MyBase.New()
    End Sub

    Public Shared Function BooleanFromString(ByVal value As String) As Boolean
        Dim flag As Boolean
        If (value Is Nothing) Then
            value = String.Empty
        End If
        If (String.Compare(value, "0") = 0) Then
            flag = False
        ElseIf (String.Compare(value.ToLower(), "true") = 0) Then
            flag = True
        ElseIf (String.Compare(value.ToLower(), "false") <> 0) Then
            Try
                flag = Double.Parse(value) <> 0
            Catch formatException As System.FormatException
                ProjectData.SetProjectError(formatException)
                Throw New InvalidCastException(String.Concat("Failed to cast ", value, " to Boolean"), formatException)
            End Try
        Else
            flag = False
        End If
        Return flag
    End Function



    Friend Shared Function smethod_2(ByVal string_0 As String, ByVal string_1 As String) As Boolean
        Dim length As Boolean
        Try
            length = CInt(Directory.GetFiles(string_0, string_1, SearchOption.AllDirectories).Length) > 0
            Return length
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Debug.WriteLine(exception.Message)
            ProjectData.ClearProjectError()
        End Try
        length = False
        Return length
    End Function

    Friend Shared Sub smethod_3(ByVal graphics_0 As Graphics, ByVal image_0 As Image, ByVal color_0 As Color, ByVal imageLayout_0 As ImageLayout, ByVal rectangle_0 As System.Drawing.Rectangle, ByVal rectangle_1 As System.Drawing.Rectangle, ByVal point_0 As Point, ByVal rightToLeft_0 As RightToLeft)
        If (graphics_0 Is Nothing) Then
            Throw New ArgumentNullException("g")
        End If
        If (imageLayout_0 <> ImageLayout.Tile) Then
            Dim x As System.Drawing.Rectangle = UtilitiesControls.smethod_4(rectangle_1, image_0, imageLayout_0)
            If (If(rightToLeft_0 <> RightToLeft.Yes, False, imageLayout_0 = ImageLayout.None)) Then
                x.X = x.X + (rectangle_1.Width - x.Width)
            End If
            Using solidBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(color_0)
                graphics_0.FillRectangle(solidBrush, rectangle_1)
            End Using
            If (rectangle_1.Contains(x)) Then
                Dim imageAttribute As ImageAttributes = New ImageAttributes()
                imageAttribute.SetWrapMode(WrapMode.TileFlipXY)
                graphics_0.DrawImage(image_0, x, 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, imageAttribute)
                imageAttribute.Dispose()
            ElseIf (If(imageLayout_0 = ImageLayout.Stretch, True, imageLayout_0 = ImageLayout.Zoom)) Then
                x.X = point_0.X
                x.Y = point_0.Y
                x.Intersect(rectangle_1)
                graphics_0.DrawImage(image_0, x)
            ElseIf (imageLayout_0 <> ImageLayout.None) Then
                Dim rectangle As System.Drawing.Rectangle = x
                rectangle.Intersect(rectangle_1)
                Dim rectangle1 As System.Drawing.Rectangle = New System.Drawing.Rectangle(New Point(rectangle.X - x.X, rectangle.Y - x.Y), rectangle.Size)
                x.X = x.X + point_0.X
                x.Y = x.Y + point_0.Y
                graphics_0.DrawImage(image_0, x)
            Else
                x.Offset(rectangle_1.Location)
                Dim rectangle2 As System.Drawing.Rectangle = x
                rectangle2.Intersect(rectangle_1)
                Dim rectangle3 As System.Drawing.Rectangle = New System.Drawing.Rectangle(Point.Empty, rectangle2.Size)
                graphics_0.DrawImage(image_0, rectangle2, rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, GraphicsUnit.Pixel)
            End If
        Else
            Using textureBrush As System.Drawing.TextureBrush = New System.Drawing.TextureBrush(image_0, WrapMode.Tile)
                If (point_0 <> Point.Empty) Then
                    Dim transform As Matrix = textureBrush.Transform
                    transform.Translate(CSng(point_0.X), CSng(point_0.Y))
                    textureBrush.Transform = transform
                End If
                graphics_0.FillRectangle(textureBrush, rectangle_1)
            End Using
        End If
    End Sub

    Friend Shared Function smethod_4(ByVal rectangle_0 As Rectangle, ByVal image_0 As Image, ByVal imageLayout_0 As ImageLayout) As Rectangle
        Dim rectangle0 As Rectangle = rectangle_0
        If (image_0 IsNot Nothing) Then
            Select Case imageLayout_0
                Case ImageLayout.None
                    rectangle0.Size = image_0.Size
                    Exit Select
                Case ImageLayout.Center
                    rectangle0.Size = image_0.Size
                    Dim size As System.Drawing.Size = rectangle_0.Size
                    rectangle0.X = CInt(Math.Round(CDbl((size.Width - rectangle0.Width)) / 2))
                    rectangle0.Y = CInt(Math.Round(CDbl((size.Height - rectangle0.Height)) / 2))
                    Exit Select
                Case ImageLayout.Stretch
                    rectangle0.Size = rectangle_0.Size
                    Exit Select
                Case ImageLayout.Zoom
                    Dim size1 As System.Drawing.Size = image_0.Size
                    Dim width As Single = CSng(rectangle_0.Width) / CSng(size1.Width)
                    Dim height As Single = CSng(rectangle_0.Height) / CSng(size1.Height)
                    If (width >= height) Then
                        rectangle0.Height = rectangle_0.Height
                        rectangle0.Width = CInt(Math.Round(CDbl((CSng(size1.Width) * height)) + 0.5))
                        'rectangle_0.X >= 0
                        Exit Select
                    Else
                        rectangle0.Width = rectangle_0.Width
                        rectangle0.Height = CInt(Math.Round(CDbl((CSng(size1.Height) * width)) + 0.5))
                        If (rectangle_0.Y < 0) Then
                            Exit Select
                        End If
                        Exit Select
                    End If
            End Select
        End If
        Return rectangle0
    End Function
End Class
