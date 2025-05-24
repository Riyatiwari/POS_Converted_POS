Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class BookingEasy_PaymentTransaction
    Inherits System.Web.UI.Page

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindTabGrid()
    End Sub
#End Region

#Region "Bind"
    Private Sub BindTabGrid()
        Dim conn As DBConnection = New DBConnection()
        Dim strQvuery As String = ""
        strQvuery &= "SELECT CASE WHEN booking_type = 0 THEN 'Room' WHEN booking_type = 1 THEN 'Table' ELSE '' END AS booking_type, tp.Tran_id, tp.Gateway_id, tp.AccountId, tp.Booking_id, tp.Transaction_ref_no, tp.Amount, tp.Currency, tp.Trasaction_date ,a.FirstName , a.LastName, addr.email1st "
        strQvuery &= "FROM T_Payment_Transaction tp "
        strQvuery &= "INNER JOIN Account a ON a.accountid = tp.accountid "
        strQvuery &= "INNER JOIN Address addr ON addr.addressId = a.addressid "
        strQvuery &= "order by tp.Trasaction_date desc"
        Dim dss As DataSet = conn.SelectData(strQvuery)
        gvTabs.DataSource = dss
    End Sub

#End Region

    Protected Sub imgbtn_Click(sender As Object, e As System.EventArgs) Handles imgbtn.Click

        gvTabs.MasterTableView.GetColumn("Print").Exportable = False

        'For set align
        'gvTabs.MasterTableView.GetColumn("Print").HeaderStyle.HorizontalAlign = HorizontalAlign.Left

        'gvTabs.MasterTableView.GetColumn("Print").ItemStyle.HorizontalAlign = HorizontalAlign.Center

        gvTabs.MasterTableView.GetColumn("No").Display = True

        gvTabs.AllowFilteringByColumn = False
        gvTabs.Rebind()
        gvTabs.ExportSettings.FileName = "Payment Transaction"
        gvTabs.ExportSettings.ExportOnlyData = True
        gvTabs.ExportSettings.IgnorePaging = True
        gvTabs.ExportSettings.OpenInNewWindow = True
        gvTabs.MasterTableView.ExportToExcel()

    End Sub

    Protected Sub gvTabs_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvTabs.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                'accessing Table cell

                Dim hndType As HiddenField = DirectCast(e.Item.FindControl("hndType"), HiddenField)
                If hndType.Value = "Room" Then
                    DirectCast(e.Item.FindControl("lnkpopup2"), LinkButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("lnkpopup1"), LinkButton).Visible = True
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class

