Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports System.IO
Partial Class Stock_Transaction_Details
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

                If Request.QueryString("pid") = Nothing Then
                    hdsearchvalueAfterEdit.Value = Session("JquerySearchFilter")
                    Dim baseDate As DateTime = DateTime.Now

                    txtFrom.SelectedDate = System.DateTime.Now
                    txtTo.SelectedDate = System.DateTime.Now

                    binddll()
                    oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))


                    BindTable()
                Else
                    binddll()
                    oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
                    Dim pid As Int32 = Int32.Parse(Request.QueryString("pid").ToString())
                    Dim pgid As Int32 = Int32.Parse(Request.QueryString("pgid").ToString())
                    Dim lid As Int32 = Int32.Parse(Request.QueryString("L_ID").ToString())
                    Dim S_Date As DateTime = DateTime.Parse(Request.QueryString("Date").ToString())
                    Dim E_Date As DateTime = DateTime.Parse(Request.QueryString("E_Date").ToString())
                    Dim name As String = Request.QueryString("name").ToString()
                    txtFrom.SelectedDate = S_Date
                    txtTo.SelectedDate = E_Date
                    radDuration.SelectedValue = "Custom"
                    If pid > 0 Then
                        radProduct.SelectedValue = pid
                    End If

                    If lid > 0 Then
                        radLocation.SelectedValue = lid
                    End If

                    If pgid > 0 Then
                        radCategory.SelectedValue = pgid
                    End If

                    BindTable()
                End If



            End If

        Catch ex As Exception
            LogHelper.Error("Page_Load:till_report:" + ex.Message)
        End Try



    End Sub
    Public Sub binddll()
        Try
            If radCategory.SelectedIndex = -1 Then
                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))
            End If
            oclsBind.BindProductGroupALL(radCategory, Val(Session("cmp_id")))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Summary_Report:binddll:" + ex.Message)
        End Try
    End Sub
    Private Sub BindTable()
        Try
            Dim baseDate As DateTime = txtFrom.SelectedDate
            Dim today = baseDate
            Dim thisWeekStart = baseDate


            Dim toDate As DateTime = txtTo.SelectedDate
            Dim todayy = toDate
            'Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)
            Dim thisWeekEnd = todayy

            'Dim connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("POSConnectionString").ToString()

            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)


            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.date1 = thisWeekStart
            oclsSales.date2 = thisWeekEnd
            oclsSales.Location_id = radLocation.SelectedValue
            oclsSales.product_id = radProduct.SelectedValue
            oclsSales.duration = radDuration.SelectedValue
            oclsSales.category_id = radCategory.SelectedValue
            Dim dtReport As DataTable = oclsSales.Stock_Transaction_Details()

            If dtReport.Rows.Count > 0 Then


                rpstockSUmmary.DataSource = dtReport
                rpstockSUmmary.DataBind()
            Else

                rpstockSUmmary.DataSource = Nothing
                rpstockSUmmary.DataBind()
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
            'Dim fileName As String = "Stock_Transaction" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

            'ExportExcel(gv_CashSummary, fileName)


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

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Private Sub rpstockSUmmary_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rpstockSUmmary.ItemCommand
        Try
            Session("JquerySearchFilter") = hdnsearchvalue.Value
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub radCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindProductByProductGroup(radProduct, Val(Session("cmp_id")), Val(radCategory.SelectedValue))
        Catch ex As Exception

        End Try
    End Sub
End Class
