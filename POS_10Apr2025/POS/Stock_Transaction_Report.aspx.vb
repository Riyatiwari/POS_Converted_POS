Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting


Partial Class Stock_Transaction_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            ReportViewer1.Visible = True
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            'If rbtDisplayReport.SelectedValue = 0 Then
            Dim sourceReportSource = New UriReportSource() With {
                 .Uri = Server.MapPath("~/Stock_Transaction_report.trdx")
            }
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("Product_Group_Id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("Product_id", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))

            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            LogHelper.Error("Stock_Transaction_Report:BindTable:" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'oclsBind.BindProduct(ddlproduct, Val(Session("cmp_id")))
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
                oclsBind.BindProductGroup(radCategory, Val(Session("cmp_id")))
                BindTable()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Transaction_Report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Stock Transaction"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Transaction_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Stock_Transaction_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub radCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radCategory.SelectedIndexChanged
        Try
            If radCategory.SelectedIndex = 0 Then
                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))
            Else
                oclsBind.BindProductByProductGroup(radProduct, Val(Session("cmp_id")), Val(radCategory.SelectedValue))
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Transaction_Report:radCategory_SelectedIndexChanged:" + ex.Message)
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
            LogHelper.Error("Stock_Transaction_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
End Class
