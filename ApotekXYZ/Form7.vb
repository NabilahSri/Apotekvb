Imports System.Data.SqlClient
Public Class Form7
    Dim sql As SqlCommand
    Dim dr As SqlDataReader
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Sub clear()
        idobat.Clear()
        notrans.Clear()
        noresep.Clear()
        pasien.Clear()
        dokter.Clear()
        harga.Clear()
        qty.Clear()
        cari.Clear()
        bayar.Clear()
        tipe.Text = ""
        obat.Text = ""
        total.Text = ""
        kembalian.Text = ""
        dgv.Columns.Clear()
    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datetime.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
        nmkasir.Text = sn
        sql = New SqlCommand("select*from tblobat", konek)
        dr = sql.ExecuteReader
        While (dr.Read())
            obat.Items.Add(dr.Item("nama"))
        End While
    End Sub

    Private Sub tipe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tipe.SelectedIndexChanged
        notrans.Text = Format(Now, "yyyyMMddHHmmss")
        dgv.DataSource = Nothing
        If tipe.SelectedItem = "Resep" Then
            tglresep.Enabled = True
            pasien.Enabled = True
            dokter.Enabled = True
            sql = New SqlCommand("select nores from tbltrans order by nores desc", konek)
            dr = sql.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Dim urut As Integer
                urut = Val(Mid(dr.Item("nores").ToString, 4, 4)) + 1
                noresep.Text = "R" + urut.ToString("000")
            Else
                noresep.Text = "R001"
            End If
        Else
            tglresep.Enabled = False
            pasien.Enabled = False
            dokter.Enabled = False
            noresep.Text = ""
        End If
        With dgv
            .ColumnCount = 5
            .Columns(0).Name = "Nama Obat"
            .Columns(1).Name = "Harga"
            .Columns(2).Name = "Jumlah Beli"
            .Columns(3).Name = "Subtotal"
            .Columns(4).Name = "Id Obat"
            .Columns(4).Visible = False
        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cek = False
        Dim tot As Integer
        For Each row As DataGridViewRow In dgv.Rows
            If (row.Cells(0).Value = obat.Text) Then
                cek = True
            End If
        Next
        If (cek = False) Then
            dgv.Rows.Add(1)
            dgv.Rows(dgv.RowCount - 2).Cells(0).Value = obat.Text
            dgv.Rows(dgv.RowCount - 2).Cells(1).Value = harga.Text
            dgv.Rows(dgv.RowCount - 2).Cells(2).Value = qty.Text
            dgv.Rows(dgv.RowCount - 2).Cells(3).Value = Val(qty.Text) * Val(harga.Text)
            dgv.Rows(dgv.RowCount - 2).Cells(4).Value = idobat.Text
        End If
        For Each row As DataGridViewRow In dgv.Rows
            If (row.Cells(0).Value = obat.Text And cek = True) Then
                row.Cells(2).Value = qty.Text
                row.Cells(3).Value = Val(row.Cells(2).Value) * Val(harga.Text)
            End If
            tot += row.Cells(3).Value
        Next
        total.Text = tot
        MsgBox("data ditambahkan")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If tipe.Text = "Resep" Then
            sql = New SqlCommand("insert into tbltrans values('" & notrans.Text & "','" & tgltrans.Text & "','0','" & sid & "','" & sn & "','" & total.Text & "','" & dokter.Text & "','" & pasien.Text & "','" & noresep.Text & "','" & tglresep.Text & "')", konek)
        Else
            sql = New SqlCommand("insert into tbltrans(notrans,tgltrans,status,iduser,namakasir,total) values('" & notrans.Text & "','" & tgltrans.Text & "','0','" & sid & "','" & sn & "','" & total.Text & "')", konek)
        End If
        sql.ExecuteNonQuery()
        sql = New SqlCommand("select idtrans from tbltrans where notrans='" & notrans.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        Dim ido As Integer
        For i As Integer = 0 To dgv.Rows.Count - 2
            ido = dgv.Rows(i).Cells(4).Value
            sql = New SqlCommand("insert into tblresep values('" & dgv.Rows(i).Cells(0).Value & "','" & dgv.Rows(i).Cells(2).Value & "','" & ido & "','" & dr.Item("idtrans") & "')", konek)
            sql.ExecuteNonQuery()
        Next
        MsgBox("Berhasil menyimpan data !")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form8.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clear()
        MsgBox("data di kosongkan")
    End Sub

    Private Sub cari_TextChanged(sender As Object, e As EventArgs) Handles cari.TextChanged
        sql = New SqlCommand("select*from tbltrans where notrans='" & cari.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If Not (IsDBNull(dr.Item("nores"))) Then
                noresep.Text = dr.Item("nores")
                tglresep.Text = Format(dr.Item("tglres"), "yyyy/MM/dd")
                pasien.Text = dr.Item("pasien")
                notrans.Text = dr.Item("notrans")
                dokter.Text = dr.Item("dokter")
            Else
                noresep.Text = ""
                tglresep.Text = Format(Now, "yyyy/MM/dd")
                pasien.Text = ""
                dokter.Text = ""
                notrans.Text = dr.Item("notrans")
            End If
            dgv.Columns.Clear()
            da = New SqlDataAdapter("select namaobat,harga,jumlahobat as jumlahbeli,jumlahobat*harga as subtotal,tblobat.idobat from tblresep full outer join tbltrans on tblresep.idtrans=tbltrans.idtrans full outer join tblobat on tblresep.idobat=tblobat.idobat where notrans='" & cari.Text & "'", konek)
            dt = New DataTable
            da.Fill(dt)
            dgv.DataSource = dt
        End If
        Dim tot As Integer
        For Each row As DataGridViewRow In dgv.Rows
            tot += If(IsDBNull(row.Cells(3).Value), 0, row.Cells(3).Value)
        Next
        total.Text = tot
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Val(bayar.Text) >= Val(total.Text) Then
            sql = New SqlCommand("update tbltrans set status=1 where notrans='" & notrans.Text & "'", konek)
            sql.ExecuteNonQuery()
            For Each row As DataGridViewRow In dgv.Rows
                sql = New SqlCommand("update tblobat set jumlah=jumlah-" & Val(row.Cells(2).Value) & " where idobat=" & Val(row.Cells(4).Value) & "", konek)
                sql.ExecuteNonQuery()
            Next
            kembalian.Text = Val(bayar.Text) - Val(total.Text)
            MsgBox("Transaksi berhasil")
        Else
            MsgBox("Transaksi gagal")
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

    Private Sub bayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles bayar.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub obat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles obat.SelectedIndexChanged
        sql = New SqlCommand("select*from tblobat where nama='" & obat.Text & "'", konek)
        dr = sql.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            harga.Text = dr.Item("harga")
            idobat.Text = dr.Item("idobat")
        End If
    End Sub
End Class