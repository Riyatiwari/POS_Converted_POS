Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Company_Master
    Inherits BaseClass
    Dim oclsRegister As New Controller_clsRegister()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oClsDataccess As New ClsDataccess
    ' Dim isPopupShown As Boolean = False
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsRegister.Company_id = Val(Session("cmp_id"))
            oclsRegister.C_Name = txtName.Text
            oclsRegister.C_Starting_Date = txtTDate.SelectedDate.ToString()
            oclsRegister.C_Code = txtCode.Text
            oclsRegister.C_Domain = txtDomain.Text
            oclsRegister.C_Email = txtEmail.Text
            oclsRegister.C_Contact = IIf(txtContact.Text = "", Nothing, txtContact.Text)
            oclsRegister.C_Description = txtDescription.Text
            oclsRegister.C_Address = txtAddress.Text
            oclsRegister.Branch_Name = txtBranchName.Text
            oclsRegister.Synchronization = txtSynchronization.Text
            oclsRegister.Venue_Name = txtVenue.Text
            oclsRegister.Vat_No = txtVatNo.Text
            oclsRegister.Receipt_Header = WebUtility.HtmlDecode(txtReciptHeader.Content)
            oclsRegister.Receipt_Footer = WebUtility.HtmlDecode(txtReciptFooter.Content)

            oclsRegister.C_Country = Val(IIf(radCountry.SelectedValue = 0, 0, radCountry.SelectedValue))
            oclsRegister.C_State = Val(IIf(radState.SelectedValue = 0, 0, radState.SelectedValue))
            oclsRegister.C_City = Val(IIf(radCity.SelectedValue = 0, 0, radCity.SelectedValue))
            oclsRegister.C_LiteVersion = Val(IIf(version.SelectedValue = 0, 0, version.SelectedValue))
            oclsRegister.C_Postal = txtPcode.Text
            oclsRegister.C_Website = txtWebsite.Text
            oclsRegister.U_IP_Address = Request.UserHostAddress
            oclsRegister.U_Login_id = 0
            oclsRegister.week_start_day = radWeekStartDay.SelectedItem.Text

            Session("product_type") = version.SelectedValue.ToString()
            ' RadBinaryImage1.DataValue = Nothing
            'If ViewState("Image") = 1 Then

            Dim i As Integer = 0
            For Each file As UploadedFile In fileupload.UploadedFiles
                i = i + 1
            Next
            If i > 0 Then
                RadBinaryImage1.DataValue = Nothing
                For Each file As UploadedFile In fileupload.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = fileupload.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    fileupload.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImage1.DataValue = image
                    Image1.Visible = True
                    Dim bytes As Byte() = RadBinaryImage1.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                RadBinaryImage1.DataValue = ViewState("logo")
            End If


            'Else
            '    RadBinaryImage1.DataValue = Nothing
            '    For Each file As UploadedFile In fileupload.UploadedFiles
            '        Dim image As Byte()
            '        Dim fileLength As Long = fileupload.UploadedFiles(0).InputStream.Length
            '        image = New Byte(fileLength - 1) {}
            '        fileupload.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
            '        RadBinaryImage1.DataValue = image
            '    Next


            'End If
            oclsRegister.C_Logo = RadBinaryImage1.DataValue

            oclsRegister.C_Registration_no = Nothing 'txtRegNo.Text
            oclsRegister.C_GST_VAT = Nothing 'txtGST.Text
            oclsRegister.C_CST_VAT = Nothing 'txtCST.Text
            oclsRegister.C_Service_tax_no = Nothing 'txtServiceTex.Text
            oclsRegister.C_Pan_no = Nothing 'txtPan.Text
            oclsRegister.log_off_time = Val(txtlogofftime.Text)

            oclsRegister.par_sale_par_operator = Val(txtparsaleperoperator.Text)
            oclsRegister.Currency_Id = Val(IIf(ddCurrency.SelectedValue = 0, 0, ddCurrency.SelectedValue))
            oclsRegister.judo_id = ""
            oclsRegister.judo_token = ""
            oclsRegister.judo_secret = ""
            oclsRegister.week_start_day = radWeekStartDay.SelectedItem.Text
            oclsRegister.Show_chips = Val(IIf(chkChips.Checked = True, 1, 0))
            oclsRegister.Display_declaration = Val(IIf(chkDisplayDeclaration.Checked = True, 1, 0))
            oclsRegister.BusinessDoneBy = Val(IIf(ddBusinessDone.SelectedValue = 0, 0, ddBusinessDone.SelectedValue))
            oclsRegister.chk_duration = Val(IIf(chk_duration.Checked = True, 1, 0))
            oclsRegister.IsAddTax2 = Val(IIf(chkIsAddTax2.Checked = True, 1, 0))
            oclsRegister.IsExclusiveTax = Val(IIf(chkIsExclusiveTax.Checked = True, 1, 0))
            oclsRegister.IsPaymentGtway = Val(IIf(chk_PGtway.Checked = True, 1, 0))

            oclsRegister.Update()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Company Information Upgraded Success');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Company settings updated successfully');window.location='Company_Master.aspx';", True)
            Session("cmp_name") = txtName.Text
            Session("IsAddTax2") = Val(IIf(chkIsAddTax2.Checked = True, 1, 0))
            Session("IsExclusiveTax") = Val(IIf(chkIsExclusiveTax.Checked = True, 1, 0))
            Session("IsPaymentGtway") = Val(IIf(chk_PGtway.Checked = True, 1, 0))

            '------------Start update all tax2 0 in M_Price if flag set to 0 (20/04/2022)------------------
            If Val(IIf(chkIsAddTax2.Checked = True, 1, 0)) = 0 Then
                oclsRegister.Update_Price()
            End If
            '------------End (20/04/2022)------------------





            'If Session("product_type") = "2" Then

            '    Dim dtcheck As DataTable = oclsRegister.checkProduct()



            '    Dim count As Integer = Convert.ToInt32(dtcheck.Rows(0)(0))
            '    If count > 75 Then

            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Downgrade to TenderLITE is only available if 75 or fewer entries are available in m_category table.');", True)
            '        Return
            '    End If


            'End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Company_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                oclsBind.BindCountry(radCountry)
                oclsBind.BindCurrency(ddCurrency)
                'oclsBind.BindBusinessDone(ddBusinessDone)

                oclsRegister.Currency_Id = Val(IIf(ddCurrency.SelectedValue = 0, 0, ddCurrency.SelectedValue))

                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Then
                    Div_Button.Visible = False
                Else
                    Div_Button.Visible = True
                End If
                If Val(ViewState("edit")) = 1 Then
                    Div_Button.Visible = True
                Else
                    Div_Button.Visible = False
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divCompany.Visible = True
                    updatenewstore()
                    BindCompany()
                Else
                    divCompany.Visible = False
                End If

                ShowStoreUUIDPopup()


            End If
        Catch ex As Exception
            LogHelper.Error("Company_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub updatenewstore()
        oclsRegister.Company_id = Val(Session("cmp_id"))
        Dim dtCompany As DataTable = oclsRegister.Select()


        If Convert.ToInt32(dtCompany.Rows(0)("Country")) = 0 AndAlso
            Convert.ToInt32(dtCompany.Rows(0)("State")) = 0 AndAlso
             Convert.ToInt32(dtCompany.Rows(0)("City")) = 0 Then
            oclsRegister.Company_id = Val(Session("cmp_id"))
            oclsRegister.Update_newStore()

        End If

    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Company"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If

        Catch ex As Exception

            LogHelper.Error("Company_Master:RoleCheck" + ex.Message)
        End Try
    End Sub
    Protected Sub BindCompany()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            oclsRegister.Company_id = Val(Session("cmp_id"))
            Dim dtCompany As DataTable = oclsRegister.Select()
            If dtCompany.Rows.Count > 0 Then
                txtName.Text = dtCompany.Rows(0)("Name").ToString
                hdcnm.Value = dtCompany.Rows(0)("Name").ToString
                txtDomain.Text = dtCompany.Rows(0)("Domain").ToString
                txtCode.Text = dtCompany.Rows(0)("Code").ToString
                txtlogofftime.Text = dtCompany.Rows(0)("log_off_time")
                txtparsaleperoperator.Text = dtCompany.Rows(0)("par_sale_par_operator")
                txtTDate.SelectedDate = dtCompany.Rows(0)("Starting_Date").ToString

                txtEmail.Text = dtCompany.Rows(0)("Email").ToString
                txtContact.Text = dtCompany.Rows(0)("Contact").ToString
                txtDescription.Text = dtCompany.Rows(0)("Description").ToString
                txtAddress.Text = dtCompany.Rows(0)("Address").ToString
                txtBranchName.Text = dtCompany.Rows(0)("Branch_Name").ToString
                txtSynchronization.Text = dtCompany.Rows(0)("Synchronization").ToString
                txtVenue.Text = dtCompany.Rows(0)("Venue_Name").ToString
                txtVatNo.Text = dtCompany.Rows(0)("Vat_No").ToString
                txtReciptHeader.Content = dtCompany.Rows(0)("Receipt_Header").ToString
                txtReciptFooter.Content = dtCompany.Rows(0)("Receipt_Footer").ToString
                version.ClearSelection()

                Try
                    '''''''''''' 15-jan-2024
                    'If Convert.ToInt32(dtCompany.Rows(0)("Country")) = 0 Then
                    '    radCountry.ClearSelection()
                    '    radCountry.FindItemByText("United Kingdom").Selected = True
                    '    dtCompany.Rows(0)("Country") = 80
                    'Else
                    '    Dim countryItem = radCountry.FindItemByValue(Val(dtCompany.Rows(0)("Country")))
                    '    If countryItem IsNot Nothing Then
                    '        countryItem.Selected = True
                    '    End If
                    'End If

                    'If Convert.ToInt32(dtCompany.Rows(0)("State")) = 0 Then
                    '    radState.ClearSelection()
                    '    Dim stateItem = radState.FindItemByText("London")
                    '    If stateItem IsNot Nothing Then
                    '        stateItem.Selected = True
                    '    End If
                    '    dtCompany.Rows(0)("State") = 2511
                    'Else
                    '    Dim stateItem = radState.FindItemByValue(Val(dtCompany.Rows(0)("State")))
                    '    If stateItem IsNot Nothing Then
                    '        stateItem.Selected = True
                    '    End If
                    'End If

                    'If Convert.ToInt32(dtCompany.Rows(0)("City")) = 0 Then
                    '    radCity.ClearSelection()
                    '    Dim cityItem = radCity.FindItemByText("London")
                    '    If cityItem IsNot Nothing Then
                    '        cityItem.Selected = True
                    '    End If
                    '    dtCompany.Rows(0)("City") = 145729
                    'Else
                    '    Dim cityItem = radCity.FindItemByValue(Val(dtCompany.Rows(0)("City")))
                    '    If cityItem IsNot Nothing Then
                    '        cityItem.Selected = True
                    '    End If
                    'End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    radCountry.ClearSelection()
                    radCountry.FindItemByValue(Val(dtCompany.Rows(0)("Country"))).Selected = True
                    radCountry_SelectedIndexChanged(sender, e)
                    radState.ClearSelection()
                    radState.FindItemByValue(Val(dtCompany.Rows(0)("State"))).Selected = True
                    radState_SelectedIndexChanged(sender, e)
                    radCity.ClearSelection()
                    radCity.FindItemByValue(Val(dtCompany.Rows(0)("City"))).Selected = True
                Catch ex As Exception

                End Try



                ddCurrency.ClearSelection()
                'ddCurrency.SelectedValue = dtCompany.Rows(0)("Currency_Id")
                Dim currencyId As Integer = Convert.ToInt32(dtCompany.Rows(0)("Currency_Id"))

                If currencyId = 0 Then
                    ddCurrency.SelectedValue = "2"
                Else
                    ddCurrency.SelectedValue = dtCompany.Rows(0)("Currency_Id")
                End If
                txtPcode.Text = dtCompany.Rows(0)("Postal").ToString
                txtWebsite.Text = dtCompany.Rows(0)("Website").ToString
                If dtCompany.Rows(0)("Logo") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dtCompany.Rows(0)("Logo"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                    ViewState("Image") = 1
                    ViewState("logo") = dtCompany.Rows(0)("Logo")
                    RadBinaryImage1.DataValue = dtCompany.Rows(0)("Logo")
                End If

                radWeekStartDay.SelectedValue = dtCompany.Rows(0)("week_start_day").ToString

                'If dtCompany.Rows(0)("ProductType") = "0" And dtCompany.Rows(0)("ProductType") = "1" Then

                '    Session("tenderPoS") = 1

                'End If

                'If dtCompany.Rows(0)("ProductType").ToString() = "2" Then
                '    Session("tenderLITE") = 1
                'End If
                If dtCompany.Rows(0)("show_chips").ToString() = "1" Then
                    Session("show_chips") = "1"
                    chkChips.Checked = True
                Else
                    Session("show_chips") = "0"
                    chkChips.Checked = False
                End If

                If dtCompany.Rows(0)("IsPaymentGtway").ToString() = "1" Then
                    chk_PGtway.Checked = True
                Else
                    chk_PGtway.Checked = False
                End If

                If dtCompany.Rows(0)("Display_declaration").ToString() = "1" Then
                    chkDisplayDeclaration.Checked = True
                Else
                    chkDisplayDeclaration.Checked = False
                End If

                If dtCompany.Rows(0)("chk_duration").ToString() = "1" Then
                    chk_duration.Checked = True
                Else
                    chk_duration.Checked = False
                End If

                If ddCurrency.SelectedItem.Text.ToString = "$" Then
                    chkIsAddTax2.Checked = True
                    div_tax2.Visible = True
                    div_ExclusiveTax.Visible = True
                    chkIsExclusiveTax.Checked = True

                Else
                    chkIsAddTax2.Checked = False
                    div_tax2.Visible = False
                    div_ExclusiveTax.Visible = False
                    chkIsExclusiveTax.Checked = False

                End If

                If dtCompany.Rows(0)("IsAddTax2").ToString() = "1" Then
                    chkIsAddTax2.Checked = True
                Else
                    chkIsAddTax2.Checked = False
                End If

                If dtCompany.Rows(0)("IsExclusiveTax").ToString() = "1" Then
                    chkIsExclusiveTax.Checked = True
                Else
                    chkIsExclusiveTax.Checked = False
                End If


                ddBusinessDone.ClearSelection()
                ddBusinessDone.SelectedValue = dtCompany.Rows(0)("BusinessDoneBy")
                version.FindItemByValue(Session("product_type")).Selected = True

            End If
        Catch ex As Exception
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Company_Master:BindCompany" + ex.Message)
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
            LogHelper.Error("Company_Master:radCountry_SelectedIndexChanged" + ex.Message)
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
            LogHelper.Error("Company_Master:radState_SelectedIndexChanged" + ex.Message)
        End Try

    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Dashboard.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Company_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            BindCompany()
        Catch ex As Exception
            LogHelper.Error("Company_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub ddCurrency_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            If ddCurrency.SelectedItem.Text.ToString = "$" Then
                chkIsAddTax2.Checked = True
                div_tax2.Visible = True
                div_ExclusiveTax.Visible = True
                chkIsExclusiveTax.Checked = True

            Else
                chkIsAddTax2.Checked = False
                div_tax2.Visible = False
                div_ExclusiveTax.Visible = False
                chkIsExclusiveTax.Checked = False

            End If

        Catch ex As Exception
            LogHelper.Error("Company_Master:ddCurrency_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub



    Protected Sub Version_SelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles version.SelectedIndexChanged
        If version.SelectedValue = "2" Then
            oclsRegister.Company_id = Val(Session("cmp_id"))
            Dim dtcheck As DataTable = oclsRegister.checkProduct()
            If dtcheck.Rows.Count > 0 Then

                Dim count As Integer = Convert.ToInt32(dtcheck.Rows(0)(0))
                If count < 75 Then
                    Dim dtGrp As DataTable = oclsRegister.checkgroup()
                    If dtGrp.Rows.Count > 0 Then
                        Dim coun As Integer = Convert.ToInt32(dtGrp.Rows(0)(0))
                        If coun <= 8 Then




                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('your match list ');", True)
                        Else

                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Contact support Team');", True)
                            version.ClearSelection()
                            Return
                        End If
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Contact the Support Team');", True)
                    version.ClearSelection()
                    Return

                End If
            End If
        End If
    End Sub

    Protected Sub btnEnableLogin_Click(sender As Object, e As EventArgs)
        Try

            ''''''''''''''''''28022024    
            If Session("conStoreuuid").ToString() = "" OrElse Session("conStoreuuid").ToString().ToLower() = "null" OrElse Session("conStoreuuid").ToString() = "0" Then
                Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                strcon.Open()
                Dim n As String = Session("Storename")
                Dim enable As String = Guid.NewGuid().ToString

                Dim cmd As SqlCommand = New SqlCommand("Storeuuid_enable", strcon)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@store_name", n)
                cmd.Parameters.AddWithValue("@store_uuid", enable)
                cmd.ExecuteNonQuery()
                Session("conStoreuuid") = enable
                If Session("conStoreuuid").ToString() IsNot Nothing Then



                    Dim enlogin As New Controller_clsLogin()
                    enlogin.Store_UUID = Session("conStoreuuid")

                    Dim dtlogin As DataTable = enlogin.enablelogin()
                End If
                ShowStoreUUIDPopup()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)

            ElseIf Session("StoreUUID").ToString() = "" OrElse Session("StoreUUID").ToString().ToLower() = "null" OrElse Session("StoreUUID").ToString() = "0" Then

                Dim storeUuid As String = Session("conStoreuuid").ToString()
                Dim objclsLogin As New Controller_clsLogin()
                objclsLogin.Store_UUID = storeUuid

                Dim dtlogin As DataTable = objclsLogin.enablelogin()
                Session("StoreUUID") = Session("conStoreuuid")
                ShowStoreUUIDPopup()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)
            ElseIf (Session("conStoreUUID").ToString()) <> Session("StoreUUID").ToString() Then
                Dim storeUuid As String = Session("conStoreuuid").ToString()
                Dim objclsLogin As New Controller_clsLogin()
                objclsLogin.Store_UUID = storeUuid

                Dim dtlogin As DataTable = objclsLogin.enablelogin()
                Session("StoreUUID") = Session("conStoreuuid")
                ShowStoreUUIDPopup()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)


            End If

        Catch ex As Exception
            LogHelper.Error("Company_Master:btnEnableLogin_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub ShowStoreUUIDPopup()
        If Session("StoreUUID") Is Nothing OrElse Session("StoreUUID").ToString() = "" OrElse Session("StoreUUID").ToString().ToLower() = "null" OrElse Session("conStoreuuid").ToString() = "" OrElse Session("conStoreuuid").ToString().ToLower() = "null" OrElse Session("conStoreuuid").ToString().ToLower() = "0" OrElse (Session("conStoreUUID").ToString()) <> Session("StoreUUID").ToString() Then
            btnEnableLogin.Visible = True

            ' isPopupShown = True
        Else
            btnEnableLogin.Visible = False

            'isPopupShown = False
        End If
    End Sub

    Protected Sub deleteAll_Click(sender As Object, e As EventArgs)
        Try


            oclsRegister.Company_id = Val(Session("cmp_id"))
            Dim dtGrp As DataTable = oclsRegister.ClearAll()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Deleted All Data Successfully');", True)
        Catch ex As Exception
            LogHelper.Error("Company_Master:deleteAll_Click:" + ex.Message)
        End Try
    End Sub
End Class
