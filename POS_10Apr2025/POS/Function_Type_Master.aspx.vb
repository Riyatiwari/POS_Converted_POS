Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Function_Type_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsFunctionType As New Controller_clsFunctionType()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("function_type_id") = Nothing Then
                    BindFunctionType()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Function_Type_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindFunctionType()
        Try
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.function_type_id = Val(Session("function_type_id"))
            Dim dtFunction As DataTable = oclsFunctionType.Select()
            If dtFunction.Rows.Count > 0 Then
                txtFName.Text = dtFunction.Rows(0)("name").ToString
                If dtFunction.Rows(0)("Active").ToString() = "Active" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                If dtFunction.Rows(0)("Panel").ToString() = "Is Panel" Then
                    chkPanel.Checked = True
                Else
                    chkPanel.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Function_Type_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("function_type_id") = Nothing Then
                Clear()
            Else
                BindFunctionType()
            End If
        Catch ex As Exception
            LogHelper.Error("Function_Type_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Function_Type_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Function_Type_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtFName.Text = ""
            chkActive.Checked = True
            chkPanel.Checked = True
        Catch ex As Exception
            LogHelper.Error("Function_Type_Master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsFunctionType.cmp_id = Val(Session("cmp_id"))
            oclsFunctionType.name = StrConv(txtFName.Text, vbProperCase).Trim()
            oclsFunctionType.Ip_address = Request.UserHostAddress
            oclsFunctionType.login_id = Val(Session("login_id"))
            oclsFunctionType.machine_id = 0
            oclsFunctionType.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsFunctionType.is_panel = IIf(chkPanel.Checked = True, 1, 0)
            If Session("function_type_id") = Nothing Then
                oclsFunctionType.function_type_id = 0
                oclsFunctionType.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsFunctionType.function_type_id = Val(Session("function_type_id"))
                oclsFunctionType.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("function_type_id") = Nothing
            Response.Redirect("Function_Type_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Type_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub
End Class