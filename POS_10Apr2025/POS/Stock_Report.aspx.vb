Imports System.Data
Imports Telerik.Web.UI

Partial Class Stock_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now
                oclsBind.BindProductGroup(radCategory, Val(Session("cmp_id")))
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Report:Page_Load:" + ex.Message)
        End Try

    End Sub

    Private Sub BindTable()
        Try

            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            'If rbtDisplayReport.SelectedValue = 0 Then
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))
            oColSqlparram.Add("@flag", IIf(ddlflag.SelectedValue > 0, ddlflag.SelectedValue.ToString, "Changed"))
            oColSqlparram.Add("@Product_Group_Id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"))
            oColSqlparram.Add("@Location_Id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            Dim dt As DataTable
            dt = oClsDal.GetdataTableSp("ws_stock_report_new", oColSqlparram)

            rpstockSUmmary.DataSource = dt
            rpstockSUmmary.DataBind()
        Catch ex As Exception
            LogHelper.Error("Stock_Report:BindTable:" + ex.Message)
        End Try

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Stock_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
End Class
