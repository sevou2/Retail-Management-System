Imports MySql.Data.MySqlClient

Public Class Form2
    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize controls visibility
        InitializeControlsVisibility()
    End Sub

    Private Sub InitializeControlsVisibility()
        ' Hide the controls initially
        Guna2Button4.Visible = False
        Guna2Button5.Visible = False
        Guna2Button10.Visible = False
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False
        Guna2GroupBox4.Visible = False
        Guna2GroupBox1.Visible = False ' Add this line to hide Guna2GroupBox1
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Show Guna2Button4 when Guna2Button1 is clicked
        Guna2Button4.Visible = True
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        ' Show Guna2GroupBox2 when Guna2Button4 is clicked
        Guna2GroupBox2.Visible = True
    End Sub
    Private Sub Guna2Button12_Click(sender As Object, e As EventArgs) Handles Guna2Button12.Click
        Try
            Dim pname As String = Guna2TextBox1.Text
            Dim pqtyStr As String = Guna2TextBox2.Text
            Dim pqty As Integer

            If Not Integer.TryParse(pqtyStr, pqty) Then
                MessageBox.Show("Please enter a valid quantity.")
                Return
            End If

            Using connection As New MySqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO product (pname, pqty) VALUES (@pname, @pqty)"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@pname", pname)
                    cmd.Parameters.AddWithValue("@pqty", pqty)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Product added successfully.")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2GroupBox3.Visible = True
        Guna2Button5.Visible = True
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Try
            Using connection As New MySqlConnection(connectionString)
                ' Open the database connection
                connection.Open()

                ' The SQL query to fetch all records from the product table
                Dim query As String = "SELECT * FROM product"

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
    Private Sub Guna2Button13_Click(sender As Object, e As EventArgs) Handles Guna2Button13.Click
        UpdateQuantity(1)
    End Sub
    Private Sub Guna2Button14_Click(sender As Object, e As EventArgs) Handles Guna2Button14.Click
        UpdateQuantity(-1)
    End Sub
    Private Sub Guna2Button9_Click(sender As Object, e As EventArgs) Handles Guna2Button9.Click
        DeleteProduct()
    End Sub
    ' Function to update quantity (add or remove)
    Private Sub UpdateQuantity(quantityChange As Integer)
        ' Check if any row is selected
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = Guna2DataGridView1.SelectedRows(0)

            ' Get the current quantity value from the selected row
            Dim currentQty As Integer = Convert.ToInt32(selectedRow.Cells("pqty").Value)

            ' Update the quantity with the change
            currentQty += quantityChange

            ' Ensure the quantity doesn't go below zero
            If currentQty < 0 Then
                MessageBox.Show("Quantity cannot be less than zero.")
                Return
            End If

            ' Update the DataGridView with the new quantity value
            selectedRow.Cells("pqty").Value = currentQty

            ' Optionally, you can update the database with the new quantity value here
            ' UpdateDatabaseWithNewQuantity(selectedRow.Cells("pid").Value, currentQty)
        Else
            MessageBox.Show("Please select a row to update.")
        End If
    End Sub

    ' Function to delete the selected product
    Private Sub DeleteProduct()
        ' Check if any row is selected
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = Guna2DataGridView1.SelectedRows(0)

            ' Optionally, you can get the product ID for further processing
            ' Dim productId As Integer = Convert.ToInt32(selectedRow.Cells("pid").Value)

            ' Remove the selected row from the DataGridView
            Guna2DataGridView1.Rows.Remove(selectedRow)

            ' Optionally, you can delete the product from the database here
            ' DeleteProductFromDatabase(productId)
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
    End Sub

End Class