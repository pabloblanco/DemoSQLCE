Imports Microsoft.win32                   'Registry Functions
Imports System.Runtime.InteropServices    'API functions

Module Module1
    Public sound As New Sound("\Windows\alarm3.wav")
    Public indata(20) As String
    Public tdata As Boolean = True ' Bandera tipo de codigo, True = separado por @
    '                                                       False = Separado por [GS]
    Public flagModulo As Boolean = True ' Bandera para modulo, True = Inventario
    '                                                         False = Toma Fisica
    Public Cantidad As String
    Public conta As Integer = 0         ' Contador de productos leidos

    Public SW_HIDE As Integer = 0
    Public SW_SHOW As Integer = 1

    <DllImport("coredll.dll")> _
    Public Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    End Function

    <DllImport("coredll.dll")> _
    Public Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Boolean
    End Function

    <DllImport("coredll.dll")> _
    Public Function EnableWindow(ByVal hwnd As Integer, ByVal enabled As Boolean) As Boolean
    End Function

    Public Sub ShowTaskbar()

        Dim h As Integer = FindWindow("HHTaskBar", "")
        ShowWindow(h, SW_SHOW)
        EnableWindow(h, True)

    End Sub

    Public Sub HideTaskbar()

        Dim h As Integer = FindWindow("HHTaskBar", "")
        ShowWindow(h, SW_HIDE)
        EnableWindow(h, False)

    End Sub

End Module
