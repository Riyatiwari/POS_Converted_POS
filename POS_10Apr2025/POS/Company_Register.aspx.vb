Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Company_Register
    Inherits BaseClass
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsBind As New clsBinding

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Try
            oclsRegister.Company_id = 0
            oclsRegister.C_Name = txtName.Text
            oclsRegister.C_Starting_Date = txtTDate.SelectedDate.ToString()
            oclsRegister.C_Code = txtCode.Text
            oclsRegister.C_Domain = txtDomain.Text
            oclsRegister.C_Email = txtEmail.Text
            oclsRegister.C_Contact = IIf(txtContact.Text = "", Nothing, txtContact.Text) 'Convert.ToInt32(txtContact.Text)
            oclsRegister.C_Description = txtDescription.Text
            oclsRegister.C_Address = txtAddress.Text
            oclsRegister.C_Country = Val(IIf(radCountry.SelectedValue = 0, 0, radCountry.SelectedValue))
            oclsRegister.C_State = Val(IIf(radState.SelectedValue = 0, 0, radState.SelectedValue))
            oclsRegister.C_City = Val(IIf(radCity.SelectedValue = 0, 0, radCity.SelectedValue))
            oclsRegister.C_Postal = txtPcode.Text
            oclsRegister.C_Website = txtWebsite.Text
            oclsRegister.U_IP_Address = Request.UserHostAddress
            oclsRegister.U_Login_id = 0
            RadBinaryImage1.DataValue = Nothing
            For Each file As UploadedFile In fileupload.UploadedFiles
                Dim image As Byte()
                Dim fileLength As Long = fileupload.UploadedFiles(0).InputStream.Length
                image = New Byte(fileLength - 1) {}
                fileupload.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                RadBinaryImage1.DataValue = image
            Next
            oclsRegister.C_Logo = RadBinaryImage1.DataValue
            oclsRegister.C_Registration_no = Nothing 'txtRegNo.Text
            oclsRegister.C_GST_VAT = Nothing 'txtGST.Text
            oclsRegister.C_CST_VAT = Nothing 'txtCST.Text
            oclsRegister.C_Service_tax_no = Nothing 'txtServiceTex.Text
            oclsRegister.C_Pan_no = Nothing 'txtPan.Text
            oclsRegister.Branch_Name = txtBranchName.Text
            oclsRegister.Synchronization = txtSynchronization.Text
            oclsRegister.Venue_Name = txtVenue.Text
            oclsRegister.Vat_No = txtVatNo.Text
            oclsRegister.Receipt_Header = txtReciptHeader.Content
            oclsRegister.Receipt_Footer = txtReciptFooter.Content
            oclsRegister.log_off_time = Val(txtlogofftime.Text)
            oclsRegister.par_sale_par_operator = IIf(txtparsaleperoperator.Text = "", 1, Val(txtparsaleperoperator.Text))
            oclsRegister.Currency_Id = Val(IIf(ddCurrency.SelectedValue = 0, 0, ddCurrency.SelectedValue))
            oclsRegister.Insert()
            Session("Sussess") = 1
            Response.Redirect("SignIn.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Company_Register:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("db_server") = Nothing Then
                Response.Redirect("Store.aspx", False)
            End If
            If Not IsPostBack Then
                oclsBind.BindCountry(radCountry)
                oclsBind.BindCurrency(ddCurrency)
            End If
        Catch ex As Exception
            LogHelper.Error("Company_Register:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub radCountry_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radCountry.SelectedIndexChanged
        Try
            If radCountry.SelectedIndex = 0 Then
                radState.Items.Clear()
                radCity.Items.Clear()
            Else
                oclsBind.BindState(radState, radCountry.SelectedValue)
                radCity.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("Company_Register:radCountry_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub radState_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radState.SelectedIndexChanged
        Try
            If radState.SelectedIndex = 0 Then
                radCity.Items.Clear()
            Else
                oclsBind.BindCity(radCity, radState.SelectedValue)
            End If
        Catch ex As Exception
            LogHelper.Error("Company_Register:radState_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("SignIn.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Company_Register:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtName.Text = Nothing
            txtCode.Text = Nothing
            txtTDate.SelectedDate = Nothing
            'txtPan.Text = Nothing
            txtDomain.Text = Nothing
            txtEmail.Text = Nothing
            txtWebsite.Text = Nothing
            txtContact.Text = Nothing
            txtDescription.Text = Nothing
            txtAddress.Text = Nothing
            radCountry.ClearSelection()
            radState.Items.Clear()
            radCity.Items.Clear()
            txtPcode.Text = Nothing
            txtVenue.Text = Nothing
            txtlogofftime.Text = Nothing
            txtBranchName.Text = Nothing
            txtSynchronization.Text = Nothing
            txtVatNo.Text = Nothing
            txtparsaleperoperator.Text = Nothing

            'txtRegNo.Text = Nothing
            'txtGST.Text = Nothing
            'txtCST.Text = Nothing
            'txtServiceTex.Text = Nothing
        Catch ex As Exception
            LogHelper.Error("Company_Register:btnReset_Click" + ex.Message)
        End Try
    End Sub
   

End Class
