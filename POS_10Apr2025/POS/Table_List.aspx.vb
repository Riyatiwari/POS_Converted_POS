Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting
Partial Class Table_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsTable As New Controller_clsTable()

    Public Sub bindGrid()
        Try
            oclsTable.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsTable.SelectAll()

            If dt.Rows.Count > 0 Then
                rdTable.DataSource = dt
                rdTable.DataBind()
            Else
                rdTable.DataSource = String.Empty
                rdTable.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Table_List:bindGrid:" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsTable.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsTable.SelectAll()

            If dt.Rows.Count > 0 Then
                rdTable.DataSource = dt
            Else
                rdTable.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Table_List:rebindGrid:" + ex.Message)
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
                    ViewState("edit") = 1
                    ViewState("add") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("edit") = 0
                    ViewState("add") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divTable.Visible = True
                Else
                    divTable.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()

            End If
            'bindGrid()
        Catch ex As Exception
            LogHelper.Error("Table_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Table"
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
            LogHelper.Error("Table_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdTable_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdTable.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdTable.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Table_List:rdTable_ItemCreated:" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdTable_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdTable.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdTable.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Table_List:rdTable_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdTable_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdTable.ItemCommand

    'End Sub


    Protected Sub rdTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("table_id") = Val(e.CommandArgument)
                Response.Redirect("Table_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteTable(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Table_List:rdTable_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteTable(ByVal id As Integer)
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oclsTable.table_id = Val(id)
            oclsTable.cmp_id = Val(Session("cmp_id"))
            oclsTable.name = ""
            oclsTable.min_cover = 0
            oclsTable.max_cover = 0
            oclsTable.shorting_no = 0
            oclsTable.is_active = 1
            oclsTable.is_open = 1
            oclsTable.ip_address = ""
            oclsTable.login_id = 0
            oclsTable.machine_id = 0
            oclsTable.Tran_Type = "D"
            oclsTable.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Table_List:deleteTable:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("table_id") = Nothing
            Response.Redirect("Table_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Table_List:lnkNew_Click:" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdTable_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdTable.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Table_List:rdTable_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

End Class
