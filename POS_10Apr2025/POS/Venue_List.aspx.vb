Imports System.Data
Imports Telerik.Web.UI

Partial Class Venue_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsVenue As New Controller_clsVenue()

    Public Sub bindGrid()
        Try
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsVenue.SelectAll()

            If dt.Rows.Count > 0 Then
                rdVenue.DataSource = dt
                rdVenue.DataBind()
            Else
                rdVenue.DataSource = String.Empty
                rdVenue.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Venue_List:bindGrid:" + ex.Message)
        End Try
       
    End Sub
    Public Sub rebindGrid()
        Try
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsVenue.SelectAll()

            If dt.Rows.Count > 0 Then
                rdVenue.DataSource = dt
            Else
                rdVenue.DataSource = String.Empty

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Venue_List:rebindGrid:" + ex.Message)
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
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(("edit")) = 1 Then
                    divKeyMap.Visible = True
                Else
                    divKeyMap.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Venue_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Venue"
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
            LogHelper.Error("Venue_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdVenue_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdVenue.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdVenue.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filterItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filterItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Venue_List:rdVenue_ItemCreated:" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdVenue_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdVenue.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdVenue.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Venue_List:rdVenue_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdVenue_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdVenue.ItemCommand

    'End Sub

    Protected Sub deleteVenue(ByVal id As Integer)
        Try
            oclsVenue.venue_id = Val(id)
            oclsVenue.cmp_id = Val(Session("cmp_id"))
            oclsVenue.venue_name = ""
            oclsVenue.description = ""
            oclsVenue.sorting_no = 0
            oclsVenue.mac_id = ""
            oclsVenue.print_receipt = 0
            oclsVenue.print_duplicate_receipt = 0
            oclsVenue.machine_id = 0
            oclsVenue.Tran_Type = "D"
            oclsVenue.is_active = 0
            oclsVenue.login_id = 0
            oclsVenue.date_time = ""
            oclsVenue.group_by = 0
            oclsVenue.consile_date = 0
            oclsVenue.group_by_with = 0

            oclsVenue.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Venue_List:deleteVenue:" + ex.Message)
        End Try
    End Sub
    

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("venue_id") = Nothing
            Response.Redirect("Venue_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Venue_List:lnkNew_Click:" + ex.Message)
        End Try
      
    End Sub

    'Protected Sub rdVenue_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdVenue.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Venue_List:rdVenue_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub rdVenue_ItemCommand1(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("venue_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Venue_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteVenue(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Venue_List:rdVenue_ItemCommand:" + ex.Message)
        End Try
    End Sub
End Class