Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Panel_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPanel As New Controller_clsPanel()
    Dim oclsFunctionCode As New Controller_clsFunctionCode()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("Panel_id") = Nothing Then
                    BindPanel()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Panel_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindPanel()
        Try
            oclsPanel.cmp_id = Val(Session("cmp_id"))
            oclsPanel.Panel_id = Val(Session("Panel_id"))

            Dim dt As DataTable = oclsPanel.Select()
            If dt.Rows.Count > 0 Then
                txpanel.Text = dt.Rows(0)("Panel").ToString
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Panel_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub

    Private Sub SaveFunctionCode(ByVal id As Integer)
        Try
            Dim dtfunctioncode As DataTable
            dtfunctioncode = oclsFunctionCode.SelectFunctioncode()

            If dtfunctioncode.Rows(0)("Panel_Id").ToString() = "0" Then
                oclsFunctionCode.Panel_Id = id
                oclsFunctionCode.update()
            Else
                For i = 0 To 29
                    Dim c As StringBuilder = New StringBuilder()
                    c.Append("F")
                    c.Append(i + 1)
                    Dim stCode As String = c.ToString()
                    oclsFunctionCode.Function_Code_id = i + 1
                    oclsFunctionCode.Code = stCode
                    oclsFunctionCode.Panel_Id = id
                    oclsFunctionCode.insert()
                Next
            End If

            'For i = 0 To 29
            '    Dim c As StringBuilder = New StringBuilder()
            '    c.Append("F")
            '    c.Append(i + 1)
            '    Dim stCode As String = c.ToString()
            '    oclsFunctionCode.Function_Code_id = i + 1
            '    oclsFunctionCode.Code = stCode
            '    oclsFunctionCode.Panel_Id = id
            '    oclsFunctionCode.insert()
            'Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Panel_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Panel_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Panel_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txpanel.Text = ""
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Panel_Master:Clear" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsPanel.cmp_id = Val(Session("cmp_id"))
            oclsPanel.Panel = txpanel.Text.Trim()
            oclsPanel.Ip_address = Request.UserHostAddress
            oclsPanel.login_id = Val(Session("login_id"))
            oclsPanel.is_active = IIf(chkActive.Checked = True, 1, 0)
            Dim dt As DataTable
            If Session("Panel_id") = Nothing Then
                oclsPanel.Panel_id = 0
                dt = oclsPanel.insert()
                Dim Panel_id As Integer = dt.Rows(0)("Panel_id").ToString
                'SaveFunctionCode(Panel_id)
                Session("Success") = "Record inserted successfully"
            Else
                oclsPanel.Panel_id = Val(Session("Panel_id"))
                dt = oclsPanel.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Panel_id") = Nothing
            Response.Redirect("Panel_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Panel_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub


End Class
