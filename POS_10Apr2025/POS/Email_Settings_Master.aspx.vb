Imports System.Data
Imports System.Security.Cryptography

Partial Class Email_Settings_Master
    Inherits BaseClass
    Dim oclsRole As Controller_clsRole
    Dim oclsBind As New clsBinding
    Dim oclsEmailSetting As New Controller_clsEmailsettings()
    Dim oClsDataccess As New ClsDataccess()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("staff_role_id") <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
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
                    divCompany.Visible = True
                    BindSettings()
                Else
                    divCompany.Visible = False
                End If
                oclsBind.BindLocation(ddLocation, Val(Session("cmp_id")))
            End If

        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Email Settings"
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

    Private Sub Clear()
        Try
            txtserver.Text = Nothing
            txtusername1.Text = Nothing
            txtpassword1.Text = Nothing
            txtport.Text = Nothing
            txtfromemail.Text = Nothing
            radssl.SelectedValue = 0
            txtemailalias.Text = Nothing
            radmes.SelectedValue = 0
            txtmesuri.Text = Nothing
            txtreplyto.Text = Nothing
            ddLocation.ClearSelection()
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:Clear" + ex.Message)
        End Try
      
    End Sub

    Private Sub BindSettings()
        Try
            oclsEmailSetting.Company_id = Val(Session("cmp_id"))
            Dim dtCompany As DataTable = oclsEmailSetting.SelectByCompany()
            If dtCompany.Rows.Count > 0 Then
                oclsEmailSetting.Tran_id = Val(dtCompany.Rows(0)("Tran_id"))
                If oclsEmailSetting.Select() Then
                    hfEmailId.Value = Val(dtCompany.Rows(0)("Tran_id"))
                    txtusername1.Text = oclsEmailSetting.MailServer_UserName
                    txtpassword1.Text = oclsEmailSetting.MailServer_Password
                    txtport.Text = oclsEmailSetting.Port
                    txtfromemail.Text = oclsEmailSetting.From_Email
                    txtreplyto.Text = oclsEmailSetting.Reply_to
                    txtemailalias.Text = oclsEmailSetting.Alias
                    txtmesuri.Text = oclsEmailSetting.MES_URI
                    radssl.FindItemByValue(oclsEmailSetting.ssl).Selected = True
                    radmes.FindItemByValue(oclsEmailSetting.is_MES).Selected = True
                   
                    txtserver.Text = oclsEmailSetting.MailServer
                    If oclsEmailSetting.S_Type = 1 Then
                        rbtnemail.Checked = False
                        rbtnsms.Checked = True
                        divOtherInfo.Visible = False
                    End If
                    If oclsEmailSetting.S_Type = 0 Then
                        rbtnemail.Checked = True
                        rbtnsms.Checked = False
                        divOtherInfo.Visible = True
                    End If
                    rbtnemail.Enabled = False
                    rbtnsms.Enabled = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Email_Settings_Master:BindSettings" + ex.Message)
        End Try
    End Sub

    Protected Sub rbtnemail_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnemail.CheckedChanged
        Try
            If rbtnemail.Checked = True Then
                divOtherInfo.Visible = True
            Else
                divOtherInfo.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:rbtnemail_CheckedChanged" + ex.Message)
        End Try
       
    End Sub

    Protected Sub rbtnsms_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnsms.CheckedChanged
        Try
            If rbtnemail.Checked = True Then
                divOtherInfo.Visible = True
            Else
                divOtherInfo.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:rbtnsms_CheckedChanged" + ex.Message)
        End Try
       
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsEmailSetting.Tran_id = Val(hfEmailId.Value)
            oclsEmailSetting.Company_id = Val(Session("cmp_id"))
            oclsEmailSetting.MailServer = txtserver.Text
            oclsEmailSetting.MailServer_UserName = txtusername1.Text
            oclsEmailSetting.MailServer_Password = txtpassword1.Text
            oclsEmailSetting.location_id = Val(ddLocation.SelectedValue)
            oclsEmailSetting.Port = txtport.Text

            If (rbtnemail.Checked) Then
                oclsEmailSetting.From_Email = txtfromemail.Text
                oclsEmailSetting.ssl = Val(radssl.SelectedValue)
                oclsEmailSetting.Alias = txtemailalias.Text
                oclsEmailSetting.is_MES = Val(radmes.SelectedValue)
                oclsEmailSetting.MES_URI = txtmesuri.Text
                oclsEmailSetting.Reply_to = txtreplyto.Text
                oclsEmailSetting.S_Type = 0
            Else
                oclsEmailSetting.From_Email = ""
                oclsEmailSetting.ssl = 0
                oclsEmailSetting.Alias = ""
                oclsEmailSetting.is_MES = 0
                oclsEmailSetting.MES_URI = ""
                oclsEmailSetting.Reply_to = ""
                oclsEmailSetting.S_Type = 1
            End If

            oclsEmailSetting.Ip_address = Request.UserHostAddress



            If Val(hfEmailId.Value) > 0 Then
                oclsEmailSetting.Update()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record updated successfully.');", True)
            Else
                oclsEmailSetting.Insert()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully.');", True)
            End If
            BindSettings()
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Dashboard.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Not hfEmailId.Value = Nothing Then
                BindSettings()
            Else
                Clear()
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Settings_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

End Class
