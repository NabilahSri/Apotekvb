Imports System.Data.SqlClient
Public Class Form4
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Sub tampil()
        da = New SqlDataAdapter("select*from tblobat", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub
    Sub clear()
        idobat.Clear()
        nama.Clear()
        jumlah.Clear()
        harga.Clear()
        cari.Clear()
        kode.Clear()
    End Sub
    Sub kdo()
        sql = New SqlCommand("select kode from tblobat order by kode desc", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim urut As Integer
            urut = Val(Mid(dr.Item("kode").ToString, 4, 4)) + 1
            kode.Text = "KD" + urut.ToString("000")
        Else
            kode.Text = "KD001"
        End If
    End Sub
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
        kdo()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = New SqlCommand("select*from tblobat", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If (dr.Item("nama") = nama.Text) Then
                MsgBox("nama obat sudah ada")
            Else
                sql = New SqlCommand("insert into tblobat values('" & kode.Text & "','" & nama.Text & "','" & exp.Text & "','" & jumlah.Text & "','" & harga.Text & "')", konek)
                sql.ExecuteNonQuery()
                tampil()
                MsgBox("Berhasil menambahkan data!")
                clear()
                kdo()
            End If
        End If
    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        idobat.Text = dgv.Rows(e.RowIndex).Cells("idobat").Value
        nama.Text = dgv.Rows(e.RowIndex).Cells("nama").Value
        exp.Text = dgv.Rows(e.RowIndex).Cells("exp").Value
        jumlah.Text = dgv.Rows(e.RowIndex).Cells("jumlah").Value
        harga.Text = dgv.Rows(e.RowIndex).Cells("harga").Value
        kode.Text = dgv.Rows(e.RowIndex).Cells("kode").Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sql = New SqlCommand("update tblobat set kode='" & kode.Text & "',nama='" & nama.Text & "',exp='" & exp.Text & "',jumlah='" & jumlah.Text & "',harga='" & harga.Text & "' where idobat='" & idobat.Text & "'", konek)
        sql.ExecuteNonQuery()
        tampil()
        MsgBox("Berhasil mengubah data!")
        clear()
        kdo()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Yakin ingin menghapus data ini ?", vbYesNo + 32, "Hapus data") = vbYes Then
            sql = New SqlCommand("delete from tblobat where idobat='" & idobat.Text & "'", konek)
            sql.ExecuteNonQuery()
            tampil()
            MsgBox("Berhasil menghapus data!")
            clear()
            kdo()
        End If
    End Sub

    Private Sub cari_TextChanged(sender As Object, e As EventArgs) Handles cari.TextChanged
        da = New SqlDataAdapter("select*from tblobat where nama like '" & cari.Text & "%'", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub

    Private Sub jumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles jumlah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub harga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles harga.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub
End Class