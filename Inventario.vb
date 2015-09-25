Imports System.IO
'Imports Microsoft.WindowsCE.Forms.InputPanel
Imports System.Text

Public Class Inventario

    Dim inst As New AztecaSql()
    Dim contador As Integer = 0
    Dim posicion As Integer = 0
    Dim StatusLee As Boolean = False
    Dim aux As String
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub
    
    'Private Sub Inventario_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus

    '    txtCant.Text = ""
    '    txtCodigo.Text = ""

    'End Sub

    Private Sub Inventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtCant.Text = ""
        txtCodigo.Text = ""

        posicion = 0                   ' Posicion de codigo de barras
        contador = 0                   ' Contador de campos
        txtUbica.Focus()
        
    End Sub

    ' Separa codigo leido con @
    Private Function Separa_Codigo() As Boolean

        Dim str As String = Nothing
        Dim value As Integer = txtCodigo.Text.Length
        Dim scanned As String = txtCodigo.Text

        'checks pass, now check if item has been entered!
        If txtCodigo.Text.Length > 6 Then

            If scanned(4) = "@" Then
                indata = scanned.Split("@"c)     ' Separador @
                tdata = True
                If (indata.Length <> 5) Then
                    Return True
                Else
                    Return False
                End If
            Else
                If Asc(scanned(6)) = 29 Then
                    indata = scanned.Split(Chr(29))     ' Separador 29
                    tdata = False
                    If (indata.Length <> 5) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
                'Return False
            End If
        Else
            Return False
        End If

    End Function

    'Private Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged

    '    Dim lastchar As Char
    '    Dim cadena As String

    '    If (txtCodigo.TextLength > 0) Then
    '        lastchar = txtCodigo.Text(txtCodigo.TextLength - 1)
    '        If (lastchar = Chr(10) Or lastchar = Chr(13)) And StatusLee = False Then
    '            cadena = txtCodigo.Text
    '            cadena = cadena.Substring(0, cadena.Length - 2)
    '            txtCodigo.Text = cadena
    '            If Separa_Codigo() = True Then
    '                txtCodigo.Text = cadena
    '                txtCant.Enabled = True
    '                txtCant.Focus()
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub txtCodigo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodigo.KeyUp

        If e.KeyCode = 29 Then
            If contador = 0 Then
                indata(contador) = txtCodigo.Text
                'indata.SetValue(txtCodigo.Text, contador)
                posicion = txtCodigo.Text.Length
            Else
                aux = txtCodigo.Text
                aux = txtCodigo.Text.Substring(posicion, txtCodigo.Text.Length - posicion)
                indata(contador) = txtCodigo.Text.Substring(posicion, txtCodigo.Text.Length - posicion)
                'indata.SetValue(txtCodigo.Text.Substring(posicion, txtCodigo.Text.Length), contador)
                posicion = txtCodigo.Text.Length
            End If
            contador = contador + 1
            '            StatusLee = True
            tdata = False
        End If
        If e.KeyCode = Keys.Return And txtCodigo.Text.Length > 2 Then

            'If tdata = False Then  ' Obtiene ultimo campo
            '    indata(contador) = txtCodigo.Text.Substring(posicion, txtCodigo.Text.Length - posicion)
            '    txtCant.Enabled = True
            '    txtCant.Focus()
            'End If
            If tdata = True Then   ' Si no leyo caracter 29, fue arroba
                If Separa_Codigo() = False Then
                    MsgBox("Codigo equivocado!! Intente nuevamente!")
                    txtCodigo.Text = ""
                    txtCodigo.Focus()
                Else
                    ' coloca la cantidad en pantalla
                    If tdata = True Then
                        aux = indata(8)
                        aux = aux.Substring(1)
                        txtCant.Text = aux
                    Else
                        aux = indata(7)
                        If aux(0) <> "Q" Then
                            aux = indata(6)
                        End If
                        aux = aux.Substring(1)
                        txtCant.Text = aux
                    End If
                    ' Coloca ultimo codigo leido en pantalla
                    If tdata Then
                        aux = indata(4)
                        aux = aux.Substring(1)
                        Label9.Text = aux
                    Else
                        aux = indata(3)
                        aux = aux.Substring(1)
                        Label9.Text = aux
                        'tdata = True
                    End If
                    txtCant.Enabled = True
                    txtCant.Focus()
                End If
            End If
        Else
            If e.KeyCode = Keys.Return And txtCodigo.Text.Length > 0 Then
                txtCodigo.Text = ""
            End If
        End If
    End Sub

    Private Sub txtCant_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCant.KeyDown
        'MsgBox(e.KeyCode)
        If e.KeyCode = 134 Or e.KeyCode = 13 Then 'Keys.Return Then
            If txtCant.Text.Length > 0 Then
                Procesa_Cant()
            End If

        End If
    End Sub

    Private Sub Procesa_Cant()

        If IsNumeric(txtCant.Text) Then
            If Val(txtCant.Text) > 0 And Val(txtCant.Text) <= 99999999 Then
                Cantidad = txtCant.Text
                ' guarda datos en base de datos
                inst.convertToSQL()

                txtCodigo.Text = ""
                txtCant.Text = ""
                'Label9.Text = ""
                txtCant.Enabled = False
                txtCodigo.Focus()
                conta = conta + 1           ' Contador de procuctos
                Label8.Text = conta.ToString

                tdata = True
                'StatusLee = False
                contador = 0
            Else
                Module1.sound.Play()
                MsgBox("Cantidad fuera de rango! (1 - 9999)")
                txtCant.Text = ""
                txtCant.Focus()
            End If
        Else
            Module1.sound.Play()
            MsgBox("Introduzca un numero!")
            txtCant.Text = ""
            txtCant.Focus()
        End If

    End Sub
    'Private Sub txtCant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCant.TextChanged
    '    '        Dim inst As New GessaSql
    '    Dim lastchar As Char
    '    Dim cadena As String

    '    If (txtCant.TextLength > 0) Then
    '        lastchar = txtCant.Text(txtCant.TextLength - 1)
    '        If lastchar = Chr(10) Or lastchar = Chr(13) Then
    '            cadena = txtCant.Text
    '            cadena = cadena.Substring(0, cadena.Length - 2)
    '            txtCant.Text = cadena

    '            Procesa_Cant()

    '        End If
    '    End If
    'End Sub


    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click

        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        response = MsgBox("Esta seguro de salir de Inventario Físico?", style)

        If response = MsgBoxResult.Yes Then
            Me.Hide()
        End If

    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        response = MsgBox("Esta seguro de Borrar el último registro?", style)

        If response = MsgBoxResult.Yes Then
            If conta > 0 Then
                If inst.BorraUltimo() Then
                    MsgBox("Ultimo Registro Borrado")

                    conta = conta - 1           ' Contador de procuctos
                    Label8.Text = conta.ToString

                End If
            Else
                MsgBox("No hay registro a borrar!")
            End If

        End If
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Alta.txtCant.Text = ""
        Alta.txtCodigo.Text = ""
        Alta.Show()
        Alta.Focus()
        Alta.txtCodigo.Focus()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtCant.Text = ""
        txtCant.Focus()
    End Sub

    Private Sub txtUbica_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbica.TextChanged
        Dim lastchar As Char
        Dim cadena As String

        If (txtUbica.TextLength > 0) Then
            lastchar = txtUbica.Text(txtUbica.TextLength - 1)
            If lastchar = Chr(10) Or lastchar = Chr(13) Then
                cadena = txtUbica.Text
                cadena = cadena.Substring(0, cadena.Length - 2)
                txtUbica.Text = cadena

                'txtCodigo.Enabled = True
                conta = 0    ' Iniciliza contador
                Label8.Text = conta.ToString
                txtCodigo.Focus()
            End If
        End If
    End Sub

End Class