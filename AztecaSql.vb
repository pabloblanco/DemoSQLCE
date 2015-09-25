Imports System
Imports System.Data

Imports System.Data.SqlServerCe
Imports System.Windows.Forms
Imports System.IO

Public Class AztecaSql
    Dim path As String = GetAppPath() + "Azteca.sdf"
    Dim ssceconn As New SqlCeConnection("Data Source= " + GetAppPath() + "Azteca.sdf")

    'Structure info
    '    Dim item As String      ' Codigo de Barras
    '    Dim desc As String      ' Descripcion 
    '    Dim precio As Integer
    '    Dim costo As String
    '    Dim itemint As String   ' Codigo interno
    '    Dim gramaje As String
    '    Dim agc As String
    '    Dim categoria As String ' Departamento
    '    Dim indprecio As String ' Bandera de impresion
    'End Structure
    'Dim infop As New info

    Public Sub cTable() ' Crea Base de Datos

        Dim sqlInsertRow As SqlCeCommand = ssceconn.CreateCommand()
        Dim sqlInsertRowC As SqlCeCommand = ssceconn.CreateCommand()

        If Not File.Exists(path) Then
            File.Delete(path)
            Dim connString As String = "Data Source= " + GetAppPath() + "Azteca.sdf"
            Dim engine As New SqlCeEngine(connString)
            engine.CreateDatabase()
        End If

        If ssceconn.State = ConnectionState.Closed Then
            ssceconn.Open()
        End If

        Dim sqlCreateTable As SqlCeCommand = ssceconn.CreateCommand()
        ' Crea Tabla de Productos
        sqlCreateTable.CommandText = _
          "CREATE TABLE Azteca(id int IDENTITY(0,1) PRIMARY KEY, producto nvarchar(32), " & _
          "campo2 nvarchar(32), campo3 nvarchar(32), campo4 nvarchar(32), campo5 nvarchar(32), " & _
          "campo6 nvarchar(32), campo7 nvarchar(32), campo8 nvarchar(32), campo9 nvarchar(32), " & _
          "campo10 nvarchar(32), campo11 nvarchar(32), campo12 nvarchar(32), campo13 nvarchar(32), " & _
          "campo14 nvarchar(32), campo15 nvarchar(32), campo16 nvarchar(32), campo17 nvarchar(32), " & _
          "campo18 nvarchar(32), campo19 nvarchar(32), ubica nvarchar(32), cant nvarchar(10))"
        sqlCreateTable.ExecuteNonQuery()

        If ssceconn.State = ConnectionState.Open Then
            ssceconn.Close()
        End If

    End Sub

    Public Function GetAppPath() As String

        Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\"

    End Function

    ' Lee Arreglo con variables de codigo de barras e importa a la tabla de Productos
    Public Sub convertToSQL()

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim fechaAct As Date
        Dim txtFecha As String
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim str As String = Nothing
        Dim sqlInsertRow As SqlCeCommand = ssceconn.CreateCommand()
        Dim aux As Integer
        Dim reader As SqlCeDataReader

        File.Delete("\errores.txt")

        'inicio = TimeOfDay

        On Error GoTo SQLError

        If ssceconn.State = ConnectionState.Closed Then
            ssceconn.Open()
        End If

        sqlInsertRow.CommandText = "INSERT INTO Azteca(producto, campo2, campo3, " & _
           "campo4, campo5, campo6, campo7, campo8, campo9, campo10, campo11, " & _
           "campo12, campo13, campo14, campo15, campo16, campo17, campo18, campo19, ubica, cant) " & _
           "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"
        'Add parameters and assign them the values from the TextBoxes on the form
        sqlInsertRow.Parameters.Add("producto", SqlDbType.NText, 32)    '"producto"
        sqlInsertRow.Parameters.Add("campo2", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo3", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo4", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo5", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo6", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo7", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo8", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo9", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo10", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo11", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo12", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo13", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo14", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo15", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo16", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo17", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo18", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("campo19", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("ubica", SqlDbType.NText, 32)
        sqlInsertRow.Parameters.Add("cant", SqlDbType.NText, 10)
        sqlInsertRow.Prepare()

        repetido = False

        If tdata Then       ' Codigo con @
            sqlInsertRow.Parameters(0).Value = indata(4)    ' Producto 
            sqlInsertRow.Parameters(1).Value = indata(2)    ' Campo2
            sqlInsertRow.Parameters(2).Value = indata(3)    ' Campo3
            sqlInsertRow.Parameters(3).Value = indata(5)    ' Campo4
            sqlInsertRow.Parameters(4).Value = indata(6)    ' Campo5
            sqlInsertRow.Parameters(5).Value = indata(7)    ' Campo6
            sqlInsertRow.Parameters(6).Value = indata(8)    ' Campo7  Cantidad
            sqlInsertRow.Parameters(7).Value = indata(9)    ' Campo8
            sqlInsertRow.Parameters(8).Value = indata(10)    ' Campo9
            sqlInsertRow.Parameters(9).Value = indata(11)    ' Campo10
            sqlInsertRow.Parameters(10).Value = indata(12)    ' Campo11
            sqlInsertRow.Parameters(11).Value = indata(13)    ' Campo12
            sqlInsertRow.Parameters(12).Value = indata(14)    ' Campo13
            sqlInsertRow.Parameters(13).Value = indata(15)    ' Campo14
            sqlInsertRow.Parameters(14).Value = indata(16)    ' Campo15
            If indata.Length > 17 Then
                sqlInsertRow.Parameters(15).Value = indata(17)    ' Campo16
            Else
                sqlInsertRow.Parameters(15).Value = ""    ' Campo16
            End If
            sqlInsertRow.Parameters(16).Value = ""
            sqlInsertRow.Parameters(17).Value = ""
            sqlInsertRow.Parameters(18).Value = ""
            If Inventario.txtUbica.Text <> "" Then
                sqlInsertRow.Parameters(19).Value = Inventario.txtUbica.Text
            Else
                sqlInsertRow.Parameters(19).Value = ""
            End If
            sqlInsertRow.Parameters(20).Value = Cantidad    ' cant
            sqlInsertRow.ExecuteNonQuery()

        Else                ' Codigo con GS
            'MsgBox(((indata(16).Length) - 2))
            sqlInsertRow.Parameters(0).Value = indata(3).Trim(" ")    ' Producto 
            sqlInsertRow.Parameters(1).Value = indata(1).Trim(" ")    ' Campo2
            sqlInsertRow.Parameters(2).Value = indata(2).Trim(" ")    ' Campo3
            sqlInsertRow.Parameters(3).Value = indata(4).Trim(" ")    ' Campo4
            sqlInsertRow.Parameters(4).Value = indata(5).Trim(" ")    ' Campo5
            sqlInsertRow.Parameters(5).Value = indata(6).Trim(" ")    ' Campo6
            sqlInsertRow.Parameters(6).Value = indata(7).Trim(" ")    ' Campo7  Cantidad
            sqlInsertRow.Parameters(7).Value = indata(8).Trim(" ")    ' Campo8
            sqlInsertRow.Parameters(8).Value = indata(9).Trim(" ")    ' Campo9
            sqlInsertRow.Parameters(9).Value = indata(10).Trim(" ")    ' Campo10
            sqlInsertRow.Parameters(10).Value = indata(11).Trim(" ")    ' Campo11
            sqlInsertRow.Parameters(11).Value = indata(12).Trim(" ")    ' Campo12
            sqlInsertRow.Parameters(12).Value = indata(13).Trim(" ")    ' Campo13
            sqlInsertRow.Parameters(13).Value = indata(14).Trim(" ")    ' Campo14
            sqlInsertRow.Parameters(14).Value = indata(15).Trim(" ")    ' Campo15
            sqlInsertRow.Parameters(15).Value = indata(16).Remove(((indata(16).Length) - 4), 4)   '.Trim(Chr(30))    ' Campo16
            sqlInsertRow.Parameters(16).Value = ""
            sqlInsertRow.Parameters(17).Value = ""
            sqlInsertRow.Parameters(18).Value = ""
            If Inventario.txtUbica.Text <> "" Then
                sqlInsertRow.Parameters(19).Value = Inventario.txtUbica.Text
            Else
                sqlInsertRow.Parameters(19).Value = ""
            End If
            sqlInsertRow.Parameters(20).Value = Cantidad      ' cant
            sqlInsertRow.ExecuteNonQuery()

        End If

        GoTo Fin

SQLError:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            '            MsgBox("Linea duplicada")
            Dim w As New StreamWriter("\errores.txt")
            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            w.Close()
            Resume Next
        End If
        If Err.Number = 9 Then
            Module1.sound.Play()
            MsgBox("Error en el formato del Código!!!, revise el código e intente nuevamente. Importacion Cancelada...")
            success = False
            GoTo Fin
        End If

Fin:
        'Close the connection
        If ssceconn.State = ConnectionState.Open Then
            ssceconn.Close()
        End If

        If success Then

            If File.Exists("\errores.txt") Then
                Module1.sound.Play()
                MsgBox("Revise archivo de errores, hubieron codigos duplicados.")
            End If
        End If

    End Sub

    Public Function BorraUltimo() As Boolean

        Dim aux As String

        If ssceconn.State = ConnectionState.Closed Then
            ssceconn.Open()
        End If

        On Error GoTo BorraError

        Dim reader As SqlCeDataReader

        'Dim cmd As New SqlCeCommand("SELECT COUNT(*) FROM azteca", ssceconn)
        'registros = cmd.ExecuteScalar

        Dim cmd As New SqlCeCommand("SELECT MAX(id) FROM azteca", ssceconn)
        aux = cmd.ExecuteScalar()

        Dim cmd2 As New SqlCeCommand("DELETE FROM azteca WHERE id = " & aux, ssceconn)
        reader = cmd2.ExecuteReader

        GoTo FinBorra

BorraError:
        'MsgBox(Err.Number & " " & Err.Description)
        MsgBox("No hay datos a borrar!")
        Return False

FinBorra:

        'Close the connection
        If ssceconn.State = ConnectionState.Open Then
            ssceconn.Close()
        End If

        Return True

    End Function

    Public Sub LimpiaTabla()

        If ssceconn.State = ConnectionState.Closed Then
            ssceconn.Open()
        End If

        Dim cmd As SqlCeCommand = ssceconn.CreateCommand

        cmd.CommandText = "DROP TABLE Azteca"
        cmd.ExecuteNonQuery()

        Dim sqlCreateTable As SqlCeCommand = ssceconn.CreateCommand()
        sqlCreateTable.CommandText = _
          "CREATE TABLE Azteca(id int IDENTITY(0,1) PRIMARY KEY, producto nvarchar(32), " & _
          "campo2 nvarchar(32), campo3 nvarchar(32), campo4 nvarchar(32), campo5 nvarchar(32), " & _
          "campo6 nvarchar(32), campo7 nvarchar(32), campo8 nvarchar(32), campo9 nvarchar(32), " & _
          "campo10 nvarchar(32), campo11 nvarchar(32), campo12 nvarchar(32), campo13 nvarchar(32), " & _
          "campo14 nvarchar(32), campo15 nvarchar(32), campo16 nvarchar(32), campo17 nvarchar(32), " & _
          "campo18 nvarchar(32), campo19 nvarchar(32), ubica nvarchar(32), cant nvarchar(10))"
        sqlCreateTable.ExecuteNonQuery()

        'Close the connection
        If ssceconn.State = ConnectionState.Open Then
            ssceconn.Close()
        End If

    End Sub
    Public Sub GuardaDatos()

        Dim str As String
        Dim caracter As Integer = 44    ' Coma
        Dim csvLocation As String
        Dim flag As Boolean = False
        Dim old_Ubica As String
        
        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        On Error GoTo ErrorArchivo

        csvLocation = "my documents\Salida.txt"

        Dim o As New FileStream(csvLocation, FileMode.CreateNew, FileAccess.Write)
        'Read the output in a stream reader
        Dim r As New StreamWriter(o)

        If ssceconn.State = ConnectionState.Closed Then
            ssceconn.Open()
        End If

        Dim reader As SqlCeDataReader
        Dim cmd As SqlCeCommand = ssceconn.CreateCommand
        cmd.CommandText = "SELECT * FROM Azteca"
        reader = cmd.ExecuteReader

        flag = False
        old_Ubica = ""

        While reader.Read()
            If old_Ubica <> reader.Item("ubica") And reader.Item("ubica") <> "" Then
                str = reader.Item("ubica")
                r.WriteLine(str)
            End If
            flag = True
            If flagModulo = True Then   ' Modulo de Inventario
                If reader.Item("campo2") = "" Then   ' Si leyo manualmente, guarda en ese formato
                    str = reader.Item("producto") + Chr(caracter) + reader.Item("cant") + ",,,,,,,,,,,,,,"
                    r.WriteLine(str)
                Else
                    str = reader.Item("campo2") + Chr(caracter) + reader.Item("campo3") + Chr(caracter) + reader.Item("producto") + Chr(caracter) + _
                         reader.Item("campo4") + Chr(caracter) + reader.Item("campo5") + Chr(caracter) + reader.Item("campo6") + Chr(caracter) + _
                         reader.Item("campo7") + Chr(caracter) + reader.Item("campo8") + Chr(caracter) + reader.Item("campo9") + Chr(caracter) + _
                        reader.Item("campo10") + Chr(caracter) + reader.Item("campo11") + Chr(caracter) + reader.Item("campo12") + Chr(caracter) + _
                        reader.Item("campo13") + Chr(caracter) + reader.Item("campo14") + Chr(caracter) + reader.Item("campo15") + Chr(caracter) + _
                        reader.Item("campo16") + Chr(caracter) + reader.Item("cant")
                    'reader.Item("campo17") + Chr(caracter) + reader.Item("campo18") + Chr(caracter) + _
                    'reader.Item("campo19") + Chr(caracter) + reader.Item("ubica") + Chr(caracter) + reader.Item("cant")
                    r.WriteLine(str)
                End If
            End If
            If flagModulo = False Then  ' Modulo de Almacen
                str = reader.Item("ubica") + Chr(caracter) + reader.Item("producto") + _
                    Chr(caracter) + reader.Item("cant")
                r.WriteLine(str)
            End If
            old_Ubica = reader.Item("ubica")
        End While

        r.Close()
        o.Close()

        'Close the connection
        If ssceconn.State = ConnectionState.Open Then
            ssceconn.Close()
        End If

        If flag = True Then
            'response = MsgBox("Datos exportados exitosamente!, desea borrar los datos en la terminal?", style)

            MsgBox("Datos exportados exitosamente!")
            '            If response = MsgBoxResult.Yes Then
            conta = 0           ' Contador de procuctos
            Inventario.Label8.Text = conta.ToString
            LimpiaTabla()
            '        End If
        Else
            MsgBox("No hay datos a exportar!!")
        End If

        GoTo fin

ErrorArchivo:
        response = MsgBox("Archivo de exportacion ya existe, desea reemplazarlo?", style)

        If response = MsgBoxResult.Yes Then
            File.Delete(csvLocation)
            Resume
        End If
        If response = MsgBoxResult.No Then
            'Close the connection
            If ssceconn.State = ConnectionState.Open Then
                ssceconn.Close()
            End If

            GoTo fin
        End If
        'MsgBox(Err.Number)
        'MsgBox(Err.Description)

fin:
        
    End Sub

End Class
