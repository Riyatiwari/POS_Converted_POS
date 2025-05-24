Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel
Partial Class Stock_Process
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole



    Private Sub BindTable()
        Try
            'ReportViewer1.Visible = True
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()


            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@location_id", radLocation.SelectedValue)


            Dim dt As DataTable

            dt = oClsDal.GetdataTableSp("P_reprocess_stock", oColSqlparram)

            'dt.Rows.Add("")
            lblMsg.Text = "Stock Reprocessed successfully."
            'If dt.Rows.Count > 0 Then
            '    rdPayment.DataSource = dt
            '    rdPayment.DataBind()

            'Else
            '    rdPayment.DataSource = String.Empty
            '    rdPayment.DataBind()
            'End If

        Catch ex As Exception
            LogHelper.Error("Stock_Process:BindTable:" + ex.Message)
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
                    'divFunction.Visible = True
                Else
                    'divFunction.Visible = False
                End If

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now

                oclsBind.BindLocation_ddl(radLocation, Val(Session("cmp_id")))
                'BindTable()

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Process:Page_Load:" + ex.Message)
        End Try
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Stock ReProcess"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Process:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Stock_Process:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub


End Class
