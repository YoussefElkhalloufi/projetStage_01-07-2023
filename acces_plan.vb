Imports System.Data.SqlClient
Imports System.IO

Public Class acces_plan
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()
    Dim reader As SqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Ce button affiche les informations des plans dans un datagridview




        Try
            cnx.ConnectionString = "data source = LAPTOP-2KV6E5EH\SQLEXPRESS;
                                    initial catalog = gestion_architecture;
                                    integrated security = true;"

            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from plans where TF ='" & TextBox1.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun plan à afficher avec le TF = " & TextBox1.Text, 0 + 32, "Attention!!!")
            End If

        Catch ex As Exception
            MsgBox("Une erreur s'est produite lors de l'affichage DES DONNEES!!! ((" & ex.Message & "))", 0 + 64, "Attention!!!")
        Finally
            cnx.Close()
            reader.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim strexit As DialogResult
        'strexit = MessageBox.Show("voulez vous vraiment fermer L'application? ", "CONFIRMATION", MessageBoxButtons.YesNo)
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
        DataGridView1 = Nothing
        TextBox1.Text = ""
        TextBox2.Text = ""

    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'ce button affiche les photos du plan desirer 




        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS;
                                        initial catalog=gestion_architecture;
                                        integrated security=true;"
            cnx.Open()


            cmd.Connection = cnx

            cmd.CommandText = "select photoname from planphotos as p, plans as p1 where p.planid =p1.planid and TF ='" & TextBox1.Text & "'"
            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    Dim fileName As String = reader("photoname")
                    'MsgBox(fileName)
                    If File.Exists("C:\PhotoFolder\" + fileName) Then
                        Process.Start("C:\PhotoFolder\" + fileName) ' Open the photo with the default image viewer
                    Else
                        MsgBox("Fichier NON Trouvé: " & "C:\PhotoFolder\" + fileName, 0 + 48, "ERREUR!!!")
                    End If
                End While
            Else
                MsgBox("Aucun PHOTOS ENREGISTRER SOUS LE TF = " & TextBox1.Text, 0 + 32, "Attention!!!")
            End If

        Catch ex As Exception
            MsgBox("une erreur s'est produite LORS de L'AFFICHAGE DES PHOTOS (( " & ex.Message & " ))", 0 + 64, "Attention!!!")
        Finally
            cnx.Close()
            reader.Close()

        End Try



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'pour afficher les données dans le datagridview au dessous du button
        ' AVEC un TM donné par l'utilisateur

        Try
            cnx.ConnectionString = "data source = LAPTOP-2KV6E5EH\SQLEXPRESS;
                                    initial catalog = gestion_architecture;
                                    integrated security = true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select * from plans where TM ='" & TextBox2.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView1.DataSource = t
            Else
                MsgBox("Aucun plan à afficher avec le TM = " & TextBox2.Text, 0 + 32, "Attention!!!")
            End If

        Catch ex As Exception
            MsgBox("Une erreur s'est produite lors de l'affichage DES DONNEES!!! (( " & ex.Message & " ))", 0 + 64, "Attention!!!")
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

            cmd.CommandText = "select photoname from planphotos as p, plans as p1 where p.planid = p1.planid and TM ='" & TextBox2.Text & "'"

            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    Dim fileName As String = reader("photoname")
                    If File.Exists("C:\PhotoFolder\" + fileName) Then
                        Process.Start("C:\PhotoFolder\" + fileName) ' Open the photo with the default image viewer
                    Else
                        MsgBox("fichier NON TROUVE: (( " & "C:\PhotoFolder\" + fileName & " ))", 0 + 48, "Attention!!!")
                    End If
                End While
            Else
                MsgBox("Aucun PHOTOS ENREGISTRER SOUS LE TM = " & TextBox2.Text, 0 + 32, "Attention!!!")
            End If

        Catch ex As Exception
            MsgBox("une ERREUR s'est produite lors de l'affichage des photos: ((" & ex.Message & "))", 0 + 64, "Attention!!!")
        Finally
            reader.Close()
            cnx.Close()
        End Try

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'ce button affiche les PDFs du plan
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; initial catalog=gestion_architecture; integrated security=true;"
            cnx.Open()

            cmd.Connection = cnx
            cmd.CommandText = "select pdfpath from planpdfs as p, plans as p1 
                            where p.planid = p1.planid and tf='" & TextBox1.Text & "'"

            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    Dim fileName As String = reader("pdfpath").ToString()
                    Dim pdfPath As String = "C:\planPDFs\" + fileName

                    If File.Exists(pdfPath) Then
                        Process.Start(pdfPath) ' Open the PDF with the default PDF viewer
                    Else
                        MsgBox("FICHIER non trouvé : (( " & pdfPath & " ))", 0 + 48, "Attention!!!")
                    End If
                End While
            Else
                MsgBox("Aucun PDF a afficher avec le  TF = " & TextBox1.Text, 0 + 32, "Attention!!!")
            End If



        Catch ex As Exception
            MsgBox("UNE ERREUR s'est produite lors de l'affichage des PDFs: (( " & ex.Message & " ))", 0 + 64, "ERREUR!!!")
        Finally
            reader.Close()
            cnx.Close()

        End Try

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            cnx.ConnectionString = "data source =LAPTOP-2KV6E5EH\SQLEXPRESS;
                                    initial catalog = gestion_architecture;
                                    integrated security = true;"
            cnx.Open()
            cmd.Connection = cnx
            cmd.CommandText = "select pdfpath from planpdfs as p, plans as p1 where p.planid = p1.planid and tm ='" & TextBox2.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows Then
                While reader.Read
                    Dim filename As String = reader("pdfpath").ToString()
                    Dim pdfpath As String = "c:\planPDFs\" + filename
                    If File.Exists(pdfpath) Then
                        Process.Start(pdfpath) ' Open the PDF with the default PDF viewer
                    Else
                        MsgBox("FICHIER non trouvé: (( " & pdfpath & " ))", 0 + 48, "Attention!!!")
                    End If
                End While
            Else
                MsgBox("Aucun PDF a afficher avec le  TM = " & TextBox2.Text, 0 + 32, "Attention!!!")
            End If
        Catch ex As Exception
            MsgBox("UNE erreur s'est produite lors de l'affichage des PDFs: (( " & ex.Message & " ))", 0 + 64, "ERREUR!!!")
        Finally
            cnx.Close()
            reader.Close()
        End Try
    End Sub
End Class