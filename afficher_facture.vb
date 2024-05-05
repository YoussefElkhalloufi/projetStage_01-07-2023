Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class afficher_facture
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



            cmd.CommandText = "select * from facture where n_facture = '" & TextBox1.Text & "'"
            reader = cmd.ExecuteReader()
            If reader.HasRows() Then
                Dim t As New DataTable
                t.Load(reader)
                DataGridView4.DataSource = t
                cmd.CommandText = "select c.nom_complet, c.adresse, c.CIN, c.sexe from client as c, facture as f 
                               where c.cin = f.cin and f.n_facture ='" & TextBox1.Text & "'"
                reader = cmd.ExecuteReader()
                Dim c As New DataTable
                c.Load(reader)
                DataGridView3.DataSource = c
                reader.Close()


                cmd.CommandText = "select p.TF, p.tm, p.situation, p.surface_m2, p.dessiné_par, p.verifie_par, p.date, p.projet, p.maitre_d_ouvrage, p.client
                                from plans as p, facture as f 
                                where p.planid = f.planid and f.n_facture ='" & TextBox1.Text & "'"
                reader = cmd.ExecuteReader()
                Dim p As New DataTable
                p.Load(reader)
                DataGridView2.DataSource = p
                reader.Close()

                cmd.CommandText = "select f1.nom_complet, f1.adresse, f1.mail, f1.tel_fax, f1.n_patente, f1.cnss, f1.identifiant_fiscal, f1.ice 
                                from fournisseur as f1, facture as f 
                                where f1.id_fournisseur = f.id_fournisseur and f.n_facture ='" & TextBox1.Text & "'"
                reader = cmd.ExecuteReader()
                Dim f As New DataTable
                f.Load(reader)
                DataGridView1.DataSource = f

            Else
                MsgBox("Aucun facture a afficher avec le n° facture donnée: " & TextBox1.Text, 0 + 64, "ATTENTION")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnx.Close()
            reader.Close()
        End Try
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



    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim facture As New Facture()
        facture.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            cnx.ConnectionString = "data source=LAPTOP-2KV6E5EH\SQLEXPRESS; 
                                    initial catalog=gestion_architecture; 
                                    integrated security=true;"
            cnx.Open()
            cmd.Connection = cnx
            ' Créer un nouveau document PDF
            Dim document As New Document(PageSize.A4, 20, 20, 20, 20)
            Dim numero_facture As String = TextBox1.Text
            numero_facture = numero_facture.Replace("/", "_")
            Dim pdfFileName As String = "facture(" & numero_facture & ").pdf" ' Name of the PDF file
            Dim pdfPath As String = Path.Combine("C:\facture_pdf\", pdfFileName)




            Using fs As New FileStream(pdfPath, FileMode.Create)
                Dim writer As PdfWriter = PdfWriter.GetInstance(document, fs)
                document.Open()

                Dim cb As PdfContentByte = writer.DirectContent

                ' Set the font for the text
                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                cb.SetFontAndSize(font, 12)

                ' Add the "oujda le : " text at the top right of the page
                Dim oujdaText As String = "oujda le :"
                cb.BeginText()
                cb.SetTextMatrix(document.PageSize.Width - 180, document.PageSize.Height - 40)
                cb.ShowText(oujdaText)
                cb.EndText()

                ' Add today's date at the top right, after the "oujda le : " text
                Dim todayDate As String = DateTime.Now.ToString("dd/MM/yyyy")
                cb.BeginText()
                cb.SetTextMatrix(document.PageSize.Width - 100, document.PageSize.Height - 40)
                cb.ShowText(todayDate)
                cb.EndText()



                Dim signature As String = "Signature et cachet:"
                cb.BeginText()
                cb.SetTextMatrix(document.PageSize.Width - 250, 250) ' Adjust the X and Y coordinates as needed
                cb.ShowText(signature)
                cb.EndText()


                Dim titleFont As New iTextSharp.text.Font(iTextSharp.text.Font.ITALIC, 24, iTextSharp.text.Font.BOLD)
                Dim title As New iTextSharp.text.Paragraph("Facture N°: " & TextBox1.Text, titleFont)
                title.Alignment = iTextSharp.text.Element.ALIGN_LEFT
                document.Add(title)




                'square surrounding all the content
                cb.SetLineWidth(3) ' Set the line width of the square
                cb.Rectangle(10, 10, PageSize.A4.Width - 20, PageSize.A4.Height - 20) ' Draw a rectangle around the content area
                cb.Stroke()



                'LOGO 
                'Dim logoPath As String = "C:\Users\ye_ar\Dropbox\PC\Downloads\Capture d'écran 2023-06-30 142746.jpg.png" ' Path to the logo image
                'Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(logoPath)
                'logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT
                'logo.ScaleAbsolute(100, 100)
                'logo.SetAbsolutePosition(30, PageSize.A4.Height - 130)
                'document.Add(logo)

                Dim cb1 As PdfContentByte = writer.DirectContent
                cb1.SetFontAndSize(font, 16)

                Dim client As String = "Infos du client:"
                cb1.BeginText()
                cb1.SetTextMatrix(document.PageSize.Width - 370, 700) ' Adjust the X and Y coordinates as needed
                cb1.ShowText(client)
                cb1.EndText()

                Dim plan As String = "Infos du Plan:"
                cb1.BeginText()
                cb1.SetTextMatrix(document.PageSize.Width - 370, 600) ' Adjust the X and Y coordinates as needed
                cb1.ShowText(plan)
                cb1.EndText()

                Dim facture As String = "Facture:"
                cb1.BeginText()
                cb1.SetTextMatrix(document.PageSize.Width - 370, 420)
                cb1.ShowText(facture)
                cb1.EndText()


                ' Create a table to display the information
                Dim table_client As New PdfPTable(2)
                table_client.WidthPercentage = 80
                table_client.HorizontalAlignment = Element.ALIGN_CENTER

                table_client.SpacingBefore = 100

                ' Define the widths of the columns (in percentage relative to the table width)
                Dim columnWidths As Single() = {30, 70}
                table_client.SetWidths(columnWidths)

                ' Define a font with bold and increased size
                Dim boldFont As New iTextSharp.text.Font(iTextSharp.text.Font.ITALIC, 14, iTextSharp.text.Font.BOLD)

                ' Récupérer les données depuis la table FACTURE

                cmd.CommandText = "select c.nom_complet, c.adresse, c.CIN, c.sexe from client as c, facture as f 
                               where c.cin = f.cin and f.n_facture ='" & TextBox1.Text & "'" ' Ajoutez vos conditions ici

                reader = cmd.ExecuteReader()
                ' Ajouter les données au document PDF
                While reader.Read()
                        ' Supposons que vous avez des colonnes telles que 'ClientName', 'PlanName', 'Fournisseur', etc.
                        Dim nom_complet As String = reader("nom_complet").ToString()
                        Dim adresse As String = reader("adresse").ToString()
                        Dim cin_client As String = reader("CIN").ToString()
                    'Dim prix As String = reader("prix").ToString()


                    ' Add more cells for other information (planid, id_fournisseur, cin_client, prix, etc.)
                    table_client.AddCell(New PdfPCell(New Phrase("Client :", boldFont)))
                    table_client.AddCell(New PdfPCell(New Phrase(nom_complet)))

                    table_client.AddCell(New PdfPCell(New Phrase("Adresse :", boldFont)))
                    table_client.AddCell(New PdfPCell(New Phrase(adresse)))

                    table_client.AddCell(New PdfPCell(New Phrase("Cin : ", boldFont)))
                    table_client.AddCell(New PdfPCell(New Phrase(cin_client)))

                    'table.AddCell(New PdfPCell(New Phrase("Prix:", boldFont)))
                    'table.AddCell(New PdfPCell(New Phrase(prix & " DH", boldFont)))

                    ' Add the table to the document
                    document.Add(table_client)
                End While
                reader.Close()

                Dim table_plan As New PdfPTable(2)
                table_plan.WidthPercentage = 80
                table_plan.HorizontalAlignment = Element.ALIGN_CENTER

                table_plan.SpacingBefore = 40

                ' Define the widths of the columns (in percentage relative to the table width)
                table_plan.SetWidths(columnWidths)

                cmd.CommandText = "select p.TF, p.tm, p.situation, p.surface_m2, p.dessiné_par, p.verifie_par, p.date, p.projet, p.maitre_d_ouvrage, p.client
                                from plans as p, facture as f 
                                where p.planid = f.planid and f.n_facture ='" & TextBox1.Text & "'"

                reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim tf As String = reader("TF").ToString()
                    Dim tm As String = reader("tm").ToString()
                    Dim situation As String = reader("situation").ToString()
                    Dim surface_m2 As String = reader("surface_m2").ToString
                    Dim dessiné_par As String = reader("dessiné_par").ToString()
                    Dim verifie_par As String = reader("verifie_par").ToString()
                    Dim maitre_d_ouvrage As String = reader("maitre_d_ouvrage").ToString()


                    table_plan.AddCell(New PdfPCell(New Phrase("TF : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(tf)))

                    table_plan.AddCell(New PdfPCell(New Phrase("TM : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(tm)))

                    table_plan.AddCell(New PdfPCell(New Phrase("Situation : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(situation)))

                    table_plan.AddCell(New PdfPCell(New Phrase("Surface par m2 : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(surface_m2)))

                    table_plan.AddCell(New PdfPCell(New Phrase("Dessiné par : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(dessiné_par)))

                    table_plan.AddCell(New PdfPCell(New Phrase("Verifié par : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(verifie_par)))

                    table_plan.AddCell(New PdfPCell(New Phrase("Maitre d'ouvrage : ", boldFont)))
                    table_plan.AddCell(New PdfPCell(New Phrase(maitre_d_ouvrage)))

                    document.Add(table_plan)
                End While

                reader.Close()
                Dim table_FACTURE As New PdfPTable(2)
                table_FACTURE.WidthPercentage = 80
                table_FACTURE.HorizontalAlignment = Element.ALIGN_CENTER
                table_FACTURE.SpacingBefore = 50

                ' Define the heights of the rows
                Dim rowHeights As Single() = {30, 30, 30} ' Adjust the values as needed for each row

                ' Set the heights of the rows
                table_FACTURE.SetTotalWidth({30, 70})

                table_FACTURE.SetExtendLastRow(True, False)

                cmd.CommandText = "select prix_ht from facture where n_facture ='" & TextBox1.Text & "'"
                reader = cmd.ExecuteReader()
                While reader.Read
                    Dim prix_ht As String = reader("prix_ht").ToString()

                    table_FACTURE.AddCell(New PdfPCell(New Phrase("Prix total H.T : ", boldFont)) With {.MinimumHeight = rowHeights(0)})
                    table_FACTURE.AddCell(New PdfPCell(New Phrase(prix_ht & " DH")) With {.MinimumHeight = rowHeights(0)})

                    Dim taxes As Integer = prix_ht * 0.2

                    table_FACTURE.AddCell(New PdfPCell(New Phrase("TVA 20% :", boldFont)) With {.MinimumHeight = rowHeights(1)})
                    table_FACTURE.AddCell(New PdfPCell(New Phrase(taxes & " DH")) With {.MinimumHeight = rowHeights(1)})

                    Dim prix_total As Integer = prix_ht + taxes

                    table_FACTURE.AddCell(New PdfPCell(New Phrase("PRIX TTC :", boldFont)) With {.MinimumHeight = rowHeights(2)})
                    table_FACTURE.AddCell(New PdfPCell(New Phrase(prix_total & " DH")) With {.MinimumHeight = rowHeights(2)})

                    document.Add(table_FACTURE)
                End While

                Dim cb2 As PdfContentByte = writer.DirectContent
                cb2.SetLineWidth(2)
                cb2.MoveTo(10, 70) ' Start position of the line (x, y)
                cb2.LineTo(document.PageSize.Width - 10, 70) ' End position of the line (x, y)
                cb2.Stroke()

                ' Add some space between the line and the fournisseur information
                document.Add(New Paragraph(""))

                ' Add fournisseur information using ColumnText
                Dim fournisseurFont As New iTextSharp.text.Font(iTextSharp.text.Font.BOLD, 12)

                ' Start a new ColumnText object
                Dim ct As New ColumnText(cb)
                ct.Alignment = Element.ALIGN_LEFT ' Align the text to the left

                ' Define the coordinates for the text
                Dim x As Single = 15 ' X-coordinate
                Dim y As Single = -5 ' Y-coordinate
                Dim width As Single = 600 ' Width of the text area
                Dim height As Single = 80 ' Height of the text area

                ' Create a rectangle for the text area
                Dim rect As New iTextSharp.text.Rectangle(x, y, x + width, y + height)
                ct.SetSimpleColumn(rect)

                ' Add the fournisseur information
                ct.AddElement(New Paragraph("Boulevard Mohamed Derfoufi n° 122, 1er étage appt n°3 Oujda 60000   TEL/FAX : 05 36 68 64 44", fournisseurFont))
                ct.AddElement(New Paragraph("EMAIL: aqel.architecte@gmail.com ", fournisseurFont))
                ct.AddElement(New Paragraph("N° PATENTE: 10100449   CNSS: 8724826   IDENTIFIANT FISCAL: 40256772   ICE: 001547227000055", fournisseurFont))

                ' Go to the next text line
                ct.Go()

                document.Close()
                End Using

            ' Ouvrir le PDF avec le programme Adobe Acrobat Reader ou un autre lecteur de PDF
            Process.Start(pdfPath)

        Catch ex As Exception
            MsgBox("Une erreur s'est produite lors de la génération du PDF: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        Finally
            cnx.Close()
            reader.Close()
        End Try

    End Sub
End Class