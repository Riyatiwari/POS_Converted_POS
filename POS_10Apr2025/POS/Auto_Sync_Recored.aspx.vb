Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Auto_Sync_Recored
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsBind As New clsBinding
    Dim oclsMachine As Controller_clsMachine = New Controller_clsMachine()
    Dim oclsAutoSyncRecored As Controller_clsAutoSyncRecored = New Controller_clsAutoSyncRecored()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If

                oclsBind.BindVenue(ddlVenue, Val(Session("cmp_id")))
                bind_Pages()
            End If
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub bind_Pages()
        Try
            oclsAutoSyncRecored.cmp_id = Val(Session("cmp_id").ToString())
            Dim dt As DataTable = oclsAutoSyncRecored.SelectForm()

            If dt.Rows.Count > 0 Then
                DL_Pages.DataSource = dt
                DL_Pages.DataBind()
            Else
                DL_Pages.DataSource = String.Empty
                DL_Pages.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:bind_Pages:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim s_cnt As Integer = 0
            For Each item As DataListItem In DL_Pages.Items

                If CType(item.FindControl("chkPages"), CheckBox).Checked Then
                    s_cnt = 1
                End If
            Next

            If s_cnt = 1 Then
                If ddlTill.Items.Count > 0 Then
                    Dim cnt As Integer = 0

                    For Each item As DataListItem In ddlTill.Items
                        If CType(item.FindControl("chk_till"), CheckBox).Checked Then
                            insert_detail(Convert.ToInt32(CType(item.FindControl("hd_machineID"), HiddenField).Value.ToString()))
                            cnt = 1
                        End If
                    Next

                    If cnt = 0 Then
                        insert_detail(0)
                    End If
                Else
                    insert_detail(0)
                End If

                Response.Redirect("Auto_Sync_Recored.aspx", False)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select master page.');", True)
            End If


        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Private Sub insert_detail(TillID As Double)
        Try

            Dim cnt As Integer = 0
            Dim page_list As String = ""
            Dim page_name As String = ""

            oclsAutoSyncRecored.cmp_id = Val(Session("cmp_id"))
            oclsAutoSyncRecored.Venue_Id = Val(ddlVenue.SelectedValue)
            If Val(ddlVenue.SelectedValue) > 0 Then
                oclsAutoSyncRecored.Location_Id = Val(ddllocation.SelectedValue)
            Else
                oclsAutoSyncRecored.Location_Id = Val(0)
            End If
            oclsAutoSyncRecored.Till_Id = Val(TillID)
            oclsAutoSyncRecored.SyncBtn_No = Val(0)
            oclsAutoSyncRecored.SyncFlag = Val(1)
            oclsAutoSyncRecored.login_id = Val(0)

            For Each item As DataListItem In DL_Pages.Items

                If CType(item.FindControl("chkPages"), CheckBox).Checked Then
                    page_list += CType(item.FindControl("hdPage_ID"), HiddenField).Value.ToString() + "#"
                    page_name += CType(item.FindControl("hd_PageName"), HiddenField).Value.ToString() + "#"

                End If
            Next

            If page_list <> "" Then
                oclsAutoSyncRecored.Page_list = page_list.Remove(page_list.Length - 1, 1)
                oclsAutoSyncRecored.page_name = page_name.Remove(page_name.Length - 1, 1)
            Else
                oclsAutoSyncRecored.Page_list = page_list
                oclsAutoSyncRecored.page_name = page_name
            End If

            oclsAutoSyncRecored.Insert()
            Session("Success") = "Record inserted successfully"

        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:insert_detail" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Try
            Session("Success") = Nothing
            Response.Redirect("Auto_Sync_Recored.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Session("Success") = Nothing
            Response.Redirect("Auto_Sync_Recored.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlVenue_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If ddlVenue.SelectedIndex = 0 Then
                ddllocation.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(ddllocation, Val(Session("cmp_id")), Val(ddlVenue.SelectedValue))
            End If

        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:ddlVenue_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub
    Protected Sub ddllocation_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddllocation.SelectedIndex = 0 Then
            Else

                oclsMachine.cmp_id = Val(Session("cmp_id"))
                oclsMachine.location_id = Val(ddllocation.SelectedValue)
                Dim dt As DataTable = oclsMachine.SelectMachineByLocation()
                If dt.Rows.Count > 0 Then
                    ddlTill.DataSource = dt
                    ddlTill.DataBind()
                Else
                    ddlTill.DataSource = String.Empty
                    ddlTill.DataBind()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:ddllocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim skipPages As New List(Of String) From {"ResDiary", "Group Category Image", "Product Image", "Get Logs", "PayCommunicator Logs"}




            For Each row As DataListItem In DL_Pages.Items
                Dim chkPages As CheckBox = CType(row.FindControl("chkPages"), CheckBox)
                Dim hdPageName As HiddenField = CType(row.FindControl("hd_PageName"), HiddenField)
                Dim pageName As String = hdPageName.Value.Trim()

                If chkAll.Checked Then
                    ' Agar page name skip list me nahi hai, toh checkbox ko check karo.
                    If Not skipPages.Contains(pageName) Then
                        chkPages.Checked = True
                    Else
                        chkPages.Checked = False
                    End If
                Else
                    chkPages.Checked = False
                End If
            Next



            'If chkAll.Checked = True Then
            '    For Each row As DataListItem In DL_Pages.Items
            '        Dim chkPages As CheckBox = CType(row.FindControl("chkPages"), CheckBox)

            '        chkPages.Checked = True
            '    Next
            'Else
            '    For Each row As DataListItem In DL_Pages.Items
            '        Dim chkPages As CheckBox = CType(row.FindControl("chkPages"), CheckBox)

            '        chkPages.Checked = False
            '    Next
            'End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Auto_Sync_Recored:chkAll_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkSyncHistory_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("Auto_Sync_History.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Auto_Sync_Recored:chkAll_CheckedChanged:" + ex.Message)
        End Try
    End Sub
End Class
