Imports System.Data
Imports System.Web.Configuration
Imports Telerik.Web.UI

Partial Class Transfered_Data
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsLocation As New Controller_clsLocation

    Public Sub bindGrid()
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            oclsLocation.modify_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)
            Dim dt As DataTable = oclsLocation.SelectTRansferd_data()
            If dt.Rows.Count > 0 Then
                rdhistory.DataSource = dt
                rdhistory.DataBind()
            Else
                rdhistory.DataSource = String.Empty
                rdhistory.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Transfered_Data:bindGrid" + ex.Message)
        End Try
    End Sub
    'Public Sub rebindGrid()
    '    Try
    '        oclsLocation.cmp_id = Val(Session("cmp_id"))
    '        Dim dt As DataTable = oclsLocation.SelectAllApi()
    '        If dt.Rows.Count > 0 Then
    '            rdhistory.DataSource = dt
    '        Else
    '            rdhistory.DataSource = String.Empty
    '        End If
    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
    '        LogHelper.Error("Transfered_Data:rebindGrid" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            oclsLocation.created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            oclsLocation.modify_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)
            ' created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                txtForDate.SelectedDate = System.DateTime.Now
                txtTo.SelectedDate = System.DateTime.Now
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Transfered_Data:Page_Load" + ex.Message)
        End Try
    End Sub



    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        bindGrid()
    End Sub
    'Protected Sub rdApi_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    Try
    '        If (TypeOf e.Item Is RepeaterItem) Then
    '            Dim lnkWpos As HtmlAnchor = e.Item.FindControl("lnkWpos")
    '            Dim lblloc As HiddenField = e.Item.FindControl("hfwLocation_id")
    '            Dim gtway_StoreName As HiddenField = e.Item.FindControl("gtway_StoreName")
    '            Dim str As String = ""


    '            If Session("db_name").ToString() = "POS_PaymentGateway" Then
    '                If gtway_StoreName.Value.ToString() = "" Then
    '                    str = ""
    '                Else
    '                    str = "SignIn.aspx?SName=" + gtway_StoreName.Value.ToString()
    '                    lnkWpos.Target = "_self"
    '                End If
    '            Else
    '                str = WebConfigurationManager.AppSettings("wpos_URL") + "?sv=" + Session("store").ToString() + "&cv=" + Session("cmp_id").ToString() + "&lv=" + lblloc.Value.ToString()
    '                lnkWpos.Target = "_blank"
    '            End If

    '            lnkWpos.HRef = str

    '            'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            'If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '            '    rdApi.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            'End If
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
    '        LogHelper.Error("Transfered_Data:rdApi_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub
    'Protected Sub rdApi_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    Try
    '        If e.CommandName = "Edit" Then
    '            Session("cloud_id") = Val(e.CommandArgument)
    '            Response.Redirect("API_Master.aspx", False)
    '        ElseIf e.CommandName = "Delete" Then


    '            deleteLocation(Val(e.CommandArgument), oclsLocation.cloud_id)
    '            oclsLocation.cmp_id = Val(Session("cmp_id"))
    '            oclsLocation.cloud_id = 0
    '            bindGrid()

    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Transfered_Data:rdApi_ItemCommand" + ex.Message)
    '    End Try
    'End Sub



    ''Protected Sub rdApi_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdApi.NeedDataSource
    ''    Try
    ''        rebindGrid()
    ''    Catch ex As Exception
    ''        LogHelper.Error("Location_List:rdApi_NeedDataSource" + ex.Message)
    ''    End Try
    ''End Sub


    'Protected Sub lnkSyncallTill_Click(sender As Object, e As EventArgs)
    '    Try
    '        oclsLocation.cmp_id = Val(Session("cmp_id"))
    '        oclsLocation.SyncLocationAllTill()
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Request added successfully');", True)
    '    Catch ex As Exception
    '        LogHelper.Error("Location_List:lnkSyncallTill_Click" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub deleteLocation(ByVal cloud_id As Integer, ByVal oldName As String)
    '    Try
    '        oclsLocation.cloud_id = Val(cloud_id)
    '        oclsLocation.cmp_id = Val(Session("cmp_id"))
    '        'oclsLocation.cloud_id = ""
    '        'oclsLocation.cloudurl = ""
    '        'oclsLocation.cloudValue = ""

    '        oclsLocation.Tran_Type = "D"

    '        oclsLocation.oldname = oldName
    '        oclsLocation.DeletecloudApi()

    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
    '        LogHelper.Error("Location_List:deleteLocation" + ex.Message)
    '    End Try
    'End Sub
End Class
