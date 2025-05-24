Imports System.Data
Imports Telerik.Web.UI
Partial Class Condiment_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCondiment As New Controller_clsCondiment
    Public Sub bindGrid()
        Try
            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCondiment.SelectAll()

            If dt.Rows.Count > 0 Then
                rdCondiment.DataSource = dt
                rdCondiment.DataBind()
            Else
                rdCondiment.DataSource = String.Empty
                rdCondiment.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Condiment_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCondiment.SelectAll()

            If dt.Rows.Count > 0 Then
                rdCondiment.DataSource = dt
            Else
                rdCondiment.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Condiment_List:rebindGrid" + ex.Message)
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
                    ViewState("edit") = 0
                    ViewState("add") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                    divCondiment.Visible = True
                Else
                    divCondiment.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()

                'If Val(Session("IsPopup")) = 1 Then

                'End If
            End If

        Catch ex As Exception
            LogHelper.Error("Condiment_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Condiment"
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
            LogHelper.Error("Condiment_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdCondiment_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdCondiment.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdCondiment.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Condiment_List:rdCondiment_ItemCreated" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdCondiment_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdCondiment.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdCondiment.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Condiment_List:rdCondiment_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdCondiment_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdCondiment.ItemCommand

    'End Sub

    Protected Sub rdCondiment_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Condiment_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Condiment_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteCondiment(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Condiment_List:rdCondiment_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteCondiment(ByVal id As Integer)
        Try
            oclsCondiment.Condiment_Id = Val(id)
            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            oclsCondiment.Condiment = ""
            oclsCondiment.login_id = 0
            oclsCondiment.Ip_address = ""
            oclsCondiment.product_id = 0
            oclsCondiment.is_active = 0
            oclsCondiment.Tran_Type = "D"
            oclsCondiment.is_add_substract = 0
            oclsCondiment.Quantity = 0
            oclsCondiment.Unit = 0
            oclsCondiment.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Condiment_List:deleteCondiment" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Condiment_id") = Nothing
            Response.Redirect("Condiment_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Condiment_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdCondiment_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdCondiment.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Condiment_List:rdCondiment_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Private Sub lnkMultNew_Click(sender As Object, e As EventArgs) Handles lnkMultNew.Click
        Try
            Session("Condiment_id") = Nothing
            Response.Redirect("Add_Condiment_Master.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

End Class

