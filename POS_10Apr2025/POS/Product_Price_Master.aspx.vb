Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Product_Price_Master
    Inherits System.Web.UI.Page
    ' Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSizeMaster As New Controller_clsSizeMaster()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Not Session("Product_Price_Id") = Nothing Then
                    BindPrice()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Price_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindPrice()
        Try
            oclsSizeMaster.cmp_id = Val(Session("cmp_id"))
            oclsSizeMaster.Product_Price_Id = Val(Session("Product_Price_Id"))

            Dim dt As DataTable = oclsSizeMaster.Select()
            If dt.Rows.Count > 0 Then
                txtPrice.Text = dt.Rows(0)("PPrice").ToString()
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

                If dt.Rows(0)("is_default").ToString() = "Yes" Then
                    chkIsDefault.Checked = True
                Else
                    chkIsDefault.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Price_Master:BindSize" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Product_Price_Master_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Price_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsSizeMaster.cmp_id = Val(Session("cmp_id"))
            oclsSizeMaster.PPrice = txtPrice.Text.Trim()
            oclsSizeMaster.Ip_address = Request.UserHostAddress
            oclsSizeMaster.login_id = Val(Session("login_id"))
            oclsSizeMaster.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsSizeMaster.is_default = IIf(chkIsDefault.Checked = True, 1, 0)
            If Session("Product_Price_Id") = Nothing Then
                oclsSizeMaster.Product_Price_Id = 0
                oclsSizeMaster.insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsSizeMaster.Product_Price_Id = Val(Session("Product_Price_Id"))
                oclsSizeMaster.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Product_Price_Id") = Nothing
            Response.Redirect("Product_Price_Master_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Price_Master:btnSave_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Product_Price_Id") = Nothing Then
                Clear()
            Else
                BindPrice()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Price_Master:btnReset_Click" + ex.Message)
        End Try

    End Sub
    Private Sub Clear()
        Try
            txtPrice.Text = ""
            chkActive.Checked = True
            chkIsDefault.Checked = False

        Catch ex As Exception
            LogHelper.Error("Product_Price_Master:Clear" + ex.Message)
        End Try
    End Sub
End Class
