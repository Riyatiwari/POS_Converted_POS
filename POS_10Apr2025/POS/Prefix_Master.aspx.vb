Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Prefix_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPrefix As New Controller_clsPrefix()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("Prefix_id") = Nothing Then
                    BindPrefix()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Prefix_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindPrefix()
        Try
            oclsPrefix.Prefix_id = Val(Session("Prefix_id"))
            oclsPrefix.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPrefix.Select()
            If dt.Rows.Count > 0 Then
                txtPrefix.Text = dt.Rows(0)("Prefix").ToString
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Prefix_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Prefix_id") = Nothing Then
                Clear()
            Else
                BindPrefix()
            End If
        Catch ex As Exception
            LogHelper.Error("Prefix_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Prefix_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Prefix_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtPrefix.Text = ""
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Prefix_Master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsPrefix.Prefix = txtPrefix.Text.Trim()
            oclsPrefix.cmp_id = Val(Session("cmp_id"))
            oclsPrefix.login_id = Val(Session("login_id"))
            oclsPrefix.Ip_address = Request.UserHostAddress
            oclsPrefix.is_active = IIf(chkActive.Checked = True, 1, 0)
            If Session("Prefix_id") = Nothing Then
                oclsPrefix.Prefix_id = 0
                oclsPrefix.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsPrefix.Prefix_id = Val(Session("Prefix_id"))
                oclsPrefix.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Prefix_id") = Nothing
            Response.Redirect("Prefix_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Prefix_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub
End Class
