Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.OleDb

Partial Class Forgot_Password
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess
    Dim oClsLogin As New clsLogin

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Request.QueryString("fpc") = Nothing Then
                    Try
                        ViewState("loginid") = Decrypt(Request.QueryString("fpc").ToString())
                        oClsLogin.Login_id = ViewState("loginid")
                        'oClsLogin.Login_id = Decrypt(Request.QueryString("fpc").ToString())
                        'Dim dtEmail As DataTable = oClsDataccess.Getdatatable("select Username from M_Login where Login_id = " + ViewState("loginid").ToString())
                        Dim dtEmail As DataTable = oClsLogin.SelectUsernameForgotPsw()
                        If Not dtEmail.Rows.Count > 0 Then
                            Response.Redirect("SignIn.aspx", False)
                        End If
                    Catch ex As Exception
                        Response.Redirect("SignIn.aspx", False)
                    End Try
                Else
                    Response.Redirect("SignIn.aspx", False)
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Forgot_Password:Page_Load" + ex.Message)
        End Try
      

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Dim login = New Controller_clsLogin
            login.Login_id = Val(ViewState("loginid"))
            login.NewPassword = Encrypt(txtNewPassword.Text)
            login.Reset()
            Session("LoginSussess") = "yes"
            Response.Redirect("SignIn.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Forgot_Password:btnReset_Click" + ex.Message)
        End Try
    End Sub

End Class
