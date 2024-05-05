Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Windows.Markup
Imports System.Xml.XPath

Public Class ajouter_document
    Dim cnx As New SqlConnection
    Dim cmd As New SqlCommand()
    Dim reader As SqlDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'THE MAIN FUNCTION OF THIS BUTTON IS, FIRST OF ALL, give the user
        'the hand to select a FOLDER by his choice, SECOND OF ALL, copy
        'the ffolder name in a folder named c:\pathdoc, AND THEN insert its name
        'in the table pathdocument TO LET the user AFTERWARDS, INSERT n° folder
        'or its name, and then the application open that folder by its self 

        'le button doit etre fixé as soon as possible 

        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Then
            MsgBox("Un (plusieurs) champ(s) au dessous est (sont) vide(s), veuillez le(s) Remlir(s)", 0 + 64, "attention")
        Else
            Try
                cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;"
                cnx.Open()
                cmd.Connection = cnx
                cmd.CommandText = "insert into documents (n_doc , titre_doc) values (@n_doc, @titre_doc)"
                cmd.Parameters.AddWithValue("@n_doc", TextBox1.Text)
                cmd.Parameters.AddWithValue("@titre_doc", TextBox2.Text)
                cmd.ExecuteNonQuery()

                Dim openFolderDialog As New FolderBrowserDialog()
                openFolderDialog.ShowNewFolderButton = True


                If openFolderDialog.ShowDialog() = DialogResult.OK Then
                    Dim sourcefolder As String = openFolderDialog.SelectedPath
                    Dim destinationfolder As String = Path.Combine("c:\pathdoc", TextBox1.Text)

                    Directory.CreateDirectory(destinationfolder)
                    My.Computer.FileSystem.CopyDirectory(sourcefolder, destinationfolder)

                    Dim pdfsql As String = "insert into PathDocument (pathdoc, n_doc) Values (@pathdoc, @n_doc )"

                    Using pdfCommand As New SqlCommand(pdfsql, cnx)
                        pdfCommand.Parameters.AddWithValue("@pathdoc", destinationfolder)
                        pdfCommand.Parameters.AddWithValue("@n_doc", TextBox1.Text)
                        pdfCommand.ExecuteNonQuery()
                    End Using
                End If
                MsgBox("Dossier ajouté avec sccees !!!", 0 + 64, "MESSAGE")
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnx.Close()

            End Try
        End If



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' La fonctionnalité de ce button est de revenir a la page precedente
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'La fonctionnalité de ce button est de afficher un message de confirmation a l'utilisateur
        'et si l'utilisateur a appuie sur oui, il va sortir de l'application 
        Dim strexit As DialogResult
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub
End Class