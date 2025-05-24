Imports System.Data
Imports System.Security.Cryptography

Partial Class Email_Template_Master
    Inherits BaseClass
    Dim oclsTemplate As New Controller_clsTemplate()
    Dim oClsDataccess As New ClsDataccess()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx")
                End If
                If Not Session("TemplateID") = Nothing Then
                    BindData()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Template_Master:Page_Load" + ex.Message)
        End Try

    End Sub

    Protected Sub BindData()
        Try
            oclsTemplate.Cmp_id = Val(Session("cmp_id"))
            oclsTemplate.Template_id = Val(Session("TemplateID"))
            Dim dtEmail_Template As DataTable = oclsTemplate.Select()
            If dtEmail_Template.Rows.Count > 0 Then
                Email_Template_ID.Value = Val(dtEmail_Template.Rows(0)("template_id"))
                txtTempName.Text = dtEmail_Template.Rows(0)("template_name").ToString
                txtSubject.Text = dtEmail_Template.Rows(0)("subject").ToString
                txtbody.Content = dtEmail_Template.Rows(0)("template_detail").ToString

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Email_Template_Master:BindData" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsTemplate.Cmp_id = Val(Session("cmp_id"))
            oclsTemplate.Template_name = txtTempName.Text.Trim
            oclsTemplate.subject = txtSubject.Text.Trim
            oclsTemplate.template_detail = txtbody.Content
            oclsTemplate.is_newsletter = 0
           
            If Session("TemplateID") = Nothing Then
                oclsTemplate.Template_id = 0
                oclsTemplate.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsTemplate.Template_id = Val(Session("TemplateID"))
                oclsTemplate.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("TemplateID") = Nothing
            Response.Redirect("Email_Template_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Email_Template_Master:btnSave_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Session("TemplateID") = Nothing
            Response.Redirect("Email_Template_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Email_Template_Master:btnCancel_Click" + ex.Message)
        End Try
        
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Not Session("TemplateID") = Nothing Then
                BindData()
            Else
                Clear()
            End If
        Catch ex As Exception
            LogHelper.Error("Email_Template_Master:btnReset_Click" + ex.Message)
        End Try
       
    End Sub

    Protected Sub Clear()
        Try
            txtTempName.Text = Nothing
            txtSubject.Text = Nothing
            txtbody.Content = Nothing
        Catch ex As Exception
            LogHelper.Error("Email_Template_Master:Clear" + ex.Message)
        End Try
       
    End Sub

End Class
