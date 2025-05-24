Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Device_Type_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsDeviceType As New Controller_clsDevice_Type()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("Device_Type_id") = Nothing Then
                    bindDevice_Type()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Device_Type_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub bindDevice_Type()
        Try
            oclsDeviceType.cmp_id = Val(Session("cmp_id"))
            oclsDeviceType.Device_Type_id = Val(Session("Device_Type_id"))
            Dim dt As DataTable = oclsDeviceType.Select()
            If dt.Rows.Count > 0 Then
                txtName.Text = dt.Rows(0)("Device_Type").ToString
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Device_Type_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Device_Type_id") = Nothing Then
                Clear()
            Else
                bindDevice_Type()
            End If
        Catch ex As Exception
            LogHelper.Error("Device_Type_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Device_Type_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Device_Type_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtName.Text = ""
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Device_Type_Master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsDeviceType.cmp_id = Val(Session("cmp_id"))
            oclsDeviceType.Device_Type = txtName.Text.Trim()
            oclsDeviceType.login_id = Val(Session("login_id"))
            oclsDeviceType.Ip_address = Request.UserHostAddress
            oclsDeviceType.is_active = IIf(chkActive.Checked = True, 1, 0)
            If Session("Device_Type_id") = Nothing Then
                oclsDeviceType.Device_Type_id = 0
                oclsDeviceType.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsDeviceType.Device_Type_id = Val(Session("Device_Type_id"))
                oclsDeviceType.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Device_Type_id") = Nothing
            Response.Redirect("Device_Type_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Device_Type_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

End Class
