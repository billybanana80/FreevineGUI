﻿Imports System.Reflection.Emit

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


End Class