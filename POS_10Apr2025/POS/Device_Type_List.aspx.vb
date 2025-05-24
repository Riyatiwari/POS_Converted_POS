Imports System.Data
Imports Telerik.Web.UI
Partial Class Device_Type_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsDeviceType As New Controller_clsDevice_Type
    Public Sub bindGrid()
        Try
            oclsDeviceType.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsDeviceType.SelectAll()
            If dt.Rows.Count > 0 Then
                rdDeviceType.DataSource = dt
                rdDeviceType.DataBind()
            Else
                rdDeviceType.DataSource = String.Empty
                rdDeviceType.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_Type_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsDeviceType.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsDeviceType.SelectAll()
            If dt.Rows.Count > 0 Then
                rdDeviceType.DataSource = dt
            Else
                rdDeviceType.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_Type_List:rebindGrid" + ex.Message)
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
                    divDeviceType.Visible = True
                Else
                    divDeviceType.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Device_Type_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Device Type"
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

            LogHelper.Error("Device_Type_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdDeviceType_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdDeviceType.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdDeviceType.MasterTableView.Columns
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

    'Protected Sub rdDeviceType_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdDeviceType.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdDeviceType.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Device_Type_List:rdDeviceType_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdDeviceType_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdDeviceType.ItemCommand

    'End Sub
    Protected Sub rdDeviceType_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Device_Type_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Device_Type_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteDeviceType(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Device_Type_List:rdDeviceType_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteDeviceType(ByVal id As Integer)
        Try
            oclsDeviceType.Device_Type_id = Val(id)
            oclsDeviceType.Device_Type = ""
            oclsDeviceType.cmp_id = Val(Session("cmp_id"))
            oclsDeviceType.login_id = 0
            oclsDeviceType.Ip_address = ""
            oclsDeviceType.is_active = 0
            oclsDeviceType.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Device_Type_List:deleteFunctionType" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Device_Type_id") = Nothing
            Response.Redirect("Device_Type_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Device_Type_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdDeviceType_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdDeviceType.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Device_Type_List:rdDeviceType_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

End Class
