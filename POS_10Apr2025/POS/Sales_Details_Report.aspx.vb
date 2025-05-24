Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Sales_Details_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            ReportViewer1.Visible = True

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password") & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim sourceReportSource = New UriReportSource() With { _
                 .Uri = Server.MapPath("~/Sales_Details_Rpt.trdx") _
            }

            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today")))

         
            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            LogHelper.Error("Sales_Details_Report:BindTable:" + ex.Message)
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

            End If
        Catch ex As Exception
            LogHelper.Error("Sales_Details_Report:Page_Load:" + ex.Message)
        End Try
        

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Sales Details"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Sales_Details_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Sales_Details_Report:LinkButton1_Click:" + ex.Message)
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
            LogHelper.Error("Sales_Details_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

End Class



