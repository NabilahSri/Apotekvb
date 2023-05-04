Imports System.Data.SqlClient
Public Class Form1
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Dim waktu As String = Format(Now, "yyyy/MM/dd HH:mm:ss")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = New SqlCommand("select*from tbluser where username='" & user.Text & "' and pass='" & pass.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("tipe") = "Admin" Then
                sid = dr.Item("iduser")
                Form2.Show()
                Me.Hide()
            ElseIf dr.Item("tipe") = "Apoteker" Then
                sid = dr.Item("iduser")
                Form6.Show()
                Me.Hide()
            Else
                sid = dr.Item("iduser")
                sn = dr.Item("nama")
                Form7.Show()
                Me.Hide()
            End If
            sql = New SqlCommand("insert into tbllog values('" & waktu & "','" & Button1.Text & "','" & sid & "')", konek)
            sql.ExecuteNonQuery()
        Else
            MsgBox("Username atau Password salah !")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        pass.Clear()
        user.Clear()
        sql = New SqlCommand("insert into tbllog values('" & waktu & "','" & Button2.Text & "','" & sid & "')", konek)
        sql.ExecuteNonQuery()
    End Sub
End Class
