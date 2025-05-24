
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Sales_By_Staff_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@emp_id", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0"), SqlDbType.NVarChar)
            oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            Dim dt As DataTable = oClsDal.GetdataTableSp("P_R_Sales_By_Staff", oColSqlparram)

            If dt.Rows.Count > 0 Then
                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()
            Else
                rptProductSUmmary.DataSource = String.Empty
                rptProductSUmmary.DataBind()
            End If



            'ReportViewer1.Visible = True

            'Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)
            'If rdType.SelectedValue = "SALE" Then

            '    ''Dim sourceReportSource = New UriReportSource() With {
            '    ''     .Uri = Server.MapPath("~/Sales_By_Staff_Report_Sale.trdx")
            '    ''}

            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    'Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    'Me.ReportViewer1.ReportSource = reportSource

            '    'Me.ReportViewer1.RefreshReport()
            'ElseIf rdType.SelectedValue = "RETURN" Then
            '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            '    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            '    oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            '    oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            '    oColSqlparram.Add("@emp_id", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0"), SqlDbType.NVarChar)
            '    oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)
            '    oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            '    Dim dt As DataTable = oClsDal.GetdataTableSp("P_R_Sales_By_Staff", oColSqlparram)


            '    rptProductSUmmary.DataSource = dt
            '    rptProductSUmmary.DataBind()
            '    ''Dim sourceReportSource = New UriReportSource() With {
            '    ''     .Uri = Server.MapPath("~/Sales_By_Staff_Report_Return.trdx")
            '    ''}

            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    'Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    'Me.ReportViewer1.ReportSource = reportSource

            '    'Me.ReportViewer1.RefreshReport()
            'Else
            '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            '    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            '    oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            '    oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            '    oColSqlparram.Add("@emp_id", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0"), SqlDbType.NVarChar)
            '    oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)
            '    oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            '    Dim dt As DataTable = oClsDal.GetdataTableSp("P_R_Sales_By_Staff", oColSqlparram)


            '    rptProductSUmmary.DataSource = dt
            '    rptProductSUmmary.DataBind()
            '    '  Dim sourceReportSource = New UriReportSource() With {
            '    '     .Uri = Server.MapPath("~/Sales_By_Staff_Report.trdx")
            '    '}

            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    '  sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    '  Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    '  Me.ReportViewer1.ReportSource = reportSource

            '    '  Me.ReportViewer1.RefreshReport()
            'End If

        Catch ex As Exception
            LogHelper.Error("Sales_By_Staff_Report:BindTable:" + ex.Message)
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

                txtFrom.SelectedDate = System.DateTime.Now
                txtTo.SelectedDate = System.DateTime.Now

                binddll()

            End If
        Catch ex As Exception
            LogHelper.Error("Sales_By_Staff_Report:Page_Load:" + ex.Message)
        End Try


    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Sales By Operator"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Sales_By_Staff_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Sales_By_Staff_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            oclsBind.BindStaff(radOprator, Val(Session("cmp_id")))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_By_Staff_Report:binddll:" + ex.Message)
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
            LogHelper.Error("Sales_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub


End Class


