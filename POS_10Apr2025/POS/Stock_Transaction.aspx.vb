Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports System.IO
Partial Class Stock_Transaction
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


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Dim baseDate As DateTime = DateTime.Now
                'Dim today = baseDate
                'Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

                txtdate.SelectedDate = baseDate
                txtToDate.SelectedDate = baseDate
                oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))


                BindTable()

            End If

        Catch ex As Exception
            LogHelper.Error("Page_Load:till_report:" + ex.Message)
        End Try



    End Sub
    Private Sub BindTable()
        Try
            Dim baseDate As DateTime = txtdate.SelectedDate
            Dim today = baseDate
            Dim thisWeekStart = baseDate


            Dim toDate As DateTime = txtToDate.SelectedDate
            Dim todayy = toDate
            'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)
            Dim thisWeekEnd = todayy

            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)


            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.date1 = thisWeekStart
            oclsSales.date2 = thisWeekEnd
            Dim dtReport As DataTable = oclsSales.Stock_Transaction()

            If dtReport.Rows.Count > 0 Then
                gv_CashSummary.DataSource = dtReport
                gv_CashSummary.DataBind()
            Else
                gv_CashSummary.DataSource = Nothing
                gv_CashSummary.DataBind()
            End If



        Catch ex As Exception
            LogHelper.Error("Business_Done:BindTable:" + ex.Message)
        End Try

    End Sub



    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()

        Catch ex As Exception
            LogHelper.Error("Business_Done:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub



    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub btnExcl_ServerClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim fileName As String = "Stock_Transaction" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

            ExportExcel(gv_CashSummary, fileName)


        Catch ex As Exception
            LogHelper.Error("Business_Done:LinkButton1_Click:" + ex.Message)
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
