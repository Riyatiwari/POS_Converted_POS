Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Class BookingEasy_TableBookConfimeZeroPayment
    Inherits System.Web.UI.Page

    'Dim oClsDataccess As New ClsDataccess
    'Dim oColSqlPar As ColSqlparram = New ColSqlparram()

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("BR")) Then
                Return Utils.Decrypt(Request.QueryString("BR")).ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property DepoAmount() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("DA")) Then
                Return Utils.Decrypt(Request.QueryString("DA")).ToString()
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Session("AllowSetting") = Nothing

            If Not IsPostBack Then

                If (BookingRef IsNot String.Empty) Then
                    ConfirmBooking()
                    BindDetails()

                    If Session("amont_final1") IsNot Nothing Then
                        lblTotalAmount.Text = Session("amont_final1").ToString()
                        Session("amont_final1") = 0
                    Else
                        Session("amont_final1") = Session("amont_final").ToString()
                    End If



                    Dim field1 As String = CType(Session.Item("WidgetVal"), String)
                    If field1 = "0" Then
                        If Sessions.TabID = 0 Or Sessions.TabID = Nothing Then
                            If Sessions.UserID <> 0 Then
                                aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt("0") + ""
                            Else
                                If Session("WidType") = "Table" Then
                                    aHome.HRef = "~/Booking/TableBookingWidget.aspx?s_name=" + Sessions.store + ""
                                Else
                                    aHome.HRef = "~/Booking/RoomBookingWidget.aspx"
                                End If
                            End If

                        Else
                            aHome.HRef = "~/Booking/Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
                        End If
                    ElseIf field1 = "1" Then
                        If Session("WidType") = "Table" Then
                            aHome.HRef = "~/Booking/TableBookingWidget.aspx?s_name=" + Sessions.store + ""
                        Else
                            aHome.HRef = "~/Booking/RoomBookingWidget.aspx"
                        End If
                    End If
                Else
                    Response.Redirect("InvalidAccess.aspx")
                End If


            End If
            If Session("IsOnline") = "1" Then

                aHome.HRef = Session("website")
                Dim aNavbarHome As HtmlAnchor = CType(Master.FindControl("aNavbarHome"), HtmlAnchor)

                If aNavbarHome IsNot Nothing Then
                    btnUpdateBooking.Visible = False
                    lnkPrint.Visible = False
                    lnkEmail.Visible = False
                    aNavbarHome.Attributes.Remove("href")
                    aNavbarHome.Attributes.Add("style", "pointer-events:none;")
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("TableBookConfimeZeroPayment:pageload:" + ex.Message)
        End Try


    End Sub

    Private Sub ConfirmBooking()
        Try
            Dim oDbConnection As New DBConnection()
            Dim objCommon As Common = New Common()
            Dim oClsDataccess As New ClsDataccess
            Dim strBookingID = oDbConnection.ExecuteNonQuery(" if not exists (select 1 from  bookings where bookingref=  '" + BookingRef() + "')	begin insert into bookings (covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, deposit, accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId) select covers, date, comment, Roomid, arrivaldate, departuredate, checkedin, bookingref, bookingtotal, " + DepoAmount().ToString() + ", accountid, period, bookingtime, BookingSettingsid, IsCanceled, CreatedDate, CreatedBy, IPAddress, GrandTotal, BookingScheduleID, BookingScheduleDateId, IsOnline, OurStoreId from Booking_Initiate where bookingref= '" + BookingRef() + "' end")

            Dim drBookingDetail As DataRow = objCommon.GetBookingDetailByRef(BookingRef())

            '--------------------start 03022022-------------------------
            Dim que As String = ""
            que += " SELECT B.comment as comment,SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid, BSc.GroupID, B.covers, (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables,"
            que += " A.FirstName+' '+A.LastName AS FullName, B.covers, convert(nvarchar(10),B.date,103) AS arrivaldate, B.bookingref, B.bookingtime, BSc.Name AS ScheduleName,B.date,convert(nvarchar(5),bookingtime,108) as time "
            que += " FROM bookings B INNER JOIN Account A ON B.accountid = A.accountid left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId  "
            que += " INNER JOIN BookingSchedule BSc ON BSc.BookingScheduleID = b.BookingScheduleID "
            que += " WHERE B.bookingref = '" + BookingRef.ToString() + "'"
            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)


            Dim strQuery As String = String.Empty
            strQuery += "SELECT top 1 Table_name ,table_id , TableNo FROM M_Table b where MaxCover >= '" + drBookingDetail("covers").ToString() + "' and Table_id in (select items from dbo.Split((select table_set from M_Table_Group where GroupID = " + ds.Tables(0).Rows(0)("GroupID").ToString() + "),('#'))) "
            strQuery += "AND Table_id not in (select isnull(items,'')  from dbo.Split(((select Stuff((SELECT '# ' + isnull(b.Allotted_Tables,0) from bookings b "
            strQuery += "where	b.BookingScheduleDateId = (select BookingScheduleDateId from bookings WHERE bookingref = '" + BookingRef.ToString() + "') "
            strQuery += "AND Convert(varchar(5), b.bookingtime, 108) BETWEEN (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-((bsh.ServiceDuration*2)-1)),bks.bookingtime), 108) AS Mservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + BookingRef.ToString() + "') "
            strQuery += "AND (SELECT Convert(varchar(5), DATEADD(minute,(bsh.ServiceDuration-1),bks.bookingtime), 108) AS Pservice from bookings bks INNER JOIN BookingSchedule bsh on bsh.BookingScheduleID = bks.BookingScheduleID WHERE bks.bookingref = '" + BookingRef.ToString() + "') FOR XML PATH('')), 1, 2, ''))),('#'))) order by MaxCover asc"
            Dim ds1 As DataSet = oDbConnection.SelectData(strQuery)


            que = "Update Bookings Set Allotted_Tables='" & ds1.Tables(0).Rows(0)("table_id").ToString() & "', TableNo='" & ds1.Tables(0).Rows(0)("TableNo").ToString() & "' WHERE bookingref = '" & BookingRef() & "'"
            oDbConnection.ExecuteNonQuery(que)

            If Session("cmp_id") = Nothing Then
                Session("cmp_id") = 0
            End If

            Dim insQry As String = String.Empty
            insQry += " if not exists (select 1 from M_Voucher where voucher_name = 'Deposits' and Voucher_Type='Deposits') "
            insQry += " begin INSERT INTO M_Voucher(voucher_name,Voucher_Type,endless,cmp_id,start_date,end_date,created_date,modify_date,Voucher_duration) "
            insQry += "values ('Deposits','Deposits',1," + Session("cmp_id").ToString() + ",GETDATE(),GETDATE(),GETDATE(),GETDATE(),'Endless') end "
            Dim voucher_id = oDbConnection.ExecuteNonQuery(insQry)

            Dim val As String = "DECLARE @start INT DECLARE @end INT SET @start = 100000 SET @end = 9999999 SELECT  Round(( ( @end - @start - 1 ) * Rand() + @start ), 0) as ref_no"
            Dim ref_ds As DataSet = oDbConnection.SelectData(val)

            Dim insQry1 As String = String.Empty
            insQry1 += "INSERT INTO M_IssueVoucher(account,Voucher_id,deposit_amount,cmp_id,ref_no,issue_datetime,created_date,modify_date,voucher_duration,start_date,end_date)"
            insQry1 += "VALUES(" + drBookingDetail("AccountID").ToString() + ",convert(nvarchar(max),(select voucher_id from M_Voucher where voucher_name = 'Deposits'))," + drBookingDetail("deposit").ToString() + "," + Session("cmp_id").ToString() + "," + ref_ds.Tables(0).Rows(0)("ref_no").ToString() + ",GETDATE(),GETDATE(),GETDATE(),'Endless',GETDATE(),GETDATE())"
            oDbConnection.ExecuteNonQuery(insQry1)

            Dim insQry2 As String = String.Empty
            insQry2 += "INSERT INTO M_VoucherTransaction( customer_id, sales_id, VoucherRef_no, Amount, Tran_Date, created_date, modify_date, is_active, ip_address, cmp_id)"
            insQry2 += "VALUES(" + drBookingDetail("AccountID").ToString() + ",0," + ref_ds.Tables(0).Rows(0)("ref_no").ToString() + "," + drBookingDetail("deposit").ToString() + ",GETDATE(),GETDATE(),GETDATE(),1,''," + Session("cmp_id").ToString() + ")"
            oDbConnection.ExecuteNonQuery(insQry2)


            '--------------------End 03022022-------------------------


            If Not String.IsNullOrEmpty(DepoAmount()) Then
                Dim amount As String = DepoAmount().ToString()
                If amount <> 0 Then
                    Dim TranID = oDbConnection.ExecuteNonQuery("INSERT INTO T_Payment_Transaction  ( Gateway_id, accountid, Booking_id, Transaction_ref_no, Amount, Currency, Trasaction_date , booking_type) VALUES  (0," + drBookingDetail("AccountId").ToString() + "," + drBookingDetail("bookingid").ToString() + ",'Back Office'," + DepoAmount().ToString() + ",'" + CurrencySymbol() + "',GETDATE(),1)")
                End If
            End If
            Dim description = Session("description_final").ToString
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
                        Dim DepositAmount As Decimal = DepoAmount().ToString()
                        Dim FinalAmount As Decimal = 0

                        If Session("amont_final1") IsNot Nothing Then
                            FinalAmount = Session("amont_final1").ToString()
                            Session("amont_final1") = 0
                        Else
                            Session("amont_final1") = Session("amont_final").ToString()
                            FinalAmount = Session("amont_final1").ToString()
                        End If

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

            Try
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@BookingRef", BookingRef())
                oclsParm.Add("@BookingScheduleID", drBookingDetail("BookingScheduleID").ToString())
                oclsParm.Add("@covers", drBookingDetail("covers").ToString())
                oClsDataccess.ExecStoredProcedure("Get_table_list", oclsParm)

            Catch ex As Exception

            End Try
            aChangeTable.HRef = "PopupChangeTable.aspx?bookingref=" + BookingRef
            aUpdateBooking.HRef = "PopupUpdateBookingTable.aspx?bookingref=" + BookingRef

            Try
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@ID", 0, SqlDbType.Int)
                oclsParm.Add("@Tran_type", "BookingRef")
                oclsParm.Add("@BookingRef", BookingRef())
                oClsDataccess.ExecStoredProcedure("P_M_OpenTable", oclsParm)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Dim s As String = ex.Message.ToString()
        End Try
    End Sub
    Private Sub BindDetails()
        Dim objCommon As Common = New Common()
        Dim drBooking As DataRow = objCommon.GetBookingDetailByRef(BookingRef())
        lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
        lblEmail.Text = drBooking("Email1st").ToString()
        lblMobile.Text = drBooking("Mobile").ToString()
        lblBookingRef.Text = BookingRef
        lblName.Text = drBooking("Name").ToString()
        lblTotalAmount.Text = Session("amont_final").ToString()

        'If Session("amont_final1") IsNot Nothing Then
        '    lblTotalAmount.Text = Session("amont_final1").ToString()
        '    Session("amont_final1") = 0
        'Else
        '    Session("amont_final1") = Session("amont_final").ToString()
        'End If

        Dim bDate As DateTime = Convert.ToDateTime(drBooking("date").ToString())
        Dim bTime As TimeSpan = TimeSpan.Parse(drBooking("bookingtime").ToString())
        lblBookingDate.Text = bDate.ToString("dd/MM/yyyy")
        lblBookingTime.Text = String.Format("{0:hh\:mm}", bTime)
        lblComment.Text = drBooking("comment").ToString()
        lblNoOfCovers.Text = drBooking("covers").ToString()
        lblAlltedTable.Text = drBooking("Allotted_Tables1").ToString()

        If lblAlltedTable.Text = "No Table Allotted" Then
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lbExistingCust.Text = "No Table Allotted "
        End If

        Dim field1 As String = CType(Session.Item("WidgetVal"), String)
        If field1 = "1" Then
            lblDepositedAmount.Text = "0.00"
        ElseIf field1 = "0" Then
            lblDepositedAmount.Text = DepoAmount()
            Session("da_amount") = DepoAmount()
        End If
        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptServices.DataSource = dsServices
            rptServices.DataBind()
            rptServices.Visible = True
        Else
            rptServices.Visible = False
        End If

        hdnStoreID.Value = drBooking("StoreID").ToString()
    End Sub


    'Protected Sub ButtonPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrint.Click
    '    Try
    '        divMessage.Visible = False

    '        Session("ctrl") = pnlPrint
    '        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub ButtonEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEmail.Click
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetEmpByBookingRef(BookingRef())
            If ds.Tables(0).Rows(0)("Email1st").ToString.Trim = "" Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "alertMessage", "alert('Email id not exists.')", True)
            Else
                divMessage.Visible = False
                Dim oDbConnection As New DBConnection()

                Dim strVenueQuery As String = "select v.OtherVenueId from StoreMaster s left outer join VenueMaster v on v.BookingVenueId = s.BookingVenueId where OurStoreId = " & hdnStoreID.Value 'SELECT VenueID FROM Store WHERE StoreID = 
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
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '     server control at run time. 
    End Sub
    Protected Sub rptServices_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptServices.ItemDataBound
        Try
            Session("amont_final1") = Val(Session("amont_final1")) + (Val(CType(e.Item.FindControl("hfTotalPrice"), HiddenField).Value))
            'If Session("total_amt_new") IsNot Nothing Then
            '    Session("amont_final1") = Session("total_amt_new").ToString()
            'End If
            Dim hf = e.Item.FindControl("hfTotalPrice")
            e.Item.Controls.Remove(hf)

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub TotalAmount()

    '    Dim i As Integer = 0
    '    For Each item As RepeaterItem In rptServices.Items
    '        If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
    '            i = Convert.ToInt32(lblTotalAmount.Text) + (Val(CType(e.Item.FindControl("hfTotalPrice"), HiddenField).Value))
    '            lblTotalAmount.Text = i.ToString()
    '        End If
    '    Next

    'End Sub

    'Protected Sub btnChangeTable_Click(sender As Object, e As System.EventArgs) Handles btnChangeTable.Click
    '    Response.Redirect("PopupChangeTable.aspx?bookingref=" + BookingRef)
    'End Sub
End Class
