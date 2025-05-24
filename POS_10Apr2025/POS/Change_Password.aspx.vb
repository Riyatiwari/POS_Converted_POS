Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Change_Password
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsCategory As New Controller_clsCategory()
    Dim login As New Controller_clsLogin
    Dim oclsRole As Controller_clsRole

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                'If Session("staff_role_id") = 0 Then
                '    divCheck.Visible = True
                '    oclsBind.BindStaffBindStaffLoginId(radEmployee, Val(Session("cmp_id")))
                'Else
                '    divCheck.Visible = False
                'End If
                'divEmployee.Visible = False
                'rfEmployee.Enabled = False
              
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                    divCheck.Visible = True
                    oclsBind.BindStaffBindStaffLoginId(radEmployee, Val(Session("cmp_id")), Val(Session("staff_id_login")))
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                    divCheck.Visible = True
                    oclsBind.BindStaffBindStaffLoginId(radEmployee, Val(Session("cmp_id")), Val(Session("staff_id_login")))
                Else
                    ViewState("view") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Then
                    Div_Button.Visible = False
                Else
                    Div_Button.Visible = True
                End If
                If Val(ViewState("edit")) = 1 Then
                    Div_Button.Visible = True
                Else
                    Div_Button.Visible = False
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divchangepassword.Visible = True
                Else
                    divchangepassword.Visible = False
                End If
                divEmployee.Visible = False
                rfEmployee.Enabled = False
            End If
        Catch ex As Exception
            LogHelper.Error("Change_password:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Change Password"
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

            LogHelper.Error("Email_Settings_Master:RoleCheck" + ex.Message)
        End Try
    End Sub
    Protected Sub rbtnPass_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnPass.CheckedChanged
        Try
            If rbtnPass.Checked = True Then
                divEmployee.Visible = False
                divOldPass.Visible = True
                rfEmployee.Enabled = False
                rfOldPass.Enabled = True
            Else
                divEmployee.Visible = True
                divOldPass.Visible = False
                rfEmployee.Enabled = True
                rfOldPass.Enabled = False
            End If
        Catch ex As Exception
            LogHelper.Error("Change_password:rbtnPass_CheckedChanged" + ex.Message)
        End Try
      
    End Sub

    Protected Sub rbtnSPass_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSPass.CheckedChanged
        Try
            If rbtnSPass.Checked = True Then
                divEmployee.Visible = True
                divOldPass.Visible = False
                rfEmployee.Enabled = True
                rfOldPass.Enabled = False
            Else
                divEmployee.Visible = False
                divOldPass.Visible = True
                rfEmployee.Enabled = False
                rfOldPass.Enabled = True
            End If
        Catch ex As Exception
            LogHelper.Error("Change_password:rbtnSPass_CheckedChanged" + ex.Message)
        End Try
     
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click

        Try
            'If Session("staff_id_login") = 0 Then
            If rbtnPass.Checked = True Then
                Try
                    login.Login_id = Val(Session("Login_id"))
                    login.cmp_id = Val(Session("cmp_id"))
                    login.Password = Encrypt(txtOldPassword.Text)
                    login.NewPassword = Encrypt(txtNewPassword.Text)
                    login.Change()
                    ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Test", "alert('Password updated successfully');", True)
                    'Response.Redirect("SignIn.aspx", False)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Password updated successfully');window.location='SignIn.aspx';", True)
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
                End Try

            ElseIf rbtnSPass.Checked = True Then
                Try
                    If radEmployee.SelectedValue = "SELECT" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Test", "alert('Please select employee');", True)
                    Else
                        login.Login_id = radEmployee.SelectedValue
                        login.cmp_id = Val(Session("cmp_id"))
                        login.NewPassword = Encrypt(txtNewPassword.Text)
                        login.Change_U()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Test", "alert('Password updated successfully');", True)
                    End If

                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
                End Try
                radEmployee.ClearSelection()
                'End If
                'Else
                '    Try
                '        login.Login_id = Val(Session("Login_id"))
                '        login.cmp_id = Val(Session("cmp_id"))
                '        login.Password = Encrypt(txtOldPassword.Text)
                '        login.NewPassword = Encrypt(txtNewPassword.Text)
                '        login.Change()
                '        ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Test", "alert('Password updated successfully');", True)
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Password updated successfully');window.location='SignIn.aspx';", True)
                '    Catch ex As Exception
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
                '    End Try
            End If

        Catch ex As Exception
            LogHelper.Error("Change_password:btnsave_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("staff_role_id") = 0 Then
                divCheck.Visible = True
                'rbtnPass.Checked = True
                oclsBind.BindStaffBindStaffLoginId(radEmployee, Val(Session("cmp_id")), Val(Session("staff_id_login")))

                If rbtnSPass.Checked Then
                    divEmployee.Visible = True
                    divOldPass.Visible = False
                    radEmployee.SelectedIndex = 0

                End If
                If rbtnPass.Checked Then
                    divEmployee.Visible = False
                    rfEmployee.Enabled = False
                    txtConfirm.Text = Nothing
                    txtNewPassword.Text = Nothing
                    txtOldPassword.Text = Nothing

                End If
            Else
                divCheck.Visible = False
                divEmployee.Visible = False
                rfEmployee.Enabled = False
                txtConfirm.Text = Nothing
                txtNewPassword.Text = Nothing
                txtOldPassword.Text = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Change_password:btnReset_Click" + ex.Message)
        End Try
       
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Dashboard.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Change_password:btnCancel_Click" + ex.Message)
        End Try
    End Sub

End Class
