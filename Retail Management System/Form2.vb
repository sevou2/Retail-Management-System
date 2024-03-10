Imports MySql.Data.MySqlClient

Public Class Form2



    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize controls visibility
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
    Private Sub Guna2Button9_Click(sender As Object, e As EventArgs) Handles Guna2Button9.Click
        DeleteProduct()
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

    Private Sub Guna2Button15_Click(sender As Object, e As EventArgs) Handles Guna2Button15.Click
        Try
            ' Ensure the DataGridView is not in edit mode to commit any pending changes
            Guna2DataGridView1.EndEdit()

            ' Get the DataTable from the DataGridView's DataSource
            Dim dt As DataTable = DirectCast(Guna2DataGridView1.DataSource, DataTable)

            ' Check if there are any changes to commit
            If dt.GetChanges() IsNot Nothing Then
                Using connection As New MySqlConnection(connectionString)
                    ' Create a data adapter with the SELECT, INSERT, UPDATE, and DELETE commands
                    Dim da As New MySqlDataAdapter("SELECT * FROM product", connection)
                    Dim cb As New MySqlCommandBuilder(da)

                    ' Update the database with the changes in the DataTable
                    da.Update(dt)

                    ' Notify the user that changes have been saved
                    MessageBox.Show("Changes saved successfully.")
                End Using
            Else
                MessageBox.Show("No changes to save.")
            End If
        Catch ex As MySqlException
            MessageBox.Show($"MySQL Error: {ex.Message}")
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub
    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        RegisterOrUpdateUser()
    End Sub
    Private Sub RegisterOrUpdateUser()
        Dim username As String = Guna2TextBox6.Text
        Dim password As String = Guna2TextBox5.Text

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Check if the username already exists in the users table
                Dim userExistsQuery As String = "SELECT COUNT(*) FROM users WHERE username = @username"
                Using userExistsCmd As New MySqlCommand(userExistsQuery, connection)
                    userExistsCmd.Parameters.AddWithValue("@username", username)
                    Dim userCount As Integer = Convert.ToInt32(userExistsCmd.ExecuteScalar())

                    If userCount > 0 Then
                        ' User already exists, update the password
                        Dim updatePasswordQuery As String = "UPDATE users SET password = @password WHERE username = @username"
                        Using updatePasswordCmd As New MySqlCommand(updatePasswordQuery, connection)
                            updatePasswordCmd.Parameters.AddWithValue("@password", password)
                            updatePasswordCmd.Parameters.AddWithValue("@username", username)
                            updatePasswordCmd.ExecuteNonQuery()
                            MessageBox.Show("Password updated successfully.")
                        End Using
                    Else
                        ' User does not exist, insert a new user
                        Dim insertUserQuery As String = "INSERT INTO users (username, password) VALUES (@username, @password)"
                        Using insertUserCmd As New MySqlCommand(insertUserQuery, connection)
                            insertUserCmd.Parameters.AddWithValue("@username", username)
                            insertUserCmd.Parameters.AddWithValue("@password", password)
                            insertUserCmd.ExecuteNonQuery()
                            MessageBox.Show("User registered successfully.")
                        End Using
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

End Class