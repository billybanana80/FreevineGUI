<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Button1 = New Button()
        Label1 = New Label()
        Label2 = New Label()
        LinkLabel1 = New LinkLabel()
        PictureBox1 = New PictureBox()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(317, 141)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 30)
        Button1.TabIndex = 0
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = SystemColors.ControlText
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(196, 30)
        Label1.TabIndex = 1
        Label1.Text = "Freevine GUI created by billybanana" & vbCrLf & "v 0.1.0"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 50)
        Label2.Name = "Label2"
        Label2.Size = New Size(305, 75)
        Label2.TabIndex = 2
        Label2.Text = "This application does not interact directly" & vbCrLf & "with any streaming service." & vbCrLf & vbCrLf & "The python function(s) invoked are created by " & vbCrLf & "stabbedbybrick. More information is available at GitHub." & vbCrLf
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.LinkBehavior = LinkBehavior.HoverUnderline
        LinkLabel1.Location = New Point(12, 135)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(242, 15)
        LinkLabel1.TabIndex = 4
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "https://github.com/stabbedbybrick/freevine"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(309, 10)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(83, 83)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' AboutForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(404, 183)
        Controls.Add(PictureBox1)
        Controls.Add(LinkLabel1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AboutForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "About Freevine GUI"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents PictureBox1 As PictureBox
End Class
