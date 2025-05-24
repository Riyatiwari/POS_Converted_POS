Imports System.Data
Imports Telerik.Web.UI
Partial Class Product_Price_Master_List
    Inherits System.Web.UI.Page
    ' Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsSizeMaster As New Controller_clsSizeMaster

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                'If Val(Session("staff_role_id")) <> 0 Then
                '    RoleCheck()
                'ElseIf Val(Session("staff_role_id")) = 0 Then
                '    ViewState("view") = 1
                '    ViewState("add") = 1
                '    ViewState("edit") = 1
                '    ViewState("delete") = 1
                'Else
                '    ViewState("view") = 0
                '    ViewState("edit") = 0
                '    ViewState("add") = 0
                '    ViewState("delete") = 0
                'End If
                'If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                '    divUnit.Visible = True
                'Else
                '    divUnit.Visible = False
                'End If
                'If Val(ViewState("add")) = 1 Then
                '    lnkNew.Visible = True
                'Else
                '    lnkNew.Visible = False
                'End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Price_Master_List:Page_Load" + ex.Message)
        End Try
    End Sub


    Public Sub bindGrid()
        Try
            oclsSizeMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsSizeMaster.SelectAll()

            If dt.Rows.Count > 0 Then
                rdPrice.DataSource = dt
                rdPrice.DataBind()
            Else
                rdPrice.DataSource = String.Empty
                rdPrice.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Price_Master_List:bindGrid" + ex.Message)
        End Try

    End Sub


    Protected Sub RoleCheck()
        Try
            'oclsRole = New Controller_clsRole()

            'oclsRole.cmp_id = Val(Session("cmp_id"))
            'oclsRole.Role_Id = Val(Session("staff_role_id"))
            'oclsRole.Form_Name = "Price"
            'oclsRole.Select()

            'If oclsRole.is_View = 1 Then
            '    ViewState("view") = 1
            'Else
            '    ViewState("view") = 0
            'End If

            'If oclsRole.is_add = 1 Then
            '    ViewState("add") = 1
            'Else
            '    ViewState("add") = 0
            'End If
            'If oclsRole.is_Edit = 1 Then
            '    ViewState("edit") = 1
            'Else
            '    ViewState("edit") = 0
            'End If

            'If oclsRole.is_Delete = 1 Then
            '    ViewState("delete") = 1
            'Else
            '    ViewState("delete") = 0
            'End If

        Catch ex As Exception
            LogHelper.Error("Product_Price_Master_List:RoleCheck" + ex.Message)
        End Try
    End Sub


    'Protected Sub rdPrice_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdPrice.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdPrice.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Product_Price_Master_List:rdPrinter_ItemCreated:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdPrice_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdPrice.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            'If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '            '    rdPrice.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            'End If
    '            'If Val(ViewState("edit")) = 1 Then
    '            '    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '            'Else
    '            '    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '            'End If
    '            'If Val(ViewState("delete")) = 1 Then
    '            '    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
    '            'Else
    '            '    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Product_Price_Master_List:rdBarcode_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdPrice_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdPrice.ItemCommand

    'End Sub

    Protected Sub rdPrice_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("Product_Price_Id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Product_Price_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteBarcode(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Price_Master_List:rdBarcode_ItemCommand" + ex.Message)
        End Try
    End Sub

    Protected Sub deleteBarcode(ByVal id As Integer)
        Try
            oclsSizeMaster.Product_Price_Id = Val(id)
            oclsSizeMaster.cmp_id = Val(Session("cmp_id"))
            oclsSizeMaster.PPrice = ""
            oclsSizeMaster.login_id = 0
            oclsSizeMaster.Ip_address = ""
            oclsSizeMaster.Tran_Type = "D"
            oclsSizeMaster.is_active = 0
            oclsSizeMaster.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Price_Master_List:deleteBarcode" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Product_Price_Id") = Nothing
            Response.Redirect("Product_Price_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Price_Master_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdPrice_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdPrice.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Product_Price_Master_List:rdPrice_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Public Sub rebindGrid()
        Try
            oclsSizeMaster.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsSizeMaster.SelectAll()

            If dt.Rows.Count > 0 Then
                rdPrice.DataSource = dt
            Else
                rdPrice.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Price_Master_List:rebindGrid" + ex.Message)
        End Try

    End Sub

End Class
