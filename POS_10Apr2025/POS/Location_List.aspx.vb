Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI

Partial Class Location_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsLocation As New Controller_clsLocation

    Public Sub bindGrid()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsLocation.SelectAll()
            If dt.Rows.Count > 0 Then
                rdLocaton.DataSource = dt
                rdLocaton.DataBind()
            Else
                rdLocaton.DataSource = String.Empty
                rdLocaton.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Location_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Public Sub rebindGrid()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsLocation.SelectAll()
            If dt.Rows.Count > 0 Then
                rdLocaton.DataSource = dt
            Else
                rdLocaton.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Location_List:rebindGrid" + ex.Message)
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
                    divLocation.Visible = True
                Else
                    divLocation.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Location_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Location"
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
            LogHelper.Error("Location_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdLocaton_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdLocaton.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdLocaton.MasterTableView.Columns
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
    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
    'Protected Sub rdLocaton_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdLocaton.ItemDataBound

    'End Sub

    'Protected Sub rdLocaton_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdLocaton.ItemCommand

    'End Sub
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub rdLocaton_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If (TypeOf e.Item Is RepeaterItem) Then
                Dim lnkWpos As HtmlAnchor = e.Item.FindControl("lnkWpos")
                Dim lblloc As HiddenField = e.Item.FindControl("hfwLocation_id")
                Dim gtway_StoreName As HiddenField = e.Item.FindControl("gtway_StoreName")
                Dim str As String = ""
                Dim psw As String = Decode_data("not4any1")

                If Session("db_name").ToString() = "POS_PaymentGateway" Then
                    If gtway_StoreName.Value.ToString() = "" Then
                        str = ""
                    Else
                        str = "SignIn.aspx?SName=" + gtway_StoreName.Value.ToString()
                        lnkWpos.Target = "_self"
                    End If
                Else
                    str = WebConfigurationManager.AppSettings("wpos_URL") + "?sv=" + Session("Storename").ToString() + "&cv=" + Session("cmp_id").ToString() + "&lv=" + lblloc.Value.ToString()
                    lnkWpos.Target = "_blank"
                End If

                lnkWpos.HRef = str

                'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                'If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                '    rdLocaton.MasterTableView.GetColumn("TemplateColumn").Visible = False
                'End If
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
            LogHelper.Error("Location_List:rdLocaton_ItemDataBound" + ex.Message)
        End Try
    End Sub
    Protected Sub rdLocaton_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("location_id") = Val(e.CommandArgument)
                Response.Redirect("Location_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                Dim lbloldname As HiddenField = e.Item.FindControl("hdnOldName")

                If lbloldname.Value <> String.Empty Then
                    oclsLocation.name = lbloldname.Value
                Else
                    oclsLocation.name = ""
                End If

                deleteLocation(Val(e.CommandArgument), oclsLocation.name)
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Location_List:rdLocaton_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteLocation(ByVal id As Integer, ByVal oldName As String)
        Try
            oclsLocation.location_id = Val(id)
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.code = ""
            oclsLocation.name = ""
            oclsLocation.address = ""
            oclsLocation.country_id = 0
            oclsLocation.state_id = 0
            oclsLocation.city_id = 0
            oclsLocation.ip_address = ""
            oclsLocation.login_id = 0
            oclsLocation.venue_id = 0
            oclsLocation.machine_id = 0
            oclsLocation.Tran_Type = "D"
            oclsLocation.is_active = 0
            oclsLocation.oldname = oldName
            oclsLocation.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Location_List:deleteLocation" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("location_id") = Nothing
            Response.Redirect("Location_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Location_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdLocaton_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdLocaton.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Location_List:rdLocaton_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub lnkSyncallTill_Click(sender As Object, e As EventArgs)
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.SyncLocationAllTill()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Request added successfully');", True)
        Catch ex As Exception
            LogHelper.Error("Location_List:lnkSyncallTill_Click" + ex.Message)
        End Try
    End Sub
End Class
