Imports System.Data
Imports System.Web.Services

Partial Class PayGatewaySummary
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole
    Dim dt As DataTable

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now
            getShifts()

            oclsBind.BindMerchant(radMerchantID)
            oclsBind.BindVenueByRoleVenue(RadReseller, Val(Session("cmp_id")), Session("VenueID").ToString())

        End If
    End Sub

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

            Dim n As String

            If chkPs2.Checked = True Then
                n = "0" '(Reseller)
            ElseIf chkPG1.Checked = True Then
                n = "2" '(Terminal)
            ElseIf chkPs4.Checked = True Then
                n = "1" '(Merchant)
            End If

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@location_id", IIf(radMerchantID.SelectedIndex > 0, radMerchantID.SelectedItem.Value.ToString, "0"))
            oColSqlparram.Add("@machine_id", "0")
            oColSqlparram.Add("@venue_id", IIf(RadReseller.SelectedIndex > 0, RadReseller.SelectedItem.Value.ToString, "0"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Withoutdate", dtwise, SqlDbType.Int)
            oColSqlparram.Add("@groupByCol", n, SqlDbType.Int)
            oColSqlparram.Add("@salestype", IIf(radsalesType.SelectedIndex > 0, radsalesType.SelectedValue.ToString, "All"))
            oColSqlparram.Add("@shift_name", IIf(radshifttype.SelectedIndex > 0, radshifttype.SelectedItem.Value.ToString, "0"))
            oColSqlparram.Add("@RoleVenue", IIf(Session("VenueID") = Nothing, "", Session("VenueID").ToString()))

            ds = oClsDal.GetdatasetSp("P_gtway_report", oColSqlparram, "")

            dt = ds.Tables(0)

            'Set Grand Total 
            Dim dt1 As DataTable = ds.Tables(1)
            hd_GTotal.Value = dt1.Rows(0)("GTotal").ToString()

            'Set Till Total 
            If ds.Tables.Count > 2 Then
                Dim dt2 As DataTable = ds.Tables(2)
                hd_TotalTill.Value = dt2.Rows(0)("s_Total").ToString()
            Else
                hd_TotalTill.Value = ""
            End If

            If n = 1 Then
                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()

                div_rpt1.Visible = True
                div_rpt2.Visible = False

            Else
                rptResellerTerminal.DataSource = dt
                rptResellerTerminal.DataBind()

                div_rpt1.Visible = False
                div_rpt2.Visible = True

            End If

            getShifts()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Public Sub getShifts()
        oclsBind.BindShiftByDuration(radshifttype, Val(Session("cmp_id")), IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtTo.SelectedDate), IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), (radDuration.SelectedItem.Value))
    End Sub

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
                getShifts()
            Else
                divcustom.Visible = False
                getShifts()
            End If
        Catch ex As Exception
            LogHelper.Error("PayGatewaySummary:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        BindData()
    End Sub
    Protected Sub txtFrom_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub txtTo_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub rptProductSUmmary_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            Dim i As Integer = e.Item.ItemIndex
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            Dim btnExpand As Button = DirectCast(rptProductSUmmary.Items(i).FindControl("btnExpand"), Button)
            Dim MerchantID As HiddenField = DirectCast(rptProductSUmmary.Items(i).FindControl("hd_MerchantID"), HiddenField)
            Dim rptOrders As Repeater = DirectCast(rptProductSUmmary.Items(i).FindControl("rptOrders"), Repeater)
            Dim dtl_panel As Panel = DirectCast(rptProductSUmmary.Items(i).FindControl("dtl_panel"), Panel)
            Dim hd_Date As HiddenField = DirectCast(rptProductSUmmary.Items(i).FindControl("hd_Date"), HiddenField)
            Dim hd_Duration As HiddenField = DirectCast(rptProductSUmmary.Items(i).FindControl("hd_Duration"), HiddenField)
            Dim link_btn As LinkButton = DirectCast(rptProductSUmmary.Items(i).FindControl("lnkDetail"), LinkButton)

            If btnExpand.Text = "+" Then

                oColSqlparram.Add("@MerchantID", MerchantID.Value.ToString())
                oColSqlparram.Add("@Date", hd_Date.Value.ToString())
                oColSqlparram.Add("@Duration", hd_Duration.Value.ToString())
                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                dt = oClsDal.GetdataTableSp("P_gtway_report_detail", oColSqlparram)

                If dt.Rows.Count > 0 Then
                    rptOrders.DataSource = dt
                    rptOrders.DataBind()
                Else
                    rptOrders.DataSource = String.Empty
                    rptOrders.DataBind()
                End If

                btnExpand.Text = "-"
                dtl_panel.Visible = True

            Else
                btnExpand.Text = "+"
                dtl_panel.Visible = False

            End If


        Catch ex As Exception
            LogHelper.Error("PayGatewaySummary:rptProductSUmmary_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkview_Click(sender As Object, e As EventArgs)
        Try

            Dim btn As LinkButton = DirectCast(sender, LinkButton)
            Dim ref As String = btn.CommandArgument

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@ref", ref)

            dt = oClsDal.GetdataTableSp("P_gtway_report_Ref_Detail", oColSqlparram)

            If (dt.Rows.Count > 0) Then

                Session("refdt") = dt

                Dim url As String = "PayGatewayDetail.aspx"

                ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

            End If
        Catch ex As Exception
            LogHelper.Error("PayGatewaySummary:lnkview_Click:" + ex.Message)
        End Try
    End Sub
    'Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
    '    Try

    '        Dim btn As LinkButton = DirectCast(sender, LinkButton)
    '        Dim ref As String() = btn.CommandArgument.ToString().Split(";")

    '        Dim mID As String = ref(0)
    '        Dim RID As String = ref(1)
    '        Dim P_date As String = ref(2)
    '        Dim Duration As String = ref(3)

    '        Dim oClsDal As ClsDataccess = New ClsDataccess()
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@mID", mID)
    '        oColSqlparram.Add("@RID", RID)
    '        oColSqlparram.Add("@TID", "")
    '        oColSqlparram.Add("@P_date", P_date)
    '        oColSqlparram.Add("@Duration", Duration)
    '        oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)

    '        dt = oClsDal.GetdataTableSp("P_gtway_List_Detail", oColSqlparram)

    '        If (dt.Rows.Count > 0) Then

    '            Session("Listdt") = dt

    '            Dim url As String = "PayGatewayListDetail.aspx"

    '            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("PayGatewaySummary:lnkDetail_Click:" + ex.Message)
    '    End Try
    'End Sub
End Class
