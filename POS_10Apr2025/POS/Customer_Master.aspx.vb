Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Customer_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsCustomer As New Controller_clsCustomer()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx")
                End If
                oclsBind.BindCountry(radCountry)
                oclsBind.Bindpro(radprofile, Val(Session("cmp_id")))
                oclsBind.BindPrices_By_Cmp(ddlPriceLevel, Val(Session("cmp_id")))

                If Not Session("customer_id") = Nothing Then
                    BindCustomer()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Customer_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindCustomer()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            oclsCustomer.cmp_id = Val(Session("cmp_id"))
            'oclsCustomer.customer_id = Val(Session("customer_id"))
            oclsCustomer.AccountID = Val(Session("customer_id"))

            Dim dtCustomer As DataTable = oclsCustomer.Select()
            If dtCustomer.Rows.Count > 0 Then
                txtFName.Text = dtCustomer.Rows(0)("first_name").ToString
                txtLName.Text = dtCustomer.Rows(0)("last_name").ToString
                txtEmail.Text = dtCustomer.Rows(0)("email").ToString
                txtContact.Text = dtCustomer.Rows(0)("contact_no").ToString
                txtAddress.Text = dtCustomer.Rows(0)("address").ToString
                txtPostal.Text = dtCustomer.Rows(0)("postal_code").ToString
                txtOther_id.Text = dtCustomer.Rows(0)("other_id").ToString
                lblBonus.Text = dtCustomer.Rows(0)("bounus_point").ToString
                txt_cardNumber.Text = dtCustomer.Rows(0)("CardNumber").ToString
                ViewState("balance") = dtCustomer.Rows(0)("Balance")
                txtbalance.Text = ViewState("balance")
                btnblnc.Text = ViewState("balance")
                If dtCustomer.Rows(0)("DateTimeExpiry").ToString <> "" And Not dtCustomer.Rows(0)("DateTimeExpiry").ToString = Nothing Then
                    txtExpDate.SelectedDate = dtCustomer.Rows(0)("DateTimeExpiry").ToString
                End If

                If dtCustomer.Rows(0)("CustomerProfile") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dtCustomer.Rows(0)("CustomerProfile"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                End If


                If Val(dtCustomer.Rows(0)("Is_credit").ToString) = 1 Then
                    chkCredit.Checked = True
                Else
                    chkCredit.Checked = False
                End If

                Try

                    radprofile.ClearSelection()
                    If dtCustomer.Rows(0)("profile_id").ToString <> 0 Then
                        radprofile.SelectedValue = (Val(dtCustomer.Rows(0)("profile_id")))

                    End If

                    radCountry.ClearSelection()
                    If dtCustomer.Rows(0)("country_id").ToString <> 0 Then
                        radCountry.SelectedValue = (Val(dtCustomer.Rows(0)("country_id")))
                        radCountry_SelectedIndexChanged(sender, e)
                    End If
                    radState.ClearSelection()
                    If dtCustomer.Rows(0)("state_id").ToString <> 0 Then
                        radState.SelectedValue = (Val(dtCustomer.Rows(0)("state_id")))
                        radState_SelectedIndexChanged(sender, e)
                    End If
                    radCity.ClearSelection()
                    If dtCustomer.Rows(0)("city_id").ToString <> 0 Then
                        radCity.SelectedValue = (Val(dtCustomer.Rows(0)("city_id")))
                    End If
                    ddlPriceLevel.SelectedValue = Val(dtCustomer.Rows(0)("price_level").ToString())
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
                End Try
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Customer_Master:BindCustomer" + ex.Message)
        End Try

    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("customer_id") = Nothing Then
                Clear()
            Else
                BindCustomer()
            End If
        Catch ex As Exception
            LogHelper.Error("Customer_Master:btnReset_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Customer_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Customer_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try
            txtFName.Text = ""
            txtLName.Text = ""
            txtEmail.Text = ""
            txtContact.Text = ""
            txtAddress.Text = ""
            txtPostal.Text = ""
            txtOther_id.Text = ""
            txtExpDate.SelectedDate = Nothing
            txt_cardNumber.Text = ""
            radprofile.SelectedValue = 0
            radCountry.SelectedValue = 0
            radState.Items.Clear()
            radCity.Items.Clear()
        Catch ex As Exception
            LogHelper.Error("Customer_Master:Clear" + ex.Message)
        End Try

    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsCustomer.cmp_id = Val(Session("cmp_id"))
            oclsCustomer.first_name = txtFName.Text
            oclsCustomer.last_name = txtLName.Text
            oclsCustomer.email = txtEmail.Text
            oclsCustomer.contact_no = txtContact.Text
            oclsCustomer.address = txtAddress.Text
            oclsCustomer.postal_code = txtPostal.Text
            oclsCustomer.profile_id = Val(IIf(radprofile.SelectedValue = "", 0, radprofile.SelectedValue))
            oclsCustomer.country_id = Val(IIf(radCountry.SelectedValue = "", 0, radCountry.SelectedValue))
            oclsCustomer.state_id = Val(IIf(radState.SelectedValue = "", 0, radState.SelectedValue))
            oclsCustomer.city_id = Val(IIf(radCity.SelectedValue = "", 0, radCity.SelectedValue))
            'oclsCustomer.is_active = 1
            'oclsCustomer.ip_address = Request.UserHostAddress
            'oclsCustomer.login_id = Val(Session("login_id"))
            oclsCustomer.other_id = txtOther_id.Text
            'oclsCustomer.machine_id = 0
            oclsCustomer.price_level = Val(ddlPriceLevel.SelectedValue)
            oclsCustomer.AccountID = 0
            oclsCustomer.Is_credit = IIf(chkCredit.Checked = True, 1, 0)
            oclsCustomer.CardNumber = txt_cardNumber.Text

            If txtExpDate.SelectedDate.ToString() <> "" And Not txtExpDate.SelectedDate.ToString() = Nothing Then
                oclsCustomer.ExpDate = txtExpDate.SelectedDate.ToString()
            Else
                oclsCustomer.ExpDate = DateTime.MaxValue.ToString()
                'System.DateTime.Now
            End If

            Dim bytes As Byte()
            If fileupload.HasFile Then
                Using br As BinaryReader = New BinaryReader(fileupload.PostedFile.InputStream)
                    bytes = br.ReadBytes(fileupload.PostedFile.ContentLength)
                End Using
            Else
                bytes = DirectCast(ViewState("logo"), Byte())
                'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                'Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                'hdlogo.Value = Convert.ToBase64String(bytes)
            End If
            oclsCustomer.CustomerProfile = bytes




            If Session("customer_id") = Nothing Then

                Dim dt As DataTable = oclsCustomer.Insert_account()

                If dt.Rows.Count > 0 Then
                    'oclsCustomer.AccountID = Val(dt.Rows(0)("AccountID"))

                    'oclsCustomer.customer_id = 0
                    'oclsCustomer.Insert()

                    Session("Success") = "Record inserted successfully"
                End If

            Else
                oclsCustomer.customer_id = Val(Session("customer_id"))
                oclsCustomer.AccountID = Val(Session("customer_id"))
                'oclsCustomer.Update()

                Dim dt As DataTable = oclsCustomer.Update_account()
                Session("Success") = "Record updated successfully"
            End If
            Session("customer_id") = Nothing
            Response.Redirect("Customer_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Customer_Master:btnSave_Click" + ex.Message)
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
            LogHelper.Error("Customer_Master:radCountry_SelectedIndexChanged" + ex.Message)
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
            LogHelper.Error("Customer_Master:radState_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub btnlist_Click(sender As Object, e As EventArgs)
        Dim customerid = Val(HttpContext.Current.Session("customer_id"))
        Dim startDate As String = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd")
        Dim endDate As String = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd")

        Response.Redirect("Customer_Credit_Report.aspx?customer_id=" + customerid.ToString() + "&start_date=" + startDate + "&end_date=" + endDate)
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function SubmitAmount(ByVal amount As Decimal, ByVal paymentType As Byte) As String
        Try

            InsertAmount(amount, paymentType)
            Return "Record inserted successfully"
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function
    Private Shared Sub InsertAmount(ByVal amount As Decimal, ByVal paymentType As Byte)

        Dim location_id = If(HttpContext.Current.Session("location_id"), 0)
        Dim customerid = Val(HttpContext.Current.Session("customer_id"))
        Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("Update_Credit_acc_Activity", con)
                cmd.CommandType = CommandType.StoredProcedure

                Dim pay_uuid As Guid = Guid.NewGuid()

                cmd.Parameters.AddWithValue("@accountId", customerid)
                cmd.Parameters.AddWithValue("@amount", amount)
                cmd.Parameters.AddWithValue("@machine_id ", 0)
                cmd.Parameters.AddWithValue("@location_id", location_id)
                cmd.Parameters.AddWithValue("@pay_uuid", pay_uuid)
                cmd.Parameters.AddWithValue("@sale_type", paymentType)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub
End Class
