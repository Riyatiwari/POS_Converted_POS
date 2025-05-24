Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Reason_Master
    Inherits BaseClass
    Dim oclsReason As New Controller_clsReason()
    Dim oclsBind As New clsBinding

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("reason_id") = Nothing Then
                    BindReason()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Reason_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
  
    Private Sub BindReason()
        Try
            oclsReason.Reason_id = Val(Session("reason_id"))
            oclsReason.cmp_id = Val(Session("cmp_id"))
            Dim dtCategory As DataTable = oclsReason.Select()
            If dtCategory.Rows.Count > 0 Then
                txtreason.Text = dtCategory.Rows(0)("reason").ToString
                txtCdescription.Text = dtCategory.Rows(0)("description").ToString
                If dtCategory.Rows(0)("is_active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Reason_Master:BindProductGroup:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("reason_id") = Nothing Then
                Clear()
            Else
                BindReason()
            End If
        Catch ex As Exception
            LogHelper.Error("Reason_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Reason_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Reason_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtreason.Text = ""
            txtCdescription.Text = ""

        Catch ex As Exception
            LogHelper.Error("Reason_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsReason.cmp_id = Val(Session("cmp_id"))
            oclsReason.reason = txtreason.Text.Trim()
            oclsReason.description = txtCdescription.Text
            'oclsReason.key_map_id = Val(ddKeyMap.SelectedValue)

            oclsReason.Is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsReason.Ip_address = Request.UserHostAddress
            oclsReason.login_id = Val(Session("login_id"))

            Dim dt As DataTable
            If Session("reason_id") = Nothing Then
                oclsReason.reason_id = 0
                dt = oclsReason.Insert()

                Session("Success") = "Record inserted successfully"
                Session("reason_id") = Nothing

                Dim s As String
                If dt.Rows.Count > 0 Then
                    s = dt.Rows(0)("msg")
                End If

                If s <> "" Then
                    Session("reason_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Reason_List.aspx';", True)
                Else
                    Session("reason_id") = Nothing
                    Response.Redirect("Reason_List.aspx", False)
                End If
            Else
                oclsReason.Reason_id = Val(Session("reason_id"))
                dt = oclsReason.Update()
                Session("Success") = "Record updated successfully"
                Dim s As String
                If dt.Rows.Count > 0 Then
                    s = dt.Rows(0)("msg")
                End If
                If s <> "" Then
                    Session("reason_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Reason_List.aspx';", True)
                Else
                    Session("reason_id") = Nothing
                    Response.Redirect("Reason_List.aspx", False)
                End If
            End If
            'Session("category_id") = Nothing
            'Response.Redirect("Product_Group_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Reason_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

End Class
