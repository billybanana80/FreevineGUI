Imports System.Threading
Imports System.Threading.Tasks
Imports System.Diagnostics
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    ' Freevine GUI created by billybanana v 0.1.4
    ' This application does not interact directly with any streaming service.

    ' The python Function(s) invoked are created by 
    ' stabbedbybrick. More information is available at GitHub.
    ' https://github.com/stabbedbybrick/freevine

    Dim process2 As New Process()
    'Process2 is the built in Command Prompt

    'this contains all of the RadioButtons in the 1st main groupbox
    Dim firstMainGroupboxRadioButtons() As RadioButton
    'this contains all of the RadioButtons in the 2nd main groupbox
    Dim secondMainGroupboxRadioButtons() As RadioButton

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Initialize the ProcessStartInfo for the command prompt
        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.RedirectStandardInput = True
        psi.RedirectStandardOutput = True
        psi.UseShellExecute = False
        psi.CreateNoWindow = True
        ' Set the working directory (start directory)
        psi.WorkingDirectory = My.Settings.FolderPath

        ' Set the ProcessStartInfo for the process
        process2.StartInfo = psi

        ' Start the process
        process2.Start()

        ' Enable asynchronous reading from the standard output
        process2.BeginOutputReadLine()
        AddHandler process2.OutputDataReceived, AddressOf OutputDataReceived

        ' Add a delay using Task.Delay
        Await Task.Delay(100) ' 100 milliseconds (1/10th second) delay

        ' Add a delay and then clear the Output box
        rtbOutput.Text = ""


        ToolTip1.SetToolTip(Button7, "Click to clear selected Service & Action")
        ToolTip1.SetToolTip(Button3, "Click to choose Options")
        ToolTip1.SetToolTip(Button4, "Click to view Help")
        ToolTip1.SetToolTip(TBcompletecommand, "Displays the complete string passed through to the Command Prompt")
        ToolTip1.SetToolTip(GroupBox5, "Choose resolution if you wish to change from default = best")
        ToolTip1.SetToolTip(GroupBox6, "Choose bitrate if you wish to change from default = best")
        ToolTip1.SetToolTip(GroupBox1, "Selecting the streaming service is required only for SEARCH function")


        ' Uncheck all RadioButtons in the groupBox2
        For Each radioButton As RadioButton In GroupBox2.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        ' Uncheck all RadioButtons in the groupBox1
        For Each radioButton As RadioButton In GroupBox1.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        ' Uncheck all RadioButtons in the groupBox5
        For Each radioButton As RadioButton In GroupBox5.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        ' Uncheck all RadioButtons in the groupBox6
        For Each radioButton As RadioButton In GroupBox6.Controls.OfType(Of RadioButton)()
            radioButton.Checked = False
        Next

        'here i setup the handler methods + load the arrays
        Array.ForEach(GroupBox1.Controls.OfType(Of GroupBox).ToArray, Sub(gb) Array.ForEach(gb.Controls.OfType(Of RadioButton).ToArray, Sub(rb) AddHandler rb.CheckedChanged, AddressOf firstMainGroupboxRadioButtons_checkedchanged))
        Dim gbs() As GroupBox = GroupBox1.Controls.OfType(Of GroupBox).ToArray
        firstMainGroupboxRadioButtons = gbs(0).Controls.OfType(Of RadioButton).ToArray
        For x As Integer = 1 To gbs.GetUpperBound(0)
            firstMainGroupboxRadioButtons = firstMainGroupboxRadioButtons.Concat(gbs(x).Controls.OfType(Of RadioButton)).ToArray
        Next
        Array.ForEach(GroupBox2.Controls.OfType(Of GroupBox).ToArray, Sub(gb) Array.ForEach(gb.Controls.OfType(Of RadioButton).ToArray, Sub(rb) AddHandler rb.CheckedChanged, AddressOf secondMainGroupboxRadioButtons_checkedchanged))
        gbs = GroupBox2.Controls.OfType(Of GroupBox).ToArray
        secondMainGroupboxRadioButtons = gbs(0).Controls.OfType(Of RadioButton).ToArray
        For x As Integer = 1 To gbs.GetUpperBound(0)
            secondMainGroupboxRadioButtons = secondMainGroupboxRadioButtons.Concat(gbs(x).Controls.OfType(Of RadioButton)).ToArray
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

    'this is the handler for the 1st main groupbox's radiobuttons
    'it ensures only 1 radiobutton in this main group can be checked
    Private Sub firstMainGroupboxRadioButtons_checkedchanged(sender As System.Object, e As System.EventArgs)
        Dim current As RadioButton = DirectCast(sender, RadioButton)
        If current.Checked Then
            Array.ForEach(firstMainGroupboxRadioButtons.ToArray(), Sub(rb) rb.Checked = If(rb IsNot current, False, True))
        End If
    End Sub

    'this is the handler for the 2nd main groupbox's radiobuttons
    'it ensures only 1 radiobutton in this main group can be checked
    Private Sub secondMainGroupboxRadioButtons_checkedchanged(sender As System.Object, e As System.EventArgs)
        Dim current As RadioButton = DirectCast(sender, RadioButton)
        If current.Checked Then
            Array.ForEach(secondMainGroupboxRadioButtons.ToArray(), Sub(rb) rb.Checked = If(rb IsNot current, False, True))
        End If
    End Sub

    Private Sub OutputDataReceived(sender As Object, e As DataReceivedEventArgs)
        ' This event is called when data is received from the standard output
        ' Update the rtbOutput TextBox with the received data
        If Not String.IsNullOrEmpty(e.Data) Then
            UpdateOutput(e.Data)

            ' Check for specific output indicating the end of the command execution
            'If e.Data.EndsWith("Done") Then
            ' Command has finished, display the current working directory
            'UpdateOutput($"Current Working Directory: {process.StartInfo.WorkingDirectory}")
            'End If

            ' Check for the word "WARN" and change its color to red
            If e.Data.Contains(" WARN ") Then
                HighlightText("WARN", Color.OrangeRed)
            End If

            ' Check for the word "INFO" and change its color to red
            If e.Data.Contains(" INFO ") Then
                HighlightText("INFO", Color.Green)

            End If

            ' Check for the word "*CENC" and change its color to red
            If e.Data.Contains("*CENC") Then
                HighlightText("*CENC", Color.Red)

            End If

            ' Check for the word "MuxAfterDone is detected, binary merging is automatically enabled" and change its color to red
            If e.Data.Contains("MuxAfterDone is detected, binary merging is automatically enabled") Then
                HighlightText("MuxAfterDone is detected, binary merging is automatically enabled", Color.Orange)

            End If

            ' Check for the word "*CENC" and change its color to red
            If e.Data.Contains("Binary merging...") Then
                HighlightText("Binary merging...", Color.Blue)

            End If

            ' Check for the word "*CENC" and change its color to red
            If e.Data.Contains("Decrypting...") Then
                HighlightText("Decrypting...", Color.Gray)

            End If

            ' Check for the word "N_m3u8DL-RE" and change its color to red
            If e.Data.Contains("N_m3u8DL-RE") Then
                HighlightText("N_m3u8DL-RE", Color.DarkGreen)

            End If

            ' Check for the word "ERROR : <Response [401]> Access denied. User is not authenticated" and change its color to red
            If e.Data.Contains("ERROR : <Response [401]> Access denied. User is not authenticated") Then
                HighlightText("ERROR : <Response [401]> Access denied. User is not authenticated", Color.Red)

            End If

            ' Check for the word "ERROR : <Response [402]> Content requires subscription and is not supported" and change its color to red
            If e.Data.Contains("ERROR : <Response [402]> Content requires subscription and is not supported") Then
                HighlightText("ERROR : <Response [402]> Content requires subscription and is not supported", Color.Red)

            End If

            ' Check for the word "ERROR : <Response [451]> Content requires subscription and is not supported" and change its color to red
            If e.Data.Contains("ERROR : <Response [451]> Content requires subscription and is not supported") Then
                HighlightText("ERROR : <Response [451]> Content requires subscription and is not supported", Color.Red)

            End If

            ' Check for the word "ERROR: Download init file failed!" and change its color to red
            If e.Data.Contains("ERROR: Download init file failed!") Then
                HighlightText("ERROR: Download init file failed!", Color.Red)

            End If

            ' Check for the word "Error! 403" and change its color to red
            If e.Data.Contains("Error! 403") Then
                HighlightText("Error! 403", Color.Red)

            End If


            ' Check for the word "ERROR : <Response [301]> Content unavailable outside UK" and change its color to red
            If e.Data.Contains("ERROR : <Response [301]> Content unavailable outside UK") Then
                HighlightText("ERROR : <Response [301]> Content unavailable outside UK", Color.Red)

            End If

            ' Check for the word "ERROR: No stream found to download" and change its color to red
            If e.Data.Contains("ERROR: No stream found to download") Then
                HighlightText("ERROR: No stream found to download", Color.Red)

            End If

            ' Check for the word "ERROR : <Response [301]> Content unavailable outside US" and change its color to red
            If e.Data.Contains("ERROR : <Response [301]> Content unavailable outside US") Then
                HighlightText("ERROR : <Response [301]> Content unavailable outside US", Color.Red)

            End If

            ' Check for the word "Response status code does not indicate success: 403 (Forbidden)." and change its color to red
            If e.Data.Contains("Response status code does not indicate success: 403 (Forbidden).") Then
                HighlightText("Response status code does not indicate success: 403 (Forbidden).", Color.Red)

            End If
        End If
    End Sub

    Private Sub HighlightText(textToHighlight As String, color As Color)
        ' Find the position of the text to highlight
        Dim startIndex As Integer = 0
        While startIndex < rtbOutput.TextLength
            Dim index As Integer = rtbOutput.Find(textToHighlight, startIndex, RichTextBoxFinds.WholeWord)
            If index = -1 Then Exit While

            ' Highlight the text
            rtbOutput.SelectionStart = index
            rtbOutput.SelectionLength = textToHighlight.Length
            rtbOutput.SelectionColor = color

            ' Move the start index past this instance to search for the next
            startIndex = index + textToHighlight.Length
        End While

        ' Reset the selection to prevent further typing from being colored
        rtbOutput.SelectionStart = rtbOutput.TextLength
        rtbOutput.SelectionLength = 0
        rtbOutput.SelectionColor = rtbOutput.ForeColor
    End Sub
    Private Sub UpdateOutput(output As String)
        ' Update the rtbOutput TextBox with the received output
        If Me.InvokeRequired Then
            Me.Invoke(Sub() UpdateOutput(output))
        Else
            rtbOutput.AppendText(output & Environment.NewLine)

            ' Scroll to the caret (automatically scroll to the end)
            rtbOutput.ScrollToCaret()
        End If
    End Sub


    ' Clean up when the form is closing
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Try to close the main window gracefully
        If Not process2.HasExited Then
            process2.CloseMainWindow()
            process2.WaitForExit(2000) ' Wait for the process to exit, with a timeout of 2000 milliseconds
        End If

        ' If the process hasn't exited, forcefully close it
        If Not process2.HasExited Then
            process2.Kill()
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
        Dim subtitleArguments As String = ""
        Dim directoryArguments As String = ""
        Dim resolutionArguments As String = ""
        Dim bitrateArguments As String = ""

        ' Check if TBfolder2.Text is empty
        If Form2.TBfolder2.Text = "" Then
            ' Handle the case where TBfolder2.Text ie: Downloads folder is empty
            Select Case True
                Case BtnInfo.Checked
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    serviceArguments = "--titles"
                     ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    ' season/576
                Case BtnSeason.Checked And btn576.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=576"
                     ' season/540
                Case BtnSeason.Checked And btn540.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=540"
                    ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=450"
                    ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=360"
                    ' season
                Case BtnSeason.Checked
                    serviceArguments = "--season"
                    ' complete/1080/Best
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=576"
                    ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=540"
                    ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=450"
                    ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=360"
                    ' complete
                Case BtnComplete.Checked
                    serviceArguments = "--complete"
                    ' episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    ' episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    ' episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=576"
                                        ' episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=540"
                    ' episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=450"
                    ' episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=360"
                    ' episode
                Case BtnEpisode.Checked
                    serviceArguments = "--episode"
                    ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=576"
                                       ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=540"
                    ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=450"
                    ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=360"
                    ' movie
                Case BtnMovie.Checked
                    serviceArguments = "--movie"
                Case BtnSubs.Checked
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only"
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

                ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    ' season/576
                Case BtnSeason.Checked And btn576.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                        ' season/540
                Case BtnSeason.Checked And btn540.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' season
                Case BtnSeason.Checked
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                      ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' complete
                Case BtnComplete.Checked
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    ' Episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                     ' Episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                     ' Episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' Episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' Episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                     ' Episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                     ' Episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                       ' Episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                     ' Episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                     ' Episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                     ' Episode
                Case BtnEpisode.Checked
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                      ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' movie
                Case BtnMovie.Checked
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                Case BtnSubs.Checked
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only --save-dir """ + downloadPath + """"
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


            ' Quote the TextBox value and include it in the arguments
            Dim textBoxValue As String = GroupBox3.Controls("TextBox1").Text
            Dim quotedTextBoxValue As String = $"{textBoxValue}"

            ' Construct the complete command with quoted TextBox value
            Dim completeCommand As String = $"freevine.py {serviceArguments} {additionalArguments} {quotedTextBoxValue} {subtitleArguments} {resolutionArguments}{bitrateArguments} {directoryArguments}"

            ' Display the complete command in TextBox Complete Command
            TBcompletecommand.Text = completeCommand

            ' Write the command to the standard input of the process
            process2.StandardInput.WriteLine(completeCommand)



        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter
        Me.AcceptButton = BtnGo
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Clears all the button selections on the form

        TextBox1.Text = ""
        TBcompletecommand.Text = ""
        rtbOutput.Text = ""

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

        btn1080.Checked = False
        btn720.Checked = False
        btn576.Checked = False
        btn540.Checked = False
        btn450.Checked = False
        btn360.Checked = False
        btnBest.Checked = False
        btnWorst.Checked = False

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

            Dim helpcommand As String = "freevine.py --help"

            ' Display the complete command in TextBox Complete Command
            TBcompletecommand.Text = helpcommand

            ' Write the command to the standard input of the process
            process2.StandardInput.WriteLine(helpcommand)

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

    Private Sub FreevineExternalCommandPromptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FreevineExternalCommandPromptToolStripMenuItem.Click
        'Open freevine in the External Command Prompt

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
End Class
