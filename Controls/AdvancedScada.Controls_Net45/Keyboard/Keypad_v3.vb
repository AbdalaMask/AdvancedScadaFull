Imports Microsoft.VisualBasic.CompilerServices
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Keypad_v3
    Inherits System.Windows.Forms.Form
    Implements IKeyboard

    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer
    Public PasscodeVerify As Boolean
    Private Declare Function HideCaret Lib "user32.dll" (ByVal hwnd As Int32) As Int32

    Public Event ButtonClick(ByVal sender As Object, ByVal e As KeypadEventArgs) Implements IKeyboard.ButtonClick

#Region "Constructor"

    Private components As System.ComponentModel.IContainer

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button0 As Button
    Friend WithEvents buDecimal As Button
    Friend WithEvents buBksp As Button
    Friend WithEvents buClear As Button
    Friend WithEvents buCancel As Button
    Friend WithEvents buAccept As Button
    Friend WithEvents buNegative As Button
    Friend WithEvents lblMaxValue As Label
    Friend WithEvents lblMinValue As Label
    Friend WithEvents lblCurrentValue As Label
    Friend WithEvents txtValue As TextBox

    Public Sub New()

        '* reduce the flicker
        SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        InitializeComponent()
    End Sub

    Public Sub New(m_KeypadWidth As Integer)
        Me.m_KeypadWidth = m_KeypadWidth
        '* reduce the flicker
        SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button0 = New System.Windows.Forms.Button()
        Me.buDecimal = New System.Windows.Forms.Button()
        Me.buBksp = New System.Windows.Forms.Button()
        Me.buClear = New System.Windows.Forms.Button()
        Me.buCancel = New System.Windows.Forms.Button()
        Me.buAccept = New System.Windows.Forms.Button()
        Me.buNegative = New System.Windows.Forms.Button()
        Me.lblMaxValue = New System.Windows.Forms.Label()
        Me.lblMinValue = New System.Windows.Forms.Label()
        Me.lblCurrentValue = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Transparent
        Me.Button1.Location = New System.Drawing.Point(12, 217)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 55)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Transparent
        Me.Button2.Location = New System.Drawing.Point(76, 217)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 55)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Transparent
        Me.Button3.Location = New System.Drawing.Point(140, 217)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(55, 55)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.Transparent
        Me.Button4.Location = New System.Drawing.Point(12, 151)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 55)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.Transparent
        Me.Button5.Location = New System.Drawing.Point(76, 151)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(55, 55)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Transparent
        Me.Button6.Location = New System.Drawing.Point(140, 151)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(55, 55)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.Transparent
        Me.Button7.Location = New System.Drawing.Point(12, 85)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(55, 55)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.Transparent
        Me.Button8.Location = New System.Drawing.Point(76, 85)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(55, 55)
        Me.Button8.TabIndex = 7
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button9.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button9.FlatAppearance.BorderSize = 0
        Me.Button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.Transparent
        Me.Button9.Location = New System.Drawing.Point(140, 85)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(55, 55)
        Me.Button9.TabIndex = 8
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button0
        '
        Me.Button0.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.Button0.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Button0.FlatAppearance.BorderSize = 0
        Me.Button0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.Button0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Button0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button0.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button0.ForeColor = System.Drawing.Color.Transparent
        Me.Button0.Location = New System.Drawing.Point(76, 283)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(55, 55)
        Me.Button0.TabIndex = 9
        Me.Button0.Text = "0"
        Me.Button0.UseVisualStyleBackColor = False
        '
        'buDecimal
        '
        Me.buDecimal.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buDecimal.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buDecimal.FlatAppearance.BorderSize = 0
        Me.buDecimal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buDecimal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buDecimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buDecimal.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buDecimal.ForeColor = System.Drawing.Color.Transparent
        Me.buDecimal.Location = New System.Drawing.Point(140, 283)
        Me.buDecimal.Name = "buDecimal"
        Me.buDecimal.Size = New System.Drawing.Size(55, 55)
        Me.buDecimal.TabIndex = 10
        Me.buDecimal.Text = "."
        Me.buDecimal.UseVisualStyleBackColor = False
        Me.buDecimal.Enabled = False
        '
        'buBksp
        '
        Me.buBksp.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buBksp.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buBksp.FlatAppearance.BorderSize = 0
        Me.buBksp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buBksp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buBksp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buBksp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buBksp.ForeColor = System.Drawing.Color.Transparent
        Me.buBksp.Location = New System.Drawing.Point(229, 85)
        Me.buBksp.Name = "buBksp"
        Me.buBksp.Size = New System.Drawing.Size(110, 55)
        Me.buBksp.TabIndex = 11
        Me.buBksp.Text = "Backspace"
        Me.buBksp.UseVisualStyleBackColor = False
        '
        'buClear
        '
        Me.buClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buClear.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buClear.FlatAppearance.BorderSize = 0
        Me.buClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buClear.ForeColor = System.Drawing.Color.Transparent
        Me.buClear.Location = New System.Drawing.Point(229, 151)
        Me.buClear.Name = "buClear"
        Me.buClear.Size = New System.Drawing.Size(110, 55)
        Me.buClear.TabIndex = 12
        Me.buClear.Text = "Clear"
        Me.buClear.UseVisualStyleBackColor = False
        '
        'buCancel
        '
        Me.buCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buCancel.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buCancel.FlatAppearance.BorderSize = 0
        Me.buCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buCancel.ForeColor = System.Drawing.Color.Transparent
        Me.buCancel.Location = New System.Drawing.Point(229, 217)
        Me.buCancel.Name = "buCancel"
        Me.buCancel.Size = New System.Drawing.Size(110, 55)
        Me.buCancel.TabIndex = 13
        Me.buCancel.Text = "Quit"
        Me.buCancel.UseVisualStyleBackColor = False
        '
        'buAccept
        '
        Me.buAccept.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buAccept.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buAccept.FlatAppearance.BorderSize = 0
        Me.buAccept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buAccept.ForeColor = System.Drawing.Color.Transparent
        Me.buAccept.Location = New System.Drawing.Point(229, 283)
        Me.buAccept.Name = "buAccept"
        Me.buAccept.Size = New System.Drawing.Size(110, 55)
        Me.buAccept.TabIndex = 14
        Me.buAccept.Text = "Enter"
        Me.buAccept.UseVisualStyleBackColor = False
        '
        'buNegative
        '
        Me.buNegative.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.buNegative.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.buNegative.FlatAppearance.BorderSize = 0
        Me.buNegative.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.buNegative.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.buNegative.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buNegative.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buNegative.ForeColor = System.Drawing.Color.Transparent
        Me.buNegative.Location = New System.Drawing.Point(12, 283)
        Me.buNegative.Name = "buNegative"
        Me.buNegative.Size = New System.Drawing.Size(55, 55)
        Me.buNegative.TabIndex = 15
        Me.buNegative.Text = "-"
        Me.buNegative.UseVisualStyleBackColor = False
        Me.buNegative.Enabled = False
        '
        'lblMaxValue
        '
        Me.lblMaxValue.AutoSize = True
        Me.lblMaxValue.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblMaxValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxValue.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblMaxValue.Location = New System.Drawing.Point(172, 65)
        Me.lblMaxValue.Name = "lblMaxValue"
        Me.lblMaxValue.Size = New System.Drawing.Size(30, 13)
        Me.lblMaxValue.TabIndex = 16
        Me.lblMaxValue.Text = "Max:"
        '
        'lblMinValue
        '
        Me.lblMinValue.AutoSize = True
        Me.lblMinValue.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblMinValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinValue.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblMinValue.Location = New System.Drawing.Point(12, 65)
        Me.lblMinValue.Name = "lblMinValue"
        Me.lblMinValue.Size = New System.Drawing.Size(27, 13)
        Me.lblMinValue.TabIndex = 18
        Me.lblMinValue.Text = "Min:"
        '
        'lblCurrentValue
        '
        Me.lblCurrentValue.AutoSize = True
        Me.lblCurrentValue.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblCurrentValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentValue.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblCurrentValue.Location = New System.Drawing.Point(12, 10)
        Me.lblCurrentValue.Name = "lblCurrentValue"
        Me.lblCurrentValue.Size = New System.Drawing.Size(74, 13)
        Me.lblCurrentValue.TabIndex = 19
        Me.lblCurrentValue.Text = "Current Value:"
        '
        'txtValue
        '
        Me.txtValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.Location = New System.Drawing.Point(12, 30)
        Me.txtValue.MaxLength = 16
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(327, 26)
        Me.txtValue.TabIndex = 20
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Keypad_v3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(353, 350)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.lblCurrentValue)
        Me.Controls.Add(Me.lblMinValue)
        Me.Controls.Add(Me.lblMaxValue)
        Me.Controls.Add(Me.buNegative)
        Me.Controls.Add(Me.buAccept)
        Me.Controls.Add(Me.buCancel)
        Me.Controls.Add(Me.buClear)
        Me.Controls.Add(Me.buBksp)
        Me.Controls.Add(Me.buDecimal)
        Me.Controls.Add(Me.Button0)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Keypad_v3"
        Me.ShowInTaskbar = False
        Me.Text = "Keypad"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

