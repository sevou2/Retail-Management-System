Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        LoadReportTable()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        DownloadReportTableAsPDF()
    End Sub

    Private Sub LoadReportTable()
        Try
            Using connection As New MySqlConnection(connectionString)
                ' Open the database connection
                connection.Open()

                ' The SQL query to fetch all records from the product_changes table
                Dim query As String = "SELECT * FROM product_changes"

                ' Create a data adapter to execute the query and fill the data table
                Using da As New MySqlDataAdapter(query, connection)
                    ' Create a DataTable to hold the query results
                    Dim dt As New DataTable()

                    ' Fill the DataTable with the results of the SELECT query
                    da.Fill(dt)

                    ' Assign the DataTable as the DataSource for Guna2DataGridView1
                    Guna2DataGridView1.DataSource = dt
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show("MySQL Error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub


    Private Sub DownloadReportTableAsPDF()
        Try
            ' Check if Guna2DataGridView1 is not Nothing before proceeding
            If Guna2DataGridView1 IsNot Nothing Then
                ' Create a SaveFileDialog to choose the destination for the PDF file
                Using saveFileDialog As New SaveFileDialog()
                    saveFileDialog.Filter = "PDF Files|*.pdf"
                    saveFileDialog.Title = "Save PDF File"

                    ' Check if the user selected a file location
                    If saveFileDialog.ShowDialog() = DialogResult.OK Then
                        ' Use the StreamWriter to write the PDF content
                        Using streamWriter As New StreamWriter(saveFileDialog.FileName)
                            ' Write the headers to the PDF
                            For Each column As DataGridViewColumn In Guna2DataGridView1.Columns
                                streamWriter.Write(column.HeaderText & vbTab)
                            Next
                            streamWriter.WriteLine()

                            ' Write the data rows to the PDF
                            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                                For Each cell As DataGridViewCell In row.Cells
                                    streamWriter.Write(cell.Value?.ToString() & vbTab)
                                Next
                                streamWriter.WriteLine()
                            Next
                        End Using

                        ' Notify the user that the PDF has been created
                        MessageBox.Show($"PDF created successfully: {saveFileDialog.FileName}")
                    End If
                End Using
            Else
                MessageBox.Show("Guna2DataGridView1 is not properly initialized.")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

End Class
