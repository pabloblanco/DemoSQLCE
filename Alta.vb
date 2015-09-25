Imports System.Data
Imports System.Data.SqlServerCe
Imports System.IO

Public Class Alta

    Dim inst As New AztecaSql()
    Dim leido As Boolean = False  ' Bandera para indicar todo validado

    'Private Sub Alta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus

    'Me.Show()
    'txtCodigo.Text = ""
    'txtCant.Text = ""
    'txtCodigo.Focus()

    'End Sub

    Public Sub Alta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Show()
        txtCodigo.Text = ""
        txtCant.Text = ""
        txtCodigo.Focus()

    End Sub

    ' Boton OK
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim i As Integer
        Dim cadena As String

        If txtCodigo.Text <> "" And txtCant.Text <> "" Then

            'If leido Then
            tdata = True
            For i = 0 To 17
                indata(i) = ""
            Next
            indata(4) = txtCodigo.Text

            If (txtCant.TextLength > 0) Then
                'cadena = txtCant.Text
                'cadena = cadena.Substring(0, cadena.Length - 2)
                'txtCant.Text = cadena
                If IsNumeric(txtCant.Text) Then
                    If Val(txtCant.Text) > 0 And Val(txtCant.Text) <= 99999999 Then
                        Cantidad = txtCant.Text
                        leido = True
                    Else
                        Module1.sound.Play()
                        MsgBox("Cantidad fuera de rango! (1 - 99999999)")
                        txtCant.Text = ""
                        txtCant.Focus()
                    End If
                Else
                    Module1.sound.Play()
                    MsgBox("Introduzca un numero!")
                    txtCant.Text = ""
                    txtCant.Focus()
                End If
            End If

            If leido Then
                inst.convertToSQL()
                leido = False
                conta = conta + 1           ' Contador de procuctos
                Inventario.Label8.Text = conta.ToString
                Sale()
            End If

            'Else
            '    Module1.sound.Play()
            '    MsgBox("Presione ENTER en Cantidad para Validar!!")
            '    txtCant.Focus()
            'End If

        Else
            Module1.sound.Play()
            MsgBox("Indique todos los datos para continuar")
            'If txtCodigo.Text = "" Then
            '    txtCodigo.Focus()
            'End If
            'If txtCant.Text = "" Then
            '    txtCant.Focus()
            'End If
        End If
    End Sub

    Public Sub Sale()
        If flagModulo = True Then
            Inventario.Label9.Text = txtCodigo.Text
            Inventario.Focus()
            Inventario.Show()
            Me.Hide()
        Else
            Almacen.Label9.Text = txtCodigo.Text
            Almacen.Focus()
            Almacen.Show()
            Me.Hide()
        End If
    End Sub

    ' Boton Cancelar
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Sale()

    End Sub

    Private Sub txtCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        Dim lastchar As Char
        Dim cadena As String

        If (txtCodigo.TextLength > 0) Then
            lastchar = txtCodigo.Text(txtCodigo.TextLength - 1)
            If (lastchar = Chr(10) Or lastchar = Chr(13)) Then
                cadena = txtCodigo.Text
                cadena = cadena.Substring(0, cadena.Length - 2)
                txtCodigo.Text = cadena

                'txtCant.Enabled = True
                txtCant.Focus()

            End If
        End If
    End Sub

    Private Sub txtCant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCant.TextChanged
        Dim lastchar As Char
        Dim cadena As String
        Dim i As Integer

        If (txtCant.TextLength > 0) Then
            lastchar = txtCant.Text(txtCant.TextLength - 1)
            If lastchar = Chr(10) Or lastchar = Chr(13) Then
                cadena = txtCant.Text
                cadena = cadena.Substring(0, cadena.Length - 2)
                txtCant.Text = cadena
                If IsNumeric(txtCant.Text) Then
                    If Val(txtCant.Text) > 0 And Val(txtCant.Text) <= 99999999 Then
                        Cantidad = txtCant.Text
                        'leido = True
                        'If leido Then

                        'If tdata = False Then
                        '    For i = 0 To 17
                        '        indata(i) = ""
                        '    Next
                        'Else
                        For i = 0 To (indata.Length - 1)   '20
                            indata(i) = ""
                        Next
                        'End If
                        indata(4) = txtCodigo.Text

                        tdata = True

                        inst.convertToSQL()
                        leido = False
                        conta = conta + 1           ' Contador de procuctos
                        Inventario.Label8.Text = conta.ToString
                        Sale()
                        'End If
                    Else
                        Module1.sound.Play()
                        MsgBox("Cantidad fuera de rango! (1 - 99999999)")
                        txtCant.Text = ""
                        txtCant.Focus()
                    End If
                Else
                    Module1.sound.Play()
                    MsgBox("Introduzca un numero!")
                    txtCant.Text = ""
                    txtCant.Focus()
                End If
            End If
        End If
    End Sub
End Class