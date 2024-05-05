
Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "admin" And TextBox2.Text <> "admin" Then
            MsgBox("Mot de passe INCORRECT!!!", 0 + 64, "ATTENTION")
        ElseIf TextBox1.Text <> "admin" And TextBox2.Text = "admin" Then
            MsgBox("identifiant INCORRECT!!!", 0 + 64, "ATTENTION")
        ElseIf TextBox1.Text = "admin" And TextBox2.Text = "admin" Then
            Dim accueil As New Accueil()
            accueil.Show()
            Me.Hide()
        Else
            MsgBox("Identifiant ET mot de passe sont incorrect", 0 + 64, "attention")
        End If
    End Sub

    Private Sub button2_click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strexit As DialogResult
        'strexit = MessageBox.Show("voulez vous vraiment fermer L'application? ", "CONFIRMATION", MessageBoxButtons.YesNo)
        strexit = MsgBox("voulez vous vraiment fermer L'application?", 3 + 64, "CONFIRMATION")
        If strexit = vbYes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Application.Exit()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
