Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class staff_privilege
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsStaff As New Controller_clsStaff()
    Dim oclsfunction As New Controller_clsFunctionType()
    Dim oclsVenue As Controller_clsVenue = New Controller_clsVenue()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                txtRname.Focus()
                BindFormGrid()
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("m_staff_id") = Nothing Then
                    EditRole()
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("staff_privilege:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub EditRole()
        Try
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.m_staff_id = Val(Session("m_staff_id"))
            Dim dt As DataTable = oclsStaff.Select_m_staff()

            If dt.Rows.Count > 0 Then

                txtRname.Text = dt.Rows(0)("name").ToString()
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("Role_Master:EditRole:" + ex.Message)
        End Try
    End Sub

    Public Sub BindFormGrid()
        Try
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsStaff.Select_rules()
            If dt.Rows.Count > 0 Then
                DL_rules.DataSource = dt
                DL_rules.DataBind()
            Else
                DL_rules.DataSource = String.Empty
                DL_rules.DataBind()
            End If

            For Each row As DataListItem In DL_rules.Items

                Dim hdfunction_type_id As HiddenField = CType(row.FindControl("hdfunction_type_id"), HiddenField)
                Dim chkRules As CheckBox = CType(row.FindControl("chkRules"), CheckBox)
                Dim lbl_name As Label = CType(row.FindControl("lbl_name"), Label)
                Dim hd_name As HiddenField = CType(row.FindControl("hd_name"), HiddenField)

                oclsStaff.cmp_id = Val(Session("cmp_id"))
                oclsStaff.m_staff_id = Val(Session("m_staff_id"))
                Dim fdt As DataTable = oclsStaff.GetFunction_type()
                If fdt.Rows.Count > 0 Then
                    For Each row1 As DataRow In fdt.Rows

                        If Val(hdfunction_type_id.Value) = Val(row1("function_type_id")) And hd_name.Value.ToString() = row1("function_name").ToString() Then
                            chkRules.Checked = True
                        End If

                    Next
                End If
            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege:BindFormGrid:" + ex.Message)
        End Try
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.name = txtRname.Text.Trim()
            oclsStaff.is_active = IIf(chkActive.Checked = True, 1, 0)

            If Session("m_staff_id") = Nothing Then
                oclsStaff.m_staff_id = Val(0)
                Dim dt As DataTable = oclsStaff.InsertStaffRulesMaster()

                If dt.Rows.Count > 0 Then
                    Insert_Function_type_detail(Val(dt.Rows(0)("id")))
                End If

                Session("Success") = "Record inserted successfully"
            Else
                oclsStaff.m_staff_id = Val(Session("m_staff_id"))
                Dim dt As DataTable = oclsStaff.UpdateStaffRulesMaster()

                oclsStaff.Function_type_id = 0
                oclsStaff.name = ""
                oclsStaff.delete_staff_function()

                Insert_Function_type_detail(Val(Session("m_staff_id")))

                Session("Success") = "Record updated successfully"
            End If

            Response.Redirect("staff_privilege_list.aspx", False)
            Session("m_staff_id") = Nothing
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub Insert_Function_type_detail(id As Double)
        Try
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.m_staff_id = Val(id)

            For Each item As DataListItem In DL_rules.Items

                If CType(item.FindControl("chkRules"), CheckBox).Checked Then
                    oclsStaff.Function_type_id = Convert.ToInt32(CType(item.FindControl("hdfunction_type_id"), HiddenField).Value.ToString())
                    oclsStaff.name = CType(item.FindControl("hd_name"), HiddenField).Value.ToString()

                    oclsStaff.Insert_Function_type()
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege:Insert_Function_type_detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("staff_privilege_list.aspx", False)
            Session("m_staff_id") = Nothing
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege:btnCancel_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkAll.Checked = True Then
                For Each row As DataListItem In DL_rules.Items
                    Dim chkRules As CheckBox = CType(row.FindControl("chkRules"), CheckBox)

                    chkRules.Checked = True
                Next
            Else
                For Each row As DataListItem In DL_rules.Items
                    Dim chkRules As CheckBox = CType(row.FindControl("chkRules"), CheckBox)

                    chkRules.Checked = False
                Next
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("staff_privilege:chkAll_CheckedChanged:" + ex.Message)
        End Try
    End Sub
End Class
