Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports System.Xml
Imports Telerik.Reporting.XmlSerialization
Imports System.IO
Imports System.Web.Configuration

Partial Class Duplicate_transaction
    Inherits BaseClass
    Dim oclsRole As Controller_clsRole
    Dim dt As DataTable
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

                txtFrom.SelectedDate = System.DateTime.Now
                txtTo.SelectedDate = System.DateTime.Now
            End If
        Catch ex As Exception
            LogHelper.Error("Duplicate_transaction:Page_Load:" + ex.Message)
        End Try

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Duplicate Transaction Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Duplicate_transaction:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Private Sub BindTable()
        Try

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))
            oColSqlparram.Add("@select", IIf(rdSelect.SelectedIndex > 0, rdSelect.SelectedValue.ToString, "Master"))
            Dim dt As DataTable = oClsDal.GetdataTableSp("Get_duplicate_M_sales", oColSqlparram)


            If rdSelect.SelectedValue.ToString = "Master" Then

                grid_1.Visible = True
                grid_2.Visible = False

                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()
            Else
                grid_2.Visible = True
                grid_1.Visible = False

                rptTranDetail.DataSource = dt
                rptTranDetail.DataBind()

            End If

        Catch ex As Exception
            LogHelper.Error("Duplicate_transaction:BindTable:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Duplicate_transaction:LinkButton1_Click:" + ex.Message)
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
            LogHelper.Error("Duplicate_transaction:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    'Protected Sub lnkview_Click(sender As Object, e As EventArgs)
    '    Try


    '        Dim btn As LinkButton = DirectCast(sender, LinkButton)
    '        Dim arr As String() = btn.CommandArgument.Split("#")
    '        Dim sales As Int32 = Val(arr(0).ToString())
    '        Dim tranuuid As String = arr(1).ToString()
    '        Session("Tran_Report") = arr(2).ToString()
    '        Session("type") = arr(3).ToString()
    '        Dim oClsDal As ClsDataccess = New ClsDataccess()
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@sales_id", Val(sales), SqlDbType.Int)
    '        oColSqlparram.Add("@tran_uuid", tranuuid)
    '        If (Session("type").ToString() = "Cash" Or Session("type").ToString() = "Card") Then
    '            oColSqlparram.Add("@istable", 0, SqlDbType.TinyInt)
    '        ElseIf Session("type").ToString() <> "Table" Then
    '            oColSqlparram.Add("@istable", 1, SqlDbType.TinyInt)
    '        End If



    '        If Session("type").ToString().Contains("Card") = True Or Session("type").ToString().Contains("Cash") = True Then
    '            If (Session("type").ToString() = "Cash" Or Session("type").ToString() = "Card") Then
    '                Session("pay_type") = "0"
    '            Else
    '                Session("pay_type") = "1"
    '            End If

    '            dt = oClsDal.GetdataTableSp("Transaction_report_Detail_View_CashCard", oColSqlparram)

    '        Else
    '            Session("pay_type") = "0"
    '            dt = oClsDal.GetdataTableSp("Transaction_report_Detail_View", oColSqlparram)

    '        End If

    '        If (dt.Rows.Count > 0) Then

    '            Session("dt") = dt

    '            Dim url As String = "Table_Transaction_Detail.aspx"

    '            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("Duplicate_transaction:lnkview_Click:" + ex.Message)
    '    End Try
    'End Sub


End Class
