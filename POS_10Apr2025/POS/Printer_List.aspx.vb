Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Printer_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsPrinter As New Controller_clsPrinter

    Public Sub bindGrid()
        Try
            oclsPrinter.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPrinter.SelectAll()

            If dt.Rows.Count > 0 Then
                rdPrinter.DataSource = dt
                rdPrinter.DataBind()
            Else
                rdPrinter.DataSource = String.Empty
                rdPrinter.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Printer_List:bindGrid:" + ex.Message)
        End Try


    End Sub
    Public Sub rebindGrid()
        Try
            oclsPrinter.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPrinter.SelectAll()
            If dt.Rows.Count > 0 Then
                rdPrinter.DataSource = dt
            Else
                rdPrinter.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Printer_List:rebindGrid:" + ex.Message)
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
                If Val(ViewState("view")) = 1 Or Val(("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
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
            LogHelper.Error("Printer_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Printer"
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
            LogHelper.Error("Printer_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdPrinter_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdPrinter.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdPrinter.MasterTableView.Columns
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
    'Protected Sub rdPrinter_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdPrinter.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdPrinter.MasterTableView.GetColumn("TemplateColumn").Visible = False

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
    '        LogHelper.Error("Printer_List:rdPrinter_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdPrinter_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdPrinter.ItemCommand

    'End Sub

    Protected Sub rdPrinter_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("printer_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Printer_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deletePrinter(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Printer_List:rdPrinter_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub deletePrinter(ByVal id As Integer)
        Try
            oclsPrinter.printer_id = Val(id)
            oclsPrinter.cmp_id = Val(Session("cmp_id"))
            oclsPrinter.name = ""
            oclsPrinter.Is_active = 0
            oclsPrinter.palias = ""
            oclsPrinter.Ip_address = ""
            oclsPrinter.login_id = 0
            oclsPrinter.machine_id = 0
            oclsPrinter.Tran_Type = "D"
            oclsPrinter.device_name = ""
            oclsPrinter.printer_ip_address = ""
            oclsPrinter.budrate = ""
            oclsPrinter.port = 0
            oclsPrinter.vender_id = 0
            oclsPrinter.network_type = ""
            oclsPrinter.is_condiment_small_large = 0
            oclsPrinter.is_product_small_large = 0
            oclsPrinter.group_by = 0
            oclsPrinter.consile_date = 0
            oclsPrinter.group_by_with = 0

            oclsPrinter.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Printer_List:deletePrinter:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("printer_id") = Nothing
            Response.Redirect("Printer_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Printer_List:lnkNew_Click:" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdPrinter_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdPrinter.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Printer_List:rdPrinter_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

End Class
