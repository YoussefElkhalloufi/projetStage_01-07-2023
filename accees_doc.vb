Imports System.Data.SqlClient
Imports System.IO

Public Class accees_doc
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim strexit As DialogResult
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; initial catalog=gestion_architecture; integrated security=true;"
            cnx.Open()

            Dim cmd As New SqlCommand()
            cmd.Connection = cnx
            cmd.CommandText = "SELECT pathdoc FROM pathdocument WHERE n_doc = @n_doc"
            cmd.Parameters.AddWithValue("@n_doc", TextBox1.Text)

            Dim folderPath As String = cmd.ExecuteScalar()?.ToString()

            If Not String.IsNullOrEmpty(folderPath) AndAlso Directory.Exists(folderPath) Then
                Process.Start(folderPath)
            Else
                MsgBox("Dossier non trouvé pour le numero : " & TextBox1.Text, MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            cnx.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; initial catalog=gestion_architecture; integrated security=true;"
            cnx.Open()

            Dim cmd As New SqlCommand()
            cmd.Connection = cnx
            cmd.CommandText = "SELECT pathdoc FROM pathdocument as p, documents as d 
                                where p.n_doc = d.n_doc and d.titre_doc = '" & TextBox2.Text & "'"

            Dim folderPath As String = cmd.ExecuteScalar()?.ToString()

            If Not String.IsNullOrEmpty(folderPath) AndAlso Directory.Exists(folderPath) Then
                Process.Start(folderPath)
            Else
                MsgBox("Dossier non trouvé pour le titre :  " & TextBox2.Text, MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            cnx.Close()
        End Try
    End Sub

End Class