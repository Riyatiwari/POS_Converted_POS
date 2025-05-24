Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_PopupEditBooking
    Inherits System.Web.UI.Page

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

    Public Property DepartureDate() As String
        Get
            If (Not ViewState("DepartureDate") Is Nothing) Then
                Return ViewState("DepartureDate")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("DepartureDate") = value
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

    Public Property RoomTypeID() As Int32
        Get
            If (Not ViewState("RoomTypeID") Is Nothing) Then
                Return ViewState("RoomTypeID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("RoomTypeID") = value
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
        If (Not Page.IsPostBack()) Then
            If Request.QueryString("venueid") IsNot Nothing AndAlso Request.QueryString("customerid") IsNot Nothing AndAlso Request.QueryString("bookingref") IsNot Nothing Then

                VenueID = Convert.ToInt32(Request.QueryString("venueid"))
                CustomerID = Convert.ToInt32(Request.QueryString("customerid"))
                BookingRefNo = Request.QueryString("bookingref")
                BookingSettingID = Convert.ToInt32(Request.QueryString("bookingsettingid"))

                BindVenueName()
            End If


        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindRepeater()
    End Sub

    Private Sub BindRepeater()

        ArrivalDate = dtpArrival.SelectedDate
        DepartureDate = dtpDeparture.SelectedDate


        'lblRoomsOf.Text = ddlVenue.SelectedText

        Dim que As String = ""
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRoomTypes')DROP TABLE StoreRoomTypes" & Environment.NewLine
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms" & Environment.NewLine
        que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)" & Environment.NewLine
        que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)" & Environment.NewLine
        que &= " SELECT B.RoomID,B.bookingid,B.date,S.StoreID" & Environment.NewLine
        que &= " FROM Bookings B" & Environment.NewLine
        que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID" & Environment.NewLine
        que &= " left outer join StoreMaster SM ON SM.OurStoreId = BS.StoreID" & Environment.NewLine
        que &= " INNER JOIN Store S ON SM.OtherStoreId = S.StoreID" & Environment.NewLine
        que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')" & Environment.NewLine
        que &= " AND (date BETWEEN '" + dtpArrival.SelectedDate.Value.ToString("s") + "' AND '" + dtpDeparture.SelectedDate.Value.ToString("s") + "') " & Environment.NewLine
        que &= " AND IsCanceled = 0" & Environment.NewLine
        'que &= " AND S.VenueID = '" + VenueID.ToString() + "'"
        que &= " AND BS.BookingSettingsid = '" + BookingSettingID.ToString() + "'"

        que &= " CREATE TABLE StoreRoomTypes(roomtypeid int,storeid int)" & Environment.NewLine
        que &= " INSERT INTO StoreRoomTypes(roomtypeid,storeid)" & Environment.NewLine
        que &= " SELECT DISTINCT P.ParentID,PS.StoreID" & Environment.NewLine
        que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.IsParent = 0 AND P.INACTIVE = 0" & Environment.NewLine
        que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID" & Environment.NewLine
        que &= " left outer join StoreMaster SM ON S.StoreID = SM.OtherStoreId" & Environment.NewLine
        que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId" & Environment.NewLine
        que &= " WHERE BS.IsActive = 1 AND P.ProdSort = BS.Sort AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')" & Environment.NewLine
        que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = PS.StoreID)" & Environment.NewLine

        'que &= " AND S.VenueID = '" + VenueID.ToString() + "'"
        que &= " AND BS.BookingSettingsid = '" + BookingSettingID.ToString() + "'"

        que &= " SELECT DISTINCT P.ProductID as RoomTypeID,P.Name as RoomType,cast(Price1Eachsize_1 *.01 as Decimal (10,2))as price, S.name StoreName,S.StoreId" & Environment.NewLine
        que &= " FROM StoreRoomTypes SRT INNER JOIN Product P ON SRT.roomtypeid = P.ProductID" & Environment.NewLine
        que &= " INNER JOIN Store S ON SRT.storeid = S.StoreID" & Environment.NewLine
        que &= " INNER JOIN ProdStore PS ON SRT.roomtypeid = PS.ProductID AND SRT.StoreID = S.StoreID" & Environment.NewLine
        que &= " DROP TABLE StoreRoomTypes" & Environment.NewLine
        que &= " drop table bookedrooms" & Environment.NewLine

        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData(que)
        repRooms.DataSource = ds.Tables(0)
        repRooms.DataBind()

        If (repRooms.Items.Count = 0) Then
            divMessageBox.Attributes.Add("class", "alert alert-info")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Info!</strong> No Record found."
        Else
            divMessageBox.Attributes.Add("class", "")
            divMessageBox.Visible = False
            lblMessageBox.Text = "<strong>Info!</strong> No Record found."
        End If

    End Sub

    Protected Sub dtpArrival_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpArrival.SelectedDateChanged
        dtpDeparture.SelectedDate = Convert.ToDateTime(dtpArrival.SelectedDate).AddDays(1)
    End Sub

    Protected Sub repRooms_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles repRooms.ItemCommand
        If e.CommandName = "BOOKNOW" Then
            Dim cmdArgu As String = e.CommandArgument

            ArrivalDate = dtpArrival.SelectedDate
            DepartureDate = dtpDeparture.SelectedDate

            StoreID = cmdArgu.Split(","c)(0)
            RoomTypeID = cmdArgu.Split(","c)(1)

            pnlExtraServices.Visible = True
            pnlSearch.Visible = False
            BindProduct()
        End If
    End Sub

    Public Sub BindProduct()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetProducts(VenueID)
        gvServices.DataSource = ds.Tables(0)
        gvServices.DataBind()

    End Sub

    Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click
        Dim objCommon As Common = New Common()
        Dim conn As DBConnection = New DBConnection()

        Dim drSetting As DataRow = objCommon.GetBookingSettings(VenueID)
        Dim strQvuery As String = "select * from BookingSchedule where BookingSettingsID = '" + drSetting("BookingSettingsid").ToString() + "'"
        Dim drSchedule As DataRow = conn.SingleRow(strQvuery)
        If gvServices.Items.Count > 0 Then
            For Each gvItem As GridDataItem In gvServices.Items
                Dim chkSelect As CheckBox = CType(gvItem.FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked Then
                    Dim dr As DataRow = BookingService.NewRow()
                    dr("bookingref") = String.Empty
                    dr("ProductID") = gvItem.GetDataKeyValue("ProductID").ToString()
                    Dim Qty As Integer = Utils.ParseInt(CType(gvItem.FindControl("ddlQuantity"), RadDropDownList).SelectedValue)
                    Dim UnitPrice As Decimal = Utils.ParseDecimal(CType(gvItem.FindControl("lblPrice"), Label).Text)
                    dr("Quantity") = Qty
                    dr("Price") = UnitPrice
                    dr("TotalPrice") = (Qty * UnitPrice)
                    BookingService.Rows.Add(dr)
                End If
            Next

        End If

        Dim serviceTotal As Decimal = 0
        If BookingService.Rows.Count > 0 Then
            serviceTotal = Utils.ParseDecimal(BookingService.Compute("Sum(TotalPrice)", ""))
        End If

        'Dim strRoomType As String = Request.QueryString("RoomType").ToString()
        Dim str As String = "SELECT Store.Name, CAST(ProdStore.Price1Eachsize_1 *.01 as Decimal (10,2))as BasePricePrice_1 FROM Store INNER JOIN ProdStore ON Store.StoreID = ProdStore.StoreID WHERE Store.StoreID = '" + StoreID.ToString() + "' AND ProductID = '" + RoomTypeID.ToString() + "'"
        Dim ds As DataSet = conn.SelectData(str)


        Dim noOfNights As String = DateDiff(DateInterval.Day, Utils.ParseDateTime(ArrivalDate), Utils.ParseDateTime(DepartureDate)).ToString()
        Dim roomPrice As String = (Utils.getInteger(noOfNights) * Utils.NullToString(ds.Tables(0).Rows(0)("BasePricePrice_1")))
        Dim totalAmount As Decimal = Utils.ParseDecimal(roomPrice) + serviceTotal


        ' Fetch RoomID Dynamically
        Dim roomId As String = objCommon.GetRoomNo(VenueID, Convert.ToDateTime(ArrivalDate), Convert.ToDateTime(DepartureDate), Utils.ParseInt(RoomTypeID))

        If roomId <> String.Empty Then
            'First delete old booking
            DeleteOldBooking()

            ' If RoomList has more than 1 row then select roomid randomly
            ' If Room Available then create booking and its services

            objCommon.CreateBooking(1, 0, txtComment.Text, Utils.ParseInt(roomId), ArrivalDate, DepartureDate, 0, BookingRefNo, Utils.ParseInt(roomPrice), Utils.ParseDecimal(txtAmount.Text),
                                                    CustomerID, 0, TimeSpan.Zero, Utils.ParseInt(drSetting("BookingSettingsid")), False, Sessions.UserID, Request.UserHostAddress, totalAmount, drSchedule("BookingScheduleID"), Nothing, False, VenueID().ToString())

            If BookingService.Rows.Count > 0 Then
                For i = 0 To BookingService.Rows.Count - 1
                    BookingService.Rows(i)("bookingref") = BookingRefNo
                Next
                BookingService.AcceptChanges()
                Common.SaveBookingServices(BookingService)
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        Else
            ' Else Message For No Room Available
            divNoRooms.Visible = True
            divNoRooms.Attributes.Add("class", "alert alert-danger")
            lblNoRooms.Text = "All Rooms Of This Room Type Are Booked Just Before Sometime. Please Select Another Room Type"
        End If
    End Sub

    Private Sub DeleteOldBooking()
        Dim conn As DBConnection = New DBConnection()

        Dim query As String = String.Empty
        query &= " DELETE FROM bookings WHERE bookingref = '" & BookingRefNo & "'"
        query &= " DELETE FROM BookingServices WHERE bookingref = '" & BookingRefNo & "'"

        conn.Ins_Upd_Del(query)
    End Sub

    Private Sub BindVenueName()
        Dim conn As DBConnection = New DBConnection()

        Dim query As String = String.Empty
        'query &= " SELECT * FROM Venue WHERE VenueID = '" & VenueID & "'"
        query &= " SELECT V.StoreName as name FROM StoreMaster V Inner Join bookingsettings BS ON BS.StoreID = V.OurStoreId WHERE BS.BookingSettingsid =  '" & BookingSettingID & "'"

        Dim ds As DataSet = conn.SelectData(query)
        Dim dt As DataTable = ds.Tables(0)
        lblVenueName.Text = dt.Rows(0)("Name").ToString()
        lblRoomsOf.Text = dt.Rows(0)("Name").ToString()
    End Sub
End Class
