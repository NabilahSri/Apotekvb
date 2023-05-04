Imports System.Data.SqlClient
Public Class Form5
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = New SqlDataAdapter("select tgltrans,total from tbltrans where status=1", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        da = New SqlDataAdapter("select tgltrans,total from tbltrans where status=1 and tgltrans between '" & dari.Text & "' and '" & sampai.Text & "'", konek)
        dt = New DataTable
        da.Fill(dt)
        If dt.Rows.Count < 1 Then
            MsgBox("Data tidak ditemukan")
        Else
            dgv.DataSource = dt
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = New SqlCommand("select format(tbltrans.tgltrans,'yyyy/MM/dd') as date,sum(tbltrans.total) as harga from tbltrans where status=1 and tgltrans between '" & dari.Text & "' and '" & sampai.Text & "' group by format(tbltrans.tgltrans,'yyyy/MM/dd')", konek)
        dr = sql.ExecuteReader
        Chart1.Series.Clear()
        Dim i As Integer
        While (dr.Read())
            Chart1.Series.Add("Omset")
            Chart1.Series(i).Points.AddXY(dr.Item("date"), dr.Item("harga"))
        End While
    End Sub
End Class