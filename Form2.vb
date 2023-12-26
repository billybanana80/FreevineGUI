Imports System.Reflection.Emit
Imports System.IO

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the saved folder(s) path from application settings
        TBfolder.Text = My.Settings.FolderPath
        TBfolder2.Text = My.Settings.FolderPath2
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        ' Show the FolderBrowserDialog to choose Freevine path
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ' Get the selected folder path
            Dim selectedPath As String = FolderBrowserDialog1.SelectedPath

            ' Save the selected folder path to application settings
            My.Settings.FolderPath = selectedPath
            My.Settings.Save()

            ' Update the TextBox with the selected folder path
            TBfolder.Text = selectedPath
        End If
    End Sub

    Private Sub BtnBrowse2_Click(sender As Object, e As EventArgs) Handles BtnBrowse2.Click
        ' Show the FolderBrowserDialog2 to choose Downloads Path
        If FolderBrowserDialog2.ShowDialog() = DialogResult.OK Then
            ' Get the selected folder path
            Dim selectedPath2 As String = FolderBrowserDialog2.SelectedPath

            ' Save the selected folder path to application settings
            My.Settings.FolderPath2 = selectedPath2
            My.Settings.Save()

            ' Update the TextBox with the selected folder path
            TBfolder2.Text = selectedPath2
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        'Show the Freevine folder location in TBfolder and Downloads location in TBfolder2
        ' Load the saved folder path from application settings
        TBfolder.Text = My.Settings.FolderPath
        TBfolder2.Text = My.Settings.FolderPath2

        ' Set ToolStrip text to the folder path from TBfolder
        Form1.ToolStripStatusLabel2.Text = TBfolder.Text

        ' Warning message to set Freevine options
        If TBfolder.Text = "" Then
            Form1.ToolStripStatusLabel2.Text = "Please set your Freevine folder location in Options"
        End If

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        ' Clear the freevine and Downloads folder location

        ' Clear TBfolder.Text
        TBfolder.Text = ""
        TBfolder2.Text = ""

        ' Save the empty string to My.Settings.FolderPath
        My.Settings.FolderPath = ""
        My.Settings.FolderPath2 = ""
        My.Settings.Save()

        ' Update ToolStrip Text with the cleared folder path
        Form1.ToolStripStatusLabel2.Text = TBfolder.Text

    End Sub

    Private Sub btnSet_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim textService As String = cbService.Text
        Dim textUsername As String = tbUsername.Text
        Dim textPassword As String = tbPassword.Text

        ' Load the saved freevine folder path from application settings
        TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        ' freevine folder
        Dim folderPath As String = TBfolder.Text

        If cbService.Text = "" Or tbUsername.Text = "" Or tbPassword.Text = "" Then

            ' Display an error message if not all profile details have been completed
            MessageBox.Show("Please complete ALL profile options before pressing Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Construct the complete command with quoted TextBox value
            Dim profileCommand As String = $"freevine.py profile --username " + textUsername + " --password " + textPassword + " --service " + textService

            ' Display the profile command in TextBox Complete Command
            Form1.TextBox1.Text = profileCommand
            Form1.TBcompletecommand.Text = profileCommand

            ' Set the command for the Command Prompt process
            process.StartInfo.Arguments = $"/k " & profileCommand

            ' Start the process
            process.Start()


            ' Clear the Combo box and Text Boxes
            cbService.Text = ""
            tbUsername.Text = ""
            tbPassword.Text = ""
            Form1.TextBox1.Text = ""
        End If



    End Sub





    Private Sub btnClientSet_Click(sender As Object, e As EventArgs) Handles btnClientSet.Click
        ' Show the Freevine folder location in TBfolder
        ' Load the saved folder path from application settings
        TBfolder.Text = My.Settings.FolderPath

        ' Specify the path to the api.yaml file
        Dim filePath As String = TBfolder.Text + "\services\channel4\api.yaml"

        ' Check if the file exists
        If File.Exists(filePath) Then
            ' Read the content of the api.yaml file
            Dim fileContent As String = File.ReadAllText(filePath)

            If btnAndroid.Checked Then
                ' Update the client to "android"
                fileContent = fileContent.Replace("client: ""web""", "client: ""android""")

                ' Write the modified content back to the api.yaml file
                File.WriteAllText(filePath, fileContent)

                ' Message to show update to android
                MessageBox.Show("Client updated to android", "All4 Client Options")

            ElseIf btnWeb.Checked Then
                ' Update the client to "web"
                fileContent = fileContent.Replace("client: ""android""", "client: ""web""")

                ' Write the modified content back to the api.yaml file
                File.WriteAllText(filePath, fileContent)

                ' Message to show update to web
                MessageBox.Show("Client updated to web", "All4 Client Options")
            Else
                ' Neither radio button is checked
                MessageBox.Show("Please select a client type before clicking Set", "All4 Client Options")
            End If
        Else
            ' File does not exist
            MessageBox.Show("The api.yaml file does not exist at the specified path", "All4 Client Options")
        End If
    End Sub


End Class