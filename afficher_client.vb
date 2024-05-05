Imports System.Data.SqlClient

Public Class afficher_client
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()
    Dim reader As SqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from client"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun client a afficher!!!", 0 + 64, "MESSAGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim strexit As DialogResult
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from client where CIN = '" & TextBox1.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun client a afficher avec LE CIN = " & TextBox1.Text & "!!!", 0 + 64, "MESSAGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from client where sexe = '" & ComboBox1.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun client a afficher!!!", 0 + 64, "MESSAGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from client where nom_complet = '" & TextBox2.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun client a afficher avec le nom complet = " & TextBox2.Text & "!!!", 0 + 64, "MESSAGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = " select * from client where adresse = '" & TextBox3.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun client a afficher ayant l'adresse =" & TextBox3.Text, 0 + 64, "MESSAGE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()
        End Try
    End Sub
End Class