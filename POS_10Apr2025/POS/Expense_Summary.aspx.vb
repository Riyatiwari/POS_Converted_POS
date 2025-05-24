Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Partial Class Expense_Summary
    Inherits BaseClass
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

    Dim thisWeekStart As DateTime
    Dim thisWeekEnd As DateTime

    Private Sub BindTable()
        Try
            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            'Dim baseDate As DateTime = txtForDate.SelectedDate
            'Dim today = baseDate
            'Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
            'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

            'txtForDate.SelectedDate = thisWeekStart.AddDays(1)
            'txtToDate.SelectedDate = thisWeekEnd

            Dim date1 As DateTime = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            Dim date2 As DateTime = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)

            oclExpense.date1 = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy")
            oclExpense.date2 = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy")
            oclExpense.location_id = rdlocation.SelectedValue
            Dim dtReport As DataTable = oclExpense.Expense_Summary()

            If dtReport.Rows.Count > 0 Then
                gv_CashReport.DataSource = dtReport
                gv_CashReport.DataBind()
            Else
                gv_CashReport.DataSource = Nothing
                gv_CashReport.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Cash_Up_Report:BindTable:" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                Else
                    ViewState("view") = 0
                End If

                If Val(ViewState("view")) = 1 Then
                    divFunction.Visible = True
                Else
                    divFunction.Visible = False
                End If
                oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))
                Dim baseDate As DateTime = DateTime.Now
                Dim today = baseDate
                Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

                txtForDate.SelectedDate = thisWeekStart.AddDays(1)
                txtToDate.SelectedDate = thisWeekEnd


                BindTable()
                'Dim baseDate As DateTime = DateTime.Now
                'Dim today = baseDate
                'Dim yesterday = baseDate.AddDays(-1)
                'Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)
                'Dim lastWeekStart = thisWeekStart.AddDays(-7)
                'Dim lastWeekEnd = thisWeekStart.AddSeconds(-1)
                'Dim thisMonthStart = baseDate.AddDays(1 - baseDate.Day)
                'Dim thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1)
                'Dim lastMonthStart = thisMonthStart.AddMonths(-1)
                'Dim lastMonthEnd = thisMonthStart.AddSeconds(-1)
                'txtToDate.SelectedDate = lastWeekEnd


            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Up_Report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Expense Summary"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Cash_Up_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            Dim baseDate As DateTime = txtToDate.SelectedDate

            While baseDate.DayOfWeek <> DayOfWeek.Monday
                baseDate = baseDate.AddDays(-1)
            End While

            Dim thisWeekStart As DateTime = baseDate
            Dim thisWeekEnd As DateTime = baseDate.AddDays(6)

            txtForDate.SelectedDate = thisWeekStart
            txtToDate.SelectedDate = thisWeekEnd

            BindTable()
        Catch ex As Exception
            LogHelper.Error("Cash_Up_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub gv_CashReport_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gv_CashReport.RowDataBound


        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim lblhead As Label = e.Row.FindControl("lblNameHead")

            If (lblhead.Text = "<b>SALES DETAILS</b>" Or lblhead.Text = "<b>BANK DETAILS</b>" Or lblhead.Text = "<b>EXPENSES DETAILS</b>") Then
                head = lblhead.Text
            End If

            Dim lblMon_Amount As Label = e.Row.FindControl("lblMon_Amount")
            Dim lblTue_Amount As Label = e.Row.FindControl("lblTue_Amount")
            Dim lblWed_Amount As Label = e.Row.FindControl("lblWed_Amount")
            Dim lblThu_Amount As Label = e.Row.FindControl("lblThu_Amount")
            Dim lblFri_Amount As Label = e.Row.FindControl("lblFri_Amount")
            Dim lblSat_Amount As Label = e.Row.FindControl("lblSat_Amount")
            Dim lblSun_Amount As Label = e.Row.FindControl("lblSun_Amount")
            Dim lblAmount As Label = e.Row.FindControl("lblAmount")

            Dim hf_Currency As HiddenField = e.Row.FindControl("hf_Currency")

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

            If head <> "<b>BANK DETAILS</b>" Then


                If head = "<b>SALES DETAILS</b>" Then


                    totalmon_amount += mon_amount
                    totaltue_amount += tue_amount
                    totalwed_amount += wed_amount
                    totalthu_amount += thu_amount
                    totalfri_amount += fri_amount
                    totalsat_amount += sat_amount
                    totalsun_amount += sun_amount
                Else
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
                lblAmount.Text = Currency + " " + amount.ToString()
            End If
        End If


        If (e.Row.RowType = DataControlRowType.Footer) Then


            Dim lblTotalMon_Amount As Label = e.Row.FindControl("lblMon_TotalAmount")
            Dim lblTotalTue_Amount As Label = e.Row.FindControl("lblTue_TotalAmount")
            Dim lblTotalWed_Amount As Label = e.Row.FindControl("lblWed_TotalAmount")
            Dim lblTotalThu_Amount As Label = e.Row.FindControl("lblThu_TotalAmount")
            Dim lblTotalFri_Amount As Label = e.Row.FindControl("lblFri_TotalAmount")
            Dim lblTotalSat_Amount As Label = e.Row.FindControl("lblSat_TotalAmount")
            Dim lblTotalSun_Amount As Label = e.Row.FindControl("lblSun_TotalAmount")
            Dim lblTotalAmount As Label = e.Row.FindControl("lblTotalAmount")


            lblTotalMon_Amount.Text = Currency + " " + totalmon_amount.ToString()
            lblTotalTue_Amount.Text = Currency + " " + totaltue_amount.ToString()
            lblTotalWed_Amount.Text = Currency + " " + totalwed_amount.ToString()
            lblTotalThu_Amount.Text = Currency + " " + totalthu_amount.ToString()
            lblTotalFri_Amount.Text = Currency + " " + totalfri_amount.ToString()
            lblTotalSat_Amount.Text = Currency + " " + totalsat_amount.ToString()
            lblTotalSun_Amount.Text = Currency + " " + totalsun_amount.ToString()

            totalamount = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount

            lblTotalAmount.Text = Currency + " " + totalamount.ToString()
            'lblTotalAmount.Text = totalmon_amount + totaltue_amount + totalwed_amount + totalthu_amount + totalfri_amount + totalsat_amount + totalsun_amount

            totalmon_amount = 0
            totaltue_amount = 0
            totalwed_amount = 0
            totalthu_amount = 0
            totalfri_amount = 0
            totalsat_amount = 0
            totalsun_amount = 0

        End If

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub btnExcl_ServerClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim fileName As String = "ExpenseSummary" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

            ExportExcel(gv_CashReport, fileName)


        Catch ex As Exception
            LogHelper.Error("bank_report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

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
End Class
