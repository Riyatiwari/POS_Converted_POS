
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Operator_Summary
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            'ReportViewer1.Visible = True

            Dim connectionString As String = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password") & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))
            oColSqlparram.Add("@type", IIf(rdType.SelectedIndex > 0, rdType.SelectedValue.ToString, "All"))
            oColSqlparram.Add("@emp_id", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue.ToString, 0))

            Dim dt As DataTable

            dt = oClsDal.GetdataTableSp("dbo.P_R_Operator_Summary", oColSqlparram)


            If dt.Rows.Count > 0 Then
                rptzSUmmary.DataSource = dt
                rptzSUmmary.DataBind()
            Else
                rptzSUmmary.DataSource = String.Empty
                rptzSUmmary.DataBind()
            End If




            'If rdType.SelectedValue = "SALE" Then

            '    Dim sourceReportSource = New UriReportSource() With { _
            '         .Uri = Server.MapPath("~/Operator_Summary_Sale.trdx") _
            '    }

            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    Me.ReportViewer1.ReportSource = reportSource

            '    Me.ReportViewer1.RefreshReport()
            'ElseIf rdType.SelectedValue = "RETURN" Then
            '    Dim sourceReportSource = New UriReportSource() With { _
            '         .Uri = Server.MapPath("~/Operator_Summary_Return.trdx") _
            '    }

            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    Me.ReportViewer1.ReportSource = reportSource
            'Else
            '    Dim sourceReportSource = New UriReportSource() With { _
            '         .Uri = Server.MapPath("~/Operator_Summary.trdx") _
            '    }

            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("empid", IIf(radOprator.SelectedIndex > 0, radOprator.SelectedValue, "0")))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            '    Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    Me.ReportViewer1.ReportSource = reportSource
            'End If


        Catch ex As Exception
            LogHelper.Error("Operator_Summary:BindTable:" + ex.Message)
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

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now

                binddll()


            End If
        Catch ex As Exception
            LogHelper.Error("Operator_Summary:Page_Load:" + ex.Message)
        End Try


    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Operator Summary"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Operator_Summary:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Operator_Summary:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            oclsBind.BindStaff(radOprator, Val(Session("cmp_id")))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Operator_Summary:binddll:" + ex.Message)
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

    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        BindTable()
    End Sub
    Protected Sub txtToDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        BindTable()
    End Sub

End Class


