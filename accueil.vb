Public Class Accueil
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strexit As DialogResult
        'strexit = MessageBox.Show("voulez vous vraiment fermer L'application? ", "CONFIRMATION", MessageBoxButtons.YesNo)
        strexit = MsgBox("voulez vous vraiment fermer L'application?", 3 + 64, "CONFIRMATION")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub


    Private Sub Accueil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button1.BackColor = Color.LightGray ' Change the button color as desired
        Button1.FlatAppearance.MouseDownBackColor = Color.Honeydew
        Button1.FlatAppearance.MouseOverBackColor = Color.Honeydew
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)


        Button2.BackColor = Color.White ' Change the button color as desired
        Button2.FlatAppearance.MouseDownBackColor = Color.IndianRed
        Button2.FlatAppearance.MouseOverBackColor = Color.IndianRed
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
        Button2.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)



        'Button3.BackColor = Color.LightGray ' Change the button color as desired
        Button3.FlatAppearance.MouseDownBackColor = Color.Honeydew
        Button3.FlatAppearance.MouseOverBackColor = Color.Honeydew
        Button3.FlatStyle = FlatStyle.Flat
        Button3.FlatAppearance.BorderSize = 0
        Button3.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)


        'Button4.BackColor = Color.LightGray ' Change the button color as desired
        Button4.FlatAppearance.MouseDownBackColor = Color.Honeydew
        Button4.FlatAppearance.MouseOverBackColor = Color.Honeydew
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0
        Button4.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)


        'Button5.BackColor = Color.LightGray ' Change the button color as desired
        Button5.FlatAppearance.MouseDownBackColor = Color.Honeydew
        Button5.FlatAppearance.MouseOverBackColor = Color.Honeydew
        Button5.FlatStyle = FlatStyle.Flat
        Button5.FlatAppearance.BorderSize = 0
        Button5.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)


        'Button6.BackColor = Color.LightGray ' Change the button color as desired
        Button6.FlatAppearance.MouseDownBackColor = Color.Honeydew
        Button6.FlatAppearance.MouseOverBackColor = Color.Honeydew
        Button6.FlatStyle = FlatStyle.Flat
        Button6.FlatAppearance.BorderSize = 0
        Button6.Font = New Font(Button1.Font.FontFamily, 12, FontStyle.Bold)
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim ajouter_documents As New ajouter_document()
        ajouter_documents.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim accees_doc As New accees_doc()
        accees_doc.Show()
        Me.Hide()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim ajouter_client As New ajouter_client()
        ajouter_client.Show()
        Me.Hide()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim afficher_client As New afficher_client()
        afficher_client.Show()
        Me.Hide()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim facture As New Facture()
        facture.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim plans As New plans()
        plans.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim accees_plan As New acces_plan()
        acces_plan.Show()
        Me.Hide()
    End Sub
End Class