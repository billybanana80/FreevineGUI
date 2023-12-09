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
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
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
        GroupBox1.Size = New Size(500, 100)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Freevine Folder Location (Mandatory)"
        ' 
        ' TBfolder
        ' 
        TBfolder.Location = New Point(6, 55)
        TBfolder.Name = "TBfolder"
        TBfolder.Size = New Size(396, 23)
        TBfolder.TabIndex = 4
        ' 
        ' BtnBrowse
        ' 
        BtnBrowse.Location = New Point(416, 50)
        BtnBrowse.Name = "BtnBrowse"
        BtnBrowse.Size = New Size(75, 30)
        BtnBrowse.TabIndex = 3
        BtnBrowse.Text = "Browse"
        BtnBrowse.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = Color.CornflowerBlue
        Label1.Location = New Point(6, 27)
        Label1.Name = "Label1"
        Label1.Size = New Size(319, 15)
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
        GroupBox2.Size = New Size(500, 100)
        GroupBox2.TabIndex = 5
        GroupBox2.TabStop = False
        GroupBox2.Text = "Freevine Downloads Location (Optional)"
        ' 
        ' TBfolder2
        ' 
        TBfolder2.Location = New Point(6, 55)
        TBfolder2.Name = "TBfolder2"
        TBfolder2.Size = New Size(396, 23)
        TBfolder2.TabIndex = 4
        ' 
        ' BtnBrowse2
        ' 
        BtnBrowse2.Location = New Point(416, 50)
        BtnBrowse2.Name = "BtnBrowse2"
        BtnBrowse2.Size = New Size(75, 30)
        BtnBrowse2.TabIndex = 3
        BtnBrowse2.Text = "Browse"
        BtnBrowse2.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.CornflowerBlue
        Label2.Location = New Point(6, 27)
        Label2.Name = "Label2"
        Label2.Size = New Size(334, 15)
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
        Label3.Size = New Size(396, 15)
        Label3.TabIndex = 6
        Label3.Text = "Note: you must restart Freevine GUI after changing either of these options"
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(523, 293)
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
End Class
