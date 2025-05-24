Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Partial Class BookingEasy_PopupEditBookingTableData
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

    Public Property BookingRefNo() As String
        Get
            If (Not ViewState("BookingRefNo") Is Nothing) Then
                Return ViewState("BookingRefNo")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("BookingRefNo") = value
        End Set
    End Property

    Public Property CustomerID() As Int32
        Get
            If (Not ViewState("CustomerID") Is Nothing) Then
                Return ViewState("CustomerID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("CustomerID") = value
        End Set
    End Property

    Public Property VenueID() As Int32
        Get
            If (Not ViewState("VenueID") Is Nothing) Then
                Return ViewState("VenueID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("VenueID") = value
        End Set
    End Property

    Public Property StoreID() As Int32
        Get
            If (Not ViewState("StoreID") Is Nothing) Then
                Return ViewState("StoreID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("StoreID") = value
        End Set
    End Property

    Public Property BookingSettingID() As Int32
        Get
            If (Not ViewState("BookingSettingID") Is Nothing) Then
                Return ViewState("BookingSettingID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("BookingSettingID") = value
        End Set
    End Property

    Public Property ArrivalDate() As String
        Get
            If (Not ViewState("ArrivalDate") Is Nothing) Then
                Return ViewState("ArrivalDate")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("ArrivalDate") = value
        End Set
    End Property

    Public Property NoOfCover() As Int32
        Get
            If (Not ViewState("NoOfCover") Is Nothing) Then
                Return ViewState("NoOfCover")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("NoOfCover") = value
        End Set
    End Property

    Public Property BookingService() As DataTable
        Get
            If (ViewState("BookingService") IsNot Nothing) Then
                Return DirectCast(ViewState("BookingService"), DataTable)
            End If
            Dim conn As DBConnection = New DBConnection()
            Dim dt As DataTable = conn.SelectData("select * from BookingServices where BookingServiceID = -1").Tables(0)
            dt.Columns("BookingServiceID").AutoIncrement = True
            'dt.Columns.Add("ProductName")
            ViewState("BookingService") = dt
            Return dt
        End Get
        Set(ByVal value As DataTable)
            ViewState("BookingService") = value
        End Set
    End Property

    Public Property BookingScheduleID() As Integer
        Get
            If ViewState("bookingScheduleId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleId") = value
        End Set
    End Property

    Public Property BookingScheduleDateId() As Integer
        Get
            If ViewState("bookingScheduleDateId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleDateId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleDateId") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Not Page.IsPostBack()) Then
                If Request.QueryString("customerid") IsNot Nothing AndAlso Request.QueryString("bookingref") IsNot Nothing AndAlso Request.QueryString("bookingsettingid") IsNot Nothing Then
                    If Request.QueryString("customerid") <> "" AndAlso Request.QueryString("bookingref") <> "" Then
                        'VenueID = Convert.ToInt32(Request.QueryString("venueid"))
                        CustomerID = Convert.ToInt32(Request.QueryString("customerid"))
                        BookingRefNo = Request.QueryString("bookingref")
                        BookingSettingID = Convert.ToInt32(Request.QueryString("bookingsettingid"))
                        Bind()
                        BindVenueName()
                        BindType()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub BindVenueName()
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim query As String = String.Empty
            'query &= " SELECT * FROM Venue WHERE VenueID = '" & VenueID & "'"
            query &= " SELECT V.StoreName as name FROM StoreMaster V Inner Join bookingsettings BS ON BS.StoreID = V.OurStoreId WHERE BS.BookingSettingsid = '" & BookingSettingID & "'"

            Dim ds As DataSet = conn.SelectData(query)
            Dim dt As DataTable = ds.Tables(0)
            lblVenue.Text = dt.Rows(0)("Name").ToString()
            lblTablesOf.Text = dt.Rows(0)("Name").ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindType()
        Try
            Dim obj As Common = New Common()
            Dim ds As DataSet = obj.GetTableType(BookingSettingID, dtpDate.SelectedDate)
            If ds IsNot Nothing Then
                If ds.Tables.Count > 0 Then
                    ddlType.DataSource = ds.Tables(0)
                    ddlType.DataTextField = "Name"
                    ddlType.DataValueField = "Id"
                    ddlType.DataBind()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Bind()
        Try

            Dim que As String = "  "
            que += " SELECT SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid, BSc.GroupID, B.covers,"
            que += " A.FirstName+' '+A.LastName AS FullName, B.covers, convert(nvarchar(10),B.date,103) AS arrivaldate, B.bookingref, B.bookingtime, BSc.Name AS ScheduleName,B.date,convert(nvarchar(5),bookingtime,108) as time "
            que += " FROM bookings B INNER JOIN Account A ON B.accountid = A.accountid left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId  "
            que += " INNER JOIN BookingSchedule BSc ON BSc.BookingScheduleID = b.BookingScheduleID "
            que += " WHERE B.bookingref = '" + BookingRefNo.ToString() + "'"
            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)
            If ds.Tables(0).Rows.Count > 0 Then
                lblBookingRef.Text = ds.Tables(0).Rows(0)("bookingref").ToString()
                lblCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                lblDate.Text = ds.Tables(0).Rows(0)("arrivaldate").ToString()
                lblTime1.Text = ds.Tables(0).Rows(0)("time").ToString()
                lblLocation.Text = ds.Tables(0).Rows(0)("Location").ToString()
                lblSchedule.Text = ds.Tables(0).Rows(0)("ScheduleName").ToString()
                dtpDate.SelectedDate = ds.Tables(0).Rows(0)("date")
                txtNoOfCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    'Private Sub Bind()
    '    Try
    '        Dim query As String = String.Empty
    '        query &= " select date,covers from bookings where bookingref = '" + BookingRefNo.ToString() + "'"
    '        Dim ds As DataSet = conn.SelectData(query)
    '        Dim dt As DataTable = ds.Tables(0)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindRepeater()
            btnConfitmBooking.Visible = False
            lblTime.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindRepeater()
        Try
            divMessageBox.Visible = False

            Dim dt As DataTable = Common.SearchTableGetStore(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), "01:00", BookingSettingID.ToString(), ddlType.SelectedValue)
            If (dt.Rows.Count > 0) Then
                repTables.DataSource = dt
                repTables.DataBind()
                repTables.Visible = True
            Else
                repTables.Visible = False
                divMessageBox.Visible = True
                divMessageBox.Attributes.Add("class", "alert alert-danger")
                lblMessageBox.Text = "No tables found under this date-time"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub repTables_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repTables.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim repTimeSlot As Repeater = DirectCast(e.Item.FindControl("repTimeSlot"), Repeater)
                Dim timeSlotId As String = DataBinder.Eval(e.Item.DataItem, "TimeSlotId").ToString()
                Dim settingsId As String = DataBinder.Eval(e.Item.DataItem, "SettingID").ToString()

                Dim noOfCovers As String
                noOfCovers = txtNoOfCovers.Text

                repTimeSlot.DataSource = Common.SearchTableGetTimeSlots(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), settingsId, noOfCovers, Utils.ParseInt(ddlType.SelectedValue), BookingRefNo, False)
                repTimeSlot.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub repTimeSlot_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        Try
            If (e.CommandName = "BookTable") Then
                btnConfitmBooking.Visible = True
                lblTime.Visible = True
                lblTime.Text = "Selected Time : " + e.CommandArgument.ToString()

                StoreID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnStoreId"), HiddenField).Value)
                BookingSettingID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnSettingId"), HiddenField).Value)
                NoOfCover = Utils.ParseInt(txtNoOfCovers.Text)
                ArrivalDate = (dtpDate.SelectedDate & " " & e.CommandArgument.ToString()).ToString()
                BookingScheduleID = Utils.ParseInt(ddlType.SelectedValue)
                BookingScheduleDateId = Utils.ParseInt(Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)))
                'StoreID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnStoreId"), HiddenField).Value)
                'BookingSettingID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnSettingId"), HiddenField).Value)
                'ArrivalDate = (dtpDate.SelectedDate & " " & e.CommandArgument.ToString()).ToString()
                'NoOfCover = Utils.ParseInt(txtNoOfCovers.Text)
                'pnlSearch.Visible = False
                'BookingScheduleID = Utils.ParseInt(ddlType.SelectedValue)
                'BookingScheduleDateId = Utils.ParseInt(Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()
            Dim bookingDate As DateTime = Convert.ToDateTime(ArrivalDate)
            Dim drSetting As DataRow = objCommon.GetBookingSettings(StoreID)
            Dim query As String = String.Empty
            query &= " UPDATE bookings SET covers = " + NoOfCover.ToString() + ", date = '" + bookingDate.ToString("MM/dd/yyyy") + "', bookingtime = '" + TimeSpan.Parse(bookingDate.ToString("HH:mm")).ToString() + "', BookingSettingsid = " + drSetting("BookingSettingsid").ToString() + ", BookingScheduleID =" + BookingScheduleID.ToString() + ", BookingScheduleDateId =" + BookingScheduleDateId.ToString() + ", OurStoreId = " + StoreID.ToString() + " WHERE bookingref = '" & BookingRefNo & "'"
            conn.Ins_Upd_Del(query)
            Session("msgbox_Val") = "Update"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location.reload();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub DeleteOldBooking()
    '    Dim conn As DBConnection = New DBConnection()

    '    Dim query As String = String.Empty
    '    query &= " DELETE FROM bookings WHERE bookingref = '" & BookingRefNo & "'"

    '    conn.Ins_Upd_Del(query)
    'End Sub

    Protected Sub dtpDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpDate.SelectedDateChanged
        Try
            BindType()
            btnConfitmBooking.Visible = False
            lblTime.Visible = False
        Catch ex As Exception

        End Try        
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlType.SelectedIndexChanged
        Try
            btnConfitmBooking.Visible = False
            lblTime.Visible = False
        Catch ex As Exception

        End Try
    End Sub
End Class
