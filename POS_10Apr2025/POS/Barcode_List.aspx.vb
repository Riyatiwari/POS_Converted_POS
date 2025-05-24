Imports System.Data
Imports Telerik.Web.UI
Partial Class Barcode_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsBarcode As New Controller_clsBarcodeSize
    Public Sub bindGrid()
        Try
            oclsBarcode.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsBarcode.SelectAll()

            If dt.Rows.Count > 0 Then
                rdBarcode.DataSource = dt
                rdBarcode.DataBind()
            Else
                rdBarcode.DataSource = String.Empty
                rdBarcode.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Barcode_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsBarcode.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsBarcode.SelectAll()

            If dt.Rows.Count > 0 Then
                rdBarcode.DataSource = dt
            Else
                rdBarcode.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Barcode_List:rebindGrid" + ex.Message)
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
                    divBarcode.Visible = True
                Else
                    divBarcode.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Barcode_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Barcode"
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
            LogHelper.Error("Barcode_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdBarcode_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdBarcode.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdBarcode.MasterTableView.Columns
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

    'Protected Sub rdBarcode_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdBarcode.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdBarcode.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Barcode_List:rdBarcode_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub deleteBarcode(ByVal id As Integer)
        Try
            oclsBarcode.Barcode_Size_id = Val(id)
            oclsBarcode.cmp_id = Val(Session("cmp_id"))
            oclsBarcode.Barcode = ""
            oclsBarcode.login_id = 0
            oclsBarcode.Ip_address = ""
            oclsBarcode.product_id = 0
            oclsBarcode.Tran_Type = "D"
            oclsBarcode.is_active = 0
            oclsBarcode.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Barcode_List:deleteBarcode" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Barcode_id") = Nothing
            Response.Redirect("Barcode_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Barcode_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdBarcode_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdBarcode.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Barcode_List:rdBarcode_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub rdBarcode_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Barcode_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Barcode_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteBarcode(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Barcode_List:rdBarcode_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class
