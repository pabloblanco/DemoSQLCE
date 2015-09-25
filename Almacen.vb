Imports System.IO
Imports Microsoft.WindowsCE.Forms.InputPanel
Imports System.Text

Public Class Almacen

    Dim inst As New AztecaSql()
    Dim contador As Integer = 0
    Dim posicion As Integer = 0
    Dim StatusLee As Boolean = False
    Dim aux As String

    Private Sub Inventario_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus

        txtCant.Text = ""
        txtCodigo.Text = ""

    End Sub

    Private Sub Inventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtCant.Text = ""
        txtCodigo.Text = ""

        posicion = 0                   ' Posicion de codigo de barras
        contador = 0                   ' Contador de campos()
        txtUbica.Focus()
        txtCodigo.Enabled = False

    End Sub

    ' Separa codigo leido con @
    Private Function Separa_Codigo() As Boolean

        Dim str As String = Nothing
        Dim value As Integer = txtCodigo.Text.Length
        Dim scanned As String = txtCodigo.Text

        'checks pass, now check if item has been entered!
        If txtCodigo.Text <> Nothing Then

            If scanned(4) = "@" Then
                indata = scanned.Split("@"c)     ' Separador @
                tdata = True
            End If
            If (indata.Length <> 5) Then
                Return True
            Else
                Return False
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
        If e.KeyCode = Keys.Return And txtCodigo.Text.Length > 0 Then

            If tdata = False Then
                indata(contador) = txtCodigo.Text.Substring(posicion, txtCodigo.Text.Length - posicion)
            End If
            If tdata = True Then
                If Separa_Codigo() = True Then
                    'txtCodigo.Text = cadena
                    'txtCant.Enabled = True
                    'txtCant.Focus()
                End If
            End If
            txtCant.Enabled = True
            txtCant.Focus()

        End If
    End Sub

    Private Sub txtCant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCant.TextChanged
        '        Dim inst As New GessaSql
        Dim lastchar As Char
        Dim cadena As String

        If (txtCant.TextLength > 0) Then
            lastchar = txtCant.Text(txtCant.TextLength - 1)
            If lastchar = Chr(10) Or lastchar = Chr(13) Then
                cadena = txtCant.Text
                cadena = cadena.Substring(0, cadena.Length - 2)
                txtCant.Text = cadena
                If IsNumeric(txtCant.Text) Then
                    If Val(txtCant.Text) > 0 And Val(txtCant.Text) <= 9999 Then

                        Cantidad = txtCant.Text
                        ' guarda datos en base de datos
                        inst.convertToSQL()
                        'inst.updateTramo(mainInfo2.item, mainInfo2.desc, mainInfo2.precio, mainInfo2.itemint, mainInfo2.agc, mainInfo2.costo, mainInfo2.gramaje, mainInfo2.categoria)

                        txtCodigo.Text = ""
                        txtCant.Text = ""
                        txtCant.Enabled = False
                        txtCodigo.Focus()
                        ' Coloca ultimo codigo1 leido en pantalla
                        If tdata Then
                            Label9.Text = indata(2)
                        Else
                            Label9.Text = indata(1)
                            tdata = True
                        End If
                        'StatusLee = False
                        contador = 0
                    Else
                        Module1.sound.Play()
                        MsgBox("Cantidad fuera de rango! (1 - 999)")
                        txtCant.Focus()
                    End If
                Else
                    Module1.sound.Play()
                    MsgBox("Introduzca un numero!")
                    txtCant.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click

        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        response = MsgBox("Esta seguro de salir de Captura Almacén?", style)

        If response = MsgBoxResult.Yes Then
            txtCodigo.Enabled = False
            txtUbica.Text = ""
            Me.Hide()
        End If

    End Sub

    Public Function DeleteLine(ByVal fName As String, ByVal LineNumber As Long) _
     As Boolean
        'Purpose: Deletes a Line from a text file

        'Parameters: fName = FullPath to File
        '            LineNumber = LineToDelete

        'Returns:    True if Successful, false otherwise

        'Requires:   Reference to Microsoft Scripting Runtime

        'Example: DeleteLine("C:\Myfile.txt", 3)
        '           Deletes third line of Myfile.txt
        '______________________________________________________________


        '        Dim oFSO As New FileSystemObject
        '        Dim oFSTR As Scripting.TextStream
        '        Dim ret As Long
        '        Dim lCtr As Long
        '        Dim sTemp As String, sLine As String
        '        Dim bLineFound As Boolean

        '        On Error GoTo ErrorHandler
        '        If oFSO.FileExists(fName) Then
        '            oFSTR = oFSO.OpenTextFile(fName)
        '            lCtr = 1
        '            Do While Not oFSTR.AtEndOfStream
        '                sLine = oFSTR.ReadLine
        '                If lCtr <> LineNumber Then
        '                    sTemp = sTemp & sLine & vbCrLf
        '                Else
        '                    bLineFound = True

        '                End If
        '                lCtr = lCtr + 1
        '            Loop

        '            oFSTR.Close()
        '            oFSTR = oFSO.CreateTextFile(fName, True)
        '            oFSTR.Write(sTemp)

        '            DeleteLine = bLineFound
        '        End If


        'ErrorHandler:
        '        On Error Resume Next
        '        oFSTR.Close()
        '        oFSTR = Nothing
        '        oFSO = Nothing

    End Function

    Public Sub DeleteLine(ByRef FileAddress As String, ByRef line As Integer)
        Dim TheFileLines As New List(Of String)
        'System.IO.File.re()
        'TheFileLines.AddRange(System.IO.File.ReadAllLines(FileAddress))
        '' if line is beyond end of list the exit sub
        'If line >= TheFileLines.Count Then Exit Sub
        'TheFileLines.RemoveAt(line)
        'System.IO.File.WriteAllLines(FileAddress, TheFileLines.ToArray)
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        response = MsgBox("Esta seguro de Borrar el último registro?", style)

        If response = MsgBoxResult.Yes Then

            inst.BorraUltimo()

        End If
    End Sub

    Private Sub txtUbica_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbica.TextChanged
        Dim lastchar As Char
        Dim cadena As String

        If (txtUbica.TextLength > 0) Then
            lastchar = txtUbica.Text(txtUbica.TextLength - 1)
            If lastchar = Chr(10) Or lastchar = Chr(13) Then
                cadena = txtUbica.Text
                cadena = cadena.Substring(0, cadena.Length - 2)
                txtUbica.Text = cadena

                txtCodigo.Enabled = True
                txtCodigo.Focus()
            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Alta.Focus()
        Alta.Show()
    End Sub
End Class