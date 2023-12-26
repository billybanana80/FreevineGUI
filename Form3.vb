
Imports System.Reflection.Emit
Imports System.IO


Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load saved Settings upon opening Favorites form
        Dim showNames As String() = My.Settings.ShowName.Split("|"c, StringSplitOptions.RemoveEmptyEntries)
        Dim services As String() = My.Settings.Service.Split("|"c, StringSplitOptions.RemoveEmptyEntries)
        Dim seriesURLs As String() = My.Settings.SeriesURL.Split("|"c, StringSplitOptions.RemoveEmptyEntries)

        Dim maxLength As Integer = Math.Max(showNames.Length, Math.Max(services.Length, seriesURLs.Length))

        For i As Integer = 0 To maxLength - 1
            Dim index As Integer = DataGridView1.Rows.Add()
            If i < showNames.Length Then
                DataGridView1.Rows(index).Cells("ShowNameColumn").Value = showNames(i)
            End If
            If i < services.Length Then
                DataGridView1.Rows(index).Cells("ServiceColumn").Value = services(i)
            End If
            If i < seriesURLs.Length Then
                DataGridView1.Rows(index).Cells("SeriesURLColumn").Value = seriesURLs(i)
            End If
        Next
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        ' Close the Favorites form
        Me.Close()
    End Sub

    Private Sub btnSave2_Click(sender As Object, e As EventArgs) Handles btnSave2.Click

        ' Clear the existing data in settings before adding new data
        My.Settings.ShowName = ""
        My.Settings.Service = ""
        My.Settings.SeriesURL = ""

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                ' Assuming "ShowNameColumn", "ServiceColumn", and "SeriesURLColumn" are the actual column names
                My.Settings.ShowName = My.Settings.ShowName & "|" & row.Cells("ShowNameColumn").Value.ToString()
                My.Settings.Service = My.Settings.Service & "|" & row.Cells("ServiceColumn").Value.ToString()
                My.Settings.SeriesURL = My.Settings.SeriesURL & "|" & row.Cells("SeriesURLColumn").Value.ToString()
            End If
        Next

        My.Settings.Save()

    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        ' Check if any row is selected
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the value from the "SeriesURL" column of the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim seriesURL As String = selectedRow.Cells("SeriesURLColumn").Value.ToString()

            ' Check if the SeriesURL is not empty
            If Not String.IsNullOrEmpty(seriesURL) Then
                ' Copy the SeriesURL to the clipboard
                Clipboard.SetText(seriesURL)

                ' Paste the SeriesURL into txtInput.Text
                Form1.TextBox1.Text = seriesURL

                ' Optionally, provide feedback to the user
                ' MessageBox.Show("SeriesURL copied to clipboard.", "Copy Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' Handle the case where SeriesURL is empty
                MessageBox.Show("Selected row does not have a SeriesURL.", "Empty SeriesURL", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            ' Handle the case where no row is selected
            MessageBox.Show("Please select a row before copying.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnClear2_Click(sender As Object, e As EventArgs) Handles btnClear2.Click
        ' Clear all rows in the DataGridView
        DataGridView1.Rows.Clear()

        ' Clear the existing data in settings to save
        My.Settings.ShowName = ""
        My.Settings.Service = ""
        My.Settings.SeriesURL = ""

    End Sub
End Class