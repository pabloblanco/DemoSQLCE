Public Class Sale

    Dim password As String = "aztadc"
    'Dim password As String = "1"

    Private Sub Sale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TextBox1.Focus()

    End Sub

    Private Sub Sale_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        '  Bypass the instruction to Close this form
        e.Cancel = True
        '  But hide it from the user.
        Me.Hide()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp

        If e.KeyCode = Keys.Return Then
            If TextBox1.Text <> "" Then
                If Button1.Visible Then
                    Button1_Click(Me, EventArgs.Empty)
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text = password Then
            Me.Hide()
            ShowTaskbar()
            Application.Exit()
        Else
            MsgBox("Contraseña incorrecta")
            Me.Hide()
            Main.Show()
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub
End Class