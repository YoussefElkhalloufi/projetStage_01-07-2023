Public Class Facture
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
        If RadioButton1.Checked = True Then
            Dim ajouter_facture As New ajouter_facture()
            ajouter_facture.Show()
            Me.Hide()
        ElseIf RadioButton2.Checked = True Then
            Dim afficher_facture As New afficher_facture()
            afficher_facture.Show()
            Me.Hide()
        Else
            MsgBox("Vous devez cocher un des radiobuttons au dessus", 0 + 64, "ATTENTION")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' La fonctionnalité de ce button est de revenir a la page precedente
        Dim accueil As New Accueil()
        accueil.Show()
        Me.Hide()
    End Sub
End Class