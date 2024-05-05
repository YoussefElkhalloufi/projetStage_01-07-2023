Imports System.Data.SqlClient

Public Class ajouter_facture
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'La fonctionnalité de ce button est de afficher un message de confirmation a l'utilisateur
        'et si l'utilisateur a appuie sur oui, il va sortir de l'application 
        Dim strexit As DialogResult
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or TextBox4.Text = Nothing Or TextBox5.Text = Nothing Then
            MsgBox("Un (plusieurs) champ(s) au dessous est (sont) vide(s), veuillez le(s) Remlir(s)", 0 + 64, "attention")
        Else
            Try
                cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
                cnx.Open()
                cmd.Connection = cnx
                cmd.CommandText = "insert into facture (n_facture, planid, id_fournisseur, cin, prix_ht) values (@n_facture, @planid, @id_fournisseur, @cin, @prix_ht)"
                cmd.Parameters.AddWithValue("@n_facture", TextBox1.Text)
                cmd.Parameters.AddWithValue("@planid", TextBox2.Text)
                cmd.Parameters.AddWithValue("@id_fournisseur", TextBox3.Text)
                cmd.Parameters.AddWithValue("@cin", TextBox4.Text)
                cmd.Parameters.AddWithValue("@prix_ht", TextBox5.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Facture Ajouté avec succées.", 0 + 64, "MESSAGE")
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnx.Close()
            End Try
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim facture As New Facture()
        facture.Show()
        Me.Hide()
    End Sub
End Class