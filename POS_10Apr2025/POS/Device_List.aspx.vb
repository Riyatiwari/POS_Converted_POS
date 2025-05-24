Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting
Partial Class Device_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsDevice As New Controller_clsDevice

    Public Sub bindGrid()
        Try
            oclsDevice.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsDevice.SelectAll()

            If dt.Rows.Count > 0 Then
                rdDevice.DataSource = dt
                rdDevice.DataBind()
            Else
                rdDevice.DataSource = String.Empty
                rdDevice.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_List:bindGrid" + ex.Message)
        End Try
        
    End Sub
    Public Sub rebindGrid()
        Try
            oclsDevice.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsDevice.SelectAll()

            If dt.Rows.Count > 0 Then
                rdDevice.DataSource = dt
            Else
                rdDevice.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_List:rebindGrid" + ex.Message)
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
                    divDevice.Visible = True
                Else
                    divDevice.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Device_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Device"
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

            LogHelper.Error("Device_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdDevice_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdDevice.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdDevice.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Device_List:rdDevice_ItemCreated" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdDevice_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdDevice.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdDevice.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Device_List:rdDevice_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdDevice_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdDevice.ItemCommand

    'End Sub

    Protected Sub rdDevice_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("device_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Device_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteDevice(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Device_List:rdDevice_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteDevice(ByVal id As Integer)
        Try
            oclsDevice.device_id = Val(id)
            oclsDevice.cmp_id = Val(Session("cmp_id"))
            oclsDevice.name = ""
            oclsDevice.code = ""
            oclsDevice.serial_no = 0
            oclsDevice.machine_id = 0
            oclsDevice.ip_address = ""
            oclsDevice.login_id = 0
            oclsDevice.Tran_Type = "D"
            oclsDevice.Device_Type_id = 0
            oclsDevice.is_active = 0
            oclsDevice.printer_ip_address = ""
            oclsDevice.network_type = ""
            oclsDevice.port = 0
            oclsDevice.budrate = ""
            oclsDevice.device_name = ""
            oclsDevice.Width = 0
            oclsDevice.User_Name = ""
            oclsDevice.Password = ""
            oclsDevice.Bluetooth_Name = ""
            oclsDevice.Service_Key = ""
            oclsDevice.API = ""
            oclsDevice.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Device_List:deleteDevice" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("device_id") = Nothing
            Response.Redirect("Device_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Device_List:lnkNew_Click" + ex.Message)
        End Try
     
    End Sub

    'Protected Sub rdDevice_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdDevice.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Device_List:rdDevice_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

End Class
