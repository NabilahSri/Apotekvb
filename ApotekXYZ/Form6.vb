Imports System.Data.SqlClient
Public Class Form6
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Sub clear()
        idobat.Clear()
        obat.Text = ""
        harga.Clear()
        stok.Clear()
        qty.Clear()
    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = New SqlDataAdapter("select * from tbltrans where status=0", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
        sql = New SqlCommand("select*from tblobat", konek)
        dr = sql.ExecuteReader
        While (dr.Read())
            obat.Items.Add(dr.Item("nama"))
        End While
    End Sub

    Private Sub cari_TextChanged(sender As Object, e As EventArgs) Handles cari.TextChanged
        sql = New SqlCommand("select*from tbltrans where notrans='" & cari.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If Not (IsDBNull(dr.Item("nores"))) Then
                noresep.Text = dr.Item("nores").ToString
                tglresep.Text = Format(dr.Item("tglres"), "yyyy/MM/dd").ToString
                pasien.Text = dr.Item("pasien").ToString
                dokter.Text = dr.Item("dokter").ToString
            Else
                noresep.Text = ""
                tglresep.Text = Format(Now, "yyyy/MM/dd")
                pasien.Text = ""
                dokter.Text = ""
            End If
            dgv.Columns.Clear()
            da = New SqlDataAdapter("select namaobat,jumlahobat,idobat,idresep from tblresep full outer join tbltrans on tblresep.idtrans=tbltrans.idtrans where notrans='" & cari.Text & "'", konek)
            dt = New DataTable
            da.Fill(dt)
            dgv.DataSource = dt
        End If
    End Sub

    Private Sub obat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles obat.SelectedIndexChanged
        sql = New SqlCommand("select*from tblobat where nama='" & obat.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            harga.Text = dr.Item("harga")
            stok.Text = dr.Item("jumlah")
            idobat.Text = dr.Item("idobat")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sql = New SqlCommand("update tblresep set namaobat='" & obat.Text & "',jumlahobat='" & qty.Text & "',idobat='" & idobat.Text & "' where idresep='" & idresep.Text & "'", konek)
        sql.ExecuteNonQuery()
        da = New SqlDataAdapter("select namaobat,jumlahobat,idobat,idresep from tblresep full outer join tbltrans on tblresep.idtrans=tbltrans.idtrans where notrans='" & cari.Text & "'", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
        MsgBox("Berhasil mengubah data!")
        clear()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        obat.Text = dgv.Rows(e.RowIndex).Cells("namaobat").Value
        qty.Text = dgv.Rows(e.RowIndex).Cells("jumlahobat").Value
        idobat.Text = dgv.Rows(e.RowIndex).Cells("idobat").Value
        idresep.Text = dgv.Rows(e.RowIndex).Cells("idresep").Value
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Yakin ingin menghapus data ini ?", vbYesNo + 32, "Hapus data") = vbYes Then
            sql = New SqlCommand("delete from tblresep where idresep='" & idresep.Text & "'", konek)
            sql.ExecuteNonQuery()
            da = New SqlDataAdapter("select namaobat,jumlahobat,idobat,idresep from tblresep full outer join tbltrans on tblresep.idtrans=tbltrans.idtrans where notrans='" & cari.Text & "'", konek)
            dt = New DataTable
            da.Fill(dt)
            dgv.DataSource = dt
            MsgBox("Berhasil menghapus data!")
            clear()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles qty.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub
End Class