'Imports System
'Imports System.Data
'Imports System.Windows.Forms
Imports System.IO
'Imports System.Runtime.InteropServices

Public Class Main
    Dim inst As New AztecaSql()

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        HideTaskbar()

        'Me.WindowState = FormWindowState.Maximized
        Me.Show()
        If Not File.Exists(inst.GetAppPath() + "Azteca.sdf") Then
            Module1.sound.Play()
            'MsgBox("Aviso: La base de datos sera creada. Aguarde un momento...")
            inst.cTable()
            'MsgBox("Base de datos creada exitosamente")
        End If

    End Sub

    Public Function GetAppPath() As String

        Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\"

    End Function

    ' Abre Pantalla inventario
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        Inventario.txtCant.Text = ""
        Inventario.txtCant.Enabled = False
        Inventario.txtCodigo.Text = ""
        Inventario.txtUbica.Text = ""

        flagModulo = True
        Inventario.Show()
        Inventario.Focus()
        Inventario.txtUbica.Focus()

    End Sub

    ' Abre pantalla para Almacen
    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        flagModulo = False
        Almacen.Focus()
        Almacen.Show()

    End Sub

    ' Boton salir
    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click

        'Me.Hide()
        Sale.TextBox1.Text = ""
        Sale.Show()
        Sale.Focus()
        Sale.TextBox1.Focus()

        'ShowTaskbar()
        'Application.Exit()

    End Sub

    ' Guarda Datos para exportar a PC
    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click

        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        Dim response As MsgBoxResult

        response = MsgBox("Esta seguro de exportar los datos?", style)

        If response = MsgBoxResult.Yes Then
            inst.GuardaDatos()
        End If


    End Sub
End Class
