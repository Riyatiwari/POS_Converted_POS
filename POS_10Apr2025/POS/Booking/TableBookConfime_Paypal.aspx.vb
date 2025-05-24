Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Class BookingEasy_TableBookConfime_Paypal
    Inherits System.Web.UI.Page
    'Dim oClsDataccess As New ClsDataccess
    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.Form("invoice")) Then
                Return Request.Form("invoice").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Deposited() As String
        Get
            If Not String.IsNullOrEmpty(Request.Form("mc_gross")) Then
                Return Request.Form("mc_gross").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

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
    Dim oDbConnection As New DBConnection()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing
        ViewState("Description") = ""

        If Not IsPostBack Then
            If (BookingRef IsNot String.Empty) Then
                ConfirmBooking()

                BindDetails()
                Dim dtAccount As DataSet = oDbConnection.SelectData(" select 1 from Booking_Initiate where Roomid <> 0 and bookingref = '" + BookingRef() + "'")
                If dtAccount.Tables(0).Rows.Count = 0 Then
                    Dim field1 As String = CType(Session.Item("WidgetVal"), String)
                    If field1 = "0" Then
                        If Sessions.TabID = 0 Or Sessions.TabID = Nothing Then
                            aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt("0") + ""
                        Else
                            aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
                        End If
                    ElseIf field1 = "1" Then
                        If Session("WidType") = "Table" Then
                            aHome.HRef = "~/BookingEasy/TableBookingWidget.aspx"
                        Else
                            aHome.HRef = "~/BookingEasy/RoomBookingWidget.aspx"
                        End If
                    End If
                Else
                    Dim field1 As String = CType(Session.Item("WidgetVal"), String)
                    If field1 = "0" Then
                        If Sessions.TabID = 0 Or Sessions.TabID = Nothing Then
                            aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt("0") + ""
                        Else
                            aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
                        End If
                    ElseIf field1 = "1" Then
                        If Session("WidType") = "Table" Then
                            aHome.HRef = "~/BookingEasy/TableBookingWidget.aspx"
                        Else
                            aHome.HRef = "~/BookingEasy/RoomBookingWidget.aspx"
                        End If
                    End If
                End If
            Else
                Response.Redirect("InvalidAccess.aspx")
            End If
        End If
    End Sub

    Private Sub ConfirmBooking()
        Dim oDbConnection As New DBConnection()
        Dim objCommon As Common = New Common()
        Dim oClsDataccess As New ClsDataccess
        Dim dtAccount As DataSet = oDbConnection.SelectData(" select 1 from Booking_Initiate where Roomid <> 0 and bookingref = '" + BookingRef() + "'")
        If dtAccount.Tables(0).Rows.Count = 0 Then
            Dim strBookingID = oDbConnection.ExecuteNonQuery(" if not exists (select 1 from  bookings where bookingref=  '" + BookingRef() + "')	begin insert into bookings (covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, deposit, accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId) select covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, " + Request.Form("mc_gross").ToString() + ", accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId from Booking_Initiate where bookingref= '" + BookingRef() + "' end")
            Dim drBookingDetail As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
            Dim TranID = oDbConnection.ExecuteNonQuery("  if not exists (select 1 from  T_Payment_Transaction where Transaction_ref_no=  '" + Request.Form("txn_id").ToString + "')	begin INSERT INTO T_Payment_Transaction  ( Gateway_id, accountid, Booking_id, Transaction_ref_no, Amount, Currency, Trasaction_date, booking_type) VALUES  (0," + drBookingDetail("AccountId").ToString() + "," + drBookingDetail("bookingid").ToString() + ",'" + Request.Form("txn_id").ToString + "'," + Request.Form("mc_gross").ToString() + ",'" + CurrencySymbol() + "',GETDATE(),1) end")
            Try
                Dim fileLoc As String = "c:\Temp\" + "Payment_Summary.txt" '+ System.DateTime.Today.ToShortDateString.Replace("/", "") + ".txt"
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
                        'Date,Time,Training,OrderID,OrderType,TillID,TillAlias,DrawerNum,ShiftNum,OperatorID,OperatorNum,OrderComment,ProductID,ProductNum,Description, SizeNumber, SizeName, Barcode, ProdOption, QtySold, Gross, Nett, PaymentName, PaymentAmount, Rounding, AccountID, AccountNum
                        'sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",," + Request.Form("txn_id").ToString + ",,,,,,,," + drBookingDetail("comment").ToString + ",,," + Request.Form("item_name").ToString + ",,,,,," + drBookingDetail("bookingtotal").ToString + "," + drBookingDetail("bookingtotal").ToString + "," + drBookingDetail("firstname").ToString + " " + drBookingDetail("Lastname").ToString + "," + Request.Form("mc_gross").ToString() + ",," + drBookingDetail("accountid").ToString + "," + drBookingDetail("accnumber").ToString + "")
                        Dim DepositAmount As Decimal = Request.Form("mc_gross").ToString()
                        Dim FinalAmount As Decimal = Session("amont_final").ToString()
                        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())
                        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
                            For Each dr As DataRow In dsServices.Tables(0).Rows
                                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,," + dr("ProductID").ToString() + ",,,1,,,," + dr("Quantity").ToString() + "," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                            Next
                        Else
                            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,,0,,,1,,,,0," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                        End If
                    End Using
                End If
            Catch ex As Exception

            End Try
            'Dim bookingid_str As String = "select bookingid from bookings where bookingref = '" + BookingRef() + "'"
            'Dim booking_id = oDbConnection.SingleCell(bookingid_str)
            'Dim string_qry As String = "select 1 from bookings b Inner join BookingSchedule s on s.BookingScheduleID = b.BookingScheduleID where b.bookingid = " + booking_id.ToString() + " and (CONVERT(VARCHAR(10),GETDATE(),120) between S.StartDate and S.EndDate) and (CONVERT(VARCHAR(15),GETDATE(),108) between S.StartTime and S.EndTime)"
            'Dim dt As DataTable = oDbConnection.SelectData(string_qry).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    Common.TodayOpenTablesByBookingID(booking_id)
            'End If
            Try
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@BookingRef", BookingRef())
                oclsParm.Add("@BookingScheduleID", drBookingDetail("BookingScheduleID").ToString())
                oclsParm.Add("@covers", drBookingDetail("covers").ToString())
                oClsDataccess.ExecStoredProcedure("Get_table_list", oclsParm)
                'Dim oClsDataccess As New ClsDataccess
                'Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                'oColSqlPar.Add("@BookingRef", BookingRef())
                'oColSqlPar.Add("@BookingScheduleID", drBookingDetail("BookingScheduleID").ToString())
                'oColSqlPar.Add("@covers", drBookingDetail("covers").ToString())
                'Dim dt_Table_list As DataTable = oClsDataccess.GetdataTableSp("Get_table_list", oColSqlPar, "Get_Table_List")
                'If dt_Table_list.Rows.Count > 0 Then
                '    'If dt_Table_list.Rows(0)("TableID") = "All Tables are full" Then
                '    '    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMessage1", "alert('All Tables are full')", True)
                '    'End If
                'End If
            Catch ex As Exception

            End Try
            Try
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@ID", 0, SqlDbType.Int)
                oclsParm.Add("@Tran_type", "BookingRef")
                oclsParm.Add("@BookingRef", BookingRef())
                oClsDataccess.ExecStoredProcedure("P_M_OpenTable", oclsParm)
            Catch ex As Exception

            End Try
        Else
            Dim strBookingID = oDbConnection.ExecuteNonQuery(" if not exists (select 1 from  bookings where bookingref=  '" + BookingRef() + "')	begin insert into bookings (covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, deposit, accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId) select covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref,bookingtotal, " + Request.Form("mc_gross").ToString() + ", accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId from Booking_Initiate where bookingref= '" + BookingRef() + "' end")
            Dim drBookingDetail As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
            Dim TranID = oDbConnection.ExecuteNonQuery("  if not exists (select 1 from  T_Payment_Transaction where Transaction_ref_no=  '" + Request.Form("txn_id").ToString + "')	begin INSERT INTO T_Payment_Transaction  ( Gateway_id, accountid, Booking_id, Transaction_ref_no, Amount, Currency, Trasaction_date, booking_type) VALUES  (0," + drBookingDetail("AccountId").ToString() + "," + drBookingDetail("bookingid").ToString() + ",'" + Request.Form("txn_id").ToString + "'," + Request.Form("mc_gross").ToString() + ",'" + CurrencySymbol() + "',GETDATE(),0) end")
            Try
                Dim fileLoc As String = "c:\Temp\" + "Payment_Summary.txt"
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
                        'Date,Time,Training,OrderID,OrderType,TillID,TillAlias,DrawerNum,ShiftNum,OperatorID,OperatorNum,OrderComment,ProductID,ProductNum,Description, SizeNumber, SizeName, Barcode, ProdOption, QtySold, Gross, Nett, PaymentName, PaymentAmount, Rounding, AccountID, AccountNum
                        'sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",," + Request.Form("x_invoice_num").ToString + ",,,,,,,," + drBookingDetail("comment").ToString + ",,," + Request.Form("x_description").ToString + ",,,,,," + drBookingDetail("bookingtotal").ToString + "," + drBookingDetail("bookingtotal").ToString + "," + drBookingDetail("firstname").ToString + " " + drBookingDetail("Lastname").ToString + "," + Request.Form("x_amount").ToString() + ",," + drBookingDetail("accountid").ToString + "," + drBookingDetail("accnumber").ToString + "")
                        Dim DepositAmount As Decimal = Request.Form("mc_gross").ToString()
                        Dim FinalAmount As Decimal = Session("amont_final").ToString()
                        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())
                        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
                            For Each dr As DataRow In dsServices.Tables(0).Rows
                                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,," + dr("ProductID").ToString() + ",,,1,,,," + dr("Quantity").ToString() + "," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                            Next
                        Else
                            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy") + "," + DateTime.Now.ToString("hh:mm tt") + ",,,,,,,,,,,0,,,1,,,,0," + FinalAmount.ToString("N2") + "," + FinalAmount.ToString("N2") + ",Cash," + DepositAmount.ToString("N2") + ",0.00," + drBookingDetail("accountid").ToString + ",,")
                        End If
                    End Using
                End If
                Try
                    'Dim oClsDataccess As New ClsDataccess
                    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                    oColSqlPar.Add("@BookingRef", BookingRef())
                    oColSqlPar.Add("@BookingScheduleID", drBookingDetail("BookingScheduleID").ToString())
                    oColSqlPar.Add("@covers", drBookingDetail("covers").ToString())
                    Dim dt_Table_list As DataTable = oClsDataccess.GetdataTableSp("Get_table_list", oColSqlPar, "Get_Table_List")
                    If dt_Table_list.Rows.Count > 0 Then
                        'If dt_Table_list.Rows(0)("TableID") = "All Tables are full" Then
                        '    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMessage1", "alert('All Tables are full')", True)
                        'End If
                    End If
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub BindDetails()
        Dim objCommon As Common = New Common()
        Dim dtAccount As DataSet = oDbConnection.SelectData(" select 1 from Booking_Initiate where Roomid <> 0 and bookingref = '" + BookingRef() + "'")
        If dtAccount.Tables(0).Rows.Count = 0 Then
            room.Visible = False
            Dim drBooking As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
            lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
            lblEmail.Text = drBooking("Email1st").ToString()
            lblMobile.Text = drBooking("Mobile").ToString()
            lblBookingRef.Text = BookingRef
            lblName.Text = drBooking("Name").ToString()
            lblTotalAmount.Text = Session("amont_final").ToString()
            lblDepositedAmount.Text = Deposited()
            Dim bDate As DateTime = Convert.ToDateTime(drBooking("date").ToString())
            Dim bTime As TimeSpan = TimeSpan.Parse(drBooking("bookingtime").ToString())
            lblBookingDate.Text = bDate.ToString("dd/MM/yyyy")
            lblBookingTime.Text = String.Format("{0:hh\:mm}", bTime)
            lblComment.Text = drBooking("comment").ToString()
            lblNoOfCovers.Text = drBooking("covers").ToString()
            lblAlltedTable.Text = drBooking("Allotted_Tables1").ToString()

            Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

            If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
                rptServices.DataSource = dsServices
                rptServices.DataBind()
                rptServices.Visible = True
            Else
                rptServices.Visible = False
            End If

            hdnStoreID.Value = drBooking("StoreID").ToString()

        Else
            table.Visible = False
            Dim drBooking As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
            lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
            lblEmail.Text = drBooking("Email1st").ToString()
            lblMobile.Text = drBooking("Mobile").ToString()
            lblBookingRef.Text = BookingRef
            lblNameU.Text = drBooking("Name").ToString()
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
            lblDepositedAmount.Text = Session("amountReceived").ToString()
            lblComment.Text = drBooking("comment").ToString()
            lblRoomType.Text = objCommon.GetRoomTypeName(drBooking("Roomid").ToString())
            hdnStoreID.Value = drBooking("StoreID").ToString()
        End If

    End Sub


    Protected Sub ButtonPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrint.Click
        divMessage.Visible = False

        Session("ctrl") = pnlPrint
        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>")
    End Sub

    Protected Sub ButtonEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEmail.Click
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetEmpByBookingRef(BookingRef())
        If ds.Tables(0).Rows(0)("Email1st").ToString.Trim = "" Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMessage", "alert('Email id not exists.')", True)
        Else
            Dim dtAccount As DataSet = oDbConnection.SelectData(" select 1 from Booking_Initiate where Roomid <> 0 and bookingref = '" + BookingRef() + "'")
            If dtAccount.Tables(0).Rows.Count = 0 Then
                divMessage.Visible = False
                Dim oDbConnection As New DBConnection()

                Dim strVenueQuery As String = "select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = " & hdnStoreID.Value
                Dim strVenueID = oDbConnection.SingleCell(strVenueQuery)

                Dim emailsettingsstring As String = "select MailServer, MailName,MailPassword, MailTCPPort, MailDefaultfrom, MailFlag, name, venue.AddressID,OtherName_1, PhoneWork,Street_1,Street_2, City,Spare, State, Country, Email1st, email2nd, pcode from venue, address where dbo.Venue.AddressID = dbo.Address.AddressID AND VenueID = '" & strVenueID & "'"

                Dim dt As DataTable = oDbConnection.SelectData(emailsettingsstring).Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt.Rows(0)

                    Try

                        Dim body As String = File.ReadAllText(Server.MapPath("~/BookingEasy/MailTemplate/TableBooking.html"))

                        Dim mailMessage As MailMessage = New MailMessage()
                        mailMessage.To.Add(lblEmail.Text)

                        Dim sb As StringBuilder = New StringBuilder()
                        Dim tw As StringWriter = New StringWriter(sb)
                        Dim hw As HtmlTextWriter = New HtmlTextWriter(tw)

                        rptServices.RenderControl(hw)
                        Dim HTMLContent As String = sb.ToString()

                        body = body.Replace("[FullName]", lblFullName.Text)
                        body = body.Replace("[BookingReference]", lblBookingRef.Text)
                        body = body.Replace("[FullName]", lblFullName.Text)
                        body = body.Replace("[Mobile]", lblMobile.Text)
                        body = body.Replace("[Email]", lblEmail.Text)
                        body = body.Replace("[HotelName]", lblName.Text)
                        body = body.Replace("[BookingDate]", lblBookingDate.Text)
                        body = body.Replace("[BookingTime]", lblBookingTime.Text)
                        body = body.Replace("[BookingCovers]", lblNoOfCovers.Text)
                        'body = body.Replace("[Deposite]", "0")
                        body = body.Replace("[ExtraService]", HTMLContent)
                        body = body.Replace("[Comments]", lblComment.Text)
                        body = body.Replace("[TotalAmount]", lblTotalAmount.Text)
                        body = body.Replace("[DepositedAmount]", lblDepositedAmount.Text)
                        body = body.Replace("[Currency]", CurrencySymbol())

                        'Add Address details 
                        If (Not String.IsNullOrEmpty(Utils.NullToString(dr("AddressID")))) Then
                            Dim drAdd As DataRow = oDbConnection.SingleRow("Select top 1 * from [address] WHERE AddressId=" + dr("AddressID").ToString())
                            Dim address As String = drAdd("Street_1").ToString() + " " + drAdd("Street_2").ToString() + " " + drAdd("City").ToString() + " " + drAdd("Spare").ToString() + " " + drAdd("State").ToString() + " " + drAdd("Country").ToString() + " " + drAdd("PCode").ToString()
                            body = body.Replace("[Address]", address)
                        End If

                        mailMessage.From = New MailAddress(dr("MailDefaultfrom").ToString(), dr("name").ToString())
                        mailMessage.Subject = "Reservation " + BookingRef + " for " + dr("name").ToString() + ""
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

                    Catch

                        Response.Write("Could not send the e-mail - error: " + ErrorToString())

                    End Try
                End If
            Else
                divMessage.Visible = False

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
                        body = body.Replace("[HotelName]", lblNameU.Text)
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
            End If
        End If
    End Sub

    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    '    Label1.Text = Request.Form("invoice")
    '    Label2.Text = Request.Form.Count.ToString()
    'End Sub
End Class
