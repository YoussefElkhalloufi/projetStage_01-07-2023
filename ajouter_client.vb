Imports System.Data.SqlClient

Public Class ajouter_client
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strexit As DialogResult
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or ComboBox1.Text = Nothing Then
            MsgBox("Un (plusieurs) champ(s) au dessous est (sont) vide(s), veuillez le(s) Remlir(s)", 0 + 64, "attention")
        Else
            Try
                cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
                cnx.Open()
                cmd.Connection = cnx
                cmd.CommandText = "insert into client (CIN, sexe, nom_complet, adresse) values (@CIN, @sexe, @nom_complet, @adresse)"

                cmd.Parameters.AddWithValue("@CIN", TextBox1.Text)
                cmd.Parameters.AddWithValue("@sexe", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@nom_complet", TextBox2.Text)
                cmd.Parameters.AddWithValue("@adresse", TextBox3.Text)

                cmd.ExecuteNonQuery()
                MsgBox("Client ajouté avec succees", 0 + 64, "MESSAGE")

                TextBox1.Text = ""
                ComboBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnx.Close()
            End Try
        End If

    End Sub
End Class