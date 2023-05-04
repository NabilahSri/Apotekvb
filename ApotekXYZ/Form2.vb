Imports System.Data.SqlClient
Public Class Form2
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = New SqlDataAdapter("select username,waktu,aktivitas from tbllog full outer join tbluser on tbllog.iduser=tbluser.iduser", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim waktu As String = Format(dtp.Value, "yyyy/MM/dd")
        da = New SqlDataAdapter("select username,waktu,aktivitas from tbllog full outer join tbluser on tbllog.iduser=tbluser.iduser where format(tbllog.waktu,'yyyy/MM/dd')='" & waktu & "'", konek)
        dt = New DataTable
        da.Fill(dt)
        If dt.Rows.Count < 1 Then
            MsgBox("data tidak ditemukan")
        Else
            dgv.DataSource = dt
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form3.TopLevel = False
        Panel2.Controls.Add(Form3)
        Form3.BringToFront()
        Form3.Show()
        Form4.Hide()
        Form5.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form4.TopLevel = False
        Panel2.Controls.Add(Form4)
        Form4.BringToFront()
        Form4.Show()
        Form3.Hide()
        Form5.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form5.TopLevel = False
        Panel2.Controls.Add(Form5)
        Form5.BringToFront()
        Form5.Show()
        Form4.Hide()
        Form3.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form1.Show()
        Me.Hide()
    End Sub
End Class