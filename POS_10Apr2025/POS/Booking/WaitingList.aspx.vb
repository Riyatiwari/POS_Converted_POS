Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing
Imports System.Net
Imports System.IO

Partial Class WaitingList
    Inherits System.Web.UI.Page
    'Dim oClsDataccess As New ClsDataccess
    'Dim oColSqlPar As ColSqlparram = New ColSqlparram()

    Public Property IsOnline() As Boolean
        Get
            If (Not ViewState("IsOnline") Is Nothing) Then
                Return ViewState("IsOnline")
            End If
            Return False
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsOnline") = value
        End Set
    End Property
    Public Property ActualStoreID() As Int32
        Get
            If (Not ViewState("ActualStoreID") Is Nothing) Then
                Return ViewState("ActualStoreID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("ActualStoreID") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                BindWaitingList()

                If Session("wls2") = Nothing Then
                    divMessageBox.Visible = False
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                End If

                If Session("wls2") = "1" Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    lbExistingCust.Text = Session("wls1").ToString()
                    Session("wls1") = Nothing
                    Session("wls2") = Nothing
                Else
                    divMessageBox.Visible = False
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                End If

                If Session("wls2") = "2" Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    lbExistingCust.Text = Session("wls1").ToString()
                    Session("wls1") = Nothing
                    Session("wls2") = Nothing
                End If

                If Session("wls2") = "3" Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    lbExistingCust.Text = Session("wls1").ToString()
                    Session("wls1") = Nothing
                    Session("wls2") = Nothing
                End If

            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub BindWaitingList()
        Try
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            Dim oClsDataccess As New ClsDataccess

            oColSqlPar.Add("@date", Session("rdpDate").ToString(), SqlDbType.DateTime)
            oColSqlPar.Add("@BookingSettingsID", Session("drpTableStoreId").ToString(), SqlDbType.Int)

            Dim dsBookingList As DataSet = oClsDataccess.GetdatasetSp("Get_Waitlist", oColSqlPar, "Waiting Table List")
            gvwaitinglist.DataSource = dsBookingList.Tables(0)
            gvwaitinglist.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvwaitinglist_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvwaitinglist.ItemCommand
        Try
            Dim oClsDataccess As New ClsDataccess
            If (e.CommandName = "btnOffer") Then

                Dim oClsDataccess1 As New ClsDataccess
                Dim oColSqlPar4 As ColSqlparram = New ColSqlparram()

                Dim BookingSettingsid As Integer = 0
                BookingSettingsid = Convert.ToInt32(e.CommandArgument.ToString())

                oColSqlPar4.Add("@BookingSettingsID", BookingSettingsid, SqlDbType.Int)
                Dim dsBookingList As DataSet = oClsDataccess1.GetdatasetSp("Get_Current_Schedule", oColSqlPar4, "Get_Current_Schedule")

                Dim BookingScheduleID As Integer = 0
                Dim BookingScheduleName As String
                BookingScheduleID = dsBookingList.Tables(0).Rows(0)("BookingScheduleID").ToString()
                BookingScheduleName = dsBookingList.Tables(0).Rows(0)("Name").ToString()

                Dim hdnwaitingno As HiddenField = TryCast(e.Item.FindControl("hdnwaitingno"), HiddenField)
                Dim hdnwaitingid As HiddenField = TryCast(e.Item.FindControl("hdnwaitingid"), HiddenField)
                Dim hdnStoreID As HiddenField = TryCast(e.Item.FindControl("hdnStoreID"), HiddenField)
                Dim hdncovers As HiddenField = TryCast(e.Item.FindControl("hdncovers"), HiddenField)
                Dim hdnaccountid As HiddenField = TryCast(e.Item.FindControl("hdnaccountid"), HiddenField)
                Dim hdncomment As HiddenField = TryCast(e.Item.FindControl("hdncomment"), HiddenField)
                Dim hdnStoreName As HiddenField = TryCast(e.Item.FindControl("hdnStoreName"), HiddenField)

                Dim noOfCovers As String

                Dim objCommon As Common = New Common()

                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@BookingScheduleID", Val(BookingScheduleID), SqlDbType.Int)
                Dim ds4 As DataSet = oClsDataccess.GetdatasetSp("Check_Mincovers_OneBooking", oColSqlparram, "MinCover")

                If (ds4.Tables(0).Rows.Count > 0) Then
                    If ds4.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds4.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                        Dim min As Integer = ds4.Tables(0).Rows(0)("MinCovers").ToString()
                        If hdncovers.Value < min Then
                            divMessageBox.Visible = True
                            divMessageBox.Attributes.Add("class", "alert alert-danger")
                            lbExistingCust.Text = "Minimum " + ds4.Tables(0).Rows(0)("MinCovers").ToString() + " Covers Required."
                            Exit Sub
                        End If
                    End If
                End If

                If Not Request.QueryString("IsOnline") Is Nothing Then
                    IsOnline = True
                    BindMaxCovers(BookingScheduleID)
                End If

                If IsOnline Then
                    ActualStoreID = objCommon.GetStoreIDForOnlineBooking(Utils.ParseInt(e.CommandArgument.ToString()), DateTime.Now, Utils.ParseInt(BookingScheduleID))
                End If


                Dim dt As DataTable = Common.SearchTableGetStore(Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd"), "01:00", e.CommandArgument.ToString(), BookingScheduleID.ToString())

                Dim timeSlotId As String = dt.Rows(0)("TimeSlotId").ToString()
                Dim settingsId As String = dt.Rows(0)("SettingID").ToString()
                Dim StoreId As String = dt.Rows(0)("StoreID").ToString()
                noOfCovers = hdncovers.Value.ToString()

                Dim ds1 As New DataSet()
                Dim oClsDataccess2 As New ClsDataccess
                Dim oColSqlPar2 As ColSqlparram = New ColSqlparram()
                oColSqlPar2.Add("@BbookingDate", Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd"), SqlDbType.DateTime)
                oColSqlPar2.Add("@BSettingsID", settingsId)
                oColSqlPar2.Add("@BcurrentCover", noOfCovers)
                oColSqlPar2.Add("@BBookingScheduleID", Utils.ParseInt(BookingScheduleID))
                oColSqlPar2.Add("@BCurrBookingRefNo", String.Empty)
                oColSqlPar2.Add("@BIsOnline", IsOnline)

                ds1 = oClsDataccess2.GetdatasetSp("SearchTableGetTimeSlots", oColSqlPar2, "SearchTableGetTimeSlots")

                Dim i As Integer
                Dim count As Integer = 0
                Dim noofrow As Integer = 0
                noofrow = ds1.Tables(0).Rows.Count - 1

                For i = 0 To ds1.Tables(0).Rows.Count - 1


                    Dim SlotTime As String = ds1.Tables(0).Rows(i)("SlotTime").ToString()
                    SlotTime = TimeSpan.Parse(SlotTime.ToString()).ToString("hh\:mm")
                    Dim Now As String = DateTime.Now.ToString("HH:mm")
                    Now = Now.Substring(0, 5)
                    Now = TimeSpan.Parse(Now.ToString()).ToString("hh\:mm")

                    Session("stime") = SlotTime
                    Dim isAvailable As String = ds1.Tables(0).Rows(i)("isAvailable").ToString()

                    If i = 0 Then
                        If Now < SlotTime Then
                            divMessageBox.Visible = True
                            divMessageBox.Attributes.Add("class", "alert alert-danger")
                            lbExistingCust.Text = "Currently no time slot available"

                            Exit Sub
                        End If
                    End If
                    If i = noofrow Then
                        If Now > SlotTime Then
                            divMessageBox.Visible = True
                            divMessageBox.Attributes.Add("class", "alert alert-danger")
                            lbExistingCust.Text = "Currently no time slot available"
                            Exit Sub
                        End If
                    End If

                    If Now <= SlotTime Then

                        If isAvailable = "0" Then
                            count = count + 1
                        Else
                            Dim bookingScheduleDateId As String = Common.GetBookingScheduleDateId(System.DateTime.Now, Utils.ParseInt(BookingScheduleID))
                            Dim dateTime As String = System.DateTime.Now.ToString("D") & " " & SlotTime

                            BindTableForBookingScheduleDate(Utils.ParseInt(bookingScheduleDateId), Utils.ParseInt(BookingScheduleID), SlotTime)

                            Dim tablejoin As String = objCommon.GetAllValue(ddlTableSet)

                            If tablejoin = "" Then
                                tablejoin = "0"
                            End If

                            Dim sum As Integer = 0
                            Dim ds3 As New DataSet()
                            Dim oColSqlPar3 As ColSqlparram = New ColSqlparram()
                            oColSqlPar3.Add("@tableid", tablejoin)
                            ds3 = oClsDataccess.GetdatasetSp("P_Get_CompareCover", oColSqlPar3, "P_Get_CompareCover")

                            If ds3.Tables.Count > 0 Then
                                Dim maxcover As Integer = ds3.Tables(0).Rows(0)("MaxCover").ToString()
                                sum = maxcover
                            End If
                            If (sum < noOfCovers) Then
                                If i = noofrow Then
                                    divMessageBox.Visible = True
                                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                                    lbExistingCust.Text = "People are not cover in available tables"
                                    Exit For
                                End If
                                If count = 2 Then
                                    divMessageBox.Visible = True
                                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                                    lbExistingCust.Text = "People are not cover in available tables"
                                    Exit For
                                End If
                                count = count + 1
                            Else
                                Session("wbdate") = dateTime.ToString()
                                Session("wbBSettingsID") = settingsId.ToString()
                                Session("wbBookingScheduleDateId") = bookingScheduleDateId.ToString()
                                Session("wbOurStoreId") = StoreId.ToString()
                                Session("wbcovers") = noOfCovers

                                Dim script As String = "function f(){$find(""" + RadWindow_Waitlist.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                                lblLocationW.Text = hdnStoreName.Value.ToString()
                                lblScheduleW.Text = BookingScheduleName
                                lblBookingdateW.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
                                lblBookingtimeW.Text = SlotTime.ToString()
                                lblTableW.Text = noOfCovers

                                Session("TCover") = noOfCovers
                                Session("Tdatetime") = dateTime
                                Session("TSloatTime") = SlotTime
                                Session("TSettingId") = settingsId
                                Session("TBScheduleId") = BookingScheduleID.ToString()
                                Session("TBScheduleDateId") = bookingScheduleDateId.ToString()
                                Session("TStoreId") = StoreId
                                Session("TComment") = hdncomment.Value.ToString()
                                Session("Twaitingid") = hdnwaitingid.Value.ToString()
                                Session("Twaitingno") = hdnwaitingno.Value.ToString()
                                Session("Taccountno") = hdnaccountid.Value.ToString()

                                Exit For
                            End If
                        End If
                        If count = 2 Then
                            divMessageBox.Visible = True
                            divMessageBox.Attributes.Add("class", "alert alert-danger")
                            lbExistingCust.Text = "All tables are full"
                            Exit For
                        End If
                    End If
                Next
            End If
            'BindWaitingList()
            'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub BindTableForBookingScheduleDate(ByVal BookingScheduleDateId As Int32, ByVal BookingScheduleID As Int32, ByVal bookingtime As String)
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNamesByBookingScheduleDateId(BookingScheduleDateId, BookingScheduleID, bookingtime)
            If ds.Tables.Count > 0 Then
                ddlTableSet.DataSource = ds
                ViewState("ds") = ds
                ddlTableSet.DataTextField = "table_name"
                ddlTableSet.DataValueField = "table_id"
                ddlTableSet.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindMaxCovers(BookingScheduleID As Integer)
        Dim drSchedule As DataRow = Common.GetOnlineMaxCover(Utils.ParseInt(BookingScheduleID))
        If drSchedule IsNot Nothing Then
            If IsOnline Then
                Dim maxCover As Int32 = Utils.ParseInt(drSchedule("OnlineMaxCovers"))
                'regNoOfCovers.MaximumValue = maxCover
            End If
        End If

    End Sub
    Public Sub sendSMS(mailserver As String, user As String, Password As String, mobile As String, Message As String, portsender As String)
        'Prepare you post parameters
        Dim sbPostData As New StringBuilder()
        sbPostData.AppendFormat("user={0}", user)
        sbPostData.AppendFormat("&password={0}", Password)
        sbPostData.AppendFormat("&PhoneNumber={0}", mobile)
        sbPostData.AppendFormat("&text={0}", Message.Replace("_", "+"))
        sbPostData.AppendFormat("&sender={0}", portsender)
        sbPostData.AppendFormat("&flash={0}", 0)
        sbPostData.AppendFormat("&unicode={0}", 0)
        sbPostData.AppendFormat("&track={0}", 1)
        Try
            'Call Send SMS API
            Dim sendSMSUri As String = mailserver '"http://sms.ssdindia.com/sendhttp.php"
            'Create HTTPWebrequest
            Dim httpWReq As HttpWebRequest = DirectCast(WebRequest.Create(sendSMSUri), HttpWebRequest)
            'Prepare and Add URL Encoded data
            Dim encoding As New UTF8Encoding()
            Dim data As Byte() = encoding.GetBytes(sbPostData.ToString())
            'Specify post method
            httpWReq.Method = "POST"
            httpWReq.ContentType = "application/x-www-form-urlencoded"
            httpWReq.ContentLength = data.Length
            Using stream As Stream = httpWReq.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using
            ''''Get the response
            Dim response As HttpWebResponse = DirectCast(httpWReq.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim responseString As String = reader.ReadToEnd()

            ''''Close the response
            reader.Close()
            response.Close()
        Catch ex As SystemException
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "MEGERROR", "alert('" + ex.Message.ToString() + "');", True)
        End Try
    End Sub

    Protected Sub gvwaitinglist_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvwaitinglist.NeedDataSource
        Try
            Dim oClsDataccess As New ClsDataccess

            Dim oColSqlPar As ColSqlparram = New ColSqlparram()

            oColSqlPar.Add("@date", Session("rdpDate").ToString(), SqlDbType.DateTime)
            oColSqlPar.Add("@BookingSettingsID", Session("drpTableStoreId").ToString(), SqlDbType.Int)

            Dim dsBookingList As DataSet = oClsDataccess.GetdatasetSp("Get_Waitlist", oColSqlPar, "Waiting Table List")
            gvwaitinglist.DataSource = dsBookingList.Tables(0)
            gvwaitinglist.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim oClsDataccess As New ClsDataccess
        If ddlTableSet.SelectedValue Is Nothing Then
            'lblError.Visible = True
            'lblError.Text = "Please Select Tables From Available Tables"
        Else
            Try
                Dim que As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableSet)

                Dim vals As String() = tablejoin.ToString().Split("#")

                Dim largest As Integer = Integer.MinValue
                Dim smallest As Integer = Integer.MaxValue

                Dim sum As Integer = 0
                Dim ds3 As New DataSet()
                Dim oColSqlPar3 As ColSqlparram = New ColSqlparram()
                oColSqlPar3.Add("@tableid", tablejoin)
                ds3 = oClsDataccess.GetdatasetSp("P_Get_CompareCover", oColSqlPar3, "P_Get_CompareCover")

                If ds3.Tables.Count > 0 Then
                    Dim maxcover As Integer = ds3.Tables(0).Rows(0)("MaxCover").ToString()
                    sum = maxcover
                End If


                For Each element As Integer In vals
                    largest = Math.Max(largest, element)
                    smallest = Math.Min(smallest, element)
                Next
                Console.WriteLine(largest)
                Console.WriteLine(smallest)
                Dim noofcover As Integer

                noofcover = Convert.ToInt32(Session("wbcovers").ToString())

                If sum < noofcover Then
                    divMessageBox1.Visible = True
                    divMessageBox1.Attributes.Add("class", "alert alert-danger")
                    lbExistingCust1.Text = "No of people is lessthan max cover of alloted tables."
                    Exit Sub
                End If

                divMessageBox1.Visible = False

                Dim oDbConnection As New DBConnection()
                Dim query As DataSet = oDbConnection.SelectData("SELECT TableNo FROM M_Table where Table_id = " + smallest.ToString() + "")

                Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                Dim random = New Random()
                Dim strBookingRef = New String(Enumerable.Repeat(chars, 8).[Select](Function(s) s(random.[Next](s.Length))).ToArray())

                Dim oColSqlPar1 As ColSqlparram = New ColSqlparram()

                oColSqlPar1.Add("@covers", Session("TCover").ToString(), SqlDbType.Int)
                oColSqlPar1.Add("@date", Utils.ParseDateTime(Session("Tdatetime").ToString()), SqlDbType.SmallDateTime)
                oColSqlPar1.Add("@BookingRef", strBookingRef)
                oColSqlPar1.Add("@Bookingtime", Session("TSloatTime").ToString(), SqlDbType.Time)
                oColSqlPar1.Add("@BSettingsID", Session("TSettingId").ToString(), SqlDbType.Int)
                oColSqlPar1.Add("@CreatedBy", Sessions.UserID)
                oColSqlPar1.Add("@IPAddress", Request.UserHostAddress)
                oColSqlPar1.Add("@BookingScheduleID", Utils.ParseInt(Session("TBScheduleId").ToString()))
                oColSqlPar1.Add("@BookingScheduleDateId", Utils.ParseInt(Session("TBScheduleDateId").ToString()))
                oColSqlPar1.Add("@IsOnline", Utils.ParseBool(IsOnline), SqlDbType.Bit)
                oColSqlPar1.Add("@OurStoreId", Session("TStoreId").ToString(), SqlDbType.Int)
                oColSqlPar1.Add("@accountid", Convert.ToInt32(Session("Taccountno").ToString()), SqlDbType.Int)
                oColSqlPar1.Add("@comment", Session("TComment").ToString())
                oColSqlPar1.Add("@waitingid", Convert.ToInt32(Session("Twaitingid").ToString()))
                oColSqlPar1.Add("@waitingno", Convert.ToInt32(Session("Twaitingno").ToString()))


                'oColSqlPar1.Add("@covers", Session("wbcovers").ToString(), SqlDbType.Int)
                'oColSqlPar1.Add("@date", Utils.ParseDateTime(Session("wbdate").ToString()), SqlDbType.SmallDateTime)
                'oColSqlPar1.Add("@BookingRef", strBookingRef)
                'oColSqlPar1.Add("@Bookingtime", lblBookingtimeW.Text, SqlDbType.Time)
                'oColSqlPar1.Add("@BSettingsID", Session("wbBSettingsID").ToString(), SqlDbType.Int)
                'oColSqlPar1.Add("@CreatedBy", Sessions.UserID)
                'oColSqlPar1.Add("@IPAddress", Request.UserHostAddress)
                'oColSqlPar1.Add("@BookingScheduleID", Session("BScheduleId").ToString(), SqlDbType.Int)
                'oColSqlPar1.Add("@BookingScheduleDateId", Val(Session("wbBookingScheduleDateId").ToString()), SqlDbType.Int)
                'oColSqlPar1.Add("@IsOnline", Utils.ParseBool(IsOnline), SqlDbType.Bit)
                'oColSqlPar1.Add("@OurStoreId", Session("wbOurStoreId").ToString(), SqlDbType.Int)

                Dim ds2 As New DataSet()

                ds2 = oClsDataccess.GetdatasetSp("P_Set_WaitingList", oColSqlPar1, "P_Set_WaitingList")


                Dim msg As String = ds2.Tables(0).Rows(0)("Booking").ToString()


                que = "Update Bookings Set Allotted_Tables='" & tablejoin.ToString() & "', TableNo='" & query.Tables(0).Rows(0)("TableNo").ToString() & "' WHERE bookingref = '" & strBookingRef & "'"
                conn.ExecuteNonQuery(que)

                que = "if exists (select 1 from M_OpenTable where bookingref = '" & strBookingRef & "') begin update Tables set TableNumber = " + query.Tables(0).Rows(0)("TableNo").ToString() + " where TableID = (select tableid from M_OpenTable where bookingref = '" & strBookingRef & "') end"
                conn.ExecuteNonQuery(que)

                'Bind()
                'lblSuccess.Text = "Booking Updated Successfully"

                Session("msgbox_Val") = "Update"

                'If msg = "Sucess" Then
                '    Session("success") = "Booking success"
                '    'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Booking success')", True)
                '    'Exit For   
                'End If
                If msg = "Sucess" Then
                    Session("wls1") = "Booking success"
                    Session("wls2") = "1"

                    Dim user As String
                    Dim Password As String
                    Dim mailserver As String
                    Dim portsender As String
                    Dim mobile As String
                    Dim Message As String
                    Dim waiting_no As String
                    Dim waiting_id As String
                    'Dim Bookingref As String

                    Dim smsDS As New DataSet()
                    Dim oClsDatacces_sms As New ClsDataccess
                    Dim oColSqlPar_sms As ColSqlparram = New ColSqlparram()

                    oColSqlPar_sms.Add("@status", 1, SqlDbType.Int)
                    smsDS = oClsDatacces_sms.GetdatasetSp("Get_SMS_Settings", oColSqlPar_sms, "Get_SMS_Settings")


                    mailserver = smsDS.Tables(0).Rows(0)("MailServer").ToString()
                    user = smsDS.Tables(0).Rows(0)("MailServer_Username").ToString()
                    Password = smsDS.Tables(0).Rows(0)("MailServer_Password").ToString()
                    portsender = smsDS.Tables(0).Rows(0)("MailServer_Port").ToString()

                    Dim smsDetail As New DataSet()
                    Dim oClsDatacces_smsDetail As New ClsDataccess
                    Dim oColSqlPar_smsDetail As ColSqlparram = New ColSqlparram()

                    oColSqlPar_smsDetail.Add("@accountid", Session("Taccountno").ToString(), SqlDbType.Int)
                    oColSqlPar_smsDetail.Add("@BookingSettingsID", Session("TSettingId").ToString(), SqlDbType.Int)
                    oColSqlPar_smsDetail.Add("@waiting_id", Session("Twaitingid").ToString(), SqlDbType.Int)
                    smsDetail = oClsDatacces_smsDetail.GetdatasetSp("Get_SMS_Detail", oColSqlPar_smsDetail, "Get_SMS_Detail")

                    For j = 0 To smsDetail.Tables(0).Rows.Count - 1
                        mobile = smsDetail.Tables(0).Rows(j)("mobile").ToString()
                        waiting_no = smsDetail.Tables(0).Rows(j)("waiting_no").ToString()
                        waiting_id = smsDetail.Tables(0).Rows(j)("waiting_id").ToString()
                        Message = "Thanks_for_your_patience_you_are_now_number_" & waiting_no & "_on_the_list_and_we_will_have_a_table_very_shortly."
                        sendSMS(mailserver, user, Password, mobile, Message, portsender)
                    Next
                End If

                Dim script As String = "function f(){$find(""" + RadWindow_Waitlist.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

                Session("wbdate") = Nothing
                Session("wbBSettingsID") = Nothing
                Session("wbBookingScheduleDateId") = Nothing
                Session("wbOurStoreId") = Nothing
                Session("wbcovers") = Nothing

                'LoadTableBookingByDate()
                'Bindchart()
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Dim script As String = "function f(){$find(""" + RadWindow_Waitlist.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

        Session("wbdate") = Nothing
        Session("wbBSettingsID") = Nothing
        Session("wbBookingScheduleDateId") = Nothing
        Session("wbOurStoreId") = Nothing
        Session("wbcovers") = Nothing
    End Sub
End Class
