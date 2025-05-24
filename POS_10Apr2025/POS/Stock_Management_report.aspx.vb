Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting

Partial Class Stock_Management_report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            'ReportViewer1.Visible = True
            'Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            'Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
            'If rbtDisplayReport.SelectedValue = 0 Then
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))
            oColSqlparram.Add("@flag", IIf(ddlflag.SelectedValue > 0, ddlflag.SelectedValue.ToString, "Changed"))
            oColSqlparram.Add("@Product_Group_Id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"))
            oColSqlparram.Add("@Location_Id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            Dim dt As DataTable
            dt = oClsDal.GetdataTableSp("ws_stock_report", oColSqlparram)

            rpstockSUmmary.DataSource = dt
            rpstockSUmmary.DataBind()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:BindTable:" + ex.Message)
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
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
                'BindTable()
                'binddll()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Stock Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    'Public Sub binddll()
    '    Try
    '        If radVenue.SelectedIndex = -1 Then
    '            oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
    '        End If
    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
    '        LogHelper.Error("Till_Summary_Report:binddll:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
    '    Try
    '        'If radVenue.SelectedIndex = 0 Then
    '        '    radLocation.Items.Clear()
    '        '    radMachine.Items.Clear()
    '        'Else
    '        '    oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
    '        '    radMachine.Items.Clear()
    '        'End If

    '    Catch ex As Exception
    '        LogHelper.Error("Till_Summary_Report:radVenue_SelectedIndexChanged:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radLocation.SelectedIndexChanged
    '    Try
    '        If radLocation.SelectedIndex = 0 Then
    '            radMachine.Items.Clear()
    '        Else
    '            oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Till_Summary_Report:radLocation_SelectedIndexChanged:" + ex.Message)
    '    End Try
    'End Sub

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
End Class
