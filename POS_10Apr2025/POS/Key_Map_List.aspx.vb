Imports System.Data
Imports Telerik.Web.UI

Partial Class Key_Map_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsKeyMap As New Controller_clsKey_Map()

    Public Sub bindGrid()
        Try
            oclsKeyMap.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsKeyMap.SelectAll()

            If dt.Rows.Count > 0 Then
                rdKayMap.DataSource = dt
                rdKayMap.DataBind()
            Else
                rdKayMap.DataSource = String.Empty
                rdKayMap.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Key_Map_List:bindGrid" + ex.Message)
        End Try
       
    End Sub
    Public Sub rebindGrid()
        Try
            oclsKeyMap.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsKeyMap.SelectAll()

            If dt.Rows.Count > 0 Then
                rdKayMap.DataSource = dt
            Else
                rdKayMap.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Key_Map_List:rebindGrid" + ex.Message)
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
            LogHelper.Error("Key_Map_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))

            oclsRole.Form_Name = "Key Map"
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
            LogHelper.Error("Key_Map_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdKayMap_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdKayMap.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdKayMap.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Printer_List:rdPrinter_ItemCreated:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdKayMap_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdKayMap.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdKayMap.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Key_Map_List:rdKayMap_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub deleteKeyMap(ByVal id As Integer)
        Try
            oclsKeyMap.key_map_id = Val(id)
            oclsKeyMap.cmp_id = Val(Session("cmp_id"))
            oclsKeyMap.name = ""
            oclsKeyMap.description = ""
            oclsKeyMap.shorting_no = 0
            oclsKeyMap.ip_address = ""
            oclsKeyMap.login_id = 0
            oclsKeyMap.machine_id = 0
            oclsKeyMap.Tran_Type = "D"
            oclsKeyMap.is_active = 0
            oclsKeyMap.Location_id = 0
            oclsKeyMap.venue_id = 0
            oclsKeyMap.Keymap_size = ""
            oclsKeyMap.Font_Color = ""
            oclsKeyMap.BG_Color = ""
            oclsKeyMap.Display_Name = ""
            oclsKeyMap.ImageOption = ""
            oclsKeyMap.ButtonStyle = ""
            oclsKeyMap.ImageStyle = ""
            oclsKeyMap.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Key_Map_List:deleteKeyMap" + ex.Message)
        End Try
    End Sub
  

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("key_map_id") = Nothing
            Response.Redirect("KeyMap_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Key_Map_List:lnkNew_Click" + ex.Message)
        End Try
        
    End Sub

    'Protected Sub rdKayMap_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdKayMap.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Key_Map_List:rdKayMap_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Private Sub lnkCopy_Click(sender As Object, e As EventArgs) Handles lnkCopy.Click
        Try
            For Each item As RepeaterItem In rdKayMap.Items
                Dim value As String
                'Dim dataitem As GridDataItem = CType(item, GridDataItem)
                'Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim hd_KeyMap As HiddenField = item.FindControl("hd_KeyMap")
                Dim checkBox As CheckBox = item.FindControl("chk_KeyMap")
                If checkBox.Checked Then
                    value = hd_KeyMap.Value
                    Session("key_map_id") = value.ToString()
                End If
            Next
            If Session("key_map_id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                oclsKeyMap.key_map_id = Val(Session("key_map_id"))
                oclsKeyMap.cmp_id = Val(Session("cmp_id"))
                oclsKeyMap.Copy_Function()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record copy successfully');", True)
                bindGrid()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub rdKayMap_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("key_map_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("KeyMap_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteKeyMap(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Key_Map_List:rdKayMap_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkSyncallTill_Click(sender As Object, e As EventArgs)
        Try
            oclsKeyMap.cmp_id = Val(Session("cmp_id"))
            oclsKeyMap.SyncKeymapAllTill()
        Catch ex As Exception
            LogHelper.Error("Key_Map_List:rdKayMap_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class