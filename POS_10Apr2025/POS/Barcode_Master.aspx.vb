Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Barcode_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsBarcode As New Controller_clsBarcodeSize()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))

                If Not Session("Barcode_id") = Nothing Then
                    BindBarcode()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Barcode_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindBarcode()
        Try
            Dim sender As Object
            Dim e As EventArgs

            oclsBarcode.cmp_id = Val(Session("cmp_id"))
            oclsBarcode.Barcode_Size_id = Val(Session("Barcode_id"))

            Dim dt As DataTable = oclsBarcode.Select()
            If dt.Rows.Count > 0 Then
                txbarcode.Text = dt.Rows(0)("Barcode_Size").ToString
                radProduct.ClearSelection()
                radProduct.SelectedValue = Val(dt.Rows(0)("product_id"))
                Try
                    radProduct_SelectedIndexChanged(sender, e)
                    radSize.ClearSelection()
                    radSize.SelectedValue = (Val(dt.Rows(0)("Size_Id")))
                    'radSize.FindItemByValue(Val(dt.Rows(0)("Size_Id"))).Selected = True

                Catch ex As Exception
                    LogHelper.Error("Barcode_Master:BindSizeDropdown" + ex.Message)
                End Try
                If dt.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Barcode_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Barcode_id") = Nothing Then
                Clear()
            Else
                BindBarcode()
            End If
        Catch ex As Exception
            LogHelper.Error("Barcode_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Barcode_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Barcode_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txbarcode.Text = ""
            radProduct.SelectedIndex = 0
            chkActive.Checked = True
        Catch ex As Exception
            LogHelper.Error("Barcode_Master:Clear" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsBarcode.cmp_id = Val(Session("cmp_id"))
            oclsBarcode.Barcode = txbarcode.Text.Trim()
            oclsBarcode.Ip_address = Request.UserHostAddress
            oclsBarcode.login_id = Val(Session("login_id"))
            oclsBarcode.product_id = Val(IIf(radProduct.SelectedValue = "", 0, radProduct.SelectedValue))
            oclsBarcode.Size_Id = Val(IIf(radSize.SelectedValue = "", 0, radSize.SelectedValue))
            oclsBarcode.is_active = IIf(chkActive.Checked = True, 1, 0)
            If Session("Barcode_id") = Nothing Then
                oclsBarcode.Barcode_Size_id = 0
                oclsBarcode.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsBarcode.Barcode_Size_id = Val(Session("Barcode_id"))
                oclsBarcode.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Barcode_id") = Nothing
            Response.Redirect("Barcode_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Barcode_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    'Protected Sub radProduct_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radProduct.SelectedIndexChanged
    '    Try
    '        If radProduct.SelectedIndex = 0 Then
    '            radSize.Items.Clear()
    '        Else
    '            oclsBind.BindProductBySize(radSize, Val(Session("cmp_id")), Val(radProduct.SelectedValue))
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Barcode_Master:radProduct_SelectedIndexChanged" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub radProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radProduct.SelectedIndex = 0 Then
                radSize.Items.Clear()
            Else
                oclsBind.BindProductBySize(radSize, Val(Session("cmp_id")), Val(radProduct.SelectedValue))
            End If
        Catch ex As Exception
            LogHelper.Error("Barcode_Master:radProduct_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub
End Class
