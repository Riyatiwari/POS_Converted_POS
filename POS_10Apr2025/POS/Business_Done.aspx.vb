Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports System.IO
Partial Class Business_Done
    Inherits BaseClass
    Dim dtExp As DataTable
    Dim dtRptCash As DataTable
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsRole As Controller_clsRole
    Dim oclExpense As New Controller_clsExpenseMaster()

    Dim head As String
    Dim Profit As Decimal
    Dim totalmon_amount As Decimal
    Dim totaltue_amount As Decimal
    Dim totalwed_amount As Decimal
    Dim totalthu_amount As Decimal
    Dim totalfri_amount As Decimal
    Dim totalsat_amount As Decimal
    Dim totalsun_amount As Decimal
    Dim totalamount As Decimal
    Dim amount As Decimal
    Dim Currency As String


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Dim baseDate As DateTime = DateTime.Now
                Dim today = baseDate
                Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

                '-----------03-06---------
                'txtdate.SelectedDate = thisWeekStart.AddDays(1)

                oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))

                oclsRegister.Company_id = Val(Session("cmp_id"))
                Dim dtCompany As DataTable = oclsRegister.Select()
                If dtCompany.Rows.Count > 0 Then

                    If dtCompany.Rows(0)("chk_duration").ToString() = "1" Then
                        For Each item As ListItem In radDuration.Items
                            If item.Value = "4th Last Week" Or item.Value = "Custom" Then
                                item.Enabled = False
                            End If
                        Next
                    Else
                        For Each item As ListItem In radDuration.Items
                            If item.Value = "4th Last Week" Or item.Value = "Custom" Then
                                item.Enabled = True
                            End If
                        Next
                    End If

                End If


                BindTable()
                BindTableExp()
            End If

        Catch ex As Exception
            LogHelper.Error("Page_Load:till_report:" + ex.Message)
        End Try



    End Sub
    Private Sub BindTable()
        Try

            'Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
            'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)


            '----------------03-06-------------
            Dim baseDate As DateTime = DateTime.Now
            Dim today = baseDate
            Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
            Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)


            'Dim baseDate As DateTime = DateTime.Now
            'Dim today = baseDate
            'Dim thisWeekStart = baseDate
            'Dim thisWeekEnd = baseDate
            '----------------03-06-------------

            Dim loc = 0

            If rdlocation.SelectedIndex > 0 Then
                loc = rdlocation.SelectedValue
            End If
            oclsSales.cmp_id = Val(Session("cmp_id"))

            If radDuration.SelectedValue.ToString = "Custom" Then
                oclsSales.date1 = txtForDate.SelectedDate
                oclsSales.date2 = txtForDate.SelectedDate
            Else
                oclsSales.date1 = baseDate
                oclsSales.date2 = baseDate
            End If

            oclsSales.Location_id = loc
            oclsSales.duration = radDuration.SelectedValue.ToString()
            Dim dtReport As DataTable = oclsSales.Cash_Control_BD()
            dtRptCash = dtReport
            If dtReport.Rows.Count > 0 Then
                'gv_CashSummary.DataSource = dtReport
                'gv_CashSummary.DataBind()
                rptcash.DataSource = dtReport
                rptcash.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Business_Done:BindTable:" + ex.Message)
        End Try

    End Sub

    Private Sub BindTableExp()
        Try
            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            '-------------------03-06-2021------------------
            Dim baseDate As DateTime = DateTime.Now
            Dim date1 As DateTime = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
            date1 = date1.AddDays(1)
            Dim date2 As DateTime = date1.AddDays(7).AddSeconds(-1)
            '--------------------------------------------

            oclExpense.cmp_id = Val(Session("cmp_id"))
            Dim loc = 0

            If rdlocation.SelectedIndex > 0 Then
                loc = rdlocation.SelectedValue
            End If

            If radDuration.SelectedValue.ToString = "Custom" Then
                oclExpense.date1 = Convert.ToDateTime(txtForDate.SelectedDate).ToString("dd-MMM-yyyy")
                oclExpense.date2 = Convert.ToDateTime(txtForDate.SelectedDate).ToString("dd-MMM-yyyy")
            Else
                oclExpense.date1 = Convert.ToDateTime(baseDate).ToString("dd-MMM-yyyy")
                'IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
                oclExpense.date2 = Convert.ToDateTime(baseDate).ToString("dd-MMM-yyyy")
                'IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            End If

            oclExpense.location_id = loc
            oclExpense.duration = radDuration.SelectedValue.ToString()
            Dim dtReport As DataTable = oclExpense.Expense_Summary_BD()
            dtExp = dtReport

            'Dim sum As Integer = Convert.ToInt32(dtExp.Compute("SUM(Salary)", String.Empty))

            If dtReport.Rows.Count > 0 Then
                rptbusiness.DataSource = dtReport
                rptbusiness.DataBind()
                'gv_CashReport.DataSource = dtReport
                'gv_CashReport.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Business_Done:BindTable:" + ex.Message)
        End Try

    End Sub
    'Private Sub gv_CashReport_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gv_CashReport.RowDataBound


    '    If (e.Row.RowType = DataControlRowType.DataRow) Then

    '        Dim lblhead As Label = e.Row.FindControl("lblNameHead")

    '        If (lblhead.Text = "<b>SALES DETAILS</b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or lblhead.Text = "<b>EXPENSES DETAILS</b>") Then
    '            head = lblhead.Text
    '        End If

    '        Dim lblMon_Amount As Label = e.Row.FindControl("lblMon_Amount")
    '        Dim lblTue_Amount As Label = e.Row.FindControl("lblTue_Amount")
    '        Dim lblWed_Amount As Label = e.Row.FindControl("lblWed_Amount")
    '        Dim lblThu_Amount As Label = e.Row.FindControl("lblThu_Amount")
    '        Dim lblFri_Amount As Label = e.Row.FindControl("lblFri_Amount")
    '        Dim lblSat_Amount As Label = e.Row.FindControl("lblSat_Amount")
    '        Dim lblSun_Amount As Label = e.Row.FindControl("lblSun_Amount")
    '        Dim lblAmount As Label = e.Row.FindControl("lblAmount")

    '        Dim hf_Currency As HiddenField = e.Row.FindControl("hf_Currency")

    '        Currency = hf_Currency.Value.ToString()

    '        Dim txtMon As String
    '        Dim txtTue As String
    '        Dim txtWed As String
    '        Dim txtThu As String
    '        Dim txtFri As String
    '        Dim txtSat As String
    '        Dim txtSun As String

    '        If Currency = "$" Then
    '            'txt = Regex.Replace(lblMon_Amount.Text, "$", " ")
    '            txtMon = lblMon_Amount.Text.Replace("$", " ")
    '            txtTue = lblTue_Amount.Text.Replace("$", " ")
    '            txtWed = lblWed_Amount.Text.Replace("$", " ")
    '            txtThu = lblThu_Amount.Text.Replace("$", " ")
    '            txtFri = lblFri_Amount.Text.Replace("$", " ")
    '            txtSat = lblSat_Amount.Text.Replace("$", " ")
    '            txtSun = lblSun_Amount.Text.Replace("$", " ")
    '        Else
    '            'txt = Regex.Replace(lblMon_Amount.Text, "£", " ")
    '            txtMon = lblMon_Amount.Text.Replace("£", " ")
    '            txtTue = lblTue_Amount.Text.Replace("£", " ")
    '            txtWed = lblWed_Amount.Text.Replace("£", " ")
    '            txtThu = lblThu_Amount.Text.Replace("£", " ")
    '            txtFri = lblFri_Amount.Text.Replace("£", " ")
    '            txtSat = lblSat_Amount.Text.Replace("£", " ")
    '            txtSun = lblSun_Amount.Text.Replace("£", " ")
    '        End If


    '        'Dim mon_amount As Decimal = Decimal.Parse(IIf(lblMon_Amount.Text = "", 0, lblMon_Amount.Text))
    '        'Dim tue_amount As Decimal = Decimal.Parse(IIf(lblTue_Amount.Text = "", 0, lblTue_Amount.Text))
    '        'Dim wed_amount As Decimal = Decimal.Parse(IIf(lblWed_Amount.Text = "", 0, lblWed_Amount.Text))
    '        'Dim thu_amount As Decimal = Decimal.Parse(IIf(lblThu_Amount.Text = "", 0, lblThu_Amount.Text))
    '        'Dim fri_amount As Decimal = Decimal.Parse(IIf(lblFri_Amount.Text = "", 0, lblFri_Amount.Text))
    '        'Dim sat_amount As Decimal = Decimal.Parse(IIf(lblSat_Amount.Text = "", 0, lblSat_Amount.Text))
    '        'Dim sun_amount As Decimal = Decimal.Parse(IIf(lblSun_Amount.Text = "", 0, lblSun_Amount.Text))

    '        Dim mon_amount As Decimal = Decimal.Parse(IIf(txtMon.ToString() = "", 0, txtMon.ToString()))
    '        Dim tue_amount As Decimal = Decimal.Parse(IIf(txtTue.ToString() = "", 0, txtTue.ToString()))
    '        Dim wed_amount As Decimal = Decimal.Parse(IIf(txtWed.ToString() = "", 0, txtWed.ToString()))
    '        Dim thu_amount As Decimal = Decimal.Parse(IIf(txtThu.ToString() = "", 0, txtThu.ToString()))
    '        Dim fri_amount As Decimal = Decimal.Parse(IIf(txtFri.ToString() = "", 0, txtFri.ToString()))
    '        Dim sat_amount As Decimal = Decimal.Parse(IIf(txtSat.ToString() = "", 0, txtSat.ToString()))
    '        Dim sun_amount As Decimal = Decimal.Parse(IIf(txtSun.ToString() = "", 0, txtSun.ToString()))

    '        If head <> "<b>BANK DETAILS</b>" Then


    '            If head = "<b>SALES DETAILS</b>" Then


    '                totalmon_amount += mon_amount
    '                totaltue_amount += tue_amount
    '                totalwed_amount += wed_amount
    '                totalthu_amount += thu_amount
    '                totalfri_amount += fri_amount
    '                totalsat_amount += sat_amount
    '                totalsun_amount += sun_amount
    '            Else
    '                totalmon_amount -= mon_amount
    '                totaltue_amount -= tue_amount
    '                totalwed_amount -= wed_amount
    '                totalthu_amount -= thu_amount
    '                totalfri_amount -= fri_amount
    '                totalsat_amount -= sat_amount
    '                totalsun_amount -= sun_amount
    '            End If



    '            'lblAmount.Text = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount
    '            amount = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount
    '            lblAmount.Text = Currency + " " + amount.ToString()
    '        End If
    '    End If


    '    If (e.Row.RowType = DataControlRowType.Footer) Then


    '        Dim lblTotalMon_Amount As Label = e.Row.FindControl("lblMon_TotalAmount")
    '        Dim lblTotalTue_Amount As Label = e.Row.FindControl("lblTue_TotalAmount")
    '        Dim lblTotalWed_Amount As Label = e.Row.FindControl("lblWed_TotalAmount")
    '        Dim lblTotalThu_Amount As Label = e.Row.FindControl("lblThu_TotalAmount")
    '        Dim lblTotalFri_Amount As Label = e.Row.FindControl("lblFri_TotalAmount")
    '        Dim lblTotalSat_Amount As Label = e.Row.FindControl("lblSat_TotalAmount")
    '        Dim lblTotalSun_Amount As Label = e.Row.FindControl("lblSun_TotalAmount")
    '        Dim lblTotalAmount As Label = e.Row.FindControl("lblTotalAmount")


    '        lblTotalMon_Amount.Text = Currency + " " + totalmon_amount.ToString()
    '        lblTotalTue_Amount.Text = Currency + " " + totaltue_amount.ToString()
    '        lblTotalWed_Amount.Text = Currency + " " + totalwed_amount.ToString()
    '        lblTotalThu_Amount.Text = Currency + " " + totalthu_amount.ToString()
    '        lblTotalFri_Amount.Text = Currency + " " + totalfri_amount.ToString()
    '        lblTotalSat_Amount.Text = Currency + " " + totalsat_amount.ToString()
    '        lblTotalSun_Amount.Text = Currency + " " + totalsun_amount.ToString()

    '        'lblTotalAmount.Text = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount
    '        totalamount = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount

    '        lblTotalAmount.Text = Currency + " " + totalamount.ToString()

    '        totalmon_amount = 0
    '        totaltue_amount = 0
    '        totalwed_amount = 0
    '        totalthu_amount = 0
    '        totalfri_amount = 0
    '        totalsat_amount = 0
    '        totalsun_amount = 0

    '    End If

    'End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
            BindTableExp()
        Catch ex As Exception
            LogHelper.Error("Business_Done:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    'Protected Sub btnexpCash_Click(sender As Object, e As ImageClickEventArgs)
    '    Try
    '        Dim fileName As String = "Business_Done_ExpenseSummary" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

    '        ExportExcel(gv_CashReport, fileName)


    '    Catch ex As Exception
    '        LogHelper.Error("Business_Done:LinkButton1_Click:" + ex.Message)
    '    End Try
    'End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    'Protected Sub btnExcl_ServerClick(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim fileName As String = "Business_Done_CashSummary" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

    '        ExportExcel(gv_CashSummary, fileName)


    '    Catch ex As Exception
    '        LogHelper.Error("Business_Done:LinkButton1_Click:" + ex.Message)
    '    End Try
    'End Sub

    Public Sub ExportExcel(ByVal Grv As GridView, ByVal ExclName As String, ByVal Optional downloadTokenValue As String = "")
        Try

            For i As Integer = 0 To Grv.Columns.Count - 1

                If (Convert.ToString(Grv.Columns(i).HeaderText) = "Edit") OrElse (Convert.ToString(Grv.Columns(i).HeaderText) = "Delete") Then
                    Grv.Columns(i).Visible = False
                End If
            Next

            Dim dt As DataTable = CType(Grv.DataSource, DataTable)
            System.Web.HttpContext.Current.Response.ClearContent()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AppendCookie(New HttpCookie("fileDownloadToken", downloadTokenValue))
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", ExclName & ".xls"))
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel"
            Dim sw As StringWriter = New StringWriter()
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Grv.AllowPaging = False
            Grv.RenderControl(htw)
            System.Web.HttpContext.Current.Response.Write(sw.ToString())
            System.Web.HttpContext.Current.Response.[End]()
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub rptbusiness_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        'Reference the Repeater Item.
    '        Dim item As RepeaterItem = e.Item

    '        'Reference the Controls.
    '        'Dim lblhead As Label = e.Item.FindControl("lblNameHead")

    '        'If (lblhead.Text = "<b>SALES DETAILS</b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or lblhead.Text = "<b>EXPENSES DETAILS</b>") Then
    '        '    head = lblhead.Text
    '        'End If
    '        'Dim hf_Currency As HiddenField = e.Item.FindControl("hf_Currency")

    '        'Currency = hf_Currency.Value.ToString()
    '        'Dim customerId As String = (TryCast(item.FindControl("lblCustomerId"), Label)).Text


    '        Dim lblhead As Label = e.Item.FindControl("lblNameHead")

    '        If (lblhead.Text = "<b>SALES DETAILS</b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or lblhead.Text = "<b>EXPENSES DETAILS</b>") Then
    '            head = lblhead.Text
    '        End If

    '        Dim lblMon_Amount As Label = e.Item.FindControl("lblMon_Amount")
    '        Dim lblTue_Amount As Label = e.Item.FindControl("lblTue_Amount")
    '        Dim lblWed_Amount As Label = e.Item.FindControl("lblWed_Amount")
    '        Dim lblThu_Amount As Label = e.Item.FindControl("lblThu_Amount")
    '        Dim lblFri_Amount As Label = e.Item.FindControl("lblFri_Amount")
    '        Dim lblSat_Amount As Label = e.Item.FindControl("lblSat_Amount")
    '        Dim lblSun_Amount As Label = e.Item.FindControl("lblSun_Amount")
    '        Dim lblAmount As Label = e.Item.FindControl("lblAmount")

    '        Dim hf_Currency As HiddenField = e.Item.FindControl("hf_Currency")

    '        Currency = hf_Currency.Value.ToString()

    '        Dim txtMon As String
    '        Dim txtTue As String
    '        Dim txtWed As String
    '        Dim txtThu As String
    '        Dim txtFri As String
    '        Dim txtSat As String
    '        Dim txtSun As String

    '        If Currency = "$" Then
    '            'txt = Regex.Replace(lblMon_Amount.Text, "$", " ")
    '            txtMon = lblMon_Amount.Text.Replace("$", " ")
    '            txtTue = lblTue_Amount.Text.Replace("$", " ")
    '            txtWed = lblWed_Amount.Text.Replace("$", " ")
    '            txtThu = lblThu_Amount.Text.Replace("$", " ")
    '            txtFri = lblFri_Amount.Text.Replace("$", " ")
    '            txtSat = lblSat_Amount.Text.Replace("$", " ")
    '            txtSun = lblSun_Amount.Text.Replace("$", " ")
    '        Else
    '            'txt = Regex.Replace(lblMon_Amount.Text, "£", " ")
    '            txtMon = lblMon_Amount.Text.Replace("£", " ")
    '            txtTue = lblTue_Amount.Text.Replace("£", " ")
    '            txtWed = lblWed_Amount.Text.Replace("£", " ")
    '            txtThu = lblThu_Amount.Text.Replace("£", " ")
    '            txtFri = lblFri_Amount.Text.Replace("£", " ")
    '            txtSat = lblSat_Amount.Text.Replace("£", " ")
    '            txtSun = lblSun_Amount.Text.Replace("£", " ")
    '        End If


    '        'Dim mon_amount As Decimal = Decimal.Parse(IIf(lblMon_Amount.Text = "", 0, lblMon_Amount.Text))
    '        'Dim tue_amount As Decimal = Decimal.Parse(IIf(lblTue_Amount.Text = "", 0, lblTue_Amount.Text))
    '        'Dim wed_amount As Decimal = Decimal.Parse(IIf(lblWed_Amount.Text = "", 0, lblWed_Amount.Text))
    '        'Dim thu_amount As Decimal = Decimal.Parse(IIf(lblThu_Amount.Text = "", 0, lblThu_Amount.Text))
    '        'Dim fri_amount As Decimal = Decimal.Parse(IIf(lblFri_Amount.Text = "", 0, lblFri_Amount.Text))
    '        'Dim sat_amount As Decimal = Decimal.Parse(IIf(lblSat_Amount.Text = "", 0, lblSat_Amount.Text))
    '        'Dim sun_amount As Decimal = Decimal.Parse(IIf(lblSun_Amount.Text = "", 0, lblSun_Amount.Text))

    '        Dim mon_amount As Decimal = Decimal.Parse(IIf(txtMon.ToString() = "", 0, txtMon.ToString()))
    '        Dim tue_amount As Decimal = Decimal.Parse(IIf(txtTue.ToString() = "", 0, txtTue.ToString()))
    '        Dim wed_amount As Decimal = Decimal.Parse(IIf(txtWed.ToString() = "", 0, txtWed.ToString()))
    '        Dim thu_amount As Decimal = Decimal.Parse(IIf(txtThu.ToString() = "", 0, txtThu.ToString()))
    '        Dim fri_amount As Decimal = Decimal.Parse(IIf(txtFri.ToString() = "", 0, txtFri.ToString()))
    '        Dim sat_amount As Decimal = Decimal.Parse(IIf(txtSat.ToString() = "", 0, txtSat.ToString()))
    '        Dim sun_amount As Decimal = Decimal.Parse(IIf(txtSun.ToString() = "", 0, txtSun.ToString()))

    '        If head <> "<b>BANK DETAILS</b>" Then


    '            If head = "<b>SALES DETAILS</b>" Then


    '                totalmon_amount += mon_amount
    '                totaltue_amount += tue_amount
    '                totalwed_amount += wed_amount
    '                totalthu_amount += thu_amount
    '                totalfri_amount += fri_amount
    '                totalsat_amount += sat_amount
    '                totalsun_amount += sun_amount
    '            Else
    '                totalmon_amount -= mon_amount
    '                totaltue_amount -= tue_amount
    '                totalwed_amount -= wed_amount
    '                totalthu_amount -= thu_amount
    '                totalfri_amount -= fri_amount
    '                totalsat_amount -= sat_amount
    '                totalsun_amount -= sun_amount
    '            End If



    '            'lblAmount.Text = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount
    '            amount = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount
    '            lblAmount.Text = Currency + " " + amount.ToString()
    '        End If
    '    End If

    '    Dim lblTotalMon_Amount As Label = e.Item.FindControl("lblMon_TotalAmount")
    '    Dim lblTotalTue_Amount As Label = e.Item.FindControl("lblTue_TotalAmount")
    '    Dim lblTotalWed_Amount As Label = e.Item.FindControl("lblWed_TotalAmount")
    '    Dim lblTotalThu_Amount As Label = e.Item.FindControl("lblThu_TotalAmount")
    '    Dim lblTotalFri_Amount As Label = e.Item.FindControl("lblFri_TotalAmount")
    '    Dim lblTotalSat_Amount As Label = e.Item.FindControl("lblSat_TotalAmount")
    '    Dim lblTotalSun_Amount As Label = e.Item.FindControl("lblSun_TotalAmount")
    '    Dim lblTotalAmount As Label = e.Item.FindControl("lblTotalAmount")


    '    'lblTotalMon_Amount.Text = Currency + " " + totalmon_amount.ToString()
    '    'lblTotalTue_Amount.Text = Currency + " " + totaltue_amount.ToString()
    '    'lblTotalWed_Amount.Text = Currency + " " + totalwed_amount.ToString()
    '    'lblTotalThu_Amount.Text = Currency + " " + totalthu_amount.ToString()
    '    'lblTotalFri_Amount.Text = Currency + " " + totalfri_amount.ToString()
    '    'lblTotalSat_Amount.Text = Currency + " " + totalsat_amount.ToString()
    '    'lblTotalSun_Amount.Text = Currency + " " + totalsun_amount.ToString()

    '    ''lblTotalAmount.Text = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount
    '    'totalamount = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount

    '    'lblTotalAmount.Text = Currency + " " + totalamount.ToString()

    '    'totalmon_amount = 0
    '    'totaltue_amount = 0
    '    'totalwed_amount = 0
    '    'totalthu_amount = 0
    '    'totalfri_amount = 0
    '    'totalsat_amount = 0
    '    'totalsun_amount = 0

    'End Sub



    Protected Sub rptbusiness_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        If e.Item.ItemType = ListItemType.Header Then
            Dim rptbusiness As Repeater = TryCast(e.Item.FindControl("Header1"), Repeater)
            rptbusiness.DataSource = dtExp.Columns
            rptbusiness.DataBind()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptbusiness As Repeater = TryCast(e.Item.FindControl("Item1"), Repeater)
            Dim row = TryCast(e.Item.DataItem, System.Data.DataRowView)

            'Reference the Repeater Item.
            Dim item As RepeaterItem = e.Item

            'Reference the Controls.
            'Dim lblhead As Label = e.Item.FindControl("lblNameHead")

            'If (lblhead.Text = "<b>SALES DETAILS</b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or lblhead.Text = "<b>EXPENSES DETAILS</b>") Then
            '    head = lblhead.Text
            'End If
            'Dim hf_Currency As HiddenField = e.Item.FindControl("hf_Currency")

            'Currency = hf_Currency.Value.ToString()
            'Dim customerId As String = (TryCast(item.FindControl("lblCustomerId"), Label)).Text


            Dim lblhead As Label = e.Item.FindControl("lblNameHead")



            If (lblhead.Text = "<b><u>Sub Total</u></b>" Or lblhead.Text = "VAT" Or
                lblhead.Text = "Cost of Sales" Or lblhead.Text = "Surcharge" Or lblhead.Text = "<b><u>Variable Expense</u></b>" Or
                lblhead.Text = "<b><u>Fixed Expense</u></b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or
                lblhead.Text = "<b>EXPENSES DETAILS</b>" Or lblhead.Text = "<b><u>Cash Expense</u></b>" Or
                lblhead.Text = "<b><u>Total Safe</u></b>" Or lblhead.Text = "Total Cash" Or lblhead.Text = "Total Card" Or
                lblhead.Text = "Payment Received" Or lblhead.Text = "<b>CHIPS</b>") Then
                head = lblhead.Text
            End If

            Dim lblMon_Amount As Label = e.Item.FindControl("lblMon_Amount")
            Dim lblTue_Amount As Label = e.Item.FindControl("lblTue_Amount")
            Dim lblWed_Amount As Label = e.Item.FindControl("lblWed_Amount")
            Dim lblThu_Amount As Label = e.Item.FindControl("lblThu_Amount")
            Dim lblFri_Amount As Label = e.Item.FindControl("lblFri_Amount")
            Dim lblSat_Amount As Label = e.Item.FindControl("lblSat_Amount")
            Dim lblSun_Amount As Label = e.Item.FindControl("lblSun_Amount")
            Dim lblAmount As Label = e.Item.FindControl("lblAmount")

            Dim hf_Currency As HiddenField = e.Item.FindControl("hf_Currency")

            Currency = hf_Currency.Value.ToString()


            Dim txtMon As String
            Dim txtTue As String
            Dim txtWed As String
            Dim txtThu As String
            Dim txtFri As String
            Dim txtSat As String
            Dim txtSun As String

            If Currency = "$" Then
                'txt = Regex.Replace(lblMon_Amount.Text, "$", " ")
                txtMon = lblMon_Amount.Text.Replace("$", " ")
                txtTue = lblTue_Amount.Text.Replace("$", " ")
                txtWed = lblWed_Amount.Text.Replace("$", " ")
                txtThu = lblThu_Amount.Text.Replace("$", " ")
                txtFri = lblFri_Amount.Text.Replace("$", " ")
                txtSat = lblSat_Amount.Text.Replace("$", " ")
                txtSun = lblSun_Amount.Text.Replace("$", " ")
            ElseIf Currency = "R" Then
                'txt = Regex.Replace(lblMon_Amount.Text, "£", " ")
                txtMon = lblMon_Amount.Text.Replace("R", " ")
                txtTue = lblTue_Amount.Text.Replace("R", " ")
                txtWed = lblWed_Amount.Text.Replace("R", " ")
                txtThu = lblThu_Amount.Text.Replace("R", " ")
                txtFri = lblFri_Amount.Text.Replace("R", " ")
                txtSat = lblSat_Amount.Text.Replace("R", " ")
                txtSun = lblSun_Amount.Text.Replace("R", " ")
            Else
                'txt = Regex.Replace(lblMon_Amount.Text, "£", " ")
                txtMon = lblMon_Amount.Text.Replace("£", " ")
                txtTue = lblTue_Amount.Text.Replace("£", " ")
                txtWed = lblWed_Amount.Text.Replace("£", " ")
                txtThu = lblThu_Amount.Text.Replace("£", " ")
                txtFri = lblFri_Amount.Text.Replace("£", " ")
                txtSat = lblSat_Amount.Text.Replace("£", " ")
                txtSun = lblSun_Amount.Text.Replace("£", " ")
            End If


            'Dim mon_amount As Decimal = Decimal.Parse(IIf(lblMon_Amount.Text = "", 0, lblMon_Amount.Text))
            'Dim tue_amount As Decimal = Decimal.Parse(IIf(lblTue_Amount.Text = "", 0, lblTue_Amount.Text))
            'Dim wed_amount As Decimal = Decimal.Parse(IIf(lblWed_Amount.Text = "", 0, lblWed_Amount.Text))
            'Dim thu_amount As Decimal = Decimal.Parse(IIf(lblThu_Amount.Text = "", 0, lblThu_Amount.Text))
            'Dim fri_amount As Decimal = Decimal.Parse(IIf(lblFri_Amount.Text = "", 0, lblFri_Amount.Text))
            'Dim sat_amount As Decimal = Decimal.Parse(IIf(lblSat_Amount.Text = "", 0, lblSat_Amount.Text))
            'Dim sun_amount As Decimal = Decimal.Parse(IIf(lblSun_Amount.Text = "", 0, lblSun_Amount.Text))

            Dim mon_amount As Decimal = Decimal.Parse(IIf(txtMon.ToString() = "", 0, txtMon.ToString()))
            Dim tue_amount As Decimal = Decimal.Parse(IIf(txtTue.ToString() = "", 0, txtTue.ToString()))
            Dim wed_amount As Decimal = Decimal.Parse(IIf(txtWed.ToString() = "", 0, txtWed.ToString()))
            Dim thu_amount As Decimal = Decimal.Parse(IIf(txtThu.ToString() = "", 0, txtThu.ToString()))
            Dim fri_amount As Decimal = Decimal.Parse(IIf(txtFri.ToString() = "", 0, txtFri.ToString()))
            Dim sat_amount As Decimal = Decimal.Parse(IIf(txtSat.ToString() = "", 0, txtSat.ToString()))
            Dim sun_amount As Decimal = Decimal.Parse(IIf(txtSun.ToString() = "", 0, txtSun.ToString()))

            'If head <> "<b>BANK DETAILS</b>" Then


            If head = "<b><u>Sub Total</u></b>" Then 'Or head = "<b>SALES DETAILS</b>" Or head = "<b>OTHER INCOME</b>" Or head = "<b>CHIPS</b>"


                totalmon_amount += mon_amount
                totaltue_amount += tue_amount
                totalwed_amount += wed_amount
                totalthu_amount += thu_amount
                totalfri_amount += fri_amount
                totalsat_amount += sat_amount
                totalsun_amount += sun_amount
            ElseIf head = "VAT" Or head = "Cost of Sales" Or head = "Surcharge" Or head = "<b><u>Variable Expense</u></b>" Or head = "<b><u>Fixed Expense</u></b>" Then
                totalmon_amount -= mon_amount
                totaltue_amount -= tue_amount
                totalwed_amount -= wed_amount
                totalthu_amount -= thu_amount
                totalfri_amount -= fri_amount
                totalsat_amount -= sat_amount
                totalsun_amount -= sun_amount
            End If
            'lblAmount.Text = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount
            amount = mon_amount + tue_amount + wed_amount + thu_amount + fri_amount + sat_amount + sun_amount

            If head = "Total Card" Then

                lblAmount.Text = Currency + " " + amount.ToString()

            Else

                lblAmount.Text = Currency + " " + amount.ToString()

            End If

            'End If

            'If lblhead.Text = "<b><u>Sub Total</u></b>" Then
            '    lblThu_Amount.Text = "<b>" + lblThu_Amount.Text + "</b>"
            'End If

            rptbusiness.DataSource = row.Row.ItemArray
            rptbusiness.DataBind()


        End If



        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblTotalMon_Amount As Label = e.Item.FindControl("lblMon_TotalAmount")
            Dim lblTotalTue_Amount As Label = e.Item.FindControl("lblTue_TotalAmount")
            Dim lblTotalWed_Amount As Label = e.Item.FindControl("lblWed_TotalAmount")
            Dim lblTotalThu_Amount As Label = e.Item.FindControl("lblThu_TotalAmount")
            Dim lblTotalFri_Amount As Label = e.Item.FindControl("lblFri_TotalAmount")
            Dim lblTotalSat_Amount As Label = e.Item.FindControl("lblSat_TotalAmount")
            Dim lblTotalSun_Amount As Label = e.Item.FindControl("lblSun_TotalAmount")
            Dim lblTotalAmount As Label = e.Item.FindControl("lblTotalAmount")

            'added


            'added end

            'Currency = "$"

            If (dtExp.Columns(1).ToString() = "Thursday") Then
                lblTotalMon_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalwed_amount.ToString()

                'lblTotalAmount.Text = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount

            ElseIf (dtExp.Columns(1).ToString() = "Friday") Then

                lblTotalMon_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalthu_amount.ToString()


            ElseIf (dtExp.Columns(1).ToString() = "Saturday") Then

                lblTotalMon_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalfri_amount.ToString()

            ElseIf (dtExp.Columns(1).ToString() = "Sunday") Then

                lblTotalMon_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalsat_amount.ToString()

            ElseIf (dtExp.Columns(1).ToString() = "Monday") Then

                lblTotalMon_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalsun_amount.ToString()

            ElseIf (dtExp.Columns(1).ToString() = "Tuesday") Then

                lblTotalMon_Amount.Text = Currency + " " + totaltue_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totalmon_amount.ToString()

            ElseIf (dtExp.Columns(1).ToString() = "Wednesday") Then

                lblTotalMon_Amount.Text = Currency + " " + totalwed_amount.ToString()
                lblTotalTue_Amount.Text = Currency + " " + totalthu_amount.ToString()
                lblTotalWed_Amount.Text = Currency + " " + totalfri_amount.ToString()
                lblTotalThu_Amount.Text = Currency + " " + totalsat_amount.ToString()
                lblTotalFri_Amount.Text = Currency + " " + totalsun_amount.ToString()
                lblTotalSat_Amount.Text = Currency + " " + totalmon_amount.ToString()
                lblTotalSun_Amount.Text = Currency + " " + totaltue_amount.ToString()
            End If

            totalamount = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount
            lblTotalAmount.Text = Currency + " " + totalamount.ToString()
            totalmon_amount = 0
            totaltue_amount = 0
            totalwed_amount = 0
            totalthu_amount = 0
            totalfri_amount = 0
            totalsat_amount = 0
            totalsun_amount = 0

        End If

    End Sub


    Protected Sub rptcash_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        If e.Item.ItemType = ListItemType.Header Then
            Dim rptcash As Repeater = TryCast(e.Item.FindControl("Header1"), Repeater)
            rptcash.DataSource = dtRptCash.Columns
            rptcash.DataBind()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptcash As Repeater = TryCast(e.Item.FindControl("Item1"), Repeater)
            Dim row = TryCast(e.Item.DataItem, System.Data.DataRowView)
            rptcash.DataSource = row.Row.ItemArray
            rptcash.DataBind()


        End If


    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
