Imports MySql.Data.MySqlClient

Public Class Form2
    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize controls visibility
        InitializeControlsVisibility()
    End Sub

    Private Sub InitializeControlsVisibility()
        ' Hide the controls initially
        Guna2Button4.Visible = False ' Assuming you want this hidden at start; adjust as needed
        Guna2Button5.Visible = False
        Guna2Button10.Visible = False
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False
        Guna2GroupBox4.Visible = False
        Guna2GroupBox1.Visible = False
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
        Dim pname As String = Guna2TextBox1.Text
        Dim pqtyStr As String = Guna2TextBox2.Text
        Dim pqty As Integer

        If Not Integer.TryParse(pqtyStr, pqty) Then
            MessageBox.Show("Please enter a valid quantity.")
            Return
        End If

        Try
            Using connection As New MySqlConnection(connectionString)
                Dim query As String = "INSERT INTO product (pname, pqty) VALUES (@pname, @pqty)"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@pname", pname)
                    cmd.Parameters.AddWithValue("@pqty", pqty)

                    connection.Open()
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

End Class