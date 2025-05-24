Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Partial Class Cash_Up_report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsRole As Controller_clsRole
    Private Sub BindTable()
        Try
            ReportViewer1.Visible = True

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim sourceReportSource = New UriReportSource() With { _
                 .Uri = Server.MapPath("~/Cash_Up_Report.trdx") _
            }

            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("locationid", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("machineid", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0")))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("venueid", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0")))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("deviceid", IIf(radDevice.SelectedIndex > 0, radDevice.SelectedValue, "0")))
            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource

            Me.ReportViewer1.RefreshReport()
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

                Dim baseDate As DateTime = DateTime.Now
                Dim today = baseDate
                Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

                txtForDate.SelectedDate = System.DateTime.Now
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
            oclsRole.Form_Name = "Cash Control Report"
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
            BindTable()
            


        Catch ex As Exception
            LogHelper.Error("Cash_Up_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub SurroundingSub()

    End Sub


End Class
