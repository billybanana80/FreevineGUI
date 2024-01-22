Imports System.Reflection.Emit
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the saved folder(s) path from application settings
        TBfolder.Text = My.Settings.FolderPath
        TBfolder2.Text = My.Settings.FolderPath2

        ' Specify the path to the config.yaml file
        Dim filePath As String = TBfolder.Text + "\config.yaml"

        ' Check if the file exists
        If File.Exists(filePath) Then
            ' Read the content of the config.yaml file
            Dim fileContent As String = File.ReadAllText(filePath)

            ' Extract the proxy value from the config.yaml file
            Dim proxyValue As String = GetConfigValue(fileContent, "proxy")

            ' Check if the proxy value starts with "http"
            If proxyValue.Trim().ToLower().StartsWith("http") Then
                lblCurrentProxy.Text = "custom"

                ' Check if the proxy value is equal to "# basic, hola, or windscribe"
            ElseIf proxyValue.Trim().ToLower() = "# basic, hola or windscribe" Then
                lblCurrentProxy.Text = "not configured"
            Else
                ' Display the extracted proxy value
                lblCurrentProxy.Text = proxyValue
            End If
        End If
    End Sub

    Private Function GetConfigValue(fileContent As String, key As String) As String
        ' Extract the value associated with the specified key from the YAML content
        ' This is a simple example and may need to be adapted based on the actual structure of your YAML file
        Dim regex As New Regex($"{key}: (.+)")
        Dim match As Match = regex.Match(fileContent)

        If match.Success Then
            Return match.Groups(1).Value.Trim()
        Else
            Return String.Empty
        End If
    End Function

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

    Private Sub BtnClearCache_Click(sender As Object, e As EventArgs) Handles BtnClearCache.Click
        'Run the clear cache command

        ' Load the saved folder path from application settings
        TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        Dim folderPath As String = TBfolder.Text

        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then

            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Construct the complete command with quoted TextBox value
            process.StartInfo.Arguments = $"/k python freevine.py clear-cache"

            ' Start the process
            process.Start()

        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnSetProxy_Click(sender As Object, e As EventArgs) Handles BtnSetProxy.Click
        ' Show the Freevine folder location in TBfolder
        ' Load the saved folder path from application settings
        TBfolder.Text = My.Settings.FolderPath

        ' Specify the path to the api.yaml file
        Dim filePath As String = TBfolder.Text + "\config.yaml"

        ' Check if the file exists
        If File.Exists(filePath) Then
            ' Read the content of the config.yaml file
            Dim fileContent As String = File.ReadAllText(filePath)

            If btnHola.Checked Then
                ' 		Update the proxy to "hola"
                Dim regex As New Regex("proxy:.*")
                fileContent = regex.Replace(fileContent, "proxy: hola")

                ' Write the modified content back to the config.yaml file
                File.WriteAllText(filePath, fileContent)

                ' Message to show update to Hola proxy
                MessageBox.Show("Hola proxy set successfully", "Set Proxy Options")

            ElseIf btnWindscribe.Checked Then
                ' Update the proxy to "windscribe"
                Dim regex As New Regex("proxy:.*")
                fileContent = regex.Replace(fileContent, "proxy: windscribe")

                ' Update the Windscribe Username
                If Not String.IsNullOrEmpty(tbWUsername.Text) Then
                    Dim regex2 As New Regex("username:.*")
                    fileContent = regex2.Replace(fileContent, "username: " + tbWUsername.Text)
                End If

                ' Update the Windscribe Password
                If Not String.IsNullOrEmpty(tbWPassword.Text) Then
                    Dim regex3 As New Regex("password:.*")
                    fileContent = regex3.Replace(fileContent, "password: " + tbWPassword.Text)
                End If

                ' Write the modified content back to the config.yaml file
                File.WriteAllText(filePath, fileContent)

                ' Message to show update to Windscribe proxy
                MessageBox.Show("Windscribe proxy set successfully", "Set Proxy Options")

            ElseIf btnCustom.Checked Then
                ' Update the proxy to "custom"
                Dim regex As New Regex("proxy:.*")
                fileContent = regex.Replace(fileContent, "proxy: " + tbCustomProxy.Text)

                ' Write the modified content back to the config.yaml file
                File.WriteAllText(filePath, fileContent)

                ' Message to show update to web
                MessageBox.Show("Custom proxy set successfully", "Set Proxy Options")

            Else
                ' Neither radio button is checked
                MessageBox.Show("Please select a Proxy option before clicking Set", "Set Proxy Options")
            End If
        Else
            ' File does not exist
            MessageBox.Show("The config.yaml file does not exist at the specified path", "Set Proxy Options")
        End If
    End Sub
End Class