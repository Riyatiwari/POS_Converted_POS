Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.IO

Imports System.Data.SqlClient
Imports System.Net.Mail

Imports System.Security.Authentication
Imports System.Net
Imports System.Net.Configuration
Imports System.Configuration

Imports System.Web.Configuration

Partial Class Staff_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsStaff As New Controller_clsStaff()
    Dim oclsfunction As New Controller_clsFunctionType()
    Dim oclsVenue As Controller_clsVenue = New Controller_clsVenue()
    '  Dim isPopupShown As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then


                txtSCode.Visible = False

                If Session("staff_id") = Nothing Then
                    SetSelectedValuesForLocation(Val(Session("cmp_id")))
                End If
                Dim UserUUID = Guid.NewGuid().ToString
                Session("UserUUID") = UserUUID
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindRole(radRole, Val(Session("cmp_id")))
                oclsBind.BindCountry(radCountry)
                oclsBind.BindVenue(ddlVenue, Val(Session("cmp_id")))
                oclsBind.BindSMaster(ddl_SMaster, Val(Session("cmp_id")))
                'oclsBind.BindFunctionType(chkfunctiontype, Val(Session("cmp_id")))
                'BindFunctionType()
                'BindVenue()
                If Not Session("staff_id") = Nothing Then
                    BindStaff()

                    txtSCode.ReadOnly = True


                End If
                'If Session("staff_id") = Nothing Then
                '    If backhoffice.Checked = False Then
                '        btnSave.Visible = False
                '        Remail.Visible = True
                '    Else
                '        Remail.Visible = False
                '        btnSave.Visible = True
                '    End If
                'End If

                If Session("staff_id") = Nothing Then
                    If backhoffice.Checked Then
                        ' bkoffice.Enabled = True
                        rkemail.Enabled = True
                        reqemail.Visible = True
                        RequiredFieldValidator2.Enabled = True
                        ' srole.Visible = True
                        btnSave.Visible = False
                        Remail.Visible = True
                        role.Visible = True
                        radRole.Visible = True
                        srole.Visible = True
                    Else
                        ' bkoffice.Enabled = False
                        rkemail.Enabled = False
                        reqemail.Visible = False
                        RequiredFieldValidator2.Enabled = False
                        'srole.Visible = False
                        btnSave.Visible = True
                        Remail.Visible = False
                        role.Visible = False
                        radRole.Visible = False
                        srole.Visible = False
                    End If
                Else

                    If backhoffice.Checked Then
                        ' bkoffice.Enabled = True
                        rkemail.Enabled = True
                        reqemail.Visible = True
                        RequiredFieldValidator2.Enabled = True
                        ' srole.Visible = True
                        btnSave.Visible = False
                        'Remail.Visible = True
                        role.Visible = True
                        radRole.Visible = True
                        srole.Visible = True
                    Else
                        ' bkoffice.Enabled = False
                        rkemail.Enabled = False
                        reqemail.Visible = False
                        RequiredFieldValidator2.Enabled = False
                        'srole.Visible = False
                        btnSave.Visible = True
                        'Remail.Visible = False
                        role.Visible = False
                        radRole.Visible = False
                        srole.Visible = False
                    End If

                    If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
                        Remail.Visible = False
                        btnSave.Visible = True
                    Else
                        Remail.Visible = True
                        btnSave.Visible = True
                    End If
                End If

                SetSelectedValuesForLocation(Val(Session("cmp_id")))
                End If

        Catch ex As Exception
            LogHelper.Error("Staff_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub SetSelectedValuesForLocation(cmp_id As Integer)
        Try
            If Session("staff_id") = Nothing Then
                Dim sender As Object
                Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
                oclsStaff.cmp_id = Val(Session("cmp_id"))
                Dim dtStaff As DataTable = oclsStaff.GetCompanyLocation()  ''''''till_code generated

                'Dim dtCompany As DataTable = oclsBind.GetCompanyLocation(cmp_id)

                If dtStaff.Rows.Count > 0 Then
                    Dim selectedCountry As Integer = Convert.ToInt32(dtStaff.Rows(0)("Country"))
                    Dim selectedState As Integer = Convert.ToInt32(dtStaff.Rows(0)("State"))
                    Dim selectedCity As Integer = Convert.ToInt32(dtStaff.Rows(0)("City"))
                    Dim tillCode As String = dtStaff.Rows(0)("tillcode").ToString()


                    radCountry.SelectedValue = selectedCountry
                    radCountry_SelectedIndexChanged(sender, e)

                    radState.SelectedValue = selectedState
                    radState_SelectedIndexChanged(sender, e)
                    radCity.ClearSelection()
                    radCity.SelectedValue = selectedCity

                    If String.IsNullOrWhiteSpace(txtTillCode.Text) Then
                        txtTillCode.Text = tillCode
                        txtSCode.Text = tillCode '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    End If
                End If
            End If
        Catch ex As Exception

            LogHelper.Error("Staff_Master:SetSelectedValuesForLocation: " + ex.Message)
        End Try
    End Sub


    Private Sub showstoreuuidpopup()
        'If Session("storeuuid") Is Nothing OrElse Session("storeuuid").ToString() = "" OrElse Session("storeuuid").ToString().ToLower() = "null" OrElse Session("constoreuuid").ToString() = "" OrElse Session("constoreuuid").ToString().ToLower() = "null" Then

        'pnlstoreuuidpopup.visible = true

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
                showstoreuuidpopup()
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)

        ElseIf Session("StoreUUID").ToString() = "" OrElse Session("StoreUUID").ToString().ToLower() = "null" OrElse Session("StoreUUID").ToString() = "0" Then

                Dim storeUuid As String = Session("conStoreuuid").ToString()
                Dim objclsLogin As New Controller_clsLogin()
                objclsLogin.Store_UUID = storeUuid

                Dim dtlogin As DataTable = objclsLogin.enablelogin()
                Session("StoreUUID") = Session("conStoreuuid")
                showstoreuuidpopup()
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)
        ElseIf (Session("conStoreUUID").ToString()) <> Session("StoreUUID").ToString() Then
                Dim storeUuid As String = Session("conStoreuuid").ToString()
                Dim objclsLogin As New Controller_clsLogin()
                objclsLogin.Store_UUID = storeUuid

                Dim dtlogin As DataTable = objclsLogin.enablelogin()
                Session("StoreUUID") = Session("conStoreuuid")
                showstoreuuidpopup()
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Enabled for Register with Email Successfully');", True)


        End If


        ' isPopupShown = True
        ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "emailerror", "alert('Contact the Support Team');", True)

        ' Else
        ' pnlstoreuuidpopup.visible = false
        '  isPopupShown = False
        'End If
    End Sub
    Private Sub BindStaff()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.staff_id = Val(Session("staff_id"))
            Dim dtStaff As DataTable = oclsStaff.Select()
            If dtStaff.Rows.Count > 0 Then
                txtSCode.Text = dtStaff.Rows(0)("staff_code").ToString
                txtFName.Text = dtStaff.Rows(0)("name").ToString
                txtTDate.SelectedDate = dtStaff.Rows(0)("joining_date").ToString
                radRole.ClearSelection()
                radRole.SelectedValue = Val(dtStaff.Rows(0)("role_id"))
                ddl_SMaster.SelectedValue = Val(dtStaff.Rows(0)("m_staff_id"))
                ViewState("useruuid") = dtStaff.Rows(0)("UserUUID")




                If Val(dtStaff.Rows(0)("till_active")) = 1 Then
                    chkTillActive.Checked = True
                    sTillCode.Visible = True
                    rfTillCode.Enabled = True
                Else
                    chkTillActive.Checked = False
                    sTillCode.Visible = False
                    rfTillCode.Enabled = False
                End If
                If dtStaff.Rows(0)("active") = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                If dtStaff.Rows(0)("trainee") = "Yes" Then
                    chktrainee.Checked = True
                Else
                    chktrainee.Checked = False
                End If
                ViewState("p") = dtStaff.Rows(0)("photo").ToString
                If (ViewState("p").ToString() <> "") Then
                    ImgPrv.Style.Add("display", "block")
                    ImgPrv.ImageUrl = "~/Files/UserProfile/" + dtStaff.Rows(0)("photo")
                Else
                    ImgPrv.Style.Add("display", "none")
                End If
                txtEmail.Text = dtStaff.Rows(0)("email").ToString

                txtContact.Text = dtStaff.Rows(0)("contact_no").ToString
                txtAddress.Text = dtStaff.Rows(0)("address").ToString
                txtTillCode.Text = dtStaff.Rows(0)("till_code").ToString
                'radCountry.SelectedValue = (Val(dtStaff.Rows(0)("country_id")))
                'radState.SelectedValue = (Val(dtStaff.Rows(0)("state_id")))
                'radCity.SelectedValue = (Val(dtStaff.Rows(0)("city_id")))

                '----------------02022022----------------
                'Dim collection As IList(Of RadComboBoxItem) = chkfunctiontype.Items
                'Dim stArr As String() = dtStaff.Rows(0)("Function_id").ToString().Split("#")

                'For i As Integer = 0 To stArr.Length - 1
                '    For Each item As RadComboBoxItem In collection
                '        If stArr(i).ToString = item.Value Then
                '            item.Checked = True
                '            Exit For
                '        End If
                '    Next
                'Next
                '----------------02022022----------------

                '--------------14-09-2021-------------
                radCountry.ClearSelection()
                If dtStaff.Rows(0)("country_id").ToString <> 0 Then
                    radCountry.SelectedValue = (Val(dtStaff.Rows(0)("country_id")))
                    radCountry_SelectedIndexChanged(sender, e)
                End If
                radState.ClearSelection()
                If dtStaff.Rows(0)("state_id").ToString <> 0 Then
                    radState.SelectedValue = (Val(dtStaff.Rows(0)("state_id")))
                    radState_SelectedIndexChanged(sender, e)
                End If
                radCity.ClearSelection()
                If dtStaff.Rows(0)("city_id").ToString <> 0 Then
                    radCity.SelectedValue = (Val(dtStaff.Rows(0)("city_id")))
                End If
                '--------------14-09-2021-------------

                '-----------------------------------------
                'Try

                '    radCountry.ClearSelection()
                '    radCountry.FindItemByValue(Val(dtStaff.Rows(0)("country_id"))).Selected = True
                '    radCountry_SelectedIndexChanged(sender, e)
                '    radState.ClearSelection()
                '    radState.FindItemByValue(Val(dtStaff.Rows(0)("state_id"))).Selected = True
                '    radState_SelectedIndexChanged(sender, e)
                '    radCity.ClearSelection()
                '    radCity.FindItemByValue(Val(dtStaff.Rows(0)("city_id"))).Selected = True
                'Catch ex As Exception
                '    Throw ex
                'End Try
                txtNational.Text = dtStaff.Rows(0)("national_id").ToString
                txtOtherId.Text = dtStaff.Rows(0)("other_id").ToString
                txtAuthentication_Code.Text = dtStaff.Rows(0)("Authentication_Code").ToString()

                '---------venue_id bind 28092022-----------------
                Dim collection As IList(Of RadComboBoxItem) = ddlVenue.Items
                Dim stArr As String() = dtStaff.Rows(0)("venue_id").ToString().Split("#")

                For i As Integer = 0 To stArr.Length - 1
                    For Each item As RadComboBoxItem In collection
                        If stArr(i).ToString = item.Value Then
                            item.Checked = True
                            Exit For
                        End If
                    Next
                Next


            End If
            If IsDBNull(ViewState("useruuid")) Then
                ViewState("useruuid") = Nothing
            End If


            If Not IsDBNull(dtStaff.Rows(0)("role_id")) AndAlso Val(dtStaff.Rows(0)("role_id")) > 0 Then
                backhoffice.Checked = True


                If Not String.IsNullOrWhiteSpace(ViewState("useruuid")) Then
                    Remail.Visible = False
                    btnSave.Visible = True
                    txtEmail.Enabled = False
                Else
                    Remail.Visible = True
                    btnSave.Visible = False
                End If
            Else



                If Not String.IsNullOrWhiteSpace(ViewState("useruuid")) Then
                    Remail.Visible = False
                    btnSave.Visible = True

                Else
                    Remail.Visible = True
                    btnSave.Visible = False

                End If

            End If
            'If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
            '    Remail.Visible = False
            'Else
            '    Remail.Visible = True
            'End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Staff_Master:BindStaff:" + ex.Message)
        End Try

    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("staff_id") = Nothing Then
                Clear()
            Else
                BindStaff()
            End If
        Catch ex As Exception
            LogHelper.Error("Staff_Master:btnReset_Click:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Staff_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Staff_Master:btnCancel_Click:" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try
            txtSCode.Text = ""
            txtFName.Text = ""
            txtTDate.SelectedDate = Nothing
            radRole.SelectedValue = 0
            txtEmail.Text = ""
            txtContact.Text = ""
            txtAddress.Text = ""
            radCountry.SelectedValue = 0
            radState.Items.Clear()
            radCity.Items.Clear()
            txtTillCode.Text = ""
            txtNational.Text = ""
            chkTillActive.Checked = True
            chkActive.Checked = True
            txtOtherId.Text = ""
            txtAuthentication_Code.Text = ""
            chktrainee.Checked = False
        Catch ex As Exception
            LogHelper.Error("Staff_Master:Clear:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim currentDate As DateTime = DateTime.Today
            Dim currentTime As DateTime = DateTime.Now

            Dim f As String
            f = ImgPrv.ImageUrl.ToString()
            If RadUpload1.UploadedFiles.Count > 0 Then
                Dim fileName As String = System.DateTime.Now.ToString().Replace("\", "").Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ", "").ToString & RadUpload1.UploadedFiles(0).FileName.ToString
                Dim fileLocation As String = Server.MapPath("~/Files/UserProfile/") + fileName
                Dim fileExtension As String = RadUpload1.UploadedFiles(0).GetExtension.ToString
                RadUpload1.UploadedFiles(0).SaveAs(fileLocation, True)
                ViewState("p") = fileName
            End If

            If (ViewState("p") = Nothing) Then
                oclsStaff.photo = ""
            Else
                ImgPrv.Visible = True
                oclsStaff.photo = ViewState("p").ToString()
            End If

            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.staff_code = txtSCode.Text.Trim()
            oclsStaff.name = txtFName.Text.Trim()
            oclsStaff.till_active = IIf(chkTillActive.Checked = True, 1, 0)
            ' oclsStaff.joining_date = txtTDate.SelectedDate.ToString()
            Dim formattedDateTime As String = currentDate.ToString("dd/MM/yyyy") + " " + currentTime.ToString("HH:mm")
            hiddenFieldDate.Value = formattedDateTime
            oclsStaff.joining_date = currentDate
            oclsStaff.branch_id = Val(0)
            oclsStaff.department_id = Val(0)
            oclsStaff.designation_id = Val(0)
            oclsStaff.role_id = Val(radRole.SelectedValue)
            oclsStaff.Function_type_id = "" 'GetSelectedValue(chkfunctiontype)
            oclsStaff.email = txtEmail.Text
            oclsStaff.contact_no = txtContact.Text
            Dim c As String = ""
            If txtTillCode.Text = "" Then
                Dim bytes = New Byte(3) {}
                Dim rng = RandomNumberGenerator.Create()
                rng.GetBytes(bytes)
                'Dim random As UInteger = BitConverter.ToUInt32(bytes, 0) Mod 100000000
                Dim random As UInteger = BitConverter.ToUInt32(bytes, 0) Mod 10000

                oclsStaff.till_code = random
            Else
                oclsStaff.till_code = (txtTillCode.Text)
            End If

            oclsStaff.address = txtAddress.Text
            oclsStaff.country_id = Val(IIf(radCountry.SelectedValue = "", 0, radCountry.SelectedValue))
            oclsStaff.state_id = Val(IIf(radState.SelectedValue = "", 0, radState.SelectedValue))
            oclsStaff.city_id = Val(IIf(radCity.SelectedValue = "", 0, radCity.SelectedValue))
            oclsStaff.national_id = txtNational.Text
            oclsStaff.last_working_date = " 1/1/1990 12:00:00 AM "
            oclsStaff.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsStaff.ip_address = Request.UserHostAddress
            oclsStaff.other_id = txtOtherId.Text
            oclsStaff.login_id = Val(Session("login_id"))
            oclsStaff.machine_id = 0
            oclsStaff.Authentication_Code = txtAuthentication_Code.Text
            oclsStaff.is_trainee = IIf(chktrainee.Checked = True, 1, 0)
            oclsStaff.m_staff_id = Val(ddl_SMaster.SelectedValue)
            oclsStaff.venue_id = GetSelectedValue_new(ddlVenue)

            Session("VenueID") = GetSelectedValue_new(ddlVenue)

            If Session("staff_id") = Nothing Then
                oclsStaff.staff_id = 0
                oclsStaff.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsStaff.staff_id = Val(Session("staff_id"))
                oclsStaff.Update()
                If Session("staff_id_login") = Session("staff_id") Then
                    Session("Photo") = ViewState("p")
                    Session("staff_name") = txtFName.Text
                End If

                Session("Success") = "Record updated successfully"

            End If
            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim n As String = txtEmail.Text.Trim()
            Dim cmd As SqlCommand = New SqlCommand("P_M_userregister", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Username", n.ToString)
            cmd.Parameters.AddWithValue("@password", Encrypt("not4any1"))
            cmd.Parameters.AddWithValue("@UserUUID", Session("UserUUID"))
            cmd.Parameters.AddWithValue("@StoreUUID", Session("StoreUUID"))
            cmd.Parameters.AddWithValue("@Alias", 4182)
            cmd.Parameters.AddWithValue("@is_active", IIf(chkActive.Checked = True, 1, 0))
            cmd.ExecuteNonQuery()


            'Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            'strcon.Open()

            'Dim cmd As SqlCommand = New SqlCommand("P_M_userRegister", strcon)
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("Is_active", IIf(chkActive.Checked = True, 1, 0))
            'cmd.Parameters.AddWithValue("@StoreUUID", Session("StoreUUID"))
            'cmd.ExecuteNonQuery()
            Session("staff_id") = Nothing
            Response.Redirect("Staff_List.aspx", False)




        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Staff_Master:btnSave_Click:" + ex.Message)
        Finally
            If ViewState("p") <> "" Then
                ImgPrv.ImageUrl = "~/Files/UserProfile/" + ViewState("p")
            End If
        End Try
        If (ImgPrv.ImageUrl = "") Then
            ImgPrv.Style.Add("display", "none")
        Else
            ImgPrv.Style.Add("display", "block")
        End If
    End Sub


    Protected Sub chkTillActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkTillActive.CheckedChanged
        Try
            If chkTillActive.Checked = True Then
                rfTillCode.Enabled = True
                sTillCode.Visible = True

            Else
                rfTillCode.Enabled = False
                sTillCode.Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Staff_Master:chkTillActive_CheckedChanged:" + ex.Message)
        End Try

    End Sub


    Protected Sub radCountry_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radCountry.SelectedIndex = 0 Then
                radState.Items.Clear()
                radCity.Items.Clear()
            Else
                oclsBind.BindState(radState, radCountry.SelectedValue)
                radCity.Items.Clear()
            End If
        Catch ex As Exception
            LogHelper.Error("Staff_Master:radCountry_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radState_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radState.SelectedIndex = 0 Then
                radCity.Items.Clear()
            Else
                oclsBind.BindCity(radCity, radState.SelectedValue)
            End If
        Catch ex As Exception
            LogHelper.Error("Staff_Master:radState_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    'Protected Sub chkfunctiontype_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim checkbox_id As CheckBox = DirectCast(e.Row.FindControl("chk_functiontype"), CheckBox)
    '            Dim functiontype_id As HiddenField = CType(e.Row.FindControl("hdfunctiontype"), HiddenField)

    '            oclsStaff.cmp_id = Val(Session("cmp_id"))
    '            oclsStaff.staff_id = Val(Session("staff_id"))
    '            Dim dtStaff As DataTable = oclsStaff.Select()
    '            If dtStaff.Rows.Count > 0 Then
    '                'Dim collection As IList(Of RadComboBoxItem) = checkbox_id

    '                Dim stArr As String() = dtStaff.Rows(0)("Function_id").ToString().Split("#")

    '                For i As Integer = 0 To stArr.Length - 1
    '                    'For Each item As RadComboBoxItem In Collection
    '                    '    If stArr(i).ToString = item.Value Then
    '                    '        item.Checked = True
    '                    '        Exit For
    '                    '    End If
    '                    'Next
    '                Next
    '            End If

    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub



    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As EventArgs)

        If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
            Remail.Visible = False
        Else
            Remail.Visible = True
        End If
    End Sub
    Protected Sub Remail_Click(sender As Object, e As EventArgs) Handles Remail.Click

        Try


            '''''''''''''''''''''''''''''''''21122023


            oclsStaff.email = txtEmail.Text

            Dim dt As String
            dt = txtEmail.Text
            If dt.Count > 0 Then


                showstoreuuidpopup()


                Dim f As String
                f = ImgPrv.ImageUrl.ToString()
                If RadUpload1.UploadedFiles.Count > 0 Then
                    Dim fileName As String = System.DateTime.Now.ToString().Replace("\", "").Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ", "").ToString & RadUpload1.UploadedFiles(0).FileName.ToString
                    Dim fileLocation As String = Server.MapPath("~/Files/UserProfile/") + fileName
                    Dim fileExtension As String = RadUpload1.UploadedFiles(0).GetExtension.ToString
                    RadUpload1.UploadedFiles(0).SaveAs(fileLocation, True)
                    ViewState("p") = fileName
                End If

                If (ViewState("p") = Nothing) Then
                    oclsStaff.photo = ""
                Else
                    ImgPrv.Visible = True
                    oclsStaff.photo = ViewState("p").ToString()
                End If


                Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                strcon.Open()
                Dim n As String = txtEmail.Text.Trim()
                Dim cmd As SqlCommand = New SqlCommand("P_M_userRegister", strcon)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Username", n.ToString)
                cmd.Parameters.AddWithValue("@password", Encrypt("not4any1"))
                cmd.Parameters.AddWithValue("@UserUUID", Session("UserUUID"))
                cmd.Parameters.AddWithValue("@StoreUUID", Session("StoreUUID"))
                cmd.Parameters.AddWithValue("@Alias", 4182)
                cmd.Parameters.AddWithValue("@is_active", IIf(chkTillActive.Checked = True, 1, 0))
                cmd.ExecuteNonQuery()
                Dim newUserUUID As String = cmd.Parameters("@UserUUID").Value.ToString()
                Dim oldUserUUID As String = Session("UserUUID").ToString()
                Dim currentDate As DateTime = DateTime.Today


                If oldUserUUID <> newUserUUID Then

                    Session("UserUUID") = newUserUUID
                End If

                'Dim strconn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
                'strconn.Open()

                'Dim cmdd As SqlCommand = New SqlCommand("P_M_userActive", strconn)
                'cmdd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.AddWithValue("@Username", n.ToString)
                ''cmd.Parameters.AddWithValue("@password", Encrypt("not4any1"))
                'cmdd.Parameters.AddWithValue("@UserUUID", Session("UserUUID"))
                'cmdd.Parameters.AddWithValue("@StoreUUID", Session("StoreUUID"))
                ''cmd.Parameters.AddWithValue("@Alias", 4182)
                'cmdd.Parameters.AddWithValue("@is_active", IIf(chkActive.Checked = True, 1, 0))
                'cmdd.ExecuteNonQuery()
                '' Session("Success") = "Record updated successfully"



                oclsStaff.cmp_id = Val(Session("cmp_id"))
                oclsStaff.staff_code = txtSCode.Text.Trim()
                oclsStaff.name = txtFName.Text.Trim()
                oclsStaff.till_active = IIf(chkTillActive.Checked = True, 1, 0)
                ' oclsStaff.joining_date = txtTDate.SelectedDate.ToString()
                hiddenFieldDate.Value = currentDate.ToString("dd/MM/yyyy")
                oclsStaff.joining_date = currentDate

                oclsStaff.branch_id = Val(0)
                oclsStaff.department_id = Val(0)
                oclsStaff.designation_id = Val(0)
                oclsStaff.role_id = Val(radRole.SelectedValue)
                oclsStaff.Function_type_id = "" 'GetSelectedValue(chkfunctiontype)
                oclsStaff.email = txtEmail.Text
                oclsStaff.contact_no = txtContact.Text
                oclsStaff.UserUUID = Session("UserUUID")
                Dim c As String = ""
                If txtTillCode.Text = "" Then
                    Dim bytes = New Byte(3) {}
                    Dim rng = RandomNumberGenerator.Create()
                    rng.GetBytes(bytes)
                    'Dim random As UInteger = BitConverter.ToUInt32(bytes, 0) Mod 100000000
                    Dim random As UInteger = BitConverter.ToUInt32(bytes, 0) Mod 10000

                    oclsStaff.till_code = random
                Else
                    oclsStaff.till_code = (txtTillCode.Text)
                End If




                oclsStaff.address = txtAddress.Text
                oclsStaff.country_id = Val(IIf(radCountry.SelectedValue = "", 0, radCountry.SelectedValue))
                oclsStaff.state_id = Val(IIf(radState.SelectedValue = "", 0, radState.SelectedValue))
                oclsStaff.city_id = Val(IIf(radCity.SelectedValue = "", 0, radCity.SelectedValue))
                oclsStaff.national_id = txtNational.Text
                oclsStaff.last_working_date = " 1/1/1990 12:00:00 AM "
                oclsStaff.is_active = IIf(chkActive.Checked = True, 1, 0)
                oclsStaff.ip_address = Request.UserHostAddress
                oclsStaff.other_id = txtOtherId.Text
                oclsStaff.login_id = Val(Session("login_id"))
                oclsStaff.machine_id = 0
                oclsStaff.Authentication_Code = txtAuthentication_Code.Text
                oclsStaff.is_trainee = IIf(chktrainee.Checked = True, 1, 0)
                oclsStaff.m_staff_id = Val(ddl_SMaster.SelectedValue)
                oclsStaff.venue_id = GetSelectedValue_new(ddlVenue)
                oclsStaff.cmp_id = Val(Session("cmp_id"))
                oclsStaff.staff_id = Val(Session("staff_id"))

                oclsStaff.staff_id = Val(Session("staff_id"))
                oclsStaff.password = "not4any1"


                If Session("staff_id") = Nothing Then
                    oclsStaff.staff_id = 0
                    oclsStaff.RegisterWithMail_insert()
                    Session("Success") = "Record inserted successfully"
                Else
                    oclsStaff.staff_id = Val(Session("staff_id"))
                    oclsStaff.RegisterWithMail()
                    If Session("staff_id_login") = Session("staff_id") Then
                        Session("Photo") = ViewState("p")
                        Session("staff_name") = txtFName.Text
                    End If
                    Session("Success") = "Record updated successfully"

                End If

                If Session("staff_id_login") = Session("staff_id") Then
                    Session("Photo") = ViewState("p")
                    Session("staff_name") = txtFName.Text
                End If
                Session("Success") = "Record updated successfully"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "emailError", "alert('Email cannot be null or empty.');", True)
 
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''sent email
                If txtEmail.Text IsNot "" Then



                    Dim resetlink As String = GeneratePasswordResetLink()

                    Dim resetpasswordmessage As String = "To access TenderPOS you ill need to set your password to click <a href='" & resetlink & "'>here</a>"
                    Dim signinlink As String = "  <a href='http://live.mytenderpos.com/User_Access.aspx'> Sign In</a>"

                    Dim congratulatorymessage As String = "Welcome to TenderPOS and congratulations🎉 on your successful registration!"
                    Dim loginbackofficemessage As String = " Once you have set your password click here 🔜" & signinlink & "To Sign into your Account"
                    Dim thankyoumessage As String = "Thank you For choosing our service! "
                    Dim emailbody As String = congratulatorymessage & "<br/><br/>" & resetpasswordmessage & "<br/><br/>" & loginbackofficemessage & "<br/><br/>" & thankyoumessage


                    Dim email As String
                    Dim Subject As String

                    email = txtEmail.Text.ToString()

                    'madhvanimitesh@gmail.com
                    Subject = "User Registration"

                    LogHelper.Error("staff_Master:email_id:" & email)
                    MailTo_receipt(email, Subject, emailbody, "", "", "")




                Else
                   
                    txtEmail.Focus()
                    Return

                End If

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''sent email
                If Session("staff_id") = Nothing Then

                    Session("Success") = "Record inserted successfully"
                Else


                    Session("Success") = "Record updated successfully"

                End If


                ScriptManager.RegisterStartupScript(Me, Me.GetType, "emailError", "alert('Successfully register with Email.');", True)
                Session("staff_id") = Nothing
                Response.Redirect("Staff_List.aspx", False)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "emailError", "alert('Email should not be null or empty.');", True)
            End If


        Catch ex As Exception
            LogHelper.Error("Staff_Master:Remail_Click:" + ex.Message)
            Dim errorMessage As String = "An error occurred: " + ex.Message
            Dim script As String = "alert('" + errorMessage + "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowError", script, True)
        End Try

    End Sub


    Private Function GeneratePasswordResetLink() As String
        Dim encryptedEmail As String = txtEmail.Text
        ' Session("email") = Encrypt(txtEmail.Text.ToString())

        Dim encryptedStoreUuid As String = Session("StoreUUID").ToString()

        Dim expirationTime As DateTime = DateTime.Now.AddMinutes(29)
        'Dim expirationTime As DateTime = DateTime.Now.AddMinutes(30)


        Dim encryptvalue As DateTime = expirationTime.ToString("yyyy-MM-dd HH:mm:ss")

        Dim encryptedExpirationTime As DateTime = encryptvalue





        'Return "http://testing.mytenderpos.com/Reset_Password.aspx?email=" & Encrypt(txtEmail.Text.ToString()) & "&storeuuid=" & Encrypt(Session("StoreUUID"))

        'Return "http://testing.mytenderpos.com/mailReset_Password.aspx?el=" & encryptedEmail & "&sid=" & encryptedStoreUuid
        Return "http://live.mytenderpos.com/mailReset_Password.aspx?el=" & encryptedEmail & "&sid=" & encryptedStoreUuid & "&exp=" & encryptedExpirationTime
    End Function

    Public Sub MailTo_receipt(ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        'Dim dtMailDetail As DataTable = oClsDataccess.Getdatatable("select * from S_Email_Settings where Company_id = " + CmpID.ToString() + " and location_id = " + LocationID.ToString())
        'If dtMailDetail.Rows.Count > 0 Then
        '    MailServer = dtMailDetail.Rows(0)("MailServer")
        '    MailServer_UserName = dtMailDetail.Rows(0)("MailServer_UserName")
        '    MailServer_Password = dtMailDetail.Rows(0)("MailServer_Password")
        '    MailServer_Port = dtMailDetail.Rows(0)("Port")
        '    From_Email = dtMailDetail.Rows(0)("From_Email")
        '    Ssl = IIf(dtMailDetail.Rows(0)("ssl") = "1", True, False)
        '    alas = dtMailDetail.Rows(0)("Alias")
        'Else
        Dim configurationFile As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
        Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")
        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Smtp.Network.Port
            MailServer = mailSettings.Smtp.Network.Host
            MailServer_Password = mailSettings.Smtp.Network.Password
            MailServer_UserName = mailSettings.Smtp.Network.UserName
            From_Email = mailSettings.Smtp.From
            Ssl = True
            alas = "TenderPOS"
        End If
        'End If

        LogHelper.Error("Base_class: 1:" + MailServer_Port.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_Password.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_UserName.ToString())
        LogHelper.Error("Base_class: 1:" + From_Email.ToString())
        Try

            '  LogHelper.Error("Base_class: 1:" + System.DateTime.Now.ToString())
            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            '    LogHelper.Error("Base_class: 2:" + System.DateTime.Now.ToString())
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            'LogHelper.Error("Base_class: 3:" + System.DateTime.Now.ToString())
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network


            ' LogHelper.Error("Base_class: 4:" + System.DateTime.Now.ToString())
            oSmtpclient.Send(oE_Mail)
            'LogHelper.Error("Base_class: 5:" + System.DateTime.Now.ToString())
        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub

    Protected Sub backhoffice_CheckedChanged(sender As Object, e As EventArgs) Handles backhoffice.CheckedChanged

        If backhoffice.Checked Then
                rkemail.Enabled = True
                radRole.Enabled = True
                reqemail.Visible = True
                RequiredFieldValidator2.Enabled = True

                btnSave.Visible = False
                Remail.Visible = True
                role.Visible = True
                radRole.Visible = True
                srole.Visible = True
            Else
                radRole.Enabled = False
                rkemail.Enabled = False
                reqemail.Visible = False
                RequiredFieldValidator2.Enabled = False

                btnSave.Visible = True
                Remail.Visible = False
                role.Visible = False
                radRole.Visible = False
                srole.Visible = False
            End If
        If Session("staff_id") > 0 Then
            If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
                Remail.Visible = False
                btnSave.Visible = True
            Else
                Remail.Visible = True
            End If
        End If



    End Sub

    Protected Sub forpos_CheckedChanged(sender As Object, e As EventArgs) Handles forpos.CheckedChanged
        If forpos.Checked Then
            rkrules.Enabled = True
            srules.Visible = True

        Else
            rkrules.Enabled = False
            srules.Visible = False

        End If


    End Sub

End Class
