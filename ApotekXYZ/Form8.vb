Imports CrystalDecisions.CrystalReports.Engine

Public Class Form8
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim print As New ReportDocument
        print.Load("C:\Users\RPL\source\repos\ApotekXYZ\ApotekXYZ\CrystalReport1.rpt")
        CrystalReportViewer1.ReportSource = print
        CrystalReportViewer1.SelectionFormula = "{tbltrans.notrans}='" & Form7.notrans.Text & "'"
        CrystalReportViewer1.RefreshReport()
    End Sub
End Class