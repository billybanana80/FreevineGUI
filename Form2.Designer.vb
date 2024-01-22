<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        GroupBox1 = New GroupBox()
        TBfolder = New TextBox()
        BtnBrowse = New Button()
        Label1 = New Label()
        BtnClose = New Button()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        BtnSave = New Button()
        BtnClear = New Button()
        GroupBox2 = New GroupBox()
        TBfolder2 = New TextBox()
        BtnBrowse2 = New Button()
        Label2 = New Label()
        FolderBrowserDialog2 = New FolderBrowserDialog()
        Label3 = New Label()
        GroupBox3 = New GroupBox()
        btnCreate = New Button()
        Label7 = New Label()
        Label6 = New Label()
        Label5 = New Label()
        tbPassword = New TextBox()
        tbUsername = New TextBox()
        cbService = New ComboBox()
        Label4 = New Label()
        GroupBox4 = New GroupBox()
        BtnClearCache = New Button()
        Label8 = New Label()
        GroupBox5 = New GroupBox()
        Label14 = New Label()
        lblCurrentProxy = New Label()
        Label13 = New Label()
        Label12 = New Label()
        BtnSetProxy = New Button()
        tbWPassword = New TextBox()
        tbWUsername = New TextBox()
        Label10 = New Label()
        Label11 = New Label()
        tbCustomProxy = New TextBox()
        btnCustom = New RadioButton()
        btnWindscribe = New RadioButton()
        btnHola = New RadioButton()
        Label9 = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox5.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TBfolder)
        GroupBox1.Controls.Add(BtnBrowse)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(500, 94)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Freevine Folder Location (Mandatory)"
        ' 
        ' TBfolder
        ' 
        TBfolder.Location = New Point(6, 49)
        TBfolder.Name = "TBfolder"
        TBfolder.Size = New Size(396, 23)
        TBfolder.TabIndex = 4
        ' 
        ' BtnBrowse
        ' 
        BtnBrowse.ForeColor = SystemColors.ControlText
        BtnBrowse.Location = New Point(416, 44)
        BtnBrowse.Name = "BtnBrowse"
        BtnBrowse.Size = New Size(75, 30)
        BtnBrowse.TabIndex = 3
        BtnBrowse.Text = "Browse"
        BtnBrowse.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label1.ForeColor = Color.CornflowerBlue
        Label1.Location = New Point(6, 21)
        Label1.Name = "Label1"
        Label1.Size = New Size(311, 15)
        Label1.TabIndex = 0
        Label1.Text = "Choose the location of your Freevine folder, then click Save"
        ' 
        ' BtnClose
        ' 
        BtnClose.Location = New Point(428, 251)
        BtnClose.Name = "BtnClose"
        BtnClose.Size = New Size(75, 30)
        BtnClose.TabIndex = 2
        BtnClose.Text = "Close"
        BtnClose.UseVisualStyleBackColor = True
        ' 
        ' BtnSave
        ' 
        BtnSave.Location = New Point(347, 251)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(75, 30)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Save"
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' BtnClear
        ' 
        BtnClear.Location = New Point(266, 251)
        BtnClear.Name = "BtnClear"
        BtnClear.Size = New Size(75, 30)
        BtnClear.TabIndex = 4
        BtnClear.Text = "Clear"
        BtnClear.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(TBfolder2)
        GroupBox2.Controls.Add(BtnBrowse2)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        GroupBox2.Location = New Point(11, 118)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(500, 94)
        GroupBox2.TabIndex = 5
        GroupBox2.TabStop = False
        GroupBox2.Text = "Freevine Downloads Location (Optional)"
        ' 
        ' TBfolder2
        ' 
        TBfolder2.Location = New Point(6, 49)
        TBfolder2.Name = "TBfolder2"
        TBfolder2.Size = New Size(396, 23)
        TBfolder2.TabIndex = 4
        ' 
        ' BtnBrowse2
        ' 
        BtnBrowse2.ForeColor = SystemColors.ControlText
        BtnBrowse2.Location = New Point(416, 44)
        BtnBrowse2.Name = "BtnBrowse2"
        BtnBrowse2.Size = New Size(75, 30)
        BtnBrowse2.TabIndex = 3
        BtnBrowse2.Text = "Browse"
        BtnBrowse2.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label2.ForeColor = Color.CornflowerBlue
        Label2.Location = New Point(6, 21)
        Label2.Name = "Label2"
        Label2.Size = New Size(326, 15)
        Label2.TabIndex = 0
        Label2.Text = "Choose the location of your Downloads folder, then click Save"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        Label3.Location = New Point(17, 223)
        Label3.Name = "Label3"
        Label3.Size = New Size(431, 15)
        Label3.TabIndex = 6
        Label3.Text = "Note: you must restart Freevine GUI after changing either of these options above"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(btnCreate)
        GroupBox3.Controls.Add(Label7)
        GroupBox3.Controls.Add(Label6)
        GroupBox3.Controls.Add(Label5)
        GroupBox3.Controls.Add(tbPassword)
        GroupBox3.Controls.Add(tbUsername)
        GroupBox3.Controls.Add(cbService)
        GroupBox3.Controls.Add(Label4)
        GroupBox3.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        GroupBox3.Location = New Point(12, 287)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(500, 179)
        GroupBox3.TabIndex = 7
        GroupBox3.TabStop = False
        GroupBox3.Text = "Set Service Profile (Optional)"
        ' 
        ' btnCreate
        ' 
        btnCreate.ForeColor = SystemColors.ControlText
        btnCreate.Location = New Point(415, 122)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(75, 30)
        btnCreate.TabIndex = 8
        btnCreate.Text = "Create"
        btnCreate.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = SystemColors.ControlText
        Label7.Location = New Point(6, 130)
        Label7.Name = "Label7"
        Label7.Size = New Size(57, 15)
        Label7.TabIndex = 7
        Label7.Text = "Password"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ForeColor = SystemColors.ControlText
        Label6.Location = New Point(6, 91)
        Label6.Name = "Label6"
        Label6.Size = New Size(60, 15)
        Label6.TabIndex = 6
        Label6.Text = "Username"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = SystemColors.ControlText
        Label5.Location = New Point(6, 55)
        Label5.Name = "Label5"
        Label5.Size = New Size(44, 15)
        Label5.TabIndex = 5
        Label5.Text = "Service"
        ' 
        ' tbPassword
        ' 
        tbPassword.Location = New Point(76, 127)
        tbPassword.Name = "tbPassword"
        tbPassword.Size = New Size(279, 23)
        tbPassword.TabIndex = 4
        ' 
        ' tbUsername
        ' 
        tbUsername.Location = New Point(76, 88)
        tbUsername.Name = "tbUsername"
        tbUsername.Size = New Size(279, 23)
        tbUsername.TabIndex = 3
        ' 
        ' cbService
        ' 
        cbService.FormattingEnabled = True
        cbService.Items.AddRange(New Object() {"ABC", "All4", "BBC", "CBC", "CTV", "CWTV", "ITV", "My 5", "Plex", "Pluto", "Roku", "STV", "SVT", "Tubi", "TV4", "TVNZ", "UKTV"})
        cbService.Location = New Point(76, 52)
        cbService.Name = "cbService"
        cbService.Size = New Size(142, 23)
        cbService.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label4.ForeColor = Color.CornflowerBlue
        Label4.Location = New Point(6, 21)
        Label4.Name = "Label4"
        Label4.Size = New Size(414, 15)
        Label4.TabIndex = 1
        Label4.Text = "Choose the required Service, enter a Username and Password and click Create"
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(BtnClearCache)
        GroupBox4.Controls.Add(Label8)
        GroupBox4.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        GroupBox4.Location = New Point(11, 698)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(499, 67)
        GroupBox4.TabIndex = 8
        GroupBox4.TabStop = False
        GroupBox4.Text = "Clear Cache (Optional)"
        ' 
        ' BtnClearCache
        ' 
        BtnClearCache.ForeColor = SystemColors.ControlText
        BtnClearCache.Location = New Point(415, 26)
        BtnClearCache.Name = "BtnClearCache"
        BtnClearCache.Size = New Size(75, 30)
        BtnClearCache.TabIndex = 9
        BtnClearCache.Text = "Clear"
        BtnClearCache.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label8.ForeColor = Color.CornflowerBlue
        Label8.Location = New Point(6, 21)
        Label8.Name = "Label8"
        Label8.Size = New Size(212, 15)
        Label8.TabIndex = 0
        Label8.Text = "Click Clear to clear the download cache"
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Controls.Add(Label14)
        GroupBox5.Controls.Add(lblCurrentProxy)
        GroupBox5.Controls.Add(Label13)
        GroupBox5.Controls.Add(Label12)
        GroupBox5.Controls.Add(BtnSetProxy)
        GroupBox5.Controls.Add(tbWPassword)
        GroupBox5.Controls.Add(tbWUsername)
        GroupBox5.Controls.Add(Label10)
        GroupBox5.Controls.Add(Label11)
        GroupBox5.Controls.Add(tbCustomProxy)
        GroupBox5.Controls.Add(btnCustom)
        GroupBox5.Controls.Add(btnWindscribe)
        GroupBox5.Controls.Add(btnHola)
        GroupBox5.Controls.Add(Label9)
        GroupBox5.ForeColor = Color.FromArgb(CByte(91), CByte(91), CByte(91))
        GroupBox5.Location = New Point(12, 472)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(500, 220)
        GroupBox5.TabIndex = 9
        GroupBox5.TabStop = False
        GroupBox5.Text = "Set Proxy Options (Optional)"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label14.Location = New Point(101, 192)
        Label14.Name = "Label14"
        Label14.Size = New Size(229, 15)
        Label14.TabIndex = 16
        Label14.Text = "eg: https://username:password@host:port"
        ' 
        ' lblCurrentProxy
        ' 
        lblCurrentProxy.AutoSize = True
        lblCurrentProxy.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        lblCurrentProxy.Location = New Point(420, 21)
        lblCurrentProxy.Name = "lblCurrentProxy"
        lblCurrentProxy.Size = New Size(39, 15)
        lblCurrentProxy.TabIndex = 15
        lblCurrentProxy.Text = "Proxy"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label13.Location = New Point(313, 21)
        Label13.Name = "Label13"
        Label13.Size = New Size(113, 15)
        Label13.TabIndex = 14
        Label13.Text = "Current set Proxy is: "
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label12.Location = New Point(88, 68)
        Label12.Name = "Label12"
        Label12.Size = New Size(203, 15)
        Label12.TabIndex = 13
        Label12.Text = "(Username and Password is required)"
        ' 
        ' BtnSetProxy
        ' 
        BtnSetProxy.ForeColor = SystemColors.ControlText
        BtnSetProxy.Location = New Point(415, 159)
        BtnSetProxy.Name = "BtnSetProxy"
        BtnSetProxy.Size = New Size(75, 30)
        BtnSetProxy.TabIndex = 12
        BtnSetProxy.Text = "Set"
        BtnSetProxy.UseVisualStyleBackColor = True
        ' 
        ' tbWPassword
        ' 
        tbWPassword.Location = New Point(101, 125)
        tbWPassword.Name = "tbWPassword"
        tbWPassword.Size = New Size(279, 23)
        tbWPassword.TabIndex = 11
        ' 
        ' tbWUsername
        ' 
        tbWUsername.Location = New Point(101, 88)
        tbWUsername.Name = "tbWUsername"
        tbWUsername.Size = New Size(279, 23)
        tbWUsername.TabIndex = 10
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.ForeColor = SystemColors.ControlText
        Label10.Location = New Point(26, 132)
        Label10.Name = "Label10"
        Label10.Size = New Size(57, 15)
        Label10.TabIndex = 9
        Label10.Text = "Password"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.ForeColor = SystemColors.ControlText
        Label11.Location = New Point(26, 93)
        Label11.Name = "Label11"
        Label11.Size = New Size(60, 15)
        Label11.TabIndex = 8
        Label11.Text = "Username"
        ' 
        ' tbCustomProxy
        ' 
        tbCustomProxy.Location = New Point(101, 166)
        tbCustomProxy.Name = "tbCustomProxy"
        tbCustomProxy.Size = New Size(279, 23)
        tbCustomProxy.TabIndex = 6
        ' 
        ' btnCustom
        ' 
        btnCustom.AutoSize = True
        btnCustom.ForeColor = SystemColors.ControlText
        btnCustom.Location = New Point(9, 166)
        btnCustom.Name = "btnCustom"
        btnCustom.Size = New Size(67, 19)
        btnCustom.TabIndex = 5
        btnCustom.TabStop = True
        btnCustom.Text = "Custom"
        btnCustom.UseVisualStyleBackColor = True
        ' 
        ' btnWindscribe
        ' 
        btnWindscribe.AutoSize = True
        btnWindscribe.ForeColor = SystemColors.ControlText
        btnWindscribe.Location = New Point(9, 66)
        btnWindscribe.Name = "btnWindscribe"
        btnWindscribe.Size = New Size(84, 19)
        btnWindscribe.TabIndex = 4
        btnWindscribe.TabStop = True
        btnWindscribe.Text = "Windscribe"
        btnWindscribe.UseVisualStyleBackColor = True
        ' 
        ' btnHola
        ' 
        btnHola.AutoSize = True
        btnHola.ForeColor = SystemColors.ControlText
        btnHola.Location = New Point(9, 41)
        btnHola.Name = "btnHola"
        btnHola.Size = New Size(50, 19)
        btnHola.TabIndex = 3
        btnHola.TabStop = True
        btnHola.Text = "Hola"
        btnHola.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point)
        Label9.ForeColor = Color.CornflowerBlue
        Label9.Location = New Point(6, 21)
        Label9.Name = "Label9"
        Label9.Size = New Size(211, 15)
        Label9.TabIndex = 2
        Label9.Text = "Choose the required proxy and click Set"
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(523, 778)
        Controls.Add(GroupBox5)
        Controls.Add(GroupBox4)
        Controls.Add(GroupBox3)
        Controls.Add(Label3)
        Controls.Add(GroupBox2)
        Controls.Add(BtnClear)
        Controls.Add(BtnSave)
        Controls.Add(BtnClose)
        Controls.Add(GroupBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form2"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Freevine GUI - Options"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        GroupBox5.ResumeLayout(False)
        GroupBox5.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents BtnBrowse As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TBfolder As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnClear As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TBfolder2 As TextBox
    Friend WithEvents BtnBrowse2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents FolderBrowserDialog2 As FolderBrowserDialog
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents tbPassword As TextBox
    Friend WithEvents tbUsername As TextBox
    Friend WithEvents cbService As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCreate As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents BtnClearCache As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnCustom As RadioButton
    Friend WithEvents btnWindscribe As RadioButton
    Friend WithEvents btnHola As RadioButton
    Friend WithEvents Label9 As Label
    Friend WithEvents tbCustomProxy As TextBox
    Friend WithEvents BtnSetProxy As Button
    Friend WithEvents tbWPassword As TextBox
    Friend WithEvents tbWUsername As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lblCurrentProxy As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
End Class
