Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Unit_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsUnit As New Controller_clsUnit()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("Unit_Id") = Nothing Then
                    BindUnit()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Unit_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindUnit()
        Try
            oclsUnit.cmp_id = Val(Session("cmp_id"))
            oclsUnit.Unit_id = Val(Session("Unit_Id"))

            Dim dt As DataTable = oclsUnit.Select()
            If dt.Rows.Count > 0 Then
                txtUnit.Text = dt.Rows(0)("Unit").ToString()
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Unit_Master:BindUnit" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Unit_Id") = Nothing Then
                Clear()
            Else
                BindUnit()
            End If
        Catch ex As Exception
            LogHelper.Error("Unit_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Unit_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Unit_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtUnit.Text = ""
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Unit_Master:Clear" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsUnit.cmp_id = Val(Session("cmp_id"))
            oclsUnit.Unit = txtUnit.Text.Trim()
            oclsUnit.Ip_address = Request.UserHostAddress
            oclsUnit.login_id = Val(Session("login_id"))
            oclsUnit.is_active = IIf(chkActive.Checked = True, 1, 0)
            If Session("Unit_Id") = Nothing Then
                oclsUnit.Unit_id = 0
                oclsUnit.insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsUnit.Unit_id = Val(Session("Unit_Id"))
                oclsUnit.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Unit_Id") = Nothing
            Response.Redirect("Unit_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Unit_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub
End Class
