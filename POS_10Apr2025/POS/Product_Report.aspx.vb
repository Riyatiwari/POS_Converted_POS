Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Product_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Private Sub BindTable()
        Try
            ReportViewer1.Visible = True

            Dim connectionString As String = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password") & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim sourceReportSource = New UriReportSource() With {
                 .Uri = Server.MapPath("~/Product_Report.trdx")
            }

            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))

            Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            Me.ReportViewer1.ReportSource = reportSource

            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            LogHelper.Error("Product_Report:BindTable:" + ex.Message)
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
                BindTable()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Report:Page_Load:" + ex.Message)
        End Try


    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Product Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

End Class
