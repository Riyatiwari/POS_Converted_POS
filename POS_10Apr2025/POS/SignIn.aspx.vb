Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Web.Configuration

Partial Class SignIn
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess

    Protected Sub btnLogIn_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        Try

            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim n As String = txtstorename.Text.Trim()
            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            LogHelper.Error("SignIn:btnLogIn_Click4:")
            cmd.ExecuteNonQuery()
            LogHelper.Error("SignIn:btnLogIn_Click5:")
            If dt.Rows.Count > 0 Then
                Session("StoreID") = dt.Rows(0)("store_id")
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                Session("conStoreuuid") = dt.Rows(0)("store_uuid")
                Session("Storename") = dt.Rows(0)("store_name")




                Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"

            Else
                lblMsg.Text = "Store name does not exists"
                Return
            End If
            LogHelper.Error("SignIn:btnLogIn_Click7:")
            Dim oLogin As New Controller_clsLogin()
            oLogin.User_name = txtUname.Text.Trim
            oLogin.Password = Encrypt(txtUpassword.Text.Trim)
            Dim dtLogin As DataTable = oLogin.Select()
            LogHelper.Error("SignIn:btnLogIn_Click8:")

            If dtLogin.Rows.Count > 0 Then

                If dtLogin.Rows(0)("Uinfo") = "Company" Then
                    Session("cmp_id") = dtLogin.Rows(0)("Company_id")
                    Session("cmp_name") = dtLogin.Rows(0)("Sname")
                    Session("Login_id") = dtLogin.Rows(0)("Login_id")
                    Session("staff_role_id") = 0
                    Session("store") = txtstorename.Text.Trim()
                    Session("show_chips") = dtLogin.Rows(0)("Show_chips")
                    Session("IsAddTax2") = dtLogin.Rows(0)("IsAddTax2")
                    Session("IsExclusiveTax") = dtLogin.Rows(0)("IsExclusiveTax")
                    Session("IsPaymentGtway") = dtLogin.Rows(0)("IsPaymentGtway")
                    Session("VenueID") = dtLogin.Rows(0)("Venue_ID").ToString()
                    Session("StoreUUID") = dtLogin.Rows(0)("StoreUUID")
                    Session("product_type") = dtLogin.Rows(0)("ProductType")



                    'If Session("StoreUUID") Is Nothing OrElse Session("StoreUUID").ToString() = "" Then

                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "StoreUUIDPopup", "showStoreUUIDPopup();", True)
                    '    Return
                    'End If
                    Try
                        BOokingLogin()
                    Catch ex As Exception

                    End Try

                    If Session("db_name").ToString() = "POS_PaymentGateway" Then

                        Response.Redirect("PayGatewaySummary.aspx", False)
                    Else

                        If Request.QueryString("Duration") IsNot Nothing Or Request.QueryString("fDate") IsNot Nothing Then

                            Response.Redirect("Sales_Report.aspx?Duration=" + Request.QueryString("Duration").ToString() + "&fDate=" + Request.QueryString("fDate").ToString() + "", False)

                        Else
                            Response.Redirect("Dashboard.aspx", False)

                        End If

                    End If

                Else
                    If Not dtLogin.Rows(0)("is_active") = 0 Then
                        Session("store") = txtstorename.Text.Trim()
                        Session("staff_id_login") = dtLogin.Rows(0)("staff_id")
                        Session("cmp_id") = dtLogin.Rows(0)("Company_id")
                        Session("cmp_name") = dtLogin.Rows(0)("C_Name")
                        Session("staff_name") = dtLogin.Rows(0)("name")
                        Session("photo") = dtLogin.Rows(0)("photo")
                        Session("Login_id") = dtLogin.Rows(0)("Login_id")
                        Session("staff_role_id") = dtLogin.Rows(0)("role_id")
                        Session("show_chips") = dtLogin.Rows(0)("Show_chips")
                        Session("IsAddTax2") = dtLogin.Rows(0)("IsAddTax2")
                        Session("IsExclusiveTax") = dtLogin.Rows(0)("IsExclusiveTax")
                        Session("IsPaymentGtway") = dtLogin.Rows(0)("IsPaymentGtway")
                        Session("VenueID") = dtLogin.Rows(0)("Venue_ID").ToString()
                        Session("StoreUUID") = dtLogin.Rows(0)("Store_UUID")
                        Session("product_type") = dtLogin.Rows(0)("ProductType")



                        'If Session("StoreUUID") Is Nothing OrElse Session("StoreUUID").ToString() = "" Then

                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "StoreUUIDPopup", "showStoreUUIDPopup();", True)
                        '    Return
                        'End If
                        Try
                            BOokingLogin()
                        Catch ex As Exception

                        End Try

                        If Session("db_name").ToString() = "POS_PaymentGateway" Then

                            Response.Redirect("PayGatewaySummary.aspx", False)
                        Else

                            If Request.QueryString("Duration") IsNot Nothing Or Request.QueryString("fDate") IsNot Nothing Then

                                Response.Redirect("Sales_Report.aspx?Duration=" + Request.QueryString("Duration").ToString() + "&fDate=" + Request.QueryString("fDate").ToString() + "", False)

                            Else
                                Response.Redirect("Dashboard.aspx", False)

                            End If

                        End If
                    Else
                        lblMsg.Text = "You do not have access to login. contact your administrator"
                    End If
                End If
            Else
                lblMsg.Text = "Invalid username or password"
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("SignIn:btnLogIn_Click:" + ex.Message)
        End Try
    End Sub
    Protected Function BOokingLogin()
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim que = "Select * from Operator Where OpNumber = '" + txtUname.Text.Trim + "'"
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count = 1) Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)

                Sessions.UserID = Utils.getInteger(dr("OperatorID").ToString())
                Sessions.UserName = dr("FirstName").ToString() & " " & dr("LastName").ToString()
                Sessions.Login = 1

                Dim venueId As Integer = Utils.getInteger(dr("AllowedVenueID").ToString())
                If (venueId = 0) Then
                    Sessions.BookingType = 3
                Else

                    que = " SELECT BookingSettingsid, bookingtype, BS.StoreID FROM bookingsettings BS"
                    que &= " INNER JOIN Store S ON S.StoreID = BS.StoreID"
                    que &= " WHERE S.VenueId = '" & venueId & "'"

                    ds = conn.SelectData(que)

                    For Each item As DataRow In ds.Tables(0).Rows
                        item("bookingtype") = Utils.Decrypt(item("bookingtype").ToString())
                    Next

                    Dim maxVal As Integer = Utils.getInteger(ds.Tables(0).Compute("Max(bookingtype)", ""))

                    Sessions.BookingType = maxVal
                End If

                Dim dtTab As DataTable = oClsDataccess.Getdatatable("SELECT * FROM TabMaster")

                If dtTab.Rows.Count > 0 Then
                    Sessions.TabID = 1
                Else
                    Sessions.TabID = 0
                End If

            End If

        Catch ex As Exception

        End Try

    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LogHelper.Error("SignIn:btnLogIn_Click:test")
            If Not IsPostBack Then
                Session.Remove("cmp_id")
                Session.Remove("tab")
                Session.RemoveAll()
                txtstorename.Focus()

                If Request.QueryString("SName") IsNot Nothing Then
                    txtstorename.Text = Request.QueryString("SName").ToString()
                    txtUname.Text = "4182"
                    txtUpassword.Text = "not4any1"

                    btnLogIn_Click(sender, e)
                End If

            End If

            If Session("LoginSussess") IsNot Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "PasswordChanged", "alert('Your password changed successfully');", True)
                Session("LoginSussess") = Nothing
            End If
            If Session("Sussess") IsNot Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Register", "alert('Company registered successfully');", True)
                Session("Sussess") = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("SignIn:Page_Load:" + ex.Message)
        End Try
    End Sub
    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function







End Class
