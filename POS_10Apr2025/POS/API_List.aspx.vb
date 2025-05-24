Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI

Partial Class API_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsLocation As New Controller_clsLocation

    Public Sub bindGrid()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsLocation.SelectAllApi()
            If dt.Rows.Count > 0 Then
                rdApi.DataSource = dt
                rdApi.DataBind()
            Else
                rdApi.DataSource = String.Empty
                rdApi.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Location_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Public Sub rebindGrid()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsLocation.SelectAllApi()
            If dt.Rows.Count > 0 Then
                rdApi.DataSource = dt
            Else
                rdApi.DataSource = String.Empty
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
                '  bindGrid()
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Location_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub rdApi_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If (TypeOf e.Item Is RepeaterItem) Then
                Dim lnkWpos As HtmlAnchor = e.Item.FindControl("lnkWpos")
                Dim lblloc As HiddenField = e.Item.FindControl("hfwLocation_id")
                Dim gtway_StoreName As HiddenField = e.Item.FindControl("gtway_StoreName")
                Dim str As String = ""


                If Session("db_name").ToString() = "POS_PaymentGateway" Then
                    If gtway_StoreName.Value.ToString() = "" Then
                        str = ""
                    Else
                        str = "SignIn.aspx?SName=" + gtway_StoreName.Value.ToString()
                        lnkWpos.Target = "_self"
                    End If
                Else
                    str = WebConfigurationManager.AppSettings("wpos_URL") + "?sv=" + Session("store").ToString() + "&cv=" + Session("cmp_id").ToString() + "&lv=" + lblloc.Value.ToString()
                    lnkWpos.Target = "_blank"
                End If

                lnkWpos.HRef = str

                'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                'If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                '    rdApi.MasterTableView.GetColumn("TemplateColumn").Visible = False
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
            LogHelper.Error("Location_List:rdApi_ItemDataBound" + ex.Message)
        End Try
    End Sub
    Protected Sub rdApi_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("cloud_id") = Val(e.CommandArgument)
                Response.Redirect("API_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then


                deleteLocation(Val(e.CommandArgument), oclsLocation.cloud_id)
                oclsLocation.cmp_id = Val(Session("cmp_id"))
                oclsLocation.cloud_id = 0
                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("API_List:rdApi_ItemCommand" + ex.Message)
        End Try
    End Sub
    'Protected Sub deleteLocation(ByVal id As Integer, ByVal oldName As String)
    '    Try
    '        oclsLocation.location_id = Val(id)
    '        oclsLocation.cmp_id = Val(Session("cmp_id"))
    '        oclsLocation.code = ""
    '        oclsLocation.name = ""
    '        oclsLocation.address = ""
    '        oclsLocation.country_id = 0
    '        oclsLocation.state_id = 0
    '        oclsLocation.city_id = 0
    '        oclsLocation.ip_address = ""
    '        oclsLocation.login_id = 0
    '        oclsLocation.venue_id = 0
    '        oclsLocation.machine_id = 0
    '        oclsLocation.Tran_Type = "D"
    '        oclsLocation.is_active = 0
    '        oclsLocation.oldname = oldName
    '        oclsLocation.Delete()
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
    '        LogHelper.Error("Location_List:deleteLocation" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Cloud_id") = Nothing
            Response.Redirect("API_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("API_Master:lnkNew_Click" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdApi_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdApi.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Location_List:rdApi_NeedDataSource" + ex.Message)
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

    Protected Sub deleteLocation(ByVal cloud_id As Integer, ByVal oldName As String)
        Try
            oclsLocation.cloud_id = Val(cloud_id)
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            'oclsLocation.cloud_id = ""
            'oclsLocation.cloudurl = ""
            'oclsLocation.cloudValue = ""

            oclsLocation.Tran_Type = "D"

            oclsLocation.oldname = oldName
            oclsLocation.DeletecloudApi()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Location_List:deleteLocation" + ex.Message)
        End Try
    End Sub
End Class
