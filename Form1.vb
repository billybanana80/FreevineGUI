Imports System.Threading
Imports System.Threading.Tasks
Imports System.Diagnostics
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form1
    ' Freevine GUI created by billybanana v 1.1.0
    ' This application does not interact directly with any streaming service.

    ' The python Function(s) invoked are created by 
    ' stabbedbybrick. More information is available at GitHub.
    ' https://github.com/stabbedbybrick/freevine

    Public Shared process2 As New Process()
    'Process2 is the built in Command Prompt

    'this contains all of the RadioButtons in the 1st main groupbox
    Dim firstMainGroupboxRadioButtons() As RadioButton
    'this contains all of the RadioButtons in the 2nd main groupbox
    Dim secondMainGroupboxRadioButtons() As RadioButton



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ToolTip1.SetToolTip(Button7, "Click to clear selected Service & Action")
        ToolTip1.SetToolTip(btnReset, "Click to kill Command window and clear selected Service & Action")
        ToolTip1.SetToolTip(Button3, "Click to choose Options")
        ToolTip1.SetToolTip(Button4, "Click to view Help")
        ToolTip1.SetToolTip(btnFavorites, "Click to view Favorites")
        ToolTip1.SetToolTip(TBcompletecommand, "Displays the complete string passed through to the Command Prompt")
        ToolTip1.SetToolTip(GroupBox5, "Choose resolution if you wish to change from default = best")
        ToolTip1.SetToolTip(GroupBox6, "Choose bitrate if you wish to change from default = best")
        ToolTip1.SetToolTip(GroupBox1, "Selecting the streaming service is required only for SEARCH function")
        ToolTip1.SetToolTip(addQueue, "Click to add the task to queue")
        ToolTip1.SetToolTip(clearQueue, "Click to clear all tasks from queue")
        ToolTip1.SetToolTip(processQueue, "Click to launch all tasks in queue")
        ToolTip1.SetToolTip(GroupBox9, "Select the dropdown box to choose a Proxy")

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

        '___________________________________________________________________________________________________

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

        '___________________________________________________________________________________________________

        ' Load the contents of the queue
        ' Specify the path to the queue.txt file
        Dim queuePath As String = Form2.TBfolder.Text + "\queue.txt"

        ' Check if the file exists
        If File.Exists(queuePath) Then
            ' Read the content of the queue.txt file
            Dim queueContent As String = File.ReadAllText(queuePath)

            ' Display the content in the RichTextBox
            rtbQueue.Text = queueContent
        End If

        '___________________________________________________________________________________________________

        ' Find the current Freevine version number by reading the "__init__.py" file
        ToolStripStatusLabel3.Text = "Freevine version:"

        ' Specify the path to the __init__.py file
        Dim filePath2 As String = Path.Combine(Form2.TBfolder.Text, "utils\__init__.py")

        If File.Exists(filePath2) Then

            ' Read the content of the __init__.py file
            Dim fileContent2 As String = File.ReadAllText(filePath2)
            Dim versionValue As String = ExtractversionValue(fileContent2, "__version__ = ""(v.*?)""")

            If versionValue.Contains("v") Then
                ' Populate the label based on the extracted version
                ToolStripStatusLabel4.Text = versionValue
            Else
                ' Handle the case where versionValue does not contain "v"
                ToolStripStatusLabel4.Text = "Freevine folder not set"
                MessageBox.Show("Please set your Freevine folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        End If
        '___________________________________________________________________________________________________

    End Sub

    Function ExtractversionValue(content As String, pattern As String) As String
        Dim match As Match = Regex.Match(content, pattern)

        If match.Success Then
            ' The version value is captured in the first group
            Return match.Groups(1).Value
        Else
            ' Return an empty string or handle the case where no match is found
            Return String.Empty
        End If
    End Function

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

    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        ' Show the Freevine folder location in TBfolder
        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Specify the path to the config.yaml file
        Dim filePath As String = Form2.TBfolder.Text + "\config.yaml"

        If File.Exists(filePath) Then
            ' Read the content of the config.yaml file
            Dim fileContent As String = File.ReadAllText(filePath)

            ' Extract the proxy value from the config.yaml file
            Dim proxyValue As String = GetConfigValue(fileContent, "proxy")

            ' Populate the dropdown list based on the proxy value
            If proxyValue = "hola" Then
                ' Options for Hola proxy
                ComboBox1.Items.Clear()
                ComboBox1.Items.AddRange({"au - Australia", "ca - Canada", "dk - Denmark", "gb - Great Britain", "ie - Ireland", "nz - New Zealand", "se - Sweden", "uk - United Kingdom", "us - United States"})
            ElseIf proxyValue = "windscribe" Then
                ' Options for Windscribe proxy
                ComboBox1.Items.Clear()
                ComboBox1.Items.AddRange({"ca - Canada", "uk - United Kingdom", "us - United States"})
            Else
                ' Display the Custom text after "proxy:" as an option
                Dim otherOption As String = proxyValue.Trim()
                If Not String.IsNullOrEmpty(otherOption) Then
                    ComboBox1.Items.Clear()
                    ComboBox1.Items.Add(otherOption)
                End If
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
        Dim getArguments As String = ""
        Dim proxyArguments As String = ""
        Dim audioArguments As String = ""
        Dim nocacheArguments As String = ""
        Dim appendidArguments As String = ""
        Dim fnArguments As String = ""
        Dim secondsArguments As String = ""
        Dim nomuxArguments As String = ""
        Dim nomuxsubsArguments As String = ""
        Dim savefilenameArguments As String = ""
        Dim shakaArguments As String = ""
        Dim threadsArguments As String = ""
        Dim ncArguments As String = ""

        ' Check if TBfolder2.Text is empty
        If Form2.TBfolder2.Text = "" Then
            ' Handle the case where TBfolder2.Text ie: Downloads folder is empty
            Select Case True
                Case BtnInfo.Checked
                    getArguments = "get"
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    getArguments = "get"
                    serviceArguments = "--titles"
                    ' season/2160/Best
                Case BtnSeason.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' season/2160/Worst
                Case BtnSeason.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' season/2160
                Case BtnSeason.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                     ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    ' season/576
                Case BtnSeason.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=576"
                     ' season/540
                Case BtnSeason.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=540"
                    ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=450"
                    ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=360"
                    ' season
                Case BtnSeason.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    ' complete/2160/Best
                Case BtnComplete.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' complete/2160/Worst
                Case BtnComplete.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' complete/2160
                Case BtnComplete.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                    ' complete/1080/Best
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=576"
                    ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=540"
                    ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=450"
                    ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=360"
                    ' complete
                Case BtnComplete.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    ' episode/2160/Best
                Case BtnEpisode.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' episode/2160/Worst
                Case BtnEpisode.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' episode/2160
                Case BtnEpisode.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                    ' episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    ' episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    ' episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=576"
                                        ' episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=540"
                    ' episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=450"
                    ' episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=360"
                    ' episode
                Case BtnEpisode.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    ' movie/2160/Best
                Case BtnMovie.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' movie/2160/Worst
                Case BtnMovie.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' movie/2160
                Case BtnMovie.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                    ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=576"
                                       ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=540"
                    ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=450"
                    ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=360"
                    ' movie
                Case BtnMovie.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                Case BtnSubs.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only"
                Case BtnHelp.Checked
                    getArguments = "get"
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "search"

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
                    ElseIf SVTBtn.Checked Then
                        additionalArguments = "svt"
                    ElseIf tv4Btn.Checked Then
                        additionalArguments = "tv4"
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
                    ElseIf BtnPlex.Checked Then
                        additionalArguments = "plex"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                    ElseIf BtnTVNZ.Checked Then
                        additionalArguments = "tvnz"
                    ElseIf btnRTE.Checked Then
                        additionalArguments = "rte"
                        ' Add more conditions for other service-specific options if needed

                    End If
            End Select


        Else
            ' Handle the case where TBfolder2.Text ie: Downloads folder is not empty
            Select Case True
                Case BtnInfo.Checked
                    getArguments = "get"
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    getArguments = "get"
                    serviceArguments = "--titles"
                ' the quotes ensure the path is correctly interpreted even if it contains spaces

               ' season/2160/Best
                Case BtnSeason.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                   ' season/2160/Worst
                Case BtnSeason.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                   ' season/2160
                Case BtnSeason.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    getArguments = "get"
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
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' season
                Case BtnSeason.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    ' complete/2160/Best
                Case BtnComplete.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' complete/2160/Worst
                Case BtnComplete.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' complete/2160
                Case BtnComplete.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    ' complete/1080/Best
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                      ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' complete
                Case BtnComplete.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                   ' Episode/2160/Best
                Case BtnEpisode.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                    ' Episode/2160/Worst
                Case BtnEpisode.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                    ' Episode/2160
                Case BtnEpisode.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    ' Episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                     ' Episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                     ' Episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' Episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' Episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                     ' Episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                     ' Episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                       ' Episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                     ' Episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                     ' Episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                     ' Episode
                Case BtnEpisode.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                   ' movie/2160/Best
                Case BtnMovie.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                   ' movie/2160/Worst
                Case BtnMovie.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                   ' movie/2160
                Case BtnMovie.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                    ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                    ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                    ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                    ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                      ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                    ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                    ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                    ' movie
                Case BtnMovie.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                Case BtnSubs.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only --save-dir """ + downloadPath + """"
                Case BtnHelp.Checked
                    getArguments = "get"
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "search"

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
                    ElseIf SVTBtn.Checked Then
                        additionalArguments = "svt"
                    ElseIf tv4Btn.Checked Then
                        additionalArguments = "tv4"
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
                    ElseIf BtnPlex.Checked Then
                        additionalArguments = "plex"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                    ElseIf BtnTVNZ.Checked Then
                        additionalArguments = "tvnz"
                    ElseIf btnRTE.Checked Then
                        additionalArguments = "rte"
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

            ' Check if ComboBox1.Text is empty
            If ComboBox1.Text = "" Then
                proxyArguments = ""
            ElseIf ComboBox1.Text.ToLower().StartsWith("http") Then
                ' If ComboBox1.Text starts with "http", set proxyArguments to the ComboBox1.Text
                proxyArguments = "--proxy " + ComboBox1.Text
            Else
                ' Handle proxyArguments based on ComboBox1.Text
                Select Case ComboBox1.Text.ToLower()
                    Case "au - australia"
                        proxyArguments = "--proxy AU"
                    Case "ca - canada"
                        proxyArguments = "--proxy CA"
                    Case "dk - denmark"
                        proxyArguments = "--proxy DK"
                    Case "gb - great britain"
                        proxyArguments = "--proxy GB"
                    Case "ie - ireland"
                        proxyArguments = "--proxy IE"
                    Case "nz - new zealand"
                        proxyArguments = "--proxy NZ"
                    Case "se - sweden"
                        proxyArguments = "--proxy SE"
                    Case "uk - united kingdom"
                        proxyArguments = "--proxy UK"
                    Case "us - united states"
                        proxyArguments = "--proxy US"
                        ' Add more cases as needed
                    Case Else
                        ' Default case if ComboBox1.Text doesn't match any specific case
                        proxyArguments = ""
                End Select
            End If

            ' Check if Audio Stream is empty
            If tbAudio.Text = "" Then
                audioArguments = ""
            Else
                ' Specify the Audio Stream entered
                audioArguments = "-sa id=" + tbAudio.Text
            End If

            ' Check if No Cache Option is checked
            If CBnocache.Checked = False Then
                nocacheArguments = ""
            Else
                nocacheArguments = "--no-cache"
            End If

            ' Check if Append ID Option is checked
            If Btnappendid.Checked = False Then
                appendidArguments = ""
            Else
                appendidArguments = "--append-id"
            End If

            ' Check if Force Numbering Option is checked
            If Btnfn.Checked = False Then
                fnArguments = ""
            Else
                fnArguments = "-fn"
            End If

            ' Check if Add Sleep Option is empty
            If tbSeconds.Text = "" Then
                secondsArguments = ""
            Else
                ' Specify the number of seconds entered
                secondsArguments = "--slowdown " + tbSeconds.Text
            End If

            ' Check if No Mux Option is checked
            If Btnnomux.Checked = False Then
                nomuxArguments = ""
            Else
                nomuxArguments = "--no-mux"
            End If

            ' Check if No Mux Subtitles Option is checked
            If Btnnomuxsubs.Checked = False Then
                nomuxsubsArguments = ""
            Else
                nomuxsubsArguments = "--sub-no-mux"
            End If

            ' Check if Save File Name Option is empty
            If tbSaveFileName.Text = "" Then
                savefilenameArguments = ""
            Else
                ' Specify the number of seconds entered
                savefilenameArguments = "--save-name " + tbSaveFileName.Text
            End If

            ' Check if Use Shaka Packager Option is checked
            If Btnshaka.Checked = False Then
                shakaArguments = ""
            Else
                shakaArguments = "--use-shaka-packager"
            End If

            ' Check if Threads Option is empty
            If tbThreads.Text = "" Then
                threadsArguments = ""
            Else
                ' Specify the number of threads entered
                threadsArguments = "--threads " + tbThreads.Text
            End If

            ' Check if N_m3u8DL-RE Option is empty
            If tbNCommand.Text = "" Then
                ncArguments = ""
            Else
                ' Specify the additional command
                ncArguments = "--threads " + tbNCommand.Text
            End If

            ' Construct the complete command with quoted TextBox value
            Dim completeCommand As String = $"freevine.py {getArguments} {serviceArguments} {additionalArguments} {quotedTextBoxValue} {subtitleArguments} {resolutionArguments}{bitrateArguments} {audioArguments} {nocacheArguments} {appendidArguments} {fnArguments} {secondsArguments} {proxyArguments} {nomuxArguments} {nomuxsubsArguments} {shakaArguments} {threadsArguments} {ncArguments} {savefilenameArguments} {directoryArguments}"

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



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Clears all the button selections on the form

        TextBox1.Text = ""
        TBcompletecommand.Text = ""
        tbAudio.Text = ""
        tbSeconds.Text = ""
        tbSaveFileName.Text = ""
        tbThreads.Text = ""
        tbNCommand.Text = ""

        BtnABC.Checked = False
        C4Btn.Checked = False
        BBCBtn.Checked = False
        ITVBtn.Checked = False
        C5Btn.Checked = False
        STVBtn.Checked = False
        SVTBtn.Checked = False
        TubiBtn.Checked = False
        UKTVBtn.Checked = False
        BtnCBCGem.Checked = False
        BtnCTV.Checked = False
        BtnCrackle.Checked = False
        BtnPlex.Checked = False
        BtnPluto.Checked = False
        BtnRoku.Checked = False
        BtnCWTV.Checked = False
        tv4Btn.Checked = False
        BtnTVNZ.Checked = False
        btnRTE.Checked = False

        BtnInfo.Checked = False
        BtnTitles.Checked = False
        BtnSeason.Checked = False
        BtnComplete.Checked = False
        BtnEpisode.Checked = False
        BtnMovie.Checked = False
        BtnSubs.Checked = False
        BtnHelp.Checked = False
        BtnSearch.Checked = False
        ComboBox1.SelectedIndex = -1

        btn2160.Checked = False
        btn1080.Checked = False
        btn720.Checked = False
        btn576.Checked = False
        btn540.Checked = False
        btn450.Checked = False
        btn360.Checked = False
        btnBest.Checked = False
        btnWorst.Checked = False
        CBnocache.Checked = False
        Btnappendid.Checked = False
        Btnfn.Checked = False
        Btnnomux.Checked = False
        Btnnomuxsubs.Checked = False
        Btnshaka.Checked = False

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
            process.StartInfo.Arguments = $"/k python freevine.py get --help"

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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Clears all the button selections on the form

        TextBox1.Text = ""
        TBcompletecommand.Text = ""
        tbAudio.Text = ""
        tbSeconds.Text = ""
        tbSaveFileName.Text = ""
        tbThreads.Text = ""
        tbNCommand.Text = ""

        BtnABC.Checked = False
        C4Btn.Checked = False
        BBCBtn.Checked = False
        ITVBtn.Checked = False
        C5Btn.Checked = False
        STVBtn.Checked = False
        SVTBtn.Checked = False
        TubiBtn.Checked = False
        UKTVBtn.Checked = False
        BtnCBCGem.Checked = False
        BtnCTV.Checked = False
        BtnCrackle.Checked = False
        BtnPlex.Checked = False
        BtnPluto.Checked = False
        BtnRoku.Checked = False
        BtnCWTV.Checked = False
        tv4Btn.Checked = False
        BtnTVNZ.Checked = False
        btnRTE.Checked = False

        BtnInfo.Checked = False
        BtnTitles.Checked = False
        BtnSeason.Checked = False
        BtnComplete.Checked = False
        BtnEpisode.Checked = False
        BtnMovie.Checked = False
        BtnSubs.Checked = False
        BtnHelp.Checked = False
        BtnSearch.Checked = False
        ComboBox1.SelectedIndex = -1

        btn2160.Checked = False
        btn1080.Checked = False
        btn720.Checked = False
        btn576.Checked = False
        btn540.Checked = False
        btn450.Checked = False
        btn360.Checked = False
        btnBest.Checked = False
        btnWorst.Checked = False
        CBnocache.Checked = False
        Btnappendid.Checked = False
        Btnfn.Checked = False
        Btnnomux.Checked = False
        Btnnomuxsubs.Checked = False
        Btnshaka.Checked = False

        ' Find all processes with the name "WindowsTerminal.exe"
        Dim processes() As Process = Process.GetProcessesByName("WindowsTerminal")

        ' Iterate through the list of processes and kill each one
        For Each process As Process In processes
            process.Kill()
        Next
    End Sub

    Private Sub btnFavorites_Click(sender As Object, e As EventArgs) Handles btnFavorites.Click
        ' Open Favorites form
        Form3.Show()
    End Sub

    Private Sub ShowFavoritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowFavoritesToolStripMenuItem.Click
        ' Open Favorites form
        Form3.Show()
    End Sub

    Private Sub addQueue_Click(sender As Object, e As EventArgs) Handles addQueue.Click
        ' Load the saved freevine folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        ' freevine folder
        Dim folderPath As String = Form2.TBfolder.Text
        ' downloads folder
        Dim downloadPath As String = Form2.TBfolder2.Text

        ' Specify the path to the queue.txt file
        Dim queuePath As String = Form2.TBfolder.Text + "\queue.txt"

        ' Check if the file exists
        If Not File.Exists(queuePath) Then
            ' If the file doesn't exist, create it
            File.Create(queuePath).Dispose()
        End If

        ' Read the existing content of the queue.txt file
        Dim queueEntries As New List(Of String)(File.ReadAllLines(queuePath))

        ' Determine which option is selected and construct the arguments accordingly
        Dim serviceArguments As String = ""
        Dim additionalArguments As String = ""
        Dim subtitleArguments As String = ""
        Dim directoryArguments As String = ""
        Dim resolutionArguments As String = ""
        Dim bitrateArguments As String = ""
        Dim getArguments As String = ""
        Dim proxyArguments As String = ""
        Dim audioArguments As String = ""
        Dim nocacheArguments As String = ""
        Dim appendidArguments As String = ""
        Dim fnArguments As String = ""
        Dim secondsArguments As String = ""
        Dim nomuxArguments As String = ""
        Dim nomuxsubsArguments As String = ""
        Dim savefilenameArguments As String = ""
        Dim shakaArguments As String = ""
        Dim threadsArguments As String = ""
        Dim ncArguments As String = ""

        ' Check if TBfolder2.Text is empty
        If Form2.TBfolder2.Text = "" Then
            ' Handle the case where TBfolder2.Text ie: Downloads folder is empty
            Select Case True
                Case BtnInfo.Checked
                    getArguments = "get"
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    getArguments = "get"
                    serviceArguments = "--titles"
                ' season/2160/Best
                Case BtnSeason.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' season/2160/Worst
                Case BtnSeason.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' season/2160
                Case BtnSeason.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=2160"
                 ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=1080"
                ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=720"
                ' season/576
                Case BtnSeason.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=576"
                 ' season/540
                Case BtnSeason.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=540"
                ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=450"
                ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    resolutionArguments = "-sv res=360"
                ' season
                Case BtnSeason.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                ' complete/2160/Best
                Case BtnComplete.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' complete/2160/Worst
                Case BtnComplete.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' complete/2160
                Case BtnComplete.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=2160"
                ' complete/1080/Best
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=1080"
                ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=720"
                ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=576"
                ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=540"
                ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=450"
                ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    resolutionArguments = "-sv res=360"
                ' complete
                Case BtnComplete.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                ' episode/2160/Best
                Case BtnEpisode.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' episode/2160/Worst
                Case BtnEpisode.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' episode/2160
                Case BtnEpisode.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=2160"
                ' episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=1080"
                ' episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=720"
                ' episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=576"
                                    ' episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=540"
                ' episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=450"
                ' episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    resolutionArguments = "-sv res=360"
                ' episode
                Case BtnEpisode.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                ' movie/2160/Best
                Case BtnMovie.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' movie/2160/Worst
                Case BtnMovie.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' movie/2160
                Case BtnMovie.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=2160"
                ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=1080"
                ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=720"
                ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=576"
                                   ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=540"
                ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=450"
                ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    resolutionArguments = "-sv res=360"
                ' movie
                Case BtnMovie.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                Case BtnSubs.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only"
                Case BtnHelp.Checked
                    getArguments = "get"
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "search"

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
                    ElseIf tv4Btn.Checked Then
                        additionalArguments = "tv4"
                    ElseIf SVTBtn.Checked Then
                        additionalArguments = "svt"
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
                    ElseIf BtnPlex.Checked Then
                        additionalArguments = "plex"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                    ElseIf BtnTVNZ.Checked Then
                        additionalArguments = "tvnz"
                    ElseIf btnRTE.Checked Then
                        additionalArguments = "rte"
                        ' Add more conditions for other service-specific options if needed


                    End If
            End Select


        Else
            ' Handle the case where TBfolder2.Text ie: Downloads folder is not empty
            Select Case True
                Case BtnInfo.Checked
                    getArguments = "get"
                    serviceArguments = "--info" + " --episode"
                Case BtnTitles.Checked
                    getArguments = "get"
                    serviceArguments = "--titles"
            ' the quotes ensure the path is correctly interpreted even if it contains spaces

           ' season/2160/Best
                Case BtnSeason.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
               ' season/2160/Worst
                Case BtnSeason.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
               ' season/2160
                Case BtnSeason.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
            ' season/1080/Best
                Case BtnSeason.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' season/1080/Worst
                Case BtnSeason.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' season/1080
                Case BtnSeason.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                ' season/720/Best
                Case BtnSeason.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' season/720/Worst
                Case BtnSeason.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' season/720
                Case BtnSeason.Checked And btn720.Checked
                    getArguments = "get"
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
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                ' season/450
                Case BtnSeason.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                ' season/360
                Case BtnSeason.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                ' season
                Case BtnSeason.Checked
                    getArguments = "get"
                    serviceArguments = "--season"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                ' complete/2160/Best
                Case BtnComplete.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' complete/2160/Worst
                Case BtnComplete.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' complete/2160
                Case BtnComplete.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                ' complete/1080/Best
                Case BtnComplete.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' complete/1080/Worst
                Case BtnComplete.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' complete/1080
                Case BtnComplete.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                ' complete/720/Best
                Case BtnComplete.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' complete/720/Worst
                Case BtnComplete.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' complete/720
                Case BtnComplete.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                ' complete/576
                Case BtnComplete.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                  ' complete/540
                Case BtnComplete.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                ' complete/450
                Case BtnComplete.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                ' complete/360
                Case BtnComplete.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                ' complete
                Case BtnComplete.Checked
                    getArguments = "get"
                    serviceArguments = "--complete"
                    directoryArguments = "--save-dir """ + downloadPath + """"
               ' Episode/2160/Best
                Case BtnEpisode.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
                ' Episode/2160/Worst
                Case BtnEpisode.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
                ' Episode/2160
                Case BtnEpisode.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                ' Episode/1080/Best
                Case BtnEpisode.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                 ' Episode/1080/Worst
                Case BtnEpisode.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                 ' Episode/1080
                Case BtnEpisode.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                ' Episode/720/Best
                Case BtnEpisode.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' Episode/720/Worst
                Case BtnEpisode.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                 ' Episode/720
                Case BtnEpisode.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                 ' Episode/576
                Case BtnEpisode.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                   ' Episode/540
                Case BtnEpisode.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                 ' Episode/450
                Case BtnEpisode.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                 ' Episode/360
                Case BtnEpisode.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                 ' Episode
                Case BtnEpisode.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    directoryArguments = "--save-dir """ + downloadPath + """"
               ' movie/2160/Best
                Case BtnMovie.Checked And btn2160.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=best"
               ' movie/2160/Worst
                Case BtnMovie.Checked And btn2160.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                    bitrateArguments = ":for=worst"
               ' movie/2160
                Case BtnMovie.Checked And btn2160.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=2160"
                ' movie/1080/Best
                Case BtnMovie.Checked And btn1080.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=best"
                ' movie/1080/Worst
                Case BtnMovie.Checked And btn1080.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                    bitrateArguments = ":for=worst"
                ' movie/1080
                Case BtnMovie.Checked And btn1080.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=1080"
                ' movie/720/Best
                Case BtnMovie.Checked And btn720.Checked And btnBest.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=best"
                ' movie/720/Worst
                Case BtnMovie.Checked And btn720.Checked And btnWorst.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                    bitrateArguments = ":for=worst"
                ' movie/720
                Case BtnMovie.Checked And btn720.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=720"
                ' movie/576
                Case BtnMovie.Checked And btn576.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=576"
                                  ' movie/540
                Case BtnMovie.Checked And btn540.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=540"
                ' movie/450
                Case BtnMovie.Checked And btn450.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=450"
                ' movie/360
                Case BtnMovie.Checked And btn360.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                    resolutionArguments = "-sv res=360"
                ' movie
                Case BtnMovie.Checked
                    getArguments = "get"
                    serviceArguments = "--movie"
                    directoryArguments = "--save-dir """ + downloadPath + """"
                Case BtnSubs.Checked
                    getArguments = "get"
                    serviceArguments = "--episode"
                    subtitleArguments = "--sub-only --save-dir """ + downloadPath + """"
                Case BtnHelp.Checked
                    getArguments = "get"
                    serviceArguments = "--help"
                Case BtnSearch.Checked
                    serviceArguments = "search"

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
                    ElseIf SVTBtn.Checked Then
                        additionalArguments = "svt"
                    ElseIf tv4Btn.Checked Then
                        additionalArguments = "tv4"
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
                    ElseIf BtnPlex.Checked Then
                        additionalArguments = "plex"
                    ElseIf BtnPluto.Checked Then
                        additionalArguments = "pluto"
                    ElseIf BtnRoku.Checked Then
                        additionalArguments = "roku"
                    ElseIf BtnABC.Checked Then
                        additionalArguments = "abc"
                    ElseIf BtnCWTV.Checked Then
                        additionalArguments = "cwtv"
                    ElseIf BtnTVNZ.Checked Then
                        additionalArguments = "tvnz"
                    ElseIf btnRTE.Checked Then
                        additionalArguments = "rte"
                        ' Add more conditions for other service-specific options if needed

                    End If

            End Select

        End If


        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then

            ' Quote the TextBox value and include it in the arguments
            Dim textBoxValue As String = GroupBox3.Controls("TextBox1").Text
            Dim quotedTextBoxValue As String = $"{textBoxValue}"

            ' Check if ComboBox1.Text is empty
            If ComboBox1.Text = "" Then
                proxyArguments = ""
            ElseIf ComboBox1.Text.ToLower().StartsWith("http") Then
                ' If ComboBox1.Text starts with "http", set proxyArguments to the ComboBox1.Text
                proxyArguments = "--proxy " + ComboBox1.Text
            Else
                ' Handle proxyArguments based on ComboBox1.Text
                Select Case ComboBox1.Text.ToLower()
                    Case "au - australia"
                        proxyArguments = "--proxy AU"
                    Case "ca - canada"
                        proxyArguments = "--proxy CA"
                    Case "dk - denmark"
                        proxyArguments = "--proxy DK"
                    Case "gb - great britain"
                        proxyArguments = "--proxy GB"
                    Case "ie - ireland"
                        proxyArguments = "--proxy IE"
                    Case "nz - new zealand"
                        proxyArguments = "--proxy NZ"
                    Case "se - sweden"
                        proxyArguments = "--proxy SE"
                    Case "uk - united kingdom"
                        proxyArguments = "--proxy UK"
                    Case "us - united states"
                        proxyArguments = "--proxy US"
                        ' Add more cases as needed
                    Case Else
                        ' Default case if ComboBox1.Text doesn't match any specific case
                        proxyArguments = ""
                End Select
            End If


            ' Check if Audio Stream is empty
            If tbAudio.Text = "" Then
                audioArguments = ""
            Else
                ' Specify the Audio Stream entered
                audioArguments = "-sa id=" + tbAudio.Text
            End If

            ' Check if No Cache Option is checked
            If CBnocache.Checked = False Then
                nocacheArguments = ""
            Else
                nocacheArguments = "--no-cache"
            End If

            ' Check if Append ID Option is checked
            If Btnappendid.Checked = False Then
                appendidArguments = ""
            Else
                appendidArguments = "--append-id"
            End If

            ' Check if Force Numbering Option is checked
            If Btnfn.Checked = False Then
                fnArguments = ""
            Else
                fnArguments = "-fn"
            End If

            ' Check if Add Sleep Option is empty
            If tbSeconds.Text = "" Then
                secondsArguments = ""
            Else
                ' Specify the number of seconds entered
                secondsArguments = "--slowdown " + tbSeconds.Text
            End If

            ' Check if No Mux Option is checked
            If Btnnomux.Checked = False Then
                nomuxArguments = ""
            Else
                nomuxArguments = "--no-mux"
            End If

            ' Check if No Mux Subtitles Option is checked
            If Btnnomuxsubs.Checked = False Then
                nomuxsubsArguments = ""
            Else
                nomuxsubsArguments = "--sub-no-mux"
            End If

            ' Check if Save File Name Option is empty
            If tbSaveFileName.Text = "" Then
                savefilenameArguments = ""
            Else
                ' Specify the number of seconds entered
                savefilenameArguments = "--save-name " + tbSaveFileName.Text
            End If

            ' Check if Use Shaka Packager Option is checked
            If Btnshaka.Checked = False Then
                shakaArguments = ""
            Else
                shakaArguments = "--use-shaka-packager"
            End If

            ' Check if Threads Option is empty
            If tbThreads.Text = "" Then
                threadsArguments = ""
            Else
                ' Specify the number of threads entered
                threadsArguments = "--threads " + tbThreads.Text
            End If

            ' Check if N_m3u8DL-RE Option is empty
            If tbNCommand.Text = "" Then
                ncArguments = ""
            Else
                ' Specify the additional command
                ncArguments = "--threads " + tbNCommand.Text
            End If

            ' Construct the complete command with quoted TextBox value
            Dim completeCommand As String = $"{getArguments} {serviceArguments} {additionalArguments} {quotedTextBoxValue} {subtitleArguments} {resolutionArguments}{bitrateArguments} {audioArguments} {nocacheArguments} {appendidArguments} {fnArguments} {secondsArguments} {proxyArguments} {nomuxArguments} {nomuxsubsArguments} {shakaArguments} {threadsArguments} {ncArguments} {savefilenameArguments} {directoryArguments}"

            ' Update the queue content (add new entry or modify as needed)
            Dim newQueueEntry As String = completeCommand

            ' Append the new entry to the end of the list
            queueEntries.Add(newQueueEntry)

            ' Write the modified content back to the queue.txt file
            File.WriteAllLines(queuePath, queueEntries)

            ' Update the RichTextBox with the updated content
            UpdateRichTextBox()

        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub UpdateRichTextBox()
        ' Specify the path to the queue.txt file
        Dim queuePath As String = Form2.TBfolder.Text + "\queue.txt"

        ' Check if the file exists
        If File.Exists(queuePath) Then
            ' Read the content of the queue.txt file
            Dim queueContent As String = File.ReadAllText(queuePath)

            ' Display the content in the RichTextBox
            rtbQueue.Text = queueContent
        End If
    End Sub

    Private Sub clearQueue_Click(sender As Object, e As EventArgs) Handles clearQueue.Click
        ' Show the Freevine folder location in TBfolder
        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Specify the path to the queue.txt file
        Dim queuePath As String = Form2.TBfolder.Text + "\queue.txt"

        ' Check if the file exists
        If File.Exists(queuePath) Then
            ' Clear the contents of the queue.txt file
            File.WriteAllText(queuePath, String.Empty)

            ' Update the RichTextBox with the cleared content
            UpdateRichTextBox()

            'Clear the main command text box
            TextBox1.Text = ""
            TBcompletecommand.Text = ""

        End If
    End Sub

    Private Sub processQueue_Click(sender As Object, e As EventArgs) Handles processQueue.Click
        ' Process the contents of queue.txt

        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        Dim folderPath As String = Form2.TBfolder.Text

        ' Specify the path to the queue.txt file
        Dim queuePath As String = Form2.TBfolder.Text + "\queue.txt"

        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then

            ' Start a new Command Prompt process
            Dim process As New Process()
            process.StartInfo.FileName = "cmd.exe"

            ' Set the working directory for the Command Prompt process
            process.StartInfo.WorkingDirectory = folderPath

            ' Construct the complete command with quoted TextBox value
            Dim processCommand As String = $"/k freevine.py file ""{queuePath}"""

            ' Set the command for the Command Prompt process
            process.StartInfo.Arguments = processCommand

            ' Start the process
            process.Start()

        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub SVTPlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SVTPlayToolStripMenuItem.Click
        'Open SVT Player

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.svtplay.se/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub PlexTVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlexTVToolStripMenuItem.Click
        'Open Plex TV

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.plex.tv/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub TV4PlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TV4PlayToolStripMenuItem.Click
        'Open TV4 Play

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.tv4play.se/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub ClearCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCacheToolStripMenuItem.Click
        'Run the clear cache command

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
            process.StartInfo.Arguments = $"/k python freevine.py clear-cache"

            ' Start the process
            process.Start()

        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TVNZToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TVNZToolStripMenuItem.Click
        'Open TVNZ

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.tvnz.co.nz/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub

    Private Sub TestCDMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestCDMToolStripMenuItem.Click
        ' Open CDM and Test your CDM in the Command Prompt
        ' Load the saved folder path from application settings
        Form2.TBfolder.Text = My.Settings.FolderPath

        ' Get the folder path from TBfolder.Text
        Dim folderPath As String = Form2.TBfolder.Text + "\utils\wvd"

        ' Check if the folder path exists
        If System.IO.Directory.Exists(folderPath) Then
            ' Get files with the .wvd extension in the specified directory
            Dim wvdFiles As String() = Directory.GetFiles(folderPath, "*.wvd")

            If wvdFiles.Length > 0 Then
                ' Take the first .wvd file (you can modify this logic as needed)
                Dim wvdFilePath As String = wvdFiles(0)

                ' Start a new Command Prompt process
                Dim process As New Process()
                process.StartInfo.FileName = "cmd.exe"

                ' Set the working directory for the Command Prompt process
                process.StartInfo.WorkingDirectory = folderPath

                ' Construct the complete command with quoted TextBox value
                process.StartInfo.Arguments = $"/k pywidevine test ""{wvdFilePath}"""

                ' Start the process
                process.Start()
            Else
                ' Display an error message if no .wvd files are found
                MessageBox.Show("No .wvd files found in the specified directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            ' Display an error message if the folder path doesn't exist
            MessageBox.Show("Please set your Freevine folder location in Options", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub RTEPlayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RTEPlayerToolStripMenuItem.Click
        'Open RTE Player

        Dim startexternal As New Process()

        startexternal.StartInfo.FileName = "https://www.rte.ie/player/"
        startexternal.StartInfo.UseShellExecute = True

        startexternal.Start()
    End Sub
End Class
