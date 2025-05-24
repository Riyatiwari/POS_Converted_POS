Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Partial Class Machine_Hierarchy_Report
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
                 .Uri = Server.MapPath("~/Machine_Hierarchy_Report.trdx") _
            }

            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("locationid", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("machineid", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("venueid", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("deviceid", IIf(radDevice.SelectedIndex > 0, radDevice.SelectedValue, "0")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))
            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource

            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:BindTable:" + ex.Message)
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
            LogHelper.Error("Machine_Hierarchy_Report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Till Hierarchy"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()

        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            If radVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Machine_Hierarchy_Report:binddll:" + ex.Message)
        End Try
    End Sub

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
                radMachine.Items.Clear()
                radDevice.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                radMachine.Items.Clear()
                radDevice.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radLocation.SelectedIndexChanged
        Try
            If radLocation.SelectedIndex = 0 Then
                radMachine.Items.Clear()
                radDevice.Items.Clear()
            Else
                oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
                radDevice.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub radMachine_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radMachine.SelectedIndexChanged
        Try
            If radMachine.SelectedIndex = 0 Then
                radDevice.Items.Clear()
            Else
                oclsBind.BindDeviceByMachine(radDevice, Val(Session("cmp_id")), Val(radMachine.SelectedValue))
            End If

        Catch ex As Exception
            LogHelper.Error("Machine_Hierarchy_Report:radMachine_SelectedIndexChanged:" + ex.Message)
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
