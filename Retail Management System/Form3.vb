Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        LoadReportTable()
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

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Me.Hide()
        Form2.Show()
    End Sub
End Class
