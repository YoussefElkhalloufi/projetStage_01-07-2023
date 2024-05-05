Imports System.Data.SqlClient
Imports System.IO



Public Class plans
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'La fonctionnalité de ce button est de DONNER la main a lutilisateur a choisir des photos
        'et les affichés dans le panel au dessus

        Try
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Multiselect = True
            openFileDialog.Filter = "Fichiers image (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                For Each filePath As String In openFileDialog.FileNames
                    '  L'ajoute des photos à  les afficher dans une zone de prévisualisation

                    Dim pictureBox As New PictureBox()
                    pictureBox.ImageLocation = filePath
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                    pictureBox.Width = 100
                    pictureBox.Height = 100
                    ' Ajoutez le PictureBox à votre zone de prévisualisation (par exemple, un Panel)
                    panelpreview.Controls.Add(pictureBox)
                Next
            End If
        Catch ex As Exception
            MsgBox("Une ERREUR s'est produite lors du selectionnement DES PHOTOS (( " & ex.Message & " ))")
        End Try

    End Sub

    Private selectedPhotoPaths As New List(Of String)()

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'La fonctionnalité de ce button est d'ajouter les informations ecrivé a les textbox
        'dans la base de données et les tables qu'ils convient
        'il ajoute les photos selectionner par lutilisateur dans la base de données et les 
        'stocker aussi dans un dossier nommé planpotos qui se retrouve dans le disque 
        '3) il donne la main a l'utilisateur de choisir des pdfs et les ajouter a la base de 
        'données et un dossier planpdfs qui se retrouve dans le disque C
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or TextBox4.Text = Nothing Or TextBox5.Text = Nothing Or TextBox6.Text = Nothing Or TextBox7.Text = Nothing Or TextBox8.Text = Nothing Or TextBox9.Text = Nothing Or ComboBox1.Text = Nothing Or ComboBox2.Text = Nothing Then
            MsgBox("Un (plusieurs) champ(s) au dessous est (sont) vide(s), veuillez le(s) Remlir(s)", 0 + 64, "attention")
        Else
            Try
                Using cnx As New SqlConnection("data source= LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                            initial catalog=gestion_architecture; 
                                            integrated security=true;")
                    cnx.Open()

                    Dim sql As String = "INSERT INTO plans (planid, TF, TM, situation, surface_m2, dessiné_par, verifie_par, date, projet, maitre_d_ouvrage, client)
                             VALUES (@planid, @TF, @TM, @situation, @surface_m2, @dessiné_par, @verifie_par, @date, @projet, @maitre_d_ouvrage, @client)"
                    Using command As New SqlCommand(sql, cnx)
                        command.Parameters.AddWithValue("@planid", TextBox1.Text)
                        command.Parameters.AddWithValue("@TF", TextBox2.Text)
                        command.Parameters.AddWithValue("@TM", TextBox3.Text)
                        command.Parameters.AddWithValue("@situation", TextBox5.Text)
                        command.Parameters.AddWithValue("@surface_m2", TextBox4.Text)
                        command.Parameters.AddWithValue("@dessiné_par", ComboBox1.Text)
                        command.Parameters.AddWithValue("@verifie_par", ComboBox2.Text)
                        command.Parameters.AddWithValue("@date", TextBox6.Text)
                        command.Parameters.AddWithValue("@projet", TextBox7.Text)
                        command.Parameters.AddWithValue("@maitre_d_ouvrage", TextBox8.Text)
                        command.Parameters.AddWithValue("@client", TextBox9.Text)

                        command.ExecuteNonQuery()
                        ' Liste pour stocker les chemins des photos sélectionnées
                        Dim selectedPhotoPaths As New List(Of String)()

                        ' Parcourir les contrôles enfants du panelpreview
                        For Each control As Control In panelpreview.Controls
                            ' Vérifier si le contrôle est un PictureBox
                            If TypeOf control Is PictureBox Then
                                Dim pictureBox As PictureBox = DirectCast(control, PictureBox)
                                ' Récupérer le chemin d'image associé au PictureBox
                                Dim imagePath As String = pictureBox.ImageLocation
                                ' Ajouter le chemin à la liste des photos sélectionnées
                                selectedPhotoPaths.Add(imagePath)
                            End If
                        Next

                        'Parcourir les chemins de fichiers des photos sélectionnées
                        For Each filePath As String In selectedPhotoPaths
                            ' Générer un nom de fichier unique pour la photo
                            Dim fileName As String = Guid.NewGuid().ToString() + ".jpg"
                            ' Construire le chemin de destination pour la photo
                            Dim destinationPath As String = "C:\PhotoFolder\" + fileName
                            ' Copier la photo vers le dossier de destination
                            File.Copy(filePath, destinationPath)

                            ' Insérer le chemin de la photo dans la table planphotos
                            Dim photoSql As String = "INSERT INTO planphotos (planid, photoname) VALUES (@PlanID, @photoname)"
                            Using photoCommand As New SqlCommand(photoSql, cnx)
                                photoCommand.Parameters.AddWithValue("@PlanID", TextBox1.Text)
                                photoCommand.Parameters.AddWithValue("@photoname", fileName)
                                photoCommand.ExecuteNonQuery()
                            End Using
                        Next

                        'selectionnement DES PDFs, au meme temps LES STOCKER dans la base de données et dans le dossier " planPDFs "

                        Dim openFileDialog As New OpenFileDialog()
                        openFileDialog.Multiselect = True
                        openFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf"

                        If openFileDialog.ShowDialog() = DialogResult.OK Then
                            ' Parcourir les fichiers PDF sélectionnés
                            For Each filePath As String In openFileDialog.FileNames
                                ' Générer un nom de fichier unique pour le PDF
                                Dim fileName As String = Guid.NewGuid().ToString() + ".pdf"
                                ' Construire le chemin de destination pour le PDF
                                Dim destinationPath As String = "C:\planPDFs\" + fileName
                                ' Copier le PDF vers le dossier de destination
                                File.Copy(filePath, destinationPath)

                                ' Insérer le chemin du PDF dans la table planPDFs
                                Dim pdfSql As String = "INSERT INTO planPDFs (planid, pdfpath) VALUES (@PlanID, @pdfpath)"
                                Using pdfCommand As New SqlCommand(pdfSql, cnx)
                                    pdfCommand.Parameters.AddWithValue("@PlanID", TextBox1.Text)
                                    pdfCommand.Parameters.AddWithValue("@pdfpath", fileName)
                                    pdfCommand.ExecuteNonQuery()
                                End Using
                            Next
                        End If
                        MsgBox("PLAN ajouté avec succès", 0 + 64, "MESSAGE")
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erreur lors de l'ajout des informations du plan: " & ex.Message, 0 + 16, "ERREUR")
            Finally
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                panelpreview.Controls.Clear()
                ComboBox1.Text = ""
                ComboBox2.Text = ""
            End Try
        End If




    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'pour quitter l'application 



        Dim strexit As DialogResult
        'strexit = MessageBox.Show("voulez vous vraiment fermer L'application? ", "CONFIRMATION", MessageBoxButtons.YesNo)
        strexit = MsgBox("Vous Voulez vraiment QUITTER lapplication", 3 + 64, "Confirmation")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Revenir a la page ( ou la forme ) precedente
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub
End Class