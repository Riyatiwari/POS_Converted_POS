Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Class BookingEasy_RoomBookConfime
    Inherits System.Web.UI.Page

    Public ReadOnly Property CurrencySymbol() As String
        Get
            Dim ds As DataSet = Common.GetXmlData()
            If Not ds.Tables("Currency") Is Nothing Then
                Return DirectCast(ds.Tables("Currency").Rows(0)("Symbol").ToString(), String)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("BookingRef")) Then
                Return Request.QueryString("BookingRef").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing
        If Not IsPostBack Then
            If (Not String.IsNullOrEmpty(BookingRef)) Then
                If Session("amount_final_room_new") = Nothing Then
                    Response.Redirect("Dashboard.aspx")
                Else
                    ConfirmBooking()
                    BindDetails()
                End If
                If Sessions.UserID > 0 Then
                    aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
                Else
                    aHome.HRef = "~/Booking/RoomBookingWidget.aspx"
                End If
            Else
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Private Sub BindDetails()
        Dim objCommon As Common = New Common()
        Dim drBooking As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
        lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
        lblEmail.Text = drBooking("Email1st").ToString()
        lblMobile.Text = drBooking("Mobile").ToString()
        lblBookingRef.Text = BookingRef
        lblName.Text = drBooking("Name").ToString()
        Dim arrival As DateTime = Convert.ToDateTime(drBooking("arrivaldate").ToString())
        Dim depature As DateTime = Convert.ToDateTime(drBooking("departuredate").ToString())
        lblNumberOfNights.Text = DateDiff(DateInterval.Day, arrival, depature).ToString()
        lblArrival.Text = arrival.ToString("dd/MM/yyyy")
        lblDepature.Text = depature.ToString("dd/MM/yyyy")
        lblTotalPrice.Text = drBooking("bookingtotal").ToString()

        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptServices.DataSource = dsServices
            rptServices.DataBind()
            rptServices.Visible = True
        Else
            rptServices.Visible = False
        End If
        lblTotalAmount.Text = Val(Session("amount_final_room_new")).ToString("N2")
        lblDepositedAmount.Text = Val(drBooking("deposit")).ToString("N2")
        lblComment.Text = drBooking("comment").ToString()
        lblRoomType.Text = objCommon.GetRoomTypeName(drBooking("Roomid").ToString())
        hdnStoreID.Value = drBooking("StoreID").ToString()
    End Sub

    Private Sub ConfirmBooking()
        Dim oDbConnection As New DBConnection()
        Dim objCommon As Common = New Common()
        Dim strBookingID = oDbConnection.ExecuteNonQuery(" if not exists (select 1 from  bookings where bookingref=  '" + BookingRef() + "')	begin insert into bookings (covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, deposit, accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId) select covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, " + Session("amount_deposited").ToString() + ", accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId from Booking_Initiate where bookingref= '" + BookingRef() + "' end")
        Dim drBookingDetail As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
        If Not String.IsNullOrEmpty(drBookingDetail("deposit")) Then
            Dim amount As String = drBookingDetail("deposit").ToString()
            If amount <> 0 Then
                Dim TranID = oDbConnection.ExecuteNonQuery("INSERT INTO T_Payment_Transaction ( Gateway_id, accountid, Booking_id, Transaction_ref_no, Amount, Currency, Trasaction_date, booking_type) VALUES  (0," + drBookingDetail("AccountId").ToString() + "," + drBookingDetail("bookingid").ToString() + ",'Back Office'," + amount + ",'" + CurrencySymbol() + "', GETDATE(), 0 )")
            End If
        End If
        Try
            Dim fileLoc As String = "c:\Temp\" + "Payment_Summary.txt"  '+ System.DateTime.Today.ToShortDateString.Replace("/", "") + ".txt"
            Dim fs As FileStream = Nothing
            If (Not File.Exists(fileLoc)) Then
                fs = File.Create(fileLoc)
                fs.Close()
                Using sw As StreamWriter = New StreamWriter(fileLoc)
                    'sw.Write("Some sample text for the file")
                End Using
            End If
            If File.Exists(fileLoc) Then
                Using sw As StreamWriter = New StreamWriter(fileLoc, True)
                    'Date,Time,Training,OrderID,OrderType,TillID,TillAlias,DrawerNum,Shift,OperatorID,OperatorNum,OrderComment,ProductID,ProductNum,ProductExportCode1, SizeNumber, SizeName, Barcode, ProdOption, QtySold, Gross, Nett, PaymentName, PaymentAmount, Rounding, AccountID, AccountNum, PaymentREFID
                    'sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",," + "Back Office" + ",,,,,,,," + drBookingDetail("comment").ToString + ",,," + description + ",,,,,," + drBookingDetail("bookingtotal").ToString + "," + BookingRef + "," + drBookingDetail("firstname").ToString + " " + drBookingDetail("Lastname").ToString + "," + Session("amont_final").ToString() + ",," + drBookingDetail("accountid").ToString + "," + drBookingDetail("accnumber").ToString + "")
                    If drBookingDetail.Table.Rows.Count > 0 Then
                        Dim DepositAmount As Decimal = drBookingDetail("deposit").ToString()
                        Dim FinalAmount As Decimal = Val(Session("amount_final_room_new")).ToString("N2")
                        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())
                        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
                            For Each dr As DataRow In dsServices.Tables(0).Rows
                                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,," + dr("ProductID").ToString() + ",,,1,,,," + dr("Quantity").ToString() + "," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                            Next
                        Else
                            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,,0,,,1,,,,0," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub ButtonPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrint.Click
    '    divMessage.Visible = False
    '    Session("ctrl") = pnlPrint
    '    ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>")
    'End Sub

    Protected Sub ButtonEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEmail.Click

        divMessage.Visible = False

        Dim oDbConnection As New DBConnection()

        Dim strVenueQuery As String = "select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = " & hdnStoreID.Value 'SELECT VenueID FROM Store WHERE StoreID = 
        Dim strVenueID = oDbConnection.SingleCell(strVenueQuery)

        Dim emailsettingsstring As String = "select MailServer, MailName,MailPassword, MailTCPPort, MailDefaultfrom, MailFlag, name, venue.AddressID,OtherName_1, PhoneWork,Street_1,Street_2, City,Spare, State, Country, Email1st, email2nd, pcode from venue, address where dbo.Venue.AddressID = dbo.Address.AddressID AND VenueID = '" & strVenueID & "'"

        Dim dt As DataTable = oDbConnection.SelectData(emailsettingsstring).Tables(0)

        If dt.Rows.Count > 0 Then


            Dim dr As DataRow = dt.Rows(0)

            Try
                Dim body As String = File.ReadAllText(Server.MapPath("~/BookingEasy/MailTemplate/RoomBooking.html"))

                Dim mailMessage As MailMessage = New MailMessage()
                mailMessage.To.Add(lblEmail.Text)


                Dim sb As StringBuilder = New StringBuilder()
                Dim tw As StringWriter = New StringWriter(sb)
                Dim hw As HtmlTextWriter = New HtmlTextWriter(tw)

                rptServices.RenderControl(hw)
                Dim HTMLContent As String = sb.ToString()

                body = body.Replace("[FullName]", lblFullName.Text)
                body = body.Replace("[Bookingreference]", lblBookingRef.Text)
                body = body.Replace("[GuestName]", lblFullName.Text)
                body = body.Replace("[Mobile]", lblMobile.Text)
                body = body.Replace("[Email]", lblEmail.Text)
                body = body.Replace("[HotelName]", lblName.Text)
                body = body.Replace("[RoomTypeName]", lblRoomType.Text)
                body = body.Replace("[ArrivalDate]", lblArrival.Text)
                body = body.Replace("[DepartureDate]", lblDepature.Text)
                body = body.Replace("[NumberOfNights]", lblNumberOfNights.Text)
                body = body.Replace("[TotalPrice]", CurrencySymbol & " " & lblTotalPrice.Text)
                body = body.Replace("[ExtraService]", HTMLContent)
                body = body.Replace("[Comments]", lblComment.Text)
                body = body.Replace("[Deposite]", CurrencySymbol & " " & Utils.NullToString(lblDepositedAmount.Text, "0"))
                body = body.Replace("[TotalAmount]", Val(Session("amount_final_room_new")).ToString("N2"))
                body = body.Replace("[DepositedAmount]", lblDepositedAmount.Text)
                body = body.Replace("[Currency]", CurrencySymbol())

                'Add Address details 
                If (Not String.IsNullOrEmpty(Utils.NullToString(dr("AddressID")))) Then
                    Dim drAdd As DataRow = oDbConnection.SingleRow("Select top 1 * from [address] WHERE AddressId=" + dr("AddressID").ToString())
                    Dim address As String = drAdd("Street_1").ToString() + " " + drAdd("Street_2").ToString() + " " + drAdd("City").ToString() + " " + drAdd("Spare").ToString() + " " + drAdd("State").ToString() + " " + drAdd("Country").ToString() + " " + drAdd("PCode").ToString()
                    body = body.Replace("[Address]", address)
                End If

                mailMessage.From = New MailAddress(dr("MailDefaultfrom").ToString(), dr("name").ToString())
                mailMessage.Subject = "Reservation " + Request.QueryString("BookingRef") + " for " + dr("name").ToString() + ""
                mailMessage.IsBodyHtml = True
                mailMessage.Body = body

                Dim smtpClient = New SmtpClient(dr("MailServer").ToString(), Utils.getInteger(dr("MailTCPPort")))
                If dr("MailFlag").ToString() = "0" Then
                    smtpClient.EnableSsl = False
                Else
                    smtpClient.EnableSsl = True
                End If

                smtpClient.Timeout = 10000
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network

                smtpClient.UseDefaultCredentials = False
                Dim smtpcredientials1 As String = dr("MailName").ToString()
                Dim smtpcredientials2 As String = dr("MailPassword").ToString()
                smtpClient.Credentials = New Net.NetworkCredential(smtpcredientials1, smtpcredientials2)
                'Send Email
                smtpClient.Send(mailMessage)
                divMessage.Attributes.Add("class", "alert alert-success")
                divMessage.Visible = True
                lblMessage.Text = "Email Sent Successfully."
                'Response.Write("E-mail sent from " + dr("MailDefaultfrom").Text.ToString)

                ''Successfull message goes here
            Catch
                'Response.Write("Could not send the e-mail - error: " + ErrorToString())
                divMessage.Attributes.Add("class", "alert alert-danger")
                divMessage.Visible = True
                lblMessage.Text = "Email Not Sent."
            End Try
        End If
    End Sub

    Protected Sub lnkEmail_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lnkEmail.Command

    End Sub
End Class