#Region "Properties"

    Private m_MinValue As Decimal?
    ''' <summary>
    ''' Minimum value allowed to be entered from Keypad
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Minimum value allowed to be entered from Keypad")>
    Public Property MinValue As Decimal?
        Get
            Return m_MinValue
        End Get
        Set(ByVal value As Decimal?)
            m_MinValue = value
            lblMinValue.Text = "Min: " + Conversions.ToString(value)
            Invalidate()
        End Set
    End Property

    Private m_MaxValue As Decimal?
    ''' <summary>
    ''' Maximum value allowed to be entered from Keypad
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Maximum value allowed to be entered from Keypad")>
    Public Property MaxValue As Decimal?
        Get
            Return m_MaxValue
        End Get
        Set(ByVal value As Decimal?)
            m_MaxValue = value
            lblMaxValue.Text = "Max: " + Conversions.ToString(value)
            Invalidate()
        End Set
    End Property

    Private m_AllowNegative As Boolean
    ''' <summary>
    ''' Allow a negative value to be entered from Keypad
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Allow a negative value to be entered from Keypad")>
    Public Property AllowNegative As Boolean
        Get
            Return m_AllowNegative
        End Get
        Set(ByVal value As Boolean)
            m_AllowNegative = value
            buNegative.Enabled = m_AllowNegative
            Invalidate()
        End Set
    End Property

    Private m_AllowDecimal As Boolean
    ''' <summary>
    ''' Allow a decimal value to be entered from Keypad
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Allow a decimal value to be entered from Keypad")>
    Public Property AllowDecimal As Boolean
        Get
            Return m_AllowDecimal
        End Get
        Set(ByVal value As Boolean)
            m_AllowDecimal = value
            buDecimal.Enabled = m_AllowDecimal
            Invalidate()
        End Set
    End Property

    Private m_Value As String = String.Empty
    ''' <summary>
    ''' Keypad current entered value
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Keypad current entered value")>
    Public Property Value As String Implements IKeyboard.Value
        Get
            Return m_Value
        End Get
        Set(value As String)
            m_Value = value
            If Operators.CompareString(value, String.Empty, False) = 0 Then
                txtValue.Text = String.Empty
                buAccept.Enabled = False
            Else
                txtValue.Text = value
                buAccept.Enabled = True
            End If
            Me.Invalidate()
        End Set
    End Property

    Private m_CurrentValue As String
    Private m_KeypadWidth As Integer

    Public Property CurrentValue As String Implements IKeyboard.CurrentValue
        Get
            Return m_CurrentValue
        End Get
        Set(value As String)
            m_CurrentValue = value
        End Set
    End Property

    Public Overrides Property Text() As String Implements IKeyboard.Text
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            lblCurrentValue.Text = MyBase.Text
            Me.Invalidate()
        End Set
    End Property

    Public Overrides Property Font As System.Drawing.Font Implements IKeyboard.Font
        Get
            Return MyBase.Font
        End Get
        Set(value As System.Drawing.Font)
            MyBase.Font = value
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property Location() As System.Drawing.Point Implements IKeyboard.Location
        Get
            Return MyBase.Location
        End Get
        Set(ByVal value As System.Drawing.Point)
            MyBase.Location = value
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property Visible() As Boolean Implements IKeyboard.Visible
        Get
            Return MyBase.Visible
        End Get
        Set(ByVal value As Boolean)
            MyBase.Visible = value
            Me.Invalidate()
        End Set
    End Property

    Public Overrides Property ForeColor As System.Drawing.Color Implements IKeyboard.ForeColor
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As System.Drawing.Color)
            MyBase.ForeColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property TopMost As Boolean Implements IKeyboard.TopMost
        Get
            Return MyBase.TopMost
        End Get
        Set(value As Boolean)
            MyBase.TopMost = value
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property StartPosition As FormStartPosition Implements IKeyboard.StartPosition
        Get
            Return MyBase.StartPosition
        End Get
        Set(value As FormStartPosition)
            MyBase.StartPosition = value
        End Set
    End Property

    Public Property PassWordChar As Boolean Implements IKeyboard.PassWordChar
        Get
            Return txtValue.UseSystemPasswordChar
        End Get
        Set(value As Boolean)
            txtValue.UseSystemPasswordChar = value
            Me.Invalidate()
        End Set
    End Property

