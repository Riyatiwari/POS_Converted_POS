Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Partial Class Site_Summary
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsRole As Controller_clsRole
    Dim oclExpense As New Controller_clsExpenseMaster()

    Dim totalmon_amount As Decimal
    Dim totaltue_amount As Decimal
    Dim totalwed_amount As Decimal
    Dim totalthu_amount As Decimal
    Dim totalfri_amount As Decimal
    Dim totalsat_amount As Decimal
    Dim totalsun_amount As Decimal
    Dim Currency As String

    Private Sub BindTable()
        Try
            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)


            Dim date1 As DateTime = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            Dim date2 As DateTime = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)

            oclExpense.date1 = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy")
            oclExpense.date2 = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy")
            Dim dtReport As DataTable = oclExpense.Bank_Summary()

            If dtReport.Rows.Count > 0 Then
                gv_CashReport.DataSource = dtReport
                gv_CashReport.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("bank_report:BindTable:" + ex.Message)
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
            LogHelper.Error("bank_report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Bank Summary"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("bank_report:RoleCheck:" + ex.Message)
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
            LogHelper.Error("bank_report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub gv_CashReport_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gv_CashReport.RowDataBound


        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim lblMon_Amount As Label = e.Row.FindControl("lblMon_Amount")
            Dim hf_Currency As HiddenField = e.Row.FindControl("hf_Currency")

            Currency = hf_Currency.Value.ToString()

            Dim txt As String
            If Currency = "$" Then
                'txt = Regex.Replace(lblMon_Amount.Text, "$", " ")
                txt = lblMon_Amount.Text.Replace("$", " ")
            Else
                'txt = Regex.Replace(lblMon_Amount.Text, "£", " ")
                txt = lblMon_Amount.Text.Replace("£", " ")
            End If

            'Dim mon_amount As Decimal = Decimal.Parse(IIf(lblMon_Amount.Text = "", 0, lblMon_Amount.Text))
            Dim mon_amount As Decimal = Decimal.Parse(IIf(txt.ToString = "", 0, txt.ToString))


            totalmon_amount += mon_amount

        End If


        If (e.Row.RowType = DataControlRowType.Footer) Then


            Dim lblTotalMon_Amount As Label = e.Row.FindControl("lblMon_TotalAmount")

            Dim totalfooter As String
            totalfooter = Currency + " " + totalmon_amount.ToString()

            lblTotalMon_Amount.Text = totalfooter.ToString()

        End If

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub btnExcl_ServerClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim fileName As String = "BankSummary" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

            ExportExcel(gv_CashReport, fileName)


        Catch ex As Exception
            LogHelper.Error("bank_report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub ExportExcel(ByVal Grv As GridView, ByVal ExclName As String, ByVal Optional downloadTokenValue As String = "")
        Try
            '1
            'Dim sw1 As StringWriter = New StringWriter()
            'Dim hw1 As HtmlTextWriter = New HtmlTextWriter(sw1)

            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("content-disposition", "attachment;filename={0}" + ExclName + ".xls")
            'Response.Charset = ""
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.ContentEncoding = System.Text.Encoding.Default
            'Grv.RenderControl(hw1)

            'Dim Style As String = "<style>.text{mso-number-format:\\@;}</style>"
            'Response.Write(Style)
            'Response.Output.Write(sw1.ToString())
            'Response.Flush()
            'Response.ClearHeaders()
            'Response.Close()
            'HttpContext.Current.ApplicationInstance.CompleteRequest()


            '2
            'Response.Clear()
            'Response.AddHeader("content-disposition", "attachment; filename=" + ExclName + ".xls")
            'Response.ContentType = "application/vnd.xls"
            'Dim WriteItem As System.IO.StringWriter = New System.IO.StringWriter()
            'Dim htmlText As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(WriteItem)
            'gv_CashReport.AllowPaging = False
            'gv_CashReport.DataBind()
            'gv_CashReport.RenderControl(htmlText)
            'Response.Write(WriteItem.ToString())
            'Response.End()

            '3
            'Response.ClearContent()
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.AddHeader("Content-Disposition", "attachment; filename=" + ExclName + ".xls")
            'Response.ContentEncoding = System.Text.Encoding.Unicode
            'Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
            'Dim sb As StringBuilder = New StringBuilder()
            'Dim tw As StringWriter = New StringWriter(sb)
            'Dim hw As HtmlTextWriter = New HtmlTextWriter(tw)
            'gv_CashReport.RenderControl(hw)
            'Response.Write(sb)
            'Response.End()

            '4
            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("content-disposition", "attachment;filename=" + ExclName + ".xls")
            'Response.Charset = ""
            'Response.ContentType = "application/vnd.ms-excel"
            'Using sw As New StringWriter()
            '    Dim hw As New HtmlTextWriter(sw)

            '    'To Export all pages  
            '    gv_CashReport.AllowPaging = False

            '    Dim date1 As DateTime = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            '    Dim date2 As DateTime = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)

            '    oclExpense.date1 = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy")
            '    oclExpense.date2 = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy")
            '    Dim dtReport As DataTable = oclExpense.Bank_Summary()

            '    If dtReport.Rows.Count > 0 Then
            '        gv_CashReport.DataSource = dtReport
            '        gv_CashReport.DataBind()
            '    End If

            '    gv_CashReport.RenderControl(hw)
            '    'style to format numbers to string  
            '    Dim style As String = "<style> .textmode { } </style>"
            '    Response.Write(style)
            '    Response.Output.Write(sw.ToString())
            '    Response.Flush()
            '    Response.[End]()
            'End Using

            '5
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
