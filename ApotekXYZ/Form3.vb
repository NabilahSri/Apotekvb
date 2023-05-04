Imports System.Data.SqlClient
Public Class Form3
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Sub tampil()
        da = New SqlDataAdapter("select*from tbluser", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub
    Sub clear()
        iduser.Clear()
        nama.Clear()
        alamat.Clear()
        telp.Clear()
        user.Clear()
        pass.Clear()
        cari.Clear()
        tipe.Text = ""
    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = New SqlCommand("select*from tbluser", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If (dr.Item("username") = user.Text) Then
                MsgBox("USername sudah digunakan")
            Else
                sql = New SqlCommand("insert into tbluser values('" & tipe.Text & "','" & nama.Text & "','" & alamat.Text & "','" & telp.Text & "','" & user.Text & "','" & pass.Text & "')", konek)
                sql.ExecuteNonQuery()
                tampil()
                MsgBox("Berhasil menambahkan data!")
                clear()
            End If
        End If
    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        iduser.Text = dgv.Rows(e.RowIndex).Cells("iduser").Value
        nama.Text = dgv.Rows(e.RowIndex).Cells("nama").Value
        alamat.Text = dgv.Rows(e.RowIndex).Cells("alamat").Value
        telp.Text = dgv.Rows(e.RowIndex).Cells("telp").Value
        user.Text = dgv.Rows(e.RowIndex).Cells("username").Value
        pass.Text = dgv.Rows(e.RowIndex).Cells("pass").Value
        tipe.Text = dgv.Rows(e.RowIndex).Cells("tipe").Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sql = New SqlCommand("update tbluser set tipe='" & tipe.Text & "',nama='" & nama.Text & "',alamat='" & alamat.Text & "',telp='" & telp.Text & "',username='" & user.Text & "',pass='" & pass.Text & "' where iduser='" & iduser.Text & "'", konek)
        sql.ExecuteNonQuery()
        tampil()
        MsgBox("Berhasil mengubah data!")
        clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Yakin ingin menghapus data ini ?", vbYesNo + 32, "Hapus data") = vbYes Then
            sql = New SqlCommand("delete from tbluser where iduser='" & iduser.Text & "'", konek)
            sql.ExecuteNonQuery()
            tampil()
            MsgBox("Berhasil menghapus data!")
            clear()
        End If
    End Sub

    Private Sub cari_TextChanged(sender As Object, e As EventArgs) Handles cari.TextChanged
        da = New SqlDataAdapter("select*from tbluser where nama like '" & cari.Text & "%'", konek)
        dt = New DataTable
        da.Fill(dt)
        dgv.DataSource = dt
    End Sub

    Private Sub telp_TextChanged(sender As Object, e As EventArgs) Handles telp.TextChanged

    End Sub

    Private Sub telp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles telp.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub
End Class