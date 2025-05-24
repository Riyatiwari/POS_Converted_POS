
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports System.Web.Services

Partial Class TillSummary
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole
    Dim dt As DataTable

    <WebMethod>
    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim dtwise As Integer = 0
            If chkDateWise.Checked = True Then
                dtwise = 0
            Else
                dtwise = 1
            End If

            If chkDateWise.Checked = True Then
                dtwise = 0
            Else
                dtwise = 1
            End If


            'Dim n As String = String.Format("{0}", Request.Form("groupby"))
            Dim n As String

            If chkPs2.Checked = True Then
                n = "0"
            ElseIf chkPG1.Checked = True Then
                n = "2"
            ElseIf chkPs4.Checked = True Then
                n = "1"
            End If



            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@location_id", "0")
            oColSqlparram.Add("@machine_id", "0")
            oColSqlparram.Add("@venue_id", "0")
            oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Withoutdate", dtwise, SqlDbType.Int)
            oColSqlparram.Add("@groupByCol", n, SqlDbType.Int)
            oColSqlparram.Add("@salestype", IIf(radsalesType.SelectedIndex > 0, radsalesType.SelectedValue.ToString, "All"))
            oColSqlparram.Add("@shift_name", IIf(radshifttype.SelectedIndex > 0, radshifttype.SelectedItem.Value.ToString, "0"))

            If hfSizelocation.Value = "0" Then
                dt = oClsDal.GetdataTableSp("P_TillSummary", oColSqlparram)
            Else
                dt = oClsDal.GetdataTableSp("P_TillSummary", oColSqlparram)
            End If

            rptProductSUmmary.DataSource = dt
            rptProductSUmmary.DataBind()



            'grdSummary.DataSource = dt
            'grdSummary.DataBind()



            getShifts()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Private Sub PSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now
            '  BindData()
            getShifts()
        End If
    End Sub
    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        BindData()
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
                getShifts()
            Else
                divcustom.Visible = False
                getShifts()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub lbtnsize_Click(sender As Object, e As EventArgs) Handles lbtnsize.Click
        If (hfSizelocation.Value = "0") Then
            hfSizelocation.Value = "1"
            lbtnsize.Text = "Show Department & Course"
        Else
            hfSizelocation.Value = "0"
            lbtnsize.Text = "Show Department & Course"
        End If
        BindData()
    End Sub

    Private Sub rptProductSUmmary_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductSUmmary.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim rpcashSUmmary As Repeater = TryCast(e.Item.FindControl("Header1"), Repeater)
            rpcashSUmmary.DataSource = dt.Columns
            rpcashSUmmary.DataBind()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rpcashSUmmary As Repeater = TryCast(e.Item.FindControl("Item1"), Repeater)
            Dim row = TryCast(e.Item.DataItem, System.Data.DataRowView)
            rpcashSUmmary.DataSource = row.Row.ItemArray
            rpcashSUmmary.DataBind()


        End If
    End Sub

    Public Sub getShifts()
        oclsBind.BindShiftByDuration(radshifttype, Val(Session("cmp_id")), IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtTo.SelectedDate), IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), (radDuration.SelectedItem.Value))
    End Sub
    Protected Sub txtFrom_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub txtTo_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
End Class
