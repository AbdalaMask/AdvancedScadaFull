Imports System.Drawing
Imports System.Windows.Forms
Public Class AlphaKeyboard_v3
    Inherits System.Windows.Forms.Form
    Implements IKeyboard

    Private ShiftMemory As Boolean
    Private CapsMemory As Boolean

    Const WM_NCHITTEST As Integer = &H84
    Const HTCLIENT As Integer = &H1
    Const HTCAPTION As Integer = &H2

    Public Event ButtonClick(ByVal sender As Object, ByVal e As KeypadEventArgs) Implements IKeyboard.ButtonClick

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_NCHITTEST
                MyBase.WndProc(m)
                If m.Result = IntPtr.op_Explicit(HTCLIENT) Then m.Result = IntPtr.op_Explicit(HTCAPTION)
            Case Else
                MyBase.WndProc(m)
        End Select
    End Sub

#Region "Constructor"
    Private components As System.ComponentModel.IContainer

    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnSpace As Button
    Friend WithEvents btnDown As Button
    Friend WithEvents btnRight As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents btnLeft As Button
    Friend WithEvents btn28 As Button
    Friend WithEvents btn40 As Button
    Friend WithEvents btn41 As Button
    Friend WithEvents btn_M As Button
    Friend WithEvents btn_N As Button
    Friend WithEvents btn_B As Button
    Friend WithEvents btn_V As Button
    Friend WithEvents btn_C As Button
    Friend WithEvents btn_X As Button
    Friend WithEvents btn_Z As Button
    Friend WithEvents btnShiftL As Button
    Friend WithEvents btnEnter As Button
    Friend WithEvents btn29 As Button
    Friend WithEvents btn30 As Button
    Friend WithEvents btn_L As Button
    Friend WithEvents btn_K As Button
    Friend WithEvents btn_J As Button
    Friend WithEvents btn_H As Button
    Friend WithEvents btn_G As Button
    Friend WithEvents btn_F As Button
    Friend WithEvents btn_D As Button
    Friend WithEvents btn_S As Button
    Friend WithEvents btn_A As Button
    Friend WithEvents btnCaps As Button
    Friend WithEvents btn14 As Button
    Friend WithEvents btn15 As Button
    Friend WithEvents btn16 As Button
    Friend WithEvents btn_P As Button
    Friend WithEvents btn_O As Button
    Friend WithEvents btn_I As Button
    Friend WithEvents btn_U As Button
    Friend WithEvents btn_Y As Button
    Friend WithEvents btn_T As Button
    Friend WithEvents btn_R As Button
    Friend WithEvents btn_E As Button
    Friend WithEvents btn_W As Button
    Friend WithEvents btn_Q As Button
    Friend WithEvents btnTab As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents btn13 As Button
    Friend WithEvents btn12 As Button
    Friend WithEvents btn11 As Button
    Friend WithEvents btn10 As Button
    Friend WithEvents btn9 As Button
    Friend WithEvents btn8 As Button
    Friend WithEvents btn7 As Button
    Friend WithEvents btn6 As Button
    Friend WithEvents btn5 As Button
    Friend WithEvents btn4 As Button
    Friend WithEvents btn3 As Button
    Friend WithEvents btn2 As Button
    Friend WithEvents btn1 As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblCurrentValue As Label
    Friend WithEvents TextBox1 As TextBox

    Public Sub New()
        '* reduce the flicker
        SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or
                    System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or
                    System.Windows.Forms.ControlStyles.UserPaint Or
                    System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnSpace = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btn28 = New System.Windows.Forms.Button()
        Me.btn40 = New System.Windows.Forms.Button()
        Me.btn41 = New System.Windows.Forms.Button()
        Me.btn_M = New System.Windows.Forms.Button()
        Me.btn_N = New System.Windows.Forms.Button()
        Me.btn_B = New System.Windows.Forms.Button()
        Me.btn_V = New System.Windows.Forms.Button()
        Me.btn_C = New System.Windows.Forms.Button()
        Me.btn_X = New System.Windows.Forms.Button()
        Me.btn_Z = New System.Windows.Forms.Button()
        Me.btnShiftL = New System.Windows.Forms.Button()
        Me.btnEnter = New System.Windows.Forms.Button()
        Me.btn29 = New System.Windows.Forms.Button()
        Me.btn30 = New System.Windows.Forms.Button()
        Me.btn_L = New System.Windows.Forms.Button()
        Me.btn_K = New System.Windows.Forms.Button()
        Me.btn_J = New System.Windows.Forms.Button()
        Me.btn_H = New System.Windows.Forms.Button()
        Me.btn_G = New System.Windows.Forms.Button()
        Me.btn_F = New System.Windows.Forms.Button()
        Me.btn_D = New System.Windows.Forms.Button()
        Me.btn_S = New System.Windows.Forms.Button()
        Me.btn_A = New System.Windows.Forms.Button()
        Me.btnCaps = New System.Windows.Forms.Button()
        Me.btn14 = New System.Windows.Forms.Button()
        Me.btn15 = New System.Windows.Forms.Button()
        Me.btn16 = New System.Windows.Forms.Button()
        Me.btn_P = New System.Windows.Forms.Button()
        Me.btn_O = New System.Windows.Forms.Button()
        Me.btn_I = New System.Windows.Forms.Button()
        Me.btn_U = New System.Windows.Forms.Button()
        Me.btn_Y = New System.Windows.Forms.Button()
        Me.btn_T = New System.Windows.Forms.Button()
        Me.btn_R = New System.Windows.Forms.Button()
        Me.btn_E = New System.Windows.Forms.Button()
        Me.btn_W = New System.Windows.Forms.Button()
        Me.btn_Q = New System.Windows.Forms.Button()
        Me.btnTab = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btn13 = New System.Windows.Forms.Button()
        Me.btn12 = New System.Windows.Forms.Button()
        Me.btn11 = New System.Windows.Forms.Button()
        Me.btn10 = New System.Windows.Forms.Button()
        Me.btn9 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblCurrentValue = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblCurrentValue
        '
        Me.lblCurrentValue.AutoSize = True
        Me.lblCurrentValue.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblCurrentValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentValue.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblCurrentValue.Location = New System.Drawing.Point(21, 5)
        Me.lblCurrentValue.Name = "lblCurrentValue"
        Me.lblCurrentValue.Size = New System.Drawing.Size(74, 13)
        Me.lblCurrentValue.TabIndex = 19
        Me.lblCurrentValue.Text = "Current Value:"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnSelectAll.FlatAppearance.BorderSize = 0
        Me.btnSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.ForeColor = System.Drawing.Color.White
        Me.btnSelectAll.Location = New System.Drawing.Point(21, 270)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(91, 50)
        Me.btnSelectAll.TabIndex = 122
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = False
        '
        'btnSpace
        '
        Me.btnSpace.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnSpace.FlatAppearance.BorderSize = 0
        Me.btnSpace.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnSpace.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpace.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSpace.ForeColor = System.Drawing.Color.White
        Me.btnSpace.Location = New System.Drawing.Point(114, 270)
        Me.btnSpace.Name = "btnSpace"
        Me.btnSpace.Size = New System.Drawing.Size(470, 50)
        Me.btnSpace.TabIndex = 120
        Me.btnSpace.Text = "Space"
        Me.btnSpace.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDown.ForeColor = System.Drawing.Color.White
        Me.btnDown.Location = New System.Drawing.Point(638, 270)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(50, 50)
        Me.btnDown.TabIndex = 119
        Me.btnDown.Text = "↓"
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'btnRight
        '
        Me.btnRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRight.ForeColor = System.Drawing.Color.White
        Me.btnRight.Location = New System.Drawing.Point(690, 270)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(50, 50)
        Me.btnRight.TabIndex = 118
        Me.btnRight.Text = "→"
        Me.btnRight.UseVisualStyleBackColor = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.ForeColor = System.Drawing.Color.White
        Me.btnUp.Location = New System.Drawing.Point(742, 270)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(50, 50)
        Me.btnUp.TabIndex = 117
        Me.btnUp.Text = "↑"
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'btnLeft
        '
        Me.btnLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLeft.ForeColor = System.Drawing.Color.White
        Me.btnLeft.Location = New System.Drawing.Point(586, 270)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(50, 50)
        Me.btnLeft.TabIndex = 116
        Me.btnLeft.Text = "←"
        Me.btnLeft.UseVisualStyleBackColor = False
        '
        'btn28
        '
        Me.btn28.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn28.FlatAppearance.BorderSize = 0
        Me.btn28.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn28.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn28.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn28.ForeColor = System.Drawing.Color.White
        Me.btn28.Location = New System.Drawing.Point(608, 218)
        Me.btn28.Name = "btn28"
        Me.btn28.Size = New System.Drawing.Size(50, 50)
        Me.btn28.TabIndex = 114
        Me.btn28.Text = "?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "/"
        Me.btn28.UseVisualStyleBackColor = False
        '
        'btn40
        '
        Me.btn40.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn40.FlatAppearance.BorderSize = 0
        Me.btn40.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn40.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn40.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn40.ForeColor = System.Drawing.Color.White
        Me.btn40.Location = New System.Drawing.Point(556, 218)
        Me.btn40.Name = "btn40"
        Me.btn40.Size = New System.Drawing.Size(50, 50)
        Me.btn40.TabIndex = 113
        Me.btn40.Text = ">" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "."
        Me.btn40.UseVisualStyleBackColor = False
        '
        'btn41
        '
        Me.btn41.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn41.FlatAppearance.BorderSize = 0
        Me.btn41.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn41.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn41.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn41.ForeColor = System.Drawing.Color.White
        Me.btn41.Location = New System.Drawing.Point(504, 218)
        Me.btn41.Name = "btn41"
        Me.btn41.Size = New System.Drawing.Size(50, 50)
        Me.btn41.TabIndex = 112
        Me.btn41.Text = "<" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ","
        Me.btn41.UseVisualStyleBackColor = False
        '
        'btn_M
        '
        Me.btn_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_M.FlatAppearance.BorderSize = 0
        Me.btn_M.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_M.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_M.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_M.ForeColor = System.Drawing.Color.White
        Me.btn_M.Location = New System.Drawing.Point(452, 218)
        Me.btn_M.Name = "btn_M"
        Me.btn_M.Size = New System.Drawing.Size(50, 50)
        Me.btn_M.TabIndex = 111
        Me.btn_M.Text = "m"
        Me.btn_M.UseVisualStyleBackColor = False
        '
        'btn_N
        '
        Me.btn_N.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_N.FlatAppearance.BorderSize = 0
        Me.btn_N.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_N.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_N.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_N.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_N.ForeColor = System.Drawing.Color.White
        Me.btn_N.Location = New System.Drawing.Point(400, 218)
        Me.btn_N.Name = "btn_N"
        Me.btn_N.Size = New System.Drawing.Size(50, 50)
        Me.btn_N.TabIndex = 110
        Me.btn_N.Text = "n"
        Me.btn_N.UseVisualStyleBackColor = False
        '
        'btn_B
        '
        Me.btn_B.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_B.FlatAppearance.BorderSize = 0
        Me.btn_B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_B.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_B.ForeColor = System.Drawing.Color.White
        Me.btn_B.Location = New System.Drawing.Point(348, 218)
        Me.btn_B.Name = "btn_B"
        Me.btn_B.Size = New System.Drawing.Size(50, 50)
        Me.btn_B.TabIndex = 109
        Me.btn_B.Text = "b"
        Me.btn_B.UseVisualStyleBackColor = False
        '
        'btn_V
        '
        Me.btn_V.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_V.FlatAppearance.BorderSize = 0
        Me.btn_V.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_V.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_V.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_V.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_V.ForeColor = System.Drawing.Color.White
        Me.btn_V.Location = New System.Drawing.Point(296, 218)
        Me.btn_V.Name = "btn_V"
        Me.btn_V.Size = New System.Drawing.Size(50, 50)
        Me.btn_V.TabIndex = 108
        Me.btn_V.Text = "v"
        Me.btn_V.UseVisualStyleBackColor = False
        '
        'btn_C
        '
        Me.btn_C.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_C.FlatAppearance.BorderSize = 0
        Me.btn_C.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_C.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_C.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_C.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_C.ForeColor = System.Drawing.Color.White
        Me.btn_C.Location = New System.Drawing.Point(244, 218)
        Me.btn_C.Name = "btn_C"
        Me.btn_C.Size = New System.Drawing.Size(50, 50)
        Me.btn_C.TabIndex = 107
        Me.btn_C.Text = "c"
        Me.btn_C.UseVisualStyleBackColor = False
        '
        'btn_X
        '
        Me.btn_X.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_X.FlatAppearance.BorderSize = 0
        Me.btn_X.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_X.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_X.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_X.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_X.ForeColor = System.Drawing.Color.White
        Me.btn_X.Location = New System.Drawing.Point(192, 218)
        Me.btn_X.Name = "btn_X"
        Me.btn_X.Size = New System.Drawing.Size(50, 50)
        Me.btn_X.TabIndex = 106
        Me.btn_X.Text = "x"
        Me.btn_X.UseVisualStyleBackColor = False
        '
        'btn_Z
        '
        Me.btn_Z.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_Z.FlatAppearance.BorderSize = 0
        Me.btn_Z.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_Z.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_Z.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Z.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Z.ForeColor = System.Drawing.Color.White
        Me.btn_Z.Location = New System.Drawing.Point(140, 218)
        Me.btn_Z.Name = "btn_Z"
        Me.btn_Z.Size = New System.Drawing.Size(50, 50)
        Me.btn_Z.TabIndex = 105
        Me.btn_Z.Text = "z"
        Me.btn_Z.UseVisualStyleBackColor = False
        '
        'btnShiftL
        '
        Me.btnShiftL.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnShiftL.FlatAppearance.BorderSize = 0
        Me.btnShiftL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnShiftL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnShiftL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShiftL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShiftL.ForeColor = System.Drawing.Color.White
        Me.btnShiftL.Location = New System.Drawing.Point(21, 218)
        Me.btnShiftL.Name = "btnShiftL"
        Me.btnShiftL.Size = New System.Drawing.Size(117, 50)
        Me.btnShiftL.TabIndex = 104
        Me.btnShiftL.Text = "Shift"
        Me.btnShiftL.UseVisualStyleBackColor = False
        '
        'btnEnter
        '
        Me.btnEnter.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnEnter.FlatAppearance.BorderSize = 0
        Me.btnEnter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnEnter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnter.ForeColor = System.Drawing.Color.White
        Me.btnEnter.Location = New System.Drawing.Point(686, 166)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(106, 50)
        Me.btnEnter.TabIndex = 103
        Me.btnEnter.Text = "Enter"
        Me.btnEnter.UseVisualStyleBackColor = False
        '
        'btn29
        '
        Me.btn29.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn29.FlatAppearance.BorderSize = 0
        Me.btn29.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn29.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn29.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn29.ForeColor = System.Drawing.Color.White
        Me.btn29.Location = New System.Drawing.Point(634, 166)
        Me.btn29.Name = "btn29"
        Me.btn29.Size = New System.Drawing.Size(50, 50)
        Me.btn29.TabIndex = 102
        Me.btn29.Text = """" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " '"
        Me.btn29.UseVisualStyleBackColor = False
        '
        'btn30
        '
        Me.btn30.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn30.FlatAppearance.BorderSize = 0
        Me.btn30.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn30.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn30.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn30.ForeColor = System.Drawing.Color.White
        Me.btn30.Location = New System.Drawing.Point(582, 166)
        Me.btn30.Name = "btn30"
        Me.btn30.Size = New System.Drawing.Size(50, 50)
        Me.btn30.TabIndex = 101
        Me.btn30.Text = ":" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ";"
        Me.btn30.UseVisualStyleBackColor = False
        '
        'btn_L
        '
        Me.btn_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_L.FlatAppearance.BorderSize = 0
        Me.btn_L.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_L.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_L.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_L.ForeColor = System.Drawing.Color.White
        Me.btn_L.Location = New System.Drawing.Point(530, 166)
        Me.btn_L.Name = "btn_L"
        Me.btn_L.Size = New System.Drawing.Size(50, 50)
        Me.btn_L.TabIndex = 100
        Me.btn_L.Text = "l"
        Me.btn_L.UseVisualStyleBackColor = False
        '
        'btn_K
        '
        Me.btn_K.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_K.FlatAppearance.BorderSize = 0
        Me.btn_K.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_K.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_K.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_K.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_K.ForeColor = System.Drawing.Color.White
        Me.btn_K.Location = New System.Drawing.Point(478, 166)
        Me.btn_K.Name = "btn_K"
        Me.btn_K.Size = New System.Drawing.Size(50, 50)
        Me.btn_K.TabIndex = 99
        Me.btn_K.Text = "k"
        Me.btn_K.UseVisualStyleBackColor = False
        '
        'btn_J
        '
        Me.btn_J.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_J.FlatAppearance.BorderSize = 0
        Me.btn_J.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_J.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_J.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_J.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_J.ForeColor = System.Drawing.Color.White
        Me.btn_J.Location = New System.Drawing.Point(426, 166)
        Me.btn_J.Name = "btn_J"
        Me.btn_J.Size = New System.Drawing.Size(50, 50)
        Me.btn_J.TabIndex = 98
        Me.btn_J.Text = "j"
        Me.btn_J.UseVisualStyleBackColor = False
        '
        'btn_H
        '
        Me.btn_H.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_H.FlatAppearance.BorderSize = 0
        Me.btn_H.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_H.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_H.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_H.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_H.ForeColor = System.Drawing.Color.White
        Me.btn_H.Location = New System.Drawing.Point(374, 166)
        Me.btn_H.Name = "btn_H"
        Me.btn_H.Size = New System.Drawing.Size(50, 50)
        Me.btn_H.TabIndex = 97
        Me.btn_H.Text = "h"
        Me.btn_H.UseVisualStyleBackColor = False
        '
        'btn_G
        '
        Me.btn_G.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_G.FlatAppearance.BorderSize = 0
        Me.btn_G.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_G.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_G.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_G.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_G.ForeColor = System.Drawing.Color.White
        Me.btn_G.Location = New System.Drawing.Point(322, 166)
        Me.btn_G.Name = "btn_G"
        Me.btn_G.Size = New System.Drawing.Size(50, 50)
        Me.btn_G.TabIndex = 96
        Me.btn_G.Text = "g"
        Me.btn_G.UseVisualStyleBackColor = False
        '
        'btn_F
        '
        Me.btn_F.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_F.FlatAppearance.BorderSize = 0
        Me.btn_F.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_F.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_F.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_F.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_F.ForeColor = System.Drawing.Color.White
        Me.btn_F.Location = New System.Drawing.Point(270, 166)
        Me.btn_F.Name = "btn_F"
        Me.btn_F.Size = New System.Drawing.Size(50, 50)
        Me.btn_F.TabIndex = 95
        Me.btn_F.Text = "f"
        Me.btn_F.UseVisualStyleBackColor = False
        '
        'btn_D
        '
        Me.btn_D.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_D.FlatAppearance.BorderSize = 0
        Me.btn_D.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_D.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_D.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_D.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_D.ForeColor = System.Drawing.Color.White
        Me.btn_D.Location = New System.Drawing.Point(218, 166)
        Me.btn_D.Name = "btn_D"
        Me.btn_D.Size = New System.Drawing.Size(50, 50)
        Me.btn_D.TabIndex = 94
        Me.btn_D.Text = "d"
        Me.btn_D.UseVisualStyleBackColor = False
        '
        'btn_S
        '
        Me.btn_S.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_S.FlatAppearance.BorderSize = 0
        Me.btn_S.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_S.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_S.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_S.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_S.ForeColor = System.Drawing.Color.White
        Me.btn_S.Location = New System.Drawing.Point(166, 166)
        Me.btn_S.Name = "btn_S"
        Me.btn_S.Size = New System.Drawing.Size(50, 50)
        Me.btn_S.TabIndex = 93
        Me.btn_S.Text = "s"
        Me.btn_S.UseVisualStyleBackColor = False
        '
        'btn_A
        '
        Me.btn_A.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_A.FlatAppearance.BorderSize = 0
        Me.btn_A.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_A.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_A.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_A.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_A.ForeColor = System.Drawing.Color.White
        Me.btn_A.Location = New System.Drawing.Point(114, 166)
        Me.btn_A.Name = "btn_A"
        Me.btn_A.Size = New System.Drawing.Size(50, 50)
        Me.btn_A.TabIndex = 92
        Me.btn_A.Text = "a"
        Me.btn_A.UseVisualStyleBackColor = False
        '
        'btnCaps
        '
        Me.btnCaps.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnCaps.FlatAppearance.BorderSize = 0
        Me.btnCaps.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnCaps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnCaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCaps.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCaps.ForeColor = System.Drawing.Color.White
        Me.btnCaps.Location = New System.Drawing.Point(21, 166)
        Me.btnCaps.Name = "btnCaps"
        Me.btnCaps.Size = New System.Drawing.Size(91, 50)
        Me.btnCaps.TabIndex = 91
        Me.btnCaps.Text = "Caps"
        Me.btnCaps.UseVisualStyleBackColor = False
        '
        'btn14
        '
        Me.btn14.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn14.FlatAppearance.BorderSize = 0
        Me.btn14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn14.ForeColor = System.Drawing.Color.White
        Me.btn14.Location = New System.Drawing.Point(723, 114)
        Me.btn14.Name = "btn14"
        Me.btn14.Size = New System.Drawing.Size(69, 50)
        Me.btn14.TabIndex = 90
        Me.btn14.Text = "|" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "\"
        Me.btn14.UseVisualStyleBackColor = False
        '
        'btn15
        '
        Me.btn15.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn15.FlatAppearance.BorderSize = 0
        Me.btn15.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn15.ForeColor = System.Drawing.Color.White
        Me.btn15.Location = New System.Drawing.Point(671, 114)
        Me.btn15.Name = "btn15"
        Me.btn15.Size = New System.Drawing.Size(50, 50)
        Me.btn15.TabIndex = 89
        Me.btn15.Text = "}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "]"
        Me.btn15.UseVisualStyleBackColor = False
        '
        'btn16
        '
        Me.btn16.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn16.FlatAppearance.BorderSize = 0
        Me.btn16.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn16.ForeColor = System.Drawing.Color.White
        Me.btn16.Location = New System.Drawing.Point(619, 114)
        Me.btn16.Name = "btn16"
        Me.btn16.Size = New System.Drawing.Size(50, 50)
        Me.btn16.TabIndex = 88
        Me.btn16.Text = "{" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "["
        Me.btn16.UseVisualStyleBackColor = False
        '
        'btn_P
        '
        Me.btn_P.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_P.FlatAppearance.BorderSize = 0
        Me.btn_P.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_P.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_P.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_P.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_P.ForeColor = System.Drawing.Color.White
        Me.btn_P.Location = New System.Drawing.Point(567, 114)
        Me.btn_P.Name = "btn_P"
        Me.btn_P.Size = New System.Drawing.Size(50, 50)
        Me.btn_P.TabIndex = 87
        Me.btn_P.Text = "p"
        Me.btn_P.UseVisualStyleBackColor = False
        '
        'btn_O
        '
        Me.btn_O.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_O.FlatAppearance.BorderSize = 0
        Me.btn_O.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_O.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_O.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_O.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_O.ForeColor = System.Drawing.Color.White
        Me.btn_O.Location = New System.Drawing.Point(515, 114)
        Me.btn_O.Name = "btn_O"
        Me.btn_O.Size = New System.Drawing.Size(50, 50)
        Me.btn_O.TabIndex = 86
        Me.btn_O.Text = "o"
        Me.btn_O.UseVisualStyleBackColor = False
        '
        'btn_I
        '
        Me.btn_I.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_I.FlatAppearance.BorderSize = 0
        Me.btn_I.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_I.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_I.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_I.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_I.ForeColor = System.Drawing.Color.White
        Me.btn_I.Location = New System.Drawing.Point(463, 114)
        Me.btn_I.Name = "btn_I"
        Me.btn_I.Size = New System.Drawing.Size(50, 50)
        Me.btn_I.TabIndex = 85
        Me.btn_I.Text = "i"
        Me.btn_I.UseVisualStyleBackColor = False
        '
        'btn_U
        '
        Me.btn_U.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_U.FlatAppearance.BorderSize = 0
        Me.btn_U.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_U.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_U.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_U.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_U.ForeColor = System.Drawing.Color.White
        Me.btn_U.Location = New System.Drawing.Point(411, 114)
        Me.btn_U.Name = "btn_U"
        Me.btn_U.Size = New System.Drawing.Size(50, 50)
        Me.btn_U.TabIndex = 84
        Me.btn_U.Text = "u"
        Me.btn_U.UseVisualStyleBackColor = False
        '
        'btn_Y
        '
        Me.btn_Y.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_Y.FlatAppearance.BorderSize = 0
        Me.btn_Y.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_Y.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_Y.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Y.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Y.ForeColor = System.Drawing.Color.White
        Me.btn_Y.Location = New System.Drawing.Point(359, 114)
        Me.btn_Y.Name = "btn_Y"
        Me.btn_Y.Size = New System.Drawing.Size(50, 50)
        Me.btn_Y.TabIndex = 83
        Me.btn_Y.Text = "y"
        Me.btn_Y.UseVisualStyleBackColor = False
        '
        'btn_T
        '
        Me.btn_T.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_T.FlatAppearance.BorderSize = 0
        Me.btn_T.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_T.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_T.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_T.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_T.ForeColor = System.Drawing.Color.White
        Me.btn_T.Location = New System.Drawing.Point(307, 114)
        Me.btn_T.Name = "btn_T"
        Me.btn_T.Size = New System.Drawing.Size(50, 50)
        Me.btn_T.TabIndex = 82
        Me.btn_T.Text = "t"
        Me.btn_T.UseVisualStyleBackColor = False
        '
        'btn_R
        '
        Me.btn_R.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_R.FlatAppearance.BorderSize = 0
        Me.btn_R.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_R.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_R.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_R.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_R.ForeColor = System.Drawing.Color.White
        Me.btn_R.Location = New System.Drawing.Point(255, 114)
        Me.btn_R.Name = "btn_R"
        Me.btn_R.Size = New System.Drawing.Size(50, 50)
        Me.btn_R.TabIndex = 81
        Me.btn_R.Text = "r"
        Me.btn_R.UseVisualStyleBackColor = False
        '
        'btn_E
        '
        Me.btn_E.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_E.FlatAppearance.BorderSize = 0
        Me.btn_E.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_E.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_E.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_E.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_E.ForeColor = System.Drawing.Color.White
        Me.btn_E.Location = New System.Drawing.Point(203, 114)
        Me.btn_E.Name = "btn_E"
        Me.btn_E.Size = New System.Drawing.Size(50, 50)
        Me.btn_E.TabIndex = 80
        Me.btn_E.Text = "e"
        Me.btn_E.UseVisualStyleBackColor = False
        '
        'btn_W
        '
        Me.btn_W.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_W.FlatAppearance.BorderSize = 0
        Me.btn_W.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_W.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_W.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_W.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_W.ForeColor = System.Drawing.Color.White
        Me.btn_W.Location = New System.Drawing.Point(151, 114)
        Me.btn_W.Name = "btn_W"
        Me.btn_W.Size = New System.Drawing.Size(50, 50)
        Me.btn_W.TabIndex = 79
        Me.btn_W.Text = "w"
        Me.btn_W.UseVisualStyleBackColor = False
        '
        'btn_Q
        '
        Me.btn_Q.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn_Q.FlatAppearance.BorderSize = 0
        Me.btn_Q.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn_Q.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_Q.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Q.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Q.ForeColor = System.Drawing.Color.White
        Me.btn_Q.Location = New System.Drawing.Point(99, 114)
        Me.btn_Q.Name = "btn_Q"
        Me.btn_Q.Size = New System.Drawing.Size(50, 50)
        Me.btn_Q.TabIndex = 78
        Me.btn_Q.Text = "q"
        Me.btn_Q.UseVisualStyleBackColor = False
        '
        'btnTab
        '
        Me.btnTab.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnTab.FlatAppearance.BorderSize = 0
        Me.btnTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTab.ForeColor = System.Drawing.Color.White
        Me.btnTab.Location = New System.Drawing.Point(21, 114)
        Me.btnTab.Name = "btnTab"
        Me.btnTab.Size = New System.Drawing.Size(76, 50)
        Me.btnTab.TabIndex = 77
        Me.btnTab.Text = "Tab"
        Me.btnTab.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(697, 62)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(95, 50)
        Me.btnBack.TabIndex = 76
        Me.btnBack.Text = "Backspace"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btn13
        '
        Me.btn13.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn13.FlatAppearance.BorderSize = 0
        Me.btn13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn13.ForeColor = System.Drawing.Color.Transparent
        Me.btn13.Location = New System.Drawing.Point(21, 62)
        Me.btn13.Name = "btn13"
        Me.btn13.Size = New System.Drawing.Size(50, 50)
        Me.btn13.TabIndex = 75
        Me.btn13.Text = "~" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "`"
        Me.btn13.UseVisualStyleBackColor = False
        '
        'btn12
        '
        Me.btn12.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn12.FlatAppearance.BorderSize = 0
        Me.btn12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn12.ForeColor = System.Drawing.Color.White
        Me.btn12.Location = New System.Drawing.Point(645, 62)
        Me.btn12.Name = "btn12"
        Me.btn12.Size = New System.Drawing.Size(50, 50)
        Me.btn12.TabIndex = 74
        Me.btn12.Text = "+" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "="
        Me.btn12.UseVisualStyleBackColor = False
        '
        'btn11
        '
        Me.btn11.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn11.FlatAppearance.BorderSize = 0
        Me.btn11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn11.ForeColor = System.Drawing.Color.White
        Me.btn11.Location = New System.Drawing.Point(593, 62)
        Me.btn11.Name = "btn11"
        Me.btn11.Size = New System.Drawing.Size(50, 50)
        Me.btn11.TabIndex = 73
        Me.btn11.Text = "_" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-"
        Me.btn11.UseVisualStyleBackColor = False
        '
        'btn10
        '
        Me.btn10.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn10.FlatAppearance.BorderSize = 0
        Me.btn10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn10.ForeColor = System.Drawing.Color.White
        Me.btn10.Location = New System.Drawing.Point(541, 62)
        Me.btn10.Name = "btn10"
        Me.btn10.Size = New System.Drawing.Size(50, 50)
        Me.btn10.TabIndex = 72
        Me.btn10.Text = ")" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0"
        Me.btn10.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn9.FlatAppearance.BorderSize = 0
        Me.btn9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn9.ForeColor = System.Drawing.Color.White
        Me.btn9.Location = New System.Drawing.Point(489, 62)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(50, 50)
        Me.btn9.TabIndex = 71
        Me.btn9.Text = "(" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn8.FlatAppearance.BorderSize = 0
        Me.btn8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn8.ForeColor = System.Drawing.Color.White
        Me.btn8.Location = New System.Drawing.Point(437, 62)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(50, 50)
        Me.btn8.TabIndex = 70
        Me.btn8.Text = "*" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn7.FlatAppearance.BorderSize = 0
        Me.btn7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn7.ForeColor = System.Drawing.Color.White
        Me.btn7.Location = New System.Drawing.Point(385, 62)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(50, 50)
        Me.btn7.TabIndex = 69
        Me.btn7.Text = "&" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "7"
        Me.btn7.UseMnemonic = False
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn6.FlatAppearance.BorderSize = 0
        Me.btn6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn6.ForeColor = System.Drawing.Color.White
        Me.btn6.Location = New System.Drawing.Point(333, 62)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(50, 50)
        Me.btn6.TabIndex = 68
        Me.btn6.Text = "^" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn5.FlatAppearance.BorderSize = 0
        Me.btn5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn5.ForeColor = System.Drawing.Color.White
        Me.btn5.Location = New System.Drawing.Point(281, 62)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(50, 50)
        Me.btn5.TabIndex = 67
        Me.btn5.Text = "%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn4.FlatAppearance.BorderSize = 0
        Me.btn4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn4.ForeColor = System.Drawing.Color.White
        Me.btn4.Location = New System.Drawing.Point(229, 62)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(50, 50)
        Me.btn4.TabIndex = 66
        Me.btn4.Text = "$" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn3.FlatAppearance.BorderSize = 0
        Me.btn3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn3.ForeColor = System.Drawing.Color.White
        Me.btn3.Location = New System.Drawing.Point(177, 62)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(50, 50)
        Me.btn3.TabIndex = 65
        Me.btn3.Text = "#" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn2.FlatAppearance.BorderSize = 0
        Me.btn2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn2.ForeColor = System.Drawing.Color.White
        Me.btn2.Location = New System.Drawing.Point(125, 62)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(50, 50)
        Me.btn2.TabIndex = 64
        Me.btn2.Text = "@" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btn1.FlatAppearance.BorderSize = 0
        Me.btn1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btn1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1.ForeColor = System.Drawing.Color.White
        Me.btn1.Location = New System.Drawing.Point(73, 62)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(50, 50)
        Me.btn1.TabIndex = 63
        Me.btn1.Text = "!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.TextBox1.Location = New System.Drawing.Point(21, 21)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(771, 32)
        Me.TextBox1.TabIndex = 123
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(660, 218)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(132, 50)
        Me.btnCancel.TabIndex = 124
        Me.btnCancel.Text = "Quit"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'AlphaKeyboard3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(815, 339)
        Me.Controls.Add(Me.lblCurrentValue)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.btnSpace)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnRight)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.btn28)
        Me.Controls.Add(Me.btn40)
        Me.Controls.Add(Me.btn41)
        Me.Controls.Add(Me.btn_M)
        Me.Controls.Add(Me.btn_N)
        Me.Controls.Add(Me.btn_B)
        Me.Controls.Add(Me.btn_V)
        Me.Controls.Add(Me.btn_C)
        Me.Controls.Add(Me.btn_X)
        Me.Controls.Add(Me.btn_Z)
        Me.Controls.Add(Me.btnShiftL)
        Me.Controls.Add(Me.btnEnter)
        Me.Controls.Add(Me.btn29)
        Me.Controls.Add(Me.btn30)
        Me.Controls.Add(Me.btn_L)
        Me.Controls.Add(Me.btn_K)
        Me.Controls.Add(Me.btn_J)
        Me.Controls.Add(Me.btn_H)
        Me.Controls.Add(Me.btn_G)
        Me.Controls.Add(Me.btn_F)
        Me.Controls.Add(Me.btn_D)
        Me.Controls.Add(Me.btn_S)
        Me.Controls.Add(Me.btn_A)
        Me.Controls.Add(Me.btnCaps)
        Me.Controls.Add(Me.btn14)
        Me.Controls.Add(Me.btn15)
        Me.Controls.Add(Me.btn16)
        Me.Controls.Add(Me.btn_P)
        Me.Controls.Add(Me.btn_O)
        Me.Controls.Add(Me.btn_I)
        Me.Controls.Add(Me.btn_U)
        Me.Controls.Add(Me.btn_Y)
        Me.Controls.Add(Me.btn_T)
        Me.Controls.Add(Me.btn_R)
        Me.Controls.Add(Me.btn_E)
        Me.Controls.Add(Me.btn_W)
        Me.Controls.Add(Me.btn_Q)
        Me.Controls.Add(Me.btnTab)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btn13)
        Me.Controls.Add(Me.btn12)
        Me.Controls.Add(Me.btn11)
        Me.Controls.Add(Me.btn10)
        Me.Controls.Add(Me.btn9)
        Me.Controls.Add(Me.btn8)
        Me.Controls.Add(Me.btn7)
        Me.Controls.Add(Me.btn6)
        Me.Controls.Add(Me.btn5)
        Me.Controls.Add(Me.btn4)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.btn2)
        Me.Controls.Add(Me.btn1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AlphaKeyboard_v3"
        Me.Text = "AlphaKeyboard_v3"
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

    Public Property PassWordChar As Boolean Implements IKeyboard.PassWordChar
        Get
            Return TextBox1.UseSystemPasswordChar
        End Get
        Set(value As Boolean)
            TextBox1.UseSystemPasswordChar = value
        End Set
    End Property

    Public Overrides Property Font As System.Drawing.Font Implements IKeyboard.Font
        Get
            Return MyBase.Font
        End Get
        Set(value As System.Drawing.Font)
            MyBase.Font = value
        End Set
    End Property

    Public Property Value As String Implements IKeyboard.Value
        Get
            Return TextBox1.Text
        End Get
        Set(value As String)
            TextBox1.Text = value
        End Set
    End Property

    Private m_CurrentValue As String
    Public Property CurrentValue As String Implements IKeyboard.CurrentValue
        Get
            Return m_CurrentValue
        End Get
        Set(value As String)
            m_CurrentValue = value
        End Set
    End Property

    Public Shadows Property Location() As System.Drawing.Point Implements IKeyboard.Location
        Get
            Return MyBase.Location
        End Get
        Set(ByVal value As System.Drawing.Point)
            MyBase.Location = value
        End Set
    End Property

    Public Shadows Property Visible() As Boolean Implements IKeyboard.Visible
        Get
            Return MyBase.Visible
        End Get
        Set(ByVal value As Boolean)
            MyBase.Visible = value
        End Set
    End Property

    Public Overrides Property ForeColor As System.Drawing.Color Implements IKeyboard.ForeColor
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As System.Drawing.Color)
            MyBase.ForeColor = value
        End Set
    End Property

    Public Shadows Property TopMost As Boolean Implements IKeyboard.TopMost
        Get
            Return MyBase.TopMost
        End Get
        Set(value As Boolean)
            MyBase.TopMost = value
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

#End Region

#Region "Methods"

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each ctl As Control In Me.Controls
            If (ctl.Name.StartsWith("btn_")) Then
                Dim bttn As Button = DirectCast(ctl, Button)
                bttn.Tag = bttn.Text.ToLower
            End If
            If (ctl.Name.StartsWith("btn")) Then
                Dim bttn As Button = DirectCast(ctl, Button)
                AddHandler bttn.MouseEnter, AddressOf mouseEnterHandler
                AddHandler bttn.MouseLeave, AddressOf mouseLeaveHandler
                AddHandler bttn.MouseDown, AddressOf mouseDownHandler
                AddHandler bttn.MouseUp, AddressOf mouseUpHandler
            End If
        Next
        Lower()

    End Sub

    Public Sub mouseEnterHandler(sender As Object, e As EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.Black
        control.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Public Sub mouseLeaveHandler(sender As Object, e As EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.White
        control.BackColor = Color.FromArgb(65, 65, 65)
    End Sub

    Public Sub mouseDownHandler(sender As Object, e As EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.White
        control.BackColor = Color.FromArgb(0, 154, 187)
    End Sub

    Public Sub mouseUpHandler(sender As Object, e As EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        control.ForeColor = Color.Black
        control.BackColor = Color.FromArgb(229, 229, 229)
    End Sub

    Private Sub btnShiftR_Click(sender As Object, e As EventArgs) Handles btnShiftL.Click
        If ShiftMemory Then
            ShiftMemory = False
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn_")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Tag = bttn.Text.ToLower
                    bttn.Text = bttn.Text.ToLower
                    Lower()
                End If
            Next
        ElseIf Not ShiftMemory Then
            ShiftMemory = True
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn_")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Tag = bttn.Text.ToUpper
                    bttn.Text = bttn.Text.ToUpper
                    Upper()
                End If
            Next
        End If
    End Sub

    Private Sub btnCaps_Click(sender As Object, e As EventArgs) Handles btnCaps.Click
        If CapsMemory Then
            CapsMemory = False
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn_")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Tag = bttn.Text.ToLower
                    bttn.Text = bttn.Text.ToLower
                    Lower()
                End If
            Next
        ElseIf Not CapsMemory Then
            CapsMemory = True
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn_")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Tag = bttn.Text.ToUpper
                    bttn.Text = bttn.Text.ToUpper
                    Upper()
                End If
            Next
        End If
    End Sub

    Private Sub Lower()

        btn1.Tag = "1"
        btn2.Tag = "2"
        btn3.Tag = "3"
        btn4.Tag = "4"
        btn5.Tag = "5"
        btn6.Tag = "6"
        btn7.Tag = "7"
        btn8.Tag = "8"
        btn9.Tag = "9"
        btn10.Tag = "0"
        btn11.Tag = "-"
        btn12.Tag = "="
        btn13.Tag = "`"
        btn14.Tag = "\"
        btn15.Tag = "]"
        btn16.Tag = "["
        btn29.Tag = "'"
        btn30.Tag = ";"
        btn28.Tag = "/"
        btn40.Tag = "."
        btn41.Tag = ","

    End Sub

    Private Sub Upper()

        btn1.Tag = "!"
        btn2.Tag = "@"
        btn3.Tag = "#"
        btn4.Tag = "$"
        btn5.Tag = "{%}"
        btn6.Tag = "{^}"
        btn7.Tag = "&"
        btn8.Tag = "*"
        btn9.Tag = "{(}"
        btn10.Tag = "{)}"
        btn11.Tag = "{_}"
        btn12.Tag = "{+}"
        btn13.Tag = "{~}"
        btn14.Tag = "|"
        btn15.Tag = "{}}"
        btn16.Tag = "{{}"
        btn29.Tag = """"
        btn30.Tag = ":"
        btn28.Tag = "?"
        btn40.Tag = ">"
        btn41.Tag = "<"

    End Sub

    Private Sub btn28_Click(sender As Object, e As EventArgs) Handles btn1.Click,
        btn10.Click, btn11.Click, btn12.Click, btn13.Click, btn14.Click, btn28.Click,
        btn15.Click, btn16.Click, btn2.Click, btn29.Click, btn3.Click,
        btn30.Click, btn4.Click, btn40.Click, btn41.Click, btn5.Click,
        btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn_A.Click, btn_B.Click,
        btn_C.Click, btn_D.Click, btn_E.Click, btn_F.Click, btn_G.Click, btn_H.Click,
        btn_I.Click, btn_J.Click, btn_K.Click, btn_L.Click, btn_M.Click, btn_N.Click,
        btn_O.Click, btn_P.Click, btn_Q.Click, btn_R.Click, btn_S.Click, btn_T.Click,
        btn_V.Click, btn_U.Click, btn_W.Click, btn_X.Click, btn_Y.Click, btn_Z.Click

        Dim t As String = sender.Tag
        If ShiftMemory Then
            btnShiftL.PerformClick()
        End If
        TextBox1.Focus()
        SendKeys.Send(t)

    End Sub

    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click
        TextBox1.Focus()
        SendKeys.Send("{LEFT}")
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        TextBox1.Focus()
        SendKeys.Send("{UP}")
    End Sub

    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        TextBox1.Focus()
        SendKeys.Send("{RIGHT}")
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        TextBox1.Focus()
        SendKeys.Send("{DOWN}")
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        TextBox1.Focus()
        SendKeys.Send("{BACKSPACE}")
    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        RaiseEvent ButtonClick(Me, New KeypadEventArgs("Enter"))
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        TextBox1.Focus()
        'SendKeys.Send("{TAB}")
        SendKeys.Send("    ")
    End Sub

    Private Sub btnSpace_Click(sender As Object, e As EventArgs) Handles btnSpace.Click
        TextBox1.Focus()
        SendKeys.Send(" ")
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        TextBox1.Focus()
        SendKeys.Send("^{A}")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RaiseEvent ButtonClick(Me, New KeypadEventArgs("Quit"))
        'Me.Close()
        Me.Visible = False
    End Sub

#End Region

End Class