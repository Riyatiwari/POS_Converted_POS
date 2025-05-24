Imports System.Data
Imports Telerik.Web.UI
Partial Class Table_Map_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsTableMap As New Controller_clsTable_Map()

    Public Sub bindGrid()
        Try
            oclsTableMap.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsTableMap.SelectAll()

            If dt.Rows.Count > 0 Then
                rdKayMap.DataSource = dt
                rdKayMap.DataBind()
            Else
                rdKayMap.DataSource = String.Empty
                rdKayMap.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Table_Map_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsTableMap.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsTableMap.SelectAll()

            If dt.Rows.Count > 0 Then
                rdKayMap.DataSource = dt
            Else
                rdKayMap.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Table_Map_List:rebindGrid" + ex.Message)
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
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divKeyMap.Visible = True
                Else
                    divKeyMap.Visible = False
                End If
                If ViewState("add") = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Table_Map_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))

            oclsRole.Form_Name = "Table Map"
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
            LogHelper.Error("Table_Map_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    Protected Sub rdKayMap_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdKayMap.ItemCreated
        Try
            If TypeOf e.Item Is GridFilteringItem Then
                For Each nColumn As GridColumn In rdKayMap.MasterTableView.Columns
                    If TypeOf nColumn Is GridBoundColumn Then
                        Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
                        Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
                        Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
                        textItem.TabIndex = 1
                    End If
                Next
            End If
        Catch ex As Exception
            LogHelper.Error("Printer_List:rdPrinter_ItemCreated:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdKayMap_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdKayMap.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                    rdKayMap.MasterTableView.GetColumn("TemplateColumn").Visible = False
                End If
                If Val(ViewState("edit")) = 1 Then
                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
                End If
                If Val(ViewState("delete")) = 1 Then
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Table_Map_List:rdKayMap_ItemDataBound" + ex.Message)
        End Try
    End Sub

    Protected Sub rdKayMap_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdKayMap.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("Table_map_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("TableMap_Master_.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteKeyMap(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Table_Map_List:rdKayMap_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteKeyMap(ByVal id As Integer)
        Try
            oclsTableMap.Table_map_id = Val(id)
            oclsTableMap.cmp_id = Val(Session("cmp_id"))

            oclsTableMap.ip_address = ""
            oclsTableMap.login_id = 0

            oclsTableMap.Tran_Type = "D"
            oclsTableMap.is_active = 0

            oclsTableMap.Tablemap_size = ""
            oclsTableMap.Font_Color = ""
            oclsTableMap.BG_Color = ""
            oclsTableMap.Display_Name = ""
            oclsTableMap.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Table_Map_List:deleteKeyMap" + ex.Message)
        End Try
    End Sub


    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Table_map_id") = Nothing
            Response.Redirect("TableMap_Master_.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Table_Map_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub rdKayMap_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdKayMap.NeedDataSource
        Try
            rebindGrid()
        Catch ex As Exception
            LogHelper.Error("Table_Map_List:rdKayMap_NeedDataSource" + ex.Message)
        End Try
    End Sub


End Class
