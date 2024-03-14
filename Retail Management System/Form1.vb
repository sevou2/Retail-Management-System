Imports MySql.Data.MySqlClient

Public Class Form1
    Private WithEvents Guna2Button1 As New Guna.UI2.WinForms.Guna2Button() ' Declare the button WithEvents
    Private connectionString As String = "Server=localhost;Database=rm;User ID=root;Password=admin;"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Guna2Button1 here or in the designer, and set its properties
        ' For example:
        ' Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        ' Guna2Button1.Text = "Login"
        ' Guna2Button1.Size = New Size(100, 30)
        ' Add it to the form's controls collection
        ' Me.Controls.Add(Guna2Button1)
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Dim username As String = Guna2TextBox1.Text
        Dim password As String = Guna2TextBox2.Text

        If AuthenticateUser(username, password) Then
            MessageBox.Show("Login Successful!")
            Me.Hide()
            Form2.Show()
        Else
            MessageBox.Show("Invalid username or password. Please try again.")
        End If
    End Sub

    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM users WHERE username = @username AND password = @password"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' If a matching user is found in the database, authentication is successful
                            Return True
                        End If
                    End Using
                End Using
            End Using
        Catch ex As MySqlException
            ' Handle MySQL-specific exceptions
            MessageBox.Show("MySQL Error: " & ex.Message)
        Catch ex As Exception
            ' Handle other exceptions
            MessageBox.Show("Error: " & ex.Message)
        End Try

        ' If no matching user is found or an error occurs, authentication fails
        Return False
    End Function

End Class
