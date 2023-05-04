Imports System.Data.SqlClient
Module Module1
    Dim conn As SqlConnection
    Public sn As String
    Public sid As Integer
    Public Function konek() As SqlConnection
        conn = New SqlConnection("data source=DESKTOP-PRSG5N7\SQLEXPRESS;initial catalog=apotek;integrated security=true")
        conn.Open()
        Return conn
    End Function

End Module
