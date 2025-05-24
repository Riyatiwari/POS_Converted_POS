Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class IssueVoucher
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsPromotion As New Controller_clsPromotion()

    Public Sub bindGrid()
        Try
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPromotion.Get_Voucher()

            If dt.Rows.Count > 0 Then
                rdIssueVoucher.DataSource = dt
                rdIssueVoucher.DataBind()
            Else
                rdIssueVoucher.DataSource = String.Empty
                rdIssueVoucher.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("IssueVoucher_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divCustomer.Visible = True
                Else
                    divCustomer.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("IssueVoucher_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "IssueVoucher"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("IssueVoucher_List:RoleCheck:" + ex.Message)
        End Try
    End Sub



    'Protected Sub rdIssueVoucher_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdIssueVoucher.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdIssueVoucher.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            End If
    '            If Val(ViewState("edit")) = 1 Then
    '                CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '            End If
    '            If Val(ViewState("delete")) = 1 Then
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("IssueVoucher_List:rdIssueVoucher_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub deleteTax(ByVal id As Integer)
        Try
            'oclsTax.vocherId = Val(id)
            'oclsTax.cmp_id = Val(Session("cmp_id"))
            'oclsTax.Name = ""
            'oclsTax.Mode = ""
            'oclsTax.Value = 0
            'oclsTax.Effective_Date = DateTime.Now
            'oclsTax.Is_active = 1
            'oclsTax.Ip_address = ""
            'oclsTax.Login_id = 0
            'oclsTax.machine_id = 0
            'oclsTax.Tran_Type = "D"
            'oclsTax.Delete()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("IssueVoucher_List:deleteTax:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try

            Response.Redirect("IssueVoucher_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("IssueVoucher_List:lnkNew_Click:" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdIssueVoucher_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdIssueVoucher.NeedDataSource
    '    Try
    '        bindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("IssueVoucher_List:rdIssueVoucher_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub rdIssueVoucher_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdIssueVoucher.ItemCommand

    'End Sub

    Protected Sub rdIssueVoucher_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Delete" Then
                Dim voucherid As Integer = Val(e.CommandArgument.ToString())
                oclsPromotion.Voucher_Code = voucherid
                oclsPromotion.Delete_Voucher()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
