Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class Machine_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsMachine As New Controller_clsMachine

    Public Sub bindGrid()
        Try
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsMachine.SelectAll()

            If dt.Rows.Count > 0 Then
                rdMachine.DataSource = dt
                rdMachine.DataBind()
            Else
                rdMachine.DataSource = String.Empty
                rdMachine.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Machine_List:bindGrid:" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsMachine.SelectAll()

            If dt.Rows.Count > 0 Then
                rdMachine.DataSource = dt
            Else
                rdMachine.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Machine_List:rebindGrid:" + ex.Message)
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
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divMachine.Visible = True
                Else
                    divMachine.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Machine_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Till"
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
            LogHelper.Error("Machine_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdMachine_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdMachine.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdMachine.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Machine_List:rdMachine_ItemCreated:" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdMachine_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdMachine.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdMachine.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
    '        LogHelper.Error("Machine_List:rdMachine_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdMachine_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rdMachine.ItemCommand

    'End Sub

    Protected Sub rdMachine_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Dim hfVenueId As HiddenField = DirectCast(e.Item.FindControl("hfvenue_id"), HiddenField)
                Dim venueId As Integer = Convert.ToInt32(hfVenueId.Value)

                Dim machineTilluuid As HiddenField = DirectCast(e.Item.FindControl("hfTilluuid"), HiddenField)
                Dim currentTilluuid As String = machineTilluuid.Value.ToString
                Session("currentTillUUID") = currentTilluuid
                Session("venue_id") = venueId
                Session("machine_id") = Convert.ToInt32(e.CommandArgument)
                'Session("venue_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Machine_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteMachine(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Machine_List:rdMachine_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteMachine(ByVal id As Integer)
        Try
            oclsMachine.machine_id = Val(id)
            oclsMachine.cmp_id = Val(Session("cmp_id"))
            oclsMachine.code = ""
            oclsMachine.name = ""
            oclsMachine.mac_address = ""
            oclsMachine.serial_no = ""
            oclsMachine.model = ""
            oclsMachine.ip_address = ""
            oclsMachine.login_id = 0
            oclsMachine.Tran_Type = "D"
            oclsMachine.location_id = 0
            oclsMachine.is_assign = 0
            oclsMachine.is_minipos = 0
            oclsMachine.is_active = 0
            oclsMachine.Receipt_Header = ""
            oclsMachine.Receipt_Footer = ""
            oclsMachine.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Machine_List:deleteMachine:" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("machine_id") = Nothing
            Response.Redirect("Machine_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Machine_List:lnkNew_Click:" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdMachine_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdMachine.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Machine_List:rdMachine_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub sync_Click(sender As Object, e As EventArgs)
        Dim modelNames As New List(Of String)()
        oclsMachine.cmp_id = Val(Session("cmp_id"))
        Dim dt As DataTable = oclsMachine.SelectAll()


        Dim strcone As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("poscontrollerconnectionstring").ConnectionString)
            strcone.Open()


        For Each row As DataRow In dt.Rows
            Dim modelName As String = row("model").ToString()

            Dim cmdt As SqlCommand = New SqlCommand("P_M_Sync_AllModels", strcone)
            cmdt.CommandType = CommandType.StoredProcedure

            cmdt.Parameters.AddWithValue("@Storeuuid", Session("conStoreuuid"))
            cmdt.Parameters.AddWithValue("@Model_Name", modelName)
            cmdt.ExecuteNonQuery()
        Next
     
        strcone.Close()




        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Model Syncronize Successfully');", True)


    End Sub
End Class