#End Region

#Region "Events"

    Private Sub Keypad_TDSA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtValue.ForeColor = Color.Black
        txtValue.BackColor = Color.WhiteSmoke

        If m_MinValue Is Nothing And Not AllowNegative Then
            MinValue = 0
        ElseIf m_MinValue Is Nothing And AllowNegative Then
            MinValue = -2147483648
        End If

        If m_MaxValue Is Nothing Then
            MaxValue = 2147483647
        End If

        If m_MinValue = m_MaxValue And Not AllowNegative Then
            MinValue = 0
            MaxValue = 2147483647
        ElseIf m_MinValue = m_MaxValue And AllowNegative Then
            MinValue = -2147483648
            MaxValue = 2147483647
        End If

        If m_MinValue < 0 And Not AllowNegative Then
            AllowNegative = True
        End If

        If m_Value = String.Empty Then
            buAccept.Enabled = False
        End If

        buClear.PerformClick()

    End Sub

    Public Sub mouseEnterHandler(sender As Object, e As EventArgs) Handles Button0.MouseEnter, Button1.MouseEnter, Button2.MouseEnter,
                                                                           Button3.MouseEnter, Button4.MouseEnter, Button5.MouseEnter,
                                                                           Button6.MouseEnter, Button7.MouseEnter, Button8.MouseEnter,
                                                                           Button9.MouseEnter, buBksp.MouseEnter, buClear.MouseEnter,
                                                                           buCancel.MouseEnter, buAccept.MouseEnter, buNegative.MouseEnter,
                                                                           buDecimal.MouseEnter
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.Black
        control.BackColor = Color.White
    End Sub

    Public Sub mouseLeaveHandler(sender As Object, e As EventArgs) Handles Button0.MouseLeave, Button1.MouseLeave, Button2.MouseLeave,
                                                                           Button3.MouseLeave, Button4.MouseLeave, Button5.MouseLeave,
                                                                           Button6.MouseLeave, Button7.MouseLeave, Button8.MouseLeave,
                                                                           Button9.MouseLeave, buBksp.MouseLeave, buClear.MouseLeave,
                                                                           buCancel.MouseLeave, buAccept.MouseLeave, buNegative.MouseLeave,
                                                                           buDecimal.MouseLeave
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.White
        control.BackColor = Color.FromArgb(65, 65, 65)
    End Sub

    Public Sub mouseDownHandler(sender As Object, e As EventArgs) Handles Button0.MouseDown, Button1.MouseDown, Button2.MouseDown,
                                                                           Button3.MouseDown, Button4.MouseDown, Button5.MouseDown,
                                                                           Button6.MouseDown, Button7.MouseDown, Button8.MouseDown,
                                                                           Button9.MouseDown, buBksp.MouseDown, buClear.MouseDown,
                                                                           buCancel.MouseDown, buAccept.MouseDown, buNegative.MouseDown,
                                                                           buDecimal.MouseDown
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.White
        control.BackColor = Color.Red
        control.BackColor = Color.FromArgb(0, 154, 187)
    End Sub

    Public Sub NumericMouseUp(sender As Object, e As EventArgs) Handles Button0.MouseUp, Button1.MouseUp, Button2.MouseUp,
                                                                           Button3.MouseUp, Button4.MouseUp, Button5.MouseUp,
                                                                           Button6.MouseUp, Button7.MouseUp, Button8.MouseUp,
                                                                           Button9.MouseUp

        If txtValue.TextLength >= 28 Then
            Return
        End If

        If sender.Text <> 0 And Operators.CompareString(Value, "0", False) = 0 Then
            Value = String.Empty
        End If

        If sender.Text = 0 And Operators.CompareString(Value, "0", False) = 0 Then
            Return
        End If

        lblMinValue.Text = "Min: " + Conversions.ToString(m_MinValue)
        lblMaxValue.Text = "Max: " + Conversions.ToString(m_MaxValue)

        Dim data As Decimal
        Decimal.TryParse(Value, data)
        Value = Value + sender.text
        Check_Limits()

        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.Black
        control.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Private Sub buAccept_MouseUp(sender As Object, e As MouseEventArgs) Handles buAccept.MouseUp
        If Not PasscodeVerify Then
            Me.DialogResult = DialogResult.OK
            RaiseEvent ButtonClick(Me, New KeypadEventArgs("Enter"))
        End If
        buAccept.ForeColor = Color.Black
        buAccept.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Private Sub buCancel_MouseUp(sender As Object, e As MouseEventArgs) Handles buCancel.MouseUp
        buCancel.ForeColor = Color.Black
        buCancel.BackColor = Color.FromArgb(229, 229, 229)
        RaiseEvent ButtonClick(Me, New KeypadEventArgs("Quit"))
        'Me.Close()
        Me.Visible = False
    End Sub

    Private Sub buClear_MouseUp(sender As Object, e As MouseEventArgs) Handles buClear.MouseUp
        If Value <> "Enter Passcode" AndAlso Value <> "Incorrect Passcode" Then
            Value = String.Empty
            txtValue.BackColor = Color.WhiteSmoke
            txtValue.ForeColor = Color.Black
            lblMinValue.ForeColor = Color.WhiteSmoke
            lblMaxValue.ForeColor = Color.WhiteSmoke
        End If
        buClear.ForeColor = Color.Black
        buClear.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Private Sub buBksp_MouseUp(sender As Object, e As MouseEventArgs) Handles buBksp.MouseUp
        If Value <> "Enter Passcode" AndAlso Value <> "Incorrect Passcode" And Value IsNot Nothing Then
            Value = If(Value.Length <> 0, Value.Substring(0, Value.Length - 1), String.Empty)
            Check_Limits()
        End If
        buBksp.ForeColor = Color.Black
        buBksp.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Private Sub buDecimal_MouseUp(sender As Object, e As MouseEventArgs) Handles buDecimal.MouseUp
        If Not Value.Contains(".") Then
            Value = Value & Convert.ToString(".")
        End If
        buDecimal.ForeColor = Color.Black
        buDecimal.BackColor = Color.FromArgb(229, 229, 229)
        Check_Limits()
    End Sub

    Private Sub buNegative_MouseUp(sender As Object, e As MouseEventArgs) Handles buNegative.MouseUp
        Value = If(Value.Contains("-"), Value.Substring(1, Value.Length - 1), Convert.ToString("-") & Value)
        buNegative.ForeColor = Color.Black
        buNegative.BackColor = Color.FromArgb(229, 229, 229)
        Check_Limits()
    End Sub

    Private Sub Keypad_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Keypad_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If Not drag Then
            Return
        End If
        Dim position As Point = Cursor.Position
        Me.Left = position.X - mouseX
        position = Cursor.Position
        Me.Top = position.Y - mouseY
    End Sub

    Private Sub Keypad_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub

    Private Sub Check_Limits()

        If IsNumeric(Value) Then
            If Convert.ToDecimal(Value) < m_MinValue Then
                lblMinValue.ForeColor = Color.Red
            Else
                lblMinValue.ForeColor = Color.WhiteSmoke
            End If
        Else
            lblMinValue.ForeColor = Color.WhiteSmoke
        End If

        If IsNumeric(Value) Then
            If Convert.ToDecimal(Value) > m_MaxValue Then
                lblMaxValue.ForeColor = Color.Red
            Else
                lblMaxValue.ForeColor = Color.WhiteSmoke
            End If
        Else
            lblMaxValue.ForeColor = Color.WhiteSmoke
        End If

        If Not PasscodeVerify Then
            If IsNumeric(Value) Then
                If Convert.ToDecimal(Value) >= m_MinValue AndAlso Convert.ToDecimal(Value) <= m_MaxValue Then
                    txtValue.ForeColor = Color.Black
                    txtValue.BackColor = Color.WhiteSmoke
                    buAccept.Enabled = True
                Else
                    txtValue.ForeColor = Color.White
                    txtValue.BackColor = Color.Red
                    buAccept.Enabled = False
                End If
            Else
                txtValue.ForeColor = Color.Black
                txtValue.BackColor = Color.WhiteSmoke
                buAccept.Enabled = False
            End If
        End If

    End Sub

    Private Sub txtValue_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValue.GotFocus

        HideCaret(txtValue.Handle.ToInt32)

    End Sub

#End Region

End Class