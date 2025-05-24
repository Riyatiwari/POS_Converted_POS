Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.ApplicationServices
Imports Telerik.Web.UI

Partial Class Switch_Account
    Inherits BaseClass

    Dim oClsDataccess As New ClsDataccess



    Public Class UserEntry

        Public Property store_name As String
        Public Property store_UUID As String
        Public Property username As String
    End Class
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                Dim username = Session("UserEmail")



                Dim userEntries As List(Of UserEntry) = GetUsers(username)

                If userEntries.Count > 0 Then
                    rdstaff.DataSource = userEntries
                    rdstaff.DataBind()
                Else

                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Page_Load:Switch_Account:" + ex.Message)
        End Try
    End Sub

    Private Function GetUsers(username As String)
        Dim userEntries As New List(Of UserEntry)()

        Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

        Using cmd As New SqlCommand("GetUserEntriesInfo", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Email", username)

            strcon.Open()

            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim entry As New UserEntry()


                    entry.store_name = reader("store_name").ToString()
                    entry.store_UUID = reader("Store_UUID").ToString()
                    'Session("storename_forswitch") = entry.store_name
                    'entry.storeuuid = reader("store_uuid").ToString()
                    userEntries.Add(entry)
                End While
            End Using
        End Using

        Return userEntries
    End Function



    Protected Function BOokingLogin()
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim que = "Select * from Operator Where OpNumber = '" + Session("UserEmail") + "'"
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

                    'que = " SELECT BookingSettingsid, bookingtype, BS.StoreID FROM bookingsettings BS"
                    'que &= " INNER JOIN Store S ON S.StoreID = BS.StoreID"
                    'que &= " WHERE S.VenueId = '" & venueId & "'"

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

    Protected Sub btnSendMail_Click(sender As Object, e As EventArgs)
        Try

            Dim builder As New StringBuilder
            Dim email As String
            Dim Subject As String


            If Session("dt1") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("dt1"), DataTable)

                builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                builder.Append("<div style='width:70%; color:#000000; font-family:verdana;'>")

                builder.Append("<table style='width:100%;height:150px; color:#000000;margin:5px;font-family:verdana;'>")
                builder.Append("<tr><td>")
                builder.Append("Hello Support Team, ")
                builder.Append("</td></tr>")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr><td>")
                builder.Append("Sync Issue")
                builder.Append("</td></tr>")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr><td><table border=1 width='80%' style='font-family:verdana;font-size:13px;border-collapse: collapse;'>")
                builder.Append("<tr><td width='30%'><b>Issue Date</b></td> <td><b>Description</b></td></tr>")

                For index = 0 To dt.Rows.Count - 1
                    builder.Append("<tr><td width='40%'>" + dt.Rows(index)("for_date").ToString() + "</td>")
                    builder.Append("<td>" + dt.Rows(index)("sync_desc").ToString() + "</td></tr>")
                Next

                builder.Append("</table></td></tr> ")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr style='height: 20px;'><td> <b>Thanks, </b> <br/> TenderPOS. </td></tr>")

                builder.Append("</table>")

                builder.Append("</div>")
                builder.Append("</body> </html>")

                'email = "developer@technometrics.in"
                email = "support@tenderpos.com"
                'madhvanimitesh@gmail.com , mitesh.m@technometrics.in
                Subject = Session("cmp_name").ToString() + "Record Sync Status"

                MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, builder.ToString(), "", "mitesh@tenderpos.com", "")

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Mail sent successfully.');", True)

                Session("dt1") = Nothing
            End If


        Catch ex As Exception
            LogHelper.Error("Switch_Account:btnSendMail_Click:" + ex.Message)
        End Try
    End Sub



    Protected Sub switchAccount_Click(sender As Object, e As EventArgs)
        Try

            Dim clickedButton As LinkButton = DirectCast(sender, LinkButton)
            Dim container As RepeaterItem = DirectCast(clickedButton.NamingContainer, RepeaterItem)
            Dim storeUUID As String = DirectCast(container.FindControl("storeUUIDLabel"), Label).Text
            ' Store the selected store name in the session
            Session("constoreuuid") = storeUUID


            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim n As String = Session("UserEmail")

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_switchAccount", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Username", n.ToString)
            cmd.Parameters.AddWithValue("@store_UUID", Session("constoreuuid"))


            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            LogHelper.Error("SignIn:btnLogIn_Click4:")
            cmd.ExecuteNonQuery()
            LogHelper.Error("SignIn:btnLogIn_Click5:")


            If dt.Rows.Count > 0 Then
                Session("storeid") = dt.Rows(0)("store_id")
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("Storename") = dt.Rows(0)("store_name")
                Session("db_name") = dt.Rows(0)("db_name")
                Session("connectionstring") = "data source=" + dt.Rows(0)("db_server") + ";initial catalog=" + dt.Rows(0)("db_name") + ";user id=" + dt.Rows(0)("db_username") + ";password=" + Session("password") + ";"
            Else
                'lblMsg.Text = "incorrect username or password"
                Return

            End If
            LogHelper.Error("User_Access:btnLogIn_Click7:")
            Dim oLogin As New Controller_clsLogin()
            oLogin.User_name = Session("UserEmail")

            Dim dtLogin As DataTable = oLogin.SelectUser()
            LogHelper.Error("User_Access:btnLogIn_Click8:")

            If dtLogin.Rows.Count > 0 Then

                If dtLogin.Rows(0)("Uinfo") = "Company" Then
                    Session("cmp_id") = dtLogin.Rows(0)("Company_id")
                    Session("cmp_name") = dtLogin.Rows(0)("Name")
                    Session("Login_id") = dtLogin.Rows(0)("Login_id")
                    Session("staff_role_id") = 0
                    'Session("store") = txtstorename.Text.Trim()
                    Session("show_chips") = dtLogin.Rows(0)("Show_chips")
                    Session("IsAddTax2") = dtLogin.Rows(0)("IsAddTax2")
                    Session("IsExclusiveTax") = dtLogin.Rows(0)("IsExclusiveTax")
                    Session("IsPaymentGtway") = dtLogin.Rows(0)("IsPaymentGtway")
                    Session("VenueID") = dtLogin.Rows(0)("Venue_ID").ToString()
                    Session("product_type") = dtLogin.Rows(0)("ProductType")
                    Session("StoreUUID") = dtLogin.Rows(0)("store_uuid")
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
                        'Session("store") = txtstorename.Text.Trim()
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
                        Session("product_type") = dtLogin.Rows(0)("ProductType")
                        Session("StoreUUID") = dtLogin.Rows(0)("store_uuid")
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

                    End If
                End If
            Else

            End If
        Catch ex As Exception
            LogHelper.Error("Switch_Account:switchAccount_Click:" + ex.Message)
        End Try
    End Sub
End Class
