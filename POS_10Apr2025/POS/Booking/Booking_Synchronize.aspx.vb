Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class BookingEasy_Booking_Synchronize
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        BindTabGrid()
    End Sub

#Region "Bind"
    Private Sub BindTabGrid()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        strQvuery &= "SELECT * "
        strQvuery &= "FROM Booking_Synchronize "
        strQvuery &= "order by Sync_Date desc"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        gvTabs.DataSource = dss
    End Sub

#End Region

    Protected Sub imgbtn_Click(sender As Object, e As System.EventArgs) Handles imgbtn.Click
        gvTabs.ExportSettings.FileName = "Booking Synchronize"
        gvTabs.ExportSettings.ExportOnlyData = True
        gvTabs.ExportSettings.IgnorePaging = True
        gvTabs.ExportSettings.OpenInNewWindow = True
        gvTabs.MasterTableView.ExportToExcel()
        gvTabs.AllowFilteringByColumn = False
        gvTabs.Rebind()
    End Sub
End Class
