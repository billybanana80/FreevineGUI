﻿Imports System.Threading
Imports System.Diagnostics
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    ' Freevine GUI created by billybanana v 0.1.0
    ' This application does not interact directly with any streaming service.

    ' The python Function(s) invoked are created by 
    ' stabbedbybrick. More information is available at GitHub.
    ' https://github.com/stabbedbybrick/freevine

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ToolTip1.SetToolTip(Btnfreevine, "Click to open a Command Prompt in the Freevine folder location")
        ToolTip1.SetToolTip(Button7, "Click to clear selected Service & Action")
        ToolTip1.SetToolTip(Button1, "Click to clear selected Service & Action(s) and to end task running Command Prompt processes")
        ToolTip1.SetToolTip(Button3, "Click to choose Options")
        ToolTip1.SetToolTip(Button4, "Click to view Help")
        ToolTip1.SetToolTip(TBcompletecommand, "Displays the complete string passed through to the Command Prompt")


        ' Uncheck all RadioButtons in the groupBox2
        For Each radioButton As RadioButton In GroupBox2.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        ' Uncheck all RadioButtons in the groupBox1
        For Each radioButton As RadioButton In GroupBox1.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        'Show the Freevine folder location in Label 5
        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath
        Form2.TBfolder2.Text = My.Settings.FolderPath2

        ' Set ToolStrip text to the folder path from TBfolder
        ToolStripStatusLabel1.Text = "Freevine folder:"
        ToolStripStatusLabel2.Text = Form2.TBfolder.Text

        If Form2.TBfolder.Text = "" Then
            ToolStripStatusLabel2.Text = "Please set your Freevine folder location in Options"
        End If

    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        ' Open Command Window to download videos from the selected service

        ' Load the saved freevine folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        ' freevine folder
        Dim folderPath As String = Form2.TBfolder.Text
        ' downloads folder
        Dim downloadPath As String = Form2.TBfolder2.Text

        ' Determine which option is selected and construct the arguments accordingly
        Dim serviceArguments As String = ""
        Dim additionalArguments As String = ""

        ' Check if TBfolder2.Text is empty
        If Form2.TBfolder2.Text = "" Then
            ' Handle the case where TBfolder2.Text ie: Downloads folder is empty
            Select Case True
                Case BtnInfo.Checked
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    serviceArguments = "--titles"
                Case BtnSeason.Checked
                    serviceArguments = "--season"
                Case BtnComplete.Checked
                    serviceArguments = "--complete"
                Case BtnEpisode.Checked
                    serviceArguments = "--episode"
                Case BtnMovie.Checked
                    serviceArguments = "--movie"
                Case BtnSubs.Checked
                    serviceArguments = "--subtitles"
                Case BtnHelp.Checked
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "--search"

                    ' Check additional service-specific options - this is in the context of the BtnSearch. Checked option as it directly follows
                    If BBCBtn.Checked Then
                        additionalArguments = "bbc"
                    ElseIf ITVBtn.Checked Then
                        additionalArguments = "itv"
                    ElseIf C4Btn.Checked Then
                        additionalArguments = "all4"
                    ElseIf C5Btn.Checked Then
                        additionalArguments = "my5"
                    ElseIf STVBtn.Checked Then
                        additionalArguments = "stv"
                    ElseIf TubiBtn.Checked Then
                        additionalArguments = "tubi"
                    ElseIf UKTVBtn.Checked Then
                        additionalArguments = "uktv"
                    ElseIf BtnCBCGem.Checked Then
                        additionalArguments = "cbc"
                    ElseIf BtnCTV.Checked Then
                        additionalArguments = "ctv"
                    ElseIf BtnCrackle.Checked Then
                        additionalArguments = "crackle"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                        ' Add more conditions for other service-specific options if needed
                    End If
            End Select


        Else
            ' Handle the case where TBfolder2.Text ie: Downloads folder is not empty
            Select Case True
                Case BtnInfo.Checked
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    serviceArguments = "--titles"
                ' the quotes ensure the path is correctly interpreted even if it contains spaces
                Case BtnSeason.Checked
                    serviceArguments = "--save-dir """ + downloadPath + """ --season"
                Case BtnComplete.Checked
                    serviceArguments = "--save-dir """ + downloadPath + """ --complete"
                Case BtnEpisode.Checked
                    serviceArguments = "--save-dir """ + downloadPath + """ --episode"
                Case BtnMovie.Checked
                    serviceArguments = "--save-dir """ + downloadPath + """ --movie"
                Case BtnSubs.Checked
                    serviceArguments = "--save-dir """ + downloadPath + """ --subtitles"
                Case BtnHelp.Checked
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "--search"

                    ' Check additional service-specific options - this is in the context of the BtnSearch. Checked option as it directly follows
                    If BBCBtn.Checked Then
                        additionalArguments = "bbc"
                    ElseIf ITVBtn.Checked Then
                        additionalArguments = "itv"
                    ElseIf C4Btn.Checked Then
                        additionalArguments = "all4"
                    ElseIf C5Btn.Checked Then
                        additionalArguments = "my5"
                    ElseIf STVBtn.Checked Then
                        additionalArguments = "stv"
                    ElseIf TubiBtn.Checked Then
                        additionalArguments = "tubi"
                    ElseIf UKTVBtn.Checked Then
                        additionalArguments = "uktv"
                    ElseIf BtnCBCGem.Checked Then
                        additionalArguments = "cbc"
                    ElseIf BtnCTV.Checked Then
                        additionalArguments = "ctv"
                    ElseIf BtnCrackle.Checked Then
                        additionalArguments = "crackle"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                        ' Add more conditions for other service-specific options if needed

                    End If
            End Select

        End If



        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then
            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Quote the TextBox value and include it in the arguments
            Dim textBoxValue As String = GroupBox3.Controls("TextBox1").Text
            Dim quotedTextBoxValue As String = $"{textBoxValue}"

            ' Construct the complete command with quoted TextBox value
            Dim completeCommand As String = $"freevine.py {serviceArguments} {additionalArguments} {quotedTextBoxValue}"

            ' Display the complete command in TextBox Complete Command
            TBcompletecommand.Text = completeCommand

            ' Set the command for the Command Prompt process
            process.StartInfo.Arguments = $"/k " & completeCommand

            ' Start the process
            process.Start()

        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter
        Me.AcceptButton = BtnGo
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Clears all the selections on the form
        TextBox1.Text = ""
        TBcompletecommand.Text = ""

        BtnABC.Checked = False
        C4Btn.Checked = False
        BBCBtn.Checked = False
        ITVBtn.Checked = False
        C5Btn.Checked = False
        STVBtn.Checked = False
        TubiBtn.Checked = False
        UKTVBtn.Checked = False
        BtnCBCGem.Checked = False
        BtnCTV.Checked = False
        BtnCrackle.Checked = False
        BtnPluto.Checked = False
        BtnRoku.Checked = False
        BtnCWTV.Checked = False

        BtnInfo.Checked = False
        BtnTitles.Checked = False
        BtnSeason.Checked = False
        BtnComplete.Checked = False
        BtnEpisode.Checked = False
        BtnMovie.Checked = False
        BtnSubs.Checked = False
        BtnHelp.Checked = False
        BtnSearch.Checked = False

        ' Find all processes with the name "WindowsTerminal.exe"
        Dim processes() As Process = Process.GetProcessesByName("WindowsTerminal")

        ' Iterate through the list of processes and kill each one
        For Each process As Process In processes
            process.Kill()
        Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Btnfreevine.Click
        'Open freevine in the Command Prompt

        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        Dim folderPath As String = Form2.TBfolder.Text

        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then
            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Start the process
            process.Start()


        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Clears all the button selections on the form

        TextBox1.Text = ""
        TBcompletecommand.Text = ""

        BtnABC.Checked = False
        C4Btn.Checked = False
        BBCBtn.Checked = False
        ITVBtn.Checked = False
        C5Btn.Checked = False
        STVBtn.Checked = False
        TubiBtn.Checked = False
        UKTVBtn.Checked = False
        BtnCBCGem.Checked = False
        BtnCTV.Checked = False
        BtnCrackle.Checked = False
        BtnPluto.Checked = False
        BtnRoku.Checked = False
        BtnCWTV.Checked = False

        BtnInfo.Checked = False
        BtnTitles.Checked = False
        BtnSeason.Checked = False
        BtnComplete.Checked = False
        BtnEpisode.Checked = False
        BtnMovie.Checked = False
        BtnSubs.Checked = False
        BtnHelp.Checked = False
        BtnSearch.Checked = False



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Open the Options form
        Form2.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub SetFreevineFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetFreevineFolderToolStripMenuItem.Click
        'Open the Options form
        Form2.Show()
    End Sub

    Private Sub ABCIViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ABCIViewToolStripMenuItem.Click
        'Open ABC iView
        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://iview.abc.net.au/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub All4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles All4ToolStripMenuItem.Click
        'Open All 4
        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.channel4.com/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub BBCIPlayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BBCIPlayerToolStripMenuItem.Click
        'Open BBC iPlayer

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.bbc.co.uk/iplayer"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub CBCGemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CBCGemToolStripMenuItem.Click
        'Open CBC Gem

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://gem.cbc.ca/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub CrackleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrackleToolStripMenuItem.Click
        'Open Crackle

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.crackle.com/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub CTVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CTVToolStripMenuItem.Click
        'Open CTV

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.ctv.ca/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub ITVXToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ITVXToolStripMenuItem.Click
        'Open ITV Hub

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.itv.com/watch/categories"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub My5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles My5ToolStripMenuItem.Click
        'Open My5

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.channel5.com/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub PlutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlutoToolStripMenuItem.Click
        'Open Pluto

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://web.pluto.tv/on-demand"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub TheRokuChannelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TheRokuChannelToolStripMenuItem.Click
        'Open The Roku Channel

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "http://therokuchannel.roku.com"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub STVPlayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STVPlayerToolStripMenuItem.Click
        'Open STV Player

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://player.stv.tv/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub TubiTVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TubiTVToolStripMenuItem.Click
        'Open Tubi TV

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://tubitv.com/home"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub UKTVPlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UKTVPlayToolStripMenuItem.Click
        'Open UKTV Play

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://uktvplay.co.uk/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutForm.Show()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        'Open the Help information for freevine in the Command Prompt

        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        Dim folderPath As String = Form2.TBfolder.Text

        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then
            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Construct the complete command with quoted TextBox value
            process.StartInfo.Arguments = $"/k python freevine.py --help"

            ' Start the process
            process.Start()


        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CWTVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CWTVToolStripMenuItem.Click
        'Open CWTV

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.cwtv.com/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub
End Class
