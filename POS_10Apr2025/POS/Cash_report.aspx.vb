Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports System.Xml
Imports Telerik.Reporting.XmlSerialization
Imports System.IO
Imports System.Web.Configuration

Partial Class Cash_report
    Inherits BaseClass
    Dim oclsRole As Controller_clsRole
    Dim oclsreportconnectionstringManager As ReportConnectionStringManager
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

                txtFrom.SelectedDate = thisWeekStart.AddDays(1)
                txtTo.SelectedDate = thisWeekEnd
                BindTable()
            End If
        Catch ex As Exception
            LogHelper.Error("Sales_Report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Cash Declaration Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Sales_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Private Sub BindTable()
        Try

            ReportViewer1.Visible = True

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim sourceReportSource = New UriReportSource() With { _
                 .Uri = Server.MapPath("~/Cash_report.trdx") _
            }

            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today")))

            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource
            Me.ReportViewer1.RefreshReport()


        Catch ex As Exception
            LogHelper.Error("Sales_Report:BindTable:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Sales_Report:LinkButton1_Click:" + ex.Message)
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


