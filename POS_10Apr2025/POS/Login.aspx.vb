Imports System.Data

Partial Class Login
    Inherits BaseClass
    Dim oLogin As New Controller_clsLogin()

    Protected Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        Try
            oLogin.till_code = txtTillCode.Text.Trim
            'oLogin.Password = Encrypt(txtUpassword.Text.Trim)
            Dim dtLogin As DataTable = oLogin.SelectSignIn()

            If dtLogin.Rows.Count > 0 Then
                If dtLogin.Rows(0)("Uinfo") = "Company" Then
                    Session("cmp_id") = dtLogin.Rows(0)("Company_id")
                    Session("cmp_name") = dtLogin.Rows(0)("Name")
                    Session("Login_id") = dtLogin.Rows(0)("Login_id")
                    Session("staff_role_id") = 0
                    Response.Redirect("Dashboard.aspx", False)
                Else
                    If Not dtLogin.Rows(0)("role_id") = 0 Then
                        Session("staff_id_login") = dtLogin.Rows(0)("staff_id")
                        Session("cmp_id") = dtLogin.Rows(0)("Company_id")
                        Session("cmp_name") = dtLogin.Rows(0)("C_Name")
                        Session("staff_name") = dtLogin.Rows(0)("name")
                        Session("Login_id") = dtLogin.Rows(0)("Login_id")
                        Session("staff_role_id") = dtLogin.Rows(0)("role_id")
                        Response.Redirect("Dashboard.aspx", False)
                        'Response.Redirect("~/Dashboard.aspx", True)
                    Else
                        lblMsg.Text = "You do not have access to logon. contact your administrator"
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "donotaccess", "alert('You do not have access to logon. Contact your administrator');", True)
                    End If
                End If
            Else
                lblMsg.Text = "Invalid till code"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "InvalidUsername", "alert('Invalid Username or Password');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Login:btnLogIn_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session.Remove("cmp_id")
                Session.Remove("tab")
                Session.RemoveAll()
                txtTillCode.Focus()
            End If

            If Session("LoginSussess") IsNot Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "PasswordChanged", "alert('Your password changed successfully');", True)
                Session("LoginSussess") = Nothing
            End If
            If Session("Sussess") IsNot Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Company registered successfully');", True)
                Session("Sussess") = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Login:Page_Load:" + ex.Message)
        End Try
      
    End Sub

   
End Class